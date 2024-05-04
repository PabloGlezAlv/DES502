using UnityEngine;
using UnityEngine.UI;

public class BackGoundDeadChooser : MonoBehaviour
{
    public static int ChosenImage;
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
            ChosenImage = 0;
        }
        else
        {
            img.sprite = supermarketDead;
            ChosenImage = 1;
        }
    }
}
