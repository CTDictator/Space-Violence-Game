using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Display all information about the planet to the user.
public class PlanetaryInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalShipsText;
    [SerializeField] private PlanetType planetType;
    [SerializeField] private PlanetModifier[] planetModifiers;

    private PlanetaryProduction planetaryProduction;
    private const int maxAllowedModifiers = 3;
    private const int maxModifierCategories = 9;

    private void Awake()
    {
        // A planet always has a type, selects one at random.
        planetType = (PlanetType)Random.Range(0, (int)PlanetType.max_planet_types);
        // Can have 1-3 modifiers.
        int numModifiers = Random.Range(1, maxAllowedModifiers);
        planetModifiers = new PlanetModifier[numModifiers];
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
            int modifierCategory;
            bool reroll = false;
            do
            {
                reroll = false;
                modifierCategory = Random.Range(1, maxModifierCategories+1);
                // Cycle through the numbers used to ensure no duplicates exist.
                for (int j = 0; j < numbersUsed.Length; j++)
                {
                    if (modifierCategory == numbersUsed[j])
                    {
                        reroll = true;
                    }
                }
            } while (reroll);
            numbersUsed[i] = modifierCategory;
            bool coinFlip = System.Convert.ToBoolean(Random.Range(0, 2));
            switch (modifierCategory)
            {
                case 1:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.low_gravity;
                    else planetModifiers[i] = PlanetModifier.high_gravity;
                    break;
                case 2:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.thin_atmosphere;
                    else planetModifiers[i] = PlanetModifier.thick_atmosphere;
                    break;
                case 3:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.poor_infrastructure;
                    else planetModifiers[i] = PlanetModifier.good_infrastructure;
                    break;
                case 4:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.weak_magnetic_field;
                    else planetModifiers[i] = PlanetModifier.strong_magnetic_field;
                    break;
                case 5:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.high_popular_support;
                    else planetModifiers[i] = PlanetModifier.high_social_unrest;
                    break;
                case 6:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.low_corruption;
                    else planetModifiers[i] = PlanetModifier.high_corruption;
                    break;
                case 7:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.mineral_poor;
                    else planetModifiers[i] = PlanetModifier.mineral_rich;
                    break;
                case 8:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.sparcely_populated;
                    else planetModifiers[i] = PlanetModifier.densely_populated;
                    break;
                case 9:
                    if (coinFlip) planetModifiers[i] = PlanetModifier.low_polution;
                    else planetModifiers[i] = PlanetModifier.high_polution;
                    break;
                default:
                    planetModifiers[i] = PlanetModifier.none; break;
            }
        }
    }

    // Change the text on the planet accordingly with the ammount of ships ready.
    public void UpdateTotalShipsText()
    {
        totalShipsText.text = $"{planetaryProduction.TotalShips}";
    }
}
