using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class EnemyUnitParam : CsvDataParam
{
    public int Enemy_ID;
    public string Enemy_name;
    public int Enemy_HP;
    public int Enemy_Attack;
    public int Enemy_Difence;
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

public class EnemyUnit : CsvData<EnemyUnitParam>
{

}
