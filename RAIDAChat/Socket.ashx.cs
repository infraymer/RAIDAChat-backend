using Fleck;
using Newtonsoft.Json;
using RAIDAChat.Reflection.dto;
using RAIDAChat.ReflectionClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat
{
    /// <summary>
    /// Сводное описание для Socket
    /// </summary>
    public class Socket : IHttpHandler
    {
        static List<AuthInfoWithSocket> mClients = new List<AuthInfoWithSocket>();
        public static void OnWebSocketConnection(IWebSocketConnection socket)
        {
            socket.OnError = delegate (Exception ex) { OnError(socket, ex); };
            socket.OnOpen = delegate () { OnOpen(socket); };
            socket.OnClose = delegate () { OnClose(socket); };
            socket.OnMessage = delegate (string message) { OnMessage(socket, message); };
        }
        private static void OnError(IWebSocketConnection socket, Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        private static void OnOpen(IWebSocketConnection socket)
        {
           // mClients.Add(socket);
        }
        private static void OnClose(IWebSocketConnection socket)
        {
            //mClients.Remove(socket);
        }

        private static void OnMessage(IWebSocketConnection socket, string message)
        {
            //string response = message;
            //var fun = message.Split(" ".ToCharArray(), 3)[2];
            OutputSocketMessage outputSocket;
            InputSocketMessage socketMessage;
            try
            {
                socketMessage = JsonConvert.DeserializeObject<InputSocketMessage>(message);
                if (socketMessage == null) throw new Exception();
            }
            catch
            {
                outputSocket = new OutputSocketMessage("DeserializeObject error:",
                            false,
                            String.Format("Input message: '{0}' is not valid", message),
                            new { }
                        );
                BroadcastEcho(socket, JsonConvert.SerializeObject(outputSocket));
                return;
            }

            if (socketMessage.execFun.Equals("subscribe", StringComparison.CurrentCultureIgnoreCase))
            {
                AuthInfoWithSocket inf = (new MainAction()).Auth(socketMessage.data);
                if (inf.auth)
                {
                    inf.socket = socket;
                    mClients.Add(inf);
                    outputSocket = new OutputSocketMessage(socketMessage.execFun, true, "", new { });
                    BroadcastEcho(socket, JsonConvert.SerializeObject(outputSocket));
                    return;
                }
                else
                {
                    outputSocket = new OutputSocketMessage(socketMessage.execFun,
                        false,
                        String.Format("Invalid login or AN"),
                        new { }
                    );
                    BroadcastEcho(socket, JsonConvert.SerializeObject(outputSocket));
                    return;
                }
            }
            else
            {

                MainAction refClass = new MainAction();

                var me = refClass.GetType().GetMethod(socketMessage.execFun.ToLower(), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (me != null)
                {

                    OutputSocketMessageWithUsers response = (OutputSocketMessageWithUsers)me.Invoke(refClass, new object[] { socketMessage.data });

                    outputSocket = response.msg;

                    if (outputSocket.success)
                    {
                        Action<AuthInfoWithSocket> action = delegate (AuthInfoWithSocket s) { BroadcastEcho(s.socket, JsonConvert.SerializeObject(outputSocket)); };
                        mClients.Where(it => response.usersId.Contains(it.login)).ToList().ForEach(action);
                    }
                    else
                    {
                        BroadcastEcho(socket, JsonConvert.SerializeObject(outputSocket));
                    }

                }
                else
                {
                    outputSocket = new OutputSocketMessage(socketMessage.execFun,
                                false,
                                String.Format("Function: '{0}' not found", socketMessage.execFun),
                                new { }
                            );
                    BroadcastEcho(socket, JsonConvert.SerializeObject(outputSocket));
                    return;
                }
            }                        
            
        }
        private static void BroadcastEcho(IWebSocketConnection s, string message)
        {
            s.Send("Echo: " + message);
        }

        public void ProcessRequest(HttpContext context)
        {
            
            /*
            System.Net.IPHostEntry ipHostInfo = System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            System.Net.IPEndPoint localEP = new System.Net.IPEndPoint(ipHostInfo.AddressList[0], 49151);
            System.Net.Sockets.Socket listener = new System.Net.Sockets.Socket(System.Net.IPAddress.Any.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            //context.Response.WriteFile(String.Format("Local address and port : {0}", localEP.Address.ToString()));
            
            try
            {
                listener.Bind(localEP);
                listener.Listen(10);
                context.Response.Write("Succes open");
            } catch (Exception e) {
                context.Response.Write(e.ToString());  
            }*/
            
    
                WebSocketServer server = new WebSocketServer("ws://0.0.0.0:49151");
                server.ListenerSocket.NoDelay = true;
                Action<IWebSocketConnection> config = delegate (IWebSocketConnection socket) { OnWebSocketConnection(socket); };
                try
                {
                    server.Start(config);
                    context.Response.Write("Success open");

                }
                catch (Exception e)
                {
                    context.Response.Write(e);
                    //context.Response.Write(String.Format("Info WebSocketserver.  ListenerSocket.LocalEndPoint:{0}; Port:{1}; Location:{2}", server.ListenerSocket.LocalEndPoint, server.Port, server.Location));
                    context.Response.StatusCode = 201;
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