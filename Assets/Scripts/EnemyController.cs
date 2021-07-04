using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using anogamelib;

public class EnemyController : StateMachineBase<EnemyController>
{
    public Transform target;
    private Animator animator;
    //private GameObject player;
    private UnityEvent AttackHitHandler = new UnityEvent();
    private UnityEvent AttackEndHandler = new UnityEvent();
    private void Start()
    {
        System.Type type = typeof(UnitController);
        target = (GameObject.FindObjectOfType(type) as UnitController).transform;
        animator = GetComponent<Animator>();
        SetState(new EnemyController.Idol(this));
        //player = GameObject.Find("RPGHeroPBR");
        
    }
    public void OnAttackHit()
    {
        Debug.Log("OnAttackHit");
        AttackHitHandler.Invoke();
    }
    public void OnAttackEnd()
    {
        Debug.Log("OnAttackEnd");
        AttackEndHandler.Invoke();
    }

    private class Idol : StateBase<EnemyController>
    {
        public Idol(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.animator.SetInteger("battle", 0);
            //machine.animator.SetInteger("battle", 0);
            Debug.Log("Idol");
        }

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
        private float timer;
        public Find(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.animator.SetInteger("battle", 1);
            //machine.animator.SetBool("Smash Attack", true);
            //machine.animator.SetInteger("battle", 1);
            
            
            Debug.Log("Find");
        }
        public override void OnUpdateState()
        {
            //base.OnUpdateState();
            timer += Time.deltaTime;
            machine.transform.LookAt(machine.target);
            float f_distance = (machine.transform.position - machine.target.position).magnitude;
            if(timer > 3)
            {
                machine.SetState(new EnemyController.Attack(machine));
            }

            if(f_distance > 3)
            {
                machine.SetState(new EnemyController.Idol(machine));
                //Debug.Log("Find.onUpdateState");
            }
                       
        }

    }

    private class Attack : StateBase<EnemyController>
    {
        public Attack(EnemyController _machine) : base(_machine)
        {
        }

        public override void OnEnterState()
        {
            machine.AttackHitHandler.AddListener(() =>
            {
                Debug.Log("Attack>Player");
            });

            machine.AttackEndHandler.AddListener(() =>
            {
                machine.SetState(new EnemyController.Find(machine));
            }); 
            
            //base.OnEnterState();
            machine.animator.SetTrigger("AttackTrigger");
            Debug.Log("Attack");
        }
        public override void OnExitState()
        {
            machine.AttackHitHandler.RemoveAllListeners();
            machine.AttackEndHandler.RemoveAllListeners();
            //base.OnExitState();
        }
    }
}
