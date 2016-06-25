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
using System.Threading;

namespace ActiveQLibrary
{
	#region class Dispacher

	/// <summary>
	/// Dispach a job to a free worker.
	/// </summary>
	public class Dispacher : IDisposable
	{
		#region Variables 

		/// <summary>
		/// Array list of worker thread.
		/// </summary>
		ArrayList _workers = new ArrayList();

		/// <summary>
		/// Queue of spooled object, this queue is FIFO (First In First Out)
		/// </summary>
        ArrayList _spooledObjects = new ArrayList();

		/// <summary>
		/// Mutex to protect integrity of data in the queue.
		/// </summary>
		private Mutex _mutexSpooledObjects = new Mutex(false,"_mutexSpooledObjects");

		/// <summary>
		/// Thread dispacher.
		/// </summary>
		private Thread _threadDispacher;

		/// <summary>
		/// Flag to indicate if the thread have to run or have to be stopped.
		/// </summary>
		private bool _isRunning = false;

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread have to run or be stopped.
		/// </summary>
		private Mutex _mutexIsRunning = new Mutex(false,"_mutexDispacherIsRunning");

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Dispacher()
		{
			// Creating the list of worker thread
			if (Global.ConfigValue.Threads <= 0)
				Global.ConfigValue.Threads = 10;

			if (Global.ConfigValue.Threads > 0)
			{
				for (int i = 0 ; i < Global.ConfigValue.Threads ; i++)
				{
					Worker newWorker = new Worker("WORKER"+i.ToString());
					_workers.Add(newWorker);
				
				}
			}

		}

		#endregion

		#region Properties

		/// <summary>
		/// Get the list of worker
		/// </summary>
		public ArrayList Workers
		{
			get
			{
				return _workers;
			}
		}

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

		#endregion

		#region Functions

		/// <summary>
		/// Add a spooled object to the dispacher.
		/// </summary>
		/// <param name="Object">Object to add</param>
		public void AddToDispacher(SpooledObject Object)
		{
			_mutexSpooledObjects.WaitOne();
			try
			{
				_spooledObjects.Add(Object);
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[DISPACHER] " + ex.Message);
				Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
			}

			_mutexSpooledObjects.ReleaseMutex();
		}

		/// <summary>
		/// Delete a element in the dispacher.
		/// </summary>
		/// <param name="Elem">The element to delete</param>
		/// <returns>True if deleted successfully, otherwise, False</returns>
		public bool Delete(string Elem)
		{
			bool flagIsDeleted = false;

			_mutexSpooledObjects.WaitOne();

			try
			{
				for (int i = 0 ; i < _spooledObjects.Count ; i++)
				{
					if (((SpooledObject)_spooledObjects[i]).Name == Elem)
					{	
						Global.Log.WriteEvent(LogType.debug,string.Format("[DISPACHER] Deleting '{0}'",Elem));
						_spooledObjects.RemoveAt(i);
						flagIsDeleted = true;
						break;
					}
				}
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[DISPACHER] " + ex.Message);
				Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
			}

			_mutexSpooledObjects.ReleaseMutex();

			return flagIsDeleted;

		}

		/// <summary>
		/// Starts the dispacher
		/// </summary>
		public void StartDispacher()
		{
			try
			{
				// Configure the thread
				_threadDispacher = new Thread(new ThreadStart(FctThreadDispacher));
				_threadDispacher.Name = "DISPACHER";

				Global.Log.WriteEvent(LogType.normal,string.Format("[DISPACHER] Starting {0}",_threadDispacher.Name));
				// start the thread
				_threadDispacher.Start();

				// start each worker
				for (int i = 0 ; i < _workers.Count ; i++)
					((Worker)_workers[i]).StartWorker();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[DISPACHER] " + ex.Message);
				Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Stops the dispacher
		/// </summary>
		public void StopDispacher()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[DISPACHER] Stopping {0}",_threadDispacher.Name));
				IsRunning = false;

				if ((_threadDispacher.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
				{
					try
					{
						_threadDispacher.Resume();
						_threadDispacher.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[PROCESSER] " + ex.Message);
						Global.Log.WriteError("[PROCESSER] " + ex.StackTrace);
					}
				}
					

				// Wait the end of the thread
				_threadDispacher.Join(10000);
				// If the thread is already alive, kill it
				if (_threadDispacher.IsAlive == true)
				{
					Global.Log.WriteError(string.Format("[DISPACHER] Time-out stopping {0}, thread is aborted",_threadDispacher.Name));
					try
					{
						_threadDispacher.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[DISPACHER] " + ex.Message);
						Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);	
					}
				}

				for (int i = 0 ; i < _workers.Count ; i++)
					((Worker)_workers[i]).StopWorker();
			}

			catch(ThreadAbortException)
			{
			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[DISPACHER] " + ex.Message);
				Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Pauses the dispacher
		/// </summary>
		public void PauseDispacher()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[DISPACHER] Pausing {0}",_threadDispacher.Name));
				// pause the thread
				_threadDispacher.Suspend();

				// pause each worker
				for (int i = 0 ; i < _workers.Count ; i++)
					((Worker)_workers[i]).PauseWorker();
			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[DISPACHER] " + ex.Message);
				Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Resumes the thread.
		/// </summary>
		public void ContinueDispacher()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[DISPACHER] Resuming {0}",_threadDispacher.Name));
				//
				_threadDispacher.Resume();

				for (int i = 0 ; i < _workers.Count ; i++)
					((Worker)_workers[i]).ContinueWorker();
			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[DISPACHER] " + ex.Message);
				Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Function used in the thread.
		/// </summary>
		private void FctThreadDispacher()
		{
			IsRunning = true;

			while (IsRunning == true)
			{
				// find a free worker
				bool oneWorkerIsFree = false;

				while (oneWorkerIsFree == false)
				{
					foreach(Worker work in _workers)
						if (work.TaskToDo == null)
						{
							oneWorkerIsFree = true;
							break;
						}

						Thread.Sleep(100);
				}

				_mutexSpooledObjects.WaitOne();

				try
				{
					if (_spooledObjects.Count > 0)
					{
						for(int i = 0 ; i < _workers.Count ; i++)
						{
							// dispach the spooled object to a free worker
							if (((Worker)_workers[i]).TaskToDo == null)
							{
								SpooledObject toWork = (SpooledObject)_spooledObjects[0];
								_spooledObjects.RemoveAt(0);

								
								// modify state of object
								switch (toWork.Type)
								{
									case TypeSpooledObject.standardMail:
									{
										QueueStandard.ModifyState(toWork.Name,StateSpooledObject.sent);
									} break;

									case TypeSpooledObject.scheduledMail:
									case TypeSpooledObject.scheduledTask:
									{
										QueueScheduled.ModifyState(toWork.Name,StateSpooledObject.sent);
									} break;
								}
									
																
								Global.Log.WriteEvent(LogType.normal,string.Format("[DISPACHER] '{0}' is assigned to {1}",toWork.Name,((Worker)_workers[i]).Name));

								((Worker)_workers[i]).TaskToDo = toWork;

								break;
							}
						}
					}
				}

				catch(ThreadAbortException)
				{
				}

				catch(Exception ex)
				{
					Global.Log.WriteError("[DISPACHER] " + ex.Message);
					Global.Log.WriteError("[DISPACHER] " + ex.StackTrace);
				}

				_mutexSpooledObjects.ReleaseMutex();
			}

		}

		/// <summary>
		/// Dispose the object.
		/// </summary>
		public void Dispose()
		{
			// stop the dispacher
			this.StopDispacher();
		}

		#endregion

	}

	#endregion
}

