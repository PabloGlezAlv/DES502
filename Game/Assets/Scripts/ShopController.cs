using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    GameObject container;
    [SerializeField]
    GameObject template;
    void Awake()
    {
        template.SetActive(false);
    }

    private void Start()
    {
        CreateItemButtom(ObjectsManager.instance.getCoinSprite(0), "MONEDA", ObjectsManager.instance.getSpeedPrice(0), 0);
        CreateItemButtom(ObjectsManager.instance.getScaleSprite(0), "Iman", ObjectsManager.instance.getSpeedPrice(2), 1);
        CreateItemButtom(ObjectsManager.instance.getSpeedSprite(0), "Spped", ObjectsManager.instance.getSpeedPrice(3), 2);
    }

    private void CreateItemButtom(Sprite sprite, string name, int cost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(template.transform, container.transform);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        float shopItemHeight = 450f;
        shopItemRectTransform.anchoredPosition = new Vector2(shopItemHeight * positionIndex - shopItemHeight, 0);
        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(name);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());

        shopItemTransform.Find("itemImage");
    }
}
