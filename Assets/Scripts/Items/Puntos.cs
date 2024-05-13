using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntos : MonoBehaviour
{
    [SerializeField] int puntos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.SubirPuntuacion(puntos);
        Destroy(gameObject);
    }
}
