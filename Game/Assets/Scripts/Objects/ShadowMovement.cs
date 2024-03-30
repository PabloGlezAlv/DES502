using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [SerializeField]
    private float frequency = 1f;

    [SerializeField]
    private float minorScale = -0.8f;
    [SerializeField]
    private float maxScale = 0.8f;

    private bool onFloor = true;  // Levitate or not

    private float scaleX = 0;
    private float scaleY = 0;

    void Start()
    {
        scaleX = transform.localScale.x; 
        scaleY = transform.localScale.y;
    }

    void Update()
    {
        if (onFloor)
        {
            float mappedValue = Mathf.Clamp01(((Mathf.Sin(Time.time * frequency) + 1f) / 2f) * (maxScale - minorScale) + minorScale);

            float realScaleX = scaleX - scaleX * mappedValue;
            float realScaleY = scaleY - scaleY * mappedValue;

            // Aplica la nueva escala al objeto
            transform.localScale = new Vector3(realScaleX, realScaleY, 1f);
        }
    }
}