using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    [Header("棋子阵容UI")]
    public Sprite BuleTile;
    public Sprite RedTile;
    public Sprite GreenTile;
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

    [Header("部署棋子UI")]
    public bool isCheakPiece=false;//棋子被选中
    public GameObject CavalryPieceUI;
    public GameObject ArcherPieceUI;
    public GameObject ChariotPieceUI;
    public GameObject BirdPieceUI;
    public GameObject InfantryPieceUI;
    public GameObject KnightPieceUI;
    public GameObject ArchitectPieceUI;
    public TextMeshProUGUI CavalryPieceCounterUI;
    public TextMeshProUGUI ArcherPieceCounterUI;
    public TextMeshProUGUI ChariotPieceCounterUI;
    public TextMeshProUGUI BirdPieceCounterUI;
    public TextMeshProUGUI InfantryPieceCounterUI;
    public TextMeshProUGUI KnightPieceCounterUI;
    public TextMeshProUGUI ArchitectPieceCounterUI;
    [Header("事件广播")]
    public VoidEventSO ResetPieceTeamTileEvent;

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
        CanCreatePiece();
        isActionPoint();
        SetCounterUI();
    }
    public void isActionPoint()
    {
        if (GameManager.Instance.ActionPoint)
        {
            CreatePiece.GetComponent<Button>().interactable = true;
            Move.GetComponent<Button>().interactable = true;
        }
        else
        {
            CreatePiece.GetComponent<Button>().interactable=false;
            Move.GetComponent<Button>().interactable = false;
        }
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
        OffCreateUI();
        UIAngle1 = Mathf.PI / 2;
        UIAngle2 = Mathf.PI / 2;

        if(tile.ThisTileType==TileType.Home&& GameManager.Instance.TeamRound== tile.ThisTeam)
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
        isCheakPiece = true;
    }
    public void OffPieceUI()
    {
        Att.SetActive(false);
        Move.SetActive(false);
        CancelPieceUI.SetActive(false);
        isCheakPiece = false;
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
    
    public void CanCreatePiece()
    {
        if (GameManager.Instance.CavalryQuantity <= 0)
        {
            CavalryPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            CavalryPieceUI.GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.ArcherQuantity <= 0)
        {
            ArcherPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            ArcherPieceUI.GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.ChariotQuantity <= 0)
        {
            ChariotPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            ChariotPieceUI.GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.BirdQuantity <= 0)
        {
            BirdPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            BirdPieceUI.GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.InfantryQuantity <= 0)
        {
           InfantryPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            InfantryPieceUI.GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.KnightQuantity <= 0)
        {
            KnightPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
           KnightPieceUI.GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.ArchitectQuantity <= 0)
        {
            ArchitectPieceUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            ArchitectPieceUI.GetComponent<Button>().interactable = true;
        }
    }
    public void ResetPieceTeam()
    {
        ResetPieceTeamTileEvent.RaiseEvent();
    }
    public void SetCounterUI()
    {
        CavalryPieceCounterUI.text =""+ GameManager.Instance.CavalryQuantity;
        ArcherPieceCounterUI.text = "" + GameManager.Instance.ArcherQuantity;
        ChariotPieceCounterUI.text = "" + GameManager.Instance.ChariotQuantity;
        BirdPieceCounterUI.text = "" + GameManager.Instance.BirdQuantity;
        InfantryPieceCounterUI.text = "" + GameManager.Instance.InfantryQuantity;
        KnightPieceCounterUI.text = "" + GameManager.Instance.KnightQuantity;
        ArchitectPieceCounterUI.text = "" + GameManager.Instance.ArchitectQuantity;
    }
}
