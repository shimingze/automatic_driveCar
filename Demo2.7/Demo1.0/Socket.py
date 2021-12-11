from socket import *
import zerorpc


class RPCTransfer(object):
    def get(self, listForGet):
        listForGet = list1
        return listForGet
    def send(self,message):
        # global recievemess
        recievemess = message
        return message # message is an action
# s = zerorpc.Server(HelloRPC())
# s.bind("tcp://192.168.1.104:8848")
# s.run()

import time
import binascii
HOST = '127.0.0.1'
PORT = 1234
BUFSIZ = 1024
ADDR = (HOST,PORT)
tcpCliSock = socket(AF_INET,SOCK_STREAM)
tcpCliSock.connect(ADDR)



class Controller:
    def __init__(self,bbox,cls):

        self.bbox = bbox
        self.cls  = cls

    def getlocation(self,data):
         # socket连接
        print("位置请求已发送\n")
        tcpCliSock.send(format(data).encode())      #发送data

        recvdata = tcpCliSock.recv(BUFSIZ)          #获取unity端回馈消息
        print("收到消息x,y,z:"+recvdata.decode('utf-8'))
        revstr = recvdata.decode('utf-8')
        loc = revstr.split(",") # loc means location 返回坐标，以逗号为分割符的格式。
        #加坐标的操作
        print(loc)
        return loc
    def getinfor(self,data):
        print("请求类别信息 和 bbox.")
        tcpCliSock.send(format(data).encode())
        recvdata = tcpCliSock.recv(BUFSIZ)
        print("收到消息x,y,z:"+recvdata.decode('utf-8'))
        revstr = recvdata.decode('utf-8')
        loc = revstr.split(",")
        print(loc)
    def sendaction(self,direction,delta):
        print("sendaction動作開始")

if __name__ == "__main__":
    controller = Controller(0, 0)
    location = []
    while(True):
        data1 = input('>')
        print(data1)
        location = controller.getlocation(data1)
        
    tcpCliSock.close()
