using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This class represents the message area.
    /// </summary>
	public partial class MessageArea : UserControl
	{

        /// <summary>
        /// Constructor.
        /// </summary>
		public MessageArea()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Property for access message list.
        /// </summary>
        public MessageList MessageList
        {
            get { return messageList1; }
        }

        /// <summary>
        /// Method for load messages.
        /// </summary>
        /// <param name="messageListType">The Message List Type.</param>
        public void LoadMessages(MessageListType messageListType)
        {
            this.toolStripLabel1.Text = MainForm.GetInstance().GetSelectedFolder();
            this.messageList1.LoadMessages(messageListType);
        }

		#region Event Handlers
		private void MessageArea_Load(object sender, EventArgs e)
		{
			if (null != this.Parent)
			{
				this.Parent.Padding = new Padding(0, 3, 0, 3);
			}

			// Set dock
			this.Dock = DockStyle.Fill;
		}
		#endregion
	}
}
