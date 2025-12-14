using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class piece : MonoBehaviour
{
    public PieceType pieceType;
    private  Collider2D PieceCollider;
    public Collider2D ThisTile;
    public LayerMask TileLayerMask;
    private bool isCreatePieceUI = false;
    public bool isMove=false;
    public Tile tile;
    [Header("棋子攻击参数")]
    public float KingAttRange;
    public float CavalryAttRange;
    public float ArcherAttRange;
    public float ChariotAttRange;
    public float BirdAttRange;
    public float InfantryAttRange;
    public float KnightAttRange;
    public float ArchitectAttRange;
    [Header("棋子移动参数")]
    public float Speed;
    public float KingMoveRange;
    public float CavalryMoveRange;
    public float ArcherMoverRange;
    public float ChariotMoveRange;
    public float BirdMoveRange;
    public float InfantryMoveRange;
    public float KnightMoveRange;
    public float ArchitectMoveRange;
    [Header("事件广播")]
    public PieceEventSO OnPieceUI;
    [Header("事件监听")]
    public VoidEventSO OnCreatePieceUIEvent;
    public VoidEventSO OffCreatePieceUIEvent;
    private void Awake()
    {
      PieceCollider = GetComponent<Collider2D>();
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
    private void Update()
    {
        PieceMove();
    }
    private void OnMouseDown()
    {
        if (!isCreatePieceUI)
        {
            OnPieceUI.RaiseEvent(this);
            ThisTile = Physics2D.OverlapBox(transform.position, transform.localScale, 0, TileLayerMask);
            GameManager.Instance.OffShowMoveTile(); ;
            UiManager.Instance.OffCreateUI();
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
    public void Move()
    {
        switch (pieceType)
        {
            case PieceType.King:
                ShowMoveTile(KingMoveRange,false);
                break;
            case PieceType.Cavalry:
                ShowMoveTile(CavalryMoveRange, true);
                break;
            case PieceType.Archer:
                ShowMoveTile(ArcherMoverRange, false);
                break;
            case PieceType.Chariot:
                ShowMoveTile(ChariotMoveRange, false);
                break;
            case PieceType.Bird:
                ShowMoveTile(BirdMoveRange, false);
                break;
            case PieceType.Infantry:
                ShowMoveTile(InfantryMoveRange, false);
                break;
            case PieceType.Knight:
                ShowMoveTile(KnightMoveRange, false);
                break;
            case PieceType.Architect:
                ShowMoveTile(ArchitectMoveRange, false);
                break;
        }
    }
    public void Att()
    {
        switch (pieceType)
        {
            case PieceType.King:
                ShowAttTile(KingMoveRange, false);
                break;
            case PieceType.Cavalry:
                ShowAttTile(CavalryMoveRange, true);
                break;
            case PieceType.Archer:
                ShowAttTile(ArcherMoverRange, false);
                break;
            case PieceType.Chariot:
                ShowAttTile(ChariotMoveRange, false);
                break;
            case PieceType.Bird:
                ShowAttTile(BirdMoveRange, false);
                break;
            case PieceType.Infantry:
                ShowAttTile(InfantryMoveRange, false);
                break;
            case PieceType.Knight:
                ShowAttTile(KnightMoveRange, false);
                break;
            case PieceType.Architect:
                ShowAttTile(ArchitectMoveRange, false);
                break;
        }
    }
    public void PieceMove()
    {
        if (isMove&&tile!=null)
        {
                transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, Speed * Time.deltaTime);
        }
    }
    private void ShowMoveTile(float Range,bool restrict)
    {

        ThisTile.GetComponent<Tile>().CreateMoveDirection(Range,this, restrict);
        isMove = true;

    }
    private void ShowAttTile(float Range, bool restrict)
    {

        ThisTile.GetComponent<Tile>().CreateAttDirection(Range, this, restrict);

    }
}
