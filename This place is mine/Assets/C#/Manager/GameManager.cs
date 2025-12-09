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
    [Header("部署UI")]
    public GameObject CreateUI;
    public GameObject CreatePieceUI;
    public GameObject CreatePiece;
    public GameObject Help;
    public GameObject Cancel;

    [Header("部署UI参数")]
    public float HelpMaxAngle;//旋转最大值
    public float CancelMaxAngle;
    public float speed;//旋转速度
    public float radius;//半径
    private float HelpAngle = 0;
    private float CancelAngle = 0;
    private Vector3 Center=Vector3.zero;//旋转中心

    [Header("事件监听")]
    public TileEventSO CreateUIEvent;
    public VoidEventSO OnCreatePieceUIEvent;
    private void OnEnable()
    {
        CreateUIEvent.OnEvent += OnCreateUI;
        OnCreatePieceUIEvent.OnEvent += OnCreatePieceUI;
    }
    private void OnDisable()
    {
        CreateUIEvent.OnEvent -= OnCreateUI;
        OnCreatePieceUIEvent.OnEvent -= OnCreatePieceUI;
    }
    private void Start()
    {
        Center=CreateUI.transform.position-new Vector3(0,radius,0);
        HelpMaxAngle = (HelpMaxAngle * Mathf.PI) / 180;
        CancelMaxAngle = (CancelMaxAngle * Mathf.PI) / 180;
        
    }
    private void Update()
    {     
            SetCreateUI();
    }
    //事件监听
    #region
    public void OnCreatePieceUI()
    {
        CreatePieceUI.SetActive(true);
    }
    public void OnDestroyCreatePieceUI()
    {
        CreatePieceUI.SetActive(false);
    }
    public void OnCreateUI(Tile tile)
    {
        HelpAngle = Mathf.PI/2;
        CancelAngle = Mathf.PI/2;

        CreatePiece.SetActive(true);
        Help.SetActive(true);
        Cancel.SetActive(true);
        Center = tile.transform.position;
        CreateUI.transform.localPosition = Center+new Vector3(0,radius,0);
    }
    public void OffCreateUI()
    {
        CreatePiece.SetActive(false);
        Help.SetActive(false);
        Cancel.SetActive(false);
    }

    public void SetCreateUI()
    {
     float x1= Help.transform.position.x;
     float y1= Help.transform.position.y;
     float x2= Cancel.transform.position.x;
     float y2= Cancel.transform.position.y;
 
     HelpAngle -= Time.deltaTime* speed;
     CancelAngle -= Time.deltaTime * speed;
       if (HelpAngle >=HelpMaxAngle)
	   {
            x1 = Center.x + radius * Mathf.Cos(HelpAngle);
            y1 = Center.y + radius * Mathf.Sin(HelpAngle);
       }
        if (CancelAngle >=CancelMaxAngle)
        {
            x2 = Center.x + radius * Mathf.Cos(CancelAngle);
            y2 = Center.y + radius * Mathf.Sin(CancelAngle);
        }
        Help.transform.position = new Vector3(x1, y1);
        Cancel.transform.position = new Vector3(x2, y2);
    }
    public void SetPiece(int arg0)
    {
            piece.GetComponent<piece>().pieceType = (PieceType)arg0;
            var Piece = Instantiate(piece, Center, Quaternion.identity);
            Piece.name = $"{arg0}";
    }
    #endregion
    //创建棋子
    public void SetPieceType()
    {

    }
}
