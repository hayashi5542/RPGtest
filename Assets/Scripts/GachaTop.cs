using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaTop : MonoBehaviour
{
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
        }

        DataManeger.Instance.weaponUser.Save();
    }
}
