using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("声明")]
    public SpriteRenderer colorTile;
    public Collider2D TileCollider;
    public GameObject heightLight;
    [Header("Tile参数")]
    public Color baseColor, offsetColor;
    [Header("事件广播")]
    public TileEventSO CreateUIEvent;
    private void Awake()
    {
        TileCollider = GetComponent<Collider2D>();
    }
    public void OnMouseEnter()
    {
        heightLight.SetActive(true);
    }
    public void OnMouseExit()
    {
        heightLight.SetActive(false);
    }
    private void Update()
    {
        SetPiece();
    }
    public void Init(bool isOffset)
    {
        colorTile.color = isOffset ? offsetColor : baseColor;
    }
    public void SetPiece()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (TileCollider.OverlapPoint(mousePos))
        {
            if (Input.GetMouseButtonDown(1))
            {
                CreateUIEvent.RaiseEvent(this);
            }
        }
    }
}
