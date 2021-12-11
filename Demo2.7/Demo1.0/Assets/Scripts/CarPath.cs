﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPath : MonoBehaviour
{
    public GameObject[] gos; //获取每个目标点，，注意数组顺序不能乱
    public float speed = 1;  //用于控制移动速度
    int i = 0;             //用于记录是第几个目标点
    float des;             //用于存储与目标点的距离     
                           // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        //看向目标点
        this.transform.LookAt(gos[i].transform);
        //计算与目标点间的距离
        des = Vector3.Distance(this.transform.position, gos[i].transform.position);
        //移向目标
        transform.position = Vector3.MoveTowards(this.transform.position, gos[i].transform.position, Time.deltaTime * speed);
        //如果移动到当前目标点，就移动向下个目标
        if (des < 0.5f && i < gos.Length - 1)
        {
            i++;
        }

        if(i == 7)
        {
            i = 0;
        }
    }
}
