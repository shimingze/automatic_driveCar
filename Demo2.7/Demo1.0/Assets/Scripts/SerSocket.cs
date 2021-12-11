using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.IO;
using System.Runtime.InteropServices;
using DefaultNamespace;


public class MMSocket : MonoBehaviour
{
    char[] buff = new char[256];
    public  static byte[] result = new byte[1024];
    private static int myProt = 0;   //端口
    static Socket serverSocket;
    public DefaultNamespace.Conn[] Conns;
    static string ipadd = "0";
    public static string sendMessage = "null";
    public static int i = 0;
    public static void BindSock()
    {
        // //服务器IP地址读取
        ipadd = "127.0.0.1";
        Serv serv = new Serv();
        serv.start("127.0.0.1",1234);
    }
    //  static double[] location = new double[2] {1.1,2.2 };
    public static void Send(string message)
    {
        sendMessage = message;
    }


}
