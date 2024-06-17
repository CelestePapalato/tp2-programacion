using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static UnityAction<int> nuevaPuntuacion;
    public static UnityAction<int> nuevaPuntuacionMaxima;

    private int puntuacion = 0;
    public int Puntuacion { get => puntuacion; }
    private static int puntuacionMaxima = 0;
    public int PuntuacionMaxima { get => puntuacionMaxima; }

    private void OnEnable()
    {
        Enemigo.OnDead += SubirPuntuacion;
        Puntos.OnCollected += SubirPuntuacion;
        Player.OnDead += GameOver;
    }

    private void OnDisable()
    {
        Enemigo.OnDead -= SubirPuntuacion;
        Puntos.OnCollected -= SubirPuntuacion;
        Player.OnDead -= GameOver;
    }

    private void Start()
    {
        nuevaPuntuacion?.Invoke(puntuacion);
        nuevaPuntuacionMaxima?.Invoke(puntuacionMaxima);
    }

    public void SubirPuntuacion(int puntos)
    {
        puntos = Mathf.Max(puntos, 0);
        puntuacion += puntos;
        nuevaPuntuacion?.Invoke(puntuacion);
        if (puntuacion > puntuacionMaxima)
        {
            puntuacionMaxima = puntuacion;
            nuevaPuntuacionMaxima?.Invoke(puntuacionMaxima);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        puntuacion = 0;
    }
}
