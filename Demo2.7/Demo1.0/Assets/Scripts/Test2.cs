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
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("LEFT");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("RIGHT");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("GO");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("BACK");
        //Instruction.Enqueue("GO");

    }

    void Update()
    {
        //AIMove(0, tag);
        // AIMove(0,0);
        // if (Test1.IsCollision() == -1)
        // {
        //     Move.transform.position = new Vector3(Move.transform.position.x,Move.transform.position.y-25,Move.transform.position.z);
        // }
        /*
        string pos = "(0,0,0)", rot = "(0,0,0,1)";

        if (Test1.AI_Log.Count != 0)
        {
            //print(Test1.AI_Log.Dequeue());
            string[] arr = Test1.AI_Log.RemoveTail().Split('#');
            pos = arr[0];
            print(pos);
            rot = arr[1];
            print(rot);
        }
        //回退
        GM_Move.transform.position = VParse(pos);
        GM_Move.transform.rotation = QParse(rot); 
        */

        //print(GM_Move.transform.localEulerAngles);
        //Move_Instruction("GO");
        //Move_Instruction("BACK");
        //Move_Instruction("LEFT");
        //Move_Instruction("RIGHT");
        //AIMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //print("111" + Move.transform.position);
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    //tag = 1;
        //    //Move_Instruction("GO");
        //    mess = "GO";

        //}
        //else if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    //tag = -1;
        //    //Move_Instruction("BACK");
        //    mess = "BACK";
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    //Move_Instruction("LEFT");
        //    mess = "LEFT";
        //}

        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    //Move_Instruction("RIGHT");
        //    mess = "RIGHT";
        //}


        
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
        


        //mess = MMSocket.result.ToString();
        //Move_Instruction("STOP");
        //switch (mess)
        //switch(mess)
        //{
        //    case "GO":
        //        tag = 1;
        //        Move_Instruction("GO");
        //        break;
        //    case "BACK":
        //        tag = -1;
        //        Move_Instruction("BACK");
        //        break;
        //    case "LEFT":
        //        Move_Instruction("LEFT");
        //        break;
        //    case "RIGHT":
        //        Move_Instruction("RIGHT");
        //        break;
        //    case "STOP":
        //        Move_Instruction("STOP");
        //        break;
        //    default:
        //        break;
        //}

    }
}
