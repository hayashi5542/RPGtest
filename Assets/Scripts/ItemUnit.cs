using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class ItemUnitParam : CsvDataParam
{
    public int Item_ID;
    public string Item_name;
    public int prob;
}

public class ItemUnit : CsvData<ItemUnitParam>
{

}
