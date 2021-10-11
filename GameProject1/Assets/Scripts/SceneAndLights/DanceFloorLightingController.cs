using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(FloorOrganizer))]
public class DanceFloorLightingController : MonoBehaviour
{
    private FloorOrganizer organizer;
    [SerializeField] private List<LightGroup> tileLights;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;

    [Header("If using random")]
    [SerializeField] private bool useRandomFill;
    [Range(1, 10)] [SerializeField] private int lightGroupCount;
    
    [Header("If using single")]
    [SerializeField] private bool useSingleLight;
    [SerializeField] private Material singleGroupMaterial;

    private void Awake()
    {
        organizer = this.GetComponent<FloorOrganizer>();
    }

    private void Start()
    {
        if (useRandomFill)
        {
            tileLights.Clear();
            tileLights.Capacity = lightGroupCount;

            List<SpriteRenderer> allTiles = organizer.Tiles;
            int aproxSplit = allTiles.Count / lightGroupCount;

            for (int i = 0; i < lightGroupCount; i++)
            {
                while (tileLights[i].renderers.Count < aproxSplit)
                {
                    SpriteRenderer randomTile = allTiles[Random.Range(0, allTiles.Count)];
                    tileLights[i].renderers.Add(randomTile);
                    allTiles.Remove(randomTile);
                }
            }

            while (allTiles.Count > 0)
            {
                int index = Random.Range(0, lightGroupCount);
                tileLights[index].renderers.Add(allTiles[0]);
                allTiles.RemoveAt(0);
            }
        }

        if (useSingleLight)
        {
            tileLights.Clear(); 
            tileLights = new List<LightGroup>();
            tileLights.Add(new LightGroup());
            tileLights[0].renderers = organizer.Tiles;
            tileLights[0].lightGroupMaterial = singleGroupMaterial;
        }

        SetGroupLightsTime();
    }

    private void SetGroupLightsTime()
    {
        for (int i = 0; i < tileLights.Count; i++)
        {
            foreach (SpriteRenderer spriteRenderer in tileLights[i].renderers)
            {
                spriteRenderer.sharedMaterial = tileLights[i].lightGroupMaterial;
            }
        }
    }

    public void SetCorrectColor()
    {
        SetDanceFloorColor(correctColor);
    }
    
    public void SetWrongColor()
    {
        SetDanceFloorColor(wrongColor);
    }

    private void SetDanceFloorColor(Color color)
    {
        singleGroupMaterial.SetColor("_Color", color);
    }
}

[Serializable]
public class LightGroup
{
    public List<SpriteRenderer> renderers;
    public Material lightGroupMaterial;
}