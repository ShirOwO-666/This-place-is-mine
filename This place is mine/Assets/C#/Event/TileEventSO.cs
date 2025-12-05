using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/TileEventSO")]
public class TileEventSO : ScriptableObject
{
      public UnityAction<Tile> OnEvent;
        public void RaiseEvent(Tile a)
        {
            OnEvent?.Invoke(a);
        }
}
