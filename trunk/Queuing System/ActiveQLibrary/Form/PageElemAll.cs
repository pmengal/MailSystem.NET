using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using ActiveQLibrary.CustomControl;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Summary description for PageElemAll.
	/// </summary>
	public class PageElemAll : PageBase
	{
		#region delegate

		delegate void AddItemDelegate(string File, string Elem);

		#endregion

		private ActiveQLibrary.CustomControl.ListBoxVS lbStandardQueue;
		private ActiveQLibrary.CustomControl.TreeViewVS tvScheduledQueue;
		private System.Windows.Forms.ImageList _imageList;
		private ActiveQLibrary.CustomControl.LabelGradient labelGradient2;
		private ActiveQLibrary.CustomControl.LabelGradient labelGradient3;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components;

		public PageElemAll(TypePage Type) : base(Type)
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

		public void AddItemScheduledQueue(string File, string Name)
		{
			AddItemDelegate addItemDelegate = new AddItemDelegate(AddItemScheduledQueueFct);
			tvScheduledQueue.Invoke(addItemDelegate,new object[] {File,Name});
		}

		public void AddItemScheduledQueueFct(string File, string Name)
		{
			if (File != null)
			{
				int index = -1;
				for (int i = 0 ; i < tvScheduledQueue.Nodes.Count ; i++)
				{
					if (tvScheduledQueue.Nodes[i].Text == File)
					{
						index = i;
						break;
					}
				}

				if (index >= 0)
				{
					tvScheduledQueue.Nodes[index].Nodes.Add(new TreeNode(Name,1,2));
				}

				else
				{
					tvScheduledQueue.Nodes.Add(new TreeNode(File,0,0));
					tvScheduledQueue.Nodes[tvScheduledQueue.Nodes.Count-1].Nodes.Add(new TreeNode(Name,1,2));
				}
			}

			else
			{
				tvScheduledQueue.Nodes.Add(new TreeNode(Name,3,3));
			}
		}

		public void RemoveItemScheduledQueue(string Elem)
		{
			for (int i = 0 ; i < tvScheduledQueue.Nodes.Count ; i++)
			{
				if (tvScheduledQueue.Nodes[i].Text == Elem)
				{
					tvScheduledQueue.Nodes[i].Remove();
					break;
				}
			}
		}

		public void RemoveItemScheduledTask(string Elem)
		{
			int index = Elem.LastIndexOf("?");
			string file = Elem.Substring(0,index);
			string id = Elem.Substring(index+1);

			for (int i = 0 ; i < tvScheduledQueue.Nodes.Count ; i++)
			{
				if (tvScheduledQueue.Nodes[i].Text == file)
				{
					for (int j = 0 ; j < tvScheduledQueue.Nodes[i].Nodes.Count ; j++)
					{
						if (tvScheduledQueue.Nodes[i].Nodes[j].Text == string.Format("Task(id:{0})",id))
						{
							tvScheduledQueue.Nodes[i].Nodes[j].Remove();
							if (tvScheduledQueue.Nodes[i].Nodes.Count == 0)
								tvScheduledQueue.Nodes[i].Remove();
							break;
						}
					}
					break;
				}
			}
		}

		public void DisableStdMail()
		{
			this.labelGradient2.Enabled = false;
			this.lbStandardQueue.Enabled = false;
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageElemAll));
			this.lbStandardQueue = new ActiveQLibrary.CustomControl.ListBoxVS();
			this.tvScheduledQueue = new ActiveQLibrary.CustomControl.TreeViewVS();
			this._imageList = new System.Windows.Forms.ImageList(this.components);
			this.labelGradient2 = new ActiveQLibrary.CustomControl.LabelGradient();
			this.labelGradient3 = new ActiveQLibrary.CustomControl.LabelGradient();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbStandardQueue
			// 
			this.lbStandardQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lbStandardQueue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbStandardQueue.Img = ((System.Drawing.Bitmap)(resources.GetObject("lbStandardQueue.Img")));
			this.lbStandardQueue.ItemHeight = 14;
			this.lbStandardQueue.Location = new System.Drawing.Point(5, 39);
			this.lbStandardQueue.Name = "lbStandardQueue";
			this.lbStandardQueue.Size = new System.Drawing.Size(222, 322);
			this.lbStandardQueue.TabIndex = 6;
			// 
			// tvScheduledQueue
			// 
			this.tvScheduledQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvScheduledQueue.ImageList = this._imageList;
			this.tvScheduledQueue.Location = new System.Drawing.Point(235, 39);
			this.tvScheduledQueue.Name = "tvScheduledQueue";
			this.tvScheduledQueue.Size = new System.Drawing.Size(220, 322);
			this.tvScheduledQueue.TabIndex = 7;
			// 
			// _imageList
			// 
			this._imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this._imageList.ImageSize = new System.Drawing.Size(16, 16);
			this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
			this._imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// labelGradient2
			// 
			this.labelGradient2.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			this.labelGradient2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelGradient2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelGradient2.GradientColorTwo = System.Drawing.Color.LightSteelBlue;
			this.labelGradient2.Location = new System.Drawing.Point(5, 7);
			this.labelGradient2.Name = "labelGradient2";
			this.labelGradient2.Size = new System.Drawing.Size(222, 30);
			this.labelGradient2.TabIndex = 9;
			this.labelGradient2.Text = "Standard";
			this.labelGradient2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelGradient3
			// 
			this.labelGradient3.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			this.labelGradient3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelGradient3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelGradient3.GradientColorTwo = System.Drawing.Color.LightSteelBlue;
			this.labelGradient3.Location = new System.Drawing.Point(234, 7);
			this.labelGradient3.Name = "labelGradient3";
			this.labelGradient3.Size = new System.Drawing.Size(222, 30);
			this.labelGradient3.TabIndex = 10;
			this.labelGradient3.Text = "Scheduled";
			this.labelGradient3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.labelGradient2,
																				 this.tvScheduledQueue,
																				 this.lbStandardQueue,
																				 this.labelGradient3});
			this.panel1.Location = new System.Drawing.Point(6, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(462, 365);
			this.panel1.TabIndex = 11;
			// 
			// PageElemAll
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageElemAll";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties

		public ListBox StandardQueue
		{
			get
			{
				return lbStandardQueue;
			}
		}

		public TreeViewVS ScheduledQueue
		{
			get
			{
				return tvScheduledQueue;
			}
		}

		
		#endregion
	}
}
