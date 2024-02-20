using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class ShopController : MonoBehaviour
{

    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(Items.SpeedHelmet, ObjectsManager.instance.getCoinSprite(0), "MONEDA", ObjectsManager.instance.getSpeedPrice(0), 0);
        CreateItemButton(Items.SpeedHelmet, ObjectsManager.instance.getScaleSprite(0), "Iman", ObjectsManager.instance.getSpeedPrice(2), 1);
        CreateItemButton(Items.SpeedHelmet, ObjectsManager.instance.getSpeedSprite(0), "Spped", ObjectsManager.instance.getSpeedPrice(3), 2);
    }

    private void CreateItemButton(Items itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 450f;
        shopItemRectTransform.anchoredPosition = new Vector2(shopItemHeight * positionIndex - shopItemHeight, 0);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () => {
            // Clicked on shop item button
            TryBuyItem(itemType);
        };
    }

    private void TryBuyItem(Items itemType)
    {

    }

}
