/*
Creative Commons Attribution-NonCommercial 4.0 International License
https://creativecommons.org/licenses/by-nc/4.0/

This work is licensed under the Creative Commons Attribution-NonCommercial 4.0 International License. To view a copy of this license, visit the link above.

You are free to:
  - Share: copy and redistribute the material in any medium or format
  - Adapt: remix, transform, and build upon the material

Under the following terms:
  - Attribution: You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
  - NonCommercial: You may not use the material for commercial purposes.

Note: Usage of this script is strictly limited to lawful and ethical purposes. It is prohibited to use this script for any malicious, harmful, or illegal activities.
*/
// Made by Commander Soul. My discord is Soul Harvester#5994
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Pixelfactor.IP.Common.Factions;
using Pixelfactor.IP.SavedGames.V2.Editor.EditorObjects;

public class PropertyGeneratorWindow : EditorWindow
{
    private GameObject sectorRoot;
    private GameObject unitRoot; 
    private GameObject faction;
    private int amount = 1; // Default value

    private GUIContent factionTooltip = new GUIContent("Faction", "faction that the property generate will belongs to");
    private GUIContent sectorTooltip = new GUIContent("Sector root folder", "Select the Sector root folder");
    private GUIContent unitTooltip = new GUIContent("Unit root folder", "Select the Unit root folder");
	private string customText = "Made by Commander Soul. My discord is Soul Harvester#5994";

    [MenuItem("Window/Property Generator")]
    public static void ShowWindow()
    {
        PropertyGeneratorWindow window = GetWindow<PropertyGeneratorWindow>("Property Generator Window");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Property Generator Window", EditorStyles.boldLabel);

        faction = (GameObject)EditorGUILayout.ObjectField(factionTooltip, faction, typeof(GameObject), true);
        sectorRoot = (GameObject)EditorGUILayout.ObjectField(sectorTooltip, sectorRoot, typeof(GameObject), true);
        unitRoot = (GameObject)EditorGUILayout.ObjectField(unitTooltip, unitRoot, typeof(GameObject), true);

        amount = EditorGUILayout.IntField("Amount", amount);

        if (sectorRoot == null || unitRoot == null)
        {
            EditorGUILayout.HelpBox("Select sector and unit root folders.", MessageType.Info);
            return;
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Property"))
        {
            GeneratePropertyObjects();
        }
		GUILayout.Label(customText);
    }

    private void GeneratePropertyObjects()
    {
        if (amount < 1)
        {
            Debug.LogWarning("Amount should be greater than or equal to 1.");
            return;
        }

        // Find all EditorSector components in the sector root folder
        EditorSector[] editorSectors = sectorRoot.GetComponentsInChildren<EditorSector>();

        if (editorSectors.Length == 0)
        {
            Debug.LogWarning("No game objects with EditorSector component found in the sector root folder.");
            return;
        }

        Transform[] unitObjects = unitRoot.GetComponentsInChildren<Transform>();

        // Filter out child objects of units (only include the units themselves)
        List<Transform> filteredUnitObjects = new List<Transform>();
        foreach (Transform unitObject in unitObjects)
        {
            if (unitObject.parent == unitRoot.transform)
            {
                filteredUnitObjects.Add(unitObject);
            }
        }

        if (filteredUnitObjects.Count == 0)
        {
            Debug.LogWarning("No units found in the unit root folder.");
            return;
        }

        System.Random random = new System.Random();

        for (int i = 0; i < amount; i++)
        {
            // Access the EditorFaction component on the faction GameObject
            EditorFaction editorFactionComponent = faction.GetComponent<EditorFaction>();
            // Filter eligible sectors based on the HomeSectorTransform condition
            List<EditorSector> eligibleSectors = editorSectors
                .Where(editorSector => editorFactionComponent.HomeSectorTransform == editorSector.transform)
                .ToList();

            if (eligibleSectors.Count == 0)
            {
                Debug.LogWarning("No eligible sectors found for the faction's HomeSectorTransform.");
                return;
            }

            // Choose a random eligible sector
            int randomIndex = random.Next(eligibleSectors.Count);
            EditorSector randomSector = eligibleSectors[randomIndex];

            foreach (EditorSector editorSector in eligibleSectors)
            {
                // Choose a random unit object
                int randomUnitIndex = random.Next(filteredUnitObjects.Count);
                Transform randomUnitObject = filteredUnitObjects[randomUnitIndex];

                // Instantiate a copy of the random unit object and parent it to the sector object
                GameObject copy = Instantiate(randomUnitObject.gameObject, editorSector.transform);
                copy.name = randomUnitObject.name; // Optionally, you can set the name

                // Reset the position to match the sector object or apply other modifications as needed
                copy.transform.position = editorSector.transform.position;

                // Access the EditorFaction component on the copied object
                EditorUnit editorUnitComponent = copy.GetComponent<EditorUnit>();

                // Check if the EditorFaction component exists on the copied object
                if (editorUnitComponent != null)
                {
                    // Set the Faction field of the copied unit to the EditorFaction component from the faction GameObject
                    editorUnitComponent.Faction = editorFactionComponent;
                    // Get the Transform of the sector object
                    Transform sectorTransform = editorSector.transform;

                    // Check if the HomeSectorTransform of the EditorFaction is the same as the sectorTransform
                    if (editorFactionComponent.HomeSectorTransform == sectorTransform)
                    {
                        // HomeSectorTransform is the same as the sectorTransform
                        Debug.Log($"HomeSectorTransform of {editorFactionComponent.gameObject.name} is the same as {sectorTransform.gameObject.name}");
                    }
                    else
                    {
                        // HomeSectorTransform is not the same as the sectorTransform
                        Debug.Log($"HomeSectorTransform of {editorFactionComponent.gameObject.name} is different from {sectorTransform.gameObject.name}");
                    }
                }
                else
                {
                    Debug.LogWarning("EditorFaction component not found on the copied object.");
                }

                Debug.Log($"Copied {randomUnitObject.name} to {editorSector.name}");
            }
        }
    }

}
