using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorProyectiles : Estado, IObjectTracker
{
    [SerializeField] bool stopMovement = true;
    [SerializeField] int projectileQuantity;
    [SerializeField] float timeBetweenShots;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Estado nextState;


    Transform target;
    public Transform Target { get => target; set => target = value; }
    Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        if (!target) {
            personaje.CambiarEstado(null);
            return;
        }
        if(stopMovement && movement)
        {
            movement.Direction = Vector3.zero;
        }
        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        int shots = 0;
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

    public override void DaņoRecibido()
    {
        StopCoroutine(ShootProjectiles());
    }

    public override void Salir()
    {
        base.Salir();
        StopCoroutine(ShootProjectiles());
    }

}
