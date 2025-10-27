using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInteractionObject : MonoBehaviour, ITriggerInteractable
{
    [SerializeField] UnityEvent OnTriggerEnter;
    [SerializeField] UnityEvent OnTriggerExit;


    public void TriggerEnterInteraction()
    {
        OnTriggerEnter?.Invoke();
    }

    public void TriggerExitInteraction()
    {
        OnTriggerExit?.Invoke();
    }
    
}
