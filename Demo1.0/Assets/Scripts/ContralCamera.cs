using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContralCamera : MonoBehaviour
{
    public Transform playerTransform; // 移动的物体
    public Vector3 deviation; // 偏移量

    // Start is called before the first frame update
    void Start()
    {
        deviation = transform.position - playerTransform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + deviation;
    }
}
