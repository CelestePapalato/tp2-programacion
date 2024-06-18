using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static UnityAction<int> OnNuevaPuntuacion;
    public static UnityAction<int> OnNuevaPuntuacionMaxima;
    public static UnityEvent OnLevelCompleted;

    private int puntuacion = 0;
    public int Puntuacion { get => puntuacion; }
    private static int puntuacionMaxima = 0;
    public int PuntuacionMaxima { get => puntuacionMaxima; }

    private void Awake()
    {
        Instance = this;
    }

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
        OnNuevaPuntuacion?.Invoke(puntuacion);
        OnNuevaPuntuacionMaxima?.Invoke(puntuacionMaxima);
    }

    private void SubirPuntuacion(int puntos)
    {
        puntos = Mathf.Max(puntos, 0);
        puntuacion += puntos;
        OnNuevaPuntuacion?.Invoke(puntuacion);
        if (puntuacion > puntuacionMaxima)
        {
            puntuacionMaxima = puntuacion;
            OnNuevaPuntuacionMaxima?.Invoke(puntuacionMaxima);
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        puntuacion = 0;
    }

    public void NivelCompletado()
    {
        OnLevelCompleted?.Invoke();
    }
}
