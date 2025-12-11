using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/PieceEventSO")]
public class PieceEventSO : ScriptableObject
{
    public UnityAction<piece> OnEvent;

    public void RaiseEvent(piece a)
    {
        OnEvent?.Invoke(a);
    }
}
