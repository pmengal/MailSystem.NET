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

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// Contains Smtp Exceptions.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class SmtpExceptionCollection : CollectionBase
    {
        /// <summary>
        /// Add a SmtpException object in the collection.
        /// </summary>
        /// <param name="exception">Exception to add.</param>
        public void Add(SmtpException exception)
        {
            List.Add(exception);
        }

        /// <summary>
        /// Add a SmtpException in the collection specifing it's exchange name and preference level.
        /// </summary>
        /// <param name="message">Message of the exception to add.</param>
        public void Add(string message)
        {
            List.Add(new SmtpException(message));
        }

        /// <summary>
        /// Remove the SmtpException at the specified index position.
        /// </summary>
        /// <param name="index">The index position.</param>
        public void Remove(int index)
        {
            // Check to see if there is a SmtpException at the supplied index.
            if (index < Count || index >= 0)
                List.RemoveAt(index); 
        }

        /// <summary>
        /// Returns the SmtpException at the specified index position.
        /// </summary>
        public SmtpException this[int index]
        {
            get { return (SmtpException)List[index]; }
        }
    }
}