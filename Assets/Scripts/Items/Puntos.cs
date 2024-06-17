using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puntos : MonoBehaviour
{
    public static UnityAction<int> OnCollected;

    [SerializeField] int puntos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollected?.Invoke(puntos);
        Destroy(gameObject);
    }
}
