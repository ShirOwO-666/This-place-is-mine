using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PieceUI : MonoBehaviour
{
    public Button Att;
    public Button Move;
    public Button Cancel;
    public piece piece;
    [Header("ÊÂ¼þ¼àÌý")]
    public PieceEventSO OnPieceUIEvent;
    private void Awake()
    {
        Att.onClick.AddListener(SetAtt);
        Move.onClick.AddListener(SetMove);
        Cancel.onClick.AddListener(SetCancel);
    }
    private void OnEnable()
    {
        OnPieceUIEvent.OnEvent += GetPiece;
    }
    private void OnDisable()
    {
        OnPieceUIEvent.OnEvent -= GetPiece;
    }
    public void GetPiece(piece a)
    {
        piece = a;
    }
    public void SetAtt()
    {
        piece.Att();
    }
    public void SetMove()
    {
        piece.Move();
    }
    public void SetCancel()
    {
       GameManager.Instance.OffShowMoveTile();
    }
}
