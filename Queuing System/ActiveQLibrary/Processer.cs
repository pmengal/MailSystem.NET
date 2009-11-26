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
using System.Collections;

namespace ActiveQLibrary
{
	/// <summary>
	/// Check the queues and send elements from it in the dispacher.
	/// </summary>
	public class Processer : IDisposable
	{
		#region Variables

		/// <summary>
		/// Temporisation in millisecond before each scan.
		/// </summary>
		private readonly int _tempoThread = 350;

		/// <summary>
		/// Thread processer (standard queue)
		/// </summary>
		private Thread _threadProcesserStandard;

		/// <summary>
		/// Thread processeur (scheduled queue)
		/// </summary>
		private Thread _threadProcesserScheduled;

		/// <summary>
		/// Flag to indicate if the thread (standard) have to run or have to be stopped.
		/// </summary>
		private bool _isRunningStandard = false;

		/// <summary>
		/// Flag to indicate if the thread (scheduled) have to run or have to be stopped.
		/// </summary>
		private bool _isRunningScheduled = false;

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread have to run or be stopped.
		/// </summary>
		private Mutex _mutexIsRunningStandard = new Mutex(false,"_mutexIsProcesserRunningStandard");

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread have to run or be stopped.
		/// </summary>
		private Mutex _mutexIsRunningScheduled = new Mutex(false,"_mutexIsProcesserRunningScheduled");

		/// <summary>
		/// Dispacher
		/// </summary>
		private Dispacher _dispacher;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Processer()
		{
			_dispacher = new Dispacher();
		}
		#endregion

		#region Properties

		/// <summary>
		/// Get The numbers of millisecont between each scan.
		/// </summary>
		public int TempoThread
		{
			get
			{
				return _tempoThread;
			}
		}

		/// <summary>
		/// Get the dispacher.
		/// </summary>
		public Dispacher Dispacher
		{
			get
			{
				return _dispacher;
			}
		}

		/// <summary>
		/// Get / set the flag to indicate if the thread (standard) have to run or be stopped.
		/// </summary>
		private bool IsRunningStandard
		{
			get
			{
				return _isRunningStandard;
			}

			set
			{
				_mutexIsRunningStandard.WaitOne();
				_isRunningStandard = value;
				_mutexIsRunningStandard.ReleaseMutex();
			}
		}


		/// <summary>
		/// Get / set the flag to indicate if the thread (scheduled) have to run or be stopped.
		/// </summary>
		private bool IsRunningScheduled
		{
			get
			{
				return _isRunningScheduled;
			}

			set
			{
				_mutexIsRunningScheduled.WaitOne();
				_isRunningScheduled = value;
				_mutexIsRunningScheduled.ReleaseMutex();
			}
		}


		#endregion

		#region Functions

		/// <summary>
		/// Start the processer
		/// </summary>
		public void StartProcesser()
		{
			try
			{
				// Configure thread for the standard queue
				_threadProcesserStandard = new Thread(new ThreadStart(FctThreadProcesserStandard));
				_threadProcesserStandard.Name = "PROCESSER STANDARD";

				// Configure thread for the scheduled queue
				_threadProcesserScheduled = new Thread(new ThreadStart(FctThreadProcesserScheduled));
				_threadProcesserScheduled.Name = "PROCESSER SCHEDULED";


				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Starting {0}",_threadProcesserStandard.Name));
				// start thread standard 
				_threadProcesserStandard.Start();

				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Starting {0}",_threadProcesserScheduled.Name));
				// start thread scheduled
				_threadProcesserScheduled.Start();

				// start the dispacher
				_dispacher.StartDispacher();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[PROCESSER] " + ex.Message);
				Global.Log.WriteError("[PROCESSER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Stop the dispacher
		/// </summary>
		public void StopProcesser()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Stopping {0}",_threadProcesserStandard.Name));
				IsRunningStandard = false;

				if ((_threadProcesserStandard.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
				{
					try
					{
						_threadProcesserStandard.Resume();
						_threadProcesserStandard.Abort();
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
				

				else
				{
					// Wait the end of the thread
					_threadProcesserStandard.Join(10000);
					// If the thread is already alive, kill it
					if (_threadProcesserStandard.IsAlive == true)
					{
						Global.Log.WriteError(string.Format("[PROCESSER] Time-out stopping {0}, thread is aborted",_threadProcesserStandard.Name));
						try
						{
							_threadProcesserStandard.Abort();
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
				}

				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Stopping {0}",_threadProcesserScheduled.Name));
				IsRunningScheduled = false;

				if ((_threadProcesserScheduled.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
				{
					try
					{
						_threadProcesserScheduled.Resume();
						_threadProcesserScheduled.Abort();
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
				
				else
				{

					// Wait the end of the thread
					_threadProcesserScheduled.Join(10000);
					// If the thread is already alive, kill it
					if (_threadProcesserScheduled.IsAlive == true)
					{
						Global.Log.WriteError(string.Format("[PROCESSER] Time-out stopping {0}, thread is aborted",_threadProcesserScheduled.Name));
						try
						{
							_threadProcesserScheduled.Abort();
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
				}

				// stop the dispacher
				this._dispacher.StopDispacher();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[PROCESSER] " + ex.Message);
				Global.Log.WriteError("[PROCESSER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Pause the processer
		/// </summary>
		public void PauseProcesser()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Pausing {0}",_threadProcesserStandard.Name));
				// pause the thread standard
				_threadProcesserStandard.Suspend();

				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Pausing {0}",_threadProcesserScheduled.Name));
				// pause the thread scheduled
				_threadProcesserScheduled.Suspend();

				// pause the dispacher
				this._dispacher.PauseDispacher();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[PROCESSER] " + ex.Message);
				Global.Log.WriteError("[PROCESSER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Continue the processer
		/// </summary>
		public void ContinueProcesser()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Resuming {0}",_threadProcesserStandard.Name));
				// resume the thread standard
				_threadProcesserStandard.Resume();

				Global.Log.WriteEvent(LogType.normal,string.Format("[PROCESSER] Resuming {0}",_threadProcesserScheduled.Name));
				// resume the thread scheduled
				_threadProcesserScheduled.Resume();

				// resume the dispacher
				this._dispacher.ContinueDispacher();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[PROCESSER] " + ex.Message);
				Global.Log.WriteError("[PROCESSER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Function used in the thread standard.
		/// </summary>
		private void FctThreadProcesserStandard()
		{
			IsRunningStandard = true;

			while (IsRunningStandard == true)
			{
				try
				{
					Global.Log.WriteEvent(LogType.debug,string.Format("[PROCESSER:STD] Scanning standard queue..."));
			
					// check if the queue is empty
					if (QueueStandard.IsEmpty() == false)
					{
						// give all the elements in the queue and dispach it
						ArrayList queuedObject = QueueStandard.GiveSpooledObject(StateSpooledObject.queued);

						foreach(SpooledObject Object in queuedObject)
						{
							QueueStandard.ModifyState(Object.Name,StateSpooledObject.dispacher);
							_dispacher.AddToDispacher(Object);
						}
					}

					Global.Log.WriteEvent(LogType.debug,string.Format("[PROCESSER:STD] Next scan in {0} seconds",(float)_tempoThread/1000));
					Thread.Sleep(_tempoThread);
				}

				catch(ThreadAbortException)
				{
				}

				catch(Exception ex)
				{
					Global.Log.WriteError("[PROCESSER:STD] " + ex.Message);
					Global.Log.WriteError("[PROCESSER:STD] " + ex.StackTrace);
				}
			}
		}

		/// <summary>
		/// Function used in the thread scheduled.
		/// </summary>
		private void FctThreadProcesserScheduled()
		{
			IsRunningScheduled = true;

			while (IsRunningScheduled == true)
			{
				try
				{
					Global.Log.WriteEvent(LogType.debug,string.Format("[PROCESSER:SCH] Scanning scheduled queue..."));
			
					// check if the queue is empty
					if (QueueScheduled.IsEmpty() == false)
					{
						// give all the elements in the queue and dispach it
						ArrayList queuedObject = QueueScheduled.GiveSpooledObject(StateSpooledObject.queued);

						foreach(SpooledObject Object in queuedObject)
						{
							TimeSpan ts = Object.SendingDate - DateTime.Now;

							if (Object.SendingDate <= DateTime.Now)
							{
								QueueScheduled.ModifyState(Object.Name,StateSpooledObject.dispacher);
								_dispacher.AddToDispacher(Object);
							}
						}
					}

					Global.Log.WriteEvent(LogType.debug,string.Format("[PROCESSER:SCH] Next scan in {0} seconds",(float)_tempoThread/1000));
					Thread.Sleep(_tempoThread);
				}

				catch(ThreadAbortException)
				{
				}


				catch(Exception ex)
				{
					Global.Log.WriteError("[PROCESSER:SCH] " + ex.Message);
					Global.Log.WriteError("[PROCESSER:SCH] " + ex.StackTrace);
				}
			}
		}

		/// <summary>
		/// Dispose the processer
		/// </summary>
		public void Dispose()
		{
			this.StopProcesser();
		}

		#endregion
	}
}
