using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoSingleton<UiManager>
{
    [Header("部署UI")]
    public GameObject CreateUI;
    public GameObject PieceCanvas;
    public GameObject CreatePiece;
    public GameObject Help;
    public GameObject Cancel;

    [Header("交互UI")]
    public GameObject PieceUI;
    public GameObject Att;
    public GameObject Move;
    public GameObject CancelPieceUI;

    [Header("部署UI参数")]
    public float HelpMaxAngle;//旋转最大值
    public float CancelMaxAngle;
    public float speed;//旋转速度
    public float radius;//半径
    private float UIAngle1 = 0;
    private float UIAngle2 = 0;
    public Vector3 Center = Vector3.zero;//旋转中心

    [Header("事件监听")]
    public TileEventSO CreateUIEvent;
    public VoidEventSO OnCreatePieceUIEvent;
    public PieceEventSO OnPieceUIEvent;


    private void OnEnable()
    {
        CreateUIEvent.OnEvent += OnCreateUI;
        OnCreatePieceUIEvent.OnEvent += OnCreatePieceUI;
        OnPieceUIEvent.OnEvent += OnPieceUI;
    }
    private void OnDisable()
    {
        CreateUIEvent.OnEvent -= OnCreateUI;
        OnCreatePieceUIEvent.OnEvent -= OnCreatePieceUI;
        OnPieceUIEvent.OnEvent -= OnPieceUI;
    }
    private void Start()
    {
        Center = CreateUI.transform.position - new Vector3(0, radius, 0);
        HelpMaxAngle = (HelpMaxAngle * Mathf.PI) / 180;
        CancelMaxAngle = (CancelMaxAngle * Mathf.PI) / 180;

    }
    private void Update()
    {
        SetCreateUI();
        SetPieceUI();
    }
    public void OnCreatePieceUI()
    {
        PieceCanvas.SetActive(true);
    }
    public void OnDestroyCreatePieceUI()
    {
        PieceCanvas.SetActive(false);
    }
    public void OnCreateUI(Tile tile)
    {
        UIAngle1 = Mathf.PI / 2;
        UIAngle2 = Mathf.PI / 2;

        CreatePiece.SetActive(true);
        Help.SetActive(true);
        Cancel.SetActive(true);
        Center = tile.transform.position;
        CreateUI.transform.localPosition = Center + new Vector3(0, radius, 0);
    }
    public void OffCreateUI()
    {
        CreatePiece.SetActive(false);
        Help.SetActive(false);
        Cancel.SetActive(false);
    }

    public void SetCreateUI()
    {
        float x1 = Help.transform.position.x;
        float y1 = Help.transform.position.y;
        float x2 = Cancel.transform.position.x;
        float y2 = Cancel.transform.position.y;

        UIAngle1 -= Time.deltaTime * speed;
        UIAngle2 -= Time.deltaTime * speed;
        if (UIAngle1 >= HelpMaxAngle)
        {
            x1 = Center.x + radius * Mathf.Cos(UIAngle1);
            y1 = Center.y + radius * Mathf.Sin(UIAngle1);
        }
        if (UIAngle2 >= CancelMaxAngle)
        {
            x2 = Center.x + radius * Mathf.Cos(UIAngle2);
            y2 = Center.y + radius * Mathf.Sin(UIAngle2);
        }
        Help.transform.position = new Vector3(x1, y1);
        Cancel.transform.position = new Vector3(x2, y2);
    }


    public void OnPieceUI(piece piece)
    {
        UIAngle1 = Mathf.PI / 2;
        UIAngle2 = Mathf.PI / 2;

        Att.SetActive(true);
        Move.SetActive(true);
        CancelPieceUI.SetActive(true);
        Center = piece.transform.position;
        PieceUI.transform.localPosition = Center + new Vector3(0, radius, 0);
    }
    public void OffPieceUI()
    {
        Att.SetActive(false);
        Move.SetActive(false);
        CancelPieceUI.SetActive(false);
    }

    public void SetPieceUI()
    {
        float x1 = Move.transform.position.x;
        float y1 = Move.transform.position.y;
        float x2 = CancelPieceUI.transform.position.x;
        float y2 = CancelPieceUI.transform.position.y;

        UIAngle1 -= Time.deltaTime * speed;
        UIAngle2 -= Time.deltaTime * speed;
        if (UIAngle1 >= HelpMaxAngle)
        {
            x1 = Center.x + radius * Mathf.Cos(UIAngle1);
            y1 = Center.y + radius * Mathf.Sin(UIAngle1);
        }
        if (UIAngle2 >= CancelMaxAngle)
        {
            x2 = Center.x + radius * Mathf.Cos(UIAngle2);
            y2 = Center.y + radius * Mathf.Sin(UIAngle2);
        }
        Move.transform.position = new Vector3(x1, y1);
        CancelPieceUI.transform.position = new Vector3(x2, y2);
    }
}
