using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class piece : MonoBehaviour
{
    public PieceType pieceType;
    private  Collider2D PieceCollider;
    public float KingMoveRange;
    public Collider2D ThisTile;
    public LayerMask TileLayerMask;

    [Header("ÊÂ¼þ¹ã²¥")]
    public PieceEventSO OnPieceUI;
    private void Awake()
    {
      PieceCollider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        ThisTile = Physics2D.OverlapBox(transform.position, transform.localScale, 0,TileLayerMask);
    }
    private void Update()
    {

    }
    private void OnMouseDown()
    {
        OnPieceUI.RaiseEvent(this);
        GameManager.Instance.OffShowMoveTile(); ;
        UiManager.Instance.OffCreateUI();
    }
    public void Move()
    {
        switch (pieceType)
        {
            case PieceType.king:
                kingShowMoveTile();
                break;
            case PieceType.Cavalry:
                kingShowMoveTile();
                break;
            case PieceType.Archer:
                kingShowMoveTile();
                break;
            case PieceType.Chariot:
                kingShowMoveTile();
                break;
            case PieceType.Bird:
                kingShowMoveTile();
                break;
            case PieceType.Infantry:
                kingShowMoveTile();
                break;
            case PieceType.knight:
                kingShowMoveTile();
                break;
            case PieceType.Architect:
                kingShowMoveTile();
                break;
        }
    }
    
    private void kingShowMoveTile()
    {
       
        ThisTile.GetComponent<Tile>().CreateDirection(KingMoveRange);
       
    }
   
}
