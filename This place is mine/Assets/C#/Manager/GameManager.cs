using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("声明")]
    public float TileSize;
    [Header("方块数组")]
    public List<Tile> tiles;
    [Header("游戏参数")]
    public int BuleGameHeart;
    public int RedGameHeart;
    public float Round;
    public Team TeamRound;
    public PlayerData BulePlayerData;
    public PlayerData RedPlayerData;
    private PlayerData NowPlayerData;
    private PlayerData EnemyPlayerData;
    [Header("部署棋子")]
    public GameObject piece;
    public float CavalryQuantity;
    public float ArcherQuantity;
    public float ChariotQuantity;
    public float BirdQuantity;
    public float InfantryQuantity;
    public float KnightQuantity;
    public float ArchitectQuantity;
    #region
    private void Start()
    {
        NowPlayerData = BulePlayerData;
        EnemyPlayerData = RedPlayerData;
    }
    public void SetPiece(int arg0)
    {
        piece.GetComponent<piece>().pieceType = (PieceType)arg0;
        piece.GetComponent<piece>().team = TeamRound;
        if(piece.GetComponent<piece>().team==Team.red)
            piece.GetComponent<SpriteRenderer>().flipX = true;
        GameObject Piece;
       
        switch (piece.GetComponent<piece>().pieceType)
        {
            case PieceType.King:
            Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                break;
            case PieceType.Cavalry:
                if (CavalryQuantity>0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    CavalryQuantity -= 1;
                }
                break;
            case PieceType.Archer:
                if (ArcherQuantity > 0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    ArcherQuantity -= 1;
                }
                break;
            case PieceType.Chariot:
                if (ChariotQuantity > 0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    ChariotQuantity -= 1;
                }
                break;
            case PieceType.Bird:
                if (BirdQuantity > 0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    BirdQuantity -= 1;
                }
                break;
            case PieceType.Infantry:
                if (InfantryQuantity > 0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    InfantryQuantity -= 1;
                }
                break;
            case PieceType.Knight:
                if (KnightQuantity > 0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    KnightQuantity -= 1;
                }
                break;
            case PieceType.Architect:
                if (ArchitectQuantity > 0)
                {
                    Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
                    Piece.name = $"{piece.GetComponent<piece>().pieceType}";
                    ArchitectQuantity -= 1;
                }
                break;
        }   
    }
    public void OffShowMoveTile()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
          tiles[i].CanMove = false;
          tiles[i].CanMoveTile();
        }
    }
    public void OffShowAttTile()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].CanAtt = false;
            tiles[i].CanMove = false;
            tiles[i].CanMoveTile();
        }
    }
    //回合函数
    public void RoundStart()
    {
        StartCoroutine(RoundOver());
        ExchangePlayerData();
    }
    IEnumerator RoundOver()
    {
        if (TeamRound == Team.bule)
        {
            TeamRound = Team.red;
            DataInput(Team.bule);
        }
        else if (TeamRound == Team.red)
        {
            TeamRound = Team.bule;
            DataInput(Team.red);
            Round += 1;
        }
        yield return new WaitForSeconds(1f);
        if (TeamRound == Team.bule)
        {
            DataOutput(Team.bule);
        }
        else if (TeamRound == Team.red)
        {
            DataOutput(Team.red);
        }
    }
    public void ExchangePlayerData()
    {
        var a = EnemyPlayerData;
        EnemyPlayerData = NowPlayerData;
        NowPlayerData = a;
    }
    public void AddPieceQuantity(Collider2D piece)
    {
        switch (piece.GetComponent<piece>().pieceType)
        {
            case PieceType.Cavalry:
                EnemyPlayerData.CavalryQuantity += 1;
                break;
            case PieceType.Archer:
                EnemyPlayerData.ArcherQuantity += 1;
                break;
            case PieceType.Chariot:
                EnemyPlayerData.ChariotQuantity += 1;
                break;
            case PieceType.Bird:
                EnemyPlayerData.BirdQuantity += 1;
                break;
            case PieceType.Infantry:
                EnemyPlayerData.InfantryQuantity += 1;
                break;
            case PieceType.Knight:
                EnemyPlayerData.KnightQuantity += 1;
                break;
            case PieceType.Architect:
                EnemyPlayerData.ArchitectQuantity += 1;
                break;
        }
    }
    //数据获取
    public void DataOutput(Team a)
    {
        if (a == Team.bule)
        {
            CavalryQuantity = BulePlayerData.CavalryQuantity;
            ArcherQuantity = BulePlayerData.ArcherQuantity;
            ChariotQuantity = BulePlayerData.ChariotQuantity;
            BirdQuantity = BulePlayerData.BirdQuantity;
            InfantryQuantity = BulePlayerData.InfantryQuantity;
            KnightQuantity = BulePlayerData.KnightQuantity;
            ArchitectQuantity = BulePlayerData.ArchitectQuantity;
        }
        else if (a == Team.red)
        {
            CavalryQuantity = RedPlayerData.CavalryQuantity;
            ArcherQuantity = RedPlayerData.ArcherQuantity;
            ChariotQuantity = RedPlayerData.ChariotQuantity;
            BirdQuantity = RedPlayerData.BirdQuantity;
            InfantryQuantity = RedPlayerData.InfantryQuantity;
            KnightQuantity = RedPlayerData.KnightQuantity;
            ArchitectQuantity = RedPlayerData.ArchitectQuantity;
        }
     
    }
    //存储数据
    public void DataInput(Team a)
    {
        if (a == Team.bule)
        {
            BulePlayerData.CavalryQuantity = CavalryQuantity;
            BulePlayerData.ArcherQuantity = ArcherQuantity;
            BulePlayerData.ChariotQuantity = ChariotQuantity;
            BulePlayerData.BirdQuantity = BirdQuantity;
            BulePlayerData.InfantryQuantity = InfantryQuantity;
            BulePlayerData.KnightQuantity = KnightQuantity;
            BulePlayerData.ArchitectQuantity = ArchitectQuantity;
        }
        else if (a == Team.red) 
        {
            RedPlayerData.CavalryQuantity = CavalryQuantity;
            RedPlayerData.ArcherQuantity = ArcherQuantity;
            RedPlayerData.ChariotQuantity = ChariotQuantity;
            RedPlayerData.BirdQuantity = BirdQuantity;
            RedPlayerData.InfantryQuantity = InfantryQuantity;
            RedPlayerData.KnightQuantity = KnightQuantity;
            RedPlayerData.ArchitectQuantity = ArchitectQuantity;
        }
     
    }
    #endregion
}
