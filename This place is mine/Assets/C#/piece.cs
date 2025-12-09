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
    private void Awake()
    {
        KingMoveRange = KingMoveRange * GameManager.Instance.TileSize;
       PieceCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {

    }
    private void OnMouseDown()
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
        for (int i = 0; i < GameManager.Instance.tiles.Count; i++)
        {
            //float DistX =  Mathf.Abs(transform.position.x - GameManager.Instance.tiles[i].transform.position.x);
            //float DistY = Mathf.Abs(transform.position.y - GameManager.Instance.tiles[i].transform.position.y);
            float Dist = Vector2.Distance(transform.position, GameManager.Instance.tiles[i].transform.position);
            if (Dist <= KingMoveRange )
                GameManager.Instance.tiles[i].CanMoveTile();
        }
    }
 
}
