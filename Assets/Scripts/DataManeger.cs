﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

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

    /*public void DecreaseHP(int damage)
    {
        unitParam.HP_current -= damage;
    }*/
}
