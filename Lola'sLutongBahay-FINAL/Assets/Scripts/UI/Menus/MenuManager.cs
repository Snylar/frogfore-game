using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    public GameObject menu;
    [SerializeField] GameObject rpgui;

    public static MenuManager instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, lvlText, xpText, currentXPText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;

    public TextMeshProUGUI itemName, itemDescription;

    public ItemManager activeItem;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
                rpgui.SetActive(true);
                GameManager.instance.gameMenuOpened = false;
            }
            else
            {
                UpdateStats();
                UpdateItemsInventory();
                menu.SetActive(true);
                rpgui.SetActive(false);
                GameManager.instance.gameMenuOpened = true;
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        characterPanel[0].SetActive(true);

        nameText[0].text = playerStats[0].playerName;
        lvlText[0].text = "Level: " + playerStats[0].playerLevel;
        currentXPText[0].text = ": " + playerStats[0].currentXp;

        xpText[0].text = playerStats[0].currentXp.ToString() + playerStats[0].xpForNextLevel[playerStats[0].playerLevel];
        xpSlider[0].maxValue = playerStats[0].xpForNextLevel[playerStats[0].playerLevel];
        xpSlider[0].value = playerStats[0].currentXp;
    }

    public void UpdateItemsInventory()
    {
        // Clear the item slots container
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        // Create new item slots and assign sprites
        foreach (ItemManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemsImage = itemSlot.Find("Items Image").GetComponent<Image>();
            itemsImage.sprite = item.itemsImage;

            TextMeshProUGUI itemsAmountText = itemSlot.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
                itemsAmountText.text = item.amount.ToString();
            else
                itemsAmountText.text = "";

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }
    public void DiscardItem()
    {
        print(activeItem.itemName);
        Inventory.instance.RemoveItem(activeItem);
    }

    // Update is called once per frame
    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");

    }
}
