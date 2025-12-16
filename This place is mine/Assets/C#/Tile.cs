using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;

public class Tile : MonoBehaviour
{
    [Header("声明")]
    public piece ThisPiece;
    public SpriteRenderer colorTile;
    public Collider2D TileCollider;
    public Collider2D AttPiece;
    private bool isCreatePieceUI=false;
    public bool CanMove;
    public bool CanAtt;
    public bool isPiece=false;
    public LayerMask TileLayerMask;
    public LayerMask PieceLayerMask;
    [Header("Tile参数")]
    public GameObject heightLight;
    public Color CanMoveColor;
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
    private void OnMouseDown()
    {
        Move();
        Att();
    }
    private void Update()
    {
        SetPiece();
        isPiece = Physics2D.OverlapBox(transform.position, transform.localScale, 0, PieceLayerMask);
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
    public void Move()
    {
        if (CanMove)
        {
            ThisPiece.tile = this;
            GameManager.Instance.OffShowMoveTile();
            UiManager.Instance.OffPieceUI();
        }
    }
    public void Att()
    {
        if (CanAtt)
        {
            ThisPiece.tile = this;
            Destroy(AttPiece.gameObject);
            GameManager.Instance.AddPieceQuantity(AttPiece);
            GameManager.Instance.OffShowMoveTile();
            UiManager.Instance.OffPieceUI();
        }
    }
    public void isCanMove()
    {
        if(isPiece)
        {
            CanMove = false;
        }
        else
        {
            CanMove = true;
        }
    }
    public void isCanAtt()
    {
        AttPiece = Physics2D.OverlapBox(transform.position, transform.localScale, 0, PieceLayerMask);
        if (isPiece&&ThisPiece.team!=AttPiece.GetComponent<piece>().team)
        {
            CanAtt = true;
            AttPiece.GetComponent<piece>().CanAtt = true;
        }
        else
        {
            CanAtt = false;
        }
    }
    public void CanMoveTile()
    {
        if (CanMove)
            colorTile.color = CanMoveColor;
        else
            colorTile.color = Color.white;
    }
    public void CanAttTile()
    {    
            colorTile.color = CanMoveColor;
    }
    public void CreateMoveDirection(float Range,piece piece,bool restrict)
    {
        Collider2D[] s = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) ), 30, TileLayerMask);
        Collider2D[] q = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) ), -30, TileLayerMask);
        Collider2D[] r = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) ), 90, TileLayerMask);
        Collider2D[] s0 = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, (Range-1) * GameManager.Instance.TileSize * Mathf.Sqrt(3)), 30, TileLayerMask);
        Collider2D[] q0 = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, (Range-1) * GameManager.Instance.TileSize * Mathf.Sqrt(3)), -30, TileLayerMask);
        Collider2D[] r0 = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, (Range-1) * GameManager.Instance.TileSize * Mathf.Sqrt(3)), 90, TileLayerMask);
        if (restrict)
        {
            s = s.Except(s0).ToArray();
            q = q.Except(q0).ToArray();
            r = r.Except(r0).ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                s[i].GetComponent<Tile>().ThisPiece = piece;
                s[i].GetComponent<Tile>().isCanMove();
                s[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < q.Length; i++)
            {
                q[i].GetComponent<Tile>().ThisPiece = piece;
                q[i].GetComponent<Tile>().isCanMove();
                q[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < r.Length; i++)
            {
                r[i].GetComponent<Tile>().ThisPiece = piece;
                r[i].GetComponent<Tile>().isCanMove();
                r[i].GetComponent<Tile>().CanMoveTile();
            }
            CanMove = true;
            CanMoveTile();
        }
        else
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i].GetComponent<Tile>().ThisPiece = piece;
                s[i].GetComponent<Tile>().isCanMove();
                s[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < q.Length; i++)
            {
                q[i].GetComponent<Tile>().ThisPiece = piece;
                q[i].GetComponent<Tile>().isCanMove();
                q[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < r.Length; i++)
            {
                r[i].GetComponent<Tile>().ThisPiece = piece;
                r[i].GetComponent<Tile>().isCanMove();
                r[i].GetComponent<Tile>().CanMoveTile();
            }
        }
    }
    public void CreateAttDirection(float Range, piece piece, bool restrict)
    {  
            for (int i = 0; i < GameManager.Instance.tiles.Count; i++)
            {
                GameManager.Instance.tiles[i].GetComponent<Tile>().ThisPiece = piece;
                GameManager.Instance.tiles[i].GetComponent<Tile>().AttDirection(Range,restrict);
            }
     }
    public void AttDirection(float Range,bool restrict)
    {
        if (restrict)
        {
            var a = Vector2.Distance(transform.position, ThisPiece.transform.position);
            if (a <= Range * GameManager.Instance.TileSize&&a> (Range-1) * GameManager.Instance.TileSize)
            {
                isCanAtt();
                CanAttTile();
            }
        }
        else
        {
            var a = Vector2.Distance(transform.position, ThisPiece.transform.position);
            if (a <= Range * GameManager.Instance.TileSize)
            {
                isCanAtt();
                CanAttTile();
            }
        }
     
    }
}
