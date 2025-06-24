using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{

    public ItemManager itemOnButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if(MenuManager.instance.menu.activeInHierarchy)
        {
            MenuManager.instance.itemName.text = itemOnButton.itemName;
            MenuManager.instance.itemDescription.text = itemOnButton.itemDescription;

            MenuManager.instance.activeItem = itemOnButton;
        }

        if(ShopManager.instance.shopMenu.activeInHierarchy)
        {
            if(ShopManager.instance.buyPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedBuyItem(itemOnButton);
            }
            else if(ShopManager.instance.sellPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedSellItem(itemOnButton);
            }
        }
        
    }
}
