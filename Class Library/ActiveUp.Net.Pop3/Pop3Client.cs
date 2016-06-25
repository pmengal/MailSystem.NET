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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using ActiveUp.Net.Security;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// POP3 Client extending a System.Net.Sockets.TcpClient to send/receive POP3 command/responses.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class Pop3Client : TcpClient
    {
#if PocketPC
        /// <summary>
        /// Finds PPC Encoding replacing ISO 8859-1 as standard.
        /// </summary>
        internal static System.Text.Encoding PPCEncode
        {
            get
            {
                //Since ISO 8859-1, not supported widelys (Depends on PPC region)
                //We are using Windows Code Page 1252 which is very much nearer to ISO
                //standard.
                return System.Text.Encoding.GetEncoding(1252);
            }
        }
#endif

        #region Events

        #region Event definitions

        /*/// <summary>
		/// Event fired when a certain amount of bytes are read during message or Header retrieval.
		/// </summary>
		public event ActiveUp.Net.Mail.ProgressEventHandler Progress;*/
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

        #endregion

        #region Event triggers and logging

        /*internal void OnProgress(ActiveUp.Net.Mail.ProgressEventArgs e)
		{
			if(Progress!=null) Progress(this,e);
		}*/
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
            Logger.AddEntry(GetType(), "Sending <" + e.Command.Trim() + ">...", 1);
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
            Logger.AddEntry(GetType(), "Retrieving message at index <" + e.MessageIndex + "> out of <" + e.TotalCount + ">...", 2);
        }
        internal void OnMessageRetrieved(MessageRetrievedEventArgs e)
        {
            if (MessageRetrieved != null)
                MessageRetrieved(this, e);
            Logger.AddEntry(GetType(), "Retrieved message at index <" + e.MessageIndex + "> out of <" + e.TotalCount + ">.", 2);
        }
        internal void OnHeaderRetrieving(HeaderRetrievingEventArgs e) 
        {
            if (HeaderRetrieving != null)
                HeaderRetrieving(this, e);
            Logger.AddEntry(GetType(), "Retrieving Header at index <" + e.MessageIndex + "> out of <" + e.TotalCount + ">...", 2);
        }
        internal void OnHeaderRetrieved(HeaderRetrievedEventArgs e)
        {
            if (HeaderRetrieved != null)
                HeaderRetrieved(this, e);
            Logger.AddEntry(GetType(), "Retrieved Header at index <" + e.MessageIndex + "> out of <" + e.TotalCount + ">.", 2);
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

        #endregion

        #endregion

        #region Private fields
        private string host;
#if !PocketPC
        SslStream _sslStream;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Number of messages on the remote POP server.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// int msgCount = pop.MessageCount;
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim msgCount as Integer = pop.MessageCount
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var msgCount:int = pop.MessageCount;
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public int MessageCount { get; set; }
        /// <summary>
        /// Size of all messages on the remote POP server.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// int accountSize = pop.TotalSize;
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim accountSize as Integer = pop.TotalSize
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var accountSize:int = pop.TotalSize;
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public int TotalSize { get; set; }
        #endregion

        #region Delegates and associated private fields

        private delegate string DelegateConnect(string host, int port);
        private DelegateConnect _delegateConnect;

        private delegate string DelegateConnectIPAddress(IPAddress addr, int port);
        private DelegateConnectIPAddress _delegateConnectIPAddress;

        private delegate string DelegateConnectIPAddresses(IPAddress[] addresses, int port);
        private DelegateConnectIPAddresses _delegateConnectIPAddresses;

        private delegate string DelegateConnectAuth(string host, int port, string user, string pass);
        private DelegateConnectAuth _delegateConnectAuth;

        private delegate string DelegateConnectAPOP(string host, int port, string user, string pass);
        private DelegateConnectAPOP _delegateConnectAPOP;

#if !PocketPC
        private delegate string DelegateConnectSslAuth(string host, int port, string username, string password, SslHandShake sslHandShake);
        private DelegateConnectSslAuth _delegateConnectSslAuth;

        private delegate string DelegateConnectSsl(string host, int port, SslHandShake sslHandShake);
        private DelegateConnectSsl _delegateConnectSsl;

        private delegate string DelegateConnectSslIPAddress(IPAddress addr, int port, SslHandShake sslHandShake);
        private DelegateConnectSslIPAddress _delegateConnectSslIPAddress;

        private delegate string DelegateConnectSslIPAddresses(IPAddress[] addresses, int port, SslHandShake sslHandShake);
        private DelegateConnectSslIPAddresses _delegateConnectSslIPAddresses;
#endif

        private delegate string DelegateAuthenticate(string username, string password, SaslMechanism mechanism);
        private DelegateAuthenticate _delegateAuthenticate;

        private delegate string DelegateDisconnect();
        private DelegateDisconnect _delegateDisconnect;

        private delegate string DelegateCommand(string command);
        private DelegateCommand _delegateCommand;

        private delegate byte[] DelegateRetrieveMessage(int messageIndex, bool deleteMessage);
        private DelegateRetrieveMessage _delegateRetrieveMessage;

        private delegate Message DelegateRetrieveMessageObject(int messageIndex, bool deleteMessage);
        private DelegateRetrieveMessageObject _delegateRetrieveMessageObject;

        private delegate void DelegateStoreMessage(int messageIndex, bool deleteMessage, string destinationPath);
        private DelegateStoreMessage _delegateStoreMessage;

        private delegate byte[] DelegateRetrieveHeader(int messageIndex, int numberOfBodyLines);
        private DelegateRetrieveHeader _delegateRetrieveHeader;

        private delegate Header DelegateRetrieveHeaderObject(int messageIndex);
        private DelegateRetrieveHeaderObject _delegateRetrieveHeaderObject;

        private delegate void DelegateStoreHeader(int messageIndex, string destinationPath);
        private DelegateStoreHeader _delegateStoreHeader;

        private delegate void DelegateDeleteMessage(int indexOnServer);
        private DelegateDeleteMessage _delegateDeleteMessage;

        private delegate int DelegateReset();
        private DelegateReset _delegateReset;

        private delegate string DelegateGetUniqueID(int messageIndex);
        private DelegateGetUniqueID _delegateGetUniqueID;

        private delegate List<PopServerUniqueId> DelegateGetUniqueIDs();
        private DelegateGetUniqueIDs _delegateGetUniqueIDs;

        private delegate int DelegateGetMessageSize(int messageIndex);
        private DelegateGetMessageSize _delegateGetMessageSize;

        private delegate void DelegateUpdateStats();
        private DelegateUpdateStats _delegateUpdateStats;

        private delegate void DelegateNoop();
        private DelegateNoop _delegateNoop;

        private delegate bool DelegateCheckAPOP(string host, int port);
        private static DelegateCheckAPOP _delegateCheckAPOP;

        private delegate bool DelegateCheckAPOPString(string host);
        private static DelegateCheckAPOPString _delegateCheckAPOPString;

        private delegate string[] DelegateGetServerCapabilities();
        private DelegateGetServerCapabilities _delegateGetServerCapabilities;

        #endregion

        #region Methods

        #region Private utility methods
#if !PocketPC
        private void DoSslHandShake(SslHandShake sslHandShake)
        {
            _sslStream = new SslStream(base.GetStream(), false, sslHandShake.ServerCertificateValidationCallback, sslHandShake.ClientCertificateSelectionCallback);
            _sslStream.AuthenticateAsClient(sslHandShake.HostName, sslHandShake.ClientCertificates, sslHandShake.SslProtocol, sslHandShake.CheckRevocation);
        }
#endif
        private string _CramMd5(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            //string digest = System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(this.Command("auth cram-md5").Split(' ')[1].Trim(new char[] { '\r', '\n' })));
            byte[] data = Convert.FromBase64String(Command("auth cram-md5").Split(' ')[1].Trim(new char[] { '\r', '\n' }));
#if !PocketPC
            string digest = Encoding.GetEncoding("iso-8859-1").GetString(data, 0, data.Length);
            string response = Command(Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(username + " " + Crypto.HMACMD5Digest(password, digest))));
#else
            string digest = PPCEncode.GetString(data, 0, data.Length);
            string response = this.Command(System.Convert.ToBase64String(PPCEncode.GetBytes(username + " " + ActiveUp.Net.Mail.Crypto.HMACMD5Digest(password, digest))));
#endif
            //string response = this.Command(System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username + " " + ActiveUp.Net.Mail.Crypto.HMACMD5Digest(password, digest))));
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            return response;
        }

        private string _Login(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            Command("auth login");
            //this.Command(System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username)));
            //string response = this.Command(System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(password)));
#if !PocketPC
            Command(Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(username)));
            string response = Command(Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(password)));
#else
            this.Command(System.Convert.ToBase64String(PPCEncode.GetBytes(username)));
            string response = this.Command(System.Convert.ToBase64String(PPCEncode.GetBytes(password)));
#endif
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            return response;
        }

        private string ReadLine()
        {
            OnTcpReading();
            //System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(), System.Text.Encoding.ASCII);
#if !PocketPC
            StreamReader sr = new StreamReader(GetStream(), Encoding.GetEncoding("iso-8859-1"));
#else
            System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(), PPCEncode);
#endif
            string response = sr.ReadLine();
            OnTcpRead(new TcpReadEventArgs(response));
            return response;
        }

        private string StoreToFile(string path, byte[] data)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Close();
            return path;
        }

        private MemoryStream StoreToStream(byte[] data)
        {
            return new MemoryStream(data, 0, data.Length, false, true);
        }

#if PocketPC
        /// <summary>
        /// This functions Injects Sleep wherever required in PocketPC. This is required so that
        /// Less powerful MessagePump in PocketPC gets chance to execute other things.
        /// </summary>
        private void PPCSleep()
        {
            System.Threading.Thread.Sleep(1);
        }
#endif
        private void receiveResponseData(Stream stream)
        {
            byte[] readBuffer = new byte[Client.ReceiveBufferSize];
            int readbytes = 0;

            try
            {
                readbytes = GetStream().Read(readBuffer, 0, readBuffer.Length);
            }
            catch (IOException)
            {
                readbytes = 0;
            }

            while (readbytes > 0)
            {
                stream.Write(readBuffer, 0, readbytes);
                readbytes = 0;
                if (Available > 0)
                    readbytes = GetStream().Read(readBuffer, 0, readBuffer.Length);
            }

        }

        #endregion

        #region Public methods

        #region Connecting, authenticating and disconnecting

        #region Cleartext methods

        /// <summary>
        /// Connects the object with the remote POP server using the given parameters.
        /// </summary>
        /// <param name="host">Remote POP server address.</param>
        /// <returns>The server's welcome greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com");
        /// </code>
        /// </example>
        public string Connect(string host)
        {
            return Connect(host, 110);
        }
        public IAsyncResult BeginConnect(string host, AsyncCallback callback)
        {
            return BeginConnect(host, 110, callback);
        }

        /// <summary>
        /// Connects the object with the remote POP server using the given parameters.
        /// </summary>
        /// <param name="host">Remote POP server address.</param>
        /// <param name="port">The port to be used.</param>
        /// <returns>The server's welcome greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com",8503);
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com",8503)
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com",8503);
        /// </code>
        /// </example>
        public new string Connect(string host, int port)
        {
            this.host = host;
            OnConnecting();
            base.Connect(host, port);
            string response = "";
            response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnect(string host, int port, AsyncCallback callback)
            {
            _delegateConnect = Connect;
            return _delegateConnect.BeginInvoke(host, port, callback, _delegateConnect);
        }

        public new string Connect(IPAddress addr, int port)
        {
            OnConnecting();
            base.Connect(addr, port);
            string response = "";
            response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnect(IPAddress addr, int port, AsyncCallback callback)
        {
            _delegateConnectIPAddress = Connect;
            return _delegateConnectIPAddress.BeginInvoke(addr, port, callback, _delegateConnectIPAddress);
        }


        public new string Connect(IPAddress[] addresses, int port)
        {
            OnConnecting();
#if !PocketPC
            base.Connect(addresses, port);
#else
            if(addresses.Length>0)
                base.Connect(addresses[0], port);
            PPCSleep();
#endif
            string response = "";
            response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback callback)
        {
            _delegateConnectIPAddresses = Connect;
            return _delegateConnectIPAddresses.BeginInvoke(addresses, port, callback, _delegateConnectIPAddresses);
        }

        public string Connect(string host, string username, string password)
        {
            return Connect(host, 110, username, password);
        }
        public IAsyncResult BeginConnect(string host, string username, string password, AsyncCallback callback)
        {
            return BeginConnect(host, 110, username, password, callback);
        }

        public string Connect(string host, int port, string username, string password)
        {
            Connect(host, port);
            return Login(username, password);
        }

        public string Login(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password, host));
            string response = Command("USER " + username);
            string presponse = Command("PASS " + password);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, host, response));
            response = Command("STAT");
            MessageCount = Convert.ToInt32(response.Split(' ')[1]);
            TotalSize = Convert.ToInt32(response.Split(' ')[2]);
            return presponse;
        }

        public IAsyncResult BeginConnect(string host, int port, string username, string password, AsyncCallback callback)
        {
            _delegateConnectAuth = Connect;
            return _delegateConnectAuth.BeginInvoke(host, port, username, password, callback, _delegateConnectAuth);
        }

        public new string EndConnect(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Connects the object with the remote POP server using the given parameters and APOP.
        /// </summary>
        /// <param name="user">Username on the remote POP server.</param>
        /// <param name="pass">Password on the remote POP server.</param>
        /// <param name="host">Remote POP server address.</param>
        /// <example>
        /// This will connect to the remote POP server using APOP.<br /><br />
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.APOPConnect("pop.myisp.com","username","password");
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client()
        /// pop.APOPConnect("pop.myisp.com","username","password")
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.APOPConnect("pop.myisp.com","username","password");
        /// </code>
        /// </example>
        public string APOPConnect(string host, string user, string pass)
        {
            return APOPConnect(host, 110, user, pass);
        }
        public IAsyncResult BeginAPOPConnect(string host, string user, string pass, AsyncCallback callback)
        {
            return BeginAPOPConnect(host, 110, user, pass, callback);
        }

        /// <summary>
        /// Connects the object with the remote POP server using the given parameters and APOP.
        /// </summary>
        /// <param name="user">Username on the remote POP server.</param>
        /// <param name="pass">Password on the remote POP server.</param>
        /// <param name="host">Remote POP server address.</param>
        /// <param name="port">Port to be used.</param>
        /// <example>
        /// This will connect to the remote POP server using APOP.<br /><br />
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.APOPConnect("pop.myisp.com","username","password",8503);
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client()
        /// pop.APOPConnect("pop.myisp.com","username","password",8503)
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.APOPConnect("pop.myisp.com","username","password",8503);
        /// </code>
        /// </example>
        public string APOPConnect(string host, int port, string user, string pass)
        {
            string response = Connect(host, port);
            string presponse = "";
            OnAuthenticating(new AuthenticatingEventArgs(user, pass, host));
            Match timestamp = Regex.Match(response, @"<.+@.+>");
            if (timestamp.Success)
            {
                string encrypted = timestamp.Value + pass;
                presponse = Command("APOP " + user + " " + Crypto.MD5Digest(encrypted));
                OnAuthenticated(new AuthenticatedEventArgs(user, pass, host, response));
                response = Command("STAT");
                MessageCount = Convert.ToInt32(response.Split(' ')[1]);
                TotalSize = Convert.ToInt32(response.Split(' ')[2]);
            }
            return presponse;
        }
        public IAsyncResult BeginAPOPConnect(string host, int port, string username, string password, AsyncCallback callback)
        {
            _delegateConnectAPOP = APOPConnect;
            return _delegateConnectAPOP.BeginInvoke(host, port, username, password, callback, _delegateConnectAPOP);
        }

        public string EndAPOPConnect(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connected; otherwise, <c>false</c>.
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
            return ConnectSsl(host, 995, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 995, new SslHandShake(host), callback);
        }

        public string ConnectSsl(string host, SslHandShake sslHandShake)
        {
            return ConnectSsl(host, 995, sslHandShake);
        }
        public IAsyncResult BeginConnectSsl(string host, SslHandShake sslHandShake, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 995, sslHandShake, callback);
        }
        public string ConnectSsl(string host, int port)
        {
            return ConnectSsl(host, port, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, int port, AsyncCallback callback)
        {
            return BeginConnectSsl(host, port, new SslHandShake(host), callback);
        }

        public string ConnectSsl(string host, int port, SslHandShake sslHandShake)
        {
            this.host = host;
            OnConnecting();
            base.Connect(host, port);
            DoSslHandShake(sslHandShake);
            string response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnectSsl(string host, int port, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSsl = ConnectSsl;
            return _delegateConnectSsl.BeginInvoke(host, port, sslHandShake, callback, _delegateConnectSsl);
        }

        public string ConnectSsl(IPAddress addr, int port, SslHandShake sslHandShake)
        {
            OnConnecting();
            base.Connect(addr, port);
            DoSslHandShake(sslHandShake);
            string response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnectSsl(IPAddress addr, int port, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSslIPAddress = ConnectSsl;
            return _delegateConnectSslIPAddress.BeginInvoke(addr, port, sslHandShake, callback, _delegateConnectSslIPAddress);
        }

        public string ConnectSsl(IPAddress[] addresses, int port, SslHandShake sslHandShake)
        {
            OnConnecting();
            base.Connect(addresses, port);
            DoSslHandShake(sslHandShake);
            string response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }
        public IAsyncResult BeginConnectSsl(IPAddress[] addresses, int port, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSslIPAddresses = ConnectSsl;
            return _delegateConnectSslIPAddresses.BeginInvoke(addresses, port, sslHandShake, callback, _delegateConnectSslIPAddresses);
        }
        public string ConnectSsl(string host, string user, string pass)
        {
            return ConnectSsl(host, 995, user, pass, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, string user, string pass, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 995, user, pass, new SslHandShake(host), callback);
        }
        public string ConnectSsl(string host, string user, string pass, SslHandShake sslHandShake)
        {
            return ConnectSsl(host, 995, user, pass, sslHandShake);
        }
        public IAsyncResult BeginConnectSsl(string host, string user, string pass, SslHandShake sslHandShake, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 995, user, pass, sslHandShake, callback);
        }
        public string ConnectSsl(string host, int port, string user, string pass)
        {
            return ConnectSsl(host, port, user, pass, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, int port, string user, string pass, AsyncCallback callback)
        {
            return BeginConnectSsl(host, port, user, pass, new SslHandShake(host), callback);
        }

        public string ConnectSsl(string host, int port, string user, string pass, SslHandShake sslHandShake)
        {
            ConnectSsl(host, port, sslHandShake);
            return Login(user, pass);
        }
        public IAsyncResult BeginConnectSsl(string host, int port, string user, string pass, SslHandShake sslHandShake, AsyncCallback callback)
        {
            _delegateConnectSslAuth = ConnectSsl;
            return _delegateConnectSslAuth.BeginInvoke(host, port, user, pass, sslHandShake, callback, _delegateConnectSslAuth);
        }

        public string EndConnectSsl(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }
#endif
        #endregion

        #region SASL authentication

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
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com");
        /// pop.Authenticate("user","pass",SASLMechanism.CramMd5);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com")
        /// pop.Authenticate("user","pass",SASLMechanism.CramMd5)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com");
        /// pop.Authenticate("user","pass",SASLMechanism.CramMd5);
        /// pop.Disconnect();
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

        #region Disconnect method

        /// <summary>
        /// Disconnects the client from the remote server.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// //Do some work...
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// 'Do some work...
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// //Do some work...
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public string Disconnect()
        {
            OnDisconnecting();
            string response = Command("QUIT");
            Close();
            OnDisconnected(new DisconnectedEventArgs(response));
            return response;
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
            Close();
        }

        #endregion

        #endregion

        #region Command sending and receiving, stream access

        /// <summary>
        /// Sends the provided string to the server.
        /// </summary>
        /// <param name="command">The string to be sent to the server.</param>
        /// <returns>The server's response.</returns>
        /// <remarks>This method is to be used only with commands that return single-line responses.</remarks>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// string response = pop.Command("XANYCOMMAND anyarguments");
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim response As String = pop.Command("XANYCOMMAND anyarguments")
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var response:string = pop.Command("XANYCOMMAND anyarguments");
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public string Command(string command)
        {
            OnTcpWriting(new TcpWritingEventArgs(command));
            //this.GetStream().Write(System.Text.Encoding.ASCII.GetBytes(command + "\r\n"), 0, command.Length + 2);
#if !PocketPC
            GetStream().Write(Encoding.GetEncoding("iso-8859-1").GetBytes(command + "\r\n"), 0, command.Length + 2);
#else
            this.GetStream().Write(PPCEncode.GetBytes(command + "\r\n"), 0, command.Length + 2);
#endif
            OnTcpWritten(new TcpWrittenEventArgs(command));
            OnTcpReading();
            //System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(), System.Text.Encoding.ASCII);
#if !PocketPC
            StreamReader sr = new StreamReader(GetStream(), Encoding.GetEncoding("iso-8859-1"));
#else
            System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(), PPCEncode);
#endif
            string response = sr.ReadLine();

            if (response == null || !response.StartsWith("+"))
                throw new Pop3Exception("Command \"" + command + "\" failed : " + response);

            OnTcpRead(new TcpReadEventArgs(response));
            return response;
        }
        public IAsyncResult BeginCommand(string command, AsyncCallback callback)
        {
            _delegateCommand = Command;
            return _delegateCommand.BeginInvoke(command, callback, null);
        }

        public string EndCommand(IAsyncResult result)
        {
            return _delegateCommand.EndInvoke(result);
        }

        /// <summary>
        /// Sends the provided string to the server.
        /// </summary>
        /// <param name="command">The string to be sent to the server.</param>
        /// <returns>The server's response.</returns>
        /// <remarks>This method is to be used only with commands that return multi-line responses.</remarks>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// string response = pop.CommandMultiline("XANYCOMMAND anyarguments");
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim response As String = pop.CommandMultiline("XANYCOMMAND anyarguments")
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var response:string = pop.CommandMultiline("XANYCOMMAND anyarguments");
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public string CommandMultiline(string command)
        {
            OnTcpWriting(new TcpWritingEventArgs(command));
#if !PocketPC
            GetStream().Write(Encoding.GetEncoding("iso-8859-1").GetBytes(command + "\r\n"), 0, command.Length + 2);
            OnTcpWritten(new TcpWrittenEventArgs(command));
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(GetStream(), Encoding.GetEncoding("iso-8859-1"));
#else
            this.GetStream().Write(PPCEncode.GetBytes(command + "\r\n"), 0, command.Length + 2);
            this.OnTcpWritten(new ActiveUp.Net.Mail.TcpWrittenEventArgs(command));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(), PPCEncode);
#endif
            string str = string.Empty;
            bool terminationOctetFound = false;

            OnTcpReading();

            str = sr.ReadLine();
            if (!str.StartsWith("+"))
                throw new Pop3Exception("Command " + command + " failed : " + str);

            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
                if (str == ".")
                {
                    terminationOctetFound = true;
                    break;
                }
                if (str.StartsWith(".") && str.Length > 1)
                    str = str.Remove(0, 1);

                sb.Append(str + "\r\n");
            }

            if (!terminationOctetFound)
                throw new Pop3Exception("Command " + command + " failed : " + str);

            OnTcpRead(new TcpReadEventArgs("Read multiline command response."));

            return sb.ToString();
        }

        public IAsyncResult BeginCommandMultiline(string command, AsyncCallback callback)
        {
            _delegateCommand = CommandMultiline;
            return _delegateCommand.BeginInvoke(command, callback, null);
        }

        public string EndCommandMultiline(IAsyncResult result)
        {
            return _delegateCommand.EndInvoke(result);
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
        [Obsolete("use Stream")]
        public new Stream GetStream()
        {
#if !PocketPC
            if (_sslStream != null)
                return _sslStream;
#endif
            return base.GetStream();
        }

        #endregion

        #region Mailbox management

        /// <summary>
        /// Marks the message with the given index for deletion on the remote POP server.
        /// </summary>
        /// <param name="indexOnServer">Index of the message to mark for deletion.</param>
        /// <remarks>
        /// This action can be cancelled by using the Reset() method before disconnection.
        /// <see cref="Reset"/>
        /// </remarks>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("pop.myisp.com","username","password");
        /// pop.DeleteMessage(1);
        /// pop.Disconnect();
        /// //Message 1 deleted.
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client()
        /// pop.Connect("pop.myisp.com","username","password")
        /// pop.DeleteMessage(1)
        /// pop.Disconnect()
        /// 'Message 1 deleted.
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("pop.myisp.com","username","password");
        /// pop.DeleteMessage(1);
        /// pop.Disconnect();
        /// //Message 1 deleted.
        /// </code>
        /// </example>
        public void DeleteMessage(int indexOnServer)
        {
            string response = Command("DELE " + indexOnServer.ToString());
            if (!response.StartsWith("+OK"))
                throw new Pop3Exception("DELE failed : " + response);
        }

        public IAsyncResult BeginDeleteMessage(int indexOnServer, AsyncCallback callback)
        {
            _delegateDeleteMessage = DeleteMessage;
            return _delegateDeleteMessage.BeginInvoke(indexOnServer, callback, null);
        }

        public void EndDeleteMessage(IAsyncResult result)
        {
            _delegateDeleteMessage.EndInvoke(result);
        }

        /// <summary>
        /// Unmarks all messages that were marked for deletion.
        /// </summary>
        /// <returns>The amount of messages unmarked.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// ActiveUp.Net.Mail.Pop3Client pop = new ActiveUp.Net.Mail.Pop3Client();
        /// pop.Connect("pop.myisp.com","username","password");
        /// pop.DeleteMessage(1);
        /// //Message is marked for deletion.
        /// pop.Reset();
        /// //Message won't be deleted.
        /// pop.Disconnect();
        /// //Nothing happened.
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New ActiveUp.Net.Mail.Pop3Client()
        /// pop.Connect("pop.myisp.com","username","password")
        /// pop.DeleteMessage(1)
        /// 'Message is marked for deletion.
        /// pop.Reset()
        /// 'Message won't be deleted.
        /// pop.Disconnect()
        /// 'Nothing happened.
        /// 
        /// JScript.NET
        /// 
        /// var pop:ActiveUp.Net.Mail.Pop3Client = new ActiveUp.Net.Mail.Pop3Client();
        /// pop.Connect("pop.myisp.com","username","password");
        /// pop.DeleteMessage(1);
        /// //Message is marked for deletion.
        /// pop.Reset();
        /// //Message won't be deleted.
        /// pop.Disconnect();
        /// //Nothing happened.
        /// </code>
        /// </example>
        public int Reset()
        {
            string response = Command("RSET");
            if (!response.StartsWith("+OK"))
                throw new Pop3Exception("RSET failed : " + response);
            else
                return Convert.ToInt32(response.Split(' ')[1]);
        }

        public IAsyncResult BeginReset(AsyncCallback callback)
        {
            _delegateReset = Reset;
            return _delegateReset.BeginInvoke(callback, null);
        }

        public int EndReset(IAsyncResult result)
        {
            return _delegateReset.EndInvoke(result);
        }

        #endregion

        #region Message retrieval methods

        #region Message as raw data

        /// <summary>
        /// Retrieves the message at the given index.
        /// </summary>
        /// <param name="messageIndex">The index of the message to be retrieved.</param>
        /// <returns>A byte array containing the message data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// byte[] messageData = pop.RetrieveMessage(1);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim messageData as Byte() = pop.RetrieveMessage(1)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var messageData:byte[] = pop.RetrieveMessage(1);
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveMessage(int messageIndex)
        {
            if (messageIndex > 0 && messageIndex <= MessageCount)
            {
                OnMessageRetrieving(new MessageRetrievingEventArgs(messageIndex, MessageCount));
                OnTcpWriting(new TcpWritingEventArgs("RETR " + messageIndex.ToString()));
                //this.GetStream().Write(System.Text.Encoding.ASCII.GetBytes("RETR " + messageIndex.ToString() + "\r\n"), 0, 7 + messageIndex.ToString().Length);
#if !PocketPC
                GetStream().Write(Encoding.GetEncoding("iso-8859-1").GetBytes("RETR " + messageIndex.ToString() + "\r\n"), 0, 7 + messageIndex.ToString().Length);
                OnTcpWritten(new TcpWrittenEventArgs("RETR " + messageIndex.ToString()));
                OnTcpReading();
#else
                this.GetStream().Write(PPCEncode.GetBytes("RETR " + messageIndex.ToString() + "\r\n"), 0, 7 + messageIndex.ToString().Length);
                this.OnTcpWritten(new ActiveUp.Net.Mail.TcpWrittenEventArgs("RETR " + messageIndex.ToString()));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                this.OnTcpReading();
                System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(), PPCEncode);
#endif
                using (StreamReader sr = new StreamReader(new MemoryStream()))
                {
                    List<int> bytesToRemove = new List<int>();
                    string firstMsgLine = null;
                    bool terminationOctetFound = false;

                    receiveResponseData(sr.BaseStream);
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);

                    string str = sr.ReadLine();

                    if (!str.StartsWith("+OK"))
                        throw new Pop3Exception("RETR failed : " + str);

                    int currentByteIndex = sr.CurrentEncoding.GetByteCount(str) + sr.CurrentEncoding.GetByteCount("\r\n");
                    while (true)
                    {
                        if (sr.EndOfStream)
                        {
                            long streamPos = sr.BaseStream.Position;
                            receiveResponseData(sr.BaseStream);
                            sr.BaseStream.Seek(streamPos, SeekOrigin.Begin);

                            if (sr.EndOfStream)
                                break;
                        }

                        str = sr.ReadLine();
                        if (str == ".")
                        {
                            terminationOctetFound = true;
                            break;
                        }

                        if (str.StartsWith(".") && str.Length > 1)
                        {
                            for (int i = 1; i <= sr.CurrentEncoding.GetByteCount("."); i++)
                                bytesToRemove.Add(currentByteIndex + i);

                            if (firstMsgLine == null)
                                firstMsgLine = str.Remove(0, 1);
                        }

                        if (firstMsgLine == null)
                            firstMsgLine = str;

                        currentByteIndex += sr.CurrentEncoding.GetByteCount(str) + sr.CurrentEncoding.GetByteCount("\r\n");
                    }

                    if (!terminationOctetFound)
                        throw new Pop3Exception("RETR failed : " + str);

                    OnTcpRead(new TcpReadEventArgs("Long message data..."));
#if !PocketPC
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    byte[] response = new byte[sr.BaseStream.Length];
                    sr.BaseStream.Read(response, 0, response.Length);

                    //remove functional chars
                    List<byte> responseList = new List<byte>(response);
                    for (int i = bytesToRemove.Count - 1; i >= 0; i--)
                        responseList.RemoveAt(bytesToRemove[i]);
                    response = responseList.ToArray();

                    //extract message
                    string prefix = sr.CurrentEncoding.GetString(response).Substring(0, sr.CurrentEncoding.GetString(response).IndexOf(firstMsgLine));
                    string suffix = sr.CurrentEncoding.GetString(response).Substring(sr.CurrentEncoding.GetString(response).LastIndexOf("."));
                    byte[] buf = new byte[response.Length - sr.CurrentEncoding.GetByteCount(prefix) - sr.CurrentEncoding.GetByteCount(suffix)];
                    Array.Copy(response, sr.CurrentEncoding.GetByteCount(prefix), buf, 0, buf.Length);

#else
                    byte[] buf = PPCEncode.GetBytes(sb.ToString());
#endif
                    OnMessageRetrieved(new MessageRetrievedEventArgs(buf, messageIndex, MessageCount));
                    return buf;
                }
            }
            else
                throw new Pop3Exception("The specified message index is invalid. Please specify an index that is greater than 0 and less or equal that MessageCount.");
        }

        public IAsyncResult BeginRetrieveMessage(int messageIndex, AsyncCallback callback)
        {
            return BeginRetrieveMessage(messageIndex, false, callback);
        }

        /// <summary>
        /// Retrieves the message at the given index.
        /// </summary>
        /// <param name="messageIndex">The index of the message to be retrieved.</param>
        /// <param name="deleteMessage">If true, the message will be deleted after it has been retrieved.</param>
        /// <returns>A byte array containing the message data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// byte[] messageData = pop.RetrieveMessage(1,true);
        /// pop.Disconnect();
        /// //Message 1 is deleted.
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim messageData as Byte() = pop.RetrieveMessage(1,True)
        /// pop.Disconnect()
        /// 'Message 1 is deleted.
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var messageData:byte[] = pop.RetrieveMessage(1,true);
        /// pop.Disconnect();
        /// //Message 1 is deleted.
        /// </code>
        /// </example>
        public byte[] RetrieveMessage(int messageIndex, bool deleteMessage)
        {
            byte[] buffer = RetrieveMessage(messageIndex);
            if (deleteMessage)
                DeleteMessage(messageIndex);
            return buffer;
        }
        public IAsyncResult BeginRetrieveMessage(int messageIndex, bool deleteMessage, AsyncCallback callback)
        {
            _delegateRetrieveMessage = RetrieveMessage;
            return _delegateRetrieveMessage.BeginInvoke(messageIndex, deleteMessage, callback, null);
        }

        public byte[] EndRetrieveMessage(IAsyncResult result)
        {
            return _delegateRetrieveMessage.EndInvoke(result);
        }

        #endregion

        #region Message as object

        /// <summary>
        /// Retrieves the message at the given index.
        /// </summary>
        /// <param name="messageIndex">The index of the message to be retrieved.</param>
        /// <returns>A Message object representing the message.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// Message message = pop.RetrieveMessageObject(1);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim message as Message = pop.RetrieveMessageObject(1)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var message:Message = pop.RetrieveMessageObject(1);
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public Message RetrieveMessageObject(int messageIndex)
        {
            return Parser.ParseMessage(RetrieveMessage(messageIndex));
        }
        public IAsyncResult BeginRetrieveMessageObject(int messageIndex, AsyncCallback callback)
        {
            return BeginRetrieveMessageObject(messageIndex, false, callback);
        }

        /// <summary>
        /// Retrieves the message at the given index.
        /// </summary>
        /// <param name="messageIndex">The index of the message to be retrieved.</param>
        /// <param name="deleteMessage">If true, the message will be deleted after it has been retrieved.</param>
        /// <returns>A Message object representing the message.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// Message message = pop.RetrieveMessageObject(1);
        /// pop.Disconnect();
        /// //Message 1 is deleted.
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim message as Message = pop.RetrieveMessageObject(1)
        /// pop.Disconnect()
        /// 'Message 1 is deleted.
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var message:Message = pop.RetrieveMessageObject(1);
        /// pop.Disconnect();
        /// //Message 1 is deleted.
        /// </code>
        /// </example>
        public Message RetrieveMessageObject(int messageIndex, bool deleteMessage)
        {
            return Parser.ParseMessage(RetrieveMessage(messageIndex, deleteMessage));
        }
        public IAsyncResult BeginRetrieveMessageObject(int messageIndex, bool deleteMessage, AsyncCallback callback)
        {
            _delegateRetrieveMessageObject = RetrieveMessageObject;
            return _delegateRetrieveMessageObject.BeginInvoke(messageIndex, deleteMessage, callback, null);
        }

        public Message EndRetrieveMessageObject(IAsyncResult result)
        {
            return _delegateRetrieveMessageObject.EndInvoke(result);
        }

        #endregion

        #region Store message data to a file

        /// <summary>
        /// Retrieves and stores the message at the specified index to the specified path.
        /// Deletes the message once retrieval operation is complete.
        /// </summary>
        /// <param name="messageIndex">Index of the message to be retrieved.</param>
        /// <param name="deleteMessage">If true, the message will be deleted after it has been retrieved.</param>
        /// <param name="destinationPath">The path where the message has to be stored.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// pop.StoreMessage(1,"C:\\My headers\\myheader.eml");
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// pop.StoreMessage(1,"C:\My headers\myheader.eml")
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// pop.StoreMessage(1,"C:\\My headers\\myheader.eml");
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public void StoreMessage(int messageIndex, bool deleteMessage, string destinationPath)
        {
            StoreToFile(destinationPath, RetrieveMessage(messageIndex, deleteMessage));
        }
        public IAsyncResult BeginStoreMessage(int messageIndex, bool deleteMessage, string destinationPath, AsyncCallback callback)
        {
            _delegateStoreMessage = StoreMessage;
            return _delegateStoreMessage.BeginInvoke(messageIndex, deleteMessage, destinationPath, callback, null);
        }

        public void EndStoreMessage(IAsyncResult result)
        {
            _delegateStoreMessage.EndInvoke(result);
        }

        #endregion

        #endregion

        #region Header retrieval methods

        #region Header as raw data

        public byte[] RetrieveHeader(int messageIndex)
        {
            return RetrieveHeader(messageIndex, 0);
        }
        public IAsyncResult BeginRetrieveHeader(int messageIndex, AsyncCallback callback)
        {
            return BeginRetrieveHeader(messageIndex, 0, callback);
        }

        /// <summary>
        /// Retrieves the Header of the message at the given index, plus a given number of lines beyond the Header limit.
        /// </summary>
        /// <param name="messageIndex">Index of the Header to be retrieved.</param>
        /// <param name="numberOfBodyLines">Number of lines to retrieve after the Header separation.</param>
        /// <returns>A byte array containing the Header data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// byte[] headerData = pop.RetrieveHeader(1,10);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim headerData as Byte() = pop.RetrieveHeader(1,10)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var headerData:byte[] = pop.RetrieveHeader(1,10);
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveHeader(int messageIndex, int numberOfBodyLines)
        {
            OnHeaderRetrieving(new HeaderRetrievingEventArgs(messageIndex, MessageCount));
            string header = CommandMultiline("TOP " + messageIndex.ToString() + " " + numberOfBodyLines.ToString());
            //header = header.Replace(header.Split('\n')[0],"").TrimStart('\n');
            //byte[] buf = System.Text.Encoding.ASCII.GetBytes(header);
#if !PocketPC
            byte[] buf = Encoding.GetEncoding("iso-8859-1").GetBytes(header);
#else
            byte[] buf = PPCEncode.GetBytes(header);
#endif
            OnHeaderRetrieved(new HeaderRetrievedEventArgs(buf, messageIndex, MessageCount));
            return buf;
        }
        public IAsyncResult BeginRetrieveHeader(int messageIndex, int numberOfBodyLines, AsyncCallback callback)
        {
            _delegateRetrieveHeader = RetrieveHeader;
            return _delegateRetrieveHeader.BeginInvoke(messageIndex, numberOfBodyLines, callback, null);
        }

        public byte[] EndRetrieveHeader(IAsyncResult result)
        {
            return _delegateRetrieveHeader.EndInvoke(result);
        }

        #endregion

        #region Header as object

        /// <summary>
        /// Retrieves the Header of the message at the given index.
        /// </summary>
        /// <param name="messageIndex">Index of the Header to be retrieved.</param>
        /// <returns>A Header object representing the header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// Header headerData = pop.RetrieveHeaderObject(1);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim headerData as Header = pop.RetrieveHeaderObject(1)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// var headerData:Header = pop.RetrieveHeaderObject(1);
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public Header RetrieveHeaderObject(int messageIndex)
        {
            return Parser.ParseHeader(RetrieveHeader(messageIndex));
        }
        public IAsyncResult BeginRetrieveHeaderObject(int messageIndex, AsyncCallback callback)
        {
            _delegateRetrieveHeaderObject = RetrieveHeaderObject;
            return _delegateRetrieveHeaderObject.BeginInvoke(messageIndex, callback, null);
        }

        public Header EndRetrieveHeaderObject(IAsyncResult result)
        {
            return _delegateRetrieveHeaderObject.EndInvoke(result);
        }

        #endregion

        #region Store header data to a file

        /// <summary>
        /// Retrieves and stores the message Header at the specified index to the specified path.
        /// </summary>
        /// <param name="messageIndex">Index of the message Header to be retrieved.</param>
        /// <param name="destinationPath">The path where the Header has to be stored.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// pop.StoreHeader(1,"C:\\My headers\\myheader.eml");
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// pop.StoreHeader(1,"C:\My headers\myheader.eml")
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// pop.StoreHeader(1,"C:\\My headers\\myheader.eml");
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public void StoreHeader(int messageIndex, string destinationPath)
        {
            StoreToFile(destinationPath, RetrieveHeader(messageIndex));
        }
        public IAsyncResult BeginStoreHeader(int messageIndex, string destinationPath, AsyncCallback callback)
        {
            _delegateStoreHeader = StoreHeader;
            return _delegateStoreHeader.BeginInvoke(messageIndex, destinationPath, callback, null);
        }

        public void StoreHeader(int messageIndex, int numberOfBodyLines, string destinationPath)
        {
            StoreToFile(destinationPath, RetrieveHeader(messageIndex, numberOfBodyLines));
        }
        public IAsyncResult BeginStoreHeader(int messageIndex, int numberOfBodyLines, string destinationPath, AsyncCallback callback)
        {
            _delegateStoreHeader = StoreHeader;
            return _delegateStoreHeader.BeginInvoke(messageIndex, destinationPath, callback, null);
        }

        public void EndStoreHeader(IAsyncResult result)
        {
            _delegateStoreHeader.EndInvoke(result);
        }

        #endregion

        #endregion

        #region Utility commands

        /// <summary>
        /// Issues a UIDL command and retrieves the message's unique Id (assigned by the server).
        /// </summary>
        /// <param name="messageIndex">The message's index.</param>
        /// <returns>The message's unique Id.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// string uniqueId = pop.UniqueId(1);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim uniqueId As String = pop.UniqueId(1)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// uniqueId:string = pop.UniqueId(1);
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public string GetUniqueId(int messageIndex)
        {
            string resp = Command("UIDL " + messageIndex.ToString());
            return resp.Split(' ')[2];
        }

        public IAsyncResult BeginGetUniqueId(int messageIndex, AsyncCallback callback)
        {
            _delegateGetUniqueID = GetUniqueId;
            return _delegateGetUniqueID.BeginInvoke(messageIndex, callback, null);
        }

        public string EndGetUniqueId(IAsyncResult result)
        {
            return _delegateGetUniqueID.EndInvoke(result);
        }

        /// <summary>
        /// Issues a UIDL command and retrieves all message unique Ids (assigned by the server).
        /// </summary>
        /// <returns>A list of a structure containing the unique Id of the messages and their index.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// System.Collections.Generic.List<PopServerUniqueId> uids = pop.UniqueIds();
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim uniqueId As System.Collections.Generic.List(Of PopServerUniqueId) = pop.UniqueIds()
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// uniqueId:System.Collections.Generic.List<PopServerUniqueId> = pop.UniqueIds();
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public List<PopServerUniqueId> GetUniqueIds()
        {
            List<PopServerUniqueId> uids = new List<PopServerUniqueId>();
            string ret = CommandMultiline("UIDL");
            string[] lines = ret.Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                PopServerUniqueId pse = new PopServerUniqueId();
                pse.Index = int.Parse(parts[0]);
                pse.UniqueId = parts[1];
                uids.Add(pse);
            }

            return uids;
        }

        public IAsyncResult BeginGetUniqueIds(AsyncCallback callback)
        {
            _delegateGetUniqueIDs = GetUniqueIds;
            return _delegateGetUniqueIDs.BeginInvoke(callback, null);
        }

        public List<PopServerUniqueId> EndGetUniqueIds(IAsyncResult result)
        {
            return _delegateGetUniqueIDs.EndInvoke(result);
        }

        /// <summary>
        /// Retreives message index on the pop server from its internal unique Id.
        /// </summary>
        /// <param name="serverUniqueId">The given message unique Id to retreive.</param>
        /// <returns>The index of the message on the pop server, 0 if not found.</returns>
        public int GetMessageIndex(string serverUniqueId)
        {
            List<PopServerUniqueId> uids = GetUniqueIds();
            foreach (PopServerUniqueId uid in uids)
                if (uid.UniqueId == serverUniqueId)
                    return uid.Index;
            return 0;
        }

        /// <summary>
        /// Indicates if the uniqueId exists on the server
        /// </summary>
        /// <param name="serverUniqueId">The given message unique Id to retreive.</param>
        /// <returns>True if unique Id exists, False if it doesn't.</returns>
        public bool UniqueIdExists(string serverUniqueId)
        {
            return GetMessageIndex(serverUniqueId) != 0;
        }

        /// <summary>
        /// Structure containing a uniqueId for a message and its associated index on the pop server
        /// </summary>
        public class PopServerUniqueId
        {
            public int Index { get; set; }
            public string UniqueId { get; set; }
        }

        /// <summary>
        /// Returns the size of the message at the given index.
        /// </summary>
        /// <param name="messageIndex">Index of the messages.</param>
        /// <returns>The size of the message at the given index.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// int uniqueId = pop.GetMessageSize(1);
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// Dim uniqueId As Integer = pop.GetMessageSize(1)
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// uniqueId:int = pop.GetMessageSize(1);
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public int GetMessageSize(int messageIndex)
        {
            string myline = Command("LIST " + messageIndex.ToString());
            //string myline = this.ReadLine();
            if (!myline.StartsWith("+OK"))
                throw new Pop3Exception("LIST failed : " + myline);
            else
                return Convert.ToInt32(myline.Split(' ')[2]);
        }

        public IAsyncResult BeginGetMessageSize(int messageIndex, AsyncCallback callback)
        {
            _delegateGetMessageSize = GetMessageSize;
            return _delegateGetMessageSize.BeginInvoke(messageIndex, callback, null);
        }

        public int EndGetMessageSize(IAsyncResult result)
        {
            return _delegateGetMessageSize.EndInvoke(result);
        }

        public void UpdateStats()
        {
            string response = Command("STAT");
            //ActiveUp.Net.Mail.Logger.AddEntry(response);
            MessageCount = int.Parse(response.Split(' ')[1]);
            TotalSize = int.Parse(response.Split(' ')[2]);
        }

        public IAsyncResult BeginUpdateStats(AsyncCallback callback)
        {
            _delegateUpdateStats = UpdateStats;
            return _delegateUpdateStats.BeginInvoke(callback, null);
        }

        public void EndUpdateStats(IAsyncResult result)
        {
            _delegateUpdateStats.EndInvoke(result);
        }

        /// <summary>
        /// Performs a NOOP command on the server. The aim of this command is to keep the connection alive.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Pop3Client pop = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// pop.Noop();
        /// pop.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim pop As New Pop3Client
        /// pop.Connect("mail.myhost.com","user","pass")
        /// pop.Noop()
        /// pop.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var pop:Pop3Client = new Pop3Client();
        /// pop.Connect("mail.myhost.com","user","pass");
        /// pop.Noop();
        /// pop.Disconnect();
        /// </code>
        /// </example>
        public void Noop()
        {
            OnNooping();
            Command("NOOP");
            OnNooped();
        }

        public IAsyncResult BeginNoop(AsyncCallback callback)
        {
            _delegateNoop = Noop;
            return _delegateNoop.BeginInvoke(callback, null);
        }

        public void EndNoop(IAsyncResult result)
        {
            _delegateNoop.EndInvoke(result);
        }

        /// <summary>
        /// Checks if specified host has APOP capability.
        /// </summary>
        /// <param name="host">Host to be checked.</param>
        /// <param name="port">Port to connect on to the host.</param>
        /// <returns>True is remote server has APOP, otherwise false.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// bool serverHasAPOP = Pop3Client.CheckAPOP("mail.myhost.com",8503);
        /// 
        /// VB.NET
        /// 
        /// Dim serverHasAPOP As Boolen = Pop3Client.CheckAPOP("mail.myhost.com",8503)
        /// 
        /// JScript.NET
        /// 
        /// var serverHasAPOP:bool Pop3Client.CheckAPOP("mail.myhost.com",8503);
        /// </code>
        /// </example>
        public static bool CheckAPOP(string host, int port)
        {
            TcpClient _tcp = new TcpClient(host, port);
            byte[] buf = new byte[256];
            _tcp.GetStream().Read(buf, 0, 256);
            //string resp = System.Text.Encoding.ASCII.GetString(buf);
#if !PocketPC
            string resp = Encoding.GetEncoding("iso-8859-1").GetString(buf, 0, buf.Length);
#else
            string resp = PPCEncode.GetString(buf, 0, buf.Length);
#endif
            _tcp.Close();
            if (resp.IndexOf("<") != -1 && resp.IndexOf(">") != -1 && (resp.IndexOf("@") < resp.IndexOf(">")) && (resp.IndexOf("@") > resp.IndexOf("<")))
                return true;
            else
                return false;
        }

        /// <see cref="CheckAPOP(string, int)"/>
        public static IAsyncResult BeginCheckAPOP(string host, int port, AsyncCallback callback)
        {
            _delegateCheckAPOP = CheckAPOP;
            return _delegateCheckAPOP.BeginInvoke(host, port, callback, _delegateCheckAPOP);
        }


        /// <summary>
        /// Checks if specified host has APOP capability.
        /// </summary>
        /// <param name="host">Host to be checked.</param>
        /// <returns>True is remote server has APOP, otherwise false.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// bool serverHasAPOP = Pop3Client.CheckAPOP("mail.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim serverHasAPOP As Boolen = Pop3Client.CheckAPOP("mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// var serverHasAPOP:bool Pop3Client.CheckAPOP("mail.myhost.com");
        /// </code>
        /// </example>
        public static bool CheckAPOP(string host)
        {
            return CheckAPOP(host, 110);
        }

        /// <see cref="CheckAPOP(string)"/>
        public static IAsyncResult BeginCheckAPOP(string host, AsyncCallback callback)
        {
            _delegateCheckAPOPString = CheckAPOP;
            return _delegateCheckAPOPString.BeginInvoke(host, callback, _delegateCheckAPOPString);
        }

        /// <see cref="CheckAPOP(string)"/>
        public static bool EndCheckAPOP(IAsyncResult result)
        {
            return (bool)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }


        /// <summary>
        /// Gets the server capabilities.
        /// </summary>
        /// <remarks>Server capabilities are returned as an array of lines. Interpretation is left to the user.</remarks>
        /// <returns>An array of strings containing the server capabilities.</returns>
        public string[] GetServerCapabilities()
        {
#if !PocketPC
            return CommandMultiline("CAPA").Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
#else
            return this.CommandMultiline("CAPA").Replace("\r", "").Split(new char[] { '\n' });
#endif

        }

        /// <see cref="GetServerCapabilities"/>
        public IAsyncResult BeginGetServerCapabilities(AsyncCallback callback)
        {
            _delegateGetServerCapabilities = GetServerCapabilities;
            return _delegateGetServerCapabilities.BeginInvoke(callback, null);
        }

        /// <see cref="GetServerCapabilities"/>
        public string[] EndGetServerCapabilities(IAsyncResult result)
        {
            return _delegateGetServerCapabilities.EndInvoke(result);
        }

        #endregion

        #endregion

        #endregion
    }
}