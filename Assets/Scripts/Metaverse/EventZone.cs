using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventZone : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            OnTriggerEnter?.Invoke();
        }
    }
}