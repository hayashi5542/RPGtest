using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelItem : MonoBehaviour
{
    public List<TextMeshProUGUI> NumTextList;
    public TextMeshProUGUI potionTextNum;

    private void OnEnable()
    {
        ShowItemNum();
    }

    private void ShowItemNum()
    {
        for(int i=0; i<NumTextList.Count; i++)
        {
            int itemID = i + 2;
            int ItemNum = 0;
            
            ItemUserParam item = DataManeger.Instance.itemUser.list.Find(p => p.Item_ID == itemID);
            if(item != null)
            {
                ItemNum = item.num;
            }
            NumTextList[i].text = ItemNum.ToString();
        }

        int potionNum = 0;
        ItemUserParam potion = DataManeger.Instance.itemUser.list.Find(p => p.Item_ID == 1);
        if(potion != null)
        {
             potionNum = potion.num;
        }
        potionTextNum.text = potionNum.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
