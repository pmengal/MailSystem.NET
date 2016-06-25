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

namespace ActiveUp.Net.Groupware.vCard
{
    /// <summary>
    /// Description résumée de vCard.
    /// </summary>
    #if !PocketPC
    [Serializable]
    #endif
    public class Label
    {
        public string Value { get; set; }
        public bool IsDomestic { get; set; }
        public bool IsInternational { get; set; }
        public bool IsPostal { get; set; }
        public bool IsParcel { get; set; }
        public bool IsWork { get; set; }
        public bool IsHome { get; set; }
        public bool IsPrefered { get; set; }

        public string GetFormattedLine()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("LABEL;");
            if(IsDomestic || IsHome || IsParcel || IsPostal || IsPrefered || IsWork)
                sb.Append("TYPE=");
            if(IsDomestic)
                sb.Append("dom,");
            if(IsHome)
                sb.Append("home,");
            if(IsParcel)
                sb.Append("parcel,");
            if(IsPostal)
                sb.Append("postal,");
            if(IsPrefered)
                sb.Append("pref,");
            if(IsWork)
                sb.Append("work,");
            sb.Remove(sb.Length-1,1);
            sb.Append(":");
            sb.Append(Parser.Escape(Value));
            return sb.ToString();
        }
    }
}
