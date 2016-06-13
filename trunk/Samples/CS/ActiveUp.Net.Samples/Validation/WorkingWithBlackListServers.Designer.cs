namespace ActiveUp.Net.Samples.Validation
{
    partial class WorkingWithBlackListServers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("0spam.fusionzero.com");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("all.rbl.kropka.net");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("bl.borderworlds.dk");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("bl.csma.biz");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("bl.deadbeef.com");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("bl.spamcop.net");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("blackhole.securitysage.com");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("blackholes.five-ten-sg.com");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("block.blars.org");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("cbl.abuseat.org");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("combined.njabl.org");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("dialups.mail-abuse.org");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("dnsbl.ahbl.org");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("dnsbl.cyberlogic.net");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("dnsbl.isoc.bg");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("dnsbl.jammconsulting.com");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("dnsbl.kempt.net");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("dnsbl.njabl.org");
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("dul.ru");
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("flowgoaway.com");
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem("forbidden.icm.edu.pl");
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem("hil.habeas.com");
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem("ohps.dnsbl.net.au");
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem("omrs.dnsbl.net.au");
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem("opm.blitzed.org");
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem("osps.dnsbl.net.au");
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem("rdts.dnsbl.net.au");
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem("relays.bl.kundenserver.de");
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem("rmst.dnsbl.net.au");
            System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem("sbl.csma.biz");
            System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem("sbl-xbl.spamhaus.org");
            System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem("spamguard.leadmon.net");
            System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem("spamsources.dnsbl.info");
            System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem("unconfirmed.dsbl.org");
            this._tbPop3Server = new System.Windows.Forms.TextBox();
            this._lUserName = new System.Windows.Forms.Label();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this._lPop3Server = new System.Windows.Forms.Label();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._lPassword = new System.Windows.Forms.Label();
            this._bCheckMessage = new System.Windows.Forms.Button();
            this._lvBlackListServers = new System.Windows.Forms.ListView();
            this._tbBlackListServer = new System.Windows.Forms.TextBox();
            this._bAddBlackListServer = new System.Windows.Forms.Button();
            this._bRemoveBlackListServer = new System.Windows.Forms.Button();
            this._lBlackListServers = new System.Windows.Forms.Label();
            this._lDefinedBlackListServer = new System.Windows.Forms.Label();
            this._lvDefinedBlackList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._lvDefinedBlackList);
            this.splitContainer1.Panel1.Controls.Add(this._lDefinedBlackListServer);
            this.splitContainer1.Panel1.Controls.Add(this._lvBlackListServers);
            this.splitContainer1.Panel1.Controls.Add(this._tbBlackListServer);
            this.splitContainer1.Panel1.Controls.Add(this._bAddBlackListServer);
            this.splitContainer1.Panel1.Controls.Add(this._bRemoveBlackListServer);
            this.splitContainer1.Panel1.Controls.Add(this._lBlackListServers);
            this.splitContainer1.Panel1.Controls.Add(this._tbPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bCheckMessage);
            // 
            // _tbPop3Server
            // 
            this._tbPop3Server.Location = new System.Drawing.Point(15, 104);
            this._tbPop3Server.Name = "_tbPop3Server";
            this._tbPop3Server.Size = new System.Drawing.Size(270, 20);
            this._tbPop3Server.TabIndex = 30;
            // 
            // _lUserName
            // 
            this._lUserName.AutoSize = true;
            this._lUserName.Location = new System.Drawing.Point(12, 10);
            this._lUserName.Name = "_lUserName";
            this._lUserName.Size = new System.Drawing.Size(61, 13);
            this._lUserName.TabIndex = 25;
            this._lUserName.Text = "User name:";
            // 
            // _tbUserName
            // 
            this._tbUserName.Location = new System.Drawing.Point(15, 26);
            this._tbUserName.Name = "_tbUserName";
            this._tbUserName.Size = new System.Drawing.Size(270, 20);
            this._tbUserName.TabIndex = 26;
            // 
            // _lPop3Server
            // 
            this._lPop3Server.AutoSize = true;
            this._lPop3Server.Location = new System.Drawing.Point(12, 88);
            this._lPop3Server.Name = "_lPop3Server";
            this._lPop3Server.Size = new System.Drawing.Size(244, 13);
            this._lPop3Server.TabIndex = 29;
            this._lPop3Server.Text = "POP3 server address (will use 110 as default port):";
            // 
            // _tbPassword
            // 
            this._tbPassword.Location = new System.Drawing.Point(15, 65);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(270, 20);
            this._tbPassword.TabIndex = 28;
            // 
            // _lPassword
            // 
            this._lPassword.AutoSize = true;
            this._lPassword.Location = new System.Drawing.Point(12, 49);
            this._lPassword.Name = "_lPassword";
            this._lPassword.Size = new System.Drawing.Size(56, 13);
            this._lPassword.TabIndex = 27;
            this._lPassword.Text = "Password:";
            // 
            // _bCheckMessage
            // 
            this._bCheckMessage.Location = new System.Drawing.Point(15, 369);
            this._bCheckMessage.Name = "_bCheckMessage";
            this._bCheckMessage.Size = new System.Drawing.Size(270, 36);
            this._bCheckMessage.TabIndex = 31;
            this._bCheckMessage.Text = "Check message";
            this._bCheckMessage.UseVisualStyleBackColor = true;
            this._bCheckMessage.Click += new System.EventHandler(this._bCheckMessage_Click);
            // 
            // _lvBlackListServers
            // 
            this._lvBlackListServers.FullRowSelect = true;
            this._lvBlackListServers.HideSelection = false;
            this._lvBlackListServers.Location = new System.Drawing.Point(15, 251);
            this._lvBlackListServers.Name = "_lvBlackListServers";
            this._lvBlackListServers.Size = new System.Drawing.Size(269, 78);
            this._lvBlackListServers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvBlackListServers.TabIndex = 41;
            this._lvBlackListServers.UseCompatibleStateImageBehavior = false;
            this._lvBlackListServers.View = System.Windows.Forms.View.List;
            this._lvBlackListServers.SelectedIndexChanged += new System.EventHandler(this._lvBlackListServers_SelectedIndexChanged);
            // 
            // _tbBlackListServer
            // 
            this._tbBlackListServer.Location = new System.Drawing.Point(15, 335);
            this._tbBlackListServer.Name = "_tbBlackListServer";
            this._tbBlackListServer.Size = new System.Drawing.Size(140, 20);
            this._tbBlackListServer.TabIndex = 40;
            this._tbBlackListServer.TextChanged += new System.EventHandler(this._tbBlackListServer_TextChanged);
            // 
            // _bAddBlackListServer
            // 
            this._bAddBlackListServer.Enabled = false;
            this._bAddBlackListServer.Location = new System.Drawing.Point(158, 333);
            this._bAddBlackListServer.Name = "_bAddBlackListServer";
            this._bAddBlackListServer.Size = new System.Drawing.Size(59, 23);
            this._bAddBlackListServer.TabIndex = 39;
            this._bAddBlackListServer.Text = "Add";
            this._bAddBlackListServer.UseVisualStyleBackColor = true;
            this._bAddBlackListServer.Click += new System.EventHandler(this._bAddBlackListServer_Click);
            // 
            // _bRemoveBlackListServer
            // 
            this._bRemoveBlackListServer.Enabled = false;
            this._bRemoveBlackListServer.Location = new System.Drawing.Point(223, 333);
            this._bRemoveBlackListServer.Name = "_bRemoveBlackListServer";
            this._bRemoveBlackListServer.Size = new System.Drawing.Size(61, 23);
            this._bRemoveBlackListServer.TabIndex = 38;
            this._bRemoveBlackListServer.Text = "Remove";
            this._bRemoveBlackListServer.UseVisualStyleBackColor = true;
            this._bRemoveBlackListServer.Click += new System.EventHandler(this._bRemoveBlackListServer_Click);
            // 
            // _lBlackListServers
            // 
            this._lBlackListServers.AutoSize = true;
            this._lBlackListServers.Location = new System.Drawing.Point(12, 237);
            this._lBlackListServers.Name = "_lBlackListServers";
            this._lBlackListServers.Size = new System.Drawing.Size(123, 13);
            this._lBlackListServers.TabIndex = 37;
            this._lBlackListServers.Text = "Custom blacklist servers:";
            // 
            // _lDefinedBlackListServer
            // 
            this._lDefinedBlackListServer.AutoSize = true;
            this._lDefinedBlackListServer.Location = new System.Drawing.Point(12, 127);
            this._lDefinedBlackListServer.Name = "_lDefinedBlackListServer";
            this._lDefinedBlackListServer.Size = new System.Drawing.Size(125, 13);
            this._lDefinedBlackListServer.TabIndex = 42;
            this._lDefinedBlackListServer.Text = "Defined blacklist servers:";
            // 
            // _lvDefinedBlackList
            // 
            this._lvDefinedBlackList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this._lvDefinedBlackList.FullRowSelect = true;
            this._lvDefinedBlackList.HideSelection = false;
            this._lvDefinedBlackList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24,
            listViewItem25,
            listViewItem26,
            listViewItem27,
            listViewItem28,
            listViewItem29,
            listViewItem30,
            listViewItem31,
            listViewItem32,
            listViewItem33,
            listViewItem34});
            this._lvDefinedBlackList.Location = new System.Drawing.Point(15, 143);
            this._lvDefinedBlackList.Name = "_lvDefinedBlackList";
            this._lvDefinedBlackList.Size = new System.Drawing.Size(269, 93);
            this._lvDefinedBlackList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvDefinedBlackList.TabIndex = 44;
            this._lvDefinedBlackList.UseCompatibleStateImageBehavior = false;
            this._lvDefinedBlackList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Server name";
            this.columnHeader1.Width = 245;
            // 
            // WorkingWithBlackListServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "WorkingWithBlackListServers";
            this.Text = "Working with black list servers";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _tbPop3Server;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.Label _lPop3Server;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.Button _bCheckMessage;
        private System.Windows.Forms.ListView _lvBlackListServers;
        private System.Windows.Forms.TextBox _tbBlackListServer;
        private System.Windows.Forms.Button _bAddBlackListServer;
        private System.Windows.Forms.Button _bRemoveBlackListServer;
        private System.Windows.Forms.Label _lBlackListServers;
        private System.Windows.Forms.Label _lDefinedBlackListServer;
        private System.Windows.Forms.ListView _lvDefinedBlackList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
