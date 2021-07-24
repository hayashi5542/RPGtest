using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class ItemUserParam : CsvDataParam
{
    public int Item_ID;
    public int num; //本数
    public int craft_count;
}

public class ItemUser : CsvData<ItemUserParam>
{
    public void Add(int item_id)
    {
        ItemUserParam param = list.Find(t => t.Item_ID == item_id);
        if (param != null)
        {
            param.num += 1;
        }
        else
        {
            param = new ItemUserParam()
            {
                Item_ID = item_id,
                num = 1
            };
            list.Add(param);
        }
    }
}
