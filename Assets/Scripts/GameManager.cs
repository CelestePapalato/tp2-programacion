using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TMP_Text UI_Puntuacion;
    [SerializeField] TMP_Text UI_PuntuacionMaxima;

    private int puntuacion = 0;
    private static int puntuacionMaxima = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        actualizarUI();
    }

    public void SubirPuntuacion(int puntos)
    {
        puntos = Mathf.Max(puntos, 0);
        puntuacion += puntos;
        if (puntuacion > puntuacionMaxima)
        {
            puntuacionMaxima = puntuacion;
        }
        actualizarUI();
    }

    private void actualizarUI()
    {
        UI_PuntuacionMaxima.text = puntuacionMaxima.ToString();
        UI_Puntuacion.text = puntuacion.ToString();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
