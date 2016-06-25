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
    /// Represents a newsgroup.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class NewsGroup
    {

        #region Private fields
        private int pointer;
        NntpClient _nntp;

        #endregion

        #region Constructors

        internal NewsGroup(string name, int firstArticle, int lastArticle, bool postingAllowed, NntpClient nntp)
        {
            Name = name;
            FirstArticle = firstArticle;
            LastArticle = lastArticle;
            PostingAllowed = postingAllowed;
            _nntp = nntp;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The newsgroup's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The ordinal position of the newsgroup's first article.
        /// </summary>
        public int FirstArticle { get; set; }
        /// <summary>
        /// The ordinal position of the newsgroup's last article.
        /// </summary>
        public int LastArticle { get; set; }
        /// <summary>
        /// The current article pointer's position.
        /// </summary>
        public int Pointer
        {
            get { return pointer; }
            set
            {
                _nntp.Command("stat " + value.ToString());
                pointer = value;
            }
        }
        /// <summary>
        /// True if posting is allowed on this newsgroup.
        /// </summary>
        public bool PostingAllowed { get; set; }
        /// <summary>
        /// The amount of article in this newsgroup.
        /// </summary>
        public int ArticleCount
        {
            get { return LastArticle - FirstArticle + 1; }
        }

        #endregion

        #region Methods

        #region Public methods

        /// <summary>
        /// Advances the current article pointer to the next article.
        /// </summary>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //group.Pointer is equal to group.FirstArticle.
        /// group.Next();
        /// //group.Pointer is now equal to group.FirstArticle + 1.
        /// //Retrieve the second article in this group.
        /// Message article2 = group.RetrieveArticleObject();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'group.Pointer is equal to group.FirstArticle.
        /// group.Next()
        /// 'group.Pointer is now equal to group.FirstArticle + 1.
        /// 'Retrieve the second article in this group.
        /// Dim article2 As Message = group.RetrieveArticleObject()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //group.Pointer is equal to group.FirstArticle.
        /// group.Next();
        /// //group.Pointer is now equal to group.FirstArticle + 1.
        /// //Retrieve the second article in this group.
        /// var article2:Message = group.RetrieveArticleObject();
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string Next()
        {
            string response = _nntp.Command("next");
            if(response.StartsWith("223"))
                Pointer = Convert.ToInt32(response.Split(' ')[1]);
            return response;
        }

        private delegate string DelegateNext();
        private DelegateNext _delegateNext;

        public IAsyncResult BeginNext(AsyncCallback callback)
        {
            _delegateNext = Next;
            return _delegateNext.BeginInvoke(callback, _delegateNext);
        }

        public string EndNext(IAsyncResult result)
        {
            return _delegateNext.EndInvoke(result);
        }

        /// <summary>
        /// Steps back the current article pointer to the previous article.
        /// </summary>
        /// <returns>The server's response.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //group.Pointer is equal to group.FirstArticle.
        /// group.Next();
        /// //group.Pointer is now equal to group.FirstArticle + 1.
        /// group.Previous();
        /// //group.Pointer is now equal to group.FirstArticle.
        /// //This retrieves the first article of the group.
        /// Message article1 = group.RetrieveArticleObject();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'group.Pointer is equal to group.FirstArticle.
        /// group.Next()
        /// 'group.Pointer is now equal to group.FirstArticle + 1.
        /// group.Previous();
        /// 'group.Pointer is now equal to group.FirstArticle.
        /// 'This retrieves the first article of the group.
        /// Dim article 1 As Message = group.RetrieveArticleObject()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //group.Pointer is equal to group.FirstArticle.
        /// group.Next();
        /// //group.Pointer is now equal to group.FirstArticle + 1.
        /// group.Previous();
        /// //group.Pointer is now equal to group.FirstArticle.
        /// //This retrieves the first article of the group.
        /// var article1:Message = group.RetrieveArticleObject();
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string Previous()
        {
            string response = _nntp.Command("last");
            if(response.StartsWith("223"))
                Pointer = Convert.ToInt32(response.Split(' ')[1]);
            return response;
        }

        private delegate string DelegatePrevious();
        private DelegatePrevious _delegatePrevious;

        public IAsyncResult BeginPrevious(AsyncCallback callback)
        {
            _delegatePrevious = Previous;
            return _delegatePrevious.BeginInvoke(callback, _delegatePrevious);
        }

        public string EndPrevious(IAsyncResult result)
        {
            return _delegatePrevious.EndInvoke(result);
        }

        /// <summary>
        /// Retrieves the article at the specified ordinal position.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <returns>A byte array containing the article data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve article at position 29 in this group.
        /// byte[] article29 = group.RetrieveArticle(29);
        /// //Retrieve last article in this group.
        /// byte[] article = group.RetrieveArticle(group.LastArticle);
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve article at position 29 in this group.
        /// Dim article29() As Byte = group.RetrieveArticle(29)
        /// 'Retrieve last article in this group.
        /// Dim article() As Byte = group.RetrieveArticle(group.LastArticle)
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve article at position 29 in this group.
        /// var article29:byte[] = group.RetrieveArticle(29);
        /// //Retrieve last article in this group.
        /// var article:byte[] = group.RetrieveArticle(group.LastArticle);
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveArticle(int index)
        {
            _nntp.OnMessageRetrieving(new MessageRetrievingEventArgs(index));
            byte[] buffer = _nntp.CommandMultiline("article "+index.ToString());
            if(Encoding.ASCII.GetString(buffer,0,buffer.Length).StartsWith("220"))
                Pointer = index;
            _nntp.OnMessageRetrieved(new MessageRetrievedEventArgs(buffer,index));
            return buffer;
        }

        private delegate byte[] DelegateRetrieveArticleInt(int index);
        private DelegateRetrieveArticleInt _delegateRetrieveArticleInt;

        public IAsyncResult BeginRetrieveArticle(int index, AsyncCallback callback)
        {
            _delegateRetrieveArticleInt = RetrieveArticle;
            return _delegateRetrieveArticleInt.BeginInvoke(index, callback, _delegateRetrieveArticleInt);
        }

        /// <summary>
        /// Retrieves the article at the position specified by the current article pointer.
        /// </summary>
        /// <returns>A byte array containing the article data.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the first article in this group.
        /// byte[] article = group.RetrieveArticle();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the first article in this group.
        /// Dim article() As Byte = group.RetrieveArticle()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the first article in this group.
        /// var article:byte[] = group.RetrieveArticle();
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveArticle()
        {
            if(Pointer != 0 & Pointer != -1)
                return _nntp.CommandMultiline("article");
            else 
            {
                Pointer = FirstArticle;
                _nntp.OnMessageRetrieving(new MessageRetrievingEventArgs(Pointer));
                byte[] buffer = _nntp.CommandMultiline("article "+ FirstArticle.ToString());
                _nntp.OnMessageRetrieved(new MessageRetrievedEventArgs(buffer, Pointer));
                return buffer;
            }
        }

        private delegate byte[] DelegateRetrieveArticle();
        private DelegateRetrieveArticle _delegateRetrieveArticle;

        public IAsyncResult BeginRetrieveArticle(AsyncCallback callback)
        {
            _delegateRetrieveArticle = RetrieveArticle;
            return _delegateRetrieveArticle.BeginInvoke(callback, _delegateRetrieveArticle);
        }

        public byte[] EndRetrieveArticle(IAsyncResult result)
        {
            return (byte[])result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Retrieves the article at the specified ordinal position.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <returns>A Message object representing the article.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve article at position 29 in this group.
        /// Message article29 = group.RetrieveArticleObject(29);
        /// //Retrieve last article in this group.
        /// Message article = group.RetrieveArticleObject(group.LastArticle);
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve article at position 29 in this group.
        /// Dim article29 As Message = group.RetrieveArticleObject(29)
        /// 'Retrieve last article in this group.
        /// Dim article As Message = group.RetrieveArticleObject(group.LastArticle)
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve article at position 29 in this group.
        /// var article29:Message = group.RetrieveArticleObject(29);
        /// //Retrieve last article in this group.
        /// var article:Message = group.RetrieveArticleObject(group.LastArticle);
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public Message RetrieveArticleObject(int index)
        {
            return Parser.ParseMessage(RetrieveArticle(index));
        }

        private delegate Message DelegateRetrieveArticleObjectInt(int index);
        private DelegateRetrieveArticleObjectInt _delegateRetrieveArticleObjectInt;

        public IAsyncResult BeginRetrieveArticleObject(int index, AsyncCallback callback)
        {
            _delegateRetrieveArticleObjectInt = RetrieveArticleObject;
            return _delegateRetrieveArticleObjectInt.BeginInvoke(index, callback, _delegateRetrieveArticleObjectInt);
        }

        /// <summary>
        /// Retrieves the article at the position specified by the current article pointer.
        /// </summary>
        /// <returns>A Message object representing the article.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the first article in this group.
        /// Message article = group.RetrieveArticleObject();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the first article in this group.
        /// Dim article As Message = group.RetrieveArticleObject()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the first article in this group.
        /// var article:Message = group.RetrieveArticleObject();
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public Message RetrieveArticleObject()
        {
            return Parser.ParseMessage(RetrieveArticle());
        }

        private delegate Message DelegateRetrieveArticleObject();
        private DelegateRetrieveArticleObject _delegateRetrieveArticleObject;

        public IAsyncResult BeginRetrieveArticleObject(AsyncCallback callback)
        {
            _delegateRetrieveArticleObject = RetrieveArticleObject;
            return _delegateRetrieveArticleObject.BeginInvoke(callback, _delegateRetrieveArticleObject);
        }

        public Message EndRetrieveArticleObject(IAsyncResult result)
        {
            return (Message)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Retrieves the article Header at the specified ordinal position.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <returns>A byte array containing the article header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the Header of the article at position 29 in this group.
        /// byte[] header29 = group.RetrieveHeader(29);
        /// //Retrieve last Header in this group.
        /// byte[] Header = group.RetrieveHeader(group.LastHeader);
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the Header of the article at position 29 in this group.
        /// Dim header29() As Byte = group.RetrieveHeader(29)
        /// 'Retrieve last Header in this group.
        /// Dim header() As Byte = group.RetrieveHeader(group.LastHeader)
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the Header of the article at position 29 in this group.
        /// var header29:byte[] = group.RetrieveHeader(29);
        /// //Retrieve last Header in this group.
        /// var header:byte[] = group.RetrieveHeader(group.LastHeader);
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveHeader(int index)
        {
            _nntp.OnHeaderRetrieving(new HeaderRetrievingEventArgs(index));
            byte[] buffer = _nntp.CommandMultiline("head "+index.ToString());
            if(Encoding.ASCII.GetString(buffer,0,buffer.Length).StartsWith("221"))
                Pointer = index;
            _nntp.OnHeaderRetrieved(new HeaderRetrievedEventArgs(buffer,index));
            return buffer;
        }

        private delegate byte[] DelegateRetrieveHeaderInt(int index);
        private DelegateRetrieveHeaderInt _delegateRetrieveHeaderInt;

        public IAsyncResult BeginRetrieveHeader(int index, AsyncCallback callback)
        {
            _delegateRetrieveHeaderInt = RetrieveHeader;
            return _delegateRetrieveHeaderInt.BeginInvoke(index, callback, _delegateRetrieveHeaderInt);
        }

        /// <summary>
        /// Retrieves the article Header at the position specified by the current article pointer.
        /// </summary>
        /// <returns>A byte array containing the article header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the first Header in this group.
        /// byte[] Header = group.RetrieveHeader();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the first Header in this group.
        /// Dim header() As Byte = group.RetrieveHeader()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the first Header in this group.
        /// var header:byte[] = group.RetrieveHeader();
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveHeader()
        {
            if(Pointer != 0 & Pointer != -1)
                return _nntp.CommandMultiline("head");
            else 
            {
                Pointer = FirstArticle;
                _nntp.OnHeaderRetrieving(new HeaderRetrievingEventArgs(Pointer));
                byte[] buffer = _nntp.CommandMultiline("head "+ FirstArticle.ToString());
                _nntp.OnHeaderRetrieved(new HeaderRetrievedEventArgs(buffer, Pointer));
                return buffer;
            }
        }

        private delegate byte[] DelegateRetrieveHeader();
        private DelegateRetrieveHeader _delegateRetrieveHeader;

        public IAsyncResult BeginRetrieveHeader(AsyncCallback callback)
        {
            _delegateRetrieveHeader = RetrieveHeader;
            return _delegateRetrieveHeader.BeginInvoke(callback, _delegateRetrieveHeader);
        }

        public byte[] EndRetrieveHeader(IAsyncResult result)
        {
            return (byte[])result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Stores the article Header at the specified ordinal position to the specified path.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <param name="filePath">The destination path for the file.</param>
        /// <returns>The path the file has been saved at.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Store the Header of the article at position 29 in this group.
        /// group.StoreHeader(29,"C:\\My news\\header.txt");
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Store the Header of the article at position 29 in this group.
        /// group.StoreHeader(29,"C:\My news\header.txt")
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Store the Header of the article at position 29 in this group.
        /// group.StoreHeader(29,"C:\\My news\\header.txt");
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string StoreHeader(int index, string filePath)
        {
            return StoreToFile(filePath, RetrieveHeader(index));
        }

        private delegate string DelegateStoreHeaderInt(int index, string filepath);
        private DelegateStoreHeaderInt _delegateStoreHeaderInt;

        public IAsyncResult BeginStoreHeader(int index, string filePath, AsyncCallback callback)
        {
            _delegateStoreHeaderInt = StoreHeader;
            return _delegateStoreHeaderInt.BeginInvoke(index, filePath, callback, _delegateStoreHeaderInt);
        }

        /// <summary>
        /// Retrieves the article Header at the position specified by the current article pointer.
        /// </summary>
        /// <param name="filePath">The destination path for the file.</param>
        /// <returns>The path the file has been saved at.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Store the first article in this group.
        /// group.StoreHeader("C:\\My news\\header.txt");
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Store the first article in this group.
        /// group.StoreHeader("C:\My news\header.txt")
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Store the first article in this group.
        /// group.StoreHeader("C:\\My news\\header.txt");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string StoreHeader(string filePath)
        {
            return StoreToFile(filePath, RetrieveHeader());
        }

        private delegate string DelegateStoreHeader(string filepath);
        private DelegateStoreHeader _delegateStoreHeader;

        public IAsyncResult BeginStoreHeader(string filepath, AsyncCallback callback)
        {
            _delegateStoreHeader = StoreHeader;
            return _delegateStoreHeader.BeginInvoke(filepath, callback, _delegateStoreHeader);
        }

        public string EndStoreHeader(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Stores the article at the specified ordinal position to the specified path.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <param name="filePath">The destination path for the file.</param>
        /// <returns>The path the file has been saved at.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Store article at position 29 in this group.
        /// group.StoreArticle(29,"C:\\My news\\article.nws");
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Store article at position 29 in this group.
        /// group.StoreArticle(29,"C:\My news\article.nws")
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Store article at position 29 in this group.
        /// group.StoreArticle(29,"C:\\My news\\article.nws");
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string StoreArticle(int index, string filePath)
        {
            return StoreToFile(filePath, RetrieveArticle(index));
        }

        private delegate string DelegateStoreArticleInt(int index, string filepath);
        private DelegateStoreArticleInt _delegateStoreArticleInt;

        public IAsyncResult BeginStoreArticle(int index, string filePath, AsyncCallback callback)
        {
            _delegateStoreArticleInt = StoreArticle;
            return _delegateStoreArticleInt.BeginInvoke(index, filePath, callback, _delegateStoreArticleInt);
        }

        /// <summary>
        /// Retrieves the article at the position specified by the current article pointer.
        /// </summary>
        /// <param name="filePath">The destination path for the file.</param>
        /// <returns>The path the file has been saved at.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Store the first article in this group.
        /// group.StoreArticle("C:\\My news\\article.nws");
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Store the first article in this group.
        /// group.StoreArticle("C:\My news\article.nws")
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Store the first article in this group.
        /// group.StoreArticle("C:\\My news\\article.nws");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string StoreArticle(string filePath)
        {
            return StoreToFile(filePath, RetrieveArticle());
        }

        private delegate string DelegateStoreArticle(string filepath);
        private DelegateStoreArticle _delegateStoreArticle;

        public IAsyncResult BeginStoreArticle(string filepath, AsyncCallback callback)
        {
            _delegateStoreArticle = StoreArticle;
            return _delegateStoreArticle.BeginInvoke(filepath, callback, _delegateStoreArticle);
        }

        public string EndStoreArticle(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Stores the article body at the specified ordinal position to the specified path.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <param name="filePath">The destination path for the file.</param>
        /// <returns>The path the file has been saved at.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Store the body of the article at position 29 in this group.
        /// group.StoreBody(29,"C:\\My news\\body.txt");
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Store the body of the article at position 29 in this group.
        /// group.StoreBody(29,"C:\My news\body.txt")
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Store the body of the article at position 29 in this group.
        /// group.StoreBody(29,"C:\\My news\\body.txt");
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string StoreBody(int index, string filePath)
        {
            return StoreToFile(filePath, RetrieveBody(index));
        }

        private delegate string DelegateStoreBodyInt(int index, string filepath);
        private DelegateStoreBodyInt _delegateStoreBodyInt;

        public IAsyncResult BeginStoreBody(int index, string filePath, AsyncCallback callback)
        {
            _delegateStoreBodyInt = StoreBody;
            return _delegateStoreBodyInt.BeginInvoke(index, filePath, callback, _delegateStoreBodyInt);
        }

        /// <summary>
        /// Retrieves the article body at the position specified by the current article pointer.
        /// </summary>
        /// <param name="filePath">The destination path for the file.</param>
        /// <returns>The path the file has been saved at.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Store the first article in this group.
        /// group.StoreBody("C:\\My news\\article.txt");
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Store the first article in this group.
        /// group.StoreBody("C:\My news\article.txt")
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Store the first article in this group.
        /// group.StoreBody("C:\\My news\\article.txt");
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public string StoreBody(string filePath)
        {
            return StoreToFile(filePath, RetrieveBody());
        }

        private delegate string DelegateStoreBody(string filepath);
        private DelegateStoreBody _delegateStoreBody;

        public IAsyncResult BeginStoreBody(string filepath, AsyncCallback callback)
        {
            _delegateStoreBody = StoreBody;
            return _delegateStoreBody.BeginInvoke(filepath, callback, _delegateStoreBody);
        }

        public string EndStoreBody(IAsyncResult result)
        {
            return (string)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Stores the given byte array into a file at the specified location.
        /// </summary>
        /// <param name="path">Path of the file to be created.</param>
        /// <param name="data">Data of the file to be created.</param>
        /// <returns>The path where the file has been created.</returns>
        private string StoreToFile(string path, byte[] data)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            fs.Write(data,0,data.Length);
            fs.Close();
            return path;
        }

        /// <summary>
        /// Retrieves the article Header at the specified ordinal position.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <returns>A Header object representing the article header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the Header of the article at position 29 in this group.
        /// Header header29 = group.RetrieveHeaderObject(29);
        /// //Retrieve last Header in this group.
        /// Header Header = group.RetrieveHeaderObject(group.LastHeader);
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the Header of the article at position 29 in this group.
        /// Dim header29 As Header = group.RetrieveHeaderObject(29)
        /// 'Retrieve last Header in this group.
        /// Dim Header As Header = group.RetrieveHeaderObject(group.LastHeader)
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the Header of the article at position 29 in this group.
        /// var header29:Header = group.RetrieveHeaderObject(29);
        /// //Retrieve last Header in this group.
        /// var header:Header = group.RetrieveHeaderObject(group.LastHeader);
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public Header RetrieveHeaderObject(int index)
        {
            return Parser.ParseHeader(RetrieveHeader(index));
        }

        private delegate Header DelegateRetrieveHeaderObjectInt(int index);
        private DelegateRetrieveHeaderObjectInt _delegateRetrieveHeaderObjectInt;

        public IAsyncResult BeginRetrieveHeaderObject(int index, AsyncCallback callback)
        {
            _delegateRetrieveHeaderObjectInt = RetrieveHeaderObject;
            return _delegateRetrieveHeaderObjectInt.BeginInvoke(index, callback, _delegateRetrieveHeaderObjectInt);
        }

        /// <summary>
        /// Retrieves the article Header at the position specified by the current article pointer.
        /// </summary>
        /// <returns>A Header object representing the article header.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the first Header in this group.
        /// Header Header = group.RetrieveHeaderObject();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the first Header in this group.
        /// Dim Header As Header = group.RetrieveHeaderObject()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the first Header in this group.
        /// var header:Header = group.RetrieveHeaderObject();
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public Header RetrieveHeaderObject()
        {
            return Parser.ParseHeader(RetrieveHeader());
        }

        private delegate Header DelegateRetrieveHeaderObject();
        private DelegateRetrieveHeaderObject _delegateRetrieveHeaderObject;

        public IAsyncResult BeginRetrieveHeaderObject(AsyncCallback callback)
        {
            _delegateRetrieveHeaderObject = RetrieveHeaderObject;
            return _delegateRetrieveHeaderObject.BeginInvoke(callback, _delegateRetrieveHeaderObject);
        }

        public Header EndRetrieveHeaderObject(IAsyncResult result)
        {
            return (Header)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Retrieves the article body at the specified ordinal position.
        /// </summary>
        /// <param name="index">The ordinal position of the article to be retrieved.</param>
        /// <returns>A byte array containing the article body.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the body of the article at position 29 in this group.
        /// byte[] body29 = group.RetrieveBody(29);
        /// //Retrieve last body in this group.
        /// byte[] body = group.RetrieveBody(group.LastBody);
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the body of the article at position 29 in this group.
        /// Dim body29() As Byte = group.RetrieveBody(29)
        /// 'Retrieve last body in this group.
        /// Dim body() As Byte = group.RetrieveBody(group.LastBody)
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the body of the article at position 29 in this group.
        /// var body29:byte[] = group.RetrieveBody(29);
        /// //Retrieve last body in this group.
        /// var body:byte[] = group.RetrieveBody(group.LastBody);
        /// 
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveBody(int index)
        {
            byte[] buffer = _nntp.CommandMultiline("body "+index.ToString());
            if(Encoding.ASCII.GetString(buffer,0,buffer.Length).StartsWith("222"))
                Pointer = index;
            return buffer;
        }

        private delegate byte[] DelegateRetrieveBodyInt(int index);
        private DelegateRetrieveBodyInt _delegateRetrieveBodyInt;

        public IAsyncResult BeginRetrieveBody(int index, AsyncCallback callback)
        {
            _delegateRetrieveBodyInt = RetrieveBody;
            return _delegateRetrieveBodyInt.BeginInvoke(index, callback, _delegateRetrieveBodyInt);
        }

        /// <summary>
        /// Retrieves the article body at the position specified by the current article pointer.
        /// </summary>
        /// <returns>A byte array containing the article body.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// NntpClient nntp = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// NewsGroup group = nntp.SelectGroup("mygroup");
        /// //Retrieve the first body in this group.
        /// byte[] body = group.RetrieveBody();
        /// 
        /// nntp.Disconnect();
        /// 
        /// VB.NET
        /// 
        /// Dim nntp As New NntpClient
        /// 
        /// nntp.Connect("news.myhost.com")
        /// 
        /// Dim group As NewsGroup = nntp.SelectGroup("mygroup")
        /// 'Retrieve the first body in this group.
        /// Dim body() As Byte = group.RetrieveBody()
        /// 
        /// nntp.Disconnect()
        /// 
        /// JScript.NET
        /// 
        /// var nntp:NntpClient = new NntpClient();
        /// 
        /// nntp.Connect("news.myhost.com");
        /// 
        /// var group:NewsGroup = nntp.SelectGroup("mygroup");
        /// //Retrieve the first body in this group.
        /// var body:byte[] = group.RetrieveBody();
        /// nntp.Disconnect();
        /// </code>
        /// </example>
        public byte[] RetrieveBody()
        {
            if(Pointer != 0 & Pointer != -1)
                return _nntp.CommandMultiline("body");
            else 
            {
                Pointer = FirstArticle;
                return _nntp.CommandMultiline("body "+ FirstArticle.ToString());;
            }
        }

        private delegate byte[] DelegateRetrieveBody();
        private DelegateRetrieveBody _delegateRetrieveBody;

        public IAsyncResult BeginRetrieveBody(AsyncCallback callback)
        {
            _delegateRetrieveBody = RetrieveBody;
            return _delegateRetrieveBody.BeginInvoke(callback, _delegateRetrieveBody);
        }

        public byte[] EndRetrieveBody(IAsyncResult result)
        {
            return (byte[])result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        #endregion

        #endregion

    }
}