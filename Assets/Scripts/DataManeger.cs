using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataManeger : Singleton<DataManeger>
{
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
}
