using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("声明")]
    public float TileSize;
    [Header("方块数组")]
    public List<Tile> tiles;
    [Header("部署棋子")]
    public GameObject piece;
   

   
    #region
   
    public void SetPiece(int arg0)
    {
            piece.GetComponent<piece>().pieceType = (PieceType)arg0;
            var Piece = Instantiate(piece, UiManager.Instance.Center, Quaternion.identity);
            Piece.name = $"{arg0}";
    }
    public void OffShowMoveTile()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
          tiles[i].CanMove = false;
          tiles[i].CanMoveTile();
        }
    }
    #endregion
}
