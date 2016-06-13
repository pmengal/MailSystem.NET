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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using ActiveQLibrary.Serialization.ConfigGlobal;

namespace ActiveQConfiguration
{
	/// <summary>
	/// Summary description for ManageElement.
	/// </summary>
	public class ManageElementSmtpServer : System.Windows.Forms.UserControl
	{
		private ActiveQLibrary.CustomControl.ButtonXP _bAdd;
		private System.ComponentModel.IContainer components;

		private Color _firstColor = Color.White;
		private Color _lastColor = Color.White;
		private bool _usePen = false;
		private Color _penColor = Color.White;
		private int _sizePen;

		private string _text;
		private Font _font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.Point);
		private Color _textColor = Color.White;

		private static LinearGradientBrush _brushBackGround;
		private static SolidBrush _brushText;
		private ActiveQLibrary.CustomControl.ButtonXP _bUp;
		private ActiveQLibrary.CustomControl.ButtonXP _bDown;
		private static Pen _pen;
		private System.Windows.Forms.ToolTip _toolTipAdd;
		private ActiveQLibrary.CustomControl.ButtonXP _bRemove;
		private System.Windows.Forms.ListView _lvItem;
		private ActiveQLibrary.CustomControl.ButtonXP _bTest;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ToolTip _toolTipRemove;
		private System.Windows.Forms.ToolTip _toolTipTest;

		private ArrayList _listServers = new ArrayList();		

		public ManageElementSmtpServer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ManageElementSmtpServer));
			this._bRemove = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bAdd = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bUp = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bDown = new ActiveQLibrary.CustomControl.ButtonXP();
			this._toolTipAdd = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipRemove = new System.Windows.Forms.ToolTip(this.components);
			this._lvItem = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this._bTest = new ActiveQLibrary.CustomControl.ButtonXP();
			this._toolTipTest = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// _bRemove
			// 
			this._bRemove._Image = null;
			this._bRemove.DefaultScheme = false;
			this._bRemove.Image = null;
			this._bRemove.Location = new System.Drawing.Point(224, 124);
			this._bRemove.Name = "_bRemove";
			this._bRemove.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bRemove.Size = new System.Drawing.Size(72, 23);
			this._bRemove.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bRemove.TabIndex = 4;
			this._bRemove.Text = "Remove";
			this._bRemove.Click += new System.EventHandler(this._bRemove_Click);
			// 
			// _bAdd
			// 
			this._bAdd._Image = null;
			this._bAdd.DefaultScheme = false;
			this._bAdd.Image = null;
			this._bAdd.Location = new System.Drawing.Point(152, 124);
			this._bAdd.Name = "_bAdd";
			this._bAdd.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bAdd.Size = new System.Drawing.Size(72, 23);
			this._bAdd.SizeImgButton = new System.Drawing.Size(24, 24);
			this._bAdd.TabIndex = 3;
			this._bAdd.Text = "Add";
			this._bAdd.Click += new System.EventHandler(this._bAdd_Click);
			// 
			// _bUp
			// 
			this._bUp._Image = null;
			this._bUp.DefaultScheme = false;
			this._bUp.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bUp.Image")));
			this._bUp.Location = new System.Drawing.Point(298, 8);
			this._bUp.Name = "_bUp";
			this._bUp.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bUp.Size = new System.Drawing.Size(32, 32);
			this._bUp.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bUp.TabIndex = 1;
			this._bUp.Click += new System.EventHandler(this._bUp_Click);
			// 
			// _bDown
			// 
			this._bDown._Image = null;
			this._bDown.DefaultScheme = false;
			this._bDown.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bDown.Image")));
			this._bDown.Location = new System.Drawing.Point(298, 88);
			this._bDown.Name = "_bDown";
			this._bDown.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bDown.Size = new System.Drawing.Size(32, 32);
			this._bDown.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bDown.TabIndex = 2;
			this._bDown.Click += new System.EventHandler(this._bDown_Click);
			// 
			// _lvItem
			// 
			this._lvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader1,
																					  this.columnHeader2});
			this._lvItem.FullRowSelect = true;
			this._lvItem.GridLines = true;
			this._lvItem.HideSelection = false;
			this._lvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._lvItem.Location = new System.Drawing.Point(8, 8);
			this._lvItem.MultiSelect = false;
			this._lvItem.Name = "_lvItem";
			this._lvItem.Size = new System.Drawing.Size(288, 112);
			this._lvItem.TabIndex = 5;
			this._lvItem.View = System.Windows.Forms.View.Details;
			this._lvItem.DoubleClick += new System.EventHandler(this._lvItem_DoubleClick);
			this._lvItem.SelectedIndexChanged += new System.EventHandler(this._lvItem_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Host";
			this.columnHeader1.Width = 234;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Port";
			this.columnHeader2.Width = 50;
			// 
			// _bTest
			// 
			this._bTest._Image = null;
			this._bTest.DefaultScheme = false;
			this._bTest.Image = null;
			this._bTest.Location = new System.Drawing.Point(8, 124);
			this._bTest.Name = "_bTest";
			this._bTest.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bTest.Size = new System.Drawing.Size(72, 23);
			this._bTest.SizeImgButton = new System.Drawing.Size(24, 24);
			this._bTest.TabIndex = 6;
			this._bTest.Text = "Test";
			this._bTest.Click += new System.EventHandler(this._bTest_Click);
			// 
			// ManageElementSmtpServer
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._bTest,
																		  this._lvItem,
																		  this._bDown,
																		  this._bUp,
																		  this._bRemove,
																		  this._bAdd});
			this.Name = "ManageElementSmtpServer";
			this.Size = new System.Drawing.Size(366, 152);
			this.Load += new System.EventHandler(this.ManageElement_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ManageElement_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		public void ClearList()
		{
			this._lvItem.Items.Clear();
			this._listServers.Clear();
			this._bRemove.Enabled = false;
			this._bTest.Enabled = false;
			EnableDisableArrow();
		}

		private void ManageElement_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (_pen != null) _pen.Dispose();
			if (_brushBackGround != null) _brushBackGround.Dispose();
			if (_brushText != null) _brushText.Dispose();

			_brushBackGround =  new LinearGradientBrush(this.ClientRectangle, _firstColor, _lastColor, 90.0f); 			

			float[] relativeIntensities = {0.0f, 0.3f, 1.0f};
			float[] relativePositions   = {0.0f, 0.7f, 1.0f};

			Blend blend = new Blend();
			blend.Factors = relativeIntensities;
			blend.Positions = relativePositions;
			_brushBackGround.Blend = blend;	
			
			e.Graphics.FillRectangle(_brushBackGround,this.ClientRectangle);
			
			if (_usePen == true)
			{
				_pen = new Pen(_penColor,_sizePen);

				PointF[] points = {	new Point(this.ClientRectangle.X, this.ClientRectangle.Y),
									  new Point(this.ClientRectangle.Width, this.ClientRectangle.Y),
									  new Point(this.ClientRectangle.Width, this.ClientRectangle.Height),
									  new Point(this.ClientRectangle.X, this.ClientRectangle.Height),
									  new Point(this.ClientRectangle.X, this.ClientRectangle.Y)};

				e.Graphics.DrawLines(_pen, points);
			}

			if (_text != null)
				if (_text.Trim() != "")
				{
					_brushText = new SolidBrush(_textColor);
					
					StringFormat stringFormat = new StringFormat();
					stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
					e.Graphics.DrawString(_text,_font, _brushText,336,6,stringFormat);
				}
		}

		private void _bUp_Click(object sender, System.EventArgs e)
		{
			int indexSel = _lvItem.SelectedItems[0].Index;
			ListViewItem item = (ListViewItem)_lvItem.SelectedItems[0].Clone();

			_lvItem.Items.Insert(indexSel -1, item);
			_lvItem.Items.RemoveAt(indexSel + 1);

			_lvItem.Focus();

			item.Selected = true;

		}

		private void _bDown_Click(object sender, System.EventArgs e)
		{
			int indexSel = _lvItem.SelectedItems[0].Index;
			ListViewItem item = (ListViewItem)_lvItem.SelectedItems[0].Clone();

			_lvItem.Items.Insert(indexSel + 2, item);
			_lvItem.Items.RemoveAt(indexSel);

			_lvItem.Focus();

			item.Selected = true;

		}

		private void _lbItem_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedItem();
			}
		}

		private void _lbItem_SelectedValueChanged(object sender, System.EventArgs e)
		{
			_bRemove.Enabled = true;
			EnableDisableArrow();
		}

		private void ManageElement_Load(object sender, System.EventArgs e)
		{
			_bRemove.Enabled = false;
			_bTest.Enabled = false;
			
			EnableDisableArrow();
		}

		private void DeleteSelectedItem()
		{	
			int indexControl = -1;
			int indexList = -1;

			_lvItem.BeginUpdate();
				
			ListViewItem lv = _lvItem.SelectedItems[0];
			indexControl = lv.Index;
			
			indexList = IndexElement(lv.Text,Int32.Parse(lv.SubItems[1].Text));
			_listServers.RemoveAt(indexList);

			lv.Remove();

			if (indexControl > 0)
				_lvItem.Items[indexControl-1].Selected = true;
			else if (_lvItem.Items.Count > 0)	
				_lvItem.Items[0].Selected = true;
			
			_lvItem.Focus();

			if (_lvItem.Items.Count == 0)
			{
				_bRemove.Enabled = false;
				_bTest.Enabled = false;
			}

			_lvItem.EndUpdate();

			EnableDisableArrow();
		}

		private void EnableDisableArrow()
		{
			if (_lvItem.Items.Count == 0)
			{
				_bUp.Enabled = false;
				_bDown.Enabled = false;
			}

			else
			{
				if (_lvItem.SelectedItems.Count > 0)
				{
					if (_lvItem.SelectedItems[0].Index == 0)
					{
						_bUp.Enabled = false;

						if (_lvItem.Items.Count == 1)
							_bDown.Enabled = false;
						else
							_bDown.Enabled = true;
					}

					else
					{
						if (_lvItem.SelectedItems[0].Index == _lvItem.Items.Count - 1)
						{
							_bUp.Enabled = true;
							_bDown.Enabled = false;
						}

						else
						{
							_bUp.Enabled = true;
							_bDown.Enabled = true;
						}
					}
				}
			}

		}

		private void _bAdd_Click(object sender, System.EventArgs e)
		{
			SmtpServerForm smtpServerForm = new SmtpServerForm();
			DialogResult res = smtpServerForm.ShowDialog();

			if (res == DialogResult.OK)
			{
				AddElement(smtpServerForm);
			}
		}

		private void AddElement(SmtpServerForm smtpServerForm)
		{
			if (ExistItem(smtpServerForm.Host,smtpServerForm.Port) == true)
			{
				MessageBox.Show(string.Format("Unable to add '{0}:{1}' because already exist",smtpServerForm.Host,smtpServerForm.Port),"ActiveQConfiguration",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			else
			{
				_listServers.Add(new SmtpServer(smtpServerForm.Host,smtpServerForm.Port,smtpServerForm.Username,smtpServerForm.Password));

				ListViewItem.ListViewSubItem subItem = null;
				ListViewItem lvi =_lvItem.Items.Add(smtpServerForm.Host);
				lvi.ImageIndex = 0;
				subItem = lvi.SubItems.Add(smtpServerForm.Port.ToString());
			}

			EnableDisableArrow();
		}
        
		private void _bRemove_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedItem();
		}

		private void _bTest_Click(object sender, System.EventArgs e)
		{
			if (_lvItem.SelectedItems.Count > 0)
			{
				int index = IndexElement(_lvItem.SelectedItems[0].Text,Int32.Parse(_lvItem.SelectedItems[0].SubItems[1].Text));

				if (index != -1)
				{
					Global.TestSmtpServer(((SmtpServer)_listServers[index]).Host,((SmtpServer)_listServers[index]).Port,((SmtpServer)_listServers[index]).Username,((SmtpServer)_listServers[index]).Password);
					this._lvItem.Focus();
				}
			}
		}

		private void _lvItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_bRemove.Enabled = true;
			_bTest.Enabled = true;
			EnableDisableArrow();
		}

		private bool ExistItem(string host, int port)
		{
			for (int i = 0 ; i < _listServers.Count ; i++)
			{
				if (((SmtpServer)_listServers[i]).Host.ToUpper() == host.ToUpper() &&
					((SmtpServer)_listServers[i]).Port == port)
					return true;
			}

			return false;
		}

		private bool Exception(string host)
		{
			return ExistItem(host,25);
		}

		public int IndexElement(string host, int port)
		{
			for (int i = 0 ; i < _listServers.Count ; i++)
			{
				if (((SmtpServer)_listServers[i]).Host.ToUpper() == host.ToUpper() &&
					((SmtpServer)_listServers[i]).Port == port)
					return i;
			}

			return -1;
		}

		private void _lvItem_DoubleClick(object sender, System.EventArgs e)
		{
			if (_lvItem.SelectedItems.Count > 0)
			{
				int index = IndexElement(_lvItem.SelectedItems[0].Text,Int32.Parse(_lvItem.SelectedItems[0].SubItems[1].Text));

				if (index != -1)
				{
					SmtpServer oldSmtpServer = (SmtpServer)_listServers[index];

					SmtpServerForm smtpServerForm = new SmtpServerForm(oldSmtpServer);
					DialogResult res = smtpServerForm.ShowDialog();

					if (res == DialogResult.OK)
					{
						bool exist = false;

						for(int i = 0 ; i < _listServers.Count ; i++)
						{
							if (i != index)
							{
								if (((SmtpServer)_listServers[i]).Host.ToUpper() == smtpServerForm.Host.ToUpper() &&
									((SmtpServer)_listServers[i]).Port.ToString() == smtpServerForm.Port.ToString())
								{
									exist = true;
									break;
								}
							}
						}

						if (exist == true)
						{
							MessageBox.Show(string.Format("Unable to modify '{0}:{1}' to '{2}:{3}' because already exist",oldSmtpServer.Host,oldSmtpServer.Port,smtpServerForm.Host,smtpServerForm.Port),"ActiveQConfiguration",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}

						else
						{
							((SmtpServer)_listServers[index]).Host = smtpServerForm.Host;
							((SmtpServer)_listServers[index]).Port = smtpServerForm.Port;
							((SmtpServer)_listServers[index]).Username = smtpServerForm.Username;
							((SmtpServer)_listServers[index]).Password = smtpServerForm.Password;

							_lvItem.SelectedItems[0].Text = smtpServerForm.Host;
							_lvItem.SelectedItems[0].SubItems[1].Text = smtpServerForm.Port.ToString();
						}
					}
				}
			}
		}

		private int IndexElement(string host)
		{
			return IndexElement(host,25);
		}

		public void AddListItem(ArrayList ArrayItem)
		{
			foreach(SmtpServer smtpServer in ArrayItem)
			{
				if (ExistItem(smtpServer.Host,smtpServer.Port) == false)
				{
					_listServers.Add(new SmtpServer(smtpServer.Host,smtpServer.Port,smtpServer.Username,smtpServer.Password));

					ListViewItem.ListViewSubItem subItem = null;
					ListViewItem lvi =_lvItem.Items.Add(smtpServer.Host);
					lvi.ImageIndex = 0;
					subItem = lvi.SubItems.Add(smtpServer.Port.ToString());
				}
			}
		}

/*		private void ManageElementSmtpServer_Leave(object sender, System.EventArgs e)
		{
			_bTest.Enabled = false;
			_bRemove.Enabled = false;
			_bUp.Enabled = false;
			_bDown.Enabled = false;
		}

		private void ManageElementSmtpServer_Enter(object sender, System.EventArgs e)
		{
			if (_lvItem.SelectedItems.Count > 0)
			{
				_bRemove.Enabled = true;
				_bTest.Enabled = true;
			}

			EnableDisableArrow();
		}
*/
		#region Properties

		[Description("First color of the gradient"), Category("Config")]
		public Color FirstColor
		{
			get
			{
				return _firstColor;
			}

			set
			{
				_firstColor = value;
				this.Invalidate();
			}
		}

		[Description("Last color of the gradient"), Category("Config")]
		public Color LastColor
		{
			get
			{
				return _lastColor;
			}

			set
			{
				_lastColor = value;
				this.Invalidate();
			}
		}

		[Description("Use of pen"), Category("Config")]
		public bool Pen
		{
			get
			{
				return _usePen;
			}

			set
			{
				_usePen = value;
				this.Invalidate();
			}
		}	

		[Description("Size of the pen"), Category("Config")]
		public int SizePen
		{
			get
			{
				return _sizePen;
			}

			set
			{
				_sizePen = value;
				this.Invalidate();
			}
		}

		[Description("Last color of the pen"), Category("Config")]
		public Color PenColor
		{
			get
			{
				return _penColor;
			}

			set
			{
				_penColor = value;
				this.Invalidate();
			}
		}
		
		[Description("Text"), Category("Config")]
		public string TextContents
		{
			get
			{
				return _text;
			}

			set
			{
				_text = value;
				this.Invalidate();
			}
		}

		[Description("Color of text"), Category("Config")]
		public Color TextColor
		{
			get
			{
				return _textColor;
			}

			set
			{
				_textColor = value;
				this.Invalidate();
			}
		}


		[Description("Font of text"), Category("Config")]
		public Font FontText
		{
			get
			{
				return _font;
			}

			set
			{
				_font = value;
				this.Invalidate();
			}
		}

		[Description("ListView contening items"), Category("Config")]
		public ListView ListViewItem
		{
			get
			{
				return _lvItem;
			}

			set
			{
				_lvItem = value;
				this.Invalidate();
			}
		}

		[Description("Tooltip for the add button"), Category("Config")]
		public string TextAddButtonToolTip
		{
			get
			{
				return _toolTipAdd.GetToolTip(_bAdd);
			}

			set
			{
				if (value.Trim() != "")
					this._toolTipAdd.SetToolTip(_bAdd,value);
				this.Invalidate();
			}
		}

		[Description("Tooltip for the remove button"), Category("Config")]
		public string TextRemoveButtonToolTip
		{
			get
			{
				return _toolTipRemove.GetToolTip(_bRemove);
			}

			set
			{
				if (value.Trim() != "")
					this._toolTipRemove.SetToolTip(_bRemove,value);
				this.Invalidate();
			}
		}

		[Description("Tooltip for the test button"), Category("Config")]
		public string TextTestToolTip
		{
			get
			{
				return _toolTipTest.GetToolTip(_bTest);
			}

			set
			{
				if (value.Trim() != "")
					this._toolTipTest.SetToolTip(_bTest,value);
				this.Invalidate();
			}
		}

		[Browsable(false)]
		public ArrayList ListItemSmtp
		{
			get
			{
				return _listServers;
			}
		}

		#endregion
	}
}
