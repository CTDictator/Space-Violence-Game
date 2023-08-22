using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Display all information about the planet to the user.
public class PlanetaryInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalShipsText;
    [SerializeField] private PlanetTypes planetType;
    [SerializeField] private PlanetModifiers[] planetModifiers;

    private PlanetaryProduction planetaryProduction;

    public PlanetTypes PlanetType { get { return planetType; } }
    public PlanetModifiers[] PlanetModifier { get { return planetModifiers; } }

    private void Awake()
    {
        // A planet always has a type, selects one at random.
        planetType = (PlanetTypes)Random.Range(0, (int)PlanetTypes.max_planet_types);
        // Can have 1-3 modifiers.
        int numModifiers = Random.Range(1, Constants.maxAllowedModifiers + 1);
        planetModifiers = new PlanetModifiers[numModifiers];
        AssignPlanetModifiers();
    }

    // Reference the planetary production script.
    private void Start()
    {
        planetaryProduction = GetComponent<PlanetaryProduction>();
        UpdateTotalShipsText();
    }

    // Assign planetary modifiers ensuring each one is unique and mutually exclusive.
    private void AssignPlanetModifiers()
    {
        int[] numbersUsed = new int[planetModifiers.Length];
        for (int i = 0; i < planetModifiers.Length; i++)
        {
            numbersUsed[i] = ProduceUniqueModifier(numbersUsed);
            AssignAModifier(numbersUsed[i], i);
        }
    }

    // Ensure 1 unique category is selected by testing against all previous categories.
    private int ProduceUniqueModifier(int[] numbersUsed)
    {
        int modifierCategory;
        bool reroll;
        do
        {
            reroll = false;
            modifierCategory = Random.Range(1, Constants.maxModifierCategories + 1);
            // Cycle through the numbers used to ensure no duplicates exist.
            for (int j = 0; j < numbersUsed.Length; j++)
            {
                if (modifierCategory == numbersUsed[j])
                {
                    reroll = true;
                }
            }
        } while (reroll);
        return modifierCategory;
    }

    // Read the unique modifier and assign the modifier to the correct index.
    private void AssignAModifier(int uniqueModifier, int i)
    {
        bool coinFlip = System.Convert.ToBoolean(Random.Range(0, 2));
        switch (uniqueModifier)
        {
            case 1:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.low_gravity;
                else planetModifiers[i] = PlanetModifiers.high_gravity;
                break;
            case 2:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.thin_atmosphere;
                else planetModifiers[i] = PlanetModifiers.thick_atmosphere;
                break;
            case 3:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.poor_infrastructure;
                else planetModifiers[i] = PlanetModifiers.good_infrastructure;
                break;
            case 4:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.weak_magnetic_field;
                else planetModifiers[i] = PlanetModifiers.strong_magnetic_field;
                break;
            case 5:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.high_popular_support;
                else planetModifiers[i] = PlanetModifiers.high_social_unrest;
                break;
            case 6:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.low_corruption;
                else planetModifiers[i] = PlanetModifiers.high_corruption;
                break;
            case 7:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.mineral_poor;
                else planetModifiers[i] = PlanetModifiers.mineral_rich;
                break;
            case 8:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.sparcely_populated;
                else planetModifiers[i] = PlanetModifiers.densely_populated;
                break;
            case 9:
                if (coinFlip) planetModifiers[i] = PlanetModifiers.low_polution;
                else planetModifiers[i] = PlanetModifiers.high_polution;
                break;
            default:
                planetModifiers[i] = PlanetModifiers.none; break;
        }
    }

    // Change the text on the planet accordingly with the ammount of ships ready.
    public void UpdateTotalShipsText()
    {
        totalShipsText.text = $"{planetaryProduction.TotalShips}";
    }
}
