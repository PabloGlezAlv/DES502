using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private RectTransform tf;
    [SerializeField] private bool StopWhenDestReached;
    [SerializeField] private float Bounds;
    [SerializeField] private float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.position += new Vector3(0f, Speed, 0f) * Time.deltaTime;
        if (tf.position.y > Bounds)
        {
            if (StopWhenDestReached)
            {
                Speed = 0;
            }
            else
            {
                tf.position -= new Vector3(0f, Bounds, 0f);
            }
        }
    }
}
