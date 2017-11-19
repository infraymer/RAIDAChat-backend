using Newtonsoft.Json;
using RAIDAChat.Reflection.dto;
using RAIDAChat.ReflectionClass;
using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat
{
    public class newSocket
    {


        WebSocketServer webSocket;

        List<AuthInfoWithSocket> mClients = new List<AuthInfoWithSocket>();

        int portRcn = 49151;

        public void OpenSocket()
        {
            try
            {
                webSocket = new WebSocketServer();
                var serverConfig = new SuperSocket.SocketBase.Config.ServerConfig();
                serverConfig.MaxConnectionNumber = 100000;
 
                serverConfig.Listeners = new List<SuperSocket.SocketBase.Config.ListenerConfig>() {
                    new SuperSocket.SocketBase.Config.ListenerConfig() { Port = portRcn, Backlog = 1000, Ip = "Any", Security = "None" }
                };

                webSocket.Setup(serverConfig);
             
                webSocket.SessionClosed += WebSocket_SessionClosed; ;
                webSocket.NewMessageReceived += WebSocket_NewMessageReceived;

                webSocket.Start();

            }
            catch (Exception e)
            {
               
            }

        }

        private void WebSocket_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            mClients.Remove(mClients.First(it => it.socket == session));
        }

        private void WebSocket_NewMessageReceived(WebSocketSession session, string value)
        {

            OutputSocketMessage outputSocket;
            InputSocketMessage socketMessage;
            try
            {
                socketMessage = JsonConvert.DeserializeObject<InputSocketMessage>(value);
                if (socketMessage == null) throw new Exception();
            }
            catch
            {
                outputSocket = new OutputSocketMessage("DeserializeObject error:",
                            false,
                            String.Format("Input message: '{0}' is not valid", value),
                            new { }
                        );
                SendMessage(session, JsonConvert.SerializeObject(outputSocket));
                return;
            }

            if (socketMessage.execFun.Equals("subscribe", StringComparison.CurrentCultureIgnoreCase))
            {
                AuthInfoWithSocket inf = (new MainAction()).Auth(socketMessage.data);
                if (inf.auth)
                {
                    inf.socket = session;
                    mClients.Add(inf);
                    outputSocket = new OutputSocketMessage(socketMessage.execFun, true, "", new { });
                    SendMessage(session, JsonConvert.SerializeObject(outputSocket));
                    return;
                }
                else
                {
                    outputSocket = new OutputSocketMessage(socketMessage.execFun,
                        false,
                        String.Format("Invalid login or AN"),
                        new { }
                    );
                    SendMessage(session, JsonConvert.SerializeObject(outputSocket));
                    return;
                }
            }
            else if (socketMessage.execFun.Equals("closesocket") && webSocket != null)
            {
                session.Send("Сокет будет закрыт");
                webSocket.Stop();
            }
            else
            {

                if (mClients.Any(it => it.socket == session))
                {

                    MainAction refClass = new MainAction();

                    var me = refClass.GetType().GetMethod(socketMessage.execFun.ToLower(), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    if (me != null)
                    {

                        OutputSocketMessageWithUsers response = (OutputSocketMessageWithUsers)me.Invoke(refClass, new object[] { socketMessage.data });

                        outputSocket = response.msgForOwner;

                        SendMessage(session, JsonConvert.SerializeObject(outputSocket));

                        if (outputSocket.success)
                        {
                            Action<AuthInfoWithSocket> action = delegate (AuthInfoWithSocket s) { SendMessage(s.socket, JsonConvert.SerializeObject(response.msgForOther)); };
                            mClients.Where(it => response.usersId.Contains(it.login)).ToList().ForEach(action);
                        }

                    }
                    else
                    {
                        outputSocket = new OutputSocketMessage(socketMessage.execFun,
                                    false,
                                    String.Format("Function: '{0}' not found", socketMessage.execFun),
                                    new { }
                                );
                        SendMessage(session, JsonConvert.SerializeObject(outputSocket));
                        return;
                    }

                }
                else
                {
                    outputSocket = new OutputSocketMessage(socketMessage.execFun,
                                    false,
                                    String.Format("Сначала необходимо подписаться на уведомления (Можно сказать это авторизация)", socketMessage.execFun),
                                    new { }
                                );
                    SendMessage(session, JsonConvert.SerializeObject(outputSocket));
                    return;
                }
            }

        }



        private void SendMessage(WebSocketSession s, string message)
        {
            s.Send(message);
        }

    }
}