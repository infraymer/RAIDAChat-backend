using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace RAIDAChat
{
    /// <summary>
    /// Сводное описание для newSocket
    /// </summary>
    public class newSocket : IHttpHandler
    {
        WebSocketServer webSocket;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                webSocket = new WebSocketServer();
                var serverConfig = new SuperSocket.SocketBase.Config.ServerConfig();
                serverConfig.MaxConnectionNumber = 100000;

                var list = new List<SuperSocket.SocketBase.Config.ListenerConfig>(20);
                int port = 49151;
                for (int i = 0; i < 1; i++)
                {
                    var listener = new SuperSocket.SocketBase.Config.ListenerConfig();
                    listener.Port = port;
                    listener.Backlog = 1000;

                    listener.Ip = "Any";
                    listener.Security = "None";
                    //port++;

                    list.Add(listener);
                }



                serverConfig.Listeners = list;
                webSocket.Setup(serverConfig);

                //webSocket.NewSessionConnected += server_NewSessionConnected;
                //webSocket.SessionClosed += server_SessionClosed;
                webSocket.NewMessageReceived += WebSocket_NewMessageReceived;

                webSocket.Start();

                context.Response.Write(webSocket.Listeners[0].EndPoint.ToString() + "; " + webSocket.State.ToString());
            }
            catch (Exception e)
            {
                context.Response.Write(e.ToString());
            }



            /*
                        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());

                        foreach (IPAddress ip in ipHostInfo.AddressList)
                        {
                            context.Response.Write(ip.ToString());
                        }

                        // получаем адреса для запуска сокета
                        IPEndPoint ipPoint = new IPEndPoint(ipHostInfo.AddressList[0], 49151);

                        // создаем сокет
                        //System.Net.IPAddress.Any.AddressFamily
                        //AddressFamily.InterNetwork

                        System.Net.Sockets.Socket listenSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        try
                        {
                            // связываем сокет с локальной точкой, по которой будем принимать данные
                            listenSocket.Bind(ipPoint);

                            // начинаем прослушивание
                            listenSocket.Listen(10);

                            context.Response.Write("Сервер запущен. Ожидание подключений...");

                            while (true)
                            {
                                System.Net.Sockets.Socket handler = listenSocket.Accept();
                                // получаем сообщение
                                StringBuilder builder = new StringBuilder();
                                int bytes = 0; // количество полученных байтов
                                byte[] data = new byte[1024]; // буфер для получаемых данных

                                do
                                {
                                    bytes = handler.Receive(data);
                                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                                }
                                while (handler.Available > 0);

                               // Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                                // отправляем ответ
                                string message = DateTime.Now.ToShortTimeString() + " - Получено сообщение: " + builder.ToString();
                                data = Encoding.Unicode.GetBytes(message);
                                handler.Send(data);
                                // закрываем сокет
                                if (builder.ToString().Equals("close socket"))
                                {
                                    handler.Shutdown(SocketShutdown.Both);
                                    handler.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            context.Response.Write(ex.Message);
                            context.Response.StatusCode = 555;
                            listenSocket.Close();
                        }
                        */
        }

        private void WebSocket_NewMessageReceived(WebSocketSession session, string value)
        {
            session.Send(value);

            if (value.Equals("exit") && webSocket != null )
            {

                session.Send("Сокет будет закрыт");
                webSocket.Stop();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}