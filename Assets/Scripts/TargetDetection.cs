using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] UnityEvent<Transform> TargetUpdate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform newTarget = collision.transform;
        TargetUpdate.Invoke(newTarget);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TargetUpdate.Invoke(null);
    }
}
