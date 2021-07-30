using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using anogamelib;

public class PanelItemGacha10 : MonoBehaviour
{
    public List<Image> image_list;
    public void SetItem(int Index, ItemUnitParam item)
    {
        image_list[Index].sprite = SpriteManager.Instance.Get(item.Sprite_Name);
    }
}
