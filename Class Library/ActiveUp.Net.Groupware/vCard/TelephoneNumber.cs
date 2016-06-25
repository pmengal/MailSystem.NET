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
using System.Text;

namespace ActiveUp.Net.Groupware.vCard
{
    public enum TelephoneNumberSingleType
    {
        Home,
        Message,
        Work,
        Voice,
        Fax,
        Prefered,
        Cellular,
        Video,
        Pager,
        BulletinBoard,
        Modem,
        Car,
        ISDN,
        PersonalCommunication
    }
    /// <summary>
    /// Description résumée de vCard.
    /// </summary>
    #if !PocketPC
    [Serializable]
    #endif
    public class TelephoneNumber
    {
        public TelephoneNumber(string number)
        {
            Number = number;
        }
        public TelephoneNumber(string number, TelephoneNumberSingleType type)
        {
            Number = number;
            switch (type)
            {
                case TelephoneNumberSingleType.BulletinBoard:
                    IsBulletinBoard = true;
                    break;
                case TelephoneNumberSingleType.Car:
                    IsCar = true;
                    break;
                case TelephoneNumberSingleType.Cellular:
                    IsCellular = true;
                    break;
                case TelephoneNumberSingleType.Fax:
                    IsFax = true;
                    break;
                case TelephoneNumberSingleType.Home:
                    IsHome = true;
                    break;
                case TelephoneNumberSingleType.ISDN:
                    IsISDN = true;
                    break;
                case TelephoneNumberSingleType.Message:
                    IsMessage = true;
                    break;
                case TelephoneNumberSingleType.Modem:
                    IsModem = true;
                    break;
                case TelephoneNumberSingleType.Pager:
                    IsPager = true;
                    break;
                case TelephoneNumberSingleType.PersonalCommunication:
                    IsPersonalCommunication = true;
                    break;
                case TelephoneNumberSingleType.Prefered:
                    IsPrefered = true;
                    break;
                case TelephoneNumberSingleType.Video:
                    IsVideo = true;
                    break;
                case TelephoneNumberSingleType.Voice:
                    IsVoice = true;
                    break;
                case TelephoneNumberSingleType.Work:
                    IsWork = true;
                    break;
            }
        }

        public string Number { get; set; }
        public bool IsHome { get; set; }
        public bool IsMessage { get; set; }
        public bool IsWork { get; set; }
        public bool IsVoice { get; set; }
        public bool IsFax { get; set; }
        public bool IsPrefered { get; set; }
        public bool IsCellular { get; set; }
        public bool IsVideo { get; set; }
        public bool IsPager { get; set; }
        public bool IsBulletinBoard { get; set; }
        public bool IsModem { get; set; }
        public bool IsCar { get; set; }
        public bool IsISDN { get; set; }
        public bool IsPersonalCommunication { get; set; }

        public string GetFormattedLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("TEL;");
            if(IsBulletinBoard || IsCar || IsCellular || IsFax || IsHome || IsISDN || IsMessage || IsModem || IsPager || IsPersonalCommunication || IsPrefered || IsVoice || IsWork)
                sb.Append("TYPE=");
            if(IsBulletinBoard)
                sb.Append("bbs,");
            if(IsCar)
                sb.Append("car,");
            if(IsCellular)
                sb.Append("cell,");
            if(IsFax)
                sb.Append("fax,");
            if(IsHome)
                sb.Append("home,");
            if(IsMessage)
                sb.Append("msg,");
            if(IsModem)
                sb.Append("modem,");
            if(IsPager)
                sb.Append("pager,");
            if(IsPersonalCommunication)
                sb.Append("pcs,");
            if(IsPrefered)
                sb.Append("pref,");
            if(IsVideo)
                sb.Append("video,");
            if(IsVoice)
                sb.Append("voice,");
            if(IsWork)
                sb.Append("work,");
            sb.Remove(sb.Length-1,1);
            sb.Append(":");
            if(Number != null)
                sb.Append(Number);
            return sb.ToString();
        }
    }
}
