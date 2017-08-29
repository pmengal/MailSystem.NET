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
    /// Contains informations about one trace information (one Received header).
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class TraceInfo
    {
        public TraceInfo()
        {

        }
        public TraceInfo(string from, DateTime date, string by, string via, string with, string ffor, string id)
        {
            Initialize(from, date, by, via, with, ffor, id);
        }
        public TraceInfo(string from, DateTime date, string by, string via, string with, string ffor)
        {
            Initialize(from, date, by, via, with, ffor, string.Empty);
        }
        public TraceInfo(string from, DateTime date, string by, string via, string with)
        {
            Initialize(from, date, by, via, with, string.Empty, string.Empty);
        }
        public TraceInfo(string from, DateTime date, string by, string via)
        {
            Initialize(from, date, by, via, string.Empty, string.Empty, string.Empty);
        }
        public TraceInfo(string from, DateTime date, string by)
        {
            Initialize(from, date, by, string.Empty, string.Empty, string.Empty, string.Empty);
        }
        private void Initialize(string from, DateTime date, string by, string via, string with, string ffor, string id)
        {
            From = from;
            By = by;
            Via = via;
            With = with;
            For = ffor;
            Id = id;
            Date = date;
        }

        /// <summary>
        /// Contains both (1) the name of the source host as presented in the EHLO command to the SMTP server and (2) an address literal containing the IP address of the source, determined from the TCP connection with the SMTP server.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Contains the name of the SMTP host who received and processed the message.
        /// </summary>
        public string By { get; set; }
        /// <summary>
        /// Contains a mean of communication that was used for the transaction between the SMTP server and the FROM user.
        /// </summary>
        /// <example>"TCP"</example>
        public string Via { get; set; }
        /// <summary>
        /// The protocol used for the transaction by the SMTP server and the FROM user.
        /// </summary>
        public string With { get; set; }
        /// <summary>
        /// An identification string for the transaction.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The destination mailbox for which the transaction was executed.
        /// </summary>
        public string For { get; set; }
        /// <summary>
        /// The date and time of the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        public override string ToString()
        {
            string source = string.Empty;
            if (!string.IsNullOrEmpty(From))
                source += " from " + From + "\r\n ";
            if (!string.IsNullOrEmpty(By))
                source += " by " + By + "\r\n ";
            if (!string.IsNullOrEmpty(With))
                source += " with " + With + "\r\n ";
            if (!string.IsNullOrEmpty(For))
                source += " for " + For + "\r\n ";
            if (!string.IsNullOrEmpty(Via))
                source += " via " + Via + "\r\n ";
            if (!string.IsNullOrEmpty(Id))
                source += " id " + Id + "\r\n ";

            if (string.IsNullOrEmpty(source))
                return "";
            return source.Remove(0, source.Length - 3) + ";" + Date.ToString("r");
        }
    }
}
