// Copyright 2001-2010 - Active Up SPRLU (http://www.agilecomponents.com)
//
// This file is part of MailSystem.NET.
// MailSystem.NET is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// MailSystem.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System.Collections.Generic;
using System.Collections.Specialized;
using ActiveUp.Net.Security.OpenPGP.Packets;

namespace ActiveUp.Net.Security.OpenPGP
{
    public class ASCIIArmoredMessage
    {
        private List<Packet> _packets = new List<Packet>();
        private NameValueCollection _headers = new NameValueCollection();

        public string CRCBase64 { get; set; }
        public string Version
        {
            get
            {
                if (Headers["version"] == null)
                    return string.Empty;
                else
                    return Headers["version"];
            }
            set 
            {
                if (Headers["version"] == null)
                    Headers.Add("version", value);
                else
                    Headers["version"] = value;
            }
        }
        public string Comment
        {
            get
            {
                if (Headers["comment"] == null)
                    return string.Empty;
                else
                    return Headers["comment"];
            }
            set
            {
                if (Headers["comment"] == null)
                    Headers.Add("comment", value);
                else
                    Headers["comment"] = value;
            }
        }
        public string MessageID
        {
            get
            {
                if (Headers["messageid"] == null)
                    return string.Empty;
                else
                    return Headers["messageid"];
            }
            set
            {
                if (Headers["messageid"] == null)
                    Headers.Add("messageid", value);
                else
                    Headers["messageid"] = value;
            }
        }
        public string Charset
        {
            get
            {
                if (Headers["charset"] == null)
                    return string.Empty;
                else
                    return Headers["charset"];
            }
            set
            {
                if (Headers["charset"] == null)
                    Headers.Add("charset", value);
                else
                    Headers["charset"] = value;
            }
        }
        public List<Packet> Packets { get; set; } = new List<Packet>();
        public ASCIIArmoredMessageType Type { get; set; }
        public NameValueCollection Headers { get; set; } = new NameValueCollection();
    }
}
