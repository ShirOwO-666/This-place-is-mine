using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("声明")]
    public SpriteRenderer colorTile;
    public Collider2D TileCollider;
    public GameObject heightLight;
    private bool isCreatePieceUI=false;
    [Header("Tile参数")]
    public Color baseColor, offsetColor;
    [Header("事件广播")]
    public TileEventSO CreateUIEvent;
    [Header("事件监听")]
    public VoidEventSO isCreatePieceUIEvent;
    public VoidEventSO OffCreatePieceUIEvent;
    private void Awake()
    {
        TileCollider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        isCreatePieceUIEvent.OnEvent += CreatePieceUI;
        OffCreatePieceUIEvent.OnEvent += OffCreatePieceUI;
    }


    private void OnDisable()
    {
        isCreatePieceUIEvent.OnEvent -= CreatePieceUI;
        OffCreatePieceUIEvent.OnEvent -= OffCreatePieceUI;
    }
    public void OnMouseEnter()
    {
        if (!isCreatePieceUI)
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
    private void CreatePieceUI()
    {
       isCreatePieceUI = true;
    }
    private void OffCreatePieceUI()
    {
        isCreatePieceUI = false;
    }

}
