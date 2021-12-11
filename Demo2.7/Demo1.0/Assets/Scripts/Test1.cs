using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Test1 : MonoBehaviour
{
    static double Pi = System.Math.PI;

    public GameObject AI;
    public GameObject Target;
    public GameObject Move;
    //public GameObject Mo;
    public LayerMask mask;
    public List<string> locationlist = new List<string>();
    public static TcpSocket mTcpSocket = new TcpSocket("127.0.0.1", 4488);
    public static string tar;
    public static int collision;
    public static float distance;
    void Start()
    {
        //Test2.mess = "BACK";
    }

    public int IsCollision(GameObject obj1, GameObject obj2)
    {
        float dis = Vector3.Distance(obj1.transform.position, Move.transform.position);
        if (dis < 2)
        {
            //print("撞到了！");
            //print(obj1.transform.name);
            //print(obj2.transform.position);
            if(obj1.transform.name == "Car")
            {
                Move.transform.position = new Vector3(0, 0, 0);
            }    
            //Move.transform.Translate(new Vector3(h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0));
            else
            {
                Move.transform.position = new Vector3(Move.transform.position.x, Move.transform.position.y - 5, Move.transform.position.z);
            }
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public void Send(string sendMessage)
    {
        //print(sendMessage);
        
        mTcpSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
        
    }

    //坐标转换
    private Vector3 GetRelativePosition(Transform origin, Vector3 position)
    {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
        return relativePosition;
    }

    /*
    float GetRelativeRotation(GameObject obj)
    {
        float AI_directioon;
        float Barrier_direction;
        float RelativeRotation;
        AI_directioon = AI.transform.rotation.z;
        //print("AI_directioon:" + AI_directioon);
        Barrier_direction = obj.transform.rotation.z;
        //print("Barrier_direction:" + Barrier_direction);
        RelativeRotation = Barrier_direction - AI_directioon;
        //print("RelativeRotation:" + RelativeRotation);
        return RelativeRotation;
    }
    */

    //数据序列
    string GetPositionValue(GameObject obj, Transform origin)
    {
        // string tar;
        string oth;
        string RelativeRotation;
        // int collision;
        collision = IsCollision(Target, Move) * IsCollision(obj, Move);
        print("collision = "+collision.ToString());
        distance = Vector3.Distance(Target.transform.position,Move.transform.position);
        print("distance = "+distance.ToString());
        RelativeRotation = (((obj.transform.eulerAngles.z - Move.transform.eulerAngles.z)/360)*2*Pi).ToString("0.00");
        
        // tar = GetRelativePosition(Target.transform, obj.transform.position).x.ToString("0.00") + ","    //Target.transform.name+","+
        //       + GetRelativePosition(Target.transform, obj.transform.position).y.ToString("0.00") + "," 
        //       + GetRelativePosition(Target.transform, obj.transform.position).z.ToString("0.00") + "," + "1,1,2," 
        //       + RelativeRotation;// + GetRelativeRotation(obj);
        
        tar = GetRelativePosition(Move.transform, Target.transform.position).x.ToString("0.00") + ","    //Target.transform.name+","+
            + GetRelativePosition(Move.transform, Target.transform.position).y.ToString("0.00") + "," 
            + GetRelativePosition(Move.transform, Target.transform.position).z.ToString("0.00") + "," + "1,1,2," 
            + RelativeRotation;// + GetRelativeRotation(obj);
        
        // mTcpSocket.Send(Encoding.UTF8.GetBytes(tar));
        oth = GetRelativePosition(origin, obj.transform.position).x.ToString("0.00") + ","      //obj.transform.name+
              +GetRelativePosition(origin, obj.transform.position).y.ToString("0.00") + ","
              +GetRelativePosition(origin, obj.transform.position).z.ToString("0.00") + ","
              + "1,1,2," + RelativeRotation;// + GetRelativeRotation(obj);
        //print(oth);
        return oth;
    }
    
    //扇面射线
    void ScanDetection()
    {
        
        RaycastHit hitInfor;
        Vector3 vec_mid = AI.transform.up;
        Vector3 vec_right = AI.transform.up + AI.transform.right;
        Vector3 vec_left = AI.transform.up - AI.transform.right;
        Vector3 vec_right1 = (float)Math.Tan(49.5 * Pi / 180) *AI.transform.up + AI.transform.right;
        Vector3 vec_right2 = (float)Math.Tan(54 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right3 = (float)Math.Tan(58.5 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right4 = (float)Math.Tan(63 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right5 = (float)Math.Tan(67.5 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right6 = (float)Math.Tan(72 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right7 = (float)Math.Tan(76.5 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right8 = (float)Math.Tan(81 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_right9 = (float)Math.Tan(85.5 * Pi / 180) * AI.transform.up + AI.transform.right;
        Vector3 vec_left1 = -(float)Math.Tan(94.5 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left2 = -(float)Math.Tan(99 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left3 = -(float)Math.Tan(103.5 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left4 = -(float)Math.Tan(108 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left5 = -(float)Math.Tan(112.5 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left6 = -(float)Math.Tan(117 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left7 = -(float)Math.Tan(121.5 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left8 = -(float)Math.Tan(126 * Pi / 180) * AI.transform.up - AI.transform.right;
        Vector3 vec_left9 = -(float)Math.Tan(130.5 * Pi / 180) * AI.transform.up - AI.transform.right;
        Ray midRay = new Ray(transform.position, vec_mid);
        Ray rightRay = new Ray(transform.position, vec_right);
        Ray leftRay = new Ray(transform.position, vec_left);
        Ray rightRay1 = new Ray(transform.position, vec_right1);
        Ray rightRay2 = new Ray(transform.position, vec_right2);
        Ray rightRay3 = new Ray(transform.position, vec_right3);
        Ray rightRay4 = new Ray(transform.position, vec_right4);
        Ray rightRay5 = new Ray(transform.position, vec_right5);
        Ray rightRay6 = new Ray(transform.position, vec_right6);
        Ray rightRay7 = new Ray(transform.position, vec_right7);
        Ray rightRay8 = new Ray(transform.position, vec_right8);
        Ray rightRay9 = new Ray(transform.position, vec_right9);
        Ray leftRay1 = new Ray(transform.position, vec_left1);
        Ray leftRay2 = new Ray(transform.position, vec_left2);
        Ray leftRay3 = new Ray(transform.position, vec_left3);
        Ray leftRay4 = new Ray(transform.position, vec_left4);
        Ray leftRay5 = new Ray(transform.position, vec_left5);
        Ray leftRay6 = new Ray(transform.position, vec_left6);
        Ray leftRay7 = new Ray(transform.position, vec_left7);
        Ray leftRay8 = new Ray(transform.position, vec_left8);
        Ray leftRay9 = new Ray(transform.position, vec_left9);
        locationlist.Clear();
        if (Physics.Raycast(midRay, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(midRay.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(rightRay.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(leftRay.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay1, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay1.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay2, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay2.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay3, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay3.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay4, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay4.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay5, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay5.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay6, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay6.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay7, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(rightRay7.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay8, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay8.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(rightRay9, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(rightRay9.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay1, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(leftRay1.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay2, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(leftRay2.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay3, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(leftRay3.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay4, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(leftRay4.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay5, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(leftRay5.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay6, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(leftRay6.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay7, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(leftRay7.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay8, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            Debug.DrawLine(leftRay8.origin, hitInfor.point, Color.red);
        }
        if (Physics.Raycast(leftRay9, out hitInfor, 20, mask, QueryTriggerInteraction.Ignore))
        {
            // Send(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            locationlist.Add(GetPositionValue(hitInfor.collider.gameObject, Move.transform));
            //print(hitInfor.transform.name);
            Debug.DrawLine(leftRay9.origin, hitInfor.point, Color.red);
        }
        else
        {
            Debug.DrawLine(midRay.origin, midRay.origin + midRay.direction * 20, Color.blue);
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * 20, Color.blue);
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * 20, Color.blue);
            Debug.DrawLine(rightRay1.origin, rightRay1.origin + rightRay1.direction * 20, Color.blue);
            Debug.DrawLine(rightRay2.origin, rightRay2.origin + rightRay2.direction * 20, Color.blue);
            Debug.DrawLine(rightRay3.origin, rightRay3.origin + rightRay3.direction * 20, Color.blue);
            Debug.DrawLine(rightRay4.origin, rightRay4.origin + rightRay4.direction * 20, Color.blue);
            Debug.DrawLine(rightRay5.origin, rightRay5.origin + rightRay5.direction * 20, Color.blue);
            Debug.DrawLine(rightRay6.origin, rightRay6.origin + rightRay6.direction * 20, Color.blue);
            Debug.DrawLine(rightRay7.origin, rightRay7.origin + rightRay7.direction * 20, Color.blue);
            Debug.DrawLine(rightRay8.origin, rightRay8.origin + rightRay8.direction * 20, Color.blue);
            Debug.DrawLine(rightRay9.origin, rightRay9.origin + rightRay9.direction * 20, Color.blue);
            Debug.DrawLine(rightRay1.origin, leftRay1.origin + leftRay1.direction * 20, Color.blue);
            
            Debug.DrawLine(leftRay2.origin, leftRay2.origin + leftRay2.direction * 20, Color.blue);
            Debug.DrawLine(leftRay3.origin, leftRay3.origin + leftRay3.direction * 20, Color.blue);
            Debug.DrawLine(leftRay4.origin, leftRay4.origin + leftRay4.direction * 20, Color.blue);
            Debug.DrawLine(leftRay5.origin, leftRay5.origin + leftRay5.direction * 20, Color.blue);
            Debug.DrawLine(leftRay6.origin, leftRay6.origin + leftRay6.direction * 20, Color.blue);
            Debug.DrawLine(leftRay7.origin, leftRay7.origin + leftRay7.direction * 20, Color.blue);
            Debug.DrawLine(leftRay8.origin, leftRay8.origin + leftRay8.direction * 20, Color.blue);
            Debug.DrawLine(leftRay9.origin, leftRay9.origin + leftRay9.direction * 20, Color.blue);
        }

        for (int j = 0; j < locationlist.Count; j++)
        {
            for (int i = j+1; i < locationlist.Count; i++)
            {
                if (locationlist[j] == locationlist[i])
                {
                    locationlist.Remove(locationlist[i]);
                }
            }
        }
        string strtemp;
        // print("目标物体的坐标："+Test1.tar);
        strtemp = Test1.tar + ",";
        for (int i = 0; i < locationlist.Count; i++)
        {
            strtemp = strtemp+locationlist[i].ToString()+",";
            
        }
        // print("strtemp     =  "+strtemp+"~~~~");
        //print(" Count of locationlist                  ="+locationlist.Count);
        strtemp = strtemp + collision.ToString() + "," + distance.ToString("0.00")+",";
        // strtemp = strtemp.Substring(0, strtemp.Length - 1);
        
        //print("strtemp     =  "+strtemp+"                           ");
        Send(strtemp);
        print("strtemp的值："+strtemp);
        strtemp = null;
        // AI.transform.Rotate(new Vector3(0, 0, 90));  //绕z轴旋转
    }

    void Update()
    {
        ScanDetection();
    }
}


