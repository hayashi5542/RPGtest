using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class WeaponUnitParam : CsvDataParam
{
    public int Weapon_ID;
    public string Weapon_name;
    public int Attack;
    /*public int Wood_sword;
    public int Stone_sword;
    public int Bronze_sword;
    public int Iron_sword;
    public int Steel_sword;
    public int Platinam_sword;
    public int Diamond_sword;
    public int Orichalcum_sword;
    public int Magic_sword;
    public int Brave_sword;*/
}

public class WeaponUnit : CsvData<WeaponUnitParam>
{

}
