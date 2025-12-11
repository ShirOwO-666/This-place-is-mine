using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("声明")]
    public SpriteRenderer colorTile;
    public Collider2D TileCollider;
    public GameObject heightLight;
    private bool isCreatePieceUI=false;
    public bool CanMove;
    public bool isPiece=false;
    public Color CanMoveColor;
    public LayerMask TileLayerMask;
    public LayerMask PieceLayerMask;
    public Collider2D[] s;
    public Collider2D[] q;
    public Collider2D[] r;
    [Header("Tile参数")]
    public Color baseColor, offsetColor;
    [Header("事件广播")]
    public TileEventSO CreateUIEvent;
    [Header("事件监听")]
    public VoidEventSO OnCreatePieceUIEvent;
    public VoidEventSO OffCreatePieceUIEvent;
    private void Awake()
    {
        TileCollider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        OnCreatePieceUIEvent.OnEvent += CreatePieceUI;
        OffCreatePieceUIEvent.OnEvent += OffCreatePieceUI;
    }


    private void OnDisable()
    {
        OnCreatePieceUIEvent.OnEvent -= CreatePieceUI;
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
        isPiece = Physics2D.OverlapBox(transform.position, transform.localScale, 0, PieceLayerMask);
    }
    public void Init(bool isOffset)
    {
        colorTile.color = isOffset ? offsetColor : baseColor;
    }
    public void SetPiece()
    {
        if (!isPiece) {
            if (!isCreatePieceUI)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (TileCollider.OverlapPoint(mousePos))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        CreateUIEvent.RaiseEvent(this);
                        GameManager.Instance.OffShowMoveTile(); ;
                        UiManager.Instance.OffPieceUI();

                    }
                }
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
    public void CanMoveTile()
    {
        if (CanMove)
            colorTile.color = CanMoveColor;
        else
            colorTile.color = Color.white;
    }
    public void CreateDirection(float Range)
    {
     
      s = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) ), 30, TileLayerMask);
      q = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) ), -30, TileLayerMask);
      r = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) ), 90, TileLayerMask);
        for (int i = 0; i < s.Length; i++)
        {
            s[i].GetComponent<Tile>().CanMove=true;
            s[i].GetComponent<Tile>().CanMoveTile();
        }
        for (int i = 0; i < q.Length; i++)
        {
            q[i].GetComponent<Tile>().CanMove = true;
            q[i].GetComponent<Tile>().CanMoveTile();
        }
        for (int i = 0; i < r.Length; i++)
        {
            r[i].GetComponent<Tile>().CanMove = true;
            r[i].GetComponent<Tile>().CanMoveTile();
        }
    }
}
