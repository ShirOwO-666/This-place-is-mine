using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HexGrid2D : MonoBehaviour
{
    public Tile tile;
    public Transform cameraPos;
    public float size;
    [Header("µØÍ¼²ÎÊý")]
    public int width;
    public int height;
    private void Awake()
    {
        CreateGrid();
    }
    public void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var TileMap = Instantiate(tile, GetPosition(x,y), Quaternion.identity);
                TileMap.name = $"Tile{x}{y}";
                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(isOffset);
            }
        }
       cameraPos.position=new Vector3((width*Mathf.Sqrt(3) / 2 )/ 2+transform.position.x, height/2 + transform.position.y, -10f);

    }
    public Vector3 GetPosition(int x,int y)
    {
        return
            new Vector3(x, 0, 0) * size* Mathf.Sqrt(3)/2 + new Vector3(0, y, 0) * size * 0.75f + ((y % 2) == 1 ? new Vector3(1, 0, 0) * size * Mathf.Sqrt(3) / 2 * 0.5f : Vector3.zero)+ this.transform.position; 
    }
}
