using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DanceFloorLightingController : MonoBehaviour
{
    [SerializeField] private FloorOrganizer organizer;
    [Range(1,10)] [SerializeField] private int lightGroupCount;
    [SerializeField] private List<LightGroup> tileLights;

    private void Start()
    {
        tileLights.Clear();
        tileLights.Capacity = lightGroupCount;

        List<SpriteRenderer> allTiles = organizer.Tiles;

        int aproxSplit = allTiles.Count/lightGroupCount;

        for (int i = 0; i < lightGroupCount; i++)
        {
            while (tileLights[i].renderers.Count < aproxSplit)
            {
                SpriteRenderer randomTile = allTiles[Random.Range(0, allTiles.Count)];
                tileLights[i].renderers.Add(randomTile);
                allTiles.Remove(randomTile);
            }
        }
    }

    // private void OnValidate() TODO
    // {
    //     tileLights.Capacity = lightGroupCount;
    // }
}

[Serializable]
public class LightGroup
{
    public List<SpriteRenderer> renderers;
}