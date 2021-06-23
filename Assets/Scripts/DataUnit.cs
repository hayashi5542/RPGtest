using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataUnitParam : CsvDataParam
{
    public int HP_current;
    public int HP_max;
    public int EXP_current;
    public int EXP_max;
    public int Attack;
    public int Speed;
    public int Defence;
}

public class DataUnit : CsvData<DataUnitParam>
{

}
