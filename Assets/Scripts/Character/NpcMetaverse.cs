using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcMetaverse : Character, IInteractable
{
    [SerializeField] private UnityEvent OnInteract; 
    
    public bool Interaction()
    {
        
        OnInteract.Invoke();
        
        // 필요하면 쓰기
        return true;
    }
    
}
