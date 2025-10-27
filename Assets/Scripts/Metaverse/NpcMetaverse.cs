using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcMetaverse : Character, IInteractable, ITriggerInteractable
{
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private UnityEvent OnTriggerEnter;
    [SerializeField] private UnityEvent OnTriggerExit;

    public void Interaction()
    {
        OnInteract?.Invoke();

        // 필요하면 쓰기
    }

    public void TriggerEnterInteraction()
    {
        OnTriggerEnter?.Invoke();
    }

    public void TriggerExitInteraction()
    {
        OnTriggerExit?.Invoke();
    }
    
}
