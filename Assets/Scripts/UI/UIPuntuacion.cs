using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPuntuacion : MonoBehaviour
{
    [SerializeField] TMP_Text UI_Puntuacion;
    [SerializeField] TMP_Text UI_PuntuacionMaxima;

    private void OnEnable()
    {
        GameManager.OnNuevaPuntuacion += actualizarPuntuacion;
        GameManager.OnNuevaPuntuacionMaxima += actualizarPuntuacionMaxima;
    }

    private void OnDisable()
    {
        GameManager.OnNuevaPuntuacion -= actualizarPuntuacion;
        GameManager.OnNuevaPuntuacionMaxima -= actualizarPuntuacionMaxima;
    }

    private void actualizarPuntuacion(int puntacion)
    {
        UI_Puntuacion.text = puntacion + "";
    }

    private void actualizarPuntuacionMaxima(int puntuacionMaxima)
    {
        UI_PuntuacionMaxima.text = puntuacionMaxima + "";
    }
}
