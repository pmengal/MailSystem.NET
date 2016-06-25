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
    /// <summary>
    /// Parses text to vCard objects.
    /// </summary>
    public static class Parser
    {
        public static vCard Parse(string data)
        {
            vCard card = new vCard();
            data = Unfold(data);
            data = data.Replace("\\,","²²²COMMA²²²");
            data = data.Replace("\\;","²²²SEMICOLON²²²");
            foreach(string line in System.Text.RegularExpressions.Regex.Split(data,"\r\n"))
            {
                string fulltype = line.Split(':')[0];
                string type = fulltype.Split(';')[0].ToUpper();
                switch(type)
                {
                    case "NAME":
                        SetDisplayName(card,line);
                        break;
                    case "FN":
                        SetFullName(card,line);
                        break;
                    case "N":
                        SetName(card,line);
                        break;
                    case "NICKNAME":
                        SetNickName(card,line);
                        break;
                    case "SOURCE":
                        SetSource(card,line);
                        break;
                    case "MAILER":
                        SetMailer(card,line);
                        break;
                    case "TZ":
                        SetTimeZone(card,line);
                        break;
                    case "TITLE":
                        SetTitle(card,line);
                        break;
                    case "ROLE":
                        SetRole(card,line);
                        break;
                    case "NOTE":
                        SetNote(card,line);
                        break;
                    case "PRODID":
                        SetGeneratorId(card,line);
                        break;
                    case "SORT-STRING":
                        SetSortText(card,line);
                        break;
                    case "UID":
                        SetUid(card,line);
                        break;
                    case "URL":
                        SetUrl(card,line);
                        break;
                    case "VERSION":
                        SetVersion(card,line);
                        break;
                    case "CLASS":
                        SetAccessClass(card,line);
                        break;
                    case "ADR":
                        AddAddress(card,line);
                        break;
                    case "TEL":
                        AddTelephoneNumber(card,line);
                        break;
                    case "LABEL":
                        AddLabel(card,line);
                        break;
                    case "EMAIL":
                        AddEmail(card,line);
                        break;
                    case "BDAY":
                        SetBirthday(card,line);
                        break;
                    case "REV":
                        SetRevision(card,line);
                        break;
                    case "ORG":
                        SetOrganization(card,line);
                        break;
                    case "CATEGORIES":
                        SetCategories(card,line);
                        break;
                    case "PHOTO":
                        SetPhoto(card,line);
                        break;
                    case "SOUND":
                        SetSound(card,line);
                        break;
                    case "LOGO":
                        SetLogo(card,line);
                        break;
                    case "KEY":
                        SetKey(card,line);
                        break;
                    case "GEO":
                        SetGeo(card,line);
                        break;
                }
            }
            return card;
        }

        public static vCard Parse(byte[] data)
        {
            return Parse(Encoding.UTF7.GetString(data,0,data.Length));
        }

        public static DateTime ParseDate(string input)
        {
            try {
                return DateTime.Parse(input);
            }
            catch 
            {
                if(input.Length==8)
                {
                    input = input.Insert(4,"-");
                    input = input.Insert(7,"-");
                }
                else if(input.Length==16)
                {
                    input = input.Insert(4,"-");
                    input = input.Insert(7,"-");
                    input = input.Insert(13,":");
                    input = input.Insert(16,":");
                }
            }
            return DateTime.Parse(input);
        }
        private static void AddAddress(vCard card, string line)
        {
            string val = Unescape(line.Replace(line.Split(':')[0]+":",""));
            string type = line.Split(':')[0].ToUpper();
            if(type.IndexOf("ENCODING=QUOTED-PRINTABLE")!=-1) val = FromQuotedPrintable(val,"utf-8");
            if (type.IndexOf("ENCODING=B") != -1)
            {
                byte[] data = Convert.FromBase64String(val);
                val = Encoding.UTF8.GetString(data,0,data.Length);
            }
            string[] values = val.Split(';');
            Address adr = new Address();
            if (values.Length > 0 && values[0].Length > 0)
                adr.POBox = Convert.ToInt32(values[0]);
            if (values.Length > 1 && values[1].Length > 0)
                adr.ExtendedAddress = values[1];
            if (values.Length > 2 && values[2].Length>0)
                adr.StreetAddress = values[2];
            if (values.Length > 3 && values[3].Length > 0)
                adr.Locality = values[3];
            if (values.Length > 4 && values[4].Length > 0)
                adr.Region = values[4];
            if (values.Length > 5 && values[5].Length > 0)
                adr.PostalCode = values[5];
            if (values.Length > 6 && values[6].Length > 0)
                adr.Country = values[6];
            string parameters = line.Split(':')[0].ToUpper();
            if(parameters.IndexOf("DOM")!=-1)
                adr.IsDomestic = true;
            if(parameters.IndexOf("INTL")!=-1)
                adr.IsInternational = true;
            if(parameters.IndexOf("POSTAL")!=-1)
                adr.IsPostal = true;
            if(parameters.IndexOf("PARCEL")!=-1)
                adr.IsParcel = true;
            if(parameters.IndexOf("HOME")!=-1)
                adr.IsHome = true;
            if(parameters.IndexOf("WORK")!=-1)
                adr.IsWork = true;
            if(parameters.IndexOf("PREF")!=-1)
                adr.IsPrefered = true;
            card.Addresses.Add(adr);
        }

        private static void AddTelephoneNumber(vCard card, string line)
        {
            string val = Unescape(line.Replace(line.Split(':')[0]+":",""));
            TelephoneNumber tel = new TelephoneNumber(val);

            string parameters = line.Split(':')[0].ToUpper();
            if(parameters.IndexOf("HOME")!=-1)
                tel.IsHome = true;
            if(parameters.IndexOf("MSG")!=-1)
                tel.IsMessage = true;
            if(parameters.IndexOf("WORK")!=-1)
                tel.IsWork = true;
            if(parameters.IndexOf("VOICE")!=-1)
                tel.IsVoice = true;
            if(parameters.IndexOf("FAX")!=-1)
                tel.IsFax = true;
            if(parameters.IndexOf("PREF")!=-1)
                tel.IsPrefered = true;
            if(parameters.IndexOf("CELL")!=-1)
                tel.IsCellular = true;
            if(parameters.IndexOf("VIDEO")!=-1)
                tel.IsVideo = true;
            if(parameters.IndexOf("PAGER")!=-1)
                tel.IsPager = true;
            if(parameters.IndexOf("BBS")!=-1)
                tel.IsBulletinBoard = true;
            if(parameters.IndexOf("MODEM")!=-1)
                tel.IsModem = true;
            if(parameters.IndexOf("CAR")!=-1)
                tel.IsCar = true;
            if(parameters.IndexOf("ISDN")!=-1)
                tel.IsISDN = true;
            if(parameters.IndexOf("PCS")!=-1)
                tel.IsPersonalCommunication = true;
            card.TelephoneNumbers.Add(tel);
        }

        private static void AddLabel(vCard card, string line)
        {
            string val = Unescape(line.Replace(line.Split(':')[0]+":",""));
            string type = line.Split(':')[0].ToUpper();
            if(type.IndexOf("ENCODING=QUOTED-PRINTABLE")!=-1)
                val = FromQuotedPrintable(val,"utf-8");
            if (type.IndexOf("ENCODING=B") != -1)
            {
                byte[] data = Convert.FromBase64String(val);
                val = Encoding.UTF8.GetString(data,0,data.Length);
            }
            Label label = new Label();
            label.Value = val;
            string parameters = line.Split(':')[0].ToUpper();
            if(parameters.IndexOf("DOM")!=-1)
                label.IsDomestic = true;
            if(parameters.IndexOf("INTL")!=-1)
                label.IsInternational = true;
            if(parameters.IndexOf("POSTAL")!=-1)
                label.IsPostal = true;
            if(parameters.IndexOf("PARCEL")!=-1)
                label.IsParcel = true;
            if(parameters.IndexOf("HOME")!=-1)
                label.IsHome = true;
            if(parameters.IndexOf("WORK")!=-1)
                label.IsWork = true;
            if(parameters.IndexOf("PREF")!=-1)
                label.IsPrefered = true;
            card.Labels.Add(label);
        }

        private static void AddEmail(vCard card, string line)
        {
            string val = line.Split(':')[1];
            EmailAddress email = new EmailAddress(val);
            string parameters = line.Split(':')[0].ToUpper();
            if(parameters.IndexOf("INTERNET")!=-1)
                email.IsInternet = true;
            if(parameters.IndexOf("X400")!=-1)
                email.IsX400 = true;
            if(parameters.IndexOf("PREF")!=-1)
                email.IsPrefered = true;
            card.EmailAddresses.Add(email);
        }

        private static void SetFullName(vCard card, string line)
        {
            card.FullName = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }

        private static void SetDisplayName(vCard card, string line)
        {
            card.DisplayName = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }

        private static void SetName(vCard card, string line)
        {
            string val = Unescape(line.Replace(line.Split(':')[0]+":",""));
            string[] values = val.Split(';');
            if(values.Length==0)
                return;
            if(values.Length>0)
                card.Name.FamilyName = Unescape(values[0]);
            if(values.Length>1)
                card.Name.GivenName = Unescape(values[1]);
            if(values.Length>2)
                card.Name.AdditionalNames = UnescapeArray(values[2].Split(','));
            if(values.Length>3)
                card.Name.Prefixes = UnescapeArray(values[3].Split(','));
            if(values.Length>4)
                card.Name.Suffixes = UnescapeArray(values[4].Split(','));
        }
        private static void SetNickName(vCard card, string line)
        {
            card.Nickname = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetSource(vCard card, string line)
        {
            card.Source = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetMailer(vCard card, string line)
        {
            card.Mailer = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetTimeZone(vCard card, string line)
        {
            card.TimeZone = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetTitle(vCard card, string line)
        {
            card.Title = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetRole(vCard card, string line)
        {
            card.Role = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetNote(vCard card, string line)
        {
            card.Note = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetGeneratorId(vCard card, string line)
        {
            card.GeneratorId = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetSortText(vCard card, string line)
        {
            card.SortText = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetUid(vCard card, string line)
        {
            card.Uid = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetUrl(vCard card, string line)
        {
            card.Url = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetVersion(vCard card, string line)
        {
            card.Version = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetAccessClass(vCard card, string line)
        {
            card.AccessClass = Unescape(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetBirthday(vCard card, string line)
        {
            card.Birthday = ParseDate(Unescape(line.Replace(line.Split(':')[0]+":","")));
        }
        private static void SetRevision(vCard card, string line)
        {
            card.Revision = ParseDate(Unescape(line.Replace(line.Split(':')[0]+":","")));
        }
        private static void SetOrganization(vCard card, string line)
        {
            foreach (string organisation in UnescapeArray(line.Replace(line.Split(':')[0] + ":", "").Split(',')))
                card.Organization.Add(organisation);// = Parser.UnescapeArray(line.Replace(line.Split(':')[0]+":","").Split(','));
        }
        private static void SetCategories(vCard card, string line)
        {
            foreach(string category in UnescapeArray(line.Replace(line.Split(':')[0]+":","").Split(',')))
                card.Categories.Add(category);// = Parser.UnescapeArray(line.Replace(line.Split(':')[0]+":","").Split(','));
        }
        private static void SetPhoto(vCard card, string line)
        {
            card.Photo = Convert.FromBase64String(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetLogo(vCard card, string line)
        {
            card.Logo = Convert.FromBase64String(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetSound(vCard card, string line)
        {
            card.Sound = Convert.FromBase64String(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetKey(vCard card, string line)
        {
            card.Key = Convert.FromBase64String(line.Replace(line.Split(':')[0]+":",""));
        }
        private static void SetGeo(vCard card, string line)
        {
            GeographicalPosition geo = new GeographicalPosition();
            string val = Unescape(line.Replace(line.Split(':')[0]+":",""));
            string[] values = val.Split(';');
            geo.Latitude = Convert.ToDecimal(values[0]);
            geo.Longitude = Convert.ToDecimal(values[1]);
            card.GeographicalPosition = geo;
        }
        public static string Escape(string input)
        {
            input = input.Replace(",","\\,");
            input = input.Replace(";","\\;");
            input = input.Replace("\\n","\\\\n");
            return input;
        }
        public static string Unescape(string input)
        {
            input = input.Replace("\\,",",");
            input = input.Replace("\\;",";");
            input = input.Replace("\\\\n","\\n");
            input = input.Replace("\\\\N","\\n");
            input = input.Replace("²²²COMMA²²²",",");
            input = input.Replace("²²²SEMICOLON²²²",";");
            return input;
        }
        public static string[] UnescapeArray(string[] input)
        {
            for(int i=0;i<input.Length;i++) input[i] = Unescape(input[i]);
            return input;
        }
        public static string Fold(string input)
        {
            StringBuilder sb = new StringBuilder();
            int i=0;
            for(i=0;i<input.Length-72;i+=72)
                sb.Append(input.Substring(i,72)+"\r\n ");
            sb.Append(input.Substring(i));
            return sb.ToString();
        }
        public static string Unfold(string input)
        {
            input = input.Replace("\r\n ","");
            return input;
        }
        public static string FromQuotedPrintable(string input, string toCharset)
        {
            try
            {
                input = input.Replace("=\r\n", "") + "=3D=3D";
                System.Collections.ArrayList arr = new System.Collections.ArrayList();
                int i = 0;
                byte[] decoded = new byte[0];
                while (true)
                {
                    if (i <= (input.Length) - 3)
                    {
                        if (input[i] == '=' && input[i + 1] != '=')
                        {
                            arr.Add(Convert.ToByte(int.Parse(string.Concat((char)input[i + 1], (char)input[i + 2]), System.Globalization.NumberStyles.HexNumber)));
                            i += 3;
                        }
                        else
                        {
                            arr.Add((byte)input[i]);
                            i++;
                        }
                    }
                    else break;
                }
                decoded = new byte[arr.Count];
                for (int j = 0; j < arr.Count; j++)
                    decoded[j] = (byte)arr[j];
                return Encoding.GetEncoding(toCharset).GetString(decoded,0,decoded.Length).TrimEnd('=');
            }
            catch { return input; }
        }
    }
}