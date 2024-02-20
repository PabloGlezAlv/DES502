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

        for (int i = 0; i < bought.Length; i++)
        {
            bought[i] = false;
        }
    }

    private void Start()
    {
        CreateItemButton(Items.SpeedHelmet, Rarity.Common, ObjectsManager.instance.getSpeedSprite(0), "SpeedCom", ObjectsManager.instance.getSpeedPrice(0), 0);
        CreateItemButton(Items.SpeedHelmet, Rarity.Legendary, ObjectsManager.instance.getSpeedSprite(4), "Speedleg", ObjectsManager.instance.getSpeedPrice(0), 1);
        CreateItemButton(Items.ScaleHelmet, Rarity.Legendary, ObjectsManager.instance.getScaleSprite(4), "Scale", ObjectsManager.instance.getSpeedPrice(0), 2);

        Hide();
    }

    private void CreateItemButton(Items itemType, Rarity rare, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItems[positionIndex] = shopItemTransform;
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 450f;
        shopItemRectTransform.anchoredPosition = new Vector2(shopItemHeight * positionIndex - shopItemHeight, 0);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<UnityEngine.UI.Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () => {
            // Clicked on shop item button
            if(TryBuyItem(itemType, rare, itemCost, positionIndex))
            {
                //Deactivate option to buy this
                bought[positionIndex] = true;
                shopItemTransform.Find("blocked").gameObject.SetActive(true);
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
