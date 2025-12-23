using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HexGrid2D : MonoBehaviour
{
    public Tile tile;
    public Transform cameraPos;
    private float size;
    [Header("地图参数")]
    public int width;
    public int height;
    public Sprite[] TileArt; 
    private void Awake()
    {
        size = GameManager.Instance.TileSize;
        CreateGrid();
        SetMap();
    }
    //地图初始化
    public void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var TileMap = Instantiate(tile, GetPosition(x,y), Quaternion.identity);
                TileMap.name = $"Tile{x}{y}";
                GameManager.Instance.tiles.Add(TileMap);
            }
        }
       cameraPos.position=new Vector3( ((width-0.5f)* size * Mathf.Sqrt(3) / 2 )/ 2+transform.position.x, size * 0.75f * height /2 + transform.position.y, -10f);

    }
    public Vector3 GetPosition(int x,int y)
    {
        return
            new Vector3(x, 0, 0) * size* Mathf.Sqrt(3)/2 + new Vector3(0, y, 0) * size * 0.75f + ((y % 2) == 1 ? new Vector3(1, 0, 0) * size * Mathf.Sqrt(3) / 2 * 0.5f : Vector3.zero)+ this.transform.position; 
    }
    //地图设置
    public void SetTileType(int x,int y,Sprite a,TileType b,Team c)
    {
        GameObject ThisTile = GameObject.Find($"Tile{x}{y}");
        ThisTile.GetComponent<SpriteRenderer>().sprite = a;
        ThisTile.GetComponent<Tile>().ThisTileType = b;
        ThisTile.GetComponent<Tile>().ThisTeam = c;
    }
    public void SetMap()
    {
        SetHomeTile();
        SetForestTile();
        SetVillageTile();
        SetRiverTile();
        SetTowerTile();
        SetKing();
    }
    public void SetKing()
    {
        GameManager.Instance.piece.GetComponent<piece>().pieceType = PieceType.King;
        GameManager.Instance.piece.GetComponent<piece>().team = Team.bule;
        GameManager.Instance.piece.GetComponent<SpriteRenderer>().flipX = false;
        GameObject Piece = Instantiate(GameManager.Instance.piece, GameObject.Find("Tile00").transform.position, Quaternion.identity);
        GameManager.Instance.piece.GetComponent<piece>().team = Team.red;
        GameManager.Instance.piece.GetComponent<SpriteRenderer>().flipX = true;
        Piece = Instantiate(GameManager.Instance.piece, GameObject.Find("Tile85").transform.position, Quaternion.identity);
        Piece.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.PieceSprite[0];
        Piece.name = $"King";
        GameObject.Find("Tile00").GetComponent<Tile>().TeamTile.GetComponent<SpriteRenderer>().sprite = UiManager.Instance.BuleTile;
    }
    public void SetHomeTile()
    {
        SetTileType(0, 0, TileArt[0],TileType.Home,Team.bule);
        SetTileType(0, 1, TileArt[0], TileType.Home, Team.bule);
        SetTileType(1, 0, TileArt[0], TileType.Home, Team.bule);
        SetTileType(width-1, height-1, TileArt[0], TileType.Home,Team.red);
        SetTileType(width-2, height-1, TileArt[0], TileType.Home, Team.red);
        SetTileType(width-1, height-2, TileArt[0], TileType.Home, Team.red);
     
    }
    public void SetForestTile()
    {

    }
    public void SetVillageTile()
    {

    }
    public void SetRiverTile()
    {

    }
    public void SetTowerTile()
    {

    }
}
