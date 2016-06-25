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
    /// Represents a mailbox.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class Mailbox : IMailbox
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Mailbox()
        {
            Fetch.ParentMailbox = this;
        }

        #region Methods

        #region Public methods

        /// <summary>
        /// Creates a child mailbox.
        /// </summary>
        /// <param name="mailboxName">The name of the child mailbox to be created.</param>
        /// <returns>The newly created mailbox.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// Mailbox staff = inbox.CreateChild("Staff");
        /// int zero = staff.MessageCount
        /// //Returns 0.
        /// inbox.Close();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// Dim staff As Mailbox = inbox.CreateChild("Staff")
        /// Dim zero As Integer = staff.MessageCount
        /// 'Returns 0.
        /// inbox.Close()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// var staff:Mailbox = inbox.CreateChild("Staff");
        /// var zero:int = staff.MessageCount
        /// //Returns 0.
        /// inbox.Close();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public IMailbox CreateChild(string mailboxName)
        {
            try
            {
                string separator = SourceClient.Command("list \"\" \"\"").Split('\"')[1].Split('\"')[0];
                return SourceClient.CreateMailbox(Name + separator+mailboxName);
            }
            catch(System.Net.Sockets.SocketException)
            {
                throw new Imap4Exception("CreateChild failed.\nThe mailbox' source client wasn't connected anymore.");
            }
        }

        private delegate IMailbox DelegateCreateChild(string mailboxName);
        private DelegateCreateChild _delegateCreateChild;

        public IAsyncResult BeginCreateChild(string mailboxName, AsyncCallback callback)
        {
            _delegateCreateChild = CreateChild;
            return _delegateCreateChild.BeginInvoke(mailboxName, callback, _delegateCreateChild);
        }

        public IMailbox EndCreateChild(IAsyncResult result)
        {
            return _delegateCreateChild.EndInvoke(result);
        }

        /// <summary>
        /// Subscribes to the mailbox.
        /// </summary>
        /// <returns>The server's response.</returns>
        public string Subscribe()
        {
            try
            {
                return SourceClient.SubscribeMailbox(Name);
            }
            catch(System.Net.Sockets.SocketException)
            {
                throw new Imap4Exception("Subscribe failed.\nThe mailbox' source client wasn't connected anymore.");
            }
        }

        private delegate string DelegateSubscribe();
        private DelegateSubscribe _delegateSubscribe;

        public IAsyncResult BeginSubscribe(AsyncCallback callback)
        {
            _delegateSubscribe = Subscribe;
            return _delegateSubscribe.BeginInvoke(callback, _delegateSubscribe);
        }

        public string EndSubscribe(IAsyncResult result)
        {
            return _delegateSubscribe.EndInvoke(result);
        }

        /// <summary>
        /// Unsubscribes from the mailbox.
        /// </summary>
        /// <returns>The server's response.</returns>
        public string Unsubscribe()
        {
            try
            {
                return SourceClient.UnsubscribeMailbox(Name);
            }
            catch(System.Net.Sockets.SocketException)
            {
                throw new Imap4Exception("Unsubscribe failed.\nThe mailbox' source client wasn't connected anymore.");
            }
        }

        private delegate string DelegateUnsubscribe();
        private DelegateUnsubscribe _delegateUnsubscribe;

        public IAsyncResult BeginUnsubscribe(AsyncCallback callback)
        {
            _delegateUnsubscribe = Unsubscribe;
            return _delegateUnsubscribe.BeginInvoke(callback, _delegateUnsubscribe);
        }

        public string EndUnsubscribe(IAsyncResult result)
        {
            return _delegateUnsubscribe.EndInvoke(result);
        }

        /// <summary>
        /// Deletes the mailbox.
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
        /// inbox.Delete();
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// inbox.Delete()
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// inbox.Delete();
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Delete()
        {
            try
            {
                return SourceClient.DeleteMailbox(Name);
            }
            catch(System.Net.Sockets.SocketException)
            {
                throw new Imap4Exception("Delete failed.\nThe mailbox' source client wasn't connected anymore.");
            }
        }

        private delegate string DelegateDelete();
        private DelegateDelete _delegateDelete;

        public IAsyncResult BeginDelete(AsyncCallback callback)
        {
            _delegateDelete = Delete;
            return _delegateDelete.BeginInvoke(callback, _delegateDelete);
        }

        public string EndDelete(IAsyncResult result)
        {
            return _delegateDelete.EndInvoke(result);
        }

        /// <summary>
        /// Renames the mailbox.
        /// </summary>
        /// <param name="newMailboxName">The new name of the mailbox.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("invox");
        /// inbox.Rename("inbox");
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("invox")
        /// inbox.Rename("inbox")
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("invox");
        /// inbox.Rename("inbox");
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Rename(string newMailboxName)
        {
            try
            {
                string response = SourceClient.RenameMailbox(Name, newMailboxName);
                Name = newMailboxName;
                return response;
            }
            catch(System.Net.Sockets.SocketException)
            {
                throw new Imap4Exception("Rename failed.\nThe mailbox' source client wasn't connected anymore.");
            }
        }

        private delegate string DelegateRename(string newMailboxName);
        private DelegateRename _delegateRename;

        public IAsyncResult BeginRename(string newMailboxName, AsyncCallback callback)
        {
            _delegateRename = Rename;
            return _delegateRename.BeginInvoke(newMailboxName, callback, _delegateRename);
        }

        public string EndRename(IAsyncResult result)
        {
            return _delegateRename.EndInvoke(result);
        }
        /// <summary>
        /// Searches the mailbox for messages corresponding to the query.
        /// </summary>
        /// <param name="query">Query to use.</param>
        /// <returns>An array of integers containing ordinal positions of messages matching the query.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// int[] ids = inbox.Search("SEARCH FLAGGED SINCE 1-Feb-1994 NOT FROM "Smith");
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// Dim ids() As Integer = inbox.Search("SEARCH FLAGGED SINCE 1-Feb-1994 NOT FROM "Smith")
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// var ids:int[] = inbox.Search("SEARCH FLAGGED SINCE 1-Feb-1994 NOT FROM "Smith");
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public int[] Search(string query)
        {
            
            string response = SourceClient.Command("search "+query);
            string[] parts = response.Substring(0,response.IndexOf("\r\n")).Split(' ');
            int[] messageOrdinals = new int[parts.Length-2];
            for(int i=2;i<parts.Length;i++)
                messageOrdinals[i-2] = Convert.ToInt32(parts[i]);
            return messageOrdinals;
        }

        private delegate int[] DelegateSearch(string query);
        private DelegateSearch _delegateSearch;

        public IAsyncResult BeginSearch(string query, AsyncCallback callback)
        {
            _delegateSearch = Search;
            return _delegateSearch.BeginInvoke(query, callback, _delegateSearch);
        }

        /// <summary>
        /// Search for messages accoridng to the given query.
        /// </summary>
        /// <param name="query">Query to use.</param>
        /// <returns>A collection of messages matching the query.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// MessageCollection messages = inbox.SearchParse("SEARCH FLAGGED SINCE 1-Feb-1994 NOT FROM "Smith");
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// Dim messages As MessageCollection = inbox.SearchParse("SEARCH FLAGGED SINCE 1-Feb-1994 NOT FROM "Smith")
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// var messages:MessageCollection = inbox.SearchParse("SEARCH FLAGGED SINCE 1-Feb-1994 NOT FROM "Smith");
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public MessageCollection SearchParse(string query)
        {
            MessageCollection msgs = new MessageCollection();
            foreach(int i in Search(query))
                msgs.Add(Fetch.MessageObject(i));
            return msgs;
        }

        private delegate MessageCollection DelegateSearchParse(string query);
        private DelegateSearchParse _delegateSearchParse;

        public IAsyncResult BeginSearchParse(string query, AsyncCallback callback)
        {
            _delegateSearchParse = SearchParse;
            return _delegateSearchParse.BeginInvoke(query, callback, _delegateSearchParse);
        }

        /// <summary>
        /// Searches the mailbox for messages corresponding to the query.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="charset">The charset the query has to be performed for.</param>
        /// <returns>An array of integers containing ordinal positions of messages matching the query.</returns>
        /// <example><see cref="Mailbox.Search"/></example>
        public int[] Search(string charset, string query)
        {
            
            string response = SourceClient.Command("search charset "+charset+" "+query);
            string[] parts = response.Substring(0,response.IndexOf("\r\n")).Split(' ');
            int[] messageOrdinals = new int[parts.Length-2];
            for(int i=2;i<parts.Length;i++) messageOrdinals[i-2] = Convert.ToInt32(parts[i]);
            return messageOrdinals;
        }

        private delegate int[] DelegateSearchStringString(string charset, string query);
        private DelegateSearchStringString _delegateSearchStringString;

        public IAsyncResult BeginSearch(string charset, string query, AsyncCallback callback)
        {
            _delegateSearchStringString = Search;
            return _delegateSearchStringString.BeginInvoke(charset, query, callback, _delegateSearchStringString);
        }

        public string EndSearch(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Search for messages accoridng to the given query.
        /// </summary>
        /// <param name="query">Query to use.</param>
        /// <param name="charset">The charset to apply the query for.</param>
        /// <returns>A collection of messages matching the query.</returns>
        /// <example><see cref="Mailbox.SearchParse"/></example>
        public MessageCollection SearchParse(string charset, string query)
        {
            MessageCollection msgs = new MessageCollection();
            foreach(int i in Search(charset,query)) msgs.Add(Fetch.MessageObject(i));
            return msgs;
        }

        private delegate MessageCollection DelegateSearchParseStringString(string charset, string query);
        private DelegateSearchParseStringString _delegateSearchParseStringString;

        public IAsyncResult BeginSearchParse(string charset, string query, AsyncCallback callback)
        {
            _delegateSearchParseStringString = SearchParse;
            return _delegateSearchParseStringString.BeginInvoke(charset, query, callback, _delegateSearchParseStringString);
        }

        public string EndSearchParse(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Adds the specified flags to the message.
        /// </summary>
        /// <param name="messageOrdinal">The message's ordinal position.</param>
        /// <param name="flags">Flags to be added to the message.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// FlagCollection flags = new FlagCollection();
        /// flags.Add("Draft");
        /// inbox.AddFlags(1,flags);
        /// //Message 1 is marked as draft.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// Dim flags As New FlagCollection
        /// flags.Add("Draft")
        /// inbox.AddFlags(1,flags)
        /// 'Message 1 is marked as draft.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// var flags:FlagCollection = new FlagCollection();
        /// flags.Add("Draft");
        /// inbox.AddFlags(1,flags);
        /// //Message is marked as draft.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string AddFlags(int messageOrdinal, IFlagCollection flags)
        {
            return SourceClient.Command("store " + messageOrdinal.ToString() + " +flags " + ((FlagCollection)flags).Merged);
        }

        private delegate string DelegateAddFlags(int messageOrdinal, IFlagCollection flags);
        private DelegateAddFlags _delegateAddFlags;

        public IAsyncResult BeginAddFlags(int messageOrdinal, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateAddFlags = AddFlags;
            return _delegateAddFlags.BeginInvoke(messageOrdinal, flags, callback, _delegateAddFlags);
        }

        public string EndAddFlags(IAsyncResult result)
        {
            return _delegateAddFlags.EndInvoke(result);
        }

        public string UidAddFlags(int uid, IFlagCollection flags)
        {
            return SourceClient.Command("uid store " + uid.ToString() + " +flags " + ((FlagCollection)flags).Merged);
        }

        private delegate string DelegateUidAddFlags(int uid, IFlagCollection flags);
        private DelegateUidAddFlags _delegateUidAddFlags;

        public IAsyncResult BeginUidAddFlags(int uid, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateUidAddFlags = UidAddFlags;
            return _delegateUidAddFlags.BeginInvoke(uid, flags, callback, _delegateUidAddFlags);
        }

        public string EndUidAddFlags(IAsyncResult result)
        {
            return _delegateUidAddFlags.EndInvoke(result);
        }

        /// <summary>
        /// Removes the specified flags from the message.
        /// </summary>
        /// <param name="messageOrdinal">The message's ordinal position.</param>
        /// <param name="flags">Flags to be removed from the message.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// FlagCollection flags = new FlagCollection();
        /// flags.Add("Read");
        /// inbox.RemoveFlags(1,flags);
        /// //Message 1 is marked as unread.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// Dim flags As New FlagCollection
        /// flags.Add("Read")
        /// inbox.RemoveFlags(1,flags)
        /// 'Message 1 is marked as unread.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// var flags:FlagCollection = new FlagCollection();
        /// flags.Add("Read");
        /// inbox.RemoveFlags(1,flags);
        /// //Message 1 is marked as unread.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string RemoveFlags(int messageOrdinal, IFlagCollection flags)
        {
            return SourceClient.Command("store " + messageOrdinal.ToString() + " -flags " + ((FlagCollection)flags).Merged);
        }

        private delegate string DelegateRemoveFlags(int messageOrdinal, IFlagCollection flags);
        private DelegateRemoveFlags _delegateRemoveFlags;

        public IAsyncResult BeginRemoveFlags(int messageOrdinal, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateRemoveFlags = RemoveFlags;
            return _delegateRemoveFlags.BeginInvoke(messageOrdinal, flags, callback, _delegateRemoveFlags);
        }

        public string EndRemoveFlags(IAsyncResult result)
        {
            return _delegateRemoveFlags.EndInvoke(result);
        }

        public string UidRemoveFlags(int uid, IFlagCollection flags)
        {
            return SourceClient.Command("uid store " + uid.ToString() + " -flags " + ((FlagCollection)flags).Merged);
        }

        private delegate string DelegateUidRemoveFlags(int uid, IFlagCollection flags);
        private DelegateUidRemoveFlags _delegateUidRemoveFlags;

        public IAsyncResult BeginUidRemoveFlags(int uid, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateUidRemoveFlags = UidRemoveFlags;
            return _delegateUidRemoveFlags.BeginInvoke(uid, flags, callback, _delegateUidRemoveFlags);
        }

        public string EndUidRemoveFlags(IAsyncResult result)
        {
            return _delegateUidRemoveFlags.EndInvoke(result);
        }
        /// <summary>
        /// Sets the specified flags for the message.
        /// </summary>
        /// <param name="messageOrdinal">The message's ordinal position.</param>
        /// <param name="flags">Flags to be stored for the message.</param>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// FlagCollection flags = new FlagCollection();
        /// flags.Add("Read");
        /// flags.Add("Answered");
        /// inbox.AddFlags(1,flags);
        /// //Message is marked as read and answered. All prior flags are unset.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// Dim flags As New FlagCollection
        /// flags.Add("Read")
        /// flags.Add("Answered")
        /// inbox.AddFlags(1,flags)
        /// 'Message is marked as read and answered. All prior flags are unset.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// var flags:FlagCollection = new FlagCollection();
        /// flags.Add("Read");
        /// flags.Add("Answered");
        /// inbox.AddFlags(1,flags);
        /// //Message is marked as read and answered. All prior flags are unset.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string SetFlags(int messageOrdinal, IFlagCollection flags)
        {
            return SourceClient.Command("store " + messageOrdinal.ToString() + " flags " + ((FlagCollection)flags).Merged);
        }

        private delegate string DelegateSetFlags(int messageOrdinal, IFlagCollection flags);
        private DelegateSetFlags _delegateSetFlags;

        public IAsyncResult BeginSetFlags(int messageOrdinal, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateSetFlags = SetFlags;
            return _delegateSetFlags.BeginInvoke(messageOrdinal, flags, callback, _delegateSetFlags);
        }

        public string EndSetFlags(IAsyncResult result)
        {
            return _delegateSetFlags.EndInvoke(result);
        }

        public string UidSetFlags(int uid, IFlagCollection flags)
        {
            return SourceClient.Command("uid store " + uid.ToString() + " flags " + ((FlagCollection)flags).Merged);
        }

        private delegate string DelegateUidSetFlags(int uid, IFlagCollection flags);
        private DelegateUidSetFlags _delegateUidSetFlags;

        public IAsyncResult BeginUidSetFlags(int uid, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateUidSetFlags = UidSetFlags;
            return _delegateUidSetFlags.BeginInvoke(uid, flags, callback, _delegateUidSetFlags);
        }

        public string EndUidSetFlags(IAsyncResult result)
        {
            return _delegateUidSetFlags.EndInvoke(result);
        }

        /// <summary>
        /// Same as <see cref="AddFlags"/> except no response is requested.
        /// </summary>
        /// <param name="messageOrdinal">The message's ordinal position.</param>
        /// <param name="flags">Flags to be added to the message.</param>
        /// <example><see cref="AddFlags"/></example>
        public void AddFlagsSilent(int messageOrdinal, IFlagCollection flags)
        {
            SourceClient.Command("store " + messageOrdinal.ToString() + " +flags.silent " + ((FlagCollection)flags).Merged);
        }
        /// <summary>
        /// Same as <see cref="AddFlags"/> except no response is requested.
        /// </summary>
        /// <param name="messageOrdinal">The message range ordinal position (from:to)</param>
        /// <param name="flags">Flags to be added to the message.</param>
        /// <example><see cref="AddFlags"/></example>
        public void AddFlagsSilent(string messageOrdinal, IFlagCollection flags)
        {
            SourceClient.Command("store " + messageOrdinal + " flags.silent " + ((FlagCollection)flags).Merged);
        }

        private delegate void DelegateAddFlagsSilent(int messageOrdinal, IFlagCollection flags);
        private DelegateAddFlagsSilent _delegateAddFlagsSilent;

        public IAsyncResult BeginAddFlagsSilent(int messageOrdinal, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateAddFlagsSilent = AddFlagsSilent;
            return _delegateAddFlagsSilent.BeginInvoke(messageOrdinal, flags, callback, _delegateAddFlagsSilent);
        }

        public void EndAddFlagsSilent(IAsyncResult result)
        {
            _delegateAddFlagsSilent.EndInvoke(result);
        }

        public void UidAddFlagsSilent(int uid, IFlagCollection flags)
        {
            SourceClient.Command("uid store " + uid.ToString() + " +flags.silent " + ((FlagCollection)flags).Merged);
        }

        private delegate void DelegateUidAddFlagsSilent(int uid, IFlagCollection flags);
        private DelegateUidAddFlagsSilent _delegateUidAddFlagsSilent;

        public IAsyncResult BeginUidAddFlagsSilent(int uid, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateUidAddFlagsSilent = UidAddFlagsSilent;
            return _delegateUidAddFlagsSilent.BeginInvoke(uid, flags, callback, _delegateUidAddFlagsSilent);
        }

        public void EndUidAddFlagsSilent(IAsyncResult result)
        {
            _delegateUidAddFlagsSilent.EndInvoke(result);
        }

        /// <summary>
        /// Same as <see cref="RemoveFlags"/> except no response is requested.
        /// </summary>
        /// <param name="messageOrdinal">The message's ordinal position.</param>
        /// <param name="flags">Flags to be removed from the message.</param>
        /// <example><see cref="RemoveFlags"/></example>
        public void RemoveFlagsSilent(int messageOrdinal, IFlagCollection flags)
        {
            SourceClient.Command("store " + messageOrdinal.ToString() + " -flags.silent " + ((FlagCollection)flags).Merged);
        }

        private delegate void DelegateRemoveFlagsSilent(int messageOrdinal, IFlagCollection flags);
        private DelegateRemoveFlagsSilent _delegateRemoveFlagsSilent;

        public IAsyncResult BeginRemoveFlagsSilent(int messageOrdinal, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateRemoveFlagsSilent = RemoveFlagsSilent;
            return _delegateRemoveFlagsSilent.BeginInvoke(messageOrdinal, flags, callback, _delegateRemoveFlagsSilent);
        }

        public void EndRemoveFlagsSilent(IAsyncResult result)
        {
            _delegateRemoveFlagsSilent.EndInvoke(result);
        }

        public void UidRemoveFlagsSilent(int uid, IFlagCollection flags)
        {
            SourceClient.Command("uid store " + uid.ToString() + " -flags.silent " + ((FlagCollection)flags).Merged);
        }

        private delegate void DelegateUidRemoveFlagsSilent(int uid, IFlagCollection flags);
        private DelegateUidRemoveFlagsSilent _delegateUidRemoveFlagsSilent;

        public IAsyncResult BeginUidRemoveFlagsSilent(int uid, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateUidRemoveFlagsSilent = UidRemoveFlagsSilent;
            return _delegateUidRemoveFlagsSilent.BeginInvoke(uid, flags, callback, _delegateUidRemoveFlagsSilent);
        }

        public void EndUidRemoveFlagsSilent(IAsyncResult result)
        {
            _delegateUidRemoveFlagsSilent.EndInvoke(result);
        }

        /// <summary>
        /// Same as <see cref="SetFlags"/> except no response is requested.
        /// </summary>
        /// <param name="messageOrdinal">The message's ordinal position.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        /// <example><see cref="SetFlags"/></example>
        public void SetFlagsSilent(int messageOrdinal, IFlagCollection flags)
        {
            SourceClient.Command("store " + messageOrdinal.ToString() + " flags.silent " + ((FlagCollection)flags).Merged);
        }

        private delegate void DelegateSetFlagsSilent(int messageOrdinal, IFlagCollection flags);
        private DelegateSetFlagsSilent _delegateSetFlagsSilent;

        public IAsyncResult BeginSetFlagsSilent(int messageOrdinal, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateSetFlagsSilent = SetFlagsSilent;
            return _delegateSetFlagsSilent.BeginInvoke(messageOrdinal, flags, callback, _delegateSetFlagsSilent);
        }

        public void EndSetFlagsSilent(IAsyncResult result)
        {
            _delegateSetFlagsSilent.EndInvoke(result);
        }

        public void UidSetFlagsSilent(int uid, IFlagCollection flags)
        {
            SourceClient.Command("uid store " + uid.ToString() + " flags.silent " + ((FlagCollection)flags).Merged);
        }

        private delegate void DelegateUidSetFlagsSilent(int uid, IFlagCollection flags);
        private DelegateUidSetFlagsSilent _delegateUidSetFlagsSilent;

        public IAsyncResult BeginUidSetFlagsSilent(int uid, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateUidSetFlagsSilent = UidSetFlagsSilent;
            return _delegateUidSetFlagsSilent.BeginInvoke(uid, flags, callback, _delegateUidSetFlagsSilent);
        }

        public void EndUidSetFlagsSilent(IAsyncResult result)
        {
            _delegateUidSetFlagsSilent.EndInvoke(result);
        }

        /// <summary>
        /// Copies the specified message to the specified mailbox.
        /// </summary>
        /// <param name="messageOrdinal">The ordinal of the message to be copied.</param>
        /// <param name="destinationMailboxName">The name of the destination mailbox.</param>
        /// <returns>The destination mailbox.</returns>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// inbox.CopyMessage(1,"Read Messages");
        /// //Copies message 1 to Read Messages mailbox.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// inbox.CopyMessage(1,"Read Messages")
        /// 'Copies message 1 to Read Messages mailbox.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// inbox.CopyMessage(1,"Read Messages");
        /// //Copies message 1 to Read Messages mailbox.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public void CopyMessage(int messageOrdinal, string destinationMailboxName)
        {
            SourceClient.Command("copy "+messageOrdinal.ToString()+" \""+destinationMailboxName+"\"");
        }

        private delegate void DelegateCopyMessage(int messageOrdinal, string destinationMailboxName);
        private DelegateCopyMessage _delegateCopyMessage;

        public IAsyncResult BeginCopyMessage(int messageOrdinal, string destinationMailboxName, AsyncCallback callback)
        {
            _delegateCopyMessage = CopyMessage;
            return _delegateCopyMessage.BeginInvoke(messageOrdinal, destinationMailboxName, callback, _delegateCopyMessage);
        }

        public void EndCopyMessage(IAsyncResult result)
        {
            _delegateCopyMessage.EndInvoke(result);
        }

        public void UidCopyMessage(int uid, string destinationMailboxName)
        {
            SourceClient.Command("uid copy "+uid.ToString()+" \""+destinationMailboxName+"\"");
        }

        private delegate void DelegateUidCopyMessage(int uid, string destinationMailboxName);
        private DelegateUidCopyMessage _delegateUidCopyMessage;

        public IAsyncResult BeginUidCopyMessage(int uid, string destinationMailboxName, AsyncCallback callback)
        {
            _delegateUidCopyMessage = UidCopyMessage;
            return _delegateUidCopyMessage.BeginInvoke(uid, destinationMailboxName, callback, _delegateUidCopyMessage);
        }

        public void EndUidCopyMessage(IAsyncResult result)
        {
            _delegateUidCopyMessage.EndInvoke(result);
        }

        public void MoveMessage(int messageOrdinal, string destinationMailboxName)
        {
            CopyMessage(messageOrdinal, destinationMailboxName);
            DeleteMessage(messageOrdinal, true);
        }

        private delegate void DelegateMoveMessage(int messageOrdinal, string destinationMailboxName);
        private DelegateMoveMessage _delegateMoveMessage;

        public IAsyncResult BeginMoveMessage(int messageOrdinal, string destinationMailboxName, AsyncCallback callback)
        {
            _delegateMoveMessage = MoveMessage;
            return _delegateMoveMessage.BeginInvoke(messageOrdinal, destinationMailboxName, callback, _delegateMoveMessage);
        }

        public void EndMoveMessage(IAsyncResult result)
        {
            _delegateMoveMessage.EndInvoke(result);
        }

        public void UidMoveMessage(int uid, string destinationMailboxName)
        {
            UidCopyMessage(uid, destinationMailboxName);
            UidDeleteMessage(uid, true);
        }

        private delegate void DelegateUidMoveMessage(int uid, string destinationMailboxName);
        private DelegateUidMoveMessage _delegateUidMoveMessage;

        public IAsyncResult BeginUidMoveMessage(int uid, string destinationMailboxName, AsyncCallback callback)
        {
            _delegateUidMoveMessage = UidMoveMessage;
            return _delegateUidMoveMessage.BeginInvoke(uid, destinationMailboxName, callback, _delegateUidMoveMessage);
        }

        public void EndUidMoveMessage(IAsyncResult result)
        {
            _delegateUidMoveMessage.EndInvoke(result);
        }


        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="messageLiteral">The message in a Rfc822 compliant format.</param>
        public string Append(string messageLiteral)
        {
            string firststamp = DateTime.Now.ToString("yyMMddhhmmss"+ DateTime.Now.Millisecond.ToString());
            SourceClient.Command("APPEND \""+ Name + "\" {"+messageLiteral.Length+"}",firststamp);
            return SourceClient.Command(messageLiteral,"",firststamp);
        }

        private delegate string DelegateAppend(string messageLiteral);
        private DelegateAppend _delegateAppend;

        public IAsyncResult BeginAppend(string messageLiteral, AsyncCallback callback)
        {
            _delegateAppend = Append;
            return _delegateAppend.BeginInvoke(messageLiteral, callback, _delegateAppend);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="messageLiteral">The message in a Rfc822 compliant format.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        public string Append(string messageLiteral, IFlagCollection flags)
        {
            string firststamp = DateTime.Now.ToString("yyMMddhhmmss"+ DateTime.Now.Millisecond.ToString());
            SourceClient.Command("APPEND \"" + Name + "\" " + ((FlagCollection)flags).Merged + " {" + (messageLiteral.Length) + "}", firststamp);
            return SourceClient.Command(messageLiteral,"",firststamp);
        }

        private delegate string DelegateAppendFlags(string messageLiteral, IFlagCollection flags);
        private DelegateAppendFlags _delegateAppendFlags;

        public IAsyncResult BeginAppend(string messageLiteral, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateAppendFlags = Append;
            return _delegateAppendFlags.BeginInvoke(messageLiteral, flags, callback, _delegateAppendFlags);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="messageLiteral">The message in a Rfc822 compliant format.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        /// <param name="dateTime">The internal date to be set for the message.</param>
        public string Append(string messageLiteral, IFlagCollection flags, DateTime dateTime)
        {
            string firststamp = DateTime.Now.ToString("yyMMddhhmmss"+ DateTime.Now.Millisecond.ToString());
            SourceClient.Command("APPEND \"" + Name + "\" " + ((FlagCollection)flags).Merged + " " + dateTime.ToString("r") + " {" + (messageLiteral.Length) + "}", firststamp);
            return SourceClient.Command(messageLiteral,"",firststamp);
        }

        private delegate string DelegateAppendFlagsDateTime(string messageLiteral, IFlagCollection flags, DateTime dateTime);
        private DelegateAppendFlagsDateTime _delegateAppendFlagsDateTime;

        public IAsyncResult BeginAppend(string messageLiteral, IFlagCollection flags, DateTime dateTime, AsyncCallback callback)
        {
            _delegateAppendFlagsDateTime = Append;
            return _delegateAppendFlagsDateTime.BeginInvoke(messageLiteral, flags, dateTime, callback, _delegateAppendFlagsDateTime);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="message">The message to be appended.</param>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Message message = new Message();
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.Subject = "hey!";
        /// message.Attachments.Add("C:\\myfile.doc");
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John.";
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// Mailbox inbox = imap.SelectMailbox("inbox");
        /// inbox.Append(message);
        /// imap.Disconnect();
        ///  
        /// VB.NET
        ///  
        /// Dim message As New Message
        /// message.From = new Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.Subject = "hey!"
        /// message.Attachments.Add("C:\myfile.doc")
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John."
        /// 
        /// Dim imap As New Imap4Client
        /// Dim inbox As Mailbox = imap.SelectMailbox("inbox")
        /// inbox.Append(message)
        /// imap.Disconnect()
        ///   
        /// JScript.NET
        ///  
        /// var message:Message = new Message();
        /// message.From = new Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.Subject = "hey!";
        /// message.Attachments.Add("C:\\myfile.doc");
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John."
        /// 
        /// var imap:Imap4Client = new Imap4Client();
        /// var inbox:Mailbox = imap.SelectMailbox("inbox");
        /// inbox.Append(message);
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Append(Message message)
        {
            return Append(message.ToMimeString());
        }

        private delegate string DelegateAppendMessage(Message message);
        private DelegateAppendMessage _delegateAppendMessage;

        public IAsyncResult BeginAppend(Message message, AsyncCallback callback)
        {
            _delegateAppendMessage = Append;
            return _delegateAppendMessage.BeginInvoke(message, callback, _delegateAppendMessage);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="message">The message to be appended.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Message message = new Message();
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.Subject = "hey!";
        /// message.Attachments.Add("C:\\myfile.doc");
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John.";
        /// 
        /// FlagCollection flags = new FlagCollection();
        /// flags.Add("Read");
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// Mailbox inbox = imap.SelectMailbox("Read Messages");
        /// inbox.Append(message,flags);
        /// imap.Disconnect();
        ///  
        /// VB.NET
        ///  
        /// Dim message As New Message
        /// message.From = new Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.Subject = "hey!"
        /// message.Attachments.Add("C:\myfile.doc")
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John."
        /// 
        /// Dim flags As New FlagCollection
        /// flags.Add("Read")
        ///  
        /// Dim imap As New Imap4Client
        /// Dim inbox As Mailbox = imap.SelectMailbox("Read Messages")
        /// inbox.Append(message,flags)
        /// imap.Disconnect()
        ///   
        /// JScript.NET
        ///  
        /// var message:Message = new Message();
        /// message.From = new Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.Subject = "hey!";
        /// message.Attachments.Add("C:\\myfile.doc");
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John."
        /// 
        /// var flags:FlagCollection = new FlagCollection();
        /// flags.Add("Read");
        ///  
        /// var imap:Imap4Client = new Imap4Client();
        /// var inbox:Mailbox = imap.SelectMailbox("Read Messages");
        /// inbox.Append(message,flags);
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Append(Message message, IFlagCollection flags)
        {
            return Append(message.ToMimeString(),flags);
        }

        private delegate string DelegateAppendMessageFlags(Message message, IFlagCollection flags);
        private DelegateAppendMessageFlags _delegateAppendMessageFlags;

        public IAsyncResult BeginAppend(Message message, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateAppendMessageFlags = Append;
            return _delegateAppendMessageFlags.BeginInvoke(message, flags, callback, _delegateAppendMessageFlags);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="message">The message to be appended.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        /// <param name="dateTime">The internal date to be set for the message.</param>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Message message = new Message();
        /// message.From = new Address("jdoe@myhost.com","John Doe");
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.Subject = "hey!";
        /// message.Attachments.Add("C:\\myfile.doc");
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John.";
        /// 
        /// FlagCollection flags = new FlagCollection();
        /// flags.Add("Read");
        /// 
        /// Imap4Client imap = new Imap4Client();
        /// Mailbox inbox = imap.SelectMailbox("Read Messages");
        /// inbox.Append(message,flags,System.DateTime.Now);
        /// imap.Disconnect();
        ///  
        /// VB.NET
        ///  
        /// Dim message As New Message
        /// message.From = new Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns")
        /// message.Subject = "hey!"
        /// message.Attachments.Add("C:\myfile.doc")
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John."
        /// 
        /// Dim flags As New FlagCollection
        /// flags.Add("Read")
        ///  
        /// Dim imap As New Imap4Client
        /// Dim inbox As Mailbox = imap.SelectMailbox("Read Messages")
        /// inbox.Append(message,flags,System.DateTime.Now)
        /// imap.Disconnect()
        ///   
        /// JScript.NET
        ///  
        /// var message:Message = new Message();
        /// message.From = new Address("jdoe@myhost.com","John Doe")
        /// message.To.Add("mjohns@otherhost.com","Mike Johns");
        /// message.Subject = "hey!";
        /// message.Attachments.Add("C:\\myfile.doc");
        /// message.HtmlBody.Text = "As promised, the requested document.&lt;br />&lt;br />Regards,&lt;br>John."
        /// 
        /// var flags:FlagCollection = new FlagCollection();
        /// flags.Add("Read");
        ///  
        /// var imap:Imap4Client = new Imap4Client();
        /// var inbox:Mailbox = imap.SelectMailbox("Read Messages");
        /// inbox.Append(message,flags,System.DateTime.Now);
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public string Append(Message message, IFlagCollection flags, DateTime dateTime)
        {
            return Append(message.ToMimeString(),flags,dateTime);
        }

        private delegate string DelegateAppendMessageFlagsDateTime(Message message, IFlagCollection flags, DateTime dateTime);
        private DelegateAppendMessageFlagsDateTime _delegateAppendMessageFlagsDateTime;

        public IAsyncResult BeginAppend(Message message, IFlagCollection flags, DateTime dateTime, AsyncCallback callback)
        {
            _delegateAppendMessageFlagsDateTime = Append;
            return _delegateAppendMessageFlagsDateTime.BeginInvoke(message, flags, dateTime, callback, _delegateAppendMessageFlagsDateTime);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="messageData">The message in a Rfc822 compliant format.</param>
        /// <example><see cref="Mailbox.Append"/></example>
        public string Append(byte[] messageData)
        {
            return Append(System.Text.Encoding.UTF8.GetString(messageData,0,messageData.Length));
        }

        private delegate string DelegateAppendByte(byte[] messageData);
        private DelegateAppendByte _delegateAppendByte;

        public IAsyncResult BeginAppend(byte[] messageData, AsyncCallback callback)
        {
            _delegateAppendByte = Append;
            return _delegateAppendByte.BeginInvoke(messageData, callback, _delegateAppendByte);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="messageData">The message in a Rfc822 compliant format.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        /// <example><see cref="Mailbox.Append"/></example>
        public string Append(byte[] messageData, IFlagCollection flags)
        {
            return Append(System.Text.Encoding.UTF8.GetString(messageData,0,messageData.Length),flags);
        }

        private delegate string DelegateAppendByteFlags(byte[] messageData, IFlagCollection flags);
        private DelegateAppendByteFlags _delegateAppendByteFlags;

        public IAsyncResult BeginAppend(byte[] messageData, IFlagCollection flags, AsyncCallback callback)
        {
            _delegateAppendByteFlags = Append;
            return _delegateAppendByteFlags.BeginInvoke(messageData, flags, callback, _delegateAppendByteFlags);
        }

        /// <summary>
        /// Appends the provided message to the mailbox.
        /// </summary>
        /// <param name="messageData">The message in a Rfc822 compliant format.</param>
        /// <param name="flags">Flags to be set for the message.</param>
        /// <param name="dateTime">The internal date to be set for the message.</param>
        /// <example><see cref="Mailbox.Append"/></example>
        public string Append(byte[] messageData, IFlagCollection flags, DateTime dateTime)
        {
            return Append(System.Text.Encoding.UTF8.GetString(messageData,0,messageData.Length),flags,dateTime);
        }

        private delegate string DelegateAppendByteFlagsDateTime(byte[] messageData, IFlagCollection flags, DateTime dateTime);
        private DelegateAppendByteFlagsDateTime _delegateAppendByteFlagsDateTime;

        public IAsyncResult BeginAppend(byte[] messageData, IFlagCollection flags, DateTime dateTime, AsyncCallback callback)
        {
            _delegateAppendByteFlagsDateTime = Append;
            return _delegateAppendByteFlagsDateTime.BeginInvoke(messageData, flags, dateTime, callback, _delegateAppendByteFlagsDateTime);
        }

        public string EndAppend(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Empties the mailbox.
        /// </summary>
        /// <param name="expunge">If true, all messages are permanently removed. Otherwise they are all marked with the Deleted flag.</param>
        /// <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// inbox.Empty(true);
        /// //Messages from inbox are permanently removed.
        /// imap.Disconnect();
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// inbox.Empty(True)
        /// 'Messages from inbox are permanently removed.
        /// imap.Disconnect()
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// inbox.Empty(true);
        /// //Messages from inbox are permanently removed.
        /// imap.Disconnect();
        /// </code>
        /// </example>
        public void Empty(bool expunge)
        {
            FlagCollection flags = new FlagCollection();
            flags.Add("Deleted");
            switch (MessageCount)
            {
                case 0:
                    break;
                case 1:
                    AddFlagsSilent(1, flags);
                    break;
                default:
                    AddFlagsSilent("1:" + MessageCount, flags);
                    break;
            }
            if (expunge)
                SourceClient.Expunge();
        }

        private delegate void DelegateEmpty(bool expunge);
        private DelegateEmpty _delegateEmpty;

        public IAsyncResult BeginEmpty(bool expunge, AsyncCallback callback)
        {
            _delegateEmpty = Empty;
            return _delegateEmpty.BeginInvoke(expunge, callback, _delegateEmpty);
        }

        public void EndEmpty(IAsyncResult result)
        {
            _delegateEmpty.EndInvoke(result);
        }

        /// <summary>
        /// Deletes the specified message.
        /// </summary>
        /// <param name="messageOrdinal">Ordinal position of the message to be deleted.</param>
        /// <param name="expunge">If true, message is permanently removed. Otherwise it is marked with the Deleted flag.</param>
        // <example>
        /// <code>
        /// C#
        ///  
        /// Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// Mailbox inbox = imap.SelectInbox("inbox");
        /// inbox.Delete(1,false);
        /// //Message 1 has been marked for deletion but is not deleted yet.
        /// imap.Disconnect();
        /// //Message 1 is now permanently removed.
        /// 
        /// VB.NET
        ///  
        /// Dim imap As New Imap4Client
        /// imap.Connect("mail.myhost.com")
        /// imap.Login("jdoe1234","tanstaaf")
        /// Dim inbox As Mailbox = imap.SelectInbox("inbox")
        /// inbox.Delete(1,False)
        /// 'Message 1 has been marked for deletion but is not deleted yet.
        /// imap.Disconnect()
        /// 'Message 1 is now permanently removed.
        /// 
        /// JScript.NET
        ///  
        /// var imap:Imap4Client imap = new Imap4Client();
        /// imap.Connect("mail.myhost.com");
        /// imap.Login("jdoe1234","tanstaaf");
        /// var inbox:Mailbox = imap.SelectInbox("inbox");
        /// inbox.Delete(1,false);
        /// //Message 1 has been marked for deletion but is not deleted yet.
        /// imap.Disconnect();
        /// //Message 1 is now permanently removed.
        /// </code>
        /// </example>
        public void DeleteMessage(int messageOrdinal, bool expunge)
        {
            FlagCollection flags = new FlagCollection();
            flags.Add("Deleted");
            AddFlagsSilent(messageOrdinal,flags);
            if(expunge)
                SourceClient.Expunge();
        }

        private delegate void DelegateDeleteMessage(int messageOrdinal, bool expunge);
        private DelegateDeleteMessage _delegateDeleteMessage;

        public IAsyncResult BeginDeleteMessage(int messageOrdinal, bool expunge, AsyncCallback callback)
        {
            _delegateDeleteMessage = DeleteMessage;
            return _delegateDeleteMessage.BeginInvoke(messageOrdinal, expunge, callback, _delegateDeleteMessage);
        }

        public void EndDeleteMessage(IAsyncResult result)
        {
            _delegateDeleteMessage.EndInvoke(result);
        }

        public void UidDeleteMessage(int uid, bool expunge)
        {
            FlagCollection flags = new FlagCollection();
            flags.Add("Deleted");
            UidAddFlagsSilent(uid,flags);
            if(expunge)
                SourceClient.Expunge();
        }

        private delegate void DelegateUidDeleteMessage(int uid, bool expunge);
        private DelegateUidDeleteMessage _delegateUidDeleteMessage;

        public IAsyncResult BeginUidDeleteMessage(int uid, bool expunge, AsyncCallback callback)
        {
            _delegateUidDeleteMessage = UidDeleteMessage;
            return _delegateUidDeleteMessage.BeginInvoke(uid, expunge, callback, _delegateUidDeleteMessage);
        }

        public void EndUidDeleteMessage(IAsyncResult result)
        {
            _delegateUidDeleteMessage.EndInvoke(result);
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// The Imap4Client object that will be used to perform commands on the server.
        /// </summary>
        public Imap4Client SourceClient { get; set; }
        /// <summary>
        /// The full (hierarchical) name of the mailbox.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The name of the mailbox, without hierarchy.
        /// </summary>
        public string ShortName
        {
            get { return Name.Substring(Name.LastIndexOf("/") + 1); }
        }
        /// <summary>
        /// The amount of recent messages (messages that have been added since this mailbox was last checked).
        /// </summary>
        public int Recent { get; set; }
        /// <summary>
        /// The amount of messages in the mailbox.
        /// </summary>
        public int MessageCount { get; set; }
        /// <summary>
        /// The ordinal position of the first unseen message in the mailbox.
        /// </summary>
        public int FirstUnseen { get; set; }
        /// <summary>
        /// The Uid Validity number. This number allows to check if Unique Identifiers have changed since the mailbox was last checked.
        /// </summary>
        public int UidValidity { get; set; }
        /// <summary>
        /// Flags that are applicable in this mailbox.
        /// </summary>
        public FlagCollection ApplicableFlags { get; set; } = new FlagCollection();
        /// <summary>
        /// Flags that the client can permanently set in this mailbox.
        /// </summary>
        public FlagCollection PermanentFlags { get; set; } = new FlagCollection();
        /// <summary>
        /// The mailbox's permission (ReadWrite or ReadOnly)
        /// </summary>
        public MailboxPermission Permission { get; set; } = new MailboxPermission();
        /// <summary>
        /// The mailbox's child mailboxes.
        /// </summary>
        public MailboxCollection SubMailboxes { get; set; } = new MailboxCollection();
        /// <summary>
        /// The mailbox's fetching utility.
        /// </summary>
        public Fetch Fetch { get; set; } = new Fetch();
        #endregion
    }
}