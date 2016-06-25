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
    /// Contains one or more TelephoneNumber object(s).
    /// </summary>
    #if !PocketPC
    [Serializable]
    #endif
    public class TelephoneNumberCollection : CollectionBase
    {
        public void Add(TelephoneNumber number)
        {
            List.Add(number);
        }
        public void Add(string number)
        {
            List.Add(new TelephoneNumber(number));
        }
        public void Add(string number, TelephoneNumberSingleType type)
        {
            List.Add(new TelephoneNumber(number, type));
        }
        public TelephoneNumber this[int index]
        {
            get
            {
                return (TelephoneNumber)List[index];
            }
        }
        /// <summary>
        /// Returns the first object in the collection whose Type is Prefered.
        /// </summary>
        /// <returns>The first object in the collection whose Type is Prefered.</returns>
        public TelephoneNumber GetPrefered()
        {
            foreach(TelephoneNumber number in this)
                if (number.IsPrefered)
                    return number;
            return null;
        }
    }
}

