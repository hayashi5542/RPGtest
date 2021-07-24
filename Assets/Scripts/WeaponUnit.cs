using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class WeaponUnitParam : CsvDataParam
{
    public int Weapon_ID;
    public string Weapon_name;
    public int Attack;
    public int prob;
}

public class WeaponUnit : CsvData<WeaponUnitParam>
{

}
