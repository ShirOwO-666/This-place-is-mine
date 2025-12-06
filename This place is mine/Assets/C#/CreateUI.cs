using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUI : MonoBehaviour
{
 public enum Type
    {
    CreatePiece,Help,Cancel
    }
    public Type type;
    private Collider2D ThisCollider;
    [Header("ÊÂ¼þ¹ã²¥")]
    public VoidEventSO OffCreateUIEvent;
    public VoidEventSO OnCreatePieceUIEvent;
    private void Awake()
    {
        ThisCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ThisCollider.OverlapPoint(mousePos))
        {
            if (Input.GetMouseButtonDown(0))
                switch (type)
                {
                    case Type.CreatePiece:
                        CreatePiece();
                        break;
                    case Type.Help:
                        Help();
                        break;
                    case Type.Cancel:
                        Cancel();
                        break;
                }
        }
    }
    public void CreatePiece()
    {
        OnCreatePieceUIEvent.RaiseEvent();
    }
    public void Help()
    {

    }
    public void Cancel() 
    {
        OffCreateUIEvent.RaiseEvent();
    }
}
