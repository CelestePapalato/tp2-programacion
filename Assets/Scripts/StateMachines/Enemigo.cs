using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemigo : StateMachine
{
    [SerializeField] Estado estadoAlEncontrarObjetivo;
    [SerializeField] Estado estadoAlPerderObjetivo;

    bool chase = false;

    List<IObjectTracker> trackers = new List<IObjectTracker>();
    private void Awake()
    {
        IObjectTracker[] _trackers = GetComponents<IObjectTracker>();
        trackers = _trackers.ToList();
    }

    public void TargetUpdate(Transform newTarget)
    {
        foreach (IObjectTracker tracker in trackers)
        {
            tracker.Target = newTarget;
        }

        if(newTarget) {
            chase = true;
            CambiarEstado(estadoAlEncontrarObjetivo);
        }
        else
        {
            chase = false;
            CambiarEstado(estadoAlPerderObjetivo);
        }
    }

    public override void CambiarEstado(Estado estado)
    {
        base.CambiarEstado(estado);
    }
}
