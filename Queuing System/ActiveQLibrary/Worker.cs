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
using System.Threading;
using System.IO;
using System.Web;
using System.Net;
using System.Diagnostics;

using ActiveQLibrary.Serialization.ConfigTask;

namespace ActiveQLibrary
{
	#region class Worker

	/// <summary>
	/// Receive a job from the dispacher, and execute it.
	/// </summary>
	public class Worker
	{

		#region Variables

		/// <summary>
		/// Thread worker.
		/// </summary>
		private Thread _threadWorker;

		/// <summary>
		/// Name of the thread;
		/// </summary>
		private string _name;

		/// <summary>
		/// Flag to indicate if the thread have to run or have to be stopped.
		/// </summary>
		private bool _isRunning = false;

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread have to run or be stopped.
		/// </summary>
		private Mutex _mutexIsRunning;

		/// <summary>
		/// Work to do, if it's null, nothing to do.
		/// </summary>
		private SpooledObject _spooledObject = null;

		/// <summary>
		/// Mutex to protect integrity of SpooledObjet.
		/// </summary>
		private Mutex _mutexSpooledObject;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Name">The name of the object, used to identify the thread</param>
		public Worker(string Name)
		{
			// check if the name is valid
			if (Name == null)
				throw new ArgumentNullException("Name");

			if (Name.Trim() == "")
				throw new ArgumentException("Cannot be blank","Name");

			_name = Name;

			// create mutex object 
			_mutexIsRunning = new Mutex(false,string.Format("_mutex{0}IsRunning",Name));
			_mutexSpooledObject = new Mutex(false,string.Format("_mutex{0}SpooledObject",Name));
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the flag to indicate if the thread have to run or be stopped.
		/// </summary>
		private bool IsRunning
		{
			get
			{
				return _isRunning;
			}

			set
			{
				_mutexIsRunning.WaitOne();
				_isRunning = value;
				_mutexIsRunning.ReleaseMutex();
			}
		}

		/// <summary>
		/// Get / set the task to do.
		/// </summary>
		public SpooledObject TaskToDo
		{
			get
			{
				return _spooledObject;
			}

			set
			{
				_mutexSpooledObject.WaitOne();
				_spooledObject = value;
				_mutexSpooledObject.ReleaseMutex();
			}
		}

		/// <summary>
		/// Give the name of the thread.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
		}

		#endregion

		#region Functions

		/// <summary>
		/// Starts the worker.
		/// </summary>
		public void StartWorker()
		{
			try
			{
				// configure the thread
				_threadWorker = new Thread(new ThreadStart(FctThreadWorker));
				_threadWorker.Name = Name;

                Global.Log.WriteEvent(LogType.normal,string.Format("[WORKER] Starting {0}",_threadWorker.Name));
				// Start the thread
				_threadWorker.Start();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.Message));
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.StackTrace));
			}
		}

		/// <summary>
		/// Stops the worker.
		/// </summary>
		public void StopWorker()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[WORKER] Stopping {0}",_threadWorker.Name));
				IsRunning = false;

				if ((_threadWorker.ThreadState & System.Threading.ThreadState.Suspended) == System.Threading.ThreadState.Suspended)
				{
					try
					{
						_threadWorker.Resume();
						_threadWorker.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[WORKER] " + ex.Message);
						Global.Log.WriteError("[WORKER] " + ex.StackTrace);
					}

				}
				

				// Wait the end of the thread
				_threadWorker.Join(10000);
				// If the thread is already alive, kill it
				if (_threadWorker.IsAlive == true)
				{
					Global.Log.WriteError(string.Format("[{0}] Time-out stopping {0}, thread is aborted",_threadWorker.Name));
					_threadWorker.Abort();
				}
			}

			catch(ThreadAbortException)
			{
			}

			catch(Exception ex)
			{
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.Message));
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.StackTrace));
			}
		}

		/// <summary>
		/// Pauses the worker.
		/// </summary>
		public void PauseWorker()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[WORKER] Pausing {0}",_threadWorker.Name));
				// pause the thread
				_threadWorker.Suspend();				
			}

			catch(Exception ex)
			{
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.Message));
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.StackTrace));
			}
		}

		/// <summary>
		/// Resumes the thread after a pause.
		/// </summary>
		public void ContinueWorker()
		{
			try
			{
				Global.Log.WriteEvent(LogType.debug,string.Format("[WORKER] Resuming {0}",_threadWorker.Name));
				// resume the thread
				_threadWorker.Resume();				
			}

			catch(Exception ex)
			{
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.Message));
				Global.Log.WriteError(string.Format("[{0}] {1}",_threadWorker.Name,ex.Message,ex.StackTrace));
			}
		}

		/// <summary>
		/// Function used in the thread.
		/// </summary>
		private void FctThreadWorker()
		{
			IsRunning = true;

			while (IsRunning == true)
			{
				// check if a task have been transmitted by the dispacher
				if (TaskToDo != null)
				{
                    string fileName = TaskToDo.Name;
					try
					{
						Global.Log.WriteEvent(LogType.normal,string.Format("[{0}] Start of task '{1}'",_threadWorker.Name,TaskToDo.Name));

						switch (TaskToDo.Type)
						{
							case TypeSpooledObject.standardMail:
							case TypeSpooledObject.scheduledMail:
							{
								Object message = Activator.CreateInstance(Global.ActiveCommonAsm.GetType("ActiveUp.Net.Mail.Message",true));
								message = TaskToDo.Object;
								/*message.GetType().GetProperty("Subject").SetValue(message,string.Format("TEST MAIL : {0}",_threadWorker.Name),null);
								Global.ActiveMailAsm.GetType("ActiveUp.Mail.Smtp.SmtpClient").GetMethod("Send",new Type[] {Global.ActiveMailAsm.GetType("ActiveUp.Mail.Common.Message",true), Type.GetType("System.String")}).Invoke(null,new object[] {message,"mail.activeup.com"});*/

								bool sentOK = true;

                                if ((int)Global.SmtpServers.GetType().GetProperty("Count").GetValue(Global.SmtpServers, null) > 0)
                                {
                                    sentOK = (bool)Global.ActiveSmtpAsm.GetType("ActiveUp.Net.Mail.SmtpClient").GetMethod("Send", new Type[] { Global.ActiveCommonAsm.GetType("ActiveUp.Net.Mail.Message", true), Global.ActiveCommonAsm.GetType("ActiveUp.Net.Mail.ServerCollection", true) }).Invoke(null, new object[] { message, Global.SmtpServers });
                                }
                                else
                                {
                                    //Global.ActiveMailAsm.GetType("ActiveUp.Net.Mail.SmtpClient").GetMethod("Send", new Type[] { Global.ActiveMailAsm.GetType("ActiveUp.Net.Mail.Message", true) }).Invoke(null, new object[] { message });
                                    Global.ActiveSmtpAsm.GetType("ActiveUp.Net.Mail.SmtpClient").GetMethod("DirectSend", new Type[] { Global.ActiveCommonAsm.GetType("ActiveUp.Net.Mail.Message", true) }).Invoke(null, new object[] { message });
                                }

                                switch (TaskToDo.Type)
								{
									case TypeSpooledObject.standardMail:
									{
                                        QueueStandard.Remove(TaskToDo.Name);
										ActiveQLibrary.Form.ManageForm.RemoveElemStandardQueue(TaskToDo.Name);
									} break;

									case TypeSpooledObject.scheduledMail:
									{
                                        QueueScheduled.Remove(TaskToDo.Name);
										ActiveQLibrary.Form.ManageForm.RemoveElemScheduledQueue(TaskToDo.Name);
									} break;
								}

								/*ActiveQLibrary.Form.ManageForm.Form.PProgress.NewValueProgressBar(_threadWorker.Name,0);
								Thread.Sleep(2000);
								ActiveQLibrary.Form.ManageForm.Form.PProgress.NewValueProgressBar(_threadWorker.Name,20);
								Thread.Sleep(2000);
								ActiveQLibrary.Form.ManageForm.Form.PProgress.NewValueProgressBar(_threadWorker.Name,40);
								Thread.Sleep(2000);
								ActiveQLibrary.Form.ManageForm.Form.PProgress.NewValueProgressBar(_threadWorker.Name,60);
								Thread.Sleep(2000);
								ActiveQLibrary.Form.ManageForm.Form.PProgress.NewValueProgressBar(_threadWorker.Name,80);
								Thread.Sleep(2000);
								ActiveQLibrary.Form.ManageForm.Form.PProgress.NewValueProgressBar(_threadWorker.Name,100);
								Thread.Sleep(2000);*/

								if (sentOK == false)
								{
									Global.Log.WriteError(string.Format("[{0}:{1}] An error occurs when sending mail.",_threadWorker.Name,fileName));
									Global.MoveFileToError(TaskToDo.Name,string.Format("{0}",_threadWorker.Name));

								}

								else if (Global.ConfigValue.DeleteMailWhenProcessed == true)
								{
                                    File.Delete(TaskToDo.Name);	
								}
								else
								{
									Global.MoveFileToProcessed(TaskToDo.Name,string.Format("{0}",_threadWorker.Name));
								}


							} break;

							case TypeSpooledObject.scheduledTask:
							{
                                Task task = (Task)TaskToDo.Object;
								
								switch(task.Method.ToLower().ToString())
								{
									case "post":
									{
										POST(task.Address);
									} break;
									case "get":
									{
										GET(task.Address);
									} break;

									case "file":
									{
										Process newProcess = new Process();
										newProcess.StartInfo.FileName = task.Address;
										newProcess.Start();
									} break;

								}

                                DateTime nextExecution = task.GetNextTriggered();
								ActiveQLibrary.Form.ManageForm.RemoveElemScheduledTask(TaskToDo.Name);
								if (nextExecution == DateTime.MinValue)
								{
									QueueScheduled.Remove(TaskToDo.Name);
									ActiveQLibrary.Form.ManageForm.RemoveElemScheduledTask(TaskToDo.Name);
									Global.Log.WriteEvent(LogType.debug,string.Format("[{0}] '{1}' no more exectution, deleting",_threadWorker.Name,TaskToDo.Name,nextExecution));
								}
								else
								{
									QueueScheduled.ModifySendingDate(TaskToDo.Name,nextExecution);
									QueueScheduled.ModifyState(TaskToDo.Name,StateSpooledObject.queued);
									Global.Log.WriteEvent(LogType.debug,string.Format("[{0}] '{1}' next sending date '{2}'",_threadWorker.Name,TaskToDo.Name,nextExecution));
								}

							} break;
						}

						//Thread.Sleep(100000); 
						Global.Log.WriteEvent(LogType.normal,string.Format("[{0}] End of task '{1}'",_threadWorker.Name,TaskToDo.Name));
						Thread.Sleep(200);
						// the task is ended, set the TaskToDo to null to indicates to the dispacher that it's free for a new task
						TaskToDo = null;
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError(string.Format("[{0}:{1}] {2}",_threadWorker.Name,fileName,ex.Message));
						Global.Log.WriteError(string.Format("[{0}:{1}] {2}",_threadWorker.Name,fileName,ex.StackTrace));
						if (ex.InnerException != null)
						{
							Global.Log.WriteError(string.Format("[{0}:{1}] {2}",_threadWorker.Name,fileName,ex.InnerException.Message));
							Global.Log.WriteError(string.Format("[{0}:{1}] {2}",_threadWorker.Name,fileName,ex.InnerException.StackTrace));
						}
						if (TaskToDo.Type != TypeSpooledObject.scheduledTask)
						{
							Global.MoveFileToError(TaskToDo.Name,string.Format("{0}",_threadWorker.Name));
							if (TaskToDo.Type == TypeSpooledObject.standardMail)
								QueueStandard.Remove(TaskToDo.Name);
							else if (TaskToDo.Type == TypeSpooledObject.scheduledMail)
								QueueScheduled.Remove(TaskToDo.Name);
						}
						TaskToDo = null; 
					}
				}

				Thread.Sleep(100);
			}

		}

		private void GET(string url)
		{
			HttpWebRequest oWRequest =(HttpWebRequest) WebRequest.Create(url);
			//la réponse
			HttpWebResponse oWResponse =(HttpWebResponse) oWRequest.GetResponse(); 
			Stream oS = oWResponse.GetResponseStream(); 
			StreamReader oSReader = new StreamReader(oS,System.Text.Encoding.ASCII);
			string result = oSReader.ReadToEnd();
			oSReader.Close();
			oS.Close();
		}

		private void POST(string url)
		{
			HttpWebRequest oWRequest =(HttpWebRequest) WebRequest.Create(url);
			//écriture dans le flux d'interrogation 
			oWRequest.ContentLength = 0;
			oWRequest.Method = "POST"; 
			oWRequest.ContentType = "application/x-www-form-urlencoded"; 
			Stream oS1 = oWRequest.GetRequestStream(); 
		}

		#endregion

	}

	#endregion
}
