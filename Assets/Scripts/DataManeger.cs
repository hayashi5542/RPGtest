using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using TMPro;
using UnityEngine.UI;

public class DataManeger : Singleton<DataManeger>
{
    public KVS gameInfo;
    public GameHUD gameHUD;
    public TextAsset taUnitData;
    public DataUnit dataUnit;
    public TextAsset taUnitWeapon;
    public WeaponUnit weaponUnit;
    public TextAsset taUnitItem;
    public ItemUnit itemUnit;
    public TextAsset taUnitEnemy;
    public EnemyUnit enemyUnit;
    public DataUnitParam unitParam;
    public ParamGauge HPgauge;
    public ParamGauge EXPgauge;
    public WeaponUser weaponUser;
    public ItemUser itemUser;
    public TextMeshProUGUI textLevel;
    public UnitController unitController;
    public Image pointer;
    public TextMeshProUGUI potionTextNum;
   /* public Button getPointATK;
    public Button getPointVIT;
    public Button getPointAGI;
    public Button getPointLUK;
    public TextMeshProUGUI pointATK;
    public TextMeshProUGUI pointVIT;
    public TextMeshProUGUI pointAGI;
    public TextMeshProUGUI pointLUK;*/

    public override void Initialize()
    {
        base.Initialize();
        gameInfo = new KVS();
        gameInfo.SetSaveFilename("gameInfo");
        if(gameInfo.Load() == false)
        {
            gameInfo.SetInt(Define.keyGold, Define.defaultGold);
            gameInfo.SetInt(Define.keyJem, Define.defaultJem);
            gameInfo.SetInt(Define.keyEquipWeaponID, Define.defaultWeaponID);
            gameInfo.SetInt(Define.keyCraftItemID, Define.defaultCraftItemID);
            gameInfo.SetInt(Define.keyLevel, Define.defaultLevel);
            gameInfo.Save();
        }
        gameInfo.AddInt(Define.keyGold, 100);
        gameInfo.Save();
        gameHUD.textGold.text = gameInfo.GetInt(Define.keyGold).ToString();
        gameHUD.textJem.text = gameInfo.GetInt(Define.keyJem).ToString();
        gameHUD.textLevel.text = gameInfo.GetInt(Define.keyLevel).ToString();
        //GameHUD.Instance.textGold.text = gameInfo.GetInt(Define.keyGold).ToString();
        //GameHUD.Instance.textJem.text = gameInfo.GetInt(Define.keyJem).ToString();
        dataUnit = new DataUnit();
        dataUnit.Load(taUnitData);
        unitParam = dataUnit.list[0];
        Debug.Log(unitParam.HP_current);
        weaponUnit = new WeaponUnit();
        weaponUnit.Load(taUnitWeapon);
        itemUnit = new ItemUnit();
        itemUnit.Load(taUnitItem);
        //unitParam = weaponUnit.list[0];
        /*foreach (WeaponUnitParam p in weaponUnit.list)
        {
            Debug.Log(p.Weapon_name);
        }*/
        weaponUser = new WeaponUser();
        weaponUser.SetSaveFilename(Define.weaponUserFile);
        if(weaponUser.Load() == false)
        {
            weaponUser.Add(Define.defaultWeaponID);
            weaponUser.Save();
        }

        itemUser = new ItemUser();
        itemUser.SetSaveFilename(Define.itemUserFile);
        if (itemUser.Load() == false)
        {
            itemUser.Save();
        }




        enemyUnit = new EnemyUnit();
        enemyUnit.Load(taUnitEnemy);
        /*foreach (EnemyUnitParam e in enemyUnit.list)
        {
            Debug.Log(e.Enemy_name);
        }*/
        HPgauge.Init(unitParam.HP_current,unitParam.HP_max);
        EXPgauge.Init(unitParam.EXP_current, unitParam.EXP_max);

        textLevel.text = DataManeger.Instance.unitParam.level.ToString();

        int potionNum = 0;
        ItemUserParam potion = DataManeger.Instance.itemUser.list.Find(p => p.Item_ID == 1);
        if (potion != null)
        {
            potionNum = potion.num;
        }
        potionTextNum.text = potionNum.ToString();


        pointer.enabled = false;
    }

    public void LevelUp()
    {
        //textLevel.text = DataManeger.Instance.unitParam.level.ToString();
    }

    public void  WeaponGacha(/*int count*/)
    {
        /*if (jem < (5*count))
        {
            Debug.Log("ガチャをひけません");
        }*/
        gameInfo.AddInt(Define.keyJem, -5);
        gameInfo.Save();
        gameHUD.textJem.text = gameInfo.GetInt(Define.keyJem).ToString();
        //Debug.Log(Define.keyJem);
    }
    public void ItemGacha()
    {
        gameInfo.AddInt(Define.keyGold, -10);
        gameInfo.Save();
        gameHUD.textGold.text = gameInfo.GetInt(Define.keyGold).ToString();
        //Debug.Log(Define.keyGold);
    }

    public void PowerUP(int _weaponID)
    {
        gameInfo.AddInt(Define.keyGold, -100);
        WeaponUserParam weaponUserParam = weaponUser.list.Find(p => p.Weapon_ID == _weaponID);
        weaponUserParam.craft_count += 1;
        gameInfo.Save();
        weaponUser.Save();
        gameHUD.textGold.text = gameInfo.GetInt(Define.keyGold).ToString();
    }

    public void SetHP(int _currentHP)
    {
        HPgauge.Set(_currentHP);
    }

    public void SetEXP(int _currentEXP, int _EXP_Max)
    {
        EXPgauge.Init(_currentEXP, _EXP_Max);
    }

    public void OnButtonHeal()
    {
        if(DataManeger.Instance.unitParam.HP_current < DataManeger.Instance.unitParam.HP_max)
        {
            unitParam.HP_current += (int)((float)unitParam.HP_max * 0.3f);
            if(unitParam.HP_current >= unitParam.HP_max)
            {
                unitParam.HP_current = unitParam.HP_max;
            }
            SetHP(unitParam.HP_current);

            int potionNum = 0;
            ItemUserParam potion = DataManeger.Instance.itemUser.list.Find(p => p.Item_ID == 1);
            potion.num -= 1;
            if (potion != null)
            {
                potionNum = potion.num;
            }
            potionTextNum.text = potionNum.ToString();
        }
        
        unitController.textHP.text = DataManeger.Instance.unitParam.HP_current + "/" + DataManeger.Instance.unitParam.HP_max; ;
    }

    /*public bool PointSign()
    {

    }*/



    /*public void DecreaseHP(int damage)
    {
        unitParam.HP_current -= damage;
    }*/
}
