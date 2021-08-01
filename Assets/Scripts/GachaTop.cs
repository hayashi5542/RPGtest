using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaTop : MonoBehaviour
{
    public PanelSwordGacha panelSwordGacha;
    public PanelSwordGacha10 panelSwordGacha10;
    public PanelItemGacha panelItemGacha;
    public PanelItemGacha10 panelItemGacha10;
    private void OnEnable()
    {
        
    }

    public void WeaponGacha(int count)
    {
        for(int i = 0; i < count; i++)
        {
            WeaponUnitParam weapon = UtilRand.GetParam(ref DataManeger.Instance.weaponUnit.list, "prob");
            Debug.Log(weapon.Weapon_name);
            DataManeger.Instance.weaponUser.Add(weapon.Weapon_ID);
            panelSwordGacha.weapon_ID = weapon.Weapon_ID;
            panelSwordGacha10.SetWeapon(i, weapon);
            DataManeger.Instance.WeaponGacha();
        }

        DataManeger.Instance.weaponUser.Save();
    }

    public void ItemGacha(int item_count)
    {
        for (int i = 0; i < item_count; i++)
        {
            ItemUnitParam item = UtilRand.GetParam(ref DataManeger.Instance.itemUnit.list, "prob");
            Debug.Log(item.Item_name);
            DataManeger.Instance.itemUser.Add(item.Item_ID);
            panelItemGacha.item_ID = item.Item_ID;
            panelItemGacha10.SetItem(i, item);
            DataManeger.Instance.ItemGacha();
        }

        DataManeger.Instance.itemUser.Save();

    }
}
