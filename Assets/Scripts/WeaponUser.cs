using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class WeaponUserParam : CsvDataParam
{
    public int Weapon_ID;
    public int num; //本数
    public int craft_count;
}

public class WeaponUser : CsvData<WeaponUserParam>
{
    public void Add(int weapon_id)
    {
        WeaponUserParam param = list.Find(t => t.Weapon_ID == weapon_id);
        if(param != null)
        {
            param.num += 1;
        }
        else
        {
            param = new WeaponUserParam()
            {
                Weapon_ID = weapon_id,
                num = 1
            };
            list.Add(param);
        }
    }
}
