using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;

namespace Joclibrarie.Library
{
    public class Player
    {
        public string Name { get; set; }
        public NetConnection Connection { get; set; }

        public double XPosition { get; set; }
        public double YPosition { get; set; }
    }
}
