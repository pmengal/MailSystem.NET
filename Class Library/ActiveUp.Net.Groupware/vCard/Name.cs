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
    public class Name
    {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string[] AdditionalNames { get; set; }
        public string[] Prefixes { get; set; }
        public string[] Suffixes { get; set; }

        public string GetFormattedLine()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("N:");
            if(FamilyName != null)
                sb.Append(Parser.Escape(FamilyName) +";");
            else
                sb.Append(";");
            if(GivenName != null)
                sb.Append(Parser.Escape(GivenName) +";");
            else
                sb.Append(";");
            if(AdditionalNames != null && AdditionalNames.Length>0)
            {
                foreach(string str in AdditionalNames)
                    sb.Append(Parser.Escape(str)+",");
                sb.Remove(sb.Length-1,1);
                sb.Append(";");
            }
            if(Prefixes != null && Prefixes.Length>0)
            {
                foreach(string str in Prefixes)
                    sb.Append(Parser.Escape(str)+",");
                sb.Remove(sb.Length-1,1);
                sb.Append(";");
            }
            if(Suffixes != null && Suffixes.Length>0)
            {
                foreach(string str in Suffixes)
                    sb.Append(Parser.Escape(str)+",");
                sb.Remove(sb.Length-1,1);
            }
            return sb.ToString();
        }
    }
}
