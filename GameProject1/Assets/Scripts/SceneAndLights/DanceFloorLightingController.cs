using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(FloorOrganizer))]public class DanceFloorLightingController : MonoBehaviour
{
    private FloorOrganizer organizer;
    [SerializeField] private List<LightGroup> tileLights;
    [SerializeField] private float timeOffset;
    [Header("If using random")]
    [SerializeField] private bool useRandomFill;
    [Range(1, 10)] [SerializeField] private int lightGroupCount;

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

        SetGroupLightsTimeOffset();
    }

    private void SetGroupLightsTimeOffset()
    {
        for (int i = 0; i < tileLights.Count; i++)
        {
            foreach (var spriteRenderer in tileLights[i].renderers)
            {
                spriteRenderer.sharedMaterial = tileLights[i].lightGroupMaterial;
                spriteRenderer.sharedMaterial.SetFloat("_TimeOffset", i * timeOffset);
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
    public Material lightGroupMaterial;
}