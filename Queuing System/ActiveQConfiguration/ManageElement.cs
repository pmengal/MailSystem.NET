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

namespace ActiveQConfiguration
{
	/// <summary>
	/// Summary description for ManageElement.
	/// </summary>
	public class ManageElement : System.Windows.Forms.UserControl
	{
		public enum TypeAddition
		{
			file = 0,
			fileXml,
			dir,
			textBox
		}

		private ActiveQLibrary.CustomControl.ButtonXP _bRemove;
		private System.Windows.Forms.ListBox _lbItem;
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
		private ActiveQLibrary.CustomControl.FolderBrowser _openDirDia;
		private System.Windows.Forms.OpenFileDialog _openFileDiaXml;
		private System.Windows.Forms.OpenFileDialog _openFileDia;
		private System.Windows.Forms.ToolTip _toolTipAdd;
		private System.Windows.Forms.ToolTip _toolTipRemove;
		private System.Windows.Forms.TextBox _tbAddString;
		
		private TypeAddition _typeAdd = TypeAddition.file;

		public ManageElement()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ManageElement));
			this._bRemove = new ActiveQLibrary.CustomControl.ButtonXP();
			this._lbItem = new System.Windows.Forms.ListBox();
			this._bAdd = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bUp = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bDown = new ActiveQLibrary.CustomControl.ButtonXP();
			this._openFileDiaXml = new System.Windows.Forms.OpenFileDialog();
			this._openDirDia = new ActiveQLibrary.CustomControl.FolderBrowser();
			this._openFileDia = new System.Windows.Forms.OpenFileDialog();
			this._toolTipAdd = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipRemove = new System.Windows.Forms.ToolTip(this.components);
			this._tbAddString = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _bRemove
			// 
			this._bRemove._Image = null;
			this._bRemove.DefaultScheme = false;
			this._bRemove.Image = null;
			this._bRemove.Location = new System.Drawing.Point(264, 124);
			this._bRemove.Name = "_bRemove";
			this._bRemove.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bRemove.Size = new System.Drawing.Size(72, 23);
			this._bRemove.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bRemove.TabIndex = 4;
			this._bRemove.Text = "Remove";
			this._bRemove.Click += new System.EventHandler(this._bRemove_Click);
			// 
			// _lbItem
			// 
			this._lbItem.HorizontalScrollbar = true;
			this._lbItem.Location = new System.Drawing.Point(8, 8);
			this._lbItem.Name = "_lbItem";
			this._lbItem.Size = new System.Drawing.Size(328, 108);
			this._lbItem.TabIndex = 0;
			this._lbItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this._lbItem_KeyDown);
			this._lbItem.SelectedValueChanged += new System.EventHandler(this._lbItem_SelectedValueChanged);
			// 
			// _bAdd
			// 
			this._bAdd._Image = null;
			this._bAdd.DefaultScheme = false;
			this._bAdd.Image = null;
			this._bAdd.Location = new System.Drawing.Point(192, 124);
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
			this._bUp.Location = new System.Drawing.Point(336, 8);
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
			this._bDown.Location = new System.Drawing.Point(336, 88);
			this._bDown.Name = "_bDown";
			this._bDown.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bDown.Size = new System.Drawing.Size(32, 32);
			this._bDown.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bDown.TabIndex = 2;
			this._bDown.Click += new System.EventHandler(this._bDown_Click);
			// 
			// _openFileDiaXml
			// 
			this._openFileDiaXml.Filter = "XML files|*.xml";
			// 
			// _tbAddString
			// 
			this._tbAddString.Location = new System.Drawing.Point(8, 125);
			this._tbAddString.Name = "_tbAddString";
			this._tbAddString.Size = new System.Drawing.Size(176, 20);
			this._tbAddString.TabIndex = 5;
			this._tbAddString.Text = "";
			this._tbAddString.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tbAddString_KeyDown);
			this._tbAddString.TextChanged += new System.EventHandler(this._tbAddString_TextChanged);
			// 
			// ManageElement
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._tbAddString,
																		  this._bDown,
																		  this._bUp,
																		  this._bRemove,
																		  this._lbItem,
																		  this._bAdd});
			this.Name = "ManageElement";
			this.Size = new System.Drawing.Size(400, 152);
			this.Load += new System.EventHandler(this.ManageElement_Load);
			this.SizeChanged += new System.EventHandler(this.ManageElement_SizeChanged);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ManageElement_Paint);
			this.ResumeLayout(false);

		}
		#endregion

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
					e.Graphics.DrawString(_text,_font, _brushText,374,6,stringFormat);
				}
		}

		private void _bUp_Click(object sender, System.EventArgs e)
		{
			int indexSel = _lbItem.SelectedIndex;
			string itemSel = (string)_lbItem.SelectedItem;

			_lbItem.Items.Insert(indexSel - 1,itemSel);
			_lbItem.Items.RemoveAt(indexSel + 1);

			_lbItem.SelectedIndex = indexSel - 1;
		}

		private void _bDown_Click(object sender, System.EventArgs e)
		{
			int indexSel = _lbItem.SelectedIndex;
			string itemSel = (string)_lbItem.SelectedItem;

			_lbItem.Items.Insert(indexSel + 2,itemSel);
			_lbItem.Items.RemoveAt(indexSel);

			_lbItem.SelectedIndex = indexSel + 1;
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
			if (_typeAdd == TypeAddition.textBox)
			{
				_tbAddString.Visible = true;
				_bAdd.Enabled = false;
			}
			else 
				_tbAddString.Visible = false;
			EnableDisableArrow();
		}

		private void DeleteSelectedItem()
		{	
			int index = _lbItem.SelectedIndex;
			if (index != -1)
			{
				_lbItem.Items.RemoveAt(index);

				if (index > 0)
					_lbItem.SelectedIndex = index - 1;
				else if (_lbItem.Items.Count > 0)
					_lbItem.SelectedIndex = 0;
				
				if (_lbItem.Items.Count == 0)
					_bRemove.Enabled = false;

			}
			EnableDisableArrow();
		}

		private void EnableDisableArrow()
		{
			if (_lbItem.Items.Count == 0)
			{
				_bUp.Enabled = false;
				_bDown.Enabled = false;
			}

			else
			{
				int index = _lbItem.SelectedIndex;
				if (index != -1)
				{
					if (index == 0)
					{
						_bUp.Enabled = false;

						if (_lbItem.Items.Count == 1)
							_bDown.Enabled = false;
						else
							_bDown.Enabled = true;
					}

					else
					{
						if (index == _lbItem.Items.Count - 1)
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
			AddElement();
		}

		private void AddElement()
		{
			if (_typeAdd == TypeAddition.textBox)
			{
				if (_tbAddString.Text.Trim() != "")
					if (ExistItem(_tbAddString.Text) == false)
					{
						_lbItem.Items.Add(_tbAddString.Text);
						_tbAddString.Text = "";
						_bAdd.Enabled = false;
					}
					else
						MessageBox.Show(string.Format("Unable to add '{0}' because already exist",_tbAddString.Text),"ActiveQConfiguration",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			else
			{

				DialogResult res = DialogResult.OK;

				if (_typeAdd == TypeAddition.file)
					res = _openFileDia.ShowDialog();
				else if (_typeAdd == TypeAddition.fileXml)
					res = _openFileDiaXml.ShowDialog();
				else if (_typeAdd == TypeAddition.dir)
					res = _openDirDia.ShowDialog();

				if (res == DialogResult.OK)
				{
					string PickupName = "";

					if (_typeAdd == TypeAddition.file)
						PickupName = _openFileDia.FileName;
					else if (_typeAdd == TypeAddition.fileXml)
						PickupName = _openFileDiaXml.FileName;
					else if (_typeAdd == TypeAddition.dir)
						PickupName = _openDirDia.DirectoryPath;

					
					if (ExistItem(PickupName) == false)
					{
						int index = _lbItem.Items.Add(PickupName);
						_lbItem.SelectedIndex = index;

					
					}

					else
						MessageBox.Show(string.Format("Unable to add '{0}' because already exist",PickupName),"ActiveQConfiguration",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}

			EnableDisableArrow();
		}
        
		private bool ExistItem(string Name)
		{
			bool isExist = false;

			for (int i = 0 ; i < _lbItem.Items.Count ; i++)
			{
				if (_lbItem.Items[i].ToString() == Name)
				{
					isExist = true;
					break;
				}
			}

			return isExist;
		}

		private void _bRemove_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedItem();
		}

		public void AddListItem(ArrayList ArrayItem)
		{
			foreach(string Item in ArrayItem)
			{
				if (ExistItem(Item) == false)
				{
					this._lbItem.Items.Add(Item);
				}
			}
		}

		public void ClearList()
		{
            this._lbItem.Items.Clear();
			_bRemove.Enabled = false;
			EnableDisableArrow();
		}

		private void _tbAddString_TextChanged(object sender, System.EventArgs e)
		{
			if (_tbAddString.Text.Trim() == "")
				_bAdd.Enabled = false;
			else
				_bAdd.Enabled = true;
		}

		private void _tbAddString_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				AddElement();
			}
		}

		private void ManageElement_SizeChanged(object sender, System.EventArgs e)
		{
			_lbItem.Height = this.Height - _tbAddString.Height - 12;
			_bAdd.Top = _lbItem.Top + _lbItem.Height - 2; 
			_bRemove.Top = _lbItem.Top + _lbItem.Height - 2; 
			_bDown.Top = _lbItem.Top + _lbItem.Height - _bDown.Height - 5;
		}

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

		[Description("Kind of pickup"), Category("Config")]
		public TypeAddition TypeAdd
		{
			get
			{
				return _typeAdd;
			}

			set
			{
				_typeAdd = value;
				this.Invalidate();
			}
		}

		[Description("Listbox contening items"), Category("Config")]
		public ListBox ListBoxItem
		{
			get
			{
				return _lbItem;
			}

			set
			{
				_lbItem = value;
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

		#endregion
	}
}
