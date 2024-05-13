using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPuntuacion : MonoBehaviour
{
    [SerializeField] TMP_Text UI_Puntuacion;
    [SerializeField] TMP_Text UI_PuntuacionMaxima;

    private void Start()
    {
        actualizarUI();
    }
    private void actualizarUI()
    {
        UI_PuntuacionMaxima.text = GameManager.PuntuacionMaxima.ToString();
        UI_Puntuacion.text = GameManager.Puntuacion.ToString();
    }
}
