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

namespace ActiveUp.Net.Groupware.vCard
{
    /// <summary>
    /// Description résumée de vCard.
    /// </summary>
    #if !PocketPC
    [System.Serializable]
    #endif
    public class EmailAddress
    {
        public EmailAddress(string address)
        {
            Address = address;
            IsInternet = true;
        }

        public EmailAddress(string address, bool isInternet, bool isPrefered)
        {
            Address = address;
            IsInternet = isInternet;
            IsPrefered = IsPrefered;
        }
        
        public string Address { get; set; }
        public bool IsInternet { get; set; }
        public bool IsX400 { get; set; }
        public bool IsPrefered { get; set; }

        public string GetFormattedLine()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("EMAIL;");
            if(IsInternet || IsPrefered || IsX400)
                sb.Append("TYPE=");
            if(IsInternet)
                sb.Append("internet,");
            if(IsPrefered)
                sb.Append("pref,");
            if(IsX400)
                sb.Append("x400,");
            sb.Remove(sb.Length-1,1);
            sb.Append(":");
            if(Address != null)
                sb.Append(Address);
            return sb.ToString();
        }
    }
}
