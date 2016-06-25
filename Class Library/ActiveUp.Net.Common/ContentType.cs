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

using System;

namespace ActiveUp.Net.Mail
{
#if !PocketPC
    [Serializable]
#endif
    public class ContentType : StructuredHeaderField
    {
        string _mimeType = "text/plain";

        public string Type
        {
            get
            {
                return _mimeType.Split('/')[0];
            }
            set
            {
                _mimeType = value + "/" + SubType;
            }
        }
        public string SubType
        {
            get
            {
                return _mimeType.Split('/')[1];
            }
            set
            {
                _mimeType = Type + "/" + value;
            }
        }
        public string MimeType
        {
            get
            {
                return _mimeType;
            }
            set
            {
                _mimeType = value;
            }
        }
        public override string ToString()
        {
            string str = string.Empty;
            str += "Content-Type: " + MimeType;
            foreach (string key in Parameters.AllKeys)
            {
                string value = string.Empty;
                
                if (key.Equals("boundary"))
                    value = "\"" + Parameters[key] + "\"";
                else
                    value = Parameters[key];

                str += ";\r\n\t" + key + "=" + value;
            }
            return str;
        }
    }
}
