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
    /// <summary>
    /// ContentType Header Class.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class ContentType : StructuredHeaderField
    {
        string _mimeType = "text/plain";

        /// <summary>
        /// Mimetype original or Type + Subtype.
        /// </summary>
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

        /// <summary>
        /// The type of content-type
        /// </summary>
        public string Type
        {
            get
            {
                var type = _mimeType.Split('/')[0];
                return type ?? "";
            }
            set
            {
                _mimeType = value + "/" + SubType;
            }
        }

        /// <summary>
        /// Subtype of content-type received, if invalid, return "plain"
        /// </summary>
        public string SubType
        {
            get
            {
                var mimeParts = _mimeType.Split('/');
                if (mimeParts.Length < 2) return "plain";
                var subtype = _mimeType.Split('/')[1];
                subtype = string.IsNullOrWhiteSpace(subtype) ? "plain" : subtype;
                return subtype;
            }
            set
            {
                _mimeType = Type + "/" + value;
            }
        }

        /// <summary>
        /// Override of method to explain content type string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Content-Type: " + MimeType);
            foreach (string key in Parameters.AllKeys)
            {
                var value = string.Empty;

                if (key.Equals("boundary"))
                    value = "\"" + Parameters[key] + "\"";
                else
                    value = Parameters[key];

                builder.Append(";\r\n\t" + key + "=" + value);
            }
            return builder.ToString();
        }
    }
}
