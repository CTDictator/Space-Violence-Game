using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Spawn in the planets and empires at the start of the game.
public class MapStartup : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private GameObject empire;
    [SerializeField] private GameObject planet;
    [SerializeField] private Transform empireContainer;
    [SerializeField] private Transform planetContainer;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private GameObject restartButton;
    private PlanetTracker planetTracker;
    private EmpireTracker empireTracker;
    private Vector3 randomLocation;

    // Generate the starting game conditions.
    private void Start()
    {
        planetTracker = GetComponent<PlanetTracker>();
        empireTracker = GetComponent<EmpireTracker>();
        GenerateAllEmpires();
        GenerateAllPlanets();
        StartCoroutine(AssignAPlanetForEachEmpire());
    }

    // Generate all the empires within the game.
    private void GenerateAllEmpires()
    {
        for (int i = 0; i < Constants.numEmpires;  i++)
        {
            // Generate a new empire.
            GameObject newEmpire = Instantiate(empire, Vector3.zero, 
                Quaternion.identity, empireContainer);
            // Confirm the empire has a unique colour.
            EnsureUniqueEmpireColour(newEmpire.GetComponent<EmpireProperties>());
            empireTracker.AddEmpire(newEmpire);
        }
    }

    // Look through the list of empires and ensure the colour is unique.
    private void EnsureUniqueEmpireColour(EmpireProperties newEmpire)
    {
        bool reroll;
        do
        {
            reroll = false;
            foreach (var empire in empireTracker.Empires)
            {
                EmpireColour empireColour = empire.GetComponent<EmpireProperties>().Colour;
                if (empireColour == newEmpire.Colour)
                {
                    newEmpire.RollEmpireColour();
                    reroll = true;
                    break;
                }
            }
        } while (reroll);
    }

    // Generate the total number of planets within the maps size.
    private void GenerateAllPlanets()
    {
        for (int i = 0; i < Constants.numPlanets; i++)
        {
            StartCoroutine(NewPlanetLocation());
        }
    }

    // Find a unique location with no planets nearby and then instantiate a world there.
    IEnumerator NewPlanetLocation()
    {
        do
        {
            yield return null;
            randomLocation = NewRandomLocation();
        } while (Physics2D.OverlapCircle(randomLocation, Constants.minPlanetDistance,
            LayerMask.NameToLayer("Confiner")));
        // Add the new planet to the list of worlds.
        CreateNewWorld();
        yield return null;
    }

    // Create a random location on the XY plane within the game boundaries.
    private Vector3 NewRandomLocation()
    {
        return new(Random.Range(-Constants.mapRangeSize, Constants.mapRangeSize),
                Random.Range(-Constants.mapRangeSize, Constants.mapRangeSize), 0.0f);
    }

    // Create the new planet and add it to the list for referencing.
    private void CreateNewWorld()
    {
        GameObject newWorld = Instantiate(planet, randomLocation, Quaternion.identity, planetContainer);
        planetTracker.AddPlanet(newWorld);
    }

    // Take one of the worlds made at random and give it to an active empire.
    private IEnumerator AssignAPlanetForEachEmpire()
    {
        yield return new WaitForSeconds(0.3f);
        int index;
        foreach (var newEmpire in empireTracker.Empires)
        {
            bool reroll;
            do
            {
                reroll = false;
                index = Random.Range(0, planetTracker.Planets.Count);
                // Confirm that the planet doesn't belong to another empire.
                reroll = EnsureUniquePlanet(index);
            } while (reroll);
            newEmpire.GetComponent<EmpireProperties>().ControlPlanet(planetTracker.Planets[index]);
            // Lock the camera onto the player homeworld.
            StartCoroutine(ChangeCameraLocation(index, newEmpire));
        }
    }

    // Reroll the planet if another empire owns the world that isn't neutral.
    private bool EnsureUniquePlanet(int index)
    {
        GameObject planetOwner = planetTracker.Planets[index].GetComponent<PlanetProperties>().Empire;
        if (!planetOwner.GetComponent<EmpireProperties>().Neutral) return true;
        return false;
    }

    private IEnumerator ChangeCameraLocation(int index, GameObject newEmpire)
    {
        yield return new WaitForEndOfFrame();
        if (newEmpire.GetComponent<EmpireProperties>().Player)
        {
            Vector3 camPos = planetTracker.Planets[index].transform.position;
            cam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 1;
            cam.transform.position = new(camPos.x, camPos.y, cam.transform.position.z);
        }
    }

    // Show the restart button on defeat of the player.
    public void ShowRestart()
    {
        restartButton.SetActive(true);
    }

    // Restart the scene.
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
