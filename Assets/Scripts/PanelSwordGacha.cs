using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class PanelSwordGacha : MonoBehaviour
{
    public Image imgSwordIcon;
    public TextMeshProUGUI textSwordName;
    public int weapon_ID;
    private void OnEnable()
    {
        if(DataManeger.Instance == null)
        {
            return;
        }
        WeaponUnitParam weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == weapon_ID);
        if(weapon == null)
        {
            return;
        }
        textSwordName.text = weapon.Weapon_name;
        imgSwordIcon.sprite = SpriteManager.Instance.Get(weapon.Sprite_Name);
        
    }
}
