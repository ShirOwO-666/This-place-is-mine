using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUI : MonoBehaviour
{
 public enum Type
    {
        CreateUI,CreatePiece,Help,Cancel
    }
    public Type type;
    public Collider2D ThisCollider;
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
    public void SetCreateUI()
    {

    }
    public void CreatePiece()
    {

    }
    public void Help()
    {

    }
    public void Cancel() 
    {
        
    }
}
