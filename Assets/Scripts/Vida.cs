using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vida : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Healed;

    int health;


    private void Awake()
    {
        health = maxHealth;
    }

    public void Heal(int healPoints)
    {
        health = Mathf.Clamp(health + healPoints, 0, maxHealth);
        Healed.Invoke(health);
    }
}
