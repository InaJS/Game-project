using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorOrganizer : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileScale;


    [Header("Horizontal")]
    [SerializeField] private int horizontalCount;
    [SerializeField] private float horizontalSpacing;

    [Header("Vertical")]
    [SerializeField] private int vertitalCount;
    [SerializeField] private float vertitalSpacing;


    public void Reorganize()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = 0; j < vertitalCount; j++)
            {
                Vector3 tilePos = transform.position + new Vector3(i*horizontalSpacing, j*vertitalSpacing,0);
                
                GameObject newTile = Instantiate(tilePrefab,tilePos,Quaternion.identity,this.transform);
                newTile.transform.localScale = Vector3.one * tileScale;
            }
        }
    }
}
