using UnityEngine;
using UnityEngine.UI;

public class BackGoundDeadChooser : MonoBehaviour
{
    [SerializeField]
    Sprite castleDead;
    [SerializeField]
    Sprite supermarketDead;

    // Start is called before the first frame update
    void Start()
    {
        Image img = GetComponent<Image>();

        if (UserInformation.lastScenario == TilemapType.Castle)
        {
            img.sprite = castleDead;
        }
        else
        {
            img.sprite = supermarketDead;
        }
    }
}
