using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class PanelSwordStanby : MonoBehaviour
{
    public TextMeshProUGUI weapon_name;
    public TextMeshProUGUI weapon_AttackNum;
    public TextMeshProUGUI weapon_PlasAttackNum;
    private UnitController unitController;
    public DataManeger dataManeger;
    public Image weapon_icon;
    public Button left;
    public Button right;
    public Button setWeapon;
    public Button PowerUp;
    public int current_weaponID;
    public Image weapon_icon_Left;
    public Image weapon_icon_Right;
    public Image weapon_craftItem;
    public int craftItemID;
    public int weaponAttack;
    public int weaponAttackPlus;

    private void Start()
    {
        System.Type type = typeof(UnitController);
        unitController = (GameObject.FindObjectOfType(type) as UnitController);
    }

    private void OnEnable()
    {
        current_weaponID = DataManeger.Instance.gameInfo.GetInt(Define.keyEquipWeaponID);
        craftItemID = DataManeger.Instance.gameInfo.GetInt(Define.keyCraftItemID);
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
        WeaponUserParam weaponUser = DataManeger.Instance.weaponUser.list.Find(p => p.Weapon_ID == _weaponID);
        ItemUnitParam item = DataManeger.Instance.itemUnit.list.Find(p => p.Item_ID == weapon.CraftItem_ID);
        ItemUserParam itemUser = DataManeger.Instance.itemUser.list.Find(p => p.Item_ID == weapon.CraftItem_ID);
        int itemNum;
        if (itemUser != null)
        {
            itemNum = itemUser.num;
            //Debug.Log(itemUser.num);
        }
        else
        {
            itemNum = 0;
            //Debug.Log(0);
        }
        Debug.Log(itemNum);

        weapon_name.text = weapon.Weapon_name;
         
        weapon_icon.sprite = SpriteManager.Instance.Get(weapon.Sprite_Name);
        weapon_craftItem.sprite = SpriteManager.Instance.Get(item.Sprite_Name);
        
        weaponAttack = weapon.Attack;
        weapon_AttackNum.text = weaponAttack.ToString();

        weaponAttackPlus = weaponUser.craft_count;
        weapon_PlasAttackNum.text = weaponAttackPlus.ToString();

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

    public void SetButton()
    {
        //Debug.Log(current_weaponID);
        //current_weaponID = DataManeger.Instance.gameInfo.GetInt(Define.keyEquipWeaponID);
        unitController.SetAttack(current_weaponID);
        DataManeger.Instance.gameInfo.SetInt(Define.keyEquipWeaponID, current_weaponID);
        DataManeger.Instance.gameInfo.Save();
    }

    public void CancelButton()
    {
      
    }

    public void PowerUpButton()
    {
        dataManeger.PowerUP(current_weaponID);
        ShowWeapon(current_weaponID);
        unitController.SetAttack(current_weaponID);
        DataManeger.Instance.weaponUser.Save();
    }

    public void WeaponAttack()
    {
        /*current_weaponID = DataManeger.Instance.gameInfo.GetInt(Define.keyEquipWeaponID);
        WeaponUnitParam weapon = DataManeger.Instance.weaponUnit.list.Find(p => p.Weapon_ID == current_weaponID);
        weaponAttack = weapon.Attack;
        return weaponAttack;*/
    }
}
