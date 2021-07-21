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
    private UnitMover unitmover;
    private bool fightMode;
    public int player_HP;
    private void Start()
    {
        animator = GetComponent<Animator>();
        unitmover = GetComponent<UnitMover>();
        SetState(new UnitController.Idle(this));
        fightMode = true;
        player_HP = 20;
    }
    private bool Find_Enemy(ref EnemyController enemy)
    {
        foreach (EnemyController e in EnemyManager.Instance.enemylist)
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
        //Debug.Log("player_AttackHit");
    }

    public void OnAttackEnd()
    {
        AttackEndHandler.Invoke();
        //Debug.Log("player_AttackEnd");
    }

    public void FightButtonDown()
    {
        fightMode = true;
    }

    public void EscapeButtonDown()
    {
        fightMode = false;
        SetState(new UnitController.Idle(this));
        Debug.Log(fightMode);
    }

    public void Damaged(int p_damage)
    {
        player_HP -= p_damage;
    }

    private class Idle : StateBase<UnitController>
    {
        public Idle(UnitController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.unitmover.can_Move = true;
            //Debug.Log("Idle");
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            
            EnemyController enemy = null;
  
            if (machine.fightMode == true &&machine.Find_Enemy(ref enemy))
            {
                Debug.Log(machine.fightMode);
                //Debug.Log("Go_Buttle");
                machine.SetState(new UnitController.Battle(machine, enemy));              
            }
        }
    }

    private class Battle : StateBase<UnitController>
    {
        private EnemyController enemy;
        private float timer;
        public GameObject mode;

        public Battle(UnitController machine, EnemyController enemy):base(machine)
        {
            this.machine = machine;
            this.enemy = enemy;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.unitmover.can_Move = false;
            //Debug.Log("player_Battle");
            //Debug.Log(machine.fightMode);
            //machine.GetComponent<UnitMover>().enabled = false;
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            
            timer += Time.deltaTime;
            if (timer > 1)
            {
                machine.SetState(new UnitController.Attack(machine, enemy));
            }
            else if (machine.Find_Enemy(ref enemy) == false)
            {
                machine.SetState(new UnitController.Idle(machine));
                //machine.GetComponent<UnitMover>().enabled = true;
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
            //machine.GetComponent<UnitMover>().enabled = false;
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
            //Debug.Log("Attack");
        }
        public override void OnExitState()
        {
            machine.AttackHitHandler.RemoveAllListeners();
            machine.AttackEndHandler.RemoveAllListeners();
            //base.OnExitState();
        }


    }
}
