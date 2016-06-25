using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Summary description for PageScheduledTask.
	/// </summary>
	/// 
	public class PageScheduledTask : PageBase
	{
		#region delegate

		delegate void AddItemDelegate(string File, string Elem);

		#endregion

		private ActiveQLibrary.CustomControl.TreeViewVS _treeTask;
		private System.ComponentModel.IContainer components;
		private ActiveQLibrary.CustomControl.ButtonXP _bDelete;
		private ActiveQLibrary.CustomControl.PanelGradient panel7;
		private System.Windows.Forms.Label label4;
		private ActiveQLibrary.CustomControl.PanelGradient panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private ActiveQLibrary.CustomControl.PanelGradient panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TextBox _tbAddress;
		private System.Windows.Forms.TextBox _tbMethod;
		private System.Windows.Forms.TextBox _tbDateStart;
		private System.Windows.Forms.TextBox _tbDateEnd;
		private ActiveQLibrary.CustomControl.PanelGradient panel6;
		private System.Windows.Forms.TextBox _tbNextExecution;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ImageList _imageList;

		public PageScheduledTask(TypePage Type) : base(Type)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			this.Location = new Point(0,51);

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageScheduledTask));
			this._treeTask = new ActiveQLibrary.CustomControl.TreeViewVS();
			this._imageList = new System.Windows.Forms.ImageList(this.components);
			this._bDelete = new ActiveQLibrary.CustomControl.ButtonXP();
			this.panel7 = new ActiveQLibrary.CustomControl.PanelGradient();
			this._tbDateStart = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this._tbDateEnd = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new ActiveQLibrary.CustomControl.PanelGradient();
			this._tbMethod = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel3 = new ActiveQLibrary.CustomControl.PanelGradient();
			this._tbAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel6 = new ActiveQLibrary.CustomControl.PanelGradient();
			this._tbNextExecution = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panel7.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// _treeTask
			// 
			this._treeTask.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._treeTask.ImageList = this._imageList;
			this._treeTask.Location = new System.Drawing.Point(8, 8);
			this._treeTask.Name = "_treeTask";
			this._treeTask.Size = new System.Drawing.Size(208, 345);
			this._treeTask.TabIndex = 1;
			this._treeTask.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeTask_AfterSelect);
			// 
			// _imageList
			// 
			this._imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this._imageList.ImageSize = new System.Drawing.Size(16, 16);
			this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
			this._imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _bDelete
			// 
			this._bDelete._Image = null;
			this._bDelete.DefaultScheme = false;
			this._bDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bDelete.Image")));
			this._bDelete.Location = new System.Drawing.Point(374, 275);
			this._bDelete.Name = "_bDelete";
			this._bDelete.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bDelete.Size = new System.Drawing.Size(75, 23);
			this._bDelete.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bDelete.TabIndex = 58;
			this._bDelete.Text = "Delete";
			this._bDelete.Click += new System.EventHandler(this._bDelete_Click);
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.SystemColors.Control;
			this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel7.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbDateStart,
																				 this.label4,
																				 this._tbDateEnd,
																				 this.label2});
			this.panel7.GradientColorTwo = System.Drawing.Color.LightSteelBlue;
			this.panel7.Location = new System.Drawing.Point(224, 120);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(224, 92);
			this.panel7.TabIndex = 59;
			// 
			// _tbDateStart
			// 
			this._tbDateStart.BackColor = System.Drawing.Color.White;
			this._tbDateStart.Location = new System.Drawing.Point(8, 24);
			this._tbDateStart.Name = "_tbDateStart";
			this._tbDateStart.ReadOnly = true;
			this._tbDateStart.Size = new System.Drawing.Size(208, 20);
			this._tbDateStart.TabIndex = 1;
			this._tbDateStart.Text = "";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Date start :";
			// 
			// _tbDateEnd
			// 
			this._tbDateEnd.BackColor = System.Drawing.Color.White;
			this._tbDateEnd.Location = new System.Drawing.Point(8, 66);
			this._tbDateEnd.Name = "_tbDateEnd";
			this._tbDateEnd.ReadOnly = true;
			this._tbDateEnd.Size = new System.Drawing.Size(208, 20);
			this._tbDateEnd.TabIndex = 1;
			this._tbDateEnd.Text = "";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(9, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Date end : ";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbMethod,
																				 this.label1});
			this.panel1.GradientColorTwo = System.Drawing.Color.LightSteelBlue;
			this.panel1.Location = new System.Drawing.Point(224, 64);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(224, 48);
			this.panel1.TabIndex = 7;
			// 
			// _tbMethod
			// 
			this._tbMethod.BackColor = System.Drawing.Color.White;
			this._tbMethod.Location = new System.Drawing.Point(8, 24);
			this._tbMethod.Name = "_tbMethod";
			this._tbMethod.ReadOnly = true;
			this._tbMethod.Size = new System.Drawing.Size(208, 20);
			this._tbMethod.TabIndex = 1;
			this._tbMethod.Text = "";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Method : ";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.Control;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbAddress,
																				 this.label3});
			this.panel3.GradientColorTwo = System.Drawing.Color.LightSteelBlue;
			this.panel3.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.panel3.Location = new System.Drawing.Point(224, 8);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(224, 48);
			this.panel3.TabIndex = 61;
			// 
			// _tbAddress
			// 
			this._tbAddress.BackColor = System.Drawing.Color.White;
			this._tbAddress.Location = new System.Drawing.Point(8, 24);
			this._tbAddress.Name = "_tbAddress";
			this._tbAddress.ReadOnly = true;
			this._tbAddress.Size = new System.Drawing.Size(208, 20);
			this._tbAddress.TabIndex = 1;
			this._tbAddress.Text = "";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
			this.label3.Location = new System.Drawing.Point(8, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Address :";
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.panel6,
																				 this._treeTask,
																				 this.panel7,
																				 this.panel1,
																				 this.panel3,
																				 this._bDelete});
			this.panel4.Location = new System.Drawing.Point(6, 8);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(462, 365);
			this.panel4.TabIndex = 62;
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.SystemColors.Control;
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbNextExecution,
																				 this.label5});
			this.panel6.GradientColorTwo = System.Drawing.Color.LightSteelBlue;
			this.panel6.Location = new System.Drawing.Point(224, 220);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(224, 48);
			this.panel6.TabIndex = 63;
			// 
			// _tbNextExecution
			// 
			this._tbNextExecution.BackColor = System.Drawing.Color.White;
			this._tbNextExecution.Location = new System.Drawing.Point(8, 24);
			this._tbNextExecution.Name = "_tbNextExecution";
			this._tbNextExecution.ReadOnly = true;
			this._tbNextExecution.Size = new System.Drawing.Size(208, 20);
			this._tbNextExecution.TabIndex = 1;
			this._tbNextExecution.Text = "";
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Location = new System.Drawing.Point(8, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Next execution : ";
			// 
			// PageScheduledTask
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel4});
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageScheduledTask";
			this.Load += new System.EventHandler(this.PageScheduledTask_Load);
			this.panel7.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void AddItem(string File, string Name)
		{
			AddItemDelegate addItemDelegate = new AddItemDelegate(AddItemFct);
			_treeTask.Invoke(addItemDelegate,new object[] {File,Name});
		}

		public void AddItemFct(string File, string Name)
		{
			int index = -1;
			for (int i = 0 ; i < _treeTask.Nodes.Count ; i++)
			{
				if (_treeTask.Nodes[i].Text == File)
				{
					index = i;
					break;
				}
			}

			if (index >= 0)
			{
				_treeTask.Nodes[index].Nodes.Add(new TreeNode(Name,1,2));
			}

			else
			{
				_treeTask.Nodes.Add(new TreeNode(File,0,0));
				_treeTask.Nodes[_treeTask.Nodes.Count-1].Nodes.Add(new TreeNode(Name,1,2));
			}
		}

		private void _treeTask_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (_treeTask.SelectedNode.Parent ==null)
				ClearInformation();

			if (_treeTask.SelectedNode.Parent != null)
				ShowInformation();
		}

		private void ShowInformation()
		{
			if (Parent != null)
			{
				int indexStartElem = _treeTask.SelectedNode.Text.IndexOf("(id:");
				int indexEndElem = _treeTask.SelectedNode.Text.IndexOf(")",indexStartElem);
				string id = "";
				if (indexStartElem >=0 && indexEndElem > indexStartElem)
				{
					for(int i = indexStartElem + 4; i < indexEndElem ; i++)
						id += _treeTask.SelectedNode.Text[i];
				}

				if (id.Trim() != "")
				{
					SpooledObject obj = ActiveQLibrary.QueueScheduled.GiveSpooledObject(string.Format("{0}?{1}",_treeTask.SelectedNode.Parent.Text,id));

					if (obj != null)
					{
						_bDelete.Enabled = true;

						ActiveQLibrary.Serialization.ConfigTask.Task task = (ActiveQLibrary.Serialization.ConfigTask.Task)obj.Object;
						this._tbAddress.Text = task.Address;
						this._tbMethod.Text = task.Method;
						this._tbDateStart.Text = task.DateStart.ToString();
						this._tbDateEnd.Text = task.DateEnd.ToString();
						this._tbNextExecution.Text = obj.SendingDate.ToString();
					}

				}
			}
			
		}

		private void ClearInformation()
		{
			_bDelete.Enabled = false;

			this._tbAddress.Text = "";
			this._tbMethod.Text = "";
			this._tbDateStart.Text = "";
			this._tbDateEnd.Text = "";
			this._tbNextExecution.Text = "";
	
		}

		private void _bDelete_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedTask();
		}

		private void DeleteSelectedTask()
		{
			if (Parent != null)
			{
				int indexStartElem = _treeTask.SelectedNode.Text.IndexOf("(id:");
				int indexEndElem = _treeTask.SelectedNode.Text.IndexOf(")",indexStartElem);
				string id = "";
				if (indexStartElem >=0 && indexEndElem > indexStartElem)
				{
					for(int i = indexStartElem + 4; i < indexEndElem ; i++)
						id += _treeTask.SelectedNode.Text[i];
				
				}
				
				if (id.Trim() != "")
				{
					string ItemToDelete = string.Format("{0}?{1}",_treeTask.SelectedNode.Parent.Text,id);
					Global.Log.WriteEvent(LogType.debug,string.Format("[FORM:SCHTASK] Deleting scheduled task : '{0}'",ItemToDelete));

					if (Global.DeleteScheduledTask(_treeTask.SelectedNode.Parent.Text,id) == true)
					{

						_treeTask.BeginUpdate();
						
						if (_treeTask.SelectedNode.Parent.Nodes.Count == 1)
							_treeTask.SelectedNode.Parent.Remove();
						else
							_treeTask.SelectedNode.Remove();

						_treeTask.EndUpdate();
				
					}

					else
					{
						MessageBox.Show("Unable to delete this task because it's in use","Error deleting scheduled task",MessageBoxButtons.OK,MessageBoxIcon.Error);
						Global.Log.WriteError(string.Format("[FORM:SCHTASK] Unable to delete scheduled task : '{0}', it's in use",ItemToDelete));
					}
				}

			}
		}

		private void PageScheduledTask_Load(object sender, System.EventArgs e)
		{
			_bDelete.Enabled = false;
		}

		public void RemoveItemScheduledTask(string Elem)
		{
			int index = Elem.LastIndexOf("?");
			string file = Elem.Substring(0,index);
			string id = Elem.Substring(index+1);

			for (int i = 0 ; i < _treeTask.Nodes.Count ; i++)
			{
				if (_treeTask.Nodes[i].Text == file)
				{
					for (int j = 0 ; j < _treeTask.Nodes[i].Nodes.Count ; j++)
					{
						if (_treeTask.Nodes[i].Nodes[j].Text == string.Format("Task(id:{0})",id))
						{
							_treeTask.Nodes[i].Nodes[j].Remove();
							if (_treeTask.Nodes[i].Nodes.Count == 0)
								_treeTask.Nodes[i].Remove();
							break;
						}
					}
					break;
				}
			}

		}

		#region Properties

		public TreeView TaskQueue
		{
			get
			{
				return _treeTask;
			}
		}

		#endregion
	}
}
