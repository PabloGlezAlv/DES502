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
        CreateItemButtom(ObjectsManager.instance.getCoinSprite(0), "MONEDA", 50, 0);
        CreateItemButtom(ObjectsManager.instance.getScaleSprite(0), "Iman", 100, 1);
        CreateItemButtom(ObjectsManager.instance.getSpeedSprite(0), "Spped", 50, 2);
    }

    private void CreateItemButtom(Sprite sprite, string name, int cost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(template.transform, container.transform);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        float shopItemHeight = 450f;
        shopItemRectTransform.anchoredPosition = new Vector2(shopItemHeight * positionIndex - shopItemHeight, 0);
        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(name);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());
        //shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = sprite;
    }
}
