using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System.Drawing;
using UnityEngine.UIElements;

public class ShopController : MonoBehaviour
{

    private Transform container;
    private Transform shopItemTemplate;
    private PlayerData playerData;

    Transform[] shopItems = new Transform[3];

    private bool[] bought = new bool[3];

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();


        for(int i = 0; i < 3; i++)
        {
            Transform shopItemTransform = Instantiate(shopItemTemplate, container);
            shopItems[i] = shopItemTransform;
        }


        ResetItems();
    }

    private void Start()
    {
        GenerateRandomItems();
        Hide();
    }

    private void GenerateRandomItems()
    {
        for(int i = 0;i < 3;i++)
        {
            int item = Random.Range(1, (int)Items.SpeedHelmet + 1);
            int rarity = Random.Range(0, (int)Rarity.Legendary + 1);

            switch((Items)item)
            {
                case Items.ScaleHelmet:
                    CreateItemButton((Items)item, (Rarity)rarity, ObjectsManager.instance.getScaleSprite(rarity), "Scale " + rarity.ToString(), 
                        ObjectsManager.instance.getScalePrice(rarity), i);
                    break;
                case Items.SpeedHelmet:
                    CreateItemButton((Items)item, (Rarity)rarity, ObjectsManager.instance.getSpeedSprite(rarity), "Speed " + rarity.ToString(),
                       ObjectsManager.instance.getSpeedPrice(rarity), i);
                    break;
            }
        }
    }

    public void ResetItems()
    {
        for (int i = 0; i < bought.Length; i++)
        {
            bought[i] = false;

            shopItems[i].Find("blocked").gameObject.SetActive(false);
        }
    }

    public void CreateNewShop()
    {
        GenerateRandomItems();
        ResetItems();
        Hide();
    }

    private void CreateItemButton(Items itemType, Rarity rare, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        shopItems[positionIndex].gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItems[positionIndex].GetComponent<RectTransform>();

        float shopItemHeight = 450f;
        shopItemRectTransform.anchoredPosition = new Vector2(shopItemHeight * positionIndex - shopItemHeight, 0);

        shopItems[positionIndex].Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItems[positionIndex].Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItems[positionIndex].Find("itemImage").GetComponent<UnityEngine.UI.Image>().sprite = itemSprite;

        shopItems[positionIndex].GetComponent<Button_UI>().ClickFunc = () => {
            // Clicked on shop item button
            if(TryBuyItem(itemType, rare, itemCost, positionIndex))
            {
                //Deactivate option to buy this
                bought[positionIndex] = true;
                shopItems[positionIndex].Find("blocked").gameObject.SetActive(true);
            }
        };
    }

    private bool TryBuyItem(Items itemType, Rarity rare, int price, int position)
    {
        if (!bought[position])
        {
            if(playerData.BuyItem(itemType, rare, price))
            {
                return true;
            }
        }
        return false;
    }


    public void Hide()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].gameObject.SetActive(true);
        }
    }
}
