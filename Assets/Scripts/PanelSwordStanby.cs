using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class PanelSwordStanby : MonoBehaviour
{
    public TextMeshProUGUI weapon_name;
    public Image weapon_icon;
    public Button left;
    public Button right;
    public int current_weaponID;
    public Image weapon_icon_Left;
    public Image weapon_icon_Right;

    private void OnEnable()
    {
        current_weaponID = DataManeger.Instance.gameInfo.GetInt(Define.keyEquipWeaponID);
        ShowWeapon(current_weaponID);
        /*WeaponUnitParam weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == current_weaponID);
        weapon_name.text = weapon.Weapon_name;
        weapon_icon.sprite = SpriteManager.Instance.Get(weapon.Sprite_Name);

        int left_wepon_ID = current_weaponID - 1;
        WeaponUnitParam left_weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == left_wepon_ID);
        weapon_icon_Left.sprite = SpriteManager.Instance.Get(left_weapon.Sprite_Name);

        int right_wepon_ID = current_weaponID + 1;
        WeaponUnitParam right_weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == right_wepon_ID);
        weapon_icon_Right.sprite = SpriteManager.Instance.Get(right_weapon.Sprite_Name);*/
    }
    private void ShowWeapon(int _weaponID)
    {
        WeaponUnitParam weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == _weaponID);
        weapon_name.text = weapon.Weapon_name;
        weapon_icon.sprite = SpriteManager.Instance.Get(weapon.Sprite_Name);

        int left_wepon_ID = _weaponID - 1;
        WeaponUnitParam left_weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == left_wepon_ID);
        if(left_weapon != null)
        {
            weapon_icon_Left.enabled = true;
            weapon_icon_Left.sprite = SpriteManager.Instance.Get(left_weapon.Sprite_Name);
        }
        else
        {
            weapon_icon_Left.enabled = false;
        }


        int right_wepon_ID = _weaponID + 1;
        WeaponUnitParam right_weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == right_wepon_ID);
        if (right_weapon != null)
        {
            weapon_icon_Right.enabled = true;
            weapon_icon_Right.sprite = SpriteManager.Instance.Get(right_weapon.Sprite_Name);
        }
        else
        {
            weapon_icon_Right.enabled = false;
        }
    }

    public void LeftButton()
    {
        if(current_weaponID > 1)
        {
            current_weaponID -= 1;
            ShowWeapon(current_weaponID);
        }

    }

    public void RightButton()
    {
        if(current_weaponID < 10)
        {
            current_weaponID += 1;
            ShowWeapon(current_weaponID);
        }

    }


}
