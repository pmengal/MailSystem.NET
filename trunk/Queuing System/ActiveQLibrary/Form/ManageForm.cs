using System;
using System.Threading;
using System.Windows.Forms;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Manage the mainForm
	/// </summary>
	public class ManageForm
	{
		#region delegates

		delegate void AddElemMainLogDelegate(string Elem);
		delegate void AddElemStandardQueueDelegate(string Elem);

		delegate void RemoveElemStandardQueueDelegate(string Elem);

		#endregion

		#region Variables

		/// <summary>
		/// Main form of the application
		/// </summary>
		private static MainForm _mainForm;

		#endregion
		
		#region Properties

		/// <summary>
		/// Get the main form.
		/// </summary>
		public static MainForm Form
		{
			get
			{
				return _mainForm;
			}
		}
		
		#endregion

		#region Functions

		/// <summary>
		/// Initialize the main form, this work in background (thread).
		/// </summary>
		public static void Initialize()
		{
			// run the thread 
			new Thread(new ThreadStart(FctThreadInitialize)).Start();
		}

		/// <summary>
		/// Function used as background to run the main form.
		/// </summary>
		private static void FctThreadInitialize()
		{
			_mainForm = new MainForm();
			Application.Run(_mainForm);
		}

		/// <summary>
		/// Add a standard element.
		/// </summary>
		/// <param name="Elem">Element to add</param>
		public static void AddElemStandardQueue(string Elem)
		{
			if (_mainForm != null)
			{
				AddElemStandardQueueDelegate addElemStandardQueueDelegate = new AddElemStandardQueueDelegate(AddElemStandardQueueElemAllFct);
				_mainForm.StandardQueueInElemAll.Invoke(addElemStandardQueueDelegate,new object[] {Elem});

				addElemStandardQueueDelegate = new AddElemStandardQueueDelegate(AddElemStandardQueueStdMailFct);
				_mainForm.StandardQueueInStandardMail.Invoke(addElemStandardQueueDelegate,new object[] {Elem});
			}
		}

		/// <summary>
		/// Used in the delegate to add an element in the ElemAll panel.
		/// </summary>
		/// <param name="Elem">Element to add</param>
		private static void AddElemStandardQueueElemAllFct(string Elem)
		{
			_mainForm.StandardQueueInElemAll.Items.Add(Elem);
		}

		/// <summary>
		/// Used in the delegate to add an element in the StandardMail panel.
		/// </summary>
		/// <param name="Elem">Element to add</param>
		private static void AddElemStandardQueueStdMailFct(string Elem)
		{
			_mainForm.StandardQueueInStandardMail.Items.Add(Elem);
		}

		/// <summary>
		/// Remove an element from the standard queue viewer.
		/// </summary>
		/// <param name="Elem">Element to remove</param>
		public static void RemoveElemStandardQueue(string Elem)
		{
			if (_mainForm != null)
			{
				RemoveElemStandardQueueDelegate removeElemStandardQueueDelegate = new RemoveElemStandardQueueDelegate(RemoveElemStandardQueueElemAllFct);
				_mainForm.StandardQueueInElemAll.Invoke(removeElemStandardQueueDelegate,new object[] {Elem});

				removeElemStandardQueueDelegate = new RemoveElemStandardQueueDelegate(RemoveElemStandardQueueStdMailFct);
				_mainForm.StandardQueueInElemAll.Invoke(removeElemStandardQueueDelegate,new object[] {Elem});
			}
		}

		/// <summary>
		/// Used in the delegate to remove an element in the ElemAll panel
		/// </summary>
		/// <param name="Elem">Element to remove.</param>
		private static void RemoveElemStandardQueueElemAllFct(string Elem)
		{
			_mainForm.StandardQueueInElemAll.Items.Remove(Elem);
		}

		/// <summary>
		/// Used in the delegate to remove an element in the StandardMail panel
		/// </summary>
		/// <param name="Elem">Element to remove.</param>
		private static void RemoveElemStandardQueueStdMailFct(string Elem)
		{
			_mainForm.StandardQueueInStandardMail.Items.Remove(Elem);
		}

		/// <summary>
		/// Add a scheduled element.
		/// </summary>
		/// <param name="Elem">Element to add</param>
		public static void AddElementScheduledQueue(string Elem)
		{
			_mainForm.PElemAll.AddItemScheduledQueue(null,Elem);
            _mainForm.ScheduledQueueInScheduledMail.Items.Add(Elem);
		}

		/// <summary>
		/// Remove a scheduled element.
		/// </summary>
		/// <param name="Elem">Element to remove</param>
		public static void RemoveElemScheduledQueue(string Elem)
		{
			_mainForm.PElemAll.RemoveItemScheduledQueue(Elem);
			_mainForm.ScheduledQueueInScheduledMail.Items.Remove(Elem);
		}

		public static void RemoveElemScheduledTask(string Elem)
		{
			_mainForm.PElemAll.RemoveItemScheduledTask(Elem);

			_mainForm.PScheduledTask.RemoveItemScheduledTask(Elem);			
		}

		/// <summary>
		/// Add a new task.
		/// </summary>
		/// <param name="Elem">Element</param>
		/// <param name="Name">Name</param>
		public static void AddElementTaskQueue(string Elem, string Name)
		{
			_mainForm.PElemAll.AddItemScheduledQueue(Elem,Name);
			_mainForm.PScheduledTask.AddItem(Elem,Name);
		}

		/// <summary>
		/// Add element in main log.
		/// </summary>
		/// <param name="s">String to add</param>
		public static void AddElementInMainLog(string s)
		{
			if (_mainForm != null)
			{

				AddElemMainLogDelegate addElemMainLogDelegate = new AddElemMainLogDelegate(AddElementInMainLogFct);
			
				_mainForm.PMainLog.Invoke(addElemMainLogDelegate,new object[] {s});
			}
		}

		/// <summary>
		/// Used in the delegate to add a new element.
		/// </summary>
		/// <param name="s">String to add</param>
		private static void AddElementInMainLogFct(string s)
		{
			try
			{
				if (_mainForm != null)
				{
					int indexStart = 0;
					int indexEnd = 0;

					do
					{

						if (indexStart == 0)
						{
							indexEnd = s.IndexOf("\n",indexStart); 
						}

						else
						{
							if (indexStart > 0)
								indexStart = s.IndexOf("\n",indexStart);
							else 
								indexStart = -1;

							if (indexStart >= 0 && indexStart + 1 < s.Length)
							{
								indexEnd = s.IndexOf("\n", indexStart + 1);
							}
							else 
								indexStart = -1;
											
						}

						if (indexStart == 0)
						{
							if (indexEnd == -1)
								_mainForm.PMainLog.AddLog(s.Substring(indexStart));

							else
								_mainForm.PMainLog.AddLog(s.Substring(indexStart,indexEnd - indexStart));

							if (indexEnd != -1)
								indexStart = indexEnd -1 ;
							else 
								indexStart = -1;
						}

						else if (indexStart > 0 && indexEnd >=0)
						{
							_mainForm.PMainLog.AddLog(s.Substring(indexStart + 1,indexEnd - indexStart - 1));
							indexStart ++;
						}
						
						else if (indexStart > 0 && indexEnd == -1)
						{
							_mainForm.PMainLog.AddLog(s.Substring(indexStart + 1));
							indexStart = -1;
						}
					
					} while (indexStart != -1);

				}
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[MANAGEFORM:ADDLOG] " + ex.Message);
				Global.Log.WriteError("[MANAGEFORM:ADDLOG] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Add a new item in the list view of the progress page control
		/// </summary>
		/// <param name="_newItem">new item</param>
		public static void AddElementProgress(ProgressItem _newItem)
		{
			_mainForm.PProgress.AddItem(_newItem);
		}

		#endregion
	}
}
