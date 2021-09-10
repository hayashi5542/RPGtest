using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using anogamelib;
using UnityEngine.Events;
using TMPro;

public class UnitController : StateMachineBase<UnitController>
{
    private UnityEvent AttackHitHandler = new UnityEvent();
    private UnityEvent AttackEndHandler = new UnityEvent();
    private Animator animator;
    private UnitMover unitmover;
    private DataManeger dataManeger;
    private bool fightMode;
    public int player_HP;
    public int player_EXP;
    public int EXP_max;
    private ButtonController buttonController;
    public int playerLevel;
    public int playerAttack;
    public int weaponAttack;
    public int weaponAttackPlus;
    public int statusAttack;
    private PanelSwordStanby panelSwordStanby;
    private int current_weaponID;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textEXP;
    public TextMeshProUGUI textLevel;
    public Image pointSign;
    public GameObject gameOverPanel;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        buttonController = GetComponent<ButtonController>();
        gameOverPanel = GetComponent<GameObject>();
        //animator.SetBool("ModeFight", true);
        //animator.SetBool("ModeEscape", false);
        unitmover = GetComponent<UnitMover>();
        dataManeger = GetComponent<DataManeger>();
        SetState(new UnitController.Idle(this));
        fightMode = true;
        player_HP = DataManeger.Instance.unitParam.HP_max;
        textHP.text = DataManeger.Instance.unitParam.HP_current + "/" + DataManeger.Instance.unitParam.HP_max;
        player_EXP = DataManeger.Instance.unitParam.EXP_current;
        EXP_max = DataManeger.Instance.unitParam.EXP_max;
        textEXP.text = DataManeger.Instance.unitParam.EXP_current + "/" + DataManeger.Instance.unitParam.EXP_max;
        //playerLevel = DataManeger.Instance.unitParam.level;
        playerLevel = DataManeger.Instance.gameInfo.GetInt(Define.keyLevel);
        textLevel.text = DataManeger.Instance.unitParam.level.ToString();

        current_weaponID = DataManeger.Instance.gameInfo.GetInt(Define.keyEquipWeaponID);
        SetAttack(current_weaponID);
        //DataManeger.Instance.pointer.enabled = false;
        //pointSign.enabled = false;
        

        //playerLevel = DataManeger.Instance.gameHUD.textLevel;
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
        //transform.Translate(200f, 0, 0);
        //buttonController.SlideFightButton();
        //animator.SetBool("ModeFight", true);
        //animator.SetBool("ModeEscape", false);
        Debug.Log(fightMode);
    }

    public void EscapeButtonDown()
    {
        fightMode = false;
        SetState(new UnitController.Idle(this));
        //buttonController.SlideEscapeButton();
        //animator.SetBool("ModeFight", false);
        //animator.SetBool("ModeEscape", true);
        Debug.Log(fightMode);
    }

    public void SetAttack(int _weaponID)
    {
        //Debug.Log("SetAttack");
        WeaponUnitParam weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == _weaponID);
        WeaponUserParam weaponUser = DataManeger.Instance.weaponUser.list.Find(p => p.Weapon_ID == _weaponID);
        weaponAttack = weapon.Attack + weaponUser.craft_count;
        statusAttack = DataManeger.Instance.unitParam.status_ATK;

        playerAttack = playerLevel + weaponAttack + statusAttack;
    }

    public void Damaged(int p_damage)
    {
        player_HP -= p_damage;
        DataManeger.Instance.unitParam.HP_current -= p_damage;
        DataManeger.Instance.SetHP(DataManeger.Instance.unitParam.HP_current);
        textHP.text = DataManeger.Instance.unitParam.HP_current + "/" + DataManeger.Instance.unitParam.HP_max;

        if (DataManeger.Instance.unitParam.HP_current <= 0)
        {
            SetState(new UnitController.Die(this));
            //animator.SetTrigger("Die");
            //Debug.Log("DieDamage");
        }
    }

    public void Heal(int _HP)
    {
        textHP.text = DataManeger.Instance.unitParam.HP_current + "/" + DataManeger.Instance.unitParam.HP_max;
    }

    public void GetEXP(int _EXP)
    {
        player_EXP += _EXP;
        DataManeger.Instance.unitParam.EXP_current += _EXP;
        
        if (player_EXP >= EXP_max) //レベルアップしたとき
        {
            //playerLevel += 1;
            playerLevel += 1;
            textLevel.text = playerLevel.ToString();
            //ataManeger.LevelUp();
            Debug.Log(playerLevel);
            player_EXP -= EXP_max;
            DataManeger.Instance.unitParam.EXP_current -= DataManeger.Instance.unitParam.EXP_max;
            //player_EXP -= need_EXP;

            //need_EXP += 10;
            EXP_max += 10;
            DataManeger.Instance.unitParam.EXP_max += 10;

            DataManeger.Instance.unitParam.status += 3;
            DataManeger.Instance.pointer.enabled = true;
        }
        DataManeger.Instance.SetEXP(DataManeger.Instance.unitParam.EXP_current, DataManeger.Instance.unitParam.EXP_max);
        textEXP.text = DataManeger.Instance.unitParam.EXP_current + "/" + DataManeger.Instance.unitParam.EXP_max;

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
                if (enemy.Damage(machine.playerAttack))
                {
                    machine.GetEXP(10);
                }
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

    private class Die : StateBase<UnitController>
    {
        public Die(UnitController _machine) : base(_machine)
        {
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.animator.SetTrigger("DieTrigger");
            //machine.gameOverPanel.SetActive(true);
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

        }

        public override void OnExitState()
        {
            base.OnExitState();
            //machine.animator.SetTrigger("RestartTrigger");
            //machine.player_HP += 10;
        }
    }
}
