using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color baseColor, offsetColor;
    public SpriteRenderer colorTile;
    public GameObject heightLight;

    public void Init(bool isOffset)
    {
       colorTile.color = isOffset ? offsetColor : baseColor; 
    }
    public void OnMouseEnter()
    {
        heightLight.SetActive(true);
    }
    public void OnMouseExit()
    {
        heightLight.SetActive(false);
    }
}
