using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    private Vector3[] AimPositions = { new Vector3(-1122, 0, 0), new Vector3(1122, 0, 0) };
    int PosIndex = 0;
    float TimeValue = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("hatch", true);

            TimeValue = 0;
            ChangeFloorMovement changeFloor = collision.gameObject.GetComponent<ChangeFloorMovement>();

            playerMovement.SetNormalMove(false);
            changeFloor.enabled = true;
            Vector3 position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            changeFloor.setTargetPostion(position);

            if (true)
            {
                //changeFloor.setTargetPostion(Vector3.Lerp(transform.position, AimPositions[PosIndex], TimeValue));
            }
        }
    }

    void FixedUpdate()
    {
        TimeValue += Time.deltaTime;
    }

    IEnumerator MoveBG()
    {
        PosIndex = 0;
        TimeValue = 0;
        yield return new WaitForSeconds(1);
    }
}