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

namespace ActiveUp.Net.WhoIs
{
    /// <summary>
    /// Result of a whois server inficates if a domain is available for registration or not.
    /// </summary>
    public class ResultIsAvailable : Result
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResultIsAvailable() : base()
        {
            IsAvailable = false;
        }

        /// <summary>
        /// Create a ResultIsAvailable object from the value indicates if a domain is available.
        /// </summary>
        /// <param name="isAvailable">Indicates if a domain if available for registration or not.</param>
        public ResultIsAvailable(bool isAvailable) : base()
        {
            IsAvailable = isAvailable;
        }

        /// <summary>
        /// Create a ResultIsAvailable object from the value indicates if a domain is available and the Server object.
        /// </summary>
        /// <param name="isAvailable">Indicates if a domain if available for registration or not.</param>
        /// <param name="server">Whois server used.</param>
        public ResultIsAvailable(bool isAvailable, Server server) : base(server)
        {
            IsAvailable = isAvailable;
        }

        /// <summary>
        /// Create a ResultIsAvailable object from the value indicates if a domain is available, the Server object and exception object.
        /// </summary>
        /// <param name="isAvailable">Indicates if a domain if available for registration or not.</param>
        /// <param name="server">Whois server used.</param>
        /// <param name="exception">Exception if an error occurs.</param>
        public ResultIsAvailable(bool isAvailable, Server server, Exception exception) : base(server, exception)
        {
            IsAvailable = isAvailable;
        }
        
        #endregion

        #region Properties
        
        /// <summary>
        /// Gets / sets the value indicates if a domain is available for registration or not.
        /// </summary>
        public bool IsAvailable { get; set; }
        #endregion
    }
}
