using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorProyectiles : Estado, IObjectTracker
{
    [SerializeField] bool stopRigidbody = true;
    [SerializeField] int projectileQuantity;
    [SerializeField] float timeBetweenShots;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Estado nextState;


    Transform target;
    public Transform Target { get => target; set => target = value; }
    Rigidbody2D rb;
    int shots = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        if (!target) {
            personaje.CambiarEstado(null);
            return;
        }
        if(stopRigidbody && rb)
        {
            rb.velocity = Vector3.zero;
        }
        shots = 0;
        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        while (shots < projectileQuantity)
        {
            if(!target)
            {
                break;
            }
            Projectile projectile = Instantiate(projectilePrefab, spawnPoint);
            Vector2 direction = target.position - transform.position;
            projectile.Direction = direction;
            shots++;
            yield return new WaitForSeconds(timeBetweenShots);
        }
        personaje.CambiarEstado(nextState);
    }

    public override void DañoRecibido()
    {
        StopCoroutine(ShootProjectiles());
    }

    public override void Salir()
    {
        StopCoroutine(ShootProjectiles());
    }

}
