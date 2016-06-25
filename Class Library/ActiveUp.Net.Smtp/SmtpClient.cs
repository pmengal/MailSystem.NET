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
using ActiveUp.Net.Dns;
using ActiveUp.Net.Security;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// Allows communication with an SMTP server.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class SmtpClient : TcpClient
    {
        #region Private fields

        static long _UIDcounter;
#if !PocketPC
        SslStream _sslStream;
#endif
        #endregion

        #region Events
        #region Event definitions

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
        /// Event fired when attempting to connect to the remote server using the specified host.
        /// </summary>
        public event ConnectingEventHandler Connecting;
        /// <summary>
        /// Event fired when the object is connected to the remote server or when connection failed.
        /// </summary>
        public event ConnectedEventHandler Connected;
        /// <summary>
        /// Event fired when attempting to disconnect from the remote server.
        /// </summary>
        public event DisconnectingEventHandler Disconnecting;
        /// <summary>
        /// Event fired when the object disconnected from the remote server.
        /// </summary>
        public event DisconnectedEventHandler Disconnected;
        /// <summary>
        /// Event fired when authentication starts.
        /// </summary>
        public event AuthenticatingEventHandler Authenticating;
        /// <summary>
        /// Event fired when authentication completed.
        /// </summary>
        public event AuthenticatedEventHandler Authenticated;
        /// <summary>
        /// Event fired when a message is being sent.
        /// </summary>
        public event MessageSendingEventHandler MessageSending;
        /// <summary>
        /// Event fired when a message has been sent.
        /// </summary>
        public event MessageSentEventHandler MessageSent;

        #endregion

        #region Event triggers and logging

        internal void OnTcpWriting(TcpWritingEventArgs e)
        {
            if(TcpWriting!=null)
                TcpWriting(this,e);
            Logger.AddEntry(GetType(), "Sending " + e.Command+"...",1);
        }
        internal void OnTcpWritten(TcpWrittenEventArgs e)
        {
            if(TcpWritten!=null)
                TcpWritten(this,e);
            Logger.AddEntry(GetType(), "Sent " + e.Command+".",1);
        }
        internal void OnTcpReading()
        {
            if(TcpReading!=null)
                TcpReading(this);
            Logger.AddEntry(GetType(), "Reading...", 1);
        }
        internal void OnTcpRead(TcpReadEventArgs e)
        {
            if(TcpRead!=null)
                TcpRead(this,e);
            Logger.AddEntry(GetType(), "Read " + e.Response+".",1);
        }
        internal void OnDisconnecting()
        {
            if(Disconnecting!=null)
                Disconnecting(this);
            Logger.AddEntry(GetType(), "Disconnecting...", 2);
        }
        internal void OnDisconnected(DisconnectedEventArgs e)
        {
            if(Disconnected!=null)
                Disconnected(this,e);
            Logger.AddEntry(GetType(), "Disconnected.", 2);
        }
        internal void OnConnecting()
        {
            if(Connecting!=null)
                Connecting(this);
            Logger.AddEntry(GetType(), "Connecting...", 2);
        }
        internal void OnConnected(ConnectedEventArgs e)
        {
            if(Connected!=null)
                Connected(this,e);
            Logger.AddEntry(GetType(), "Connected. Server replied : " + e.ServerResponse+"...",2);
        }
        internal void OnAuthenticating(AuthenticatingEventArgs e)
        {
            if(Authenticating!=null)
                Authenticating(this,e);
            Logger.AddEntry(GetType(), "Authenticating as " + e.Username+" on "+e.Host+"...",2);
        }
        internal void OnAuthenticated(AuthenticatedEventArgs e)
        {
            if(Authenticated!=null)
                Authenticated(this,e);
            Logger.AddEntry(GetType(), "Authenticated as " + e.Username+" on "+e.Host+".",2);
        }
        internal void OnMessageSending(MessageSendingEventArgs e)
        {
            if(MessageSending!=null)
                MessageSending(this,e);
            Logger.AddEntry(GetType(), "Sending message with subject : " + e.Message.Subject+"...",2);
        }
        internal void OnMessageSent(MessageSentEventArgs e)
        {
            if(MessageSent!=null)
                MessageSent(this,e);
            Logger.AddEntry(GetType(), "Sent message with subject : " + e.Message.Subject+"...",2);
        }

        #endregion

        #endregion

        #region Delegates and associated private fields

        // Methods associated wit a same command shouldn't be called concurrently.
        // So one delegate per command is enough, when parameters and return types allow it.

        #region Connection management and SMTP implementation

        private delegate string DelegateConnect(string host, int port);
        private DelegateConnect _delegateConnect;

        private delegate string DelegateConnectIPAddress(IPAddress addr, int port);
        private DelegateConnectIPAddress _delegateConnectIPAddress;

        private delegate string DelegateConnectIPAddresses(IPAddress[] addresses, int port);
        private DelegateConnectIPAddresses _delegateConnectIPAddresses;

#if !PocketPC
        private delegate string DelegateConnectSsl(string host, int port, SslHandShake sslHandShake);
        private DelegateConnectSsl _delegateConnectSsl;

        private delegate string DelegateConnectSslIPAddress(IPAddress addr, int port, SslHandShake sslHandShake);
        private DelegateConnectSslIPAddress _delegateConnectSslIPAddress;

        private delegate string DelegateConnectSslIPAddresses(IPAddress[] addresses, int port, SslHandShake sslHandShake);
        private DelegateConnectSslIPAddresses _delegateConnectSslIPAddresses;
#endif

        private delegate string DelegateDisconnect();
        private DelegateDisconnect _delegateDisconnect;

        private delegate string DelegateCommand(string command, int expectedResponseCode);
        private DelegateCommand _delegateCommand;

        private delegate string DelegateAuthenticate(string username, string password, SaslMechanism mechanism);
        private DelegateAuthenticate _delegateAuthenticate;

        private delegate string DelegateData(string data);
        private DelegateData _delegateData;

        private delegate string DelegateHelo(string domain);
        private DelegateHelo _delegateHelo;

        private delegate string DelegateEhlo(string domain);
        private DelegateEhlo _delegateEhlo;

        private delegate string DelegateHelp();
        private DelegateHelp _delegateHelp;

        private delegate string DelegateMailFrom(string address);
        private DelegateMailFrom _delegateMailFrom;

        private delegate string DelegateRcptTo(string address);
        private DelegateRcptTo _delegateRcptTo;

        private delegate void DelegateRcptToAddressCollection(AddressCollection addresses);
        private DelegateRcptToAddressCollection _delegateRcptToAddressCollection;

        private delegate string DelegateNoop();
        private DelegateNoop _delegateNoop;
            
        private delegate bool DelegateVrfy(string address);
        private DelegateVrfy _delegateVrfy;

        private delegate AddressCollection DelegateVrfyAddresCollection(AddressCollection addresses);
        private DelegateVrfyAddresCollection _delegateVrfyAddressCollection;

        #endregion

        // Sending methods might be called concurrently, so we need one delegate per overload.

        #region Direct Send

        private delegate int DelegateDirectSendMessageCollection(MessageCollection messages);
        private static DelegateDirectSendMessageCollection _delegateDirectSendMessageCollection;

        private delegate int DelegateDirectSendMessageCollectionServerCollection(MessageCollection messages, ServerCollection dnsServers);
        private static DelegateDirectSendMessageCollectionServerCollection _delegateDirectSendMessageCollectionServerCollection;

        private delegate int DelegateDirectSendMessageCollectionString(MessageCollection messages, string dnsHost);
        private static DelegateDirectSendMessageCollectionString _delegateDirectSendMessageCollectionString;

        private delegate int DelegateDirectSendMessageCollectionStringInt(MessageCollection messages, string dnsHost, int dnsPort);
        private static DelegateDirectSendMessageCollectionStringInt _delegateDirectSendMessageCollectionStringInt;

        private delegate string DelegateDirectSendMessage(Message message);
        private static DelegateDirectSendMessage _delegateDirectSendMessage;

        private delegate string DelegateDirectSendMessageServerCollection(Message message, ServerCollection collection);
        private static DelegateDirectSendMessageServerCollection _delegateDirectSendMessageServerCollection;

        private delegate string DelegateDirectSendMessageString(Message message, string dnsHost);
        private static DelegateDirectSendMessageString _delegateDirectSendMessageString;

        private delegate string DelegateDirectSendMessageStringInt(Message message, string dnsHost, int dnsPort);
        private static DelegateDirectSendMessageStringInt _delegateDirectSendMessageStringInt;

        private delegate int DelegateDirectSendMessageCollectionSmtpExceptionCollection(MessageCollection message, ref SmtpExceptionCollection errors);
        private static DelegateDirectSendMessageCollectionSmtpExceptionCollection _delegateDirectSendMessageCollectionSmtpExceptionCollection;

        #endregion

        #region Send Queued

        private delegate void DelegateSendQueuedMessage(Message message, string spoolDirectory, QueuingService service);
        private static DelegateSendQueuedMessage _delegateSendQueuedMessage;

        private delegate void DelegateSendQueuedMessageCollection(MessageCollection messages, string spoolDirectory, QueuingService service);
        private static DelegateSendQueuedMessageCollection _delegateSendQueuedMessageCollection;

        #endregion

        #region Send with relay servers

        private delegate bool DelegateSendMessageServerCollection(Message message, ServerCollection servers);
        private static DelegateSendMessageServerCollection _delegateSendMessageServerCollection;

        private delegate bool DelegateSendMessageServerCollectionString(Message message, ServerCollection servers, out string serverMessage);
        private static DelegateSendMessageServerCollectionString _delegateSendMessageServerCollectionString;

        private delegate bool DelegateSendMessageString(Message message, string server);
        private static DelegateSendMessageString _delegateSendMessageString;

        private delegate bool DelegateSendMessageStringInt(Message message, string host, int port);
        private static DelegateSendMessageStringInt _delegateSendMessageStringInt;

        private delegate bool DelegateSendMessageStringStringStringSaslMechanism(Message message, string host, string username, string password, SaslMechanism mechanism);
        private static DelegateSendMessageStringStringStringSaslMechanism _delegateSendMessageStringStringStringSaslMechanism;

        private delegate bool DelegateSendMessageStringIntStringStringSaslMechanism(Message message, string host, int port, string username, string password, SaslMechanism mechanism);
        private static DelegateSendMessageStringIntStringStringSaslMechanism _delegateSendMessageStringIntStringStringSaslMechanism;

        private delegate int DelegateSendMessageCollectionString(MessageCollection messages, string server);
        private static DelegateSendMessageCollectionString _delegateSendMessageCollectionString;

        private delegate int DelegateSendMessageCollectionStringInt(MessageCollection messages, string server, int port);
        private static DelegateSendMessageCollectionStringInt _delegateSendMessageCollectionStringInt;

        private delegate int DelegateSendMessageCollectionStringIntSmtpExceptionCollection(MessageCollection messages, string server, int port, ref SmtpExceptionCollection errors);
        private static DelegateSendMessageCollectionStringIntSmtpExceptionCollection _delegateSendMessageCollectionStringIntSmtpExceptionCollection;

        private delegate int DelegateSendMessageCollectionServerCollection(MessageCollection messages, ServerCollection servers);
        private static DelegateSendMessageCollectionServerCollection _delegateSendMessageCollectionServerCollection;

        private delegate int DelegateSendMessageCollectionStringSmtpExceptionCollection(MessageCollection messages, string server, ref SmtpExceptionCollection errors);
        private static DelegateSendMessageCollectionStringSmtpExceptionCollection _delegateSendMessageCollectionStringSmtpExceptionCollection;

        private delegate int DelegateSendMessageCollectionServerCollectionSmtpExceptionCollection(MessageCollection messages, ServerCollection servers, ref SmtpExceptionCollection errors);
        private static DelegateSendMessageCollectionServerCollectionSmtpExceptionCollection _delegateSendMessageCollectionServerCollectionSmtpExceptionCollection;

        private delegate int DelegateSendMessageCollectionStringStringStringSaslMechanism(MessageCollection messages, string host, string username, string password, SaslMechanism mechanism);
        private static DelegateSendMessageCollectionStringStringStringSaslMechanism _delegateSendMessageCollectionStringStringStringSaslMechanism;

        private delegate int DelegateSendMessageCollectionStringStringStringSaslMechanismSmtpExceptionCollection(MessageCollection messages, string host, string username, string password, SaslMechanism mechanism, ref SmtpExceptionCollection errors);
        private static DelegateSendMessageCollectionStringStringStringSaslMechanismSmtpExceptionCollection _delegateSendMessageCollectionStringStringStringSaslMechanismSmtpExceptionCollection;

        private delegate int DelegateSendMessageCollectionStringIntStringStringSaslMechanism(MessageCollection messages, string host, int port, string username, string password, SaslMechanism mechanism);
        private static DelegateSendMessageCollectionStringIntStringStringSaslMechanism _delegateSendMessageCollectionStringIntStringStringSaslMechanism;

        private delegate int DelegateSendMessageCollectionStringIntStringStringSaslMechanismSmtpExceptionCollection(MessageCollection messages, string host, int port, string username, string password, SaslMechanism mechanism, ref SmtpExceptionCollection errors);
        private static DelegateSendMessageCollectionStringIntStringStringSaslMechanismSmtpExceptionCollection _delegateSendMessageCollectionStringIntStringStringSaslMechanismSmtpExceptionCollection;

        #endregion

        #region Quick Direct Send

        private delegate void DelegateQuickDirectSend(string from, string to, string subject, string textBody);
        private static DelegateQuickDirectSend _delegateQuickDirectSend;

        private delegate void DelegateQuickDirectSendAttach(string from, string to, string subject, string body, BodyFormat bodyFormat, string attachmentPath);
        private static DelegateQuickDirectSendAttach _delegateQuickDirectSendAttach;

        #endregion

        #region Quick Send

        private delegate void DelegateQuickSend(string from, string to, string subject, string textBody, string smtpServer);
        private static DelegateQuickSend _delegateQuickSend;

        private delegate void DelegateQuickSendAttach(string from, string to, string subject, string body, BodyFormat bodyFormat, string attachmentPath, string smtpServer);
        private static DelegateQuickSendAttach _delegateQuickSendAttach;

        #endregion

        #endregion

        #region Methods

        #region Private utility methods

        private string _CramMd5(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            byte[] data = Convert.FromBase64String(Command("auth cram-md5", 334).Split(' ')[1].Trim(new char[] { '\r', '\n' }));
            string digest = Encoding.ASCII.GetString(data,0,data.Length);
            string response = Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(username + " " + Crypto.HMACMD5Digest(password, digest))), 235);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            return response;
        }
            
        private string _Login(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username, password));
            Command("auth login", 334);
            Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(username)), 334);
            string response = Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)), 235);
            OnAuthenticated(new AuthenticatedEventArgs(username, password, response));
            return response;
        }
#if !PocketPC
        private void DoSslHandShake(SslHandShake sslHandShake)
        {
            _sslStream = new SslStream(base.GetStream(), false, sslHandShake.ServerCertificateValidationCallback, sslHandShake.ClientCertificateSelectionCallback);
            _sslStream.AuthenticateAsClient(sslHandShake.HostName, sslHandShake.ClientCertificates, sslHandShake.SslProtocol, sslHandShake.CheckRevocation);
        }
#endif
        private string ReadLine()
        {
            OnTcpReading();
            StreamReader sr = new StreamReader(GetStream(), Encoding.ASCII);
            string response = sr.ReadLine();
            OnTcpRead(new TcpReadEventArgs(response));
            return response;
        }

        #endregion

        #region Public methods

        #region Connecting, authenticating and disconnecting

        #region Cleartext methods

        /// <summary>
        /// Connects to the specified server.
        /// </summary>
        /// <param name="host">Address of the server.</param>
        /// <returns>The server's welcome greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com");
        /// </code>
        /// </example>
        public string Connect(string host)
        {
            return Connect(host,25);
        }
        public IAsyncResult BeginConnect(string host, AsyncCallback callback)
        {
            return BeginConnect(host, 25, callback);
        }

        /// <summary>
        /// Connects to the specified server using the specified port.
        /// </summary>
        /// <param name="host">Address of the server.</param>
        /// <param name="port">Port to be used for connection.</param>
        /// <returns>The server's welcome greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// </code>
        /// </example>
        public new string Connect(string host, int port)
        {
            OnConnecting();
            base.Connect(host,port);
            //System.IO.StreamReader sr = new System.IO.StreamReader(this.GetStream(),System.Text.Encoding.ASCII);
            string response = ReadLine();// sr.ReadLine();
            OnConnected(new ConnectedEventArgs(response+" - "+host));
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
            //string response = "";
            string response = ReadLine();
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
            if (addresses.Length > 0)
            {
                base.Connect(addresses[0], port);
            }
#endif
            string response = ReadLine();
            OnConnected(new ConnectedEventArgs(response));
            return response;
        }

        public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback callback)
        {
            _delegateConnectIPAddresses = Connect;
#if !PocketPC
            return _delegateConnectIPAddresses.BeginInvoke(addresses, port, callback, _delegateConnectSslIPAddresses);
#else
            return this._delegateConnectIPAddresses.BeginInvoke(addresses, port, callback, null);
#endif
        }

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
            return ConnectSsl(host, 465, new SslHandShake(host));
        }

        public IAsyncResult BeginConnectSsl(string host, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 465, new SslHandShake(host), callback);
        }
        public string ConnectSsl(string host, SslHandShake sslHandShake)
        {
            return ConnectSsl(host, 465, sslHandShake);
        }
        public IAsyncResult BeginConnectSsl(string host, SslHandShake sslHandShake, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 465, sslHandShake, callback);
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
#endif
        public string EndConnectSsl(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

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
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com");
        /// smtp.Authenticate("user","pass",SASLMechanism.CramMd5);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com")
        /// smtp.Authenticate("user","pass",SASLMechanism.CramMd5)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com");
        /// smtp.Authenticate("user","pass",SASLMechanism.CramMd5);
        /// smtp.Disconnect();
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
        /// Performs a QUIT command on the server and closes connection.
        /// </summary>
        /// <returns>The server's good bye greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com");
        /// //Do some work...
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com")
        /// 'Do some work...
        /// smtp.Disconnect()
        ///  
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com");
        /// //Do some work...
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Disconnect()
        {
            OnDisconnecting();
            string response = Command("QUIT", 221);
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

        #endregion

        #endregion

        #region Command sending and receiving, stream access

        /// <summary>
        /// Performs the specified command on the server and returns the response.
        /// </summary>
        /// <param name="command">The command to be performed.</param>
        /// <param name="expectedResponseCode">The expected response code, which will allow the client to know if the command succeeded or failed.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// smtp.Command("XANYCOMMAND",213);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// smtp.Command("XANYCOMMAND",213)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// smtp.Command("XANYCOMMAND",213);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Command(string command, int expectedResponseCode)
        {
            if(command.Length<200)
                OnTcpWriting(new TcpWritingEventArgs(command));
            else
                OnTcpWriting(new TcpWritingEventArgs("Message Data"));
            GetStream().Write(Encoding.ASCII.GetBytes(command+"\r\n"),0,command.Length+2);
            if(command.Length<200)
                OnTcpWritten(new TcpWrittenEventArgs(command));
            else
                OnTcpWritten(new TcpWrittenEventArgs("Message Data"));
            StreamReader sr = new StreamReader(GetStream(), Encoding.ASCII);
            StringBuilder buffer = new StringBuilder();
            string line = "";
            if(command.Length<200)
                OnTcpReading();
            while(true)
            {
                line = sr.ReadLine();
                if(line!=null)
                {
                    if(line.StartsWith(expectedResponseCode.ToString()+"-"))
                        buffer.Append(string.Concat(line,Tokenizer.NewLine));
                    else if(line.StartsWith(expectedResponseCode.ToString()+" "))
                    {
                        buffer.Append(string.Concat(line,Tokenizer.NewLine));
                        break;
                    }
                    else
                        throw new SmtpException("Command \""+command+"\" failed : "+line);
                }
                else
                    throw new SmtpException("Could not read response from the server. The connection has been closed by the remote server.");
            }
            if(buffer.Length<200)
                OnTcpRead(new TcpReadEventArgs(buffer.ToString().Trim(new char[] {'\r','\n'})));
                    
            return buffer.ToString();
        }
        public IAsyncResult BeginCommand(string command, int expectedResponseCode, AsyncCallback callback)
        {
            _delegateCommand = new DelegateCommand(Command);
            return _delegateCommand.BeginInvoke(command, expectedResponseCode, callback, null);
        }

        public string EndCommand(IAsyncResult result)
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
            if (_sslStream != null) return _sslStream;
#endif
            return base.GetStream();
        }

        #endregion

        #region Implementation of the SMTP and ESMTP protocols

        /// <summary>
        /// Performs a DATA command on the server with the specified data.
        /// </summary>
        /// <param name="data">The data to be transfered.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Data(string data)
        {
            Command("data",354);
            return Command(data+"\r\n.",250);
        }
        public IAsyncResult BeginData(string data, AsyncCallback callback)
        {
            _delegateData = Data;
            return _delegateData.BeginInvoke(data, callback, null);
        }
                                
        /// <summary>
        /// Performs a DATA command on the server with the specified data.
        /// </summary>
        /// <param name="data">The data to be transfered.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(Encoding.ASCII.GetBytes(message.ToMimeString()));
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(Encoding.ASCII.GetBytes(message.ToMimeString()))
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(Encoding.ASCII.GetBytes(message.ToMimeString()));
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Data(byte[] data)
        {
            return Data(Encoding.ASCII.GetString(data,0,data.Length));
        }
        public IAsyncResult BeginData(byte[] data, AsyncCallback callback)
        {
            return BeginData(Encoding.ASCII.GetString(data,0,data.Length), callback);
        }

        /// <summary>
        /// Performs a DATA command on the server with the specified data.
        /// </summary>
        /// <param name="dataStream">A stream containing the data to be transfered.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(new MemoryStream(Encoding.ASCII.GetBytes(message.ToMimeString()),0,message.Size));
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(New MemoryStream(Encoding.ASCII.GetBytes(message.ToMimeString()),0,message.Size)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(new MemoryStream(Encoding.ASCII.GetBytes(message.ToMimeString()),0,message.Size));
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Data(MemoryStream dataStream)
        {
            byte[] buffer = new byte[dataStream.Length];
            dataStream.Read(buffer,0, Convert.ToInt32(dataStream.Length));
            return Data(buffer);
        }
        public IAsyncResult BeginData(MemoryStream dataStream, AsyncCallback callback)
        {
            byte[] buffer = new byte[dataStream.Length];
            dataStream.Read(buffer, 0, Convert.ToInt32(dataStream.Length));
            return BeginData(buffer, callback);
        }

        public string EndData(IAsyncResult result)
        {
            return _delegateData.EndInvoke(result);
        }
        
        /// <summary>
        /// Performs a DATA command on the server with the specified data.
        /// </summary>
        /// <param name="dataFilePath">The path to a file containing the data to be transfered.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom("jdoe@myhost.com");
        /// smtp.RcptTo("mjohns@otherhost.com");
        /// smtp.DataFromFile("D:\\My mails\\mailtosend.eml");
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom("jdoe@myhost.com")
        /// smtp.RcptTo("mjohns@otherhost.com")
        /// smtp.DataFromFile("D:\My mails\mailtosend.eml")
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom("jdoe@myhost.com");
        /// smtp.RcptTo("mjohns@otherhost.com");
        /// smtp.DataFromFile("D:\\My mails\\mailtosend.eml");
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string DataFromFile(string dataFilePath)
        {
            return Data(new StreamReader(dataFilePath, Encoding.ASCII).ReadToEnd());
        }
        public IAsyncResult BeginDataFromFile(string dataFilePath, AsyncCallback callback)
        {
            return BeginData(new StreamReader(dataFilePath, Encoding.ASCII).ReadToEnd(), callback);
        }

        public string EndDataFromFile(IAsyncResult result)
        {
            return _delegateData.EndInvoke(result);
        }
                
        /// <summary>
        /// Performs a EHLO command on the server.
        /// </summary>
        /// <param name="domain">The domain to be used as identifier.</param>
        /// <remarks>The use of this method indicates that the client is capable of using SMTP extensions (RFC2821).</remarks>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <seealso cref="Helo"/>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Ehlo(string domain)
        {
            return Command("ehlo "+domain,250);
        }
        public IAsyncResult BeginEhlo(string domain, AsyncCallback callback)
        {
            _delegateEhlo = Ehlo;
            return _delegateEhlo.BeginInvoke(domain, callback, null);
        }

        public string EndEhlo(IAsyncResult result)
        {
            return _delegateEhlo.EndInvoke(result);
        }

        /// <summary>
        /// Performs a HELO command on the server.
        /// </summary>
        /// <param name="domain">The domain to be used as identifier.</param>
        /// <remarks>The use of this method isntead of Ehlo() indicates that the client is not capable of using SMTP extensions (RFC821).</remarks>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <seealso cref="Ehlo"/>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Helo(string domain)
        {
            return Command("helo "+domain,250);
        }
        public IAsyncResult BeginHelo(string domain, AsyncCallback callback)
        {
            _delegateHelo = Helo;
            return _delegateHelo.BeginInvoke(domain, callback, null);
        }

        public string EndHelo(IAsyncResult result)
        {
            return _delegateHelo.EndInvoke(result);
        }

        /// <summary>
        /// Performs a HELP command on the server.
        /// </summary>
        /// <returns>The server's response (usually containing the commands it supports).</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// string helpString = smtp.Help();
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Dim helpString As String = smtp.Help()
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// var helpString:string = smtp.Help();
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Help()
        {
            return Command("help",211);
        }
        public IAsyncResult BeginHelp(AsyncCallback callback)
        {
            _delegateHelp = Help;
            return _delegateHelp.BeginInvoke(callback, null);
        }

        public string EndHelp(IAsyncResult result)
        {
            return _delegateHelp.EndInvoke(result);
        }

        /// <summary>
        /// Performs a MAIL FROM: command on the server using the specified address.
        /// </summary>
        /// <param name="address">The address of the message sender.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string MailFrom(string address)
        {
            return Command("mail from: <"+address+">",250);
        }
        public IAsyncResult BeginMailFrom(string address, AsyncCallback callback)
        {
            _delegateMailFrom = MailFrom;
            return _delegateMailFrom.BeginInvoke(address, callback, null);
        }

        /// <summary>
        /// Performs a MAIL FROM: command on the server using the specified address.
        /// </summary>
        /// <param name="address">The address of the message sender.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string MailFrom(Address address)
        {
            return MailFrom(address.Email);
        }
        public IAsyncResult BeginMailFrom(Address address, AsyncCallback callback)
        {
            return BeginMailFrom(address.Email, callback);
        }

        public string EndMailFrom(IAsyncResult result)
        {
            return _delegateMailFrom.EndInvoke(result);
        }

        /// <summary>
        /// Performs a NOOP command on the server (used to keep connection alive).
        /// </summary>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Noop()
        ///        //Connection still alive and timer reset on server.
        ///    }
        ///    catch
        ///    {
        ///        //Connection no longer available.
        ///    }
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///        smtp.Noop()
        ///        'Connection still alive and timer reset on server.
        ///    Catch
        ///        'Connection no longer available.
        ///    End Try
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// var helpString:string = smtp.Help();
        /// /// try
        /// {
        ///        smtp.Noop()
        ///        //Connection still alive and timer reset on server.
        ///    }
        ///    catch
        ///    {
        ///        //Connection no longer available.
        ///    }
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string Noop()
        {
            return Command("noop",250);
        }
        public IAsyncResult BeginNoop(AsyncCallback callback)
        {
            _delegateNoop = Noop;
            return _delegateNoop.BeginInvoke(callback, null);
        }

        public string EndNoop(IAsyncResult result)
        {
            return _delegateNoop.EndInvoke(result);
        }

        /// <summary>
        /// Performs a RCPT TO: command on the server using the specified address.
        /// </summary>
        /// <param name="address">The address of one of the message's recipients.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// foreach(Address address in message.To) smtp.RcptTo(address.Email);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// For Each address In message.To
        ///        smtp.RcptTo(address.Email)
        ///    Next
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// for(i:int=0;i&lt;message.To.Count;i++) smtp.RcptTo(message.To[i].Email);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string RcptTo(string address)
        {
            return Command("rcpt to: <"+address+">",250);
        }
        public IAsyncResult BeginRcptTo(string address, AsyncCallback callback)
        {
            _delegateRcptTo = RcptTo;
            return _delegateRcptTo.BeginInvoke(address, callback, _delegateRcptTo);
        }
                        
        /// <summary>
        /// Performs a RCPT TO: command on the server using the specified address.
        /// </summary>
        /// <param name="address">The address of one of the message's recipients.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// foreach(Address address in message.To) smtp.RcptTo(address.Email);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// For Each address In message.To
        ///        smtp.RcptTo(address)
        ///    Next
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// for(i:int=0;i&lt;message.To.Count;i++) smtp.RcptTo(message.To[i]);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public string RcptTo(Address address)
        {
            return RcptTo(address.Email);
        }
        public IAsyncResult BeginRcptTo(Address address, AsyncCallback callback)
        {
            return BeginRcptTo(address.Email, callback);
        }
                        
        /// <summary>
        /// Performs a RCPT TO: command on the server using the specified addresses.
        /// </summary>
        /// <param name="addresses">The address of some of the message's recipients.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        /// smtp.MailFrom(message.From)
        /// smtp.RcptTo(message.To)
        /// smtp.Data(message.ToMimeString(),message.Charset)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        /// smtp.MailFrom(message.From);
        /// smtp.RcptTo(message.To);
        /// smtp.Data(message.ToMimeString(),message.Charset);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public void RcptTo(AddressCollection addresses)
        {
            foreach(Address address in addresses)
        RcptTo(address.Email);
        }
        public IAsyncResult BeginRcptTo(AddressCollection addresses, AsyncCallback callback)
        {
            _delegateRcptToAddressCollection = RcptTo;
            return _delegateRcptToAddressCollection.BeginInvoke(addresses, callback, _delegateRcptToAddressCollection);
        }

        public string EndRcptTo(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Performs a VRFY command on the server using the specified address (checks if the address refers to a mailbox on the server).
        /// </summary>
        /// <param name="address">The address to be verified.</param>
        /// <returns>True if address is valid and false if address doesn't reside on the server.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        ///    if(smtp.Verify("jdoe@myhost.com"))
        ///        bool isValid = true;
        ///        //Address is valid and resides on this server.
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        ///    If smtp.Verify("jdoe@myhost.com") Then
        ///        Dim isValid As Boolean = True
        ///        'Address is valid and resides on this server.
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        ///    if(smtp.Verify("jdoe@myhost.com")) 
        ///            var isValid:bool = true;
        ///        //Address is valid and resides on this server.
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public bool Verify(string address)
        {
            try
            {
                string rep = Command("vrfy "+address,250);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IAsyncResult BeginVerify(string address, AsyncCallback callback)
        {
            _delegateVrfy = Verify;
            return _delegateVrfy.BeginInvoke(address, callback, _delegateVrfy);
        }

        /// <summary>
        /// Performs a VRFY command on the server using the specified address (checks if the address refers to a mailbox on the server).
        /// </summary>
        /// <param name="address">The address to be verified.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        ///    if(smtp.Verify(someAddressObject))
        ///        bool isValid = true;
        ///        //Address is valid and resides on this server.
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        ///    If smtp.Verify(someAddressObject) Then
        ///        Dim isValid As Boolean = True
        ///        'Address is valid and resides on this server.
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        ///    if(smtp.Verify(someAddressObject)) 
        ///            var isValid:bool = true;
        ///        //Address is valid and resides on this server.
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public bool Verify(Address address)
        {
            return Verify(address.Email);
        }
        public IAsyncResult BeginVerify(Address address, AsyncCallback callback)
        {
            return BeginVerify(address.Email, callback);
        }

        /// <summary>
        /// Performs a VRFY command on the server using the specified addresses (checks if the addresses refer to mailboxes on the server).
        /// </summary>
        /// <param name="address">The addresses to be verified.</param>
        /// <returns>A collection containing the invalid addresses.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// SmtpClient smtp = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        ///    //Create a collection to test.
        ///    AddressCollection myaddresses = new AddressCollection();
        ///    myaddresses.Add("jdoe@myhost.com","John Doe");
        ///    myaddresses.Add("mjohns@otherhost.com","Mike Johns");
        ///    //Verifies all addresses.
        /// AddressCollection invalidAddresses = smtp.Verify(myaddresses);
        /// smtp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim smtp As New SmtpClient
        /// smtp.Connect("mail.myhost.com",8504)
        /// Try
        ///     smtp.Ehlo()
        ///    Catch
        ///        smtp.Helo()
        ///    End Try
        ///    'Create a collection to test.
        ///    Dim myaddresses As New AddressCollection
        ///    myaddresses.Add("jdoe@myhost.com","John Doe")
        ///    myaddresses.Add("mjohns@otherhost.com","Mike Johns")
        ///    'Verifies all addresses.
        /// Dim invalidAddresses As AddressCollection = smtp.Verify(myaddresses)
        /// smtp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var smtp:SmtpClient = new SmtpClient();
        /// smtp.Connect("mail.myhost.com",8504);
        /// try
        /// {
        ///        smtp.Ehlo();
        ///    }
        ///    catch
        ///    {
        ///        smtp.Helo();
        ///    }
        ///    //Create a collection to test.
        ///    var myaddresses:AddressCollection = new AddressCollection();
        ///    myaddresses.Add("jdoe@myhost.com","John Doe");
        ///    myaddresses.Add("mjohns@otherhost.com","Mike Johns");
        ///    //Verifies all addresses.
        /// var invalidAddresses:AddressCollection = smtp.Verify(myaddresses);
        /// smtp.Disconnect();
        /// </code>
        /// </example>
        public AddressCollection Verify(AddressCollection addresses)
        {
            AddressCollection incorrects = new AddressCollection();
            foreach(Address address in addresses) 
            {
                try
                {
                    Verify(address.Email);
                }
                catch
                {
                    incorrects.Add(address);
                }
            }
            return incorrects;
        }
        public IAsyncResult BeginVerify(AddressCollection addresses, AsyncCallback callback)
        {
            _delegateVrfyAddressCollection = Verify;
            return _delegateVrfyAddressCollection.BeginInvoke(addresses, callback, _delegateVrfyAddressCollection);
        }

        public string EndVerify(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        #endregion

        #region Sending methods

        #region Direct Send

        /// <summary>
        /// Sends the given message using the Direct Mailing method. The client connects to each recipient's mail exchange server and delivers the message.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.DirectSend(message);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.DirectSend(message)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.DirectSend(message);
        /// </code>
        /// </example>
        public static string DirectSend(Message message)
        {
            return DirectSend(message, new ServerCollection());
        }
        public static IAsyncResult BeginDirectSend(Message message, AsyncCallback callback)
        {
            _delegateDirectSendMessage = DirectSend;
            return _delegateDirectSendMessage.BeginInvoke(message, callback, _delegateDirectSendMessage);
        }

        /// <summary>
        /// Sends the message using the specified host as dns server on the specified port.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="host">The host to be used.</param>
        /// <param name="port">The port to be used.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.DirectSend(message,"ns1.dnsserver.com",53);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.DirectSend(message,"ns1.dnsserver.com",53)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.DirectSend(message,"ns1.dnsserver.com",53);
        /// </code>
        /// </example>
        public static string DirectSend(Message message, string dnsHost, int dnsPort)
        {
            ServerCollection servers = new ServerCollection();
            servers.Add(dnsHost, dnsPort);
            return DirectSend(message, servers);
        }
        public static IAsyncResult BeginDirectSend(Message message, string dnsHost, int dnsPort, AsyncCallback callback)
        {
            _delegateDirectSendMessageStringInt = DirectSend;
            return _delegateDirectSendMessageStringInt.BeginInvoke(message, dnsHost, dnsPort, callback, _delegateDirectSendMessageStringInt);
        }

        /// <summary>
        /// Sends the message using the specified host as dns server.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="host">The host to be used.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.DirectSend(message,"ns1.dnsserver.com");
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.DirectSend(message,"ns1.dnsserver.com")
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.DirectSend(message,"ns1.dnsserver.com");
        /// </code>
        /// </example>
        public static string DirectSend(Message message, string dnsHost)
        {
            return DirectSend(message, dnsHost, 53);
        }
        public static IAsyncResult BeginDirectSend(Message message, string dnsHost, AsyncCallback callback)
        {
            _delegateDirectSendMessageString = DirectSend;
            return _delegateDirectSendMessageString.BeginInvoke(message, dnsHost, callback, _delegateDirectSendMessageString);
        }

        /// <summary>
        /// Sends the message using the specified DNS servers to get mail exchange servers addresses.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="dnsServers">Servers to be used (in preference order).</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// ServerCollection servers = new ServerCollection();
        /// servers.Add("ns1.dnsserver.com",53);
        /// servers.Add("ns2.dnsserver.com",53);
        /// 
        /// SmtpClient.DirectSend(message,servers);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim servers As New ServerCollection
        /// servers.Add("ns1.dnsserver.com",53)
        /// servers.Add("ns2.dnsserver.com",53)
        /// 
        /// SmtpClient.DirectSend(message,servers)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var servers:ServerCollection = new ServerCollection();
        /// servers.Add("ns1.dnsserver.com",53);
        /// servers.Add("ns2.dnsserver.com",53);
        /// 
        /// SmtpClient.DirectSend(message,servers);
        /// </code>
        /// </example>
        public static string DirectSend(Message message, ServerCollection dnsServers)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            string email = (message.From.Name!="(unknown)") ? message.From.Email : message.Sender.Email;
            int recipientCount = message.To.Count+message.Cc.Count+message.Bcc.Count;
#if !PocketPC
            Array domains = Array.CreateInstance(typeof(string),new int[] {recipientCount},new int[] {0});
            Array adds = Array.CreateInstance(typeof(Address),new int[] {recipientCount},new int[] {0});
#else
            System.Array domains = System.Array.CreateInstance(typeof(string), new int[] { recipientCount });
            System.Array adds = System.Array.CreateInstance(typeof(ActiveUp.Net.Mail.Address), new int[] { recipientCount });
#endif
            AddressCollection recipients = new AddressCollection();
            recipients += message.To;
            recipients += message.Cc;
            recipients += message.Bcc;
            for(int i=0;i<recipients.Count;i++)
            {
                if (Validator.ValidateSyntax(recipients[i].Email))
                {
                    domains.SetValue(recipients[i].Email.Split('@')[1],i);
                    adds.SetValue(recipients[i],i);
                }
            }
            Array.Sort(domains,adds,null);
            string currentDomain = "";
            string address = "";
            string buf = "";
            SmtpClient smtp = new SmtpClient();
            for(int j=0;j<adds.Length;j++)
            {
                address = ((Address)adds.GetValue(j)).Email;
                if(((string)domains.GetValue(j))==currentDomain)
                {
                    smtp.RcptTo(address);
                    if(j==(adds.Length-1))
                    {
                        smtp.Data(message.ToMimeString(true));//,(message.Charset!=null ? message.Charset : "iso-8859-1"));
                        smtp.Disconnect();
                    }
                }
                else
                {
                    if(currentDomain!="")
                    {
                        smtp.Data(message.ToMimeString(true));//,(message.Charset!=null ? message.Charset : "iso-8859-1"));
                        smtp.Disconnect();
                        smtp = new SmtpClient(); 
                    }
                    currentDomain = (string)domains.GetValue(j);                
                    buf += currentDomain+"|";

                    if (dnsServers == null || dnsServers.Count == 0)
                    {
                        if (dnsServers == null)
                            dnsServers = new ServerCollection();

                        IList<IPAddress> machineDnsServers = DnsQuery.GetMachineDnsServers();
                        foreach (IPAddress ipAddress in machineDnsServers)
                            dnsServers.Add(ipAddress.ToString());
                    }
                    MxRecordCollection mxs = Validator.GetMxRecords(currentDomain, dnsServers);
                    if(mxs != null && mxs.Count>0)
                        smtp.Connect(mxs.GetPrefered().Exchange);
                    else
                        throw new SmtpException("No MX record found for the domain \""+currentDomain+"\". Check that the domain is correct and exists or specify a DNS server.");
                    try
                    {
                        smtp.Ehlo(System.Net.Dns.GetHostName());
                    }
                    catch
                    {
                        smtp.Helo(System.Net.Dns.GetHostName());
                    }
                    smtp.MailFrom(email);
                    smtp.RcptTo(address);
                    if (j == (adds.Length - 1))
                    {
                        smtp.Data(message.ToMimeString(true));//,(message.Charset!=null ? message.Charset : "iso-8859-1"));                    
                        smtp.Disconnect();
                    }
                }
                //}
                //catch(ActiveUp.Net.Mail.SmtpException ex) { throw ex; }
            }
            return buf;
        }
        public static IAsyncResult BeginDirectSend(Message message, ServerCollection dnsServers, AsyncCallback callback)
        {
            _delegateDirectSendMessageServerCollection = DirectSend;
            return _delegateDirectSendMessageServerCollection.BeginInvoke(message, dnsServers, callback, _delegateDirectSendMessageServerCollection);
        }

        public static string EndDirectSend(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Sends each message using the DirectMailing technique (SMTP connection with every recipient's mail exchange server for delivery).
        /// MX Records are cached for faster operation.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.DirectSend(messages);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.DirectSend(messages)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.DirectSend(messages);
        /// </code>
        /// </example>
        public static int DirectSendCollection(MessageCollection messages)
        {
            return DirectSendCollection(messages, new ServerCollection());
        }
        public static IAsyncResult BeginDirectSendCollection(MessageCollection messages, AsyncCallback callback)
        {
            _delegateDirectSendMessageCollection = DirectSendCollection;
            return _delegateDirectSendMessageCollection.BeginInvoke(messages, callback, _delegateDirectSendMessageCollection);
        }

        /// <summary>
        /// Sends all messages using the specified host as the DNS server.
        /// </summary>
        /// <param name="dnsServers">Servers to be used to send the message (in preference order).</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// ServerCollection servers = new ServerCollection();
        /// servers.Add("ns1.dnsserver.com",53);
        /// servers.Add("ns2.dnsserver.com",53);
        /// 
        /// SmtpClient.DirectSend(messages,servers);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// Dim servers As New ServerCollection
        /// servers.Add("ns1.dnsserver.com",53)
        /// servers.Add("ns2.dnsserver.com",53)
        /// 
        /// SmtpClient.DirectSend(messages,servers)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// var servers:ServerCollection = new ServerCollection();
        /// servers.Add("ns1.dnsserver.com",53);
        /// servers.Add("ns2.dnsserver.com",53);
        /// 
        /// SmtpClient.DirectSend(messages,servers);
        /// </code>
        /// </example>
        public static int DirectSendCollection(MessageCollection messages, ServerCollection dnsServers)
        {
            int sent=0;
            foreach(Message message in messages)
            {
                DirectSend(message);
                sent++;
            }
            return sent;
        }
        public static IAsyncResult BeginDirectSendCollection(MessageCollection messages, ServerCollection dnsServers, AsyncCallback callback)
        {
            _delegateDirectSendMessageCollectionServerCollection = DirectSendCollection;
            return _delegateDirectSendMessageCollectionServerCollection.BeginInvoke(messages, dnsServers, callback, _delegateDirectSendMessageCollectionServerCollection);
        }

        /// <summary>
        /// Sends all messages using the specified host and port as the dns server.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="dnsHost">Address of the server to be used.</param>
        /// <param name="dnsPort">Port to be used.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.DirectSend(messages,"ns1.dnsserver.com",53);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.DirectSend(messages,"ns1.dnsserver.com",53)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.DirectSend(messages,"ns1.dnsserver.com",53);
        /// </code>
        /// </example>
        public static int DirectSendCollection(MessageCollection messages, string dnsHost, int dnsPort)
        {
            ServerCollection servers = new ServerCollection();
            servers.Add(dnsHost, dnsPort);
            return DirectSendCollection(messages, servers);
        }
        public static IAsyncResult BeginDirectSendCollection(MessageCollection messages, string dnsHost, int dnsPort, AsyncCallback callback)
        {
            _delegateDirectSendMessageCollectionStringInt = DirectSendCollection;
            return _delegateDirectSendMessageCollectionStringInt.BeginInvoke(messages, dnsHost, dnsPort, callback, _delegateDirectSendMessageCollectionStringInt);
        }

        /// <summary>
        /// Sends all messages using the specified host as the dns server.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="dnsHost">Address of the server to be used.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.DirectSend(messages,"ns1.dnsserver.com");
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.DirectSend(messages,"ns1.dnsserver.com")
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.DirectSend(messages,"ns1.dnsserver.com");
        /// </code>
        /// </example>
        public static int DirectSendCollection(MessageCollection messages, string dnsHost)
        {
            return DirectSendCollection(messages, dnsHost, 53);
        }
        public static IAsyncResult BeginDirectSendCollection(MessageCollection messages, string dnsHost, AsyncCallback callback)
        {
            _delegateDirectSendMessageCollectionString = DirectSendCollection;
            return _delegateDirectSendMessageCollectionString.BeginInvoke(messages, dnsHost, callback, _delegateDirectSendMessageCollectionString);
        }

        /// <summary>
        /// Sends each message using the DirectMailing technique (SMTP connection with every recipient's mail exchange server for delivery).
        /// MX Records are cached for faster operation. Errors occuring during the process are catched and stored in a user-defined collection for review at a later time.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="errors">Reference to an SmtpException Collection to be filled with errors occuring during this process.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,myErrorCollection);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,myErrorCollection)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,myErrorCollection);
        /// </code>
        /// </example>
        public static int DirectSendCollection(MessageCollection messages, ref SmtpExceptionCollection errors)
        {
            string domain = string.Empty;
            string samer = string.Empty;
            int sent = 0;
            foreach (Message message in messages)
            {
                try
                {
                    DirectSend(message);
                    sent++;
                }
                catch (SmtpException ex) { errors.Add(ex); }
            }
            return sent;
        }
        public static IAsyncResult BeginDirectSendCollection(MessageCollection messages, ref SmtpExceptionCollection errors, AsyncCallback callback)
        {
            _delegateDirectSendMessageCollectionSmtpExceptionCollection = DirectSendCollection;
            return _delegateDirectSendMessageCollectionSmtpExceptionCollection.BeginInvoke(messages, ref errors, callback, _delegateDirectSendMessageCollectionSmtpExceptionCollection);
        }

        public static int EndDirectSendCollection(IAsyncResult result)
        {
            return (int)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        public static int EndDirectSendCollection(ref SmtpExceptionCollection errors, IAsyncResult result)
        {
            return (int)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { errors, result });
        }

        #endregion

        #region Send Queued

        /// <summary>
        /// Sends the message using the specified queuing service and spool directory.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="spoolDirectory">The full path to the full directory.</param>
        /// <param name="queuingService">The queuing service to use.</param>
        public static void SendQueued(Message message, string spoolDirectory, QueuingService queuingService)
        {
            _UIDcounter++;
            if (_UIDcounter > 99999)
                _UIDcounter = 0;

            string UID = DateTime.Now.ToString("yyMMddhhmmss")+ DateTime.Now.Millisecond.ToString()+_UIDcounter.ToString().PadLeft(5, '0');
            Logger.AddEntry(typeof(SmtpClient), "UID Created for filename: '" + UID + "'", 0);

            switch (queuingService)
            {
                case QueuingService.MicrosoftSmtp:
                case QueuingService.ActiveQ:
                    message.StoreToFile(spoolDirectory.TrimEnd('\\') + "\\" + UID + ".eml", true);
                    break;
                case QueuingService.IpSwitchIMail:
                    string imailQueued = spoolDirectory.TrimEnd('\\') + "\\" + "D" + UID + ".SMD";
                    string imailSpec = spoolDirectory.TrimEnd('\\') + "\\" + "Q" + UID + ".SMD";
                    StringBuilder imailDef = new StringBuilder();
                    imailDef.Append("Q" + imailQueued + "\n");
                    imailDef.Append("H" + message.From.Email.Split('@')[1]);
                    imailDef.Append("S" + message.From.Email);
                    foreach(Address address in message.To)
                        imailDef.Append("R" + address.Email);
                    foreach(Address address in message.Cc)
                        imailDef.Append("R" + address.Email);
                    foreach(Address address in message.Bcc)
                        imailDef.Append("R" + address.Email);
                    StreamWriter sw = File.CreateText(imailSpec);
                    sw.Write(imailDef);
                    sw.Close();
                    message.StoreToFile(imailQueued, true);
                    break;
            }
        }
        public static IAsyncResult BeginSendQueued(Message message, string spoolDirectory, QueuingService queuingService, AsyncCallback callback)
        {
            _delegateSendQueuedMessage = SendQueued;
            return _delegateSendQueuedMessage.BeginInvoke(message, spoolDirectory, queuingService, callback, _delegateSendQueuedMessage);
        }

        /// <summary>
        /// Sends the messages using the specified queuing service and spool directory.
        /// </summary>
        /// <param name="messages">The messages to send.</param>
        /// <param name="spoolDirectory">The full path to the full directory.</param>
        /// <param name="queuingService">The queuing service to use.</param>
        public static void SendQueued(MessageCollection messages, string spoolDirectory, QueuingService queuingService)
        {
            foreach (Message message in messages)
                SendQueued(message, spoolDirectory, queuingService);
        }
        public static IAsyncResult BeginSendQueued(MessageCollection messages, string spoolDirectory, QueuingService queuingService, AsyncCallback callback)
        {
            _delegateSendQueuedMessageCollection = SendQueued;
            return _delegateSendQueuedMessageCollection.BeginInvoke(messages, spoolDirectory, queuingService, callback, _delegateSendQueuedMessageCollection);
        }

        public static void EndSendQueued(IAsyncResult result)
        {
            result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        #endregion

        #region Send with relay servers

        /// <summary>
        /// Sends the message using the specified host as mail exchange server.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="servers">Servers to be used to send the message (in preference order).</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// ServerCollection servers = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(message,servers);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim servers As New ServerCollection
        /// servers.Add("mail.myhost.com",25)
        /// servers.Add("mail2.myhost.com",25)
        /// 
        /// SmtpClient.Send(message,servers)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var servers:ServerCollection = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(message,servers);
        /// </code>
        /// </example>
        public static bool Send(Message message, ServerCollection servers)
        {
            string nothing;
            bool sent = Send(message, servers, out nothing);
            return sent;
        }
        public static IAsyncResult BeginSend(Message message, ServerCollection servers, AsyncCallback callback)
        {
            _delegateSendMessageServerCollection = Send;
            return _delegateSendMessageServerCollection.BeginInvoke(message, servers, callback, _delegateSendMessageServerCollection);
        }

        /// <summary>
        /// Sends the message using the specified host as mail exchange server.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="servers">Servers to be used to send the message (in preference order).</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// ServerCollection servers = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(message,servers);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim servers As New ServerCollection
        /// servers.Add("mail.myhost.com",25)
        /// servers.Add("mail2.myhost.com",25)
        /// 
        /// SmtpClient.Send(message,servers)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var servers:ServerCollection = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(message,servers);
        /// </code>
        /// </example>
        public static bool Send(Message message, ServerCollection servers, out string serverMessage)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            serverMessage = string.Empty;
            SmtpClient smtp = new SmtpClient();
            bool messageSent = false;
            for(int i=0;i<servers.Count;i++)
            {
                try
                {
                    if (servers[i].ServerEncryptionType != EncryptionType.None)
                    {
#if !PocketPC
                        smtp.ConnectSsl(servers[i].Host,servers[i].Port);
#else
                        smtp.Connect(servers[i].Host, servers[i].Port);
#endif
                    }else {
                        smtp.Connect(servers[i].Host,servers[i].Port);
                    }
                    try
                    {
                        smtp.Ehlo(System.Net.Dns.GetHostName());
                    }
                    catch
                    {
                        smtp.Helo(System.Net.Dns.GetHostName());
                    }
                    if(servers[i].Username!=null && servers[i].Username.Length>0 && servers[i].Password!=null && servers[i].Password.Length>0)
                        smtp.Authenticate(servers[i].Username,servers[i].Password,SaslMechanism.Login);
                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    serverMessage = smtp.Data(message.ToMimeString());//,(message.Charset!=null ? message.Charset : "iso-8859-1"));
                    smtp.Disconnect();
                    messageSent = true;
                    break;
                }
                catch
                {
                    continue;
                }
            }

            return messageSent;
        }
        public static IAsyncResult BeginSend(Message message, ServerCollection servers, out string serverMessage, AsyncCallback callback)
        {
            _delegateSendMessageServerCollectionString = Send;
            return _delegateSendMessageServerCollectionString.BeginInvoke(message, servers, out serverMessage, callback, _delegateSendMessageServerCollectionString);
        }

        /// <summary>
        /// Sends the message using the specified host as mail exchange server.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="server">Server to be used to send the message.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com");
        /// </code>
        /// </example>
        public static bool Send(Message message, string server)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.Connect(server);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            if(message.From.Email!=string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());//,(message.Charset!=null ? message.Charset : "iso-8859-1"));
            smtp.Disconnect();
            return true;
        }

#if !PocketPC
        /// <summary>
        /// Sends the message using a secure connection with the specified host as mail exchange server.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="server">Server to be used to send the message.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.SendSsl(message,"mail.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.SendSsl(message,"mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.SendSsl(message,"mail.myhost.com");
        /// </code>
        /// </example>
        public static bool SendSsl(Message message, string server)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.ConnectSsl(server);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            if (message.From.Email != string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());//,(message.Charset!=null ? message.Charset : "iso-8859-1"));
            smtp.Disconnect();
            return true;
        }

        public static bool SendSsl(Message message, string server, int port)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.ConnectSsl(server, port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            if (message.From.Email != string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());//,(message.Charset!=null ? message.Charset : "iso-8859-1"));
            smtp.Disconnect();
            return true;
        }
#endif

        public static IAsyncResult BeginSend(Message message, string server, AsyncCallback callback)
        {
            _delegateSendMessageString = Send;
            return _delegateSendMessageString.BeginInvoke(message, server, callback, _delegateSendMessageString);
        }

        /// <summary>
        /// Sends the message using the specified host as mail exchange server on the specified port.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="host">The host to be used.</param>
        /// <param name="port">The port to be used.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com",8504);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com",8504)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com",8504);
        /// </code>
        /// </example>
        public static bool Send(Message message, string host, int port)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            if(message.From.Email!=string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());
            smtp.Disconnect();
            return true;
        }
        public static IAsyncResult BeginSend(Message message, string host, int port, AsyncCallback callback)
        {
            _delegateSendMessageStringInt = Send;
            return _delegateSendMessageStringInt.BeginInvoke(message, host, port, callback, _delegateSendMessageStringInt);
        }

        /// <summary>
        /// Sends the message using the specified host. Secure SASL Authentication is performed according to the requested mechanism.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="host">The host to be used.</param>
        /// <param name="username">The username to be used for authentication.</param>
        /// <param name="password">The password to be used for authentication.</param>
        /// <param name="mechanism">SASL Mechanism to be used for authentication.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5);
        /// </code>
        /// </example>
        public static bool Send(Message message, string host, string username, string password, SaslMechanism mechanism)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username,password,mechanism);
            if(message.From.Email!=string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());
            smtp.Disconnect();
            return true;
        }

#if !PocketPC
        /// <summary>
        /// Sends the message using the specified host in a secured connection. Secure SASL Authentication is performed according to the requested mechanism.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="host">The host to be used.</param>
        /// <param name="username">The username to be used for authentication.</param>
        /// <param name="password">The password to be used for authentication.</param>
        /// <param name="mechanism">SASL Mechanism to be used for authentication.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.SendSsl(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.SendSsl(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.SendSsl(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5);
        /// </code>
        /// </example>
        public static bool SendSsl(Message message, string host, string username, string password, SaslMechanism mechanism)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.ConnectSsl(host);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username, password, mechanism);
            if (message.From.Email != string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());
            smtp.Disconnect();
            return true;
        }

        public static bool SendSsl(Message message, string host, int port, string username, string password, SaslMechanism mechanism)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.ConnectSsl(host, port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username, password, mechanism);
            if (message.From.Email != string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());
            smtp.Disconnect();
            return true;
        }
#endif

        public static IAsyncResult BeginSend(Message message, string host, string username, string password, SaslMechanism mechanism, AsyncCallback callback)
        {
            _delegateSendMessageStringStringStringSaslMechanism = Send;
            return _delegateSendMessageStringStringStringSaslMechanism.BeginInvoke(message, host, username, password, mechanism, callback, _delegateSendMessageStringStringStringSaslMechanism);
        }

        /// <summary>
        /// Sends the message using the specified host on the specified port. Secure SASL Authentication is performed according to the requested mechanism.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="host">The host to be used.</param>
        /// <param name="username">The username to be used for authentication.</param>
        /// <param name="password">The password to be used for authentication.</param>
        /// <param name="mechanism">SASL Mechanism to be used for authentication.</param>
        /// <param name="port">The port to be used.</param>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,8504);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,8504)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// SmtpClient.Send(message,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,8504);
        /// </code>
        /// </example>
        public static bool Send(Message message, string host, int port, string username, string password, SaslMechanism mechanism)
        {
            // Ensure that the mime part tree is built
            message.CheckBuiltMimePartTree();

            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username,password,mechanism);
            if(message.From.Email!=string.Empty)
                smtp.MailFrom(message.From);
            else
                smtp.MailFrom(message.Sender);
            smtp.RcptTo(message.To);
            smtp.RcptTo(message.Cc);
            smtp.RcptTo(message.Bcc);
            smtp.Data(message.ToMimeString());
            smtp.Disconnect();
            return true;
        }
        public static IAsyncResult BeginSend(Message message, string host, int port, string username, string password, SaslMechanism mechanism, AsyncCallback callback)
        {
            _delegateSendMessageStringIntStringStringSaslMechanism = Send;
            return _delegateSendMessageStringIntStringStringSaslMechanism.BeginInvoke(message, host, port, username, password, mechanism, callback, _delegateSendMessageStringIntStringStringSaslMechanism);
        }

        public static bool EndSend(IAsyncResult result)
        {
            return (bool)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }
                        
        /// <summary>
        /// Sends all messages using the specified host.
        /// </summary>
        /// <param name="host">Address of the server to be used.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com");
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host)
        {
            return SendCollection(messages,host,25);
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, AsyncCallback callback)
        {
            _delegateSendMessageCollectionString = SendCollection;
            return _delegateSendMessageCollectionString.BeginInvoke(messages, host, callback, _delegateSendMessageCollectionString);
        }

        /// <summary>
        /// Sends all messages using the specified host.
        /// </summary>
        /// <param name="host">Address of the server to be used.</param>
        /// <param name="errors">Reference to SmtpException object collection where errors occuring during the process will be stored.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",myErrorCollection);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",myErrorCollection)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",myErrorCollection);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host, ref SmtpExceptionCollection errors)
        {
            return SendCollection(messages,host,25,ref errors);
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, ref SmtpExceptionCollection errors, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringSmtpExceptionCollection = SendCollection;
            return _delegateSendMessageCollectionStringSmtpExceptionCollection.BeginInvoke(messages, host, ref errors, callback, _delegateSendMessageCollectionStringSmtpExceptionCollection);
        }
                    
        /// <summary>
        /// Sends all messages using the specified host.
        /// </summary>
        /// <param name="servers">Servers to be used to send the message (in preference order).</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// ServerCollection servers = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(messages,servers);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// Dim servers As New ServerCollection
        /// servers.Add("mail.myhost.com",25)
        /// servers.Add("mail2.myhost.com",25)
        /// 
        /// SmtpClient.Send(messages,servers)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// var servers:ServerCollection = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(messages,servers);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, ServerCollection servers)
        {
            SmtpClient smtp = new SmtpClient();
            int sent=0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    for(int i=0;i<servers.Count;i++)
                    {
                        try
                        {
                            if (servers[i].ServerEncryptionType != EncryptionType.None)
                            {
#if !PocketPC
                                smtp.ConnectSsl(servers[i].Host, servers[i].Port);
#else
                                smtp.Connect(servers[i].Host, servers[i].Port);
#endif
                            }
                            else
                            {
                                smtp.Connect(servers[i].Host, servers[i].Port);
                            }
                            try
                            {
                                smtp.Ehlo(System.Net.Dns.GetHostName());
                            }
                            catch
                            {
                                smtp.Helo(System.Net.Dns.GetHostName());
                            }
                            if(servers[i].Username!=null && servers[i].Username.Length>0 && servers[i].Password!=null && servers[i].Password.Length>0)
                                smtp.Authenticate(servers[i].Username,servers[i].Password,SaslMechanism.Login);
                            if(message.From.Email!=string.Empty)
                                smtp.MailFrom(message.From);
                            else
                                smtp.MailFrom(message.Sender);
                            smtp.RcptTo(message.To);
                            smtp.RcptTo(message.Cc);
                            smtp.RcptTo(message.Bcc);
                            smtp.Data(message.ToMimeString());
                            smtp.Disconnect();
                            sent++;
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                catch(SmtpException) { }
            }
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, ServerCollection servers, AsyncCallback callback)
        {
            _delegateSendMessageCollectionServerCollection = SendCollection;
            return _delegateSendMessageCollectionServerCollection.BeginInvoke(messages, servers, callback, _delegateSendMessageCollectionServerCollection);
        }
                    
        /// <summary>
        /// Sends all messages using the specified host.
        /// </summary>
        /// <param name="servers">Servers to be used to send the message (in preference order).</param>
        /// <param name="messages">MessageCollection to be sent.</param>
        /// <param name="errors">Reference to SmtpException object collection where errors occuring during the process will be stored.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// ServerCollection servers = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(messages,servers,myErrorCollection);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// Dim servers As New ServerCollection
        /// servers.Add("mail.myhost.com",25)
        /// servers.Add("mail2.myhost.com",25)
        /// 
        /// SmtpClient.Send(messages,servers,myErrorCollection)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// var servers:ServerCollection = new ServerCollection();
        /// servers.Add("mail.myhost.com",25);
        /// servers.Add("mail2.myhost.com",25);
        /// 
        /// SmtpClient.Send(messages,servers,myErrorCollection);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, ServerCollection servers, ref SmtpExceptionCollection errors)
        {
            SmtpClient smtp = new SmtpClient();
            int sent=0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    for(int i=0;i<servers.Count;i++)
                    {
                        try
                        {
                            if (servers[i].ServerEncryptionType != EncryptionType.None)
                            {
#if !PocketPC
                                smtp.ConnectSsl(servers[i].Host, servers[i].Port);
#else
                                smtp.Connect(servers[i].Host, servers[i].Port);
#endif
                            }
                            else
                            {
                                smtp.Connect(servers[i].Host, servers[i].Port);
                            }
                            try
                            {
                                smtp.Ehlo(System.Net.Dns.GetHostName());
                            }
                            catch
                            {
                                smtp.Helo(System.Net.Dns.GetHostName());
                            }
                            if(servers[i].Username!=null && servers[i].Username.Length>0 && servers[i].Password!=null && servers[i].Password.Length>0)
                                smtp.Authenticate(servers[i].Username,servers[i].Password,SaslMechanism.Login);
                            if(message.From.Email!=string.Empty)
                                smtp.MailFrom(message.From);
                            else
                                smtp.MailFrom(message.Sender);
                            smtp.RcptTo(message.To);
                            smtp.RcptTo(message.Cc);
                            smtp.RcptTo(message.Bcc);
                            smtp.Data(message.ToMimeString());
                            smtp.Disconnect();
                            sent++;
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                catch(SmtpException ex) { errors.Add(ex); }
            }
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, ServerCollection servers, ref SmtpExceptionCollection errors, AsyncCallback callback)
        {
            _delegateSendMessageCollectionServerCollectionSmtpExceptionCollection = SendCollection;
            return _delegateSendMessageCollectionServerCollectionSmtpExceptionCollection.BeginInvoke(messages, servers, ref errors, callback, _delegateSendMessageCollectionServerCollectionSmtpExceptionCollection);
        }
                    
        /// <summary>
        /// Sends all messages using the specified host and port.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="host">Address of the server to be used.</param>
        /// <param name="port">Port to be used.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",8504);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",8504)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",8504);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host, int port)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            int sent=0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    smtp.Data(message.ToMimeString());
                    sent++;
                }
                catch(SmtpException) { }
            }
            smtp.Disconnect();
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, int port, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringInt = SendCollection;
            return _delegateSendMessageCollectionStringInt.BeginInvoke(messages, host, port, callback, _delegateSendMessageCollectionStringInt);
        }
                    
        /// <summary>
        /// Sends all messages using the specified host and port.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="host">Address of the server to be used.</param>
        /// <param name="port">Port to be used.</param>
        /// <param name="errors">Reference to an SmtpException Collection to be filled with errors occuring during this process.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",8504,myErrorCollection);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",8504,myErrorCollection)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com",8504,myErrorCollection);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host, int port, ref SmtpExceptionCollection errors)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            int sent=0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    smtp.Data(message.ToMimeString());
                    sent++;
                }
                catch(SmtpException ex) { errors.Add(ex); }
            }
            smtp.Disconnect();
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, int port, ref SmtpExceptionCollection errors, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringIntSmtpExceptionCollection = SendCollection;
            return _delegateSendMessageCollectionStringIntSmtpExceptionCollection.BeginInvoke(messages, host, port, ref errors, callback, _delegateSendMessageCollectionStringIntSmtpExceptionCollection);
        }
                    
        /// <summary>
        /// Sends the message using the specified host and port after authentication.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="host">Host to be used to send the message.</param>
        /// <param name="username">Username to be used for the authentication process.</param>
        /// <param name="password">Password to be used for the authentication process.</param>
        /// <param name="mechanism">SASL mechanism to be used.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host, string username, string password, SaslMechanism mechanism)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,25);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username,password,mechanism);
            int sent=0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    smtp.Data(message.ToMimeString());
                    sent++;
                }
                catch(SmtpException) { }
            }
            smtp.Disconnect();
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, string username, string password, SaslMechanism mechanism, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringStringStringSaslMechanism = SendCollection;
            return _delegateSendMessageCollectionStringStringStringSaslMechanism.BeginInvoke(messages, host, username, password, mechanism, callback, _delegateSendMessageCollectionStringStringStringSaslMechanism);
        }
                    
        /// <summary>
        /// Sends the message using the specified host and port after authentication.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="host">Host to be used to send the message.</param>
        /// <param name="username">Username to be used for the authentication process.</param>
        /// <param name="password">Password to be used for the authentication process.</param>
        /// <param name="mechanism">SASL mechanism to be used.</param>
        /// <param name="errors">Reference to SmtpException object collection where errors occuring during the process will be stored.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,myErrorCollection);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,myErrorCollection)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,myErrorCollection);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host, string username, string password, SaslMechanism mechanism, ref SmtpExceptionCollection errors)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,25);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username,password,mechanism);
            int sent=0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    smtp.Data(message.ToMimeString());
                    sent++;
                }
                catch(SmtpException ex) { errors.Add(ex); }
            }
            smtp.Disconnect();
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, string username, string password, SaslMechanism mechanism, ref SmtpExceptionCollection errors, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringStringStringSaslMechanismSmtpExceptionCollection = SendCollection;
            return _delegateSendMessageCollectionStringStringStringSaslMechanismSmtpExceptionCollection.BeginInvoke(messages, host, username, password, mechanism, ref errors, callback, _delegateSendMessageCollectionStringStringStringSaslMechanismSmtpExceptionCollection);
        }
                    
        /// <summary>
        /// Sends the message using the specified host and port after authentication.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="host">Host to be used to send the message.</param>
        /// <param name="username">Username to be used for the authentication process.</param>
        /// <param name="password">Password to be used for the authentication process.</param>
        /// <param name="mechanism">SASL mechanism to be used.</param>
        /// <param name="port">Port to be used to connect to the specified host.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// Message message1 = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// MessageCollection messages = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,8504);
        /// 
        /// VB.NET
        /// 
        /// Dim message As New Message
        /// message.Subject = "Test"
        /// message.From = New Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.BodyText.Text = "Hello this is a test!"
        /// 
        /// Dim message1 As New Message
        /// message1.Subject = "Hey David!"
        /// message1.From = New Address("jdoe@myhost.com","John Doe")
        /// message1.To.Add("dclarck@otherhost.com","David Clark")
        /// message1.BodyText.Text = "How you doing ?"
        /// 
        /// Dim messages As New MessageCollection
        /// messages.Add(message)
        /// messages.Add(message1)
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,8504)
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = new Message();
        /// message.Subject = "Test";
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.BodyText.Text = "Hello this is a test!";
        /// 
        /// var message1:Message = new Message();
        /// message1.Subject = "Hey David!";
        /// message1.From = New Address("jdoe@myhost.com","John Doe");
        /// message1.To.Add("dclarck@otherhost.com","David Clark");
        /// message1.BodyText.Text = "How you doing ?";
        /// 
        /// var messages:MessageCollection = new MessageCollection();
        /// messages.Add(message);
        /// messages.Add(message1);
        /// 
        /// SmtpClient.Send(messages,"mail.myhost.com","jdoe1234","tanstaaf",SaslMechanism.CramMd5,8504);
        /// </code>
        /// </example>
        public static int SendCollection(MessageCollection messages, string host, int port, string username, string password, SaslMechanism mechanism)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username,password,mechanism);
            int sent = 0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    smtp.Data(message.ToMimeString());
                    sent++;
                }
                catch(SmtpException) { }
            }
            smtp.Disconnect();
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, int port, string username, string password, SaslMechanism mechanism, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringIntStringStringSaslMechanism = SendCollection;
            return _delegateSendMessageCollectionStringIntStringStringSaslMechanism.BeginInvoke(messages, host, port, username, password, mechanism, callback, _delegateSendMessageCollectionStringIntStringStringSaslMechanism);
        }

        /// <summary>
        /// Sends the message using the specified host and port after authentication.
        /// </summary>
        /// <param name="messages">The message collection to be sent.</param>
        /// <param name="host">Host to be used to send the message.</param>
        /// <param name="username">Username to be used for the authentication process.</param>
        /// <param name="password">Password to be used for the authentication process.</param>
        /// <param name="mechanism">SASL mechanism to be used.</param>
        /// <param name="port">Port to be used to connect to the specified host.</param>
        /// <param name="errors">Reference to SmtpException object collection where errors occuring during the process will be stored.</param>
        /// <returns>Amount of messages successfully sent.</returns>
        public static int SendCollection(MessageCollection messages, string host, int port, string username, string password, SaslMechanism mechanism, ref SmtpExceptionCollection errors)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(host,port);
            try
            {
                smtp.Ehlo(System.Net.Dns.GetHostName());
            }
            catch
            {
                smtp.Helo(System.Net.Dns.GetHostName());
            }
            smtp.Authenticate(username,password,mechanism);
            int sent = 0;
            foreach(Message message in messages)
            {
                try
                {
                    // Ensure that the mime part tree is built
                    message.CheckBuiltMimePartTree();

                    if(message.From.Email!=string.Empty)
                        smtp.MailFrom(message.From);
                    else
                        smtp.MailFrom(message.Sender);
                    smtp.RcptTo(message.To);
                    smtp.RcptTo(message.Cc);
                    smtp.RcptTo(message.Bcc);
                    smtp.Data(message.ToMimeString());
                    sent++;
                }
                catch(SmtpException ex) { errors.Add(ex); }
            }
            smtp.Disconnect();
            return sent;
        }
        public static IAsyncResult BeginSendCollection(MessageCollection messages, string host, int port, string username, string password, SaslMechanism mechanism, ref SmtpExceptionCollection errors, AsyncCallback callback)
        {
            _delegateSendMessageCollectionStringIntStringStringSaslMechanismSmtpExceptionCollection = SendCollection;
            return _delegateSendMessageCollectionStringIntStringStringSaslMechanismSmtpExceptionCollection.BeginInvoke(messages, host, port, username, password, mechanism, ref errors, callback, _delegateSendMessageCollectionStringIntStringStringSaslMechanismSmtpExceptionCollection);
        }

        public static int EndSendCollection(IAsyncResult result)
        {
            return (int)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        public static int EndSendCollection(ref SmtpExceptionCollection errors, IAsyncResult result)
        {
            return (int)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { errors, result });
        }

        #endregion

        #region Quick Direct Send

        /// <summary>
        /// This static method allows to send an email in 1 line of code.
        /// </summary>
        /// <param name="from">The email address of the person sending the message.</param>
        /// <param name="to">The email address of the message's recipient.</param>
        /// <param name="subject">The message's subject.</param>
        /// <param name="textBody">The text body of the message.</param>
        /// <example>
        /// C#
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!");
        ///
        /// VB.NET
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!")
        /// 
        /// JScript.NET
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!");
        /// </example>
        public static void QuickDirectSend(string from, string to, string subject, string textBody)
        {
            QuickSend(from, to, subject, textBody, BodyFormat.Text, string.Empty, string.Empty);
            /*ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            //message.From.Add(new ActiveUp.Net.Mail.Address(from));
            message.From = new ActiveUp.Net.Mail.Address(from);
            message.To.Add(to);
            message.Subject = subject;
            message.BodyText.Text = textBody;
            message.DirectSend();*/
        }
        public static IAsyncResult BeginQuickDirectSend(string from, string to, string subject, string textBody, AsyncCallback callback)
        {
            _delegateQuickDirectSend = QuickDirectSend;
            return _delegateQuickDirectSend.BeginInvoke(from, to, subject, textBody, callback, _delegateQuickDirectSend);
        }

        /// <summary>
        /// This static method allows to send an email in 1 line of code.
        /// </summary>
        /// <param name="from">The email address of the person sending the message.</param>
        /// <param name="to">The email address of the message's recipient.</param>
        /// <param name="subject">The message's subject.</param>
        /// <param name="textBody">The text body of the message.</param>
        /// <param name="attachmentPath">The path to a file to be attached to the message.</param>
        /// <example>
        /// C#
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","C:\\My Documents\\file.doc");
        ///
        /// VB.NET
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","C:\My Documents\file.doc")
        /// 
        /// JScript.NET
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","C:\\My Documents\\file.doc");
        /// </example>
        public static void QuickDirectSend(string from, string to, string subject, string textBody, string attachmentPath)
        {
            QuickSend(from, to, subject, textBody, BodyFormat.Text, attachmentPath, string.Empty);
            /*ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            //message.From.Add(new ActiveUp.Net.Mail.Address(from));
            message.From = new ActiveUp.Net.Mail.Address(from);
            message.To.Add(to);
            message.Subject = subject;
            message.BodyText.Text = textBody;
            message.Attachments.Add(attachmentPath,false);
            message.DirectSend();*/
        }
        public static IAsyncResult BeginQuickDirectSend(string from, string to, string subject, string textBody, string attachmentPath, AsyncCallback callback)
        {
            _delegateQuickDirectSendAttach = QuickDirectSend;
            return _delegateQuickDirectSendAttach.BeginInvoke(from, to, subject, textBody, BodyFormat.Text, attachmentPath, callback, _delegateQuickDirectSendAttach);
        }

        public static void EndQuickDirectSend(IAsyncResult result)
        {
            result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// This static method allows to send an email in 1 line of code.
        /// </summary>
        /// <param name="from">The email address of the person sending the message.</param>
        /// <param name="to">The email address of the message's recipient.</param>
        /// <param name="subject">The message's subject.</param>
        /// <param name="textBody">The text body of the message.</param>
        /// <param name="attachmentPath">The path to a file to be attached to the message.</param>
        /// <example>
        /// C#
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!",BodyFormat.Text,"C:\\My Documents\\file.doc");
        ///
        /// VB.NET
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!",BodyFormat.Text,"C:\My Documents\file.doc")
        /// 
        /// JScript.NET
        /// 
        /// SmtpClient.QuickDirectSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!",BodyFormat.Text,"C:\\My Documents\\file.doc");
        /// </example>
        public static void QuickDirectSend(string from, string to, string subject, string body, BodyFormat bodyFormat, string attachmentPath)
        {
            QuickSend(from, to, subject, body, bodyFormat, attachmentPath, string.Empty);
            /*ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            //message.From.Add(new ActiveUp.Net.Mail.Address(from));
            message.From = new ActiveUp.Net.Mail.Address(from);
            message.To.Add(to);
            message.Subject = subject;
            if (bodyFormat == BodyFormat.Text)
                message.BodyText.Text = body;
            else
                message.BodyHtml.Text = body;
            if (!string.IsNullOrEmpty(attachmentPath))
                message.Attachments.Add(attachmentPath, false);
            message.DirectSend();*/
        }

        public static IAsyncResult BeginQuickDirectSend(string from, string to, string subject, string body, BodyFormat bodyFormat, string attachmentPath, AsyncCallback callback)
        {
            _delegateQuickDirectSendAttach = QuickDirectSend;
            return _delegateQuickDirectSendAttach.BeginInvoke(from, to, subject, body, bodyFormat, attachmentPath, callback, _delegateQuickDirectSendAttach);
        }

        #endregion

        #region Quick Send

        /// <summary>
        /// This static method allows to send an email in 1 line of code.
        /// </summary>
        /// <param name="from">The email address of the person sending the message.</param>
        /// <param name="to">The email address of the message's recipient.</param>
        /// <param name="subject">The message's subject.</param>
        /// <param name="textBody">The text body of the message.</param>
        /// <example>
        /// C#
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","mail.myhost.com");
        ///
        /// VB.NET
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","mail.myhost.com");
        /// </example>
        public static void QuickSend(string from, string to, string subject, string textBody, string smtpServer)
        {
            QuickSend(from, to, subject, textBody, BodyFormat.Text, string.Empty, smtpServer);
            /*ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            message.From = new ActiveUp.Net.Mail.Address(from);
            message.To.Add(to);
            message.Subject = subject;
            message.BodyText.Text = textBody;
            message.Send(smtpServer);*/
        }
        public static IAsyncResult BeginQuickSend(string from, string to, string subject, string textBody, string smtpServer, AsyncCallback callback)
        {
            _delegateQuickSend = QuickSend;
            return _delegateQuickSend.BeginInvoke(from, to, subject, textBody, smtpServer, callback, _delegateQuickSend);
        }
                    
        /// <summary>
        /// This static method allows to send an email in 1 line of code.
        /// </summary>
        /// <param name="from">The email address of the person sending the message.</param>
        /// <param name="to">The email address of the message's recipient.</param>
        /// <param name="subject">The message's subject.</param>
        /// <param name="textBody">The text body of the message.</param>
        /// <example>
        /// C#
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","C:\\My Documents\\file.doc","mail.myhost.com");
        ///
        /// VB.NET
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","C:\My Documents\file.doc","mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!","C:\\My Documents\\file.doc","mail.myhost.com");
        /// </example>
        public static void QuickSend(string from, string to, string subject, string textBody, string attachmentPath, string smtpServer)
        {
            QuickSend(from, to, subject, textBody, BodyFormat.Text, attachmentPath, smtpServer);
            /*ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            message.From = new ActiveUp.Net.Mail.Address(from);
            message.To.Add(to);
            message.Subject = subject;
            message.BodyText.Text = textBody;
            message.Attachments.Add(attachmentPath,false);
            message.Send(smtpServer);*/
        }
        public static IAsyncResult BeginQuickSend(string from, string to, string subject, string textBody, string attachmentPath, string smtpServer, AsyncCallback callback)
        {
            _delegateQuickSendAttach = QuickSend;
            return _delegateQuickSendAttach.BeginInvoke(from, to, subject, textBody, BodyFormat.Text, attachmentPath, smtpServer, callback, _delegateQuickSendAttach);
        }

        /// <summary>
        /// This static method allows to send an email in 1 line of code.
        /// </summary>
        /// <param name="from">The email address of the person sending the message.</param>
        /// <param name="to">The email address of the message's recipient.</param>
        /// <param name="subject">The message's subject.</param>
        /// <param name="textBody">The text body of the message.</param>
        /// <example>
        /// C#
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!",BodyFormat.Text,"C:\\My Documents\\file.doc","mail.myhost.com");
        ///
        /// VB.NET
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!",BodyFormat.Text,"C:\My Documents\file.doc","mail.myhost.com")
        /// 
        /// JScript.NET
        /// 
        /// SmtpClient.QuickSend("jdoe@myhost.com","mjohns@otherhost.com","Test","Hello this is a test!",BodyFormat.Text,"C:\\My Documents\\file.doc","mail.myhost.com");
        /// </example>
        public static void QuickSend(string from, string to, string subject, string body, BodyFormat bodyFormat, string attachmentPath, string smtpServer)
        {
            SmtpMessage message = new SmtpMessage();
            message.From = new Address(from);
            message.To.Add(to);
            message.Subject = subject;
            if (bodyFormat == BodyFormat.Text)
                message.BodyText.Text = body;
            else
                message.BodyHtml.Text = body;
            if (!string.IsNullOrEmpty(attachmentPath))
                message.Attachments.Add(attachmentPath, false);
            if (!string.IsNullOrEmpty(smtpServer))
                message.Send(smtpServer);
            else
                message.DirectSend();
        }

        public static void EndQuickSend(IAsyncResult result)
        {
            result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }
        #endregion
        #endregion
        #endregion
        #endregion
    }
}
