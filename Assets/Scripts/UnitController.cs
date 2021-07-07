using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using UnityEngine.Events;

public class UnitController : StateMachineBase<UnitController>
{
    private UnityEvent AttackHitHandler = new UnityEvent();
    private UnityEvent AttackEndHandler = new UnityEvent();
    private Animator animator;
    private void Start()
    {
        SetState(new UnitController.Idle(this));
        animator = GetComponent<Animator>();
    }
    private bool Find_Enemy(ref EnemyController enemy)
    {
        foreach(EnemyController e in EnemyManager.Instance.enemylist)
        {
            if (e.isFind())
            {
                enemy = e;
                return true;
            }
        }
        return false;
    }

    public void OnAttackHit()
    {
        AttackHitHandler.Invoke();
        Debug.Log("player_AttackHit");
    }

    public void OnAttackEnd()
    {
        AttackEndHandler.Invoke();
        Debug.Log("player_AttackEnd");
    }

    private class Idle : StateBase<UnitController>
    {
        public Idle(UnitController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Idle");
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            EnemyController enemy = null;
            if (machine.Find_Enemy(ref enemy))
            {
                machine.SetState(new UnitController.Battle(machine, enemy));
               
            }
        }
    }

    private class Battle : StateBase<UnitController>
    {
        private EnemyController enemy;
        private float timer;

        public Battle(UnitController machine, EnemyController enemy):base(machine)
        {
            this.machine = machine;
            this.enemy = enemy;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Battle");
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            timer += Time.deltaTime;
            if (timer > 3)
            {
                machine.SetState(new UnitController.Attack(machine, enemy));
            }
            else if (machine.Find_Enemy(ref enemy) == false)
            {
                machine.SetState(new UnitController.Idle(machine));
                //Debug.Log("Find.onUpdateState");
            }
        }

    }

    private class Attack : StateBase<UnitController>
    {
        private EnemyController enemy;

        public Attack(UnitController machine, EnemyController enemy):base(machine)
        {
            this.machine = machine;
            this.enemy = enemy;
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.AttackHitHandler.AddListener(() =>
            {
                enemy.Damage(2);
            }
            );

            machine.AttackEndHandler.AddListener(() =>
            {
                machine.SetState(new UnitController.Battle(machine,enemy));
            });

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
    //}
}
