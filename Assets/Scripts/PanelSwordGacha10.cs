using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using anogamelib;

public class PanelSwordGacha10 : MonoBehaviour
{
    public List<Image> image_list;
    public void SetWeapon(int Index,WeaponUnitParam weapon)
    {
        image_list[Index].sprite = SpriteManager.Instance.Get(weapon.Sprite_Name);
    }
}
