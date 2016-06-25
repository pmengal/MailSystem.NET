using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using ActiveQLibrary.CustomControl;

namespace ActiveQLibrary.Form
{


	/// <summary>
	/// Summary description for PageForm.
	/// </summary>
	public class PageForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// List of pages
		/// </summary>
		private ArrayList _pageList = new ArrayList();

		/// <summary>
		/// Index of the current page
		/// </summary>
		private int _indexCurrentPage;

		/// <summary>
		/// Page contening all the queues.
		/// </summary>
		private PageElemAll _pageElemAll = new PageElemAll(TypePage.ElemAll);

		/// <summary>
		/// Page contening all the info about standard mail.
		/// </summary>
		private PageStandardMail _pageStdMail = new PageStandardMail(TypePage.DetailsStdMail);

		private PageScheduledMail _pageSchMail = new PageScheduledMail(TypePage.DetailsSchMail);

		private PageScheduledTask _pageSchTask = new PageScheduledTask(TypePage.DetailsSchTask);

		private PageButtons _pageButtons = new PageButtons();

		private PageMainLog _pageMainLog = new PageMainLog(TypePage.MainLog);

		private PageProgress _pageProgress = new PageProgress(TypePage.Progress);

		public PageForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			Controls.AddRange(new Control[] {
												_pageButtons,
												_pageMainLog,
												_pageElemAll,
												_pageStdMail,
												_pageSchMail,
												_pageSchTask,
												_pageProgress
											} );

			_indexCurrentPage = -1;
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

		protected override void OnControlAdded( ControlEventArgs e )
		{
			base.OnControlAdded(e);

			PageBase page = e.Control as PageBase;
			if (page != null)
			{
				page.Visible = false;
				_pageList.Add(page);
								
			}
		}

		public int GetIndex(TypePage Type)
		{
			int index = -1;

			for (int i = 0 ; i < _pageList.Count ; i++)
			{
				if (((PageBase)_pageList[i]).Type == Type)
				{
					index = i;
					break;
				}
			}

			return index;
		}

		public void HidePage(int Index)
		{
			if (Index > _pageList.Count)
				throw new ArgumentOutOfRangeException();

			((PageBase)_pageList[Index]).Visible = false;
		}

		public void HidePage(TypePage Type)
		{
			int index = GetIndex(Type);

			if (index >= 0)
			{
				((PageBase)_pageList[index]).Visible = false;
			}
		}

		public void ShowPage(int Index)
		{
			if (Index > _pageList.Count)
				throw new ArgumentOutOfRangeException();

			((PageBase)_pageList[Index]).Visible = true;
			_indexCurrentPage = Index;
		}

		public void ShowPage(TypePage Type)
		{
			int index = GetIndex(Type);

			if (index != _indexCurrentPage)
			{
				if (_indexCurrentPage >= 0)
					HidePage(_indexCurrentPage);
				_indexCurrentPage = index;
				ShowPage(index);
			}
		}



		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageForm));
			// 
			// PageForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 430);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "PageForm";

		}
		#endregion

		#region Properties

		public int IndexCurrentPage
		{
			get
			{
				return _indexCurrentPage;
			}

			set
			{
				_indexCurrentPage = value;
			}
		}

		public ListBox StandardQueueInElemAll
		{
			get
			{
				return _pageElemAll.StandardQueue;
			}
		}

		public ListBox StandardQueueInStandardMail
		{
			get
			{
				return _pageStdMail.StandardQueue;
			}
		}

		public TreeViewVS ScheduledQueueInElemAll
		{
			get
			{
				return _pageElemAll.ScheduledQueue;
			}
		}

		public ListBox ScheduledQueueInScheduledMail
		{
			get
			{
				return _pageSchMail.ScheduledQueue;
			}
		}

		public TreeView ScheduledQueueInScheduledTask
		{
			get
			{
				return _pageSchTask.TaskQueue;
			}
		}

		public PageElemAll PElemAll
		{
			get
			{
				return _pageElemAll;
			}
		}

		public PageStandardMail PStandardMail
		{
			get
			{
				return _pageStdMail;
			}
		}

		public PageScheduledMail PScheduledMail
		{
			get
			{
				return _pageSchMail;
			}
		}

		public PageScheduledTask PScheduledTask
		{
			get
			{
				return _pageSchTask;
			}
		}

		public PageButtons PButtons
		{
			get
			{
				return _pageButtons;
			}
		}

		public PageMainLog PMainLog
		{
			get
			{
				return _pageMainLog;
			}
		}

		public PageProgress PProgress
		{
			get
			{
				return _pageProgress;
			}
		}

		#endregion
	}


}
