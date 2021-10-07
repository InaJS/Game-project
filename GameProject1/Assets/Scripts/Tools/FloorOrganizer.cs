using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorOrganizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer tilePrefab;
    [SerializeField] private GameObject tilesHolder;
    [SerializeField] private float tileScale;
    private List<SpriteRenderer> tiles = new List<SpriteRenderer>();

    public List<SpriteRenderer> Tiles => tiles;

    [Header("Horizontal")] [SerializeField]
    private int horizontalCount;

    [SerializeField] private float horizontalSpacing;

    [Header("Vertical")] [SerializeField] private int vertitalCount;
    [SerializeField] private float vertitalSpacing;

    public void Reorganize()
    {
        for (int i = tilesHolder.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(tilesHolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = 0; j < vertitalCount; j++)
            {
                Vector3 tilePos = tilesHolder.transform.position + new Vector3(i * horizontalSpacing, j * vertitalSpacing, 0);

                SpriteRenderer newTile = Instantiate(tilePrefab, tilePos, Quaternion.identity, tilesHolder.transform);
                newTile.gameObject.transform.localScale = Vector3.one * tileScale;
                newTile.gameObject.name = "(" + i + "," + j + ")";
                tiles.Add(newTile);
            }
        }
    }
}