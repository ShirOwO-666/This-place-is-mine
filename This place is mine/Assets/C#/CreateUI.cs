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
    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
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
