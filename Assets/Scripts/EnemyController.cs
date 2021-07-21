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
    public int HP;
    public float ground_Hight;
    private float Attack_delay;
    private void Start()
    {
        System.Type type = typeof(UnitController);
        target = (GameObject.FindObjectOfType(type) as UnitController).transform;
        animator = GetComponent<Animator>();
        SetState(new EnemyController.Idol(this));
        EnemyManager.Instance.Add(this);
        HP = 5;
        ground_Hight = 5;
        
    }
    public void OnAttackHit()
    {
        Debug.Log("OnAttackHit");
        AttackHitHandler.Invoke();
    }
    public void OnAttackEnd()
    {
        //Debug.Log("OnAttackEnd");
        AttackEndHandler.Invoke();
    }

    public bool isFind()
    {
        if(HP <= 0)
        {
            return false;
        }
        float f_distance = (transform.position - target.position).magnitude;
        return f_distance < 3;
    }

    public void Damage(int damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            SetState(new EnemyController.Die(this));
            //animator.SetTrigger("Die");
            Debug.Log("DieDamage");
        }
    }

    /*public bool DieMotion()
    {
        if(HP <= 0)
        {
            return true;
        }
        return false;
    }*/

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
            machine.Attack_delay = 3;
            Debug.Log("Idol");
        }

        public override void OnUpdateState()
        {
            if (machine.isFind())
            {
                machine.SetState(new EnemyController.Find(machine));
            }

        }
    }

    private class Find : StateBase<EnemyController>
    {
        private float timer;
        private UnitController unit;
        public Find(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.animator.SetInteger("battle", 1);
            //machine.animator.SetBool("Smash Attack", true);
            //machine.animator.SetInteger("battle", 1);
            unit = machine.target.gameObject.GetComponent<UnitController>();

            Debug.Log("Find");
        }
        public override void OnUpdateState()
        {
            //base.OnUpdateState();
            timer += Time.deltaTime;
            machine.transform.LookAt(machine.target);
            float f_distance = (machine.transform.position - machine.target.position).magnitude;


            /*if (machine.DieMotion() == true)
            {
                machine.SetState(new EnemyController.Die(machine));

            }*/
            if (timer > machine.Attack_delay)
            {
                machine.SetState(new EnemyController.Attack(machine, unit));
            }
           else if(machine.isFind() == false)
            {
                machine.SetState(new EnemyController.Idol(machine));
                //Debug.Log("Find.onUpdateState");
            }
                       
        }

    }

    private class Attack : StateBase<EnemyController>
    {
       private UnitController unit;
        public Attack(EnemyController _machine,UnitController _unit) : base(_machine)
        {
            //this.machine = _machine;
            this.unit = _unit;
        }

        public override void OnEnterState()
        {
            machine.Attack_delay = 1;
            machine.AttackHitHandler.AddListener(() =>
            {               
                Debug.Log("Attack>Player");
                unit.Damaged(2);
            });

            machine.AttackEndHandler.AddListener(() =>
            {
                Debug.Log("Enemy_AttackEnd");
                machine.SetState(new EnemyController.Find(machine));
                
            }); 
            
            //base.OnEnterState();
            machine.animator.SetTrigger("AttackTrigger");
            Debug.Log("Enemy_Attack");
        }
        public override void OnExitState()
        {
            machine.AttackHitHandler.RemoveAllListeners();
            machine.AttackEndHandler.RemoveAllListeners();
            //base.OnExitState();
        }
    }

    private class Die : StateBase<EnemyController>
    {
        private float Die_timer;
        //private GameObject break_effect;
        public Die(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            //base.OnEnterState();
            machine.animator.SetTrigger("DieTrigger");
            //machine.animator.//SetTrigger("ExitTrigger");
            //Destroy(machine.gameObject, 4.0f);
            //GameObject effect = Instantiate(break_effect) as GameObject;
            Debug.Log("Enemy_Die");
        }

        public override void OnUpdateState()
        {
            //base.OnUpdateState();
            Die_timer += Time.deltaTime;
            if(Die_timer > 4.0f)
            {
                machine.SetState(new EnemyController.Down(machine));
                //Destroy(machine.gameObject);
            }
        }
    }

    private class Down : StateBase<EnemyController>
    {
        private float speed;
        private float down_timer;
        public Down(EnemyController _machine) : base(_machine)
        {
        }

        public override void OnEnterState()
        {
            //base.OnEnterState();
           
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();
            speed = -0.1f;
            down_timer += Time.deltaTime;
            if(machine.transform.position.y > -5)
            {
                machine.transform.Translate(0, speed, 0);
            }

            if(down_timer > 10.0f)
            {
                machine.SetState(new EnemyController.Idol(machine));
            }
            
        }
        public override void OnExitState()
        {
            //base.OnExitState();
            machine.HP = 5;
            machine.animator.SetTrigger("IdleTrigger");
            machine.transform.position = new Vector3(machine.transform.position.x, machine.ground_Hight, machine.transform.position.z);
        }
    }
}
