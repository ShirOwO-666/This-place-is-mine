using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color baseColor, offsetColor;
    public SpriteRenderer colorTile;
    public GameObject heightLight;
    private void Update()
    {
        
    }
    public void OnMouseEnter()
    {
        heightLight.SetActive(true);
    }
    public void OnMouseExit()
    {
        heightLight.SetActive(false);
    }
    public void Init(bool isOffset)
    {
        colorTile.color = isOffset ? offsetColor : baseColor;
    }
    private void OnMouseDown()
    {
        SetPiece();
    }
    public void SetPiece()
    {
        if (Input.GetMouseButtonDown(1))
        { 
      
        }
    }
}
