using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyQueue : MonoBehaviour
{
    private static string[] array;
    private static string[] Barrier_ID;
    private static int front = 0;
    private static int rear = 0;
    public MyQueue(int capacity)
    {
        array = new string[capacity];
        Barrier_ID = new string[capacity];
    }
    /// <summary>
    /// 入队
    /// </summary>
    public  void EnQueue(string id,string value)
    {
        if ((rear + 1) % array.Length == front)
        {
            //print("队列已满！");
            return;
        }
        Barrier_ID[rear] = id;
        array[rear] = value;
        rear = (rear + 1) % array.Length;
    }
    /// <summary>
    /// 出队
    /// </summary>
    public  string DeQueue()
    {
        if (rear == front)
        {
            //print("队列已空！");
            return null;
        }
        string value = array[front];
        string id = Barrier_ID[front];
        front = (front + 1) % array.Length;
        return id + value;
    }

    public string GetRear()
    {
        if (rear == front)
        {
            //print("队列已空！");
            return null;
        }
        string id = Barrier_ID[rear];
        return id;
    }

    public bool IsEmpty()
    {
        if (rear == front)
        {
            //print("队列已空！");
            return true;
        }
        else
            return false;
    }

    public int QueueCount()
    {
        return (rear - front + 1);
    }
}