using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSelection : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color baseColor;

    public void ClickedOnButton(int index)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if (index == i)
            {
                texts[index].color = selectedColor;
                continue;
            }

            texts[i].color = baseColor;
        }
    }
}
