using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resultados : MonoBehaviour
{
    [Header("Ganar")]
    [SerializeField] Canvas canvasGanar;
    [SerializeField] TMP_Text textPuntaje;
    [Header("Perder")]
    [SerializeField] Canvas canvasPerder;

    private void Awake()
    {
        canvasGanar?.gameObject.SetActive(false);
        canvasPerder?.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += MostrarResultados;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= MostrarResultados;
    }

    public void MostrarResultados(bool nivelCompletado)
    {
        if(nivelCompletado)
        {
            canvasGanar?.gameObject.SetActive(true);
            if (textPuntaje)
            {
                textPuntaje.text = GameManager.Instance.Puntuacion + "";
            }
        }
        else
        {
            canvasPerder?.gameObject.SetActive(true);
        }
    }
}
