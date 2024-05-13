using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPuntuacion : MonoBehaviour
{
    [SerializeField] TMP_Text UI_Puntuacion;
    [SerializeField] TMP_Text UI_PuntuacionMaxima;

    private void Awake()
    {
        GameManager.nuevaPuntuacion += actualizarPuntuacion;
        GameManager.nuevaPuntuacionMaxima += actualizarPuntuacionMaxima;
    }

    private void Start()
    {
        actualizarPuntuacion();
        actualizarPuntuacionMaxima();
    }

    private void actualizarPuntuacion()
    {
        UI_Puntuacion.text = GameManager.Puntuacion.ToString();
    }

    private void actualizarPuntuacionMaxima()
    {
        UI_PuntuacionMaxima.text = GameManager.PuntuacionMaxima.ToString();
    }
}
