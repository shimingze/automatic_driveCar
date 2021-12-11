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
    
    public class Serv : MonoBehaviour
    {
        //public static MyQueue messageQueue = new MyQueue(50);
        public Socket listenfd;
        public static Conn[] conns;
        public int maxConn = 3;

        public int NewIndex()
        {
            if (conns == null)
                return -1;
            for (int i = 0; i < conns.Length; i++)
            {
                
                if (conns[i] == null)
                {
                    Conn newconn = new Conn();
                    conns[i] = newconn;
                    return i;
                }
                else if (conns[i].isUse == false)
                {
                    return i;
                }
            }
            return -1;
        }

        public void start(string host, int port)
        {
            conns = new Conn[maxConn];
            for (int i = 0; i < maxConn; i++)
            {
                conns[i] = new Conn();
            }

            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdr = IPAddress.Parse(host);
            IPEndPoint ipEp = new IPEndPoint(ipAdr, port);
            listenfd.Bind(ipEp);
            listenfd.Listen(maxConn);
            listenfd.BeginAccept(AcceptCB,null);


        }
        private void AcceptCB(IAsyncResult ar)
        {
            try
            {
                Socket socket = listenfd.EndAccept(ar);
                int index = NewIndex();

                if (index < 0)
                {
                    socket.Close();
                }
                else
                {
                    //Conn conn = conns[index];
                    conns[index].isUse = true;
                    conns[index].Init(socket);
                    string adr = conns[index].GetAdress();
                    print("client connect to ["+adr+"] conn pool ID:"+index);
                    conns[index].Socket.BeginReceive
                    (conns[index].readBuff, conns[index].buffCount, 
                        conns[index].BuffRemain(), SocketFlags.None,
                        receiveCB, conns[index]);
                    
                }

                listenfd.BeginAccept(AcceptCB, null);
                
            }
            catch (Exception e)
            {
                print(e.Message+"~~~~AcceptCB失败~~~~");
                throw;
            }
        }

        private void receiveCB(IAsyncResult ar)
        {
            print("回调receiveCB");
            Conn conn = (Conn) ar.AsyncState;
            try
            {
                int count = conn.Socket.EndReceive(ar);
                if (count <= 0)
                {
                    print("because count of socket = "
                          +count.ToString()
                          +" \n received from ["
                          +conn.GetAdress()
                          +"] cut down the connection");
                    conn.Close();
                    return;
                }

                string str = System.Text.Encoding.UTF8.GetString(conn.readBuff,0,count);
                print("received data["+str+"]"+" from ["+conn.GetAdress()+"]");
                str = conn.GetAdress() + ":" + str;
                byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
                for (int i = 0; i < conns.Length; i++)
                {
                    // if (conns[i] == null)
                    // {
                    //     print(conns[i].isUse.ToString());
                    //     print("conn["+i.ToString()+"] is NULL!!");
                    //     continue;
                    // }

                    if(!conns[i].isUse)
                        continue;
                    print("transfer the message to "+conns[i].GetAdress());
                    conns[i].Socket.Send(bytes);
                }

                conn.Socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), 
                    SocketFlags.None, receiveCB, conn);
            }
            catch (Exception e)
            {
                print("收到：["+conn.GetAdress()+"]断开连接");
                conn.Close();
                throw;
            }
        }    
    }

}