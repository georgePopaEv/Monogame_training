using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joclibrarie.Library;
using Lidgren.Network;


namespace ServerApp.Server
{
    class Program
    {
        private static NetServer _server;
        private static List<Player> _players;
        static void Main(string[] args)
        {
           
            var _players = new List<Player>();

            var config = new NetPeerConfiguration("netgame") { Port = 9981 };
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            var server = new NetServer(config);
            server.Start();

            Console.WriteLine("<<<<<Serverul este pornit>>>>>");
            while (true)
            {
                NetIncomingMessage inc;
                if ((inc = server.ReadMessage()) == null) continue;

                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Error:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        Console.WriteLine("...Conexiune noua");
                        var data = inc.ReadByte();
                        if(data == (byte)PacketType.Login)
                        {
                            Console.WriteLine("...Conexiune Acceptata");
                            var player = CreatePlayer(inc);
                            inc.SenderConnection.Approve();

                            var outmsg = server.CreateMessage();
                            outmsg.Write((byte)PacketType.Login);
                            outmsg.Write(true);
                            outmsg.Write(player.XPosition);
                            outmsg.Write(player.YPosition);
                            server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                        }
                        else
                        {
                            inc.SenderConnection.Deny("Nu s-au trimis datele de conexiune corecte");
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        break;
                    case NetIncomingMessageType.Receipt:
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.NatIntroductionSuccess:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                }
            }

        }

        private static Player CreatePlayer(NetIncomingMessage inc)
        {
            var random = new Random();
            var player = (new Player
            {
                Connection = inc.SenderConnection,
                Name = inc.ReadString(),
                XPosition = random.Next(0, 420),
                YPosition = random.Next(0, 750)

            });
            _players.Add(player);
            return player;
        }
    }
}
