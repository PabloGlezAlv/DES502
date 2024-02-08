using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItems : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponentInParent<BaseObject>() != null)
        {
            Debug.Log("nEW OBJECT");

            collision.transform.parent.gameObject.SetActive(false);
        }
    }
}
