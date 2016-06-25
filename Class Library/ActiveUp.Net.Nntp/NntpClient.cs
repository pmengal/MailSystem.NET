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
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using ActiveUp.Net.Security;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// NNTP Client extending a System.Net.Sockets.TcpClient to send/receive NNTP command/responses.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class NntpClient : TcpClient
    {
        #region Private fields
#if !PocketPC
        SslStream _sslStream;
#endif
        #endregion

        #region Properties

        /// <summary>
        /// If true, posting is allowed.
        /// </summary>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// if(!nntp.PostingAllowed) throw new NntpException("Posting not allowed");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com")
        /// If Not nntp.PostingAllowed Then
        /// Throw New NntpException("Posting not allowed");
        /// End If
        /// nntp.Dicsonnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com"); 
        /// if(!nntp.PostingAllowed) throw new NntpException("Posting not allowed");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public bool PostingAllowed { get; set; }

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
        /// Event fired when authentication starts.
        /// </summary>
        public event AuthenticatingEventHandler Authenticating;
        /// <summary>
        /// Event fired when authentication completed.
        /// </summary>
        public event AuthenticatedEventHandler Authenticated;

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
        internal void OnMessageRetrieving(MessageRetrievingEventArgs e)
        {
            if(MessageRetrieving!=null)
                MessageRetrieving(this,e);
            Logger.AddEntry(GetType(), "Retrieving message at index " + e.MessageIndex+" out of "+e.TotalCount+"...",2);
        }
        internal void OnMessageRetrieved(MessageRetrievedEventArgs e)
        {
            if(MessageRetrieved!=null)
                MessageRetrieved(this,e);
            Logger.AddEntry(GetType(), "Retrieved message at index " + e.MessageIndex+" out of "+e.TotalCount+".",2);
        }
        internal void OnHeaderRetrieving(HeaderRetrievingEventArgs e)
        {
            if(HeaderRetrieving!=null)
                HeaderRetrieving(this,e);
            Logger.AddEntry(GetType(), "Retrieving Header at index " + e.MessageIndex+" out of "+e.TotalCount+"...",2);
        }
        internal void OnHeaderRetrieved(HeaderRetrievedEventArgs e)
        {
            if(HeaderRetrieved!=null)
                HeaderRetrieved(this,e);
            Logger.AddEntry(GetType(), "Retrieved Header at index " + e.MessageIndex+" out of "+e.TotalCount+".",2);
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

        #endregion

        #region Delegates and associated private fields

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
        private delegate string DelegateConnectAuth(string host, int port, string username, string password);
        private DelegateConnectAuth _delegateConnectAuth;

        #endregion

        #region Methods

        #region Private utility methods

        private string Write(string command)
        {
            OnTcpWriting(new TcpWritingEventArgs(command));
            GetStream().Write(Encoding.ASCII.GetBytes(command+"\r\n"),0,command.Length+2);
            OnTcpWritten(new TcpWrittenEventArgs(command));
            return ReadLine();
        }

        private string ReadLine()
        {
            OnTcpReading();
            StreamReader sr = new StreamReader(GetStream(), Encoding.ASCII);
            string response = sr.ReadLine();
            OnTcpRead(new TcpReadEventArgs(response));
            if (response.StartsWith("2") || response.StartsWith("3"))
                return response;
            else throw new NntpException("Command failed : " + response);
        }

        private byte[] writeMulti(string command)
        {
            OnTcpWriting(new TcpWritingEventArgs(command));
            GetStream().Write(Encoding.ASCII.GetBytes(command+"\r\n"),0,command.Length+2);
            OnTcpWritten(new TcpWrittenEventArgs(command));
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(GetStream(), Encoding.ASCII);
            string stri = sr.ReadLine();
            string str = "";
            if(stri.StartsWith("2"))
            {
                while(true)
                {
                    str = sr.ReadLine();
                    if(str!=".") 
                    {
                        if(str!="..")
                            sb.Append(str+"\r\n");
                        else
                            sb.Append("."+"\r\n");
                    }
                    else break;
                }
                return Encoding.ASCII.GetBytes(stri+"\r\n"+sb.ToString());
            }
            else throw new NntpException(command.Split(' ')[0]+" failed : "+stri);
        }

        private string _CramMd5(string username, string password)
        {
            OnAuthenticating(new AuthenticatingEventArgs(username,password));
            byte[] data = Convert.FromBase64String(Command("authinfo sasl cram-md5").Split(' ')[1].Trim(new char[] {'\r','\n'}));
            string digest = Encoding.ASCII.GetString(data,0,data.Length);
            string response = Command(Convert.ToBase64String(Encoding.ASCII.GetBytes(username+" "+ Crypto.HMACMD5Digest(password,digest))));
            OnAuthenticated(new AuthenticatedEventArgs(username,password,response));
            return response;

        }

#if !PocketPC
        private void DoSslHandShake(SslHandShake sslHandShake)
        {
            _sslStream = new SslStream(GetStream(), false, sslHandShake.ServerCertificateValidationCallback, sslHandShake.ClientCertificateSelectionCallback);
            _sslStream.AuthenticateAsClient(sslHandShake.HostName, sslHandShake.ClientCertificates, sslHandShake.SslProtocol, sslHandShake.CheckRevocation);
        }
#endif

        #endregion

        #region Public methods

        #region Connecting, authenticating and disconnecting

        #region Cleartext methods

        /// <summary>
        /// Connects to the specified server.
        /// </summary>
        /// <param name="host">Server address.</param>
        /// <returns>The server's greeting response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com"); 
        /// </code>
        /// </example>
        public string Connect(string host)
        {
            return Connect(host,119);
        }

        public IAsyncResult BeginConnect(string host, AsyncCallback callback)
        {
            _delegateConnect = Connect;
            return _delegateConnect.BeginInvoke(host, 119, callback, _delegateConnect);
        }

        /// <summary>
        /// Connects to the specified server.
        /// </summary>
        /// <param name="host">Server address.</param>
        /// <param name="port">Server port.</param>
        /// <returns>The server's greeting response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com",8502);
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com",8502)
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com",8502);
        /// </code>
        /// </example>
        public new string Connect(string host, int port)
        {
            OnConnecting();
            string response = "Connection failed.";
            try
            {
                base.Connect(host,port);
                response = ReadLine();
                if(response.StartsWith("2")) 
                {
                    OnConnected(new ConnectedEventArgs(response));
                    if(response.StartsWith("200"))
                        PostingAllowed = true;
                }
                else throw new NntpException("Failed to connect : " + response);
            }
            catch(SocketException)
            {
                OnConnected(new ConnectedEventArgs(response));
            }
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
                base.Connect(addresses[0], port);
#endif
            string response = ReadLine();
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
            return Connect(host, 119, username, password);
        }
        public IAsyncResult BeginConnect(string host, string username, string password, AsyncCallback callback)
        {
            return BeginConnect(host, 119, username, password, callback);
        }

        public string Connect(string host, int port, string username, string password)
        {
            Connect(host, port);
            return Authenticate(username, password);
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

        #endregion

        #region SSL methods

#if !PocketPC
        public string ConnectSsl(string host)
        {
            return ConnectSsl(host, 563, new SslHandShake(host));
        }
        public IAsyncResult BeginConnectSsl(string host, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 563, new SslHandShake(host), callback);
        }
        public string ConnectSsl(string host, SslHandShake sslHandShake)
        {
            return ConnectSsl(host, 563, sslHandShake);
        }
        public IAsyncResult BeginConnectSsl(string host, SslHandShake sslHandShake, AsyncCallback callback)
        {
            return BeginConnectSsl(host, 563, sslHandShake, callback);
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
            return _delegateConnectSsl.BeginInvoke(host, port, sslHandShake, callback, null);
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
            return _delegateConnectSslIPAddress.BeginInvoke(addr, port, sslHandShake, callback, null);
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
            return _delegateConnectSslIPAddresses.BeginInvoke(addresses, port, sslHandShake, callback, null);
        }

        public string EndConnectSsl(IAsyncResult result)
        {
            return _delegateConnectSsl.EndInvoke(result);
        }
#endif

        #endregion

        #region Authentication

        /// <summary>
        /// Authenticates the given user.
        /// </summary>
        /// <param name="username">Username to log in.</param>
        /// <param name="password">Password.</param>
        /// <returns>True if authentication succeded.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// nntp.Authenticate("admin","password");
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// nntp.Authenticate("admin","password")
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// nntp.Authenticate("admin","password");
        /// </code>
        /// </example>
        public string Authenticate(string username, string password)
        {
            string response = Command("authinfo user "+username);
            if(response.StartsWith("381"))
            {
                response = Command("authinfo pass "+password);
                if(response.StartsWith("281"))
                    return response;
            }
            throw new NntpException("Authentication failed. Server Response : " + response);
        }

        private delegate string DelegateAuthenticate(string username, string password);
        private DelegateAuthenticate _delegateAuthenticate;

        public IAsyncResult BeginAuthenticate(string username, string password, AsyncCallback callback)
        {
            _delegateAuthenticate = Authenticate;
            return _delegateAuthenticate.BeginInvoke(username, password, callback, _delegateAuthenticate);
        }

        /// <summary>
        /// Authenticates the given user and SASL mechanism.
        /// </summary>
        /// <param name="username">Username to log in.</param>
        /// <param name="password">Password.</param>
        /// <param name="mechanism">SASL Mechanism to be used.</param>
        /// <returns>True if authentication succeded.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// nntp.Authenticate("admin","password",SaslMechanism.CramMd5);
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// nntp.Authenticate("admin","password",SaslMechanism.CramMd5)
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// nntp.Authenticate("admin","password",SaslMechanism.CramMd5);
        /// </code>
        /// </example>
        public string Authenticate(string username, string password, SaslMechanism mechanism)
        {
            switch(mechanism)
            {
                case SaslMechanism.CramMd5 :
                    return _CramMd5(username,password);
                case SaslMechanism.Login:
                    throw new NntpException("LOGIN mechanism cannot be used for NNTP authentication. If your server accepts it, please perform the commands yourself.");
            }
            return "";
        }
        
        private delegate string DelegateAuthenticateSasl(string username, string password, SaslMechanism mechanism);
        private DelegateAuthenticateSasl _delegateAuthenticateSasl;

        public IAsyncResult BeginAuthenticate(string username, string password, SaslMechanism mechanism, AsyncCallback callback)
        {
            _delegateAuthenticateSasl = Authenticate;
            return _delegateAuthenticateSasl.BeginInvoke(username, password, mechanism, callback, _delegateAuthenticateSasl);
        }

        public string EndAuthenticate(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        #endregion

        #region Disconnect method

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <returns>The server's goodbye greeting.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Do some work
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Do some work
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com"); 
        /// //Do some work
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string Disconnect()
        {
            OnDisconnecting();
            string response = Write("quit");
            OnDisconnected(new DisconnectedEventArgs(response));
            return response;
        }

        private delegate string DelegateDisconnect();
        private DelegateDisconnect _delegateDisconnect;

        public IAsyncResult BeginDisconnect(AsyncCallback callback)
        {
            _delegateDisconnect = Disconnect;
            return _delegateDisconnect.BeginInvoke(callback, _delegateDisconnect);
        }

        public string EndDisconnect(IAsyncResult result)
        {
            return _delegateDisconnect.EndInvoke(result);
        }

        #endregion

        #endregion

        #region Command sending, receiving and stream access

        /// <summary>
        /// Sends the specified command to the server.
        /// </summary>
        /// <param name="command">The command to be sent.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// string response = nntp.Command("XANYCOMMAND anyargument");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim response as String = nntp.Command("XANYCOMMAND anyargument")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var response:string = nntp.Command("XANYCOMMAND anyargument");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string Command(string command)
        {
            return Write(command);
        }

        private delegate string DelegateCommand(string command);
        private DelegateCommand _delegateCommand;

        public IAsyncResult BeginCommand(string command, AsyncCallback callback)
        {
            _delegateCommand = Command;
            return _delegateCommand.BeginInvoke(command, callback, _delegateCommand);
        }

        public string EndCommand(IAsyncResult result)
        {
            return _delegateCommand.EndInvoke(result);
        }


        /// <summary>
        /// Sends the specified command to the server.
        /// </summary>
        /// <param name="command">The command to be sent.</param>
        /// <returns>A byte array containing the server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// nntp.SelectGroup("mygroup");
        /// byte[] articleData = nntp.CommandMultiline("ARTICLE 1");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// nntp.SelectGroup("mygroup")
        /// Dim articleData as Byte() = nntp.CommandMultiline("ARTICLE 1")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// nntp.SelectGroup("mygroup");
        /// var articleData:byte[] = nntp.CommandMultiline("ARTICLE 1");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] CommandMultiline(string command)
        {
            byte[] buffer = writeMulti(command);
            return buffer;
        }

        private delegate byte[] DelegateCommandMultiline(string command);
        private DelegateCommandMultiline _delegateCommandMultiline;

        public IAsyncResult BeginCommandMultiline(string command, AsyncCallback callback)
        {
            _delegateCommandMultiline = CommandMultiline;
            return _delegateCommandMultiline.BeginInvoke(command, callback, _delegateCommandMultiline);
        }

        public byte[] EndCommandMultiline(IAsyncResult result)
        {
            return _delegateCommandMultiline.EndInvoke(result);
        }

        #endregion

        #region Implementation of the NNTP protocol

        /// <summary>
        /// Retrieves the server's newsgroups.
        /// </summary>
        /// <returns>The server's newsgroups.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// NewsGroupCollection groups = nntp.GetNewsGroups();
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim groups as NewsGroupCollection = nntp.GetNewsGroups()
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var groups:NewsGroupCollection = nntp.GetNewsGroups();
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public NewsGroupCollection GetNewsGroups()
        {
            byte[] data = CommandMultiline("list");
            string response = Encoding.ASCII.GetString(data,0,data.Length);
            NewsGroupCollection groups = new NewsGroupCollection();
            string[] _groups = Regex.Split(response,"\r\n");
            for(int i=1;i<_groups.Length-1;i++) 
            {
                string[] _splitted = _groups[i].Split(' ');
                groups.Add(new NewsGroup(_splitted[0], Convert.ToInt32(_splitted[2]), Convert.ToInt32(_splitted[1]),(_splitted[3].ToLower()=="y") ? true : false,this));
            }
            return groups;
        }

        private delegate NewsGroupCollection DelegateGetNewsGroups();
        private DelegateGetNewsGroups _delegateGetNewsGroups;

        public IAsyncResult BeginGetNewsGroups(AsyncCallback callback)
        {
            _delegateGetNewsGroups = GetNewsGroups;
            return _delegateGetNewsGroups.BeginInvoke(callback, _delegateGetNewsGroups);
        }

        /// <summary>
        /// Retrieves the server's newsgroups that have been created since the specified date/time.
        /// </summary>
        /// <param name="startDate">The minimum creation date/time.</param>
        /// <returns>The server's newsgroups that have been created since the specified date/time.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Get groups created less than one months ago.
        /// NewsGroupCollection groups = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1));
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Get groups created less than one months ago.
        /// Dim groups as NewsGroupCollection = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1))
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Get groups created less than one months ago.
        /// var groups:NewsGroupCollection = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1));
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public NewsGroupCollection GetNewsGroups(DateTime startDate)
        {
            return GetNewsGroups(startDate,false,string.Empty);
        }

        private delegate NewsGroupCollection DelegateGetNewsGroupsDateTime(DateTime startDate);
        private DelegateGetNewsGroupsDateTime _delegateGetNewsGroupsDateTime;

        public IAsyncResult BeginGetNewsGroups(DateTime startDate, AsyncCallback callback)
        {
            _delegateGetNewsGroupsDateTime = GetNewsGroups;
            return _delegateGetNewsGroupsDateTime.BeginInvoke(startDate, callback, _delegateGetNewsGroups);
        }

        /// <summary>
        /// Retrieves the server's newsgroups that have been created since the specified date/time.
        /// </summary>
        /// <param name="startDate">The minimum creation date/time.</param>
        /// <param name="gmt">Specifies if startDate is GMT or not.</param>
        /// <returns>The server's newsgroups that have been created since the specified date/time.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Get groups created less than one months ago (GMT time).
        /// NewsGroupCollection groups = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1),true);
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Get groups created less than one months ago (GMT time).
        /// Dim groups as NewsGroupCollection = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1),True)
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Get groups created less than one months ago (GMT time).
        /// var groups:NewsGroupCollection = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1),true);
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public NewsGroupCollection GetNewsGroups(DateTime startDate, bool gmt)
        {
            return GetNewsGroups(startDate,gmt,string.Empty);
        }

        private delegate NewsGroupCollection DelegateGetNewsGroupsDateTimeBool(DateTime startDate, bool gmt);
        private DelegateGetNewsGroupsDateTimeBool _delegateGetNewsGroupsDateTimeBool;

        public IAsyncResult BeginGetNewsGroups(DateTime startDate, bool gmt, AsyncCallback callback)
        {
            _delegateGetNewsGroupsDateTimeBool = GetNewsGroups;
            return _delegateGetNewsGroupsDateTimeBool.BeginInvoke(startDate, gmt, callback, _delegateGetNewsGroupsDateTimeBool);
        }

        /// <summary>
        /// Retrieves the server's newsgroups that have been created since the specified date/time.
        /// </summary>
        /// <param name="startDate">The minimum creation date/time.</param>
        /// <param name="gmt">Specifies if startDate is GMT or not.</param>
        /// <param name="distribution">Distribution filter of the articles.</param>
        /// <returns>The server's newsgroups that have been created since the specified date/time and that contain articles for the specified distribution.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Get groups created less than one months ago (GMT time) and containing articles to be distributed to "staff".
        /// NewsGroupCollection groups = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1),true,"staff");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Get groups created less than one months ago (GMT time) and containing articles to be distributed to "staff".
        /// Dim groups as NewsGroupCollection = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1),True,"staff")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Get groups created less than one months ago (GMT time) and containing articles to be distributed to "staff".
        /// var groups:NewsGroupCollection = nntp.GetNewsGroups(System.DateTime.Now.AddMonth(-1),true,"staff");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public NewsGroupCollection GetNewsGroups(DateTime startDate, bool gmt, string distribution)
        {
            string sdistribution = (distribution.Length>0) ? " "+distribution : "";
            string sgmt = (gmt) ? " GMT" : "";
            byte[] data = CommandMultiline("newgroups " + startDate.ToString("yyMMdd hhmmss") + sgmt + sdistribution);
            string response = Encoding.ASCII.GetString(data,0,data.Length);
            NewsGroupCollection groups = new NewsGroupCollection();
            string[] _groups = Regex.Split(response,"\r\n");
            for(int i=1;i<_groups.Length-1;i++) 
            {
                string[] _splitted = _groups[i].Split(' ');
                groups.Add(new NewsGroup(_splitted[0], Convert.ToInt32(_splitted[2]), Convert.ToInt32(_splitted[1]), (_splitted[3].ToLower() == "y") ? true : false, this));
            }
            return groups;
        }

        private delegate NewsGroupCollection DelegateGetNewsGroupsDateTimeBoolString(DateTime startDate, bool gmt, string distribution);
        private DelegateGetNewsGroupsDateTimeBoolString _delegateGetNewsGroupsDateTimeBoolString;

        public IAsyncResult BeginGetNewsGroups(DateTime startDate, bool gmt, string distribution, AsyncCallback callback)
        {
            _delegateGetNewsGroupsDateTimeBoolString = GetNewsGroups;
            return _delegateGetNewsGroupsDateTimeBoolString.BeginInvoke(startDate, gmt, distribution, callback, _delegateGetNewsGroupsDateTimeBoolString);
        }

        public NewsGroupCollection EndGetNewsGroups(IAsyncResult result)
        {
            return (NewsGroupCollection)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Retrieves message-ids from articles that have been added since the specified date/time.
        /// </summary>
        /// <param name="newsGroup">Newsgroup to be checked.</param>
        /// <param name="startDate">Minimum addition date of the articles.</param>
        /// <returns>The article message-ids</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago.
        /// string[] newids = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1));
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago.
        /// Dim newids as String() = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1))
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago.
        /// var newids:string[] = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1));
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string[] GetNewArticleIds(string newsGroup, DateTime startDate)
        {
            return GetNewArticleIds(newsGroup,startDate,false,string.Empty);
        }

        private delegate string[] DelegateGetNewArticleIdsStringDateTime(string newsgroup, DateTime startDate);
        private DelegateGetNewArticleIdsStringDateTime _delegateGetNewArticleIdsStringDateTime;

        public IAsyncResult BeginGetNewArticleIds(string newsgroup, DateTime startDate, AsyncCallback callback)
        {
            _delegateGetNewArticleIdsStringDateTime = GetNewArticleIds;
            return _delegateGetNewArticleIdsStringDateTime.BeginInvoke(newsgroup, startDate, callback, _delegateGetNewArticleIdsStringDateTime);
        }

        /// <summary>
        /// Retrieves message-ids from articles that have been added since the specified date/time.
        /// </summary>
        /// <param name="newsGroup">Newsgroup to be checked.</param>
        /// <param name="startDate">Minimum addition date of the articles.</param>
        /// <param name="gmt">Specifies if startDate is GMT or not.</param>
        /// <returns>The article message-ids</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago (GMT time).
        /// string[] newids = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1),true);
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago (GMT time).
        /// Dim newids as String() = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1),True)
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago (GMT time).
        /// var newids:string[] = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1),true);
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string[] GetNewArticleIds(string newsGroup, DateTime startDate, bool gmt)
        {
            return GetNewArticleIds(newsGroup,startDate,gmt,string.Empty);
        }

        private delegate string[] DelegateGetNewArticleIdsStringDateTimeBool(string newsgroup, DateTime startDate, bool gmt);
        private DelegateGetNewArticleIdsStringDateTimeBool _delegateGetNewArticleIdsStringDateTimeBool;

        public IAsyncResult BeginGetNewArticleIds(string newsgroup, DateTime startDate, bool gmt, AsyncCallback callback)
        {
            _delegateGetNewArticleIdsStringDateTimeBool = GetNewArticleIds;
            return _delegateGetNewArticleIdsStringDateTimeBool.BeginInvoke(newsgroup, startDate, gmt, callback, _delegateGetNewArticleIdsStringDateTimeBool);
        }

        /// <summary>
        /// Retrieves message-ids from articles that have been added since the specified date/time.
        /// </summary>
        /// <param name="newsGroup">Newsgroup to be checked.</param>
        /// <param name="startDate">Minimum addition date of the articles.</param>
        /// <param name="gmt">Specifies if startDate is GMT or not.</param>
        /// <param name="distribution">The distribution filter of the articles.</param>
        /// <returns>The article message-ids</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Gets article ids that belong to the "myhost.info" newsgroup, that have been added less than one month ago (GMT time) and that should be distributed to "staff".
        /// string[] newids = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1),true,"staff");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago (GMT time) and that should be distributed to "staff".
        /// Dim newids as String() = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1),True,"staff")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago (GMT time) and that should be distributed to "staff".
        /// var newids:string[] = nntp.GetNewArticleIds("myhost.info",System.DateTime.Now.AddMonth(-1),true,"staff");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string[] GetNewArticleIds(string newsGroup, DateTime startDate, bool gmt, string distribution)
        {
            string sdistribution = (distribution.Length>0) ? " "+distribution : "";
            string sgmt = (gmt) ? " GMT" : "";
            byte[] data = CommandMultiline("newnews " + newsGroup + " " + startDate.ToString("yyMMdd hhmmss") + sgmt + sdistribution);
            string response = Encoding.ASCII.GetString(data,0,data.Length);
            string[] articles = Regex.Split(response,"\r\n");
            string[] farticles = new string[articles.Length-2];
            for(int i=1;i<articles.Length-1;i++) farticles[i-1] = articles[i].TrimStart('<').TrimEnd('>');
            return farticles;
        }

        private delegate string[] DelegateGetNewArticleIdsStringDateTimeBoolString(string newsgroup, DateTime startDate, bool gmt, string distribution);
        private DelegateGetNewArticleIdsStringDateTimeBoolString _delegateGetNewArticleIdsStringDateTimeBoolString;

        public IAsyncResult BeginGetNewArticleIds(string newsgroup, DateTime startDate, bool gmt, string distribution, AsyncCallback callback)
        {
            _delegateGetNewArticleIdsStringDateTimeBoolString = GetNewArticleIds;
            return _delegateGetNewArticleIdsStringDateTimeBoolString.BeginInvoke(newsgroup, startDate, gmt, distribution, callback, _delegateGetNewArticleIdsStringDateTimeBoolString);
        }

        public string[] EndGetNewArticleIds(IAsyncResult result)
        {
            return (string[])result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Selects the specified newsgroup.
        /// </summary>
        /// <param name="groupName">The newsgroup to be selected.</param>
        /// <returns>The selected newsgroup.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Selects the "myhost.info" newsgroup as the current newsgroup.
        /// NewsGroup group = nntp.SelectGroup("myhost.info");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// 'Selects the "myhost.info" newsgroup as the current newsgroup.
        /// Dim group as NewsGroup = nntp.SelectGroup("myhost.info")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// //Selects the "myhost.info" newsgroup as the current newsgroup.
        /// var group:NewsGroup = nntp.SelectGroup("myhost.info");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public NewsGroup SelectGroup(string groupName)
        {
            string response = Command("group "+groupName);
            string[] parts = response.Split(' ');
            return new NewsGroup(groupName, Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]),true,this);
        }

        private delegate NewsGroup DelegateSelectGroup(string groupName);
        private DelegateSelectGroup _delegateSelectGroup;

        public IAsyncResult BeginSelectGroup(string groupName, AsyncCallback callback)
        {
            _delegateSelectGroup = SelectGroup;
            return _delegateSelectGroup.BeginInvoke(groupName, callback, _delegateSelectGroup);
        }

        public NewsGroup EndSelectGroup(IAsyncResult result)
        {
            return (NewsGroup)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }
                
        /// <summary>
        /// Retrieves the article with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A byte array containing the article's data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// byte[] articleData = nntp.RetrieveArticle("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim articleData as Byte() = nntp.RetrieveArticle("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var articleData:byte[] = nntp.RetrieveArticle("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveArticle(string messageId)
        {
            return CommandMultiline("article <"+messageId.TrimStart('<').TrimEnd('>')+">");
        }

        private delegate byte[] DelegateRetrieveArticle(string messageId);
        private DelegateRetrieveArticle _delegateRetrieveArticle;

        public IAsyncResult BeginRetrieveArticle(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveArticle = RetrieveArticle;
            return _delegateRetrieveArticle.BeginInvoke(messageId, callback, _delegateRetrieveArticle);
        }

        public byte[] EndRetrieveArticle(IAsyncResult result)
        {
            return _delegateRetrieveArticle.EndInvoke(result);
        }
        
        /// <summary>
        /// Retrieves the article Header with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A byte array containing the article's header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// byte[] headerData = nntp.RetrieveHeader("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim headerData as Byte() = nntp.RetrieveHeader("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var headerData:byte[] = nntp.RetrieveHeader("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveHeader(string messageId)
        {
            return CommandMultiline("head <" + messageId.TrimStart('<').TrimEnd('>') + ">");
        }

        private delegate byte[] DelegateRetrieveHeader(string messageId);
        private DelegateRetrieveHeader _delegateRetrieveHeader;

        public IAsyncResult BeginRetrieveHeader(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveHeader = RetrieveHeader;
            return _delegateRetrieveHeader.BeginInvoke(messageId, callback, _delegateRetrieveHeader);
        }

        public byte[] EndRetrieveHeader(IAsyncResult result)
        {
            return _delegateRetrieveHeader.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves the article with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A string containing the article's data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// string articleData = nntp.RetrieveArticleString("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim articleData as String = nntp.RetrieveArticleString("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var articleData:string = nntp.RetrieveArticleString("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string RetrieveArticleString(string messageId)
        {
            byte[] data = CommandMultiline("article <" + messageId.TrimStart('<').TrimEnd('>') + ">");
            return Encoding.ASCII.GetString(data,0,data.Length);
        }

        private delegate string DelegateRetrieveArticleString(string messageId);
        private DelegateRetrieveArticleString _delegateRetrieveArticleString;

        public IAsyncResult BeginRetrieveArticleString(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveArticleString = RetrieveArticleString;
            return _delegateRetrieveArticleString.BeginInvoke(messageId, callback, _delegateRetrieveArticleString);
        }

        public string EndRetrieveArticleString(IAsyncResult result)
        {
            return _delegateRetrieveArticleString.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves the article Header with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A string containing the article's header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// string headerData = nntp.RetrieveHeaderString("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim headerData as String = nntp.RetrieveHeaderString("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var headerData:string = nntp.RetrieveHeaderString("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string RetrieveHeaderString(string messageId)
        {
            byte[] data = CommandMultiline("head <" + messageId + ">");
            return Encoding.ASCII.GetString(data,0,data.Length);
        }

        private delegate string DelegateRetrieveHeaderString(string messageId);
        private DelegateRetrieveHeaderString _delegateRetrieveHeaderString;

        public IAsyncResult BeginRetrieveHeaderString(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveHeaderString = RetrieveHeaderString;
            return _delegateRetrieveHeaderString.BeginInvoke(messageId, callback, _delegateRetrieveHeaderString);
        }

        public string EndRetrieveHeaderString(IAsyncResult result)
        {
            return _delegateRetrieveHeaderString.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves the article with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A Message object representing the article.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// Message article = nntp.RetrieveArticleObject("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim article as Message = nntp.RetrieveArticleObject("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var article:Message = nntp.RetrieveArticleObject("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public Message RetrieveArticleObject(string messageId)
        {
            return Parser.ParseMessage(CommandMultiline("article <"+messageId.Trim(new char[] {'<','>'})+">"));
        }

        private delegate Message DelegateRetrieveArticleObject(string messageId);
        private DelegateRetrieveArticleObject _delegateRetrieveArticleObject;

        public IAsyncResult BeginRetrieveArticleObject(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveArticleObject = RetrieveArticleObject;
            return _delegateRetrieveArticleObject.BeginInvoke(messageId, callback, _delegateRetrieveArticleObject);
        }

        public Message EndRetrieveArticleObject(IAsyncResult result)
        {
            return _delegateRetrieveArticleObject.EndInvoke(result);
        }


        /// <summary>
        /// Retrieves the article Header with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A Header object representing the article's header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// Header Header = nntp.RetrieveHeaderObject("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim Header as Header = nntp.RetrieveHeaderObject("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var header:Header = nntp.RetrieveHeaderObject("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public Header RetrieveHeaderObject(string messageId)
        {
            return Parser.ParseHeader(CommandMultiline("head <"+messageId.Trim(new char[] {'<','>'})+">"));
        }

        private delegate Header DelegateRetrieveHeaderObject(string messageId);
        private DelegateRetrieveHeaderObject _delegateRetrieveHeaderObject;

        public IAsyncResult BeginRetrieveHeaderObject(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveHeaderObject = RetrieveHeaderObject;
            return _delegateRetrieveHeaderObject.BeginInvoke(messageId, callback, _delegateRetrieveHeaderObject);
        }

        public Header EndRetrieveHeaderObject(IAsyncResult result)
        {
            return _delegateRetrieveHeaderObject.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves the article body with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A byte array containing the article's body.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// byte[] body = nntp.RetrieveBody("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim body as Byte() = nntp.RetrieveBody("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var body:byte[] = nntp.RetrieveBody("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveBody(string messageId)
        {
            return CommandMultiline("body <"+messageId.TrimEnd('>').TrimStart('<')+">");
        }

        private delegate byte[] DelegateRetrieveBody(string messageId);
        private DelegateRetrieveBody _delegateRetrieveBody;

        public IAsyncResult BeginRetrieveBody(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveBody = RetrieveBody;
            return _delegateRetrieveBody.BeginInvoke(messageId, callback, _delegateRetrieveBody);
        }

        public byte[] EndRetrieveBody(IAsyncResult result)
        {
            return _delegateRetrieveBody.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves the article body with the specified message-id.
        /// </summary>
        /// <param name="messageId">Message-Id of the article.</param>
        /// <returns>A string containing the article's body.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// string body = nntp.RetrieveBodyString("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// Dim body as String = nntp.RetrieveBodyString("3e061eae$1@news.myhost.com")
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// var body:string = nntp.RetrieveBodyString("3e061eae$1@news.myhost.com");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string RetrieveBodyString(string messageId)
        {
            byte[] data = CommandMultiline("body <" + messageId.TrimEnd('>').TrimStart('<') + ">");
            return Encoding.ASCII.GetString(data,0,data.Length);
        }

        private delegate string DelegateRetrieveBodyString(string messageId);
        private DelegateRetrieveBodyString _delegateRetrieveBodyString;

        public IAsyncResult BeginRetrieveBodyString(string messageId, AsyncCallback callback)
        {
            _delegateRetrieveBodyString = RetrieveBodyString;
            return _delegateRetrieveBodyString.BeginInvoke(messageId, callback, _delegateRetrieveBodyString);
        }

        public string EndRetrieveBodyString(IAsyncResult result)
        {
            return _delegateRetrieveBodyString.EndInvoke(result);
        }

        /// <summary>
        /// Posts the provided article.
        /// </summary>
        /// <param name="article">The article data as a string.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message article = new Message();
        /// article.NewsGroups = "myhost.info";
        /// article.From = new Address("john.doe@myhost.com","John Doe");
        /// article.Subject = "Test";
        /// article.Body = "Hello this is a test !";
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// if(nntp.PostingAllowed) nntp.Post(article.PostableString);
        /// else throw new NntpException("Posting not allowed. Couldn't post.");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim article as New Message
        /// article.NewsGroups = "myhost.info"
        /// article.From = New Address("john.doe@myhost.com","John Doe")
        /// article.Subject = "Test"
        /// article.Body = "Hello this is a test !"
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// If nntp.PostingAllowed Then
        /// nntp.Post(article.PostableString)
        /// Else
        /// Throw New NntpException("Posting not allowed. Couldn't post.")
        /// End If
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var article:Message = new Message();
        /// article.NewsGroups = "myhost.info";
        /// article.From = new Address("john.doe@myhost.com","John Doe");
        /// article.Subject = "Test";
        /// article.Body = "Hello this is a test !";
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// if(nntp.PostingAllowed) nntp.Post(article.PostableString);
        /// else throw new NntpException("Posting not allowed. Couldn't post.");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string Post(string article)
        {
            string response = Command("post");
            string messageid = "";
            if(response.StartsWith("340"))
                messageid = response.Substring(response.LastIndexOf(' '));
            else
                throw new NntpException("Command POST failed : " + response);
            GetStream().Write(Encoding.ASCII.GetBytes(article+"\r\n.\r\n"),0,article.Length+5);
            response = ReadLine();
            if(response.StartsWith("2"))
                return response;
            else throw new NntpException("POST failed : " + response);
        }
        
        private delegate string DelegatePost(string article);
        private DelegatePost _delegatePost;

        public IAsyncResult BeginPost(string article, AsyncCallback callback)
        {
            _delegatePost = Post;
            return _delegatePost.BeginInvoke(article, callback, _delegatePost);
        }

        /// <summary>
        /// Posts the provided article.
        /// </summary>
        /// <param name="article">The article data as a string.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message article = new Message();
        /// article.HeaderFields.Add("NewsGroups","myhost.info");
        /// article.From = new Address("john.doe@myhost.com","John Doe");
        /// article.Subject = "Test";
        /// article.Body = "Hello this is a test !";
        /// NntpClient nttp = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// if(nntp.PostingAllowed) nntp.Post(article);
        /// else throw new NntpException("Posting not allowed. Couldn't post.");
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim article as New Message
        /// article.HeaderFields.Add("NewsGroups","myhost.info")
        /// article.From = New Address("john.doe@myhost.com","John Doe")
        /// article.Subject = "Test"
        /// article.Body = "Hello this is a test !"
        /// Dim nttp as New NntpClient()
        /// nntp.Connect("news.myhost.com") 
        /// If nntp.PostingAllowed Then
        /// nntp.Post(article)
        /// Else
        /// Throw New NntpException("Posting not allowed. Couldn't post.")
        /// End If
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var article:Message = new Message();
        /// article.HeaderFields.Add("NewsGroups","myhost.info");
        /// article.From = new Address("john.doe@myhost.com","John Doe");
        /// article.Subject = "Test";
        /// article.Body = "Hello this is a test !";
        /// var nntp:NntpClient = new NntpClient();
        /// nntp.Connect("news.myhost.com");
        /// if(nntp.PostingAllowed) nntp.Post(article);
        /// else throw new NntpException("Posting not allowed. Couldn't post.");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string Post(Message article)
        {
            return Post(article.ToMimeString());
        }

        private delegate string DelegatePostMessage(Message article);
        private DelegatePostMessage _delegatePostMessage;

        public IAsyncResult BeginPost(Message article, AsyncCallback callback)
        {
            _delegatePostMessage = Post;
            return _delegatePostMessage.BeginInvoke(article, callback, _delegatePostMessage);
        }
        
        public string EndPost(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        #endregion

        #endregion

        #endregion 
    }
}