using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public static class GameManager
{
    public static UnityAction nuevaPuntuacion;
    public static UnityAction nuevaPuntuacionMaxima;

    private static int puntuacion = 0;
    public static int Puntuacion { get => puntuacion; }
    private static int puntuacionMaxima = 0;
    public static int PuntuacionMaxima { get => puntuacionMaxima; }

    public static void SubirPuntuacion(int puntos)
    {
        puntos = Mathf.Max(puntos, 0);
        puntuacion += puntos;
        nuevaPuntuacion.Invoke();
        if (puntuacion > puntuacionMaxima)
        {
            puntuacionMaxima = puntuacion;
            nuevaPuntuacionMaxima.Invoke();
        }
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        puntuacion = 0;
    }
}
