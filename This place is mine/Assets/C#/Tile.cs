using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEngine.Rendering;
using System.Security.Cryptography;
using Unity.VisualScripting;

public class Tile : MonoBehaviour
{
    [Header("声明")]
    public piece ThisPiece;
    public Team ThisTeam;
    public SpriteRenderer colorTile;
    public Collider2D TileCollider;
    public Collider2D AttPiece;
    private bool isCreatePieceUI=false;
    public bool CanMove;
    public bool CanAtt;
    public Collider2D isPiece;
    public LayerMask TileLayerMask;
    public LayerMask PieceLayerMask;
    [Header("Tile参数")]
    public TileType ThisTileType;
    public GameObject heightLight;
    public Color CanMoveColor;
    public GameObject TeamTile;
    [Header("事件广播")]
    public TileEventSO CreateUIEvent;
    [Header("事件监听")]
    public VoidEventSO ResetPieceTeamTileEvent;
    public VoidEventSO OnCreatePieceUIEvent;
    public VoidEventSO OffCreatePieceUIEvent;
    private void Awake()
    {
        TileCollider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        ResetPieceTeamTileEvent.OnEvent += isTeamTile;
        OnCreatePieceUIEvent.OnEvent += CreatePieceUI;
        OffCreatePieceUIEvent.OnEvent += OffCreatePieceUI;
    }


    private void OnDisable()
    {
        ResetPieceTeamTileEvent.OnEvent -= isTeamTile;
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
        ShowTeamTile();
    }

    //基本
    #region
    public void SetPiece()
    {
            if (!isPiece)
            {
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
        if (GameManager.Instance.ActionPoint)
        {
            if (CanMove)
            {
                ThisPiece.tile = this;
                GameManager.Instance.ActionPoint = false;
                GameManager.Instance.OffShowMoveTile();
                UiManager.Instance.OffPieceUI();
            }
        }     
    }
    public void Att()
    {
        if (CanAtt)
        {
            ThisPiece.tile = this;
            if (AttPiece != null)
            {
                Destroy(AttPiece.gameObject);
            }
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
        if (ThisPiece.pieceType!=PieceType.Bird)
        {
            if (isPiece && ThisPiece.team != AttPiece.GetComponent<piece>().team && AttPiece.GetComponent<piece>().CanLook)
            {
                CanAtt = true;
                AttPiece.GetComponent<piece>().CanAtt = true;
            }
            else
            {
                CanAtt = false;
            }
            Debug.Log(0);
        }
        else if(ThisPiece.pieceType == PieceType.Bird)
        {
            if (isPiece && ThisPiece.team != AttPiece.GetComponent<piece>().team)
            {
                CanAtt = true;
                AttPiece.GetComponent<piece>().CanAtt = true;
            }
            Debug.Log(1);
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
    public void CreateMoveDirection(float Range, piece piece, bool restrict)
    {

        Collider2D[] s = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3)), 30, TileLayerMask);
        Collider2D[] q = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3)), -30, TileLayerMask);
        Collider2D[] r = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, Range * GameManager.Instance.TileSize * Mathf.Sqrt(3)), 90, TileLayerMask);
        Collider2D[] s0 = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, (Range - 1) * GameManager.Instance.TileSize * Mathf.Sqrt(3)), 30, TileLayerMask);
        Collider2D[] q0 = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, (Range - 1) * GameManager.Instance.TileSize * Mathf.Sqrt(3)), -30, TileLayerMask);
        Collider2D[] r0 = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, (Range - 1) * GameManager.Instance.TileSize * Mathf.Sqrt(3)), 90, TileLayerMask);
        if (restrict)
        {
            s = s.Except(s0).ToArray();
            q = q.Except(q0).ToArray();
            r = r.Except(r0).ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                s[i].GetComponent<Tile>().ThisPiece = piece;
                s[i].GetComponent<Tile>().isCanMove();
                s[i].GetComponent<Tile>().ifRiverTile();
                s[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < q.Length; i++)
            {
                q[i].GetComponent<Tile>().ThisPiece = piece;
                q[i].GetComponent<Tile>().isCanMove();
                q[i].GetComponent<Tile>().ifRiverTile();
                q[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < r.Length; i++)
            {
                r[i].GetComponent<Tile>().ThisPiece = piece;
                r[i].GetComponent<Tile>().isCanMove();
                r[i].GetComponent<Tile>().ifRiverTile();
                r[i].GetComponent<Tile>().CanMoveTile();
            }
     
        }
        else
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i].GetComponent<Tile>().ThisPiece = piece;
                s[i].GetComponent<Tile>().isCanMove();
                s[i].GetComponent<Tile>().ifRiverTile();
                s[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < q.Length; i++)
            {
                q[i].GetComponent<Tile>().ThisPiece = piece;
                q[i].GetComponent<Tile>().isCanMove();
                q[i].GetComponent<Tile>().ifRiverTile();
                q[i].GetComponent<Tile>().CanMoveTile();
            }
            for (int i = 0; i < r.Length; i++)
            {
                r[i].GetComponent<Tile>().ThisPiece = piece;
                r[i].GetComponent<Tile>().isCanMove();
                r[i].GetComponent<Tile>().ifRiverTile();
                r[i].GetComponent<Tile>().CanMoveTile();
            }
        }
        CanMove = true;
        CanMoveTile();
    }
    //public void ConvertCollider2DCreateMoveDirection(RaycastHit2D[] a ,piece piece)
    //{
    //    Collider2D[] colliders = new Collider2D[a.Length];
    //    for (int i = 0; i < a.Length; i++)
    //    {
    //        colliders[i] =a[i].collider;
    //    }
    //    foreach (Collider2D col in colliders)
    //    {
    //        if (col != null)
    //        {
    //            col.GetComponent<Tile>().ThisPiece = piece;
    //            col.GetComponent<Tile>().isCanMove();
    //            col.GetComponent<Tile>().CanMoveTile();
    //        }
    //    }
    //}
    //public RaycastHit2D[] ExceptRange(RaycastHit2D[] a, float Range,Vector2 vector2)
    //{
    //    RaycastHit2D[] a0 = Physics2D.RaycastAll(transform.position, vector2, (Range-1f) * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);
    //    a = a.Except(a0).ToArray();
    //    return a;
    //}
    //public void CreateMoveDirection(float Range, piece piece, bool restrict)
    //{
    //    RaycastHit2D[] s1 = Physics2D.RaycastAll(transform.position, new Vector2(-1, Mathf.Sqrt(3)), Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);
    //    RaycastHit2D[] q1 = Physics2D.RaycastAll(transform.position, new Vector2(1, Mathf.Sqrt(3)), Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);
    //    RaycastHit2D[] r1 = Physics2D.RaycastAll(transform.position, new Vector2(-1, 0), Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);
    //    RaycastHit2D[] s2 = Physics2D.RaycastAll(transform.position, new Vector2(1, -Mathf.Sqrt(3)), Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);
    //    RaycastHit2D[] q2 = Physics2D.RaycastAll(transform.position, new Vector2(-1, -Mathf.Sqrt(3)), Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);
    //    RaycastHit2D[] r2 = Physics2D.RaycastAll(transform.position, new Vector2(1, 0), Range * GameManager.Instance.TileSize * Mathf.Sqrt(3) / 2, TileLayerMask.value);

    //    if (restrict)
    //    {
    //        s1 = ExceptRange(s1, Range , new Vector2(-1, Mathf.Sqrt(3)));
    //        q1 = ExceptRange(q1, Range , new Vector2(1, Mathf.Sqrt(3)));
    //        r1 = ExceptRange(r1, Range , new Vector2(-1,0));
    //        s2 = ExceptRange(s2, Range, new Vector2(1, -Mathf.Sqrt(3)));
    //        q2 = ExceptRange(q2, Range, new Vector2(-1, -Mathf.Sqrt(3)));
    //        r2 = ExceptRange(r2, Range, new Vector2(1,0));
    //    }
  
    //    ConvertCollider2DCreateMoveDirection(s1,piece);
    //    ConvertCollider2DCreateMoveDirection(s2, piece);
    //    ConvertCollider2DCreateMoveDirection(q1, piece);
    //    ConvertCollider2DCreateMoveDirection(q2, piece);
    //    ConvertCollider2DCreateMoveDirection(r1, piece);
    //    ConvertCollider2DCreateMoveDirection(r2, piece);

    //    //s = s.Except(s0).ToArray();
    //    //q = q.Except(q0).ToArray();
    //    //r = r.Except(r0).ToArray();

    //    CanMove = true;
    //        CanMoveTile();
    //}
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
    #endregion
    //特殊Tile
    public void ifTile(piece a)
    {
        switch (ThisTileType)
        {
            case TileType.Forest:
                ForestTile(a);
                break;
            case TileType.Village:
                VillageTile(a);
                break;
            case TileType.River:
                break;
            case TileType.Tower:
                break;
            case TileType.Tile:
                Reset(a);
                break;
        }
    }
    public void ForestTile(piece a)
    { 
         a.CanLook = false;
    }
    public void VillageTile(piece a)
    {
        if (a.team==Team.bule)
        {
            ThisTeam = Team.bule;
        }
        else if (a.team == Team.red)
        {
            ThisTeam = Team.red;
        }
    }
    public void ifRiverTile()
    {
        if (ThisTileType == TileType.River)
        {
            if (ThisPiece.pieceType == PieceType.Bird || ThisPiece.pieceType == PieceType.Chariot)
            {
                CanMove = true;
            }
            else
            {
                CanMove = false;
            }
        }
      
    }
    public void Reset(piece a)
    {
        a.CanLook = true;
    }
    //判断棋子阵营
    public void isTeamTile()
    {
        if (isPiece != null)
        {
            if (isPiece.GetComponent<piece>().team == Team.bule)
            {
                TeamTile.GetComponent<SpriteRenderer>().sprite = UiManager.Instance.BuleTile;
            }else if (isPiece.GetComponent<piece>().team == Team.red)
            {
                TeamTile.GetComponent<SpriteRenderer>().sprite = UiManager.Instance.RedTile;
            }

        }
    }
    public void ShowTeamTile()
    {
        if(isPiece != null)
        {
            if (isPiece.GetComponent<piece>().team ==GameManager.Instance.TeamRound)
            {
                TeamTile.SetActive(true);
            }
            else 
            {
                TeamTile.SetActive(false);
            }
        }
        else
        {
            TeamTile.SetActive(false);
        }
    }
}
