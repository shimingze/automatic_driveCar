using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class TcpSocket : MonoBehaviour
{
    private Socket mSocket;
    private byte[] mData;
    private int mOffect;
    public static string receiveMessage = "GO";
    /// <summary>
    ///	一次性接收数据的最大字节
    /// </summary>
    private int mSize = 2048 * 2048 * 1;


    public TcpSocket(string varIp, int varPort)
    {
        IPEndPoint point = new IPEndPoint(IPAddress.Parse(varIp), varPort);
        mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        mSocket.BeginConnect(point, Connect, null);
    }
    ~TcpSocket()
    {
        CloseConnect();
    }
    /// <summary>
    ///	连接服务器结果
    /// </summary>
    private void Connect(IAsyncResult result)
    {
        if (result.IsCompleted)
        {

            //连接成功
            mData = new byte[mSize];
            //开启数据接收的异步监听
            mSocket.BeginReceive(mData, mOffect, mSize - mOffect, SocketFlags.None, Receive, null);
        }
        else
        {
            //连接失败
            print("连接失败！");
        }
    }
    /// <summary>
    ///	接收到服务器数据
    /// </summary>
    private void Receive(IAsyncResult result)
    {
        //结束当前监听进行数据处理
        int size = mSocket.EndReceive(result, out SocketError error);
        if (error == 0)
        {
            //接收数据成功
            if (size != 0)
            {
                //当数据大小不为0时进行数据内容处理
                //此处理方式为测试数据
                byte[] bytes = new byte[size + mOffect];
                Array.Copy(mData, 0, bytes, 0, size + mOffect);
                //receiveMessage = Encoding.UTF8.GetString(bytes);

                Test2.mess = Encoding.UTF8.GetString(bytes);//= receiveMessage;
                // receiveMessage = null;


                //来自服务器的信息

            }
            //开启数据接收的异步监听
            mSocket.BeginReceive(mData, mOffect, mSize - mOffect, SocketFlags.None, Receive, null);
        }
        else
        {
            //接收数据发生错误-自动断开连接
            CloseConnect();
            //return null;
        }
        //return receiveMessage;
    }
    /// <summary>
    ///	发生数据给服务器
    /// </summary>
    public void Send(byte[] bytes)
    {
        try
        {
            mSocket.Send(bytes);
        }
        catch (Exception exp)
        {
            Debug.LogError(exp.Message);
            //发生数据异常-自动断开连接
            CloseConnect();
        }
    }
    /// <summary>
    ///	关闭服务器连接
    /// </summary>
    public void CloseConnect()
    {
        if (mSocket.Connected)
        {
            mSocket.Close();
        }
    }
}

