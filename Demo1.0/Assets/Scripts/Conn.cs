using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.IO;
using System.Runtime.InteropServices;
namespace DefaultNamespace
{
    public class Conn : MonoBehaviour
    {
        public const int BUFFER_SIZE = 2048;
        public Socket Socket;
        public bool isUse = false;
        public byte[] readBuff = new byte[BUFFER_SIZE];
        public int buffCount = 0;
        
        public Conn()
        {
            readBuff = new byte[BUFFER_SIZE];
        }//初始化

        public void Init(Socket socket)
        {
            this.Socket = socket;
            isUse = true;
            buffCount = 0;
        }

        public int BuffRemain()
        {
            return BUFFER_SIZE - buffCount;
        }

        public string GetAdress()
        {
            if (!isUse)
                return "无法获取地址";
            return Socket.RemoteEndPoint.ToString();
        }

        public void Close()
        {
            if (!isUse)
                return;
            print("[lose connection!!]"+GetAdress());
            Socket.Close();
            isUse = false;
        }
    }
}