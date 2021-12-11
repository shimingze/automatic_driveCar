using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.IO;
using System.Runtime.InteropServices;


public class MSocket : MonoBehaviour
{
    char[] buff = new char[256];
    public  static byte[] result = new byte[1024];
    private static int myProt = 0;   //端口
    static Socket serverSocket;
    static string ipadd = "0";
    public static string sendMessage = "start";
    public static int i = 0;
    public static void BindSock()
    {
        //服务器IP地址读取
        ipadd = "127.0.0.1";
        myProt = Convert.ToInt32("4488");

        IPAddress ip = IPAddress.Parse(ipadd);
        IPEndPoint remoteEP = new IPEndPoint(ip, myProt);
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        serverSocket.Bind(new IPEndPoint(ip, myProt));

    }
    //  static double[] location = new double[2] {1.1,2.2 };
    public static void Send(string args)
    {
        sendMessage = args;

        //serverSocket.Connect(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
        //serverSocket.Connect(remoteEP);
        if (serverSocket.Connected)
        {
            print("Connected");
        }
        
        serverSocket.Listen(10);    //设定最多10个排队连接请求
        //print("启动监听{0}成功" + serverSocket.LocalEndPoint.ToString());   //通过Clientsoket发送数据
        Thread myThread = new Thread(ListenClientConnect);
        myThread.Start();
        //print(i++);
        //logout("readline");
        Console.ReadLine();
        //logout("主函数结束");
    }
    /// <summary>          
    /// 监听客户端连接          
    /// /// </summary>        
    private static void ListenClientConnect()
    {
        while (true)
        {
            //print("3");
            Socket clientSocket = serverSocket.Accept();
            //print("4");
            //clientSocket.Send(System.Text.Encoding.ASCII.GetBytes("链接成功"));
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start(clientSocket);

        }
    }
    /// <summary>          
    /// /// 接收消息          
    /// /// </summary>          
    /// /// <param name="clientSocket"></param>        
    private static void ReceiveMessage(object clientSocket)
    {

        Socket myClientSocket = (Socket)clientSocket;
        while (true)
        {
            try
            {
                //通过clientSocket接收数据
                int receiveNumber = myClientSocket.Receive(result);
                print("接收客户端{0}消息{1}"+ myClientSocket.RemoteEndPoint.ToString() + System.Text.Encoding.ASCII.GetString(result, 0, receiveNumber));
                if (System.Text.Encoding.ASCII.GetString(result, 0, receiveNumber) == "A")
                {
                    print("对方请求位置信息");
                    // myClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(result));
                    myClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(sendMessage));

                }
                //logout("接收客户端{0}消息{1}"+ myClientSocket.RemoteEndPoint.ToString()+ System.Text.Encoding.ASCII.GetString(result, 0, receiveNumber));
            }
            catch (Exception ex)
            {
                print(ex.Message);
                myClientSocket.Shutdown(SocketShutdown.Both);
                myClientSocket.Close();
                break;
            }
        }
    }
}
