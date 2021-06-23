using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class EnemyController : StateMachineBase<EnemyController>
{
    private Transform target;
    private void Start()
    {
        System.Type type = typeof(UnitController);
        target = (GameObject.FindObjectOfType(type) as UnitController).transform;
        SetState(new EnemyController.Idol(this));

    }

    private class Idol : StateBase<EnemyController>
    {
        public Idol(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Idol");
        }
        /*public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("");
        }*/
        public override void OnUpdateState()
        {
            float f_distance = (machine.transform.position - machine.target.position).magnitude;
            if (f_distance < 3)
            {
                machine.SetState(new EnemyController.Find(machine));
            }

        }
    }

    private class Find : StateBase<EnemyController>
    {
        public Find(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Find");
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            Debug.Log("Find.onUpdateState");
        }
        /*public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("NoFind");
        }*/
    }
}
