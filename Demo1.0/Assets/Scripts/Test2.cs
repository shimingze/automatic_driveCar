using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{

    public GameObject Move;
    public float speed = 0;
    public GameObject GM_Move;

    string key;
    int derection = 0;
    public static string mess = "STOP";
    int tag = 0;

    void AIMove(float h, float v)
    {
        Move.transform.Translate(new Vector3(h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0));
    }

    void Move_Instruction(string Ins)
    {
        switch(Ins)
        {
            case "GO":
                //print("GO");
                AIMove(0, tag);
                break;
            case "BACK":
                //print("BACK");
                AIMove(0, tag);
                break;
            case "GOLEFT":
                //print("LEFT");
                derection = derection + 15;
                GM_Move.transform.localEulerAngles = new Vector3(0, 0, derection);
                AIMove(0, tag);
                break;
            case "GORIGHT":
                //print("RIGHT");
                derection = derection - 15;
                GM_Move.transform.localEulerAngles = new Vector3(0, 0, derection);
                AIMove(0, tag);
                break;
            case "BACKLEFT":
                derection = derection + 15;
                GM_Move.transform.localEulerAngles = new Vector3(0, 0, derection);
                AIMove(0, -tag);
                break;
            case "BACKRIGHT":
                derection = derection - 15;
                GM_Move.transform.localEulerAngles = new Vector3(0, 0, derection);
                AIMove(0, -tag);
                break;
            case "STOP":
                AIMove(0, 0);
                break;
            default:
                break;
        }
    }

    public static Vector3 VParse(string str)
    {
        str = str.Replace("(", " ").Replace(")", " "); //将字符串中"("和")"替换为" "
        string[] s = str.Split(',');
        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }

    public static Quaternion QParse(string str)
    {
        str = str.Replace("(", " ").Replace(")", " "); //将字符串中"("和")"替换为" "
        string[] s = str.Split(',');
        return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }


    void Start()
    {
    }

    void Update()
    {
        if (mess == "GO")
        {
            tag = 1;
            Move_Instruction("GO");

        }
        else if (mess == "BACK")
        {
            tag = -1;
            Move_Instruction("BACK");
        }
        else if (mess == "GOLEFT")
            Move_Instruction("GOLEFT");
        else if (mess == "GORIGHT")
            Move_Instruction("GORIGHT");
        else if (mess == "BACKLEFT")
            Move_Instruction("BACKLEFT");
        else if (mess == "BACKRIGHT")
            Move_Instruction("BACKRIGHT");

        mess = "STOP";
    }
}
