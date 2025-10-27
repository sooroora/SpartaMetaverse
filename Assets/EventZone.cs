using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventZone : MonoBehaviour
{
    [SerializeField] private UnityAction OnTriggerEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter?.Invoke();
    }
}