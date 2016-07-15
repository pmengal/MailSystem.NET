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
using ActiveUp.Net.Security;
using System.Net.Sockets;
using System.Net.Security;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// This class allows communication with an IMAP4 or IMAP4rev1 compatible server.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class Imap4Client : TcpClient
    {
        static Imap4Client()
        {
            _badCommandStrings = new[] {
                "\\", //Important this comes first
                "\""
            };
        }

        #region Private fields

        static string[] _badCommandStrings;

        private string host;
#if !PocketPC
        SslStream _sslStream;
#endif
        private bool _idleInProgress = false;

        #endregion

        #region Properties

        /// <summary>
        /// Mailboxes on the account.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// int mailboxCount = imap.Mailboxes.Count;
        /// //User jdoe1234 has mailboxCount mailboxes.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim mailboxCount As Integer = imap.Mailboxes.Count
        /// //User jdoe1234 has mailboxCount mailboxes.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var mailboxCount:int = imap.Mailboxes.Count;
        /// //User jdoe1234 has mailboxCount mailboxes.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public MailboxCollection Mailboxes { get; set; } = new MailboxCollection();

        /// <summary>
        /// Same as the Mailboxes property, except that all mailboxes on the account are presented at the same level of hierarchy.
        /// In example, a child mailbox of the "INBOX" mailbox could be accessed directly with this collection.
        /// </summary>
        public MailboxCollection AllMailboxes { get; set; } = new MailboxCollection();

        /// <summary>
        /// Server capabilities.
        /// </summary>
        public string ServerCapabilities { get; set; }

        /// <summary>
        /// Turn this on to not make any parameters safe.  Injection attacks more likely.  Turn this on only if you are already doing checking or if performance is absolutely critical.  
        /// </summary>
        public bool IsUnsafeParamsAllowed { get; set; }

        #endregion

        #region Delegates and associated private fields

        private delegate string DelegateConnect(string host, int port);
        private DelegateConnect _delegateConnect;

        private delegate string DelegateConnectAuth(string host, int port, string username, string password);
        private DelegateConnectAuth _delegateConnectAuth;

        private delegate string DelegateConnectIPAddress(System.Net.IPAddress addr, int port);
        private DelegateConnectIPAddress _delegateConnectIPAddress;

        private delegate string DelegateConnectIPAddresses(System.Net.IPAddress[] addresses, int port);
        private DelegateConnectIPAddresses _delegateConnectIPAddresses;

#if !PocketPC
        private delegate string DelegateConnectSsl(string host, int port, SslHandShake sslHandShake);
        private DelegateConnectSsl _delegateConnectSsl;

        private delegate string DelegateConnectSslIPAddress(System.Net.IPAddress addr, int port, SslHandShake sslHandShake);
        private DelegateConnectSslIPAddress _delegateConnectSslIPAddress;

        private delegate string DelegateConnectSslIPAddresses(System.Net.IPAddress[] addresses, int port, SslHandShake sslHandShake);
        private DelegateConnectSslIPAddresses _delegateConnectSslIPAddresses;
#endif

        private delegate string DelegateDisconnect();
        private DelegateDisconnect _delegateDisconnect;

        private delegate string DelegateAuthenticate(string username, string password, SaslMechanism mechanism);
        private DelegateAuthenticate _delegateAuthenticate;

        private delegate string DelegateLogin(string username, string password);
        private DelegateLogin _delegateLogin;

        private delegate string DelegateCommand(string command, string stamp, CommandOptions options);
        private DelegateCommand _delegateCommand;

        private delegate string DelegateCommandStringStringString(string command, string stamp, string checkStamp, CommandOptions options);
        private DelegateCommandStringStringString _delegateCommandStringStringString;

        private delegate string DelegateNoop();
        private DelegateNoop _delegateNoop;

        private delegate string DelegateCheck();
        private DelegateCheck _delegateCheck;

        private delegate string DelegateClose();
        private DelegateClose _delegateClose;

        private delegate void DelegateExpunge();
        private DelegateExpunge _delegateExpunge;

        private delegate void DelegateLoadMailboxes();
        private DelegateLoadMailboxes _delegateLoadMailboxes;

        private delegate Mailbox DelegateMailboxOperation(string mailboxName);
        private DelegateMailboxOperation _delegateMailboxOperation;

        private delegate string DelegateRenameMailbox(string oldMailboxName, string newMailboxName);
        private DelegateRenameMailbox _delegateRenameMailbox;

        private delegate string DelegateMailboxOperationReturnsString(string mailboxName);
        private DelegateMailboxOperationReturnsString _delegateMailboxOperationReturnsString;

        private delegate MailboxCollection DelegateGetMailboxes(string reference, string mailboxName);
        private DelegateGetMailboxes _delegateGetMailboxes;

        #endregion

        #region Events

        #region Event definitions

        /// <summary>
        /// Event fired when authentication starts.
        /// </summary>
        public event AuthenticatingEventHandler Authenticating;
        /// <summary>
        /// Event fired when authentication completed.
        /// </summary>
        public event AuthenticatedEventHandler Authenticated;
        /// <summary>
        /// Event fired when NOOP command is issued.
        /// </summary>
        public event NoopingEventHandler Nooping;
        /// <summary>
        /// Event fired when NOOP command completed.
        /// </summary>
        public event NoopedEventHandler Nooped;
        /// <summary>
        /// Event fired when a command is being written to the server.
        /// </summary>
        public event TcpWritingEventHandler TcpWriting;
        /// <summary>
        /// Event fired when a command has been written to the server.
        /// </summary>
        public event TcpWrittenEventHandler TcpWritten;
        /// <summary>
        /// Event fired when a response is being read from the server.
        /// </summary>
        public event TcpReadingEventHandler TcpReading;
        /// <summary>
        /// Event fired when a response has been read from the server.
        /// </summary>
        public event TcpReadEventHandler TcpRead;
        /// <summary>
        /// Event fired when a message is being requested using the RetrieveMessage() method.
        /// </summary>
        public event MessageRetrievingEventHandler MessageRetrieving;
        /// <summary>
        /// Event fired when a message is being retrieved using the RetrieveMessage() method.
        /// </summary>
        public event MessageRetrievedEventHandler MessageRetrieved;
        /// <summary>
        /// Event fired when a message Header is being requested using the RetrieveHeader() method.
        /// </summary>
        public event HeaderRetrievingEventHandler HeaderRetrieving;
        /// <summary>
        /// Event fired when a message Header has been retrieved using the RetrieveHeader() method.
        /// </summary>
        public event HeaderRetrievedEventHandler HeaderRetrieved;
        /// <summary>
        /// Event fired when attempting to connect to the remote server using the specified host.
        /// </summary>
        public event ConnectingEventHandler Connecting;
        /// <summary>
        /// Event fired when the object is connected to the remote server or when connection failed.
        /// </summary>
        public new event ConnectedEventHandler Connected;
        /// <summary>
        /// Event fired when attempting to disconnect from the remote server.
        /// </summary>
        public event DisconnectingEventHandler Disconnecting;
        /// <summary>
        /// Event fired when the object disconnected from the remote server.
        /// </summary>
        public event DisconnectedEventHandler Disconnected;
        /// <summary>
        /// Event fired when a message is being sent.
        /// </summary>
        public event MessageSendingEventHandler MessageSending;
        /// <summary>
        /// Event fired when a message has been sent.
        /// </summary>
        public event MessageSentEventHandler MessageSent;
        /// <summary>
        /// Event fired when a new message received.
        /// </summary>
        public event NewMessageReceivedEventHandler NewMessageReceived;

        #endregion

        #region Event triggers and logging

        internal void OnAuthenticating(AuthenticatingEventArgs e)
        {
            if (Authenticating != null)
                Authenticating(this, e);
            Logger.AddEntry(GetType(), "Authenticating as <" + e.Username + "> on <" + e.Host + ">...", 2);
        }
        internal void OnAuthenticated(AuthenticatedEventArgs e)
        {
            if (Authenticated != null)
                Authenticated(this, e);
            Logger.AddEntry(GetType(), "Authenticated as <" + e.Username + "> on <" + e.Host + ">.", 2);
        }
        internal void OnNooping()
        {
            if (Nooping != null)
                Nooping(this);
            Logger.AddEntry(GetType(), "Nooping...", 1);
        }
        internal void OnNooped()
        {
            if (Nooped != null)
                Nooped(this);
            Logger.AddEntry(GetType(), "Nooped.", 1);
        }
        internal void OnTcpWriting(TcpWritingEventArgs e)
        {
            if (TcpWriting != null)
                TcpWriting(this, e);
            Logger.AddEntry(GetType(), "Sending <" + e.Command.Trim() + ">", 1);
        }
        internal void OnTcpWritten(TcpWrittenEventArgs e)
        {
            if (TcpWritten != null)
                TcpWritten(this, e);
            Logger.AddEntry(GetType(), "Sent <" + e.Command.Trim() + ">", 1);
        }
        internal void OnTcpReading()
        {
            if (TcpReading != null)
                TcpReading(this);
            Logger.AddEntry(GetType(), "Reading...", 1);
        }
        internal void OnTcpRead(TcpReadEventArgs e)
        {
            if (TcpRead != null)
                TcpRead(this, e);
            Logger.AddEntry(GetType(), "Read <" + e.Response.Trim() + ">", 1);
        }
        internal void OnMessageRetrieving(MessageRetrievingEventArgs e)
        {
            if (MessageRetrieving != null)
                MessageRetrieving(this, e);
            Logger.AddEntry(GetType(), "Retrieving message at index <" + e.MessageIndex + ">...", 2);
        }
        internal void OnMessageRetrieved(MessageRetrievedEventArgs e)
        {
            if (MessageRetrieved != null)
                MessageRetrieved(this, e);
            Logger.AddEntry(GetType(), "Retrieved message at index <" + e.MessageIndex + ">.", 2);
        }
        internal void OnHeaderRetrieving(HeaderRetrievingEventArgs e)
        {
            if (HeaderRetrieving != null)
                HeaderRetrieving(this, e);
            Logger.AddEntry(GetType(), "Retrieving Header at index <" + e.MessageIndex + ">...", 2);
        }
        internal void OnHeaderRetrieved(HeaderRetrievedEventArgs e)
        {
            if (HeaderRetrieved != null)
                HeaderRetrieved(this, e);
            Logger.AddEntry(GetType(), "Retrieved Header at index <" + e.MessageIndex + ">.", 2);
        }
        internal void OnDisconnecting()
        {
            if (Disconnecting != null)
                Disconnecting(this);
            Logger.AddEntry(GetType(), "Disconnecting...", 2);
        }
        internal void OnDisconnected(DisconnectedEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
            Logger.AddEntry(GetType(), "Disconnected.", 2);
        }
        internal void OnConnecting()
        {
            if (Connecting != null)
                Connecting(this);
            Logger.AddEntry(GetType(), "Connecting...", 2);
        }
        internal void OnConnected(ConnectedEventArgs e)
        {
            if (Connected != null)
                Connected(this, e);
            Logger.AddEntry(GetType(), "Connected. Server replied <" + e.ServerResponse.Trim() + ">.", 2);
        }
        internal void OnMessageSending(MessageSendingEventArgs e)
        {
            if (MessageSending != null)
                MessageSending(this, e);
            Logger.AddEntry(GetType(), "Sending message with subject: <" + e.Message.Subject + ">...", 2);
        }
        internal void OnMessageSent(MessageSentEventArgs e)
        {
            if (MessageSent != null)
                MessageSent(this, e);
            Logger.AddEntry(GetType(), "Sent message with subject: <" + e.Message.Subject + ">.", 2);
        }
        internal void OnNewMessageReceived(NewMessageReceivedEventArgs e)
        {
            if (NewMessageReceived != null)
                NewMessageReceived(this, e);
            Logger.AddEntry(GetType(), "Received <" + e.MessageCount + "> new message(s).", 2);
        }

        #endregion
        #endregion

        #region Methods

        #region Private utility methods

        private string _CramMd5(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            string stamp = DateTime.Now.ToString("yyMMddhhmmss" + DateTime.Now.Millisecond.ToString());
            byte[] data = Convert.FromBase64String(Command(stamp + " authenticate cram-md5", stamp).Split(' ')[1].Trim(new char[] { '\r', '\n' }));
            string digest = Encoding.ASCII.GetString(data, 0, data.Length);
            string response = Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(username + " " + Crypto.HMACMD5Digest(password, digest))), stamp);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            LoadMailboxes();
            return response;
        }

        private string _Login(string username, string password)
            {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            string stamp = DateTime.Now.ToString("yyMMddhhmmss" + DateTime.Now.Millisecond.ToString());
            Command("authenticate login");
            Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(username)), stamp);
            string response = Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)), stamp);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            LoadMailboxes();
            return response;
        }

        private string _Plain(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            string stamp = DateTime.Now.ToString("yyMMddhhmmss" + DateTime.Now.Millisecond.ToString());
            Command("authenticate plain", stamp);
            string response = Command(Convert.ToBase64String(Encoding.UTF8.GetBytes('\0' + username + '\0' + password)), "", stamp);
            // needs to use UTF8 encoding instead of ASCII at least for password chars. _Login might need to use UTF8 as well
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            LoadMailboxes();
            return response;
        }

        private static string FindLine(string[] input, string pattern)
            {
            foreach (string str in input)
                if (str.IndexOf(pattern) != -1)
                    return str;
            return "";
        }
#if !PocketPC
        private void DoSslHandShake(SslHandShake sslHandShake)
        {
            _sslStream = new SslStream(base.GetStream(), false, sslHandShake.ServerCertificateValidationCallback, sslHandShake.ClientCertificateSelectionCallback);
            bool authenticationFailed = false;
            try
            {
                _sslStream.AuthenticateAsClient(sslHandShake.HostName, sslHandShake.ClientCertificates, sslHandShake.SslProtocol, sslHandShake.CheckRevocation);
            }
            catch (Exception ex)
            {
                authenticationFailed = true;
            }

            if (authenticationFailed)
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                _sslStream.AuthenticateAsClient(sslHandShake.HostName, sslHandShake.ClientCertificates, sslHandShake.SslProtocol, sslHandShake.CheckRevocation);
            }

        }
#endif
        private string ReadLine()
        {
            OnTcpReading();
            StreamReader sr = new StreamReader(GetStream(), true);
            string response = sr.ReadLine();
            OnTcpRead(new TcpReadEventArgs(response));
            return response;
        }

        /// <summary>
        /// Takes a command parameter and makes it safe for IMAP
        /// </summary>
        /// <param name="commandParam"></param>
        /// <returns></returns>
        private string renderSafeParam(string commandParam)
        {
            if (IsUnsafeParamsAllowed)
                return commandParam;

            var sb = new StringBuilder(commandParam);
            foreach (var badString in _badCommandStrings)
                sb.Replace(badString, "\\" + badString);

            return sb.ToString();
        }

        private void receiveResponseData(Stream stream)
        {
            byte[] readBuffer = new byte[Client.ReceiveBufferSize];
            int readbytes = 0;

            readbytes = GetStream().Read(readBuffer, 0, readBuffer.Length);
            while (readbytes > 0)
            {
                Logger.AddEntry(GetType(), readbytes + " bytes received");
                stream.Write(readBuffer, 0, readbytes);
                readbytes = 0;
                if (Available > 0)
                    readbytes = GetStream().Read(readBuffer, 0, readBuffer.Length);
            }
            Logger.AddEntry(GetType(), "no more data incoming");
        }
        #endregion

        #region Public methods

        #region Connecting, authenticating and disconnecting

        #region Cleartext methods

        /// <summary>
        /// Connects to the server.
        /// </summary>
        /// <param name="host">Server address.</param>
        /// <returns>The server's response greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// ...
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// ...
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// ...
        /// </code>
        /// </example>
        public string Connect(string host)
        {
            return Connect(host, 143);
        }
        /// <summary>
        /// Begins the connect.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginConnect(string host, AsyncCallback callback)
        {
            _delegateConnect = Connect;
            return _delegateConnect.BeginInvoke(host, 143, callback, _delegateConnect);
        }

        /// <summary>
        /// Connects to the server.
        /// </summary>
        /// <param name="host">Server address.</param>
        /// <param name="port">Server port.</param>
        /// <returns>The server's response greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com",8505);
        /// ...
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com",8505)
        /// ...
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com",8505);
        /// ...
        /// </code>
        /// </example>
        public new string Connect(string host, int port)
        {
            this.host = host;
            OnConnecting();
            base.Connect(host, port);
            string response = ReadLine();
            ServerCapabilities = Command("capability");
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        /// <summary>
        /// Begins the connect.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginConnect(string host, int port, AsyncCallback callback)
        {
            _delegateConnect = Connect;
            return _delegateConnect.BeginInvoke(host, port, callback, _delegateConnect);
        }

        /// <summary>
        /// Connects the specified addr.
        /// </summary>
        /// <param name="addr">The addr.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public new string Connect(System.Net.IPAddress addr, int port)
        {
            OnConnecting();
            base.Connect(addr, port);
            string response = ReadLine();
            ServerCapabilities = Command("capability");
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        /// <summary>
        /// Begins the connect.
        /// </summary>
        /// <param name="addr">The addr.</param>
        /// <param name="port">The port.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginConnect(System.Net.IPAddress addr, int port, AsyncCallback callback)
        {
            _delegateConnectIPAddress = Connect;
            return _delegateConnectIPAddress.BeginInvoke(addr, port, callback, _delegateConnectIPAddress);
        }


        /// <summary>
        /// Connects the specified addresses.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public new string Connect(System.Net.IPAddress[] addresses, int port)
        {
            OnConnecting();
#if !PocketPC
            base.Connect(addresses, port);
#else
                if(addresses.Length>0)
                    base.Connect(addresses[0], port);
#endif
            string response = ReadLine();
            ServerCapabilities = Command("capability");
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, int port, AsyncCallback callback)
        {
            _delegateConnectIPAddresses = Connect;
            return _delegateConnectIPAddresses.BeginInvoke(addresses, port, callback, _delegateConnectIPAddresses);
        }

        public string Connect(string host, string username, string password)
        {
            return Connect(host, 143, username, password);
        }
        /// <summary>
        /// Begins the connect.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginConnect(string host, string username, string password, AsyncCallback callback)
        {
            return BeginConnect(host, 143, username, password, callback);
        }

        /// <summary>
        /// Connects the specified host.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string Connect(string host, int port, string username, string password)
        {
            Connect(host, port);
            return LoginFast(username, password);
        }
        /// <summary>
        /// Begins the connect.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginConnect(string host, int port, string username, string password, AsyncCallback callback)
        {
            _delegateConnectAuth = Connect;
            return _delegateConnectAuth.BeginInvoke(host, port, username, password, callback, _delegateConnectAuth);
        }

        /// <summary>
        /// Ends the connect.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public new string EndConnect(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get
            {
                if (Client != null)
                    return Client.Connected;
                else
                    return false;
            }
        }
        #endregion

        #region SSL methods
#if !PocketPC
        public string ConnectSsl(string host)
        {
            return ConnectSsl(host, 993, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 993, new SslHandShake(host), callback);
        }
        public string ConnectSsl(string host, SslHandShake sslHandShake)
        {
            return ConnectSsl(host, 993, sslHandShake);
        }
        public IAsyncResult BeginConnectSsl(string host, SslHandShake sslHandShake, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 993, sslHandShake, callback);
        }
        public string ConnectSsl(string host, int port)
        {
            return ConnectSsl(host, port, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, int port, AsyncCallback callback)
        {
            return BeginConnectSsl(host, port, new SslHandShake(host), callback);
        }
#endif
#if !PocketPC
        public string ConnectSsl(string host, int port, SslHandShake sslHandShake)
        {
            this.host = host;
            OnConnecting();
            base.Connect(host, port);
            DoSslHandShake(sslHandShake);
            string response = ReadLine();
            ServerCapabilities = Command("capability");
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnectSsl(string host, int port, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSsl = ConnectSsl;
            return _delegateConnectSsl.BeginInvoke(host, port, sslHandShake, callback, _delegateConnectSsl);
        }

        public string ConnectSsl(System.Net.IPAddress addr, int port, SslHandShake sslHandShake)
        {
            OnConnecting();
            base.Connect(addr, port);
            DoSslHandShake(sslHandShake);
            string response = ReadLine();
            ServerCapabilities = Command("capability");
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnectSsl(System.Net.IPAddress addr, int port, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSslIPAddress = ConnectSsl;
            return _delegateConnectSslIPAddress.BeginInvoke(addr, port, sslHandShake, callback, _delegateConnectSslIPAddress);
        }

        public string ConnectSsl(System.Net.IPAddress[] addresses, int port, SslHandShake sslHandShake)
        {
            OnConnecting();
            base.Connect(addresses, port);
            DoSslHandShake(sslHandShake);
            string response = ReadLine();
            ServerCapabilities = Command("capability");
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnectSsl(System.Net.IPAddress[] addresses, int port, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSslIPAddresses = ConnectSsl;
            return _delegateConnectSslIPAddresses.BeginInvoke(addresses, port, sslHandShake, callback, _delegateConnectSslIPAddresses);
        }

        public string EndConnectSsl(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }
#endif
        #endregion

        #region Disconnect method

        /// <summary>
        /// Logs out and closes the connection with the server.
        /// </summary>
        /// <returns>The server's googbye greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// //Do some work...
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// 'Do some work...
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// //Do some work...
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Disconnect()
        {
            string greeting = Command("logout");
            base.Close();
            return greeting;
        }
        public IAsyncResult BeginDisconnect(AsyncCallback callback)
        {
            _delegateDisconnect = Disconnect;
            return _delegateDisconnect.BeginInvoke(callback, null);
        }

        public string EndDisconnect(IAsyncResult result)
        {
            return _delegateDisconnect.EndInvoke(result);
        }

        public void CloseBaseTCPConnection()
        {
            base.Close();
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Logs in to the specified account and load all account MailBox.
        /// </summary>
        /// <param name="username">Username of the account.</param>
        /// <param name="password">Password of the account.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// //Do some work...
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// 'Do some work...
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// //Do some work...
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Login(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password, host));
            string response = Command("login " + username + " " + password);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, host, response));
            LoadMailboxes();
            return response;
        }
        public IAsyncResult BeginLogin(string username, string password, AsyncCallback callback)
        {
            _delegateLogin = Login;
            return _delegateLogin.BeginInvoke(username, password, callback, _delegateLogin);
        }


        /// <summary>
        /// Same as Login but doesn't load the AllMailboxes and Mailboxes properties of the Imap4Client object, ensuring faster operation.
        /// </summary>
        /// <param name="username">Username of the account.</param>
        /// <param name="password">Password of the account.</param>
        /// <returns>The server's response.</returns>
        public string LoginFast(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password, host));
            string response = Command("login " + username + " " + password);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, host, response));
            return response;
        }
        public IAsyncResult BeginLoginFast(string username, string password, AsyncCallback callback)
        {
            _delegateLogin = LoginFast;
            return _delegateLogin.BeginInvoke(username, password, callback, _delegateLogin);
        }

        /// <summary>
        /// Authenticates using the given SASL mechanism.
        /// </summary>
        /// <param name="username">Username to authenticate as.</param>
        /// <param name="password">Password.</param>
        /// <param name="mechanism">SASL mechanism to be used.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Authenticate("user","pass",SASLMechanism.CramMd5);
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Authenticate("user","pass",SASLMechanism.CramMd5)
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Authenticate("user","pass",SASLMechanism.CramMd5);
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Authenticate(string username, string password, SaslMechanism mechanism)
        {
            switch (mechanism)
            {
                case SaslMechanism.CramMd5:
                    return _CramMd5(username, password);
                case SaslMechanism.Login:
                    return _Login(username, password);
                case SaslMechanism.Plain:
                    return _Plain(username, password);
            }
            return string.Empty;
        }
        public IAsyncResult BeginAuthenticate(string username, string password, SaslMechanism mechanism, AsyncCallback callback)
        {
            _delegateAuthenticate = Authenticate;
            return _delegateAuthenticate.BeginInvoke(username, password, mechanism, callback, null);
        }

        public string EndAuthenticate(IAsyncResult result)
        {
            return _delegateAuthenticate.EndInvoke(result);
        }

        #endregion

        #region Idle

        /// <summary>
        /// Start the idle on the mail server.
        /// </summary>
        public void StartIdle()
        {
            Command("IDLE");

            StreamReader sr = new StreamReader(GetStream(), Encoding.ASCII);
            StringBuilder buffer = new StringBuilder();
            string response = string.Empty;
            _idleInProgress = true;
            while (true)
            {
                if (_idleInProgress)
                {
                    OnTcpReading();
                    response = sr.ReadLine();
                    OnTcpRead(new TcpReadEventArgs(response));

                    if (response.ToUpper().IndexOf("RECENT") > 0)
                        OnNewMessageReceived(new NewMessageReceivedEventArgs(int.Parse(response.Split(' ')[1])));
#if DEBUG
                    Console.WriteLine(response);
#endif
                }
                else
                {
                    Command("DONE", string.Empty);
                    break;
                }
            }
        }

        /// <summary>
        /// Stop the idle on the imap4 server.
        /// </summary>
        public void StopIdle()
        {
            _idleInProgress = false;
        }

        #endregion

        #endregion

        #region Command sending, receiving and stream access

        //Binary result
        public byte[] CommandBinary(string command, CommandOptions options = null)
        {
            return CommandBinary(command, DateTime.Now.ToString("yyMMddhhmmss" + DateTime.Now.Millisecond.ToString()), options);
        }

        public byte[] CommandBinary(string command, string stamp, CommandOptions options = null)
        {
            if (options == null)
                options = new CommandOptions();

            if (command.Length < 200)
                OnTcpWriting(new TcpWritingEventArgs(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n"));
            else
                OnTcpWriting(new TcpWritingEventArgs("long command data"));
            //base.GetStream().Write(System.Text.Encoding.ASCII.GetBytes(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n\r\n"), 0, stamp.Length + ((stamp.Length > 0) ? 1 : 0) + command.Length + 2);

            // Although I have still not read all the relevant code but here it looks that you are
            // directly trying to write to the network stream which is incorrect. I have commented your
            // line above writing directly to network stream and have slightly changed it to write to
            // sslstream. I am unable to biuld this solution as 200+ missing file errors are shown. But
            // I have run the NUnit test twice and it is passing succesfully therefore I have not checked
            // the reading portion from ssl stream. Theoreticaly decrytpion exception should only get generated
            // when there is a problem with reading from ssl stream but may be because direct attempt was made
            // to write to Network stream so some how it threw decryption exception.
            // please see it run and test it--------Atif

            // Complement the Atif changes. Use the flag for !PocketPC config for avoid build errors.

#if !PocketPC
            if (_sslStream != null)
                _sslStream.Write(Encoding.ASCII.GetBytes(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n\r\n"), 0, stamp.Length + ((stamp.Length > 0) ? 1 : 0) + command.Length + 2);
            else
                base.GetStream().Write(Encoding.ASCII.GetBytes(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n\r\n"), 0, stamp.Length + ((stamp.Length > 0) ? 1 : 0) + command.Length + 2);
#else
            base.GetStream().Write(System.Text.Encoding.ASCII.GetBytes(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n\r\n"), 0, stamp.Length + ((stamp.Length > 0) ? 1 : 0) + command.Length + 2);
#endif
            if (command.Length < 200)
                OnTcpWritten(new TcpWrittenEventArgs(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n"));
            else
                OnTcpWritten(new TcpWrittenEventArgs("long command data"));
            OnTcpReading();
            StringBuilder buffer = new StringBuilder();

            var commandAsUpper = command.ToUpper();
            string partResponse = "";
            string lastLine = "";
            string lastLineOfPartResponse = "";
            using (StreamReader sr = new StreamReader(new MemoryStream()))
            {
                while (true)
                {
                    if (sr.EndOfStream)
                    {
                        Logger.AddEntry(GetType(), "read data from stream");
                        long streamPos = sr.BaseStream.Position;
                        receiveResponseData(sr.BaseStream);
                        sr.BaseStream.Seek(streamPos, SeekOrigin.Begin);
                    }
                    partResponse = sr.ReadToEnd();

                    if (partResponse == null)
                    {
                        Logger.AddEntry(GetType(), "part response is null");
                        partResponse = "";
                    } else
                        Logger.AddEntry(GetType(), "part response: " + partResponse);
                    buffer.Append(partResponse);

                    int pos = partResponse.Trim().LastIndexOf("\r\n");
                    if (pos > 0)
                        pos += 2;
                    else
                        pos = 0;
                    lastLineOfPartResponse = partResponse.Substring(pos);

                    if (commandAsUpper.StartsWith("LIST") == true)
                    {
                        if (lastLineOfPartResponse.StartsWith(stamp) || (lastLineOfPartResponse.StartsWith("+ ") && options.IsPlusCmdAllowed))
                        {
                            lastLine = lastLineOfPartResponse;
                            break;
                        }
                        else
                        {
                            if (CheckResponse(buffer, command, lastLineOfPartResponse, ref lastLine, stamp))
                                break;
                        }
                    }
                    else if (commandAsUpper.StartsWith("DONE") == true)
                    {
                        lastLine = lastLineOfPartResponse;
                        stamp = lastLine.Split(' ')[0];
                        break;
                    }
                    else if (lastLineOfPartResponse != null)
                    {
                        //Had to remove + check - this was failing when the email contained a line with + 
                        //Please add comments as to why here, and reimplement differently
                        if (lastLineOfPartResponse.StartsWith(stamp) || lastLineOfPartResponse.ToLower().StartsWith("* " + command.Split(' ')[0].ToLower()) || (lastLineOfPartResponse.StartsWith("+ ") && options.IsPlusCmdAllowed))
                        {
                            if (lastLineOfPartResponse.IndexOf(stamp + " OK") >= 0)
                            {
                                if (lastLineOfPartResponse.EndsWith("\n") || lastLineOfPartResponse.EndsWith("\r"))
                                {
                                    lastLine = lastLineOfPartResponse;
                                    break;
                                }
                            }
                            else if (lastLineOfPartResponse.IndexOf(stamp + " NO") >= 0 || lastLineOfPartResponse.IndexOf(stamp + " BAD") >= 0)
                            {
                                if (lastLineOfPartResponse.EndsWith("\n") || lastLineOfPartResponse.EndsWith("\r"))
                                {
                                    lastLine = lastLineOfPartResponse.Replace(stamp, "");
                                    throw new Imap4Exception("Command \"" + command + "\" failed\r\nServer replied:" + lastLine);
                                }
                            }
                        }
                        else
                        {
                            if (CheckResponse(buffer, command, lastLineOfPartResponse, ref lastLine, stamp))
                                break;
                        }
                    }
                }

                var bufferString = buffer.ToString();
                byte[] bufferBytes = new byte[sr.BaseStream.Length];
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                sr.BaseStream.Read(bufferBytes, 0, bufferBytes.Length);

                if (!sr.CurrentEncoding.Equals(Encoding.UTF8))
                    {
                    var utf8Bytes = Encoding.Convert(sr.CurrentEncoding, Encoding.UTF8, sr.CurrentEncoding.GetBytes(bufferString));
                    bufferString = Encoding.UTF8.GetString(utf8Bytes);
                }

                if (buffer.Length < 200)
                    OnTcpRead(new TcpReadEventArgs(bufferString));
                else
                    OnTcpRead(new TcpReadEventArgs("long data"));
                if (lastLine.StartsWith(stamp + " OK") || lastLine.ToLower().StartsWith("* " + command.Split(' ')[0].ToLower()) || lastLine.StartsWith("+ "))
                    return bufferBytes;
                else
                    throw new Imap4Exception("Command \"" + command + "\" failed", bufferBytes);
            }
        }

        private bool CheckResponse(StringBuilder buffer, string command, string lastLineOfPartResponse, ref string lastLine, string stamp)
        {
            if (buffer.Length > 100)
                lastLineOfPartResponse = buffer.ToString().Substring(buffer.Length - 100).Replace("\r\n", "");
            else
                lastLineOfPartResponse = buffer.ToString().Replace("\r\n", "");
            int stampPos = lastLineOfPartResponse.IndexOf(stamp + " OK");
            if (stampPos >= 0)
            {
                if (buffer.ToString().EndsWith("\n") || buffer.ToString().EndsWith("\r"))
                {
                    lastLine = lastLineOfPartResponse.Substring(stampPos);
                    return true;
                }
            }
            stampPos = lastLineOfPartResponse.IndexOf(stamp + " NO");
            if (stampPos >= 0)
            {
                if (buffer.ToString().EndsWith("\n") || buffer.ToString().EndsWith("\r"))
                {
                    lastLine = lastLineOfPartResponse.Replace(stamp, "");
                    throw new Imap4Exception("Command \"" + command + "\" failed\r\nServer replied:" + lastLine);
                }
            }
            stampPos = lastLineOfPartResponse.IndexOf(stamp + " BAD");
            if (stampPos >= 0)
            {
                if (buffer.ToString().EndsWith("\n") || buffer.ToString().EndsWith("\r"))
                {
                    lastLine = lastLineOfPartResponse.Replace(stamp, "");
                    throw new Imap4Exception("Command \"" + command + "\" failed\r\nServer replied:" + lastLine);
                }
            }
            return false;
        }

        public byte[] CommandBinary(string command, string stamp, string checkStamp, CommandOptions options = null)
        {
            if (options == null)
                options = new CommandOptions();

            if (command.Length < 200)
                OnTcpWriting(new TcpWritingEventArgs(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n"));
            else
                OnTcpWriting(new TcpWritingEventArgs("long command data"));
            base.GetStream().Write(Encoding.ASCII.GetBytes(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n"), 0, stamp.Length + ((stamp.Length > 0) ? 1 : 0) + command.Length + 2);
            if (command.Length < 200)
                OnTcpWritten(new TcpWrittenEventArgs(stamp + ((stamp.Length > 0) ? " " : "") + command + "\r\n"));
            else
                OnTcpWritten(new TcpWrittenEventArgs("long command data"));
            OnTcpReading();
            //System.IO.StreamReader sr = new System.IO.StreamReader(base.GetStream(), true);
            StringBuilder buffer = new StringBuilder();
            string temp = "";
            string lastline = "";
            using (StreamReader sr = new StreamReader(new MemoryStream()))
            {
                while (true)
                {
                    if (sr.EndOfStream)
                    {
                        long streamPos = sr.BaseStream.Position;
                        receiveResponseData(sr.BaseStream);
                        sr.BaseStream.Seek(streamPos, SeekOrigin.Begin);
                    }
                    temp = sr.ReadLine();
                    buffer.Append(temp + "\r\n");
                    if (temp.StartsWith("+ ") && options.IsPlusCmdAllowed)
                    {
                        lastline = temp;
                        break;
                    }

                    if (temp.StartsWith(checkStamp) || temp.ToLower().StartsWith("* " + command.Split(' ')[0].ToLower()))
                    {
                        lastline = temp;
                        break;
                    }
                }

                byte[] bufferBytes = new byte[sr.BaseStream.Length];
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                sr.BaseStream.Read(bufferBytes, 0, bufferBytes.Length);

                if (buffer.Length < 200)
                    OnTcpRead(new TcpReadEventArgs(buffer.ToString()));
                else
                    OnTcpRead(new TcpReadEventArgs("long data"));
                if (lastline.StartsWith(checkStamp + " OK") || temp.ToLower().StartsWith("* " + command.Split(' ')[0].ToLower()) || temp.StartsWith("+ ") || temp.StartsWith("+\r\n"))
                    return bufferBytes;
                else
                    throw new Imap4Exception("Command \"" + command + "\" failed", bufferBytes);
            }
        }

        //With Encoding parameter
        public string Command(string command, string stamp, Encoding encoding, CommandOptions options = null)
        {
            return encoding.GetString(CommandBinary(command, stamp, options));
        }

        public string Command(string command, string stamp, string checkStamp, Encoding encoding, CommandOptions options = null)
        {
            return encoding.GetString(CommandBinary(command, stamp, checkStamp, options));
        }

        /// <summary>
        /// Sends the command to the server.
        /// The command tag is automatically added.
        /// </summary>
        /// <param name="command">The command (with arguments if necesary) to be sent.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.Command("select inbox");
        /// //Selected mailbox is inbox.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// imap.Command("select inbox")
        /// 'Selected mailbox is inbox.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.Command("select inbox");
        /// //Selected mailbox is inbox.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Command(string command, CommandOptions options = null)
        {
            return Command(command, DateTime.Now.ToString("yyMMddhhmmss" + DateTime.Now.Millisecond.ToString()), options);
        }
        public IAsyncResult BeginCommand(string command, AsyncCallback callback)
        {

            return BeginCommand(command, DateTime.Now.ToString("yyMMddhhmmss" + DateTime.Now.Millisecond.ToString()), callback);
        }

        public string Command(string command, string stamp, CommandOptions options = null)
        {
            return Command(command, stamp, Encoding.UTF8, options);
        }
        public IAsyncResult BeginCommand(string command, string stamp, AsyncCallback callback, CommandOptions options = null)
        {
            _delegateCommand = Command;
            return _delegateCommand.BeginInvoke(command, stamp, options, callback, _delegateCommand);
        }

        public string Command(string command, string stamp, string checkStamp, CommandOptions options = null)
        {
            return Command(command, stamp, checkStamp, Encoding.UTF8, options);
        }
        public IAsyncResult BeginCommand(string command, string stamp, string checkStamp, AsyncCallback callback, CommandOptions options = null)
        {
            _delegateCommandStringStringString = Command;
            return _delegateCommandStringStringString.BeginInvoke(command, stamp, checkStamp, options, callback, _delegateCommandStringStringString);
        }

        public string EndCommand(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Gets the communication stream of this object.
        /// </summary>
        /// <returns>A Stream object, either of type NetworkStream or SslStream if the channel is secured.</returns>
        public Stream Stream {
            get {
#if !PocketPC
                if (_sslStream != null)
                    return _sslStream;
#endif
                return base.GetStream();
            }
        }
        /// <summary>
        /// Gets the communication stream of this object.
        /// </summary>
        /// <returns>A Stream object, either of type NetworkStream or SslStream if the channel is secured.</returns>
        /// [Obsolete("use Stream")]
        public new Stream GetStream()
        {
#if !PocketPC
            if (_sslStream != null)
                return _sslStream;
#endif
            return base.GetStream();
        }

        #endregion

        #region Implementation of the IMAP4 protocol

        /// <summary>
        /// Performs a NOOP command which is used to maintain the connection alive.
        /// </summary>
        /// <returns>The server response.</returns>
        /// <remarks>Some servers include mailbox update informations in the response.</remarks>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// try
        /// {
        ///        imap.Noop();
        ///        imap.Disconnect();
        ///    }
        ///    catch
        ///    {
        ///        throw new Exception("Connection lost.");
        ///    }
        ///     
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// Try
        ///     imap.Noop()
        ///        imap.Disconnect()
        ///    Catch
        ///        Throw New Exception("Connection lost.");
        ///    End Try
        ///    
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// try
        /// {
        ///        imap.Noop();
        ///        imap.Disconnect();
        ///    }
        ///    catch
        ///    {
        ///        throw new Exception("Connection lost.");
        ///    }
        /// </code>
        /// </example>
        public string Noop()
        {
            OnNooping();
            string response = Command("noop");
            OnNooped();
            return response;
        }
        public IAsyncResult BeginNoop(AsyncCallback callback)
        {
            _delegateNoop = Noop;
            return _delegateNoop.BeginInvoke(callback, _delegateNoop);
        }

        public string EndNoop(IAsyncResult result)
        {
            return _delegateNoop.EndInvoke(result);
        }

        /// <summary>
        /// Equivalent to Noop().
        /// </summary>
        /// <returns>The server's response.</returns>
        public string Check()
        {
            return Command("check");
        }
        public IAsyncResult BeginCheck(AsyncCallback callback)
        {
            _delegateCheck = Check;
            return _delegateCheck.BeginInvoke(callback, _delegateCheck);
        }

        public string EndCheck(IAsyncResult result)
        {
            return _delegateCheck.EndInvoke(result);
        }

        /// <summary>
        /// Closes the mailbox and removes messages marked with the Deleted flag.
        /// </summary>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// //Get the amount of messages in the inbox.
        /// int messageCount = inbox.MessageCount;
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// //Get the amount of messages in the inbox.
        /// Dim messageCount As Integer = inbox.MessageCount
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// //Get the amount of messages in the inbox.
        /// var messageCount:int = inbox.MessageCount;
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public new string Close()
        {
            return Command("close");
        }
        public IAsyncResult BeginClose(AsyncCallback callback)
        {
            _delegateClose = Close;
            return _delegateClose.BeginInvoke(callback, _delegateClose);
        }

        public string EndClose(IAsyncResult result)
        {
            return _delegateClose.EndInvoke(result);
        }

        /// <summary>
        /// Removes all messages marked with the Deleted flag.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// //Mark message 1 for deletion.
        /// inbox.DeleteMessage(1);
        /// //Effectively remove all message marked with Deleted flag.
        /// imap.Expunge();
        /// //Message 1 is permanently removed.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// 'Mark message 1 for deletion.
        /// inbox.DeleteMessage(1)
        /// 'Effectively remove all message marked with Deleted flag.
        /// imap.Expunge()
        /// 'Message 1 is permanently removed.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// //Mark message 1 for deletion.
        /// inbox.DeleteMessage(1);
        /// //Effectively remove all message marked with Deleted flag.
        /// imap.Expunge();
        /// //Message 1 is permanently removed.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public void Expunge()
        {
            Command("expunge");
        }
        public IAsyncResult BeginExpunge(AsyncCallback callback)
        {
            _delegateExpunge = Expunge;
            return _delegateExpunge.BeginInvoke(callback, _delegateExpunge);
        }

        public void EndExpunge(IAsyncResult result)
        {
            _delegateExpunge.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves a list of mailboxes.
        /// </summary>
        /// <param name="reference">The base path.</param>
        /// <param name="mailboxName">Mailbox name.</param>
        /// <returns>A MailboxCollection object containing the requested mailboxes.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// //Return all children mailboxes of "inbox".
        /// MailboxCollection mailboxes = imap.GetMailboxes("inbox","*");
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// 'Return all children mailboxes of "inbox".
        /// Dim mailboxes As MailboxCollection = imap.GetMailboxes("inbox","*")
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// //Return all children mailboxes of "inbox".
        /// var mailboxes:MailboxCollection  = imap.GetMailboxes("inbox","*");
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public MailboxCollection GetMailboxes(string reference, string mailboxName)
        {
            MailboxCollection mailboxes = new MailboxCollection();
            string response = Command("list \"" + reference + "\" \"" + mailboxName + "\"");
            string[] t = System.Text.RegularExpressions.Regex.Split(response, "\r\n");
            string box = "";
            for (int i = 0; i < t.Length - 2; i++)
            {
                try
                {
                    if (t[i].ToUpper().Trim().StartsWith("* LIST"))
                    {
                        box = t[i].Substring(t[i].IndexOf("\" ") + 1).Trim(new char[] { ' ', '\"' });
                        if (box != reference)
                            mailboxes.Add(ExamineMailbox(box));
                    }
                }
                catch
                {
                    continue;
                }
            }
            return mailboxes;
        }

        public IAsyncResult BeginGetMailboxes(string reference, string mailboxName, AsyncCallback callback)
        {
            _delegateGetMailboxes = GetMailboxes;
            return _delegateGetMailboxes.BeginInvoke(reference, mailboxName, callback, _delegateGetMailboxes);
        }

        public MailboxCollection EndGetMailboxes(IAsyncResult result)
        {
            return _delegateGetMailboxes.EndInvoke(result);
        }

        /// <summary>
        /// Fills in or refreshes the Imap4Client.AllMailboxes and Imap4Client.Mailboxes properties.
        /// </summary>
        public void LoadMailboxes()
        {
            Mailboxes = GetMailboxes("", "%");
            AllMailboxes = GetMailboxes("", "*");
        }
        public IAsyncResult BeginLoadMailboxes(AsyncCallback callback)
        {
            _delegateLoadMailboxes = LoadMailboxes;
            return _delegateLoadMailboxes.BeginInvoke(callback, _delegateLoadMailboxes);
        }

        public void EndLoadMailboxes(IAsyncResult result)
        {
            _delegateLoadMailboxes.EndInvoke(result);
        }

        /// <summary>
        /// Creates a mailbox with the specified name.
        /// </summary>
        /// <param name="mailboxName">The name of the new mailbox.</param>
        /// <returns>The newly created mailbox.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.CreateMailbox("inbox.Staff");
        /// //Child mailbox of inbox named Staff has been created.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// imap.CreateMailbox("inbox.Staff");
        /// 'Child mailbox of inbox named Staff has been created.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.CreateMailbox("inbox.Staff");
        /// //Child mailbox of inbox named Staff has been created.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public Mailbox CreateMailbox(string mailboxName)
        {
            Command("create \"" + mailboxName + "\"");
            return SelectMailbox(mailboxName);
        }
        public IAsyncResult BeginCreateMailbox(string mailboxName, AsyncCallback callback)
        {
            _delegateMailboxOperation = CreateMailbox;
            return _delegateMailboxOperation.BeginInvoke(mailboxName, callback, _delegateMailboxOperation);
        }

        public Mailbox EndCreateMailbox(IAsyncResult result)
        {
            return _delegateMailboxOperation.EndInvoke(result);
        }

        /// <summary>
        /// Renames a mailbox.
        /// </summary>
        /// <param name="oldMailboxName">The mailbox to be renamed.</param>
        /// <param name="newMailboxName">The new name of the mailbox.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.RenameMailbox("inbox.Staff","Staff");
        /// //The Staff mailbox is now a top-level mailbox.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// imap.RenameMailbox("inbox.Staff","Staff");
        /// 'The Staff mailbox is now a top-level mailbox.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.RenameMailbox("inbox.Staff","Staff");
        /// //The Staff mailbox is now a top-level mailbox.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string RenameMailbox(string oldMailboxName, string newMailboxName)
        {
            string response = Command("rename \"" + oldMailboxName + "\" \"" + newMailboxName + "\"");
            return response;
        }
        public IAsyncResult BeginRenameMailbox(string oldMailboxName, string newMailboxName, AsyncCallback callback)
        {
            _delegateRenameMailbox = RenameMailbox;
            return _delegateRenameMailbox.BeginInvoke(oldMailboxName, newMailboxName, callback, _delegateRenameMailbox);
        }

        public void EndRenameMailbox(IAsyncResult result)
        {
            _delegateRenameMailbox.EndInvoke(result);
        }

        /// <summary>
        /// Deletes a mailbox.
        /// </summary>
        /// <param name="mailboxName">The mailbox to be deleted.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.DeleteMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now deleted.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// imap.DeleteMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now deleted.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// imap.DeleteMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now deleted.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string DeleteMailbox(string mailboxName)
        {
            return Command("delete \"" + mailboxName + "\"");
        }
        public IAsyncResult BeginDeleteMailbox(string mailboxName, AsyncCallback callback)
        {
            _delegateMailboxOperationReturnsString = DeleteMailbox;
            return _delegateMailboxOperationReturnsString.BeginInvoke(mailboxName, callback, _delegateMailboxOperationReturnsString);
        }

        public string EndDeleteMailbox(IAsyncResult result)
        {
            return _delegateMailboxOperationReturnsString.EndInvoke(result);
        }

        /// <summary>
        /// Subscribes to a mailbox.
        /// </summary>
        /// <param name="mailboxName">The mailbox to be subscribed to.</param>
        /// <returns>The server's response.</returns>
        public string SubscribeMailbox(string mailboxName)
        {
            return Command("subscribe \"" + mailboxName + "\"");
        }
        public IAsyncResult BeginSubscribeMailbox(string mailboxName, AsyncCallback callback)
        {
            _delegateMailboxOperationReturnsString = SubscribeMailbox;
            return _delegateMailboxOperationReturnsString.BeginInvoke(mailboxName, callback, _delegateMailboxOperationReturnsString);
        }

        public void EndSubscribeMailbox(IAsyncResult result)
        {
            _delegateMailboxOperationReturnsString.EndInvoke(result);
        }

        /// <summary>
        /// Unsubscribes from a mailbox.
        /// </summary>
        /// <param name="mailboxName">The mailbox to be unsubscribed from.</param>
        /// <returns>The server's response.</returns>
        public string UnsubscribeMailbox(string mailboxName)
        {
            return Command("unsubscribe \"" + mailboxName + "\"");
        }
        public IAsyncResult BeginUnsubscribeMailbox(string mailboxName, AsyncCallback callback)
        {
            _delegateMailboxOperationReturnsString = UnsubscribeMailbox;
            return _delegateMailboxOperationReturnsString.BeginInvoke(mailboxName, callback, _delegateMailboxOperationReturnsString);
        }

        public void EndUnsubscribeMailbox(IAsyncResult result)
        {
            _delegateMailboxOperationReturnsString.EndInvoke(result);
        }

        /// <summary>
        /// Selects a mailbox on the server.
        /// </summary>
        /// <param name="mailboxName">The mailbox to be selected.</param>
        /// <returns>The selected mailbox.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// Mailbox mbox = imap.SelectMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now selected.
        /// mbox.Empty(true);
        /// //Mailbox inbox.Staff is now empty.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// Dim mbox As Mailbox = imap.SelectMailbox("inbox.Staff")
        /// 'The inbox.Staff mailbox is now selected.
        /// mbox.Empty(true)
        /// 'Mailbox inbox.Staff is now empty.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// var mbox:Mailbox = imap.SelectMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now selected.
        /// mbox.Empty(true);
        /// //Mailbox inbox.Staff is now empty.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public Mailbox SelectMailbox(string mailboxName)
        {
            mailboxName = renderSafeParam(mailboxName);

            Mailbox mailbox = new Mailbox();
            mailbox.SubMailboxes = GetMailboxes(mailboxName, "*");
            string response = Command("select \"" + mailboxName + "\"");
            string[] lines = System.Text.RegularExpressions.Regex.Split(response, "\r\n");

            // message count.
            int messageCount = 0;
            try
            {
                messageCount = Convert.ToInt32(FindLine(lines, "EXISTS").Split(' ')[1]);
            }
            catch (Exception) { }
            mailbox.MessageCount = messageCount;

            // recent.
            int recent = 0;
            try
            {
                recent = Convert.ToInt32(FindLine(lines, "RECENT").Split(' ')[1]);
            }
            catch (Exception) { }
            mailbox.Recent = recent;

            // unseen.
            int unseen = 0;
            try
            {
                unseen = Convert.ToInt32(FindLine(lines, "[UNSEEN ").Split(' ')[3].TrimEnd(']'));
            }
            catch (Exception) { }
            mailbox.FirstUnseen = (response.ToLower().IndexOf("[unseen") != -1) ? unseen : 0;

            // uid validity.
            int uidValidity = 0;
            try
            {
                uidValidity = Convert.ToInt32(FindLine(lines, "[UIDVALIDITY ").Split(' ')[3].TrimEnd(']'));
            }
            catch (Exception) { }
            mailbox.UidValidity = uidValidity;

            // flags.
            foreach (string str in FindLine(lines, " FLAGS").Split(' ')) {
                if (str.StartsWith("(\\") || str.StartsWith("\\")) {
                    mailbox.ApplicableFlags.Add(str.Trim(new char[] { ' ', '\\', ')', '(' }));
                }
            }

            // permanent flags.
            if (response.ToLower().IndexOf("[permanentflags") != -1)
                foreach (string str in FindLine(lines, "[PERMANENTFLAGS").Split(' '))
                    if (str.StartsWith("(\\") || str.StartsWith("\\"))
                        mailbox.PermanentFlags.Add(str.Trim(new char[] { ' ', '\\', ')', '(' }));

            // read-write and read-only.
            if (response.ToLower().IndexOf("[read-write]") != -1)
                mailbox.Permission = MailboxPermission.ReadWrite;
            else if (response.ToLower().IndexOf("[read-only]") != -1)
                mailbox.Permission = MailboxPermission.ReadOnly;

            mailbox.Name = mailboxName;
            mailbox.SourceClient = this;
            return mailbox;
        }
        public IAsyncResult BeginSelectMailbox(string mailboxName, AsyncCallback callback)
        {
            _delegateMailboxOperation = SelectMailbox;
            return _delegateMailboxOperation.BeginInvoke(mailboxName, callback, _delegateMailboxOperation);
        }

        public Mailbox EndSelectMailbox(IAsyncResult result)
        {
            return _delegateMailboxOperation.EndInvoke(result);
        }

        /// <summary>
        /// Same as <see cref="SelectMailbox(string)"/> except that the mailbox is opened with read-only permission.
        /// </summary>
        /// <param name="mailboxName">The mailbox to be examined.</param>
        /// <returns>The examined mailbox.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// Mailbox mbox = imap.ExamineMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now selected (read-only).
        /// int recentMessageCount = mbox.Recent;
        /// //There are recentMessageCount messages that haven't been read in inbox.Staff.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("user","pass")
        /// Dim mbox As Mailbox = imap.ExamineMailbox("inbox.Staff")
        /// 'The inbox.Staff mailbox is now selected (read-only).
        /// Dim recentMessageCount As Integer = mbox.Recent
        /// 'There are recentMessageCount messages that haven't been read in inbox.Staff.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("user","pass");
        /// var mbox:Mailbox = imap.ExamineMailbox("inbox.Staff");
        /// //The inbox.Staff mailbox is now selected (read-only).
        /// int recentMessageCount = mbox.Recent;
        /// //There are recentMessageCount messages that haven't been read in inbox.Staff.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public Mailbox ExamineMailbox(string mailboxName)
        {
            Mailbox mailbox = new Mailbox();
            mailbox.SubMailboxes = GetMailboxes(mailboxName, "*");
            string response = Command("examine \"" + mailboxName + "\"");
            string[] lines = System.Text.RegularExpressions.Regex.Split(response, "\r\n");
            mailbox.MessageCount = Convert.ToInt32(FindLine(lines, "EXISTS").Split(' ')[1]);
            mailbox.Recent = Convert.ToInt32(FindLine(lines, "RECENT").Split(' ')[1]);
            mailbox.FirstUnseen = (response.ToLower().IndexOf("[unseen") != -1) ? Convert.ToInt32(FindLine(lines, "[UNSEEN ").Split(' ')[3].TrimEnd(']')) : 0;
            mailbox.UidValidity = Convert.ToInt32(FindLine(lines, "[UIDVALIDITY ").Split(' ')[3].TrimEnd(']'));
            foreach (string str in FindLine(lines, " FLAGS").Split(' '))
                if (str.StartsWith("(\\") || str.StartsWith("\\"))
                    mailbox.ApplicableFlags.Add(str.Trim(new char[] { ' ', '\\', ')', '(' }));
            if (response.ToLower().IndexOf("[permanentflags") != -1)
                foreach (string str in FindLine(lines, "[PERMANENTFLAGS").Split(' '))
                    if (str.StartsWith("(\\") || str.StartsWith("\\"))
                        mailbox.PermanentFlags.Add(str.Trim(new char[] { ' ', '\\', ')', '(' }));
            mailbox.Permission = MailboxPermission.ReadOnly;
            mailbox.Name = mailboxName;
            mailbox.SourceClient = this;
            return mailbox;
        }
        public IAsyncResult BeginExamineMailbox(string mailboxName, AsyncCallback callback)
        {
            _delegateMailboxOperation = ExamineMailbox;
            return _delegateMailboxOperation.BeginInvoke(mailboxName, callback, _delegateMailboxOperation);
        }

        public Mailbox EndExamineMailbox(IAsyncResult result)
        {
            return _delegateMailboxOperation.EndInvoke(result);
        }

        #endregion

        #endregion

        #endregion


    }
}