using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Vector2 mousePos;
    private bool isCreateUI=false;
    [Header("创建UI")]
    public GameObject CreateUI;
    public GameObject CreatePiece;
    public GameObject Help;
    public GameObject Cancel;

    [Header("UI参数")]
    public float HelpMaxAngle;//旋转最大值
    public float CancelMaxAngle;
    public float speed;//旋转速度
    public float radius;//半径
    private float HelpAngle = 0;
    private float CancelAngle = 0;
    private Vector3 Center=Vector3.zero;//旋转中心

    [Header("事件监听")]
    public TileEventSO CreateUIEvent;
    private void OnEnable()
    {
        CreateUIEvent.OnEvent += OnCreateUI;
    }
    private void OnDisable()
    {
        CreateUIEvent.OnEvent -= OnCreateUI;
    }
    private void Start()
    {
        Center=CreateUI.transform.position-new Vector3(0,radius,0);
        HelpMaxAngle = (HelpMaxAngle * Mathf.PI) / 180;
        CancelMaxAngle = (CancelMaxAngle * Mathf.PI) / 180;
    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isCreateUI)
        {
            SetCreateUI();
        }
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
        isCreateUI = true;
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
}
