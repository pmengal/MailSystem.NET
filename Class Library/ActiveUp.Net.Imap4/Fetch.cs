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
using System.IO;
using System.Text;

namespace ActiveUp.Net.Mail
    {
    /// <summary>
    /// Allows to fetch (retrieve) partial or complete messages, as well as specific message informations.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class Fetch
    {
        private string _response;
        private byte[] _binaryResponse;

        #region Methods

        #region Private methods

        private CommandOptions getFetchOptions()
        {
            return new CommandOptions
            {
                IsPlusCmdAllowed = false
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns a non-extensible form of the BodyStructure() method.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The server's response containing a parenthesized list.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// string body = inbox.Fetch.Body(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim body As String = inbox.Fetch.Body(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var body:string = inbox.Fetch.Body(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Body(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("fetch "+messageOrdinal.ToString()+" body", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(" UID")-response.IndexOf("}")-7);
        }

        private delegate string DelegateBody(int messageOrdinal);
        private DelegateBody _delegateBody;

        public IAsyncResult BeginBody(int messageOrdinal, AsyncCallback callback)
        {
            _delegateBody = Body;
            return _delegateBody.BeginInvoke(messageOrdinal, callback, _delegateBody);
        }

        public string EndBody(IAsyncResult result)
        {
            return _delegateBody.EndInvoke(result);
        }
        
        public string UidBody(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(" UID")-response.IndexOf("}")-7);
        }

        private delegate string DelegateUidBody(int uid);
        private DelegateUidBody _delegateUidBody;

        public IAsyncResult BeginUidBody(int uid, AsyncCallback callback)
        {
            _delegateUidBody = UidBody;
            return _delegateUidBody.BeginInvoke(uid, callback, _delegateUidBody);
        }

        public string EndUidBody(IAsyncResult result)
        {
            return _delegateUidBody.EndInvoke(result);
        }

        /// <summary>
        /// Fetches a specific section of the message's body.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="section">The section (part number) to be fetched.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// string body = inbox.Fetch.BodySection(1,3);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim body As String = inbox.Fetch.Body(1,3);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var body:string = inbox.Fetch.Body(1,3);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string BodySection(int messageOrdinal, int section)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " body[" + section + "]", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(" UID")-response.IndexOf("}")-7);
        }

        private delegate string DelegateBodySection(int messageOrdinal, int section);
        private DelegateBodySection _delegateBodySection;

        public IAsyncResult BeginBodySection(int messageOrdinal, int section, AsyncCallback callback)
        {
            _delegateBodySection = BodySection;
            return _delegateBodySection.BeginInvoke(messageOrdinal, section, callback, _delegateBodySection);
        }

        public string EndBodySection(IAsyncResult result)
        {
            return _delegateBodySection.EndInvoke(result);
        }

        public string UidBodySection(int uid, int section)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body[" + section + "]", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(" UID")-response.IndexOf("}")-7);
        }

        private delegate string DelegateUidBodySection(int uid, int section);
        private DelegateUidBodySection _delegateUidBodySection;

        public IAsyncResult BeginUidBodySection(int uid, int section, AsyncCallback callback)
        {
            _delegateUidBodySection = BodySection;
            return _delegateUidBodySection.BeginInvoke(uid, section, callback, _delegateUidBodySection);
        }

        public string EndUidBodySection(IAsyncResult result)
        {
            return _delegateUidBodySection.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's body structure.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The server's response containing a parenthesized list</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// string body = inbox.Fetch.BodyStructure(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim body As String = inbox.Fetch.BodyStructure(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var body:string = inbox.Fetch.BodyStructure(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string BodyStructure(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " bodystructure", getFetchOptions());
            return response.Substring(response.IndexOf("bodystructure")+13,response.LastIndexOf(" UID")-response.IndexOf("bodystructure")-13);
        }

        private delegate string DelegateBodyStructure(int messageOrdinal);
        private DelegateBodyStructure _delegateBodyStructure;

        public IAsyncResult BeginBodyStructure(int messageOrdinal, AsyncCallback callback)
        {
            _delegateBodyStructure = BodyStructure;
            return _delegateBodyStructure.BeginInvoke(messageOrdinal, callback, _delegateBodyStructure);
        }

        public string EndBodyStructure(IAsyncResult result)
        {
            return _delegateBodyStructure.EndInvoke(result);
        }

        public string UidBodyStructure(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " bodystructure", getFetchOptions());
            return response.Substring(response.IndexOf("bodystructure")+13,response.LastIndexOf(" UID")-response.IndexOf("bodystructure")-13);
        }

        private delegate string DelegateUidBodyStructure(int uid);
        private DelegateUidBodyStructure _delegateUidBodyStructure;

        public IAsyncResult BeginUidBodyStructure(int uid, AsyncCallback callback)
        {
            _delegateUidBodyStructure = UidBodyStructure;
            return _delegateUidBodyStructure.BeginInvoke(uid, callback, _delegateUidBodyStructure);
        }

        public string EndUidBodyStructure(IAsyncResult result)
        {
            return _delegateUidBodyStructure.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's internal date.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's internal date.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// string internalDate = inbox.Fetch.InternalDate(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim internalDate As String = inbox.Fetch.InternalDate(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var internalDate:string = inbox.Fetch.InternalDate(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string InternalDate(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " internaldate", getFetchOptions());
            return response.Split('\"')[1];
        }

        private delegate string DelegateInternalDate(int messageOrdinal);
        private DelegateInternalDate _delegateInternalDate;

        public IAsyncResult BeginInternalDate(int messageOrdinal, AsyncCallback callback)
        {
            _delegateInternalDate = InternalDate;
            return _delegateInternalDate.BeginInvoke(messageOrdinal, callback, _delegateInternalDate);
        }

        public string EndInternalDate(IAsyncResult result)
        {
            return _delegateInternalDate.EndInvoke(result);
        }

        public string UidInternalDate(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " internaldate", getFetchOptions());
            return response.Split('\"')[1];
        }

        private delegate string DelegateUidInternalDate(int uid);
        private DelegateUidInternalDate _delegateUidInternalDate;

        public IAsyncResult BeginUidInternalDate(int uid, AsyncCallback callback)
        {
            _delegateUidInternalDate = UidInternalDate;
            return _delegateUidInternalDate.BeginInvoke(uid, callback, _delegateUidInternalDate);
        }

        public string EndUidInternalDate(IAsyncResult result)
        {
            return _delegateUidInternalDate.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's flags.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message info to be fetched.</param>
        /// <returns>A collection of flags.</returns>
            /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// FlagCollection flags = inbox.Fetch.Flags(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim flags As FlagCollection = inbox.Fetch.Flags(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var flags:FlagCollection = inbox.Fetch.Flags(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public FlagCollection Flags(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            FlagCollection flags = new FlagCollection();
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " flags", getFetchOptions());
            string flags_string = System.Text.RegularExpressions.Regex.Split(response.ToLower(),"flags ")[1].TrimStart('(').Split(')')[0];
            foreach(string str in flags_string.Split(' '))
                if (str.StartsWith("\\"))
                    flags.Add(str.Trim(new char[] {' ','\\',')','('}));
            return flags;
        }

        private delegate FlagCollection DelegateFlags(int messageOrdinal);
        private DelegateFlags _delegateFlags;

        public IAsyncResult BeginFlags(int messageOrdinal, AsyncCallback callback)
        {
            _delegateFlags = Flags;
            return _delegateFlags.BeginInvoke(messageOrdinal, callback, _delegateFlags);
        }

        public FlagCollection EndFlags(IAsyncResult result)
        {
            return _delegateFlags.EndInvoke(result);
        }

        public FlagCollection UidFlags(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            FlagCollection flags = new FlagCollection();
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " flags", getFetchOptions());
            string flags_string = System.Text.RegularExpressions.Regex.Split(response.ToLower(),"flags ")[1].TrimStart('(').Split(')')[0];
            foreach(string str in flags_string.Split(' '))
                if (str.StartsWith("\\"))
                    flags.Add(str.Trim(new char[] {' ','\\',')','('}));
            return flags;
        }

        private delegate FlagCollection DelegateUidFlags(int uid);
        private DelegateUidFlags _delegateUidFlags;

        public IAsyncResult BeginUidFlags(int uid, AsyncCallback callback)
        {
            _delegateUidFlags = UidFlags;
            return _delegateUidFlags.BeginInvoke(uid, callback, _delegateUidFlags);
        }

        public FlagCollection EndUidFlags(IAsyncResult result)
        {
            return _delegateUidFlags.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant Header (parsable by the Parsing namespace classes).
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the Header to be fetched.</param>
        /// <returns>The message's Header as a byte array.</returns>
        public byte[] Header(int messageOrdinal)
        {
            return Encoding.UTF8.GetBytes(HeaderString(messageOrdinal));
        }

        private delegate byte[] DelegateHeader(int messageOrdinal);
        private DelegateHeader _delegateHeader;

        public IAsyncResult BeginHeader(int messageOrdinal, AsyncCallback callback)
        {
            _delegateHeader = Header;
            return _delegateHeader.BeginInvoke(messageOrdinal, callback, _delegateHeader);
        }

        public byte[] EndHeader(IAsyncResult result)
        {
            return _delegateHeader.EndInvoke(result);
        }

        public byte[] UidHeader(int uid)
        {
            return Encoding.UTF8.GetBytes(UidHeaderString(uid));
        }

        private delegate byte[] DelegateUidHeader(int uid);
        private DelegateUidHeader _delegateUidHeader;

        public IAsyncResult BeginUidHeader(int uid, AsyncCallback callback)
        {
            _delegateUidHeader = UidHeader;
            return _delegateUidHeader.BeginInvoke(uid, callback, _delegateUidHeader);
        }

        public byte[] EndUidHeader(IAsyncResult result)
        {
            return _delegateUidHeader.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant header.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the Header to be fetched.</param>
        /// <returns>The message's Header as a Header object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// Header Header = inbox.Fetch.Header(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim Header As Header = inbox.Fetch.Header(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var header:Header = inbox.Fetch.Header(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public Header HeaderObject(int messageOrdinal)
        {
            return Parser.ParseHeader(Header(messageOrdinal));
        }

        private delegate Header DelegateHeaderObject(int messageOrdinal);
        private DelegateHeaderObject _delegateHeaderObject;

        public IAsyncResult BeginHeaderObject(int messageOrdinal, AsyncCallback callback)
        {
            _delegateHeaderObject = UidHeaderObject;
            return _delegateHeaderObject.BeginInvoke(messageOrdinal, callback, _delegateHeaderObject);
        }

        public Header EndHeaderObject(IAsyncResult result)
        {
            return _delegateHeaderObject.EndInvoke(result);
        }

        public Header UidHeaderObject(int uid)
        {
            return Parser.ParseHeader(UidHeader(uid));
        }

        private delegate Header DelegateUidHeaderObject(int uid);
        private DelegateUidHeaderObject _delegateUidHeaderObject;

        public IAsyncResult BeginUidHeaderObject(int uid, AsyncCallback callback)
        {
            _delegateUidHeaderObject = UidHeaderObject;
            return _delegateUidHeaderObject.BeginInvoke(uid, callback, _delegateUidHeaderObject);
        }

        public Header EndUidHeaderObject(IAsyncResult result)
        {
            return _delegateUidHeaderObject.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant Header (parsable by the Parsing namespace classes).
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the Header to be fetched.</param>
        /// <returns>The message's Header as a MemoryStream.</returns>
        /// <example><see cref="HeaderObject"/></example>
        public MemoryStream HeaderStream(int messageOrdinal)
        {
            byte[] buf = Header(messageOrdinal);
            return new MemoryStream(buf,0,buf.Length,false);
        }

        private delegate MemoryStream DelegateHeaderStream(int messageOrdinal);
        private DelegateHeaderStream _delegateHeaderStream;

        public IAsyncResult BeginHeaderStream(int messageOrdinal, AsyncCallback callback)
        {
            _delegateHeaderStream = HeaderStream;
            return _delegateHeaderStream.BeginInvoke(messageOrdinal, callback, _delegateHeaderStream);
        }

        public MemoryStream EndHeaderStream(IAsyncResult result)
        {
            return _delegateHeaderStream.EndInvoke(result);
        }

        public MemoryStream UidHeaderStream(int uid)
        {
            byte[] buf = UidHeader(uid);
            return new MemoryStream(buf,0,buf.Length,false);
        }

        private delegate MemoryStream DelegateUidHeaderStream(int uid);
        private DelegateUidHeaderStream _delegateUidHeaderStream;

        public IAsyncResult BeginUidHeaderStream(int uid, AsyncCallback callback)
        {
            _delegateUidHeaderStream = UidHeaderStream;
            return _delegateUidHeaderStream.BeginInvoke(uid, callback, _delegateUidHeaderStream);
        }

        public MemoryStream EndUidHeaderStream(IAsyncResult result)
        {
            return _delegateUidHeaderStream.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant Header (parsable by the Parsing namespace classes).
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the Header to be fetched.</param>
        /// <returns>The message's Header as a string.</returns>
        /// <example><see cref="HeaderObject"/></example>
        public string HeaderString(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            ParentMailbox.SourceClient.OnHeaderRetrieving(new HeaderRetrievingEventArgs(messageOrdinal));
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " rfc822.header", getFetchOptions());
            string header = response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
            ParentMailbox.SourceClient.OnHeaderRetrieved(new HeaderRetrievedEventArgs(Encoding.UTF8.GetBytes(header),messageOrdinal));
            return header;
        }

        private delegate string DelegateHeaderString(int messageOrdinal);
        private DelegateHeaderString _delegateHeaderString;

        public IAsyncResult BeginHeaderString(int messageOrdinal, AsyncCallback callback)
        {
            _delegateHeaderString = HeaderString;
            return _delegateHeaderString.BeginInvoke(messageOrdinal, callback, _delegateHeaderString);
        }

        public string EndHeaderString(IAsyncResult result)
        {
            return _delegateHeaderString.EndInvoke(result);
        }

        public string UidHeaderString(int uid)
        {
            var response = string.Empty;

            try
            {
                ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
                ParentMailbox.SourceClient.OnHeaderRetrieving(new HeaderRetrievingEventArgs(uid));
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.header", getFetchOptions());
                string header = response.Substring(response.IndexOf("}") + 3, response.LastIndexOf(")") - response.IndexOf("}") - 3);
                ParentMailbox.SourceClient.OnHeaderRetrieved(new HeaderRetrievedEventArgs(Encoding.UTF8.GetBytes(header), uid));
                return header;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving header. Response: " + response, ex);
            }
        }

        private delegate string DelegateUidHeaderString(int uid);
        private DelegateUidHeaderString _delegateUidHeaderString;

        public IAsyncResult BeginUidHeaderString(int uid, AsyncCallback callback)
        {
            _delegateUidHeaderString = UidHeaderString;
            return _delegateUidHeaderString.BeginInvoke(uid, callback, _delegateUidHeaderString);
        }

        public string EndUidHeaderString(IAsyncResult result)
        {
            return _delegateUidHeaderString.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the requested Header lines without setting the \Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="headerHeaders">An array of string representing the requested headers.</param>
        /// <returns>A NameValueCollection where Names are the Header delimiters and Values are the Header bodies.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// //Request the message's subject and from header.
        /// NameValueCollection lines = inbox.Fetch.HeaderLinesPeek(1,new string[] {"subject","from"});
        /// //Extract the subject.
        /// string messageSubject = lines["subject"];
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// 'Request the message's subject and from header.
        /// Dim lines As NameValueCollection = inbox.Fetch.HeaderLinesPeek(1,new string[] {"subject","from"})
        /// 'Extract the subject.
        /// Dim messageSubject As String = lines("subject")
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// //Request the message's subject and from header.
        /// var lines:NameValueCollection = inbox.Fetch.HeaderLinesPeek(1,new string[] {"subject","from"});
        /// //Extract the subject.
        /// var messageSubject:string = lines["subject"];
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public System.Collections.Specialized.NameValueCollection HeaderLinesPeek(int messageOrdinal, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.ToLower().IndexOf("imap4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " body.peek[header.fields " + delimiters + "]", getFetchOptions());
            else
                ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " rfc822.peek.header.GetStringRepresentation() " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateHeaderLinesPeek(int messageOrdinal, string[] headerHeaders);
        private DelegateHeaderLinesPeek _delegateHeaderLinesPeek;

        public IAsyncResult BeginHeaderLinesPeek(int messageOrdinal, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateHeaderLinesPeek = HeaderLinesPeek;
            return _delegateHeaderLinesPeek.BeginInvoke(messageOrdinal, headerHeaders, callback, _delegateHeaderLinesPeek);
        }

        public System.Collections.Specialized.NameValueCollection EndHeaderLinesPeek(IAsyncResult result)
        {
            return _delegateHeaderLinesPeek.EndInvoke(result);
        }

        public System.Collections.Specialized.NameValueCollection UidHeaderLinesPeek(int uid, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.ToLower().IndexOf("imap4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body.peek[header.fields " + delimiters + "]", getFetchOptions());
            else
                ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.peek.header.GetStringRepresentation() " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateUidHeaderLinesPeek(int uid, string[] headerHeaders);
        private DelegateUidHeaderLinesPeek _delegateUidHeaderLinesPeek;

        public IAsyncResult BeginUidHeaderLinesPeek(int uid, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateUidHeaderLinesPeek = UidHeaderLinesPeek;
            return _delegateUidHeaderLinesPeek.BeginInvoke(uid, headerHeaders, callback, _delegateUidHeaderLinesPeek);
        }

        public System.Collections.Specialized.NameValueCollection EndUidHeaderLinesPeek(IAsyncResult result)
        {
            return _delegateUidHeaderLinesPeek.EndInvoke(result);
        }

        /// <summary>
        /// Same as HeaderLines except that it will return all headers EXCEPT the specified ones, without setting the \Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="headerHeaders">An array of string representing the NOT-requested headers.</param>
        /// <returns>A NameValueCollection where Names are the Header delimiters and Values are the Header bodies.</returns>
        /// <example><see cref="HeaderLines"/></example>
        public System.Collections.Specialized.NameValueCollection HeaderLinesNotPeek(int messageOrdinal, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " body.peek[header.fields.not " + delimiters + "]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " rfc822.peek.header.GetStringRepresentation().not " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateHeaderLinesNotPeek(int messageOrdinal, string[] headerHeaders);
        private DelegateHeaderLinesNotPeek _delegateHeaderLinesNotPeek;

        public IAsyncResult BeginHeaderLinesNotPeek(int messageOrdinal, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateHeaderLinesNotPeek = HeaderLinesNotPeek;
            return _delegateHeaderLinesNotPeek.BeginInvoke(messageOrdinal, headerHeaders, callback, _delegateHeaderLinesNotPeek);
        }

        public System.Collections.Specialized.NameValueCollection EndHeaderLinesNotPeek(IAsyncResult result)
        {
            return _delegateHeaderLinesNotPeek.EndInvoke(result);
        }

        public System.Collections.Specialized.NameValueCollection UidHeaderLinesNotPeek(int uid, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body.peek[header.fields.not " + delimiters + "]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.peek.header.GetStringRepresentation().not " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateUidHeaderLinesNotPeek(int uid, string[] headerHeaders);
        private DelegateUidHeaderLinesNotPeek _delegateUidHeaderLinesNotPeek;

        public IAsyncResult BeginUidHeaderLinesNotPeek(int uid, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateUidHeaderLinesNotPeek = UidHeaderLinesNotPeek;
            return _delegateUidHeaderLinesNotPeek.BeginInvoke(uid, headerHeaders, callback, _delegateUidHeaderLinesNotPeek);
        }

        public System.Collections.Specialized.NameValueCollection EndUidHeaderLinesNotPeek(IAsyncResult result)
        {
            return _delegateUidHeaderLinesNotPeek.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the requested Header lines.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="headerHeaders">An array of string representing the requested headers.</param>
        /// <returns>A NameValueCollection where Names are the Header delimiters and Values are the Header bodies.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// //Request the message's subject and from header.
        /// NameValueCollection lines = inbox.Fetch.HeaderLines(1,new string[] {"subject","from"});
        /// //Extract the subject.
        /// string messageSubject = lines["subject"];
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// 'Request the message's subject and from header.
        /// Dim lines As NameValueCollection = inbox.Fetch.HeaderLines(1,new string[] {"subject","from"})
        /// 'Extract the subject.
        /// Dim messageSubject As String = lines("subject")
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// //Request the message's subject and from header.
        /// var lines:NameValueCollection = inbox.Fetch.HeaderLines(1,new string[] {"subject","from"});
        /// //Extract the subject.
        /// var messageSubject:string = lines["subject"];
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public System.Collections.Specialized.NameValueCollection HeaderLines(int messageOrdinal, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if(ParentMailbox.SourceClient.ServerCapabilities.ToLower().IndexOf("imap4rev1")!=-1)
                response = ParentMailbox.SourceClient.Command("fetch "+messageOrdinal.ToString()+" body[header.fields "+delimiters+"]", getFetchOptions());
            else
                ParentMailbox.SourceClient.Command("fetch "+messageOrdinal.ToString()+" rfc822.header.GetStringRepresentation() "+delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateHeaderLines(int messageOrdinal, string[] headerHeaders);
        private DelegateHeaderLines _delegateHeaderLines;

        public IAsyncResult BeginHeaderLines(int messageOrdinal, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateHeaderLines = HeaderLines;
            return _delegateHeaderLines.BeginInvoke(messageOrdinal, headerHeaders, callback, _delegateHeaderLines);
        }

        public System.Collections.Specialized.NameValueCollection EndHeaderLines(IAsyncResult result)
        {
            return _delegateHeaderLines.EndInvoke(result);
        }

        public System.Collections.Specialized.NameValueCollection UidHeaderLines(int uid, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.ToLower().IndexOf("imap4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body[header.fields " + delimiters + "]", getFetchOptions());
            else
                ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.header.GetStringRepresentation() " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateUidHeaderLines(int uid, string[] headerHeaders);
        private DelegateUidHeaderLines _delegateUidHeaderLines;

        public IAsyncResult BeginUidHeaderLines(int uid, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateUidHeaderLines = UidHeaderLines;
            return _delegateUidHeaderLines.BeginInvoke(uid, headerHeaders, callback, _delegateUidHeaderLines);
        }

        public System.Collections.Specialized.NameValueCollection EndUidHeaderLines(IAsyncResult result)
        {
            return _delegateUidHeaderLines.EndInvoke(result);
        }

        /// <summary>
        /// Same as HeaderLines except that it will return all headers EXCEPT the specified ones.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="headerHeaders">An array of string representing the NOT-requested headers.</param>
        /// <returns>A NameValueCollection where Names are the Header delimiters and Values are the Header bodies.</returns>
        /// <example><see cref="HeaderLines"/></example>
        public System.Collections.Specialized.NameValueCollection HeaderLinesNot(int messageOrdinal, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " body[header.fields.not " + delimiters + "]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " rfc822.header.GetStringRepresentation().not " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateHeaderLinesNot(int messageOrdinal, string[] headerHeaders);
        private DelegateHeaderLinesNot _delegateHeaderLinesNot;

        public IAsyncResult BeginHeaderLinesNot(int messageOrdinal, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateHeaderLinesNot = HeaderLinesNot;
            return _delegateHeaderLinesNot.BeginInvoke(messageOrdinal, headerHeaders, callback, _delegateHeaderLinesNot);
        }

        public System.Collections.Specialized.NameValueCollection EndHeaderLinesNot(IAsyncResult result)
        {
            return _delegateHeaderLinesNot.EndInvoke(result);
        }

        public System.Collections.Specialized.NameValueCollection UidHeaderLinesNot(int uid, string[] headerHeaders)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string delimiters = "(";
            foreach(string str in headerHeaders)
                delimiters += str+" ";
            delimiters = delimiters.Trim(' ')+")";
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body[header.fields.not " + delimiters + "]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.header.GetStringRepresentation().not " + delimiters, getFetchOptions());
            response = response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n\r\n")-response.IndexOf("}")-3);
            System.Collections.Specialized.NameValueCollection fieldcol = new System.Collections.Specialized.NameValueCollection();
            string[] fields = System.Text.RegularExpressions.Regex.Split(response,"\r\n");
            for(int i=0;i<fields.Length;i++)
                if (fields[i].IndexOf(":")!=-1)
                    fieldcol.Add(fields[i].Substring(0,fields[i].IndexOf(":")).ToLower().TrimEnd(':'),fields[i].Substring(fields[i].IndexOf(":")).TrimStart(new char[] {':',' '}));
            return fieldcol;
        }

        private delegate System.Collections.Specialized.NameValueCollection DelegateUidHeaderLinesNot(int uid, string[] headerHeaders);
        private DelegateUidHeaderLinesNot _delegateUidHeaderLinesNot;

        public IAsyncResult BeginUidHeaderLinesNot(int uid, string[] headerHeaders, AsyncCallback callback)
        {
            _delegateUidHeaderLinesNot = UidHeaderLinesNot;
            return _delegateUidHeaderLinesNot.BeginInvoke(uid, headerHeaders, callback, _delegateUidHeaderLinesNot);
        }

        public System.Collections.Specialized.NameValueCollection EndUidHeaderLinesNot(IAsyncResult result)
        {
            return _delegateUidHeaderLinesNot.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant form.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a byte array.</returns>
        /// <example><see cref="MessageObject"/></example>
        public byte[] Message(int messageOrdinal) {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            ParentMailbox.SourceClient.OnMessageRetrieving(new MessageRetrievingEventArgs(messageOrdinal));
            byte[] response = ParentMailbox.SourceClient.CommandBinary("fetch " + messageOrdinal.ToString() + " rfc822", getFetchOptions());
            _binaryResponse = response;
            _response = Encoding.UTF8.GetString(response);
            Logger.AddEntry(GetType(), _response);

            byte[] message = ExtractMessageFromReponse(response);

            ParentMailbox.SourceClient.OnMessageRetrieved(new MessageRetrievedEventArgs(message, messageOrdinal));
            return message;
        }

        private delegate byte[] DelegateMessage(int messageOrdinal);
        private DelegateMessage _delegateMessage;

        public IAsyncResult BeginMessage(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessage = Message;
            return _delegateMessage.BeginInvoke(messageOrdinal, callback, _delegateMessage);
        }

        public byte[] EndMessage(IAsyncResult result)
        {
            return _delegateMessage.EndInvoke(result);
        }


        public byte[] UidMessage(int uid)
        {
            return Encoding.UTF8.GetBytes(UidMessageString(uid));
        }

        private delegate byte[] DelegateUidMessage(int uid);
        private DelegateUidMessage _delegateUidMessage;

        public IAsyncResult BeginUidMessage(int uid, AsyncCallback callback)
        {
            _delegateUidMessage = UidMessage;
            return _delegateUidMessage.BeginInvoke(uid, callback, _delegateUidMessage);
        }

        public byte[] EndUidMessage(IAsyncResult result)
        {
            return _delegateUidMessage.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant form.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a Message object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// Message message = inbox.Fetch.Message(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim message As Message = inbox.Fetch.Message(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var message:Message = inbox.Fetch.Message(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public Message MessageObject(int messageOrdinal)
        {
            return Parser.ParseMessage(Message(messageOrdinal));
        }

        private delegate Message DelegateMessageObject(int messageOrdinal);
        private DelegateMessageObject _delegateMessageObject;

        public IAsyncResult BeginMessageObject(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessageObject = MessageObject;
            return _delegateMessageObject.BeginInvoke(messageOrdinal, callback, _delegateMessageObject);
        }

        public Message EndMessageObject(IAsyncResult result)
        {
            return _delegateMessageObject.EndInvoke(result);
        }

        public Message UidMessageObject(int uid)
        {
            return Parser.ParseMessage(UidMessage(uid));
        }

        private delegate Message DelegateUidMessageObject(int uid);
        private DelegateUidMessageObject _delegateUidMessageObject;

        public IAsyncResult BeginUidMessageObject(int uid, AsyncCallback callback)
        {
            _delegateUidMessageObject = UidMessageObject;
            return _delegateUidMessageObject.BeginInvoke(uid, callback, _delegateUidMessageObject);
        }

        public Message EndUidMessageObject(IAsyncResult result)
        {
            return _delegateUidMessageObject.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant form.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a MemoryStream.</returns>
        /// <example><see cref="MessageObject"/></example>
        public MemoryStream MessageStream(int messageOrdinal)
        {
            byte[] buf = Message(messageOrdinal);
            return new MemoryStream(buf,0,buf.Length,false);
        }

        private delegate MemoryStream DelegateMessageStream(int messageOrdinal);
        private DelegateMessageStream _delegateMessageStream;

        public IAsyncResult BeginMessageStream(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessageStream = MessageStream;
            return _delegateMessageStream.BeginInvoke(messageOrdinal, callback, _delegateMessageStream);
        }

        public MemoryStream EndMessageStream(IAsyncResult result)
        {
            return _delegateMessageStream.EndInvoke(result);
        }

        public MemoryStream UidMessageStream(int uid)
        {
            byte[] buf = UidMessage(uid);
            return new MemoryStream(buf,0,buf.Length,false);
        }

        private delegate MemoryStream DelegateUidMessageStream(int uid);
        private DelegateUidMessageStream _delegateUidMessageStream;

        public IAsyncResult BeginUidMessageStream(int uid, AsyncCallback callback)
        {
            _delegateUidMessageStream = UidMessageStream;
            return _delegateUidMessageStream.BeginInvoke(uid, callback, _delegateUidMessageStream);
        }

        public MemoryStream EndUidMessageStream(IAsyncResult result)
        {
            return _delegateUidMessageStream.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the message's Rfc822 compliant form.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a string.</returns>
        /// <example><see cref="MessageObject"/></example>
        public string MessageString(int messageOrdinal) 
        {
            return Encoding.UTF8.GetString(Message(messageOrdinal));
        }

        private static byte[] ExtractMessageFromReponse(byte[] response) 
        {
            byte[] message = null;
            string responseString = Encoding.ASCII.GetString(response);
            StringReader sr = new StringReader(responseString);
            string line = null;
            string firstline = null;

            while ((line = sr.ReadLine()) != null) {
                try {
                    int messageSize = Convert.ToInt32(line.Substring(line.IndexOf("{") + 1, line.IndexOf("}") - line.IndexOf("{") - 1));
                    if (messageSize >= response.Length)
                        throw new InvalidDataException(string.Format("defined message size of '{0}' is higher then size of response ({1})", messageSize, response.Length));
                    message = new byte[messageSize];
                    firstline = sr.ReadLine();
                    break;
                } catch (InvalidDataException e) {
                    throw new Exception("failed to extract message from response", e);
                } catch (Exception) {
                    continue;
                }
            }
            
            try {
                if (message == null || firstline == null)
                    throw new Exception("failed to determine messagesize");

                int prefixLength = Encoding.ASCII.GetByteCount(responseString.Substring(0, responseString.IndexOf(firstline)));
                Array.Copy(response, prefixLength, message, 0, message.Length);

                // Some (older) MS Exchange versions return a smaller message size, use old version as fallback if last character is not new line or the next line starts not with FLAGS
                string messageString = Encoding.ASCII.GetString(message);
                if (messageString.Substring(messageString.Length - 2, 2) != "\r\n" || !responseString.Substring(responseString.IndexOf(messageString) + Encoding.ASCII.GetString(message).Length).Trim().ToUpper().StartsWith("FLAGS")) {
                    int suffixLength = Encoding.ASCII.GetByteCount(responseString.Substring(responseString.LastIndexOf(")")));
                    message = new byte[response.Length - prefixLength - suffixLength];
                    Array.Copy(response, prefixLength, message, 0, message.Length);
                }                               

                return message;
            } catch (Exception e) {
                throw new Exception("failed to extract message from response", e);
            }
        }
       

        private delegate string DelegateMessageString(int messageOrdinal);
        private DelegateMessageString _delegateMessageString;

        public IAsyncResult BeginMessageString(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessageString = MessageString;
            return _delegateMessageString.BeginInvoke(messageOrdinal, callback, _delegateMessageString);
        }

        public string EndMessageString(IAsyncResult result)
        {
            return _delegateMessageString.EndInvoke(result);
        }

        public string UidMessageString(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            ParentMailbox.SourceClient.OnMessageRetrieving(new MessageRetrievingEventArgs(uid));
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822", getFetchOptions());
            string message = response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
            ParentMailbox.SourceClient.OnMessageRetrieved(new MessageRetrievedEventArgs(Encoding.UTF8.GetBytes(message),uid));
            return message;
        }

        private delegate string DelegateUidMessageString(int uid);
        private DelegateUidMessageString _delegateUidMessageString;

        public IAsyncResult BeginUidMessageString(int uid, AsyncCallback callback)
        {
            _delegateUidMessageString = UidMessageString;
            return _delegateUidMessageString.BeginInvoke(uid, callback, _delegateUidMessageString);
        }

        public string EndUidMessageString(IAsyncResult result)
        {
            return _delegateUidMessageString.EndInvoke(result);
        }

        /// <summary>
        /// Same as Message() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a byte array.</returns>
        /// <example><see cref="MessageObject"/></example>
        public byte[] MessagePeek(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            ParentMailbox.SourceClient.OnMessageRetrieving(new MessageRetrievingEventArgs(messageOrdinal));
            byte[] response;
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.CommandBinary("fetch " + messageOrdinal.ToString() + " body[mime]", getFetchOptions());                
            else
                response = ParentMailbox.SourceClient.CommandBinary("fetch " + messageOrdinal.ToString() + " rfc822.peek", getFetchOptions());
            _binaryResponse = response;
            _response = Encoding.UTF8.GetString(response);
            
            byte[] message = ExtractMessageFromReponse(response);
            ParentMailbox.SourceClient.OnMessageRetrieved(new MessageRetrievedEventArgs(message, messageOrdinal));
            return message;
        }

        private delegate byte[] DelegateMessagePeek(int messageOrdinal);
        private DelegateMessagePeek _delegateMessagePeek;

        public IAsyncResult BeginMessagePeek(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessagePeek = MessagePeek;
            return _delegateMessagePeek.BeginInvoke(messageOrdinal, callback, _delegateMessagePeek);
        }

        public byte[] EndMessagePeek(IAsyncResult result)
        {
            return _delegateMessagePeek.EndInvoke(result);
        }


        public byte[] UidMessagePeek(int uid)
        {
            return Encoding.UTF8.GetBytes(UidMessageStringPeek(uid));
        }

        private delegate byte[] DelegateUidMessagePeek(int uid);
        private DelegateUidMessagePeek _delegateUidMessagePeek;

        public IAsyncResult BeginUidMessagePeek(int uid, AsyncCallback callback)
        {
            _delegateUidMessagePeek = UidMessagePeek;
            return _delegateUidMessagePeek.BeginInvoke(uid, callback, _delegateUidMessagePeek);
        }

        public byte[] EndUidMessagePeek(IAsyncResult result)
        {
            return _delegateUidMessagePeek.EndInvoke(result);
        }

        /// <summary>
        /// Same as MessageObject() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a Message object.</returns>
        /// <example><see cref="MessageObject"/></example>
        public Message MessageObjectPeek(int messageOrdinal)
        {
            return Parser.ParseMessage(MessagePeek(messageOrdinal));
        }

        private delegate Message DelegateMessageObjectPeek(int messageOrdinal);
        private DelegateMessageObjectPeek _delegateMessageObjectPeek;

        public IAsyncResult BeginMessageObjectPeek(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessageObjectPeek = MessageObjectPeek;
            return _delegateMessageObjectPeek.BeginInvoke(messageOrdinal, callback, _delegateMessageObjectPeek);
        }

        public Message EndMessageObjectPeek(IAsyncResult result)
        {
            return _delegateMessageObjectPeek.EndInvoke(result);
        }

        public Message UidMessageObjectPeek(int uid)
        {
            return Parser.ParseMessage(UidMessagePeek(uid));
        }

        private delegate Message DelegateUidMessageObjectPeek(int uid);
        private DelegateUidMessageObjectPeek _delegateUidMessageObjectPeek;

        public IAsyncResult BeginUidMessageObjectPeek(int uid, AsyncCallback callback)
        {
            _delegateUidMessageObjectPeek = UidMessageObjectPeek;
            return _delegateUidMessageObjectPeek.BeginInvoke(uid, callback, _delegateUidMessageObjectPeek);
        }

        public Message EndUidMessageObjectPeek(IAsyncResult result)
        {
            return _delegateUidMessageObjectPeek.EndInvoke(result);
        }

        /// <summary>
        /// Same as MessageStream() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a MemoryStream.</returns>
        /// <example><see cref="MessageStream"/></example>
        public MemoryStream MessageSreamPeek(int messageOrdinal)
        {
            byte[] buf = MessagePeek(messageOrdinal);
            return new MemoryStream(buf,0,buf.Length,false);
        }

        private delegate MemoryStream DelegateMessageStreamPeek(int messageOrdinal);
        private DelegateMessageStreamPeek _delegateMessageStreamPeek;

        public IAsyncResult BeginMessageStreamPeek(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessageStreamPeek = MessageSreamPeek;
            return _delegateMessageStreamPeek.BeginInvoke(messageOrdinal, callback, _delegateMessageStreamPeek);
        }

        public MemoryStream EndMessageStreamPeek(IAsyncResult result)
        {
            return _delegateMessageStreamPeek.EndInvoke(result);
        }

        public MemoryStream UidMessageStreamPeek(int uid)
        {
            byte[] buf = UidMessagePeek(uid);
            return new MemoryStream(buf,0,buf.Length,false);
        }

        private delegate MemoryStream DelegateUidMessageStreamPeek(int uid);
        private DelegateUidMessageStreamPeek _delegateUidMessageStreamPeek;

        public IAsyncResult BeginUidMessageStreamPeek(int uid, AsyncCallback callback)
        {
            _delegateUidMessageStreamPeek = UidMessageStreamPeek;
            return _delegateUidMessageStreamPeek.BeginInvoke(uid, callback, _delegateUidMessageStreamPeek);
        }

        public MemoryStream EndUidMessageStreamPeek(IAsyncResult result)
        {
            return _delegateUidMessageStreamPeek.EndInvoke(result);
        }

        /// <summary>
        /// Same as MessageString() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's data as a string.</returns>
        /// <example><see cref="MessageString"/></example>
        public string MessageStringPeek(int messageOrdinal) {
            return Encoding.UTF8.GetString(MessagePeek(messageOrdinal));
        }

        private delegate string DelegateMessageStringPeek(int messageOrdinal);
        private DelegateMessageStringPeek _delegateMessageStringPeek;

        public IAsyncResult BeginMessageStringPeek(int messageOrdinal, AsyncCallback callback)
        {
            _delegateMessageStringPeek = MessageStringPeek;
            return _delegateMessageStringPeek.BeginInvoke(messageOrdinal, callback, _delegateMessageStringPeek);
        }

        public string EndMessageStringPeek(IAsyncResult result)
        {
            return _delegateMessageStringPeek.EndInvoke(result);
        }

        public string UidMessageStringPeek(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            ParentMailbox.SourceClient.OnMessageRetrieving(new MessageRetrievingEventArgs(uid));
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body[mime]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("fetch " + uid.ToString() + " rfc822.peek", getFetchOptions());
            string message = response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
            ParentMailbox.SourceClient.OnMessageRetrieved(new MessageRetrievedEventArgs(Encoding.UTF8.GetBytes(message),uid));
            return message;
        }

        private delegate string DelegateUidMessageStringPeek(int uid);
        private DelegateUidMessageStringPeek _delegateUidMessageStringPeek;

        public IAsyncResult BeginUidMessageStringPeek(int uid, AsyncCallback callback)
        {
            _delegateUidMessageStringPeek = UidMessageStringPeek;
            return _delegateUidMessageStringPeek.BeginInvoke(uid, callback, _delegateUidMessageStringPeek);
        }

        public string EndUidMessageStringPeek(IAsyncResult result)
        {
            return _delegateUidMessageStringPeek.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the specified message's size (in bytes).
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's size in bytes.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// int size = inbox.Fetch.Size(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim size As Integer = inbox.Fetch.Size(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var size:int = inbox.Fetch.Size(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public int Size(int messageOrdinal)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " rfc822.size", getFetchOptions());
            return Convert.ToInt32(response.Substring(response.ToLower().IndexOf("rfc822.size")+12).Split(')')[0]);
        }

        private delegate int DelegateSize(int messageOrdinal);
        private DelegateSize _delegateSize;

        public IAsyncResult BeginSize(int messageOrdinal, AsyncCallback callback)
        {
            _delegateSize = Size;
            return _delegateSize.BeginInvoke(messageOrdinal, callback, _delegateSize);
        }

        public int EndSize(IAsyncResult result)
        {
            return _delegateSize.EndInvoke(result);
        }

        public int UidSize(int uid)
        {
            ParentMailbox.SourceClient.SelectMailbox(ParentMailbox.Name);
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.size", getFetchOptions());
            return Convert.ToInt32(response.Substring(response.ToLower().IndexOf("rfc822.size")+12).Split(')')[0]);
        }

        private delegate int DelegateUidSize(int messageOrdinal);
        private DelegateUidSize _delegateUidSize;

        public IAsyncResult BeginUidSize(int messageOrdinal, AsyncCallback callback)
        {
            _delegateUidSize = UidSize;
            return _delegateUidSize.BeginInvoke(messageOrdinal, callback, _delegateUidSize);
        }

        public int EndUidSize(IAsyncResult result)
        {
            return _delegateUidSize.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the specified message's text (body).
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's text.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// string messageBody = inbox.Fetch.Text(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim messageBody As Header = inbox.Fetch.Text(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var messageBody:string = inbox.Fetch.Text(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Text(int messageOrdinal)
        {
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " rfc822.text", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
        }

        private delegate string DelegateText(int messageOrdinal);
        private DelegateText _delegateText;

        public IAsyncResult BeginText(int messageOrdinal, AsyncCallback callback)
        {
            _delegateText = Text;
            return _delegateText.BeginInvoke(messageOrdinal, callback, _delegateText);
        }

        public string EndText(IAsyncResult result)
        {
            return _delegateText.EndInvoke(result);
        }

        public string UidText(int uid)
        {
            string response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " rfc822.text", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
        }

        private delegate string DelegateUidText(int uid);
        private DelegateUidText _delegateUidText;

        public IAsyncResult BeginUidText(int uid, AsyncCallback callback)
        {
            _delegateUidText = UidText;
            return _delegateUidText.BeginInvoke(uid, callback, _delegateUidText);
        }

        public string EndUidText(IAsyncResult result)
        {
            return _delegateUidText.EndInvoke(result);
        }

        /// <summary>
        /// Same as Text() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's text.</returns>
        /// <example><see cref="Text"/></example>
        public string TextPeek(int messageOrdinal)
        {
            string response = "";
            if(ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1")!=-1)
                response = ParentMailbox.SourceClient.Command("fetch "+messageOrdinal.ToString()+" body[text]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("fetch "+messageOrdinal.ToString()+" rfc822.text.peek", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
        }

        private delegate string DelegateTextPeek(int messageOrdinal);
        private DelegateTextPeek _delegateTextPeek;

        public IAsyncResult BeginTextPeek(int messageOrdinal, AsyncCallback callback)
        {
            _delegateTextPeek = TextPeek;
            return _delegateTextPeek.BeginInvoke(messageOrdinal, callback, _delegateTextPeek);
        }

        public string EndTextPeek(IAsyncResult result)
        {
            return _delegateTextPeek.EndInvoke(result);
        }

        public string UidTextPeek(int uid)
        {
            string response = "";
            if (ParentMailbox.SourceClient.ServerCapabilities.IndexOf("IMAP4rev1") != -1)
                response = ParentMailbox.SourceClient.Command("uid fetch " + uid.ToString() + " body[text]", getFetchOptions());
            else
                response = ParentMailbox.SourceClient.Command("uid fetch "+uid.ToString()+" rfc822.text.peek", getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf(")")-response.IndexOf("}")-3);
        }

        private delegate string DelegateUidTextPeek(int messageOrdinal);
        private DelegateUidTextPeek _delegateUidTextPeek;

        public IAsyncResult BeginUidTextPeek(int messageOrdinal, AsyncCallback callback)
        {
            _delegateUidTextPeek = UidTextPeek;
            return _delegateUidTextPeek.BeginInvoke(messageOrdinal, callback, _delegateUidTextPeek);
        }

        public string EndUidTextPeek(IAsyncResult result)
        {
            return _delegateUidTextPeek.EndInvoke(result);
        }

        /// <summary>
        /// Fetches the specified message's Unique Identifier number.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <returns>The message's unique identifier number.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// int uid = inbox.Fetch.Uid(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// Dim uid As Integer = inbox.Fetch.Uid(1);
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// var uid:int = inbox.Fetch.Uid(1);
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public int Uid(int messageOrdinal)
        {
            string response = ParentMailbox.SourceClient.Command("fetch " + messageOrdinal.ToString() + " uid", getFetchOptions());
            return Convert.ToInt32(response.Substring(response.ToLower().IndexOf("uid")+3).Split(')')[0]);
        }

        private delegate int DelegateUid(int messageOrdinal);
        private DelegateUid _delegateUid;

        public IAsyncResult BeginUid(int messageOrdinal, AsyncCallback callback)
        {
            _delegateUid = Uid;
            return _delegateUid.BeginInvoke(messageOrdinal, callback, _delegateUid);
        }

        public int EndUid(IAsyncResult result)
        {
            return _delegateUid.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of BodySection().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="section">The message's body section to be retrieved.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array.</returns>
        public byte[] PartialBodySection(int messageOrdinal, int section, int index, int count)
        {
            return Encoding.UTF8.GetBytes(PartialBodySectionString(messageOrdinal,section,index,count));
        }

        private delegate byte[] DelegatePartialBodySection(int messageOrdinal, int section, int index, int count);
        private DelegatePartialBodySection _delegatePartialBodySection;

        public IAsyncResult BeginPartialBodySection(int messageOrdinal, int section, int index, int count, AsyncCallback callback)
        {
            _delegatePartialBodySection = PartialBodySection;
            return _delegatePartialBodySection.BeginInvoke(messageOrdinal, section, index, count, callback, _delegatePartialBodySection);
        }

        public byte[] EndPartialBodySection(IAsyncResult result)
        {
            return _delegatePartialBodySection.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of Header().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array.</returns>
        public byte[] PartialHeader(int messageOrdinal, int index, int count)
        {
            return Encoding.UTF8.GetBytes(PartialHeaderString(messageOrdinal,index,count));
        }

        private delegate byte[] DelegatePartialHeader(int messageOrdinal, int index, int count);
        private DelegatePartialHeader _delegatePartialHeader;

        public IAsyncResult BeginPartialHeader(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialHeader = PartialHeader;
            return _delegatePartialHeader.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialHeader);
        }

        public byte[] EndPartialHeader(IAsyncResult result)
        {
            return _delegatePartialHeader.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of Message().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array.</returns>
        public byte[] PartialMessage(int messageOrdinal, int index, int count)
        {
            return Encoding.UTF8.GetBytes(PartialMessageString(messageOrdinal,index,count));
        }

        private delegate byte[] DelegatePartialMessage(int messageOrdinal, int index, int count);
        private DelegatePartialMessage _delegatePartialMessage;

        public IAsyncResult BeginPartialMessage(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialMessage = PartialMessage;
            return _delegatePartialMessage.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialMessage);
        }

        public byte[] EndPartialMessage(IAsyncResult result)
        {
            return _delegatePartialMessage.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of MessagePeek().
        /// Same as PartialMessage() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array.</returns>
        public byte[] PartialMessagePeek(int messageOrdinal, int index, int count)
        {
            return Encoding.UTF8.GetBytes(PartialMessageStringPeek(messageOrdinal, index, count));
        }

        private delegate byte[] DelegatePartialMessagePeek(int messageOrdinal, int index, int count);
        private DelegatePartialMessagePeek _delegatePartialMessagePeek;

        public IAsyncResult BeginPartialMessagePeek(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialMessagePeek = PartialMessagePeek;
            return _delegatePartialMessagePeek.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialMessagePeek);
        }

        public byte[] EndPartialMessagePeek(IAsyncResult result)
        {
            return _delegatePartialMessagePeek.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of Text().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array.</returns>
        public byte[] PartialText(int messageOrdinal, int index, int count)
        {
            return Encoding.UTF8.GetBytes(PartialTextString(messageOrdinal,index,count));
        }

        private delegate byte[] DelegatePartialText(int messageOrdinal, int index, int count);
        private DelegatePartialText _delegatePartialText;

        public IAsyncResult BeginPartialText(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialText = PartialText;
            return _delegatePartialText.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialText);
        }

        public byte[] EndPartialText(IAsyncResult result)
        {
            return _delegatePartialText.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of TextPeek().
        /// Same as PartialText() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array.</returns>
        public byte[] PartialTextPeek(int messageOrdinal, int index, int count)
        {
            return Encoding.UTF8.GetBytes(PartialTextStringPeek(messageOrdinal, index, count));
        }

        private delegate byte[] DelegatePartialTextPeek(int messageOrdinal, int index, int count);
        private DelegatePartialTextPeek _delegatePartialTextPeek;

        public IAsyncResult BeginPartialTextPeek(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialTextPeek = PartialTextPeek;
            return _delegatePartialTextPeek.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialTextPeek);
        }

        public byte[] EndPartialTextPeek(IAsyncResult result)
        {
            return _delegatePartialTextPeek.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of BodySectionString().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array converted to a string.</returns>
        public string PartialBodySectionString(int messageOrdinal, int section, int index, int count)
        {
            string response = ParentMailbox.SourceClient.Command("partial " + messageOrdinal.ToString() + " body[" + section + "] " + index.ToString() + " " + count.ToString(), getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n")-response.IndexOf("}")-3);
        }

        private delegate string DelegatePartialBodySectionString(int messageOrdinal, int section, int index, int count);
        private DelegatePartialBodySectionString _delegatePartialBodySectionString;

        public IAsyncResult BeginPartialBodySectionString(int messageOrdinal, int section, int index, int count, AsyncCallback callback)
        {
            _delegatePartialBodySectionString = PartialBodySectionString;
            return _delegatePartialBodySectionString.BeginInvoke(messageOrdinal, section, index, count, callback, _delegatePartialBodySectionString);
        }

        public string EndPartialBodySectionString(IAsyncResult result)
        {
            return _delegatePartialBodySectionString.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of HeaderString().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array converted to a string.</returns>
        public string PartialHeaderString(int messageOrdinal, int index, int count)
        {
            string response = ParentMailbox.SourceClient.Command("partial " + messageOrdinal.ToString() + " rfc822.Header " + index.ToString() + " " + count.ToString(), getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n")-response.IndexOf("}")-3);
        }

        private delegate string DelegatePartialHeaderString(int messageOrdinal, int index, int count);
        private DelegatePartialHeaderString _delegatePartialHeaderString;

        public IAsyncResult BeginPartialHeaderString(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialHeaderString = PartialHeaderString;
            return _delegatePartialHeaderString.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialHeaderString);
        }

        public string EndPartialHeaderString(IAsyncResult result)
        {
            return _delegatePartialHeaderString.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of MessageString().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array converted to a string.</returns>
        public string PartialMessageString(int messageOrdinal, int index, int count)
        {
            string response = ParentMailbox.SourceClient.Command("partial " + messageOrdinal.ToString() + " rfc822 " + index.ToString() + " " + count.ToString(), getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n")-response.IndexOf("}")-3);
        }

        private delegate string DelegatePartialMessageString(int messageOrdinal, int index, int count);
        private DelegatePartialMessageString _delegatePartialMessageString;

        public IAsyncResult BeginPartialMessageString(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialMessageString = PartialMessageString;
            return _delegatePartialMessageString.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialMessageString);
        }

        public string EndPartialMessageString(IAsyncResult result)
        {
            return _delegatePartialMessageString.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of MessagePeekString().
        /// Same as PartialMessageString() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array converted to a string.</returns>
        public string PartialMessageStringPeek(int messageOrdinal, int index, int count)
        {
            string response = ParentMailbox.SourceClient.Command("partial " + messageOrdinal.ToString() + " rfc822.peek " + index.ToString() + " " + count.ToString(), getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n")-response.IndexOf("}")-3);
        }

        private delegate string DelegatePartialMessageStringPeek(int messageOrdinal, int index, int count);
        private DelegatePartialMessageStringPeek _delegatePartialMessageStringPeek;

        public IAsyncResult BeginPartialMessageStringPeek(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialMessageStringPeek = PartialMessageStringPeek;
            return _delegatePartialMessageStringPeek.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialMessageStringPeek);
        }

        public string EndPartialMessageStringPeek(IAsyncResult result)
        {
            return _delegatePartialMessageStringPeek.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of TextString().
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array converted to a string.</returns>
        public string PartialTextString(int messageOrdinal, int index, int count)
        {
            string response = ParentMailbox.SourceClient.Command("partial " + messageOrdinal.ToString() + " rfc822.text " + index.ToString() + " " + count.ToString(), getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n")-response.IndexOf("}")-3);
        }

        private delegate string DelegatePartialTextString(int messageOrdinal, int index, int count);
        private DelegatePartialTextString _delegatePartialTextString;

        public IAsyncResult BeginPartialTextString(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialTextString = PartialTextString;
            return _delegatePartialTextString.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialTextString);
        }

        public string EndPartialTextString(IAsyncResult result)
        {
            return _delegatePartialTextString.EndInvoke(result);
        }

        /// <summary>
        /// Fetches [count] bytes starting at [index].
        /// Partial version of TextPeekString().
        /// Same as PartialTextString() except that it doesn't set the Seen flag.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal position of the message to be fetched.</param>
        /// <param name="index">The byte index to start retrieving from.</param>
        /// <param name="count">The amount of bytes to be retrieved, starting at index.</param>
        /// <returns>The requested byte array converted to a string.</returns>
        public string PartialTextStringPeek(int messageOrdinal, int index, int count)
        {
            string response = ParentMailbox.SourceClient.Command("partial " + messageOrdinal.ToString() + " rfc822.text.peek " + index.ToString() + " " + count.ToString(), getFetchOptions());
            return response.Substring(response.IndexOf("}")+3,response.LastIndexOf("\r\n")-response.IndexOf("}")-3);
        }

        private delegate string DelegatePartialTextStringPeek(int messageOrdinal, int index, int count);
        private DelegatePartialTextStringPeek _delegatePartialTextStringPeek;

        public IAsyncResult BeginPartialTextStringPeek(int messageOrdinal, int index, int count, AsyncCallback callback)
        {
            _delegatePartialTextStringPeek = PartialTextStringPeek;
            return _delegatePartialTextStringPeek.BeginInvoke(messageOrdinal, index, count, callback, _delegatePartialTextStringPeek);
        }

        public string EndPartialTextStringPeek(IAsyncResult result)
        {
            return _delegatePartialTextStringPeek.EndInvoke(result);
        }

        #endregion

        #endregion

        #region Properties
        /// <summary>
        /// The complete mail server response
        /// </summary>
        public string Response {
            get { return _response; }
        }

        /// <summary>
        /// The complete mail server response (binary)
        /// </summary>
        public byte[] BinaryResponse {
            get { return _binaryResponse; }
        }

        /// <summary>
        /// The Fetch's parent mailbox.
        /// </summary>
        public Mailbox ParentMailbox { get; set; }
    
        #endregion
    }
}