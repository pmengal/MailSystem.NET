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
using System.Collections;

namespace ActiveUp.Net.Groupware.vCard
{
    /// <summary>
    /// Contains one or more EmailAddress object(s).
    /// </summary>
    #if !PocketPC
    [Serializable]
    #endif
    public class EmailAddressCollection : CollectionBase
    {
        public void Add(EmailAddress email)
        {
            List.Add(email);
        }
        public void Add(string address)
        {
            List.Add(new EmailAddress(address));
        }
        public void Add(string address, bool isInternet)
        {
            List.Add(new EmailAddress(address, isInternet, false));
        }

        public EmailAddress this[int index]
        {
            get
            {
                return (EmailAddress)List[index];
            }
        }
        /// <summary>
        /// Returns the first object in the collection whose Type is Prefered.
        /// </summary>
        /// <returns>The first object in the collection whose Type is Prefered.</returns>
        public EmailAddress GetPrefered()
        {
            foreach(EmailAddress email in this)
                if (email.IsPrefered)
                    return email;
            return null;
        }
    }
}
