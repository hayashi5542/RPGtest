using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class PanelItemGacha : MonoBehaviour
{
    public Image imgItemIcon;
    public TextMeshProUGUI textItemName;
    public int item_ID;
    private void OnEnable()
    {
        if (DataManeger.Instance == null)
        {
            return;
        }
        ItemUnitParam item = DataManeger.Instance.itemUnit.list.Find(p => p.Item_ID == item_ID);
        if (item == null)
        {
            return;
        }
        textItemName.text = item.Item_name;
        imgItemIcon.sprite = SpriteManager.Instance.Get(item.Sprite_Name);

    }
}
