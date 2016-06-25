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
	#region class QueueStandard

	/// <summary>
	/// Manage the standard queue
	/// </summary>
	public class QueueStandard
	{
		#region Variables

		/// <summary>
		/// Queue FIFO (First In First Out) contening mail to send immediately.
		/// </summary>
		private static ArrayList _queueFIFO = new ArrayList();

		/// <summary>
		/// Mutex to protect integrity of data in the queue.
		/// </summary>
		private static Mutex _mutexQueueFIFO = new Mutex(false,"_mutexQueueFIFO");

		#endregion

		#region Functions

		/// <summary>
		/// Add an element in the queue.
		/// </summary>
		/// <param name="Object">SpooledObject to put in the queue</param>
		public static void AddElement(SpooledObject Object)
		{
			// check if the element is not already in the queue
			if (FindInQueue(Object.Name) == false)
			{
				_mutexQueueFIFO.WaitOne();
				try
				{
					// add the element in the queue
					_queueFIFO.Add(Object);
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Name         : {0}",Object.Name));
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Object       : {0}",Object.Object));
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Type         : {0}",Object.Type));
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] State        : {0}",Object.State));
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Sending date : {0}",Object.SendingDate));
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Added date   : {0}",Object.AddedDate));
				}

				catch (Exception ex)
				{
					Global.Log.WriteError("[QUEUE STD] " + ex.Message);
					Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
				}

				_mutexQueueFIFO.ReleaseMutex();
			}
			
		}

		/// <summary>
		/// Modifiy the state of an element.
		/// </summary>
		/// <param name="Name">Name of the element to modify</param>
		/// <param name="State">New state of the element</param>
		public static void ModifyState(string Name,StateSpooledObject State)
		{
			_mutexQueueFIFO.WaitOne();

			try
			{
				// check if the element is in the queue
				int index = -1;

				for(int i = 0 ; i < _queueFIFO.Count ; i++)
					if (((SpooledObject)_queueFIFO[i]).Name == Name)
					{
						index = i;
						break;
					}

				if (index >= 0)
				{
					// element is found, modify its state
					Global.Log.WriteEvent(LogType.normal,string.Format("[QUEUE STD] Changing state of '{0}', new state is {1}",Name,State));
					((SpooledObject)_queueFIFO[index]).State = State;
				}
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();
		}

		/// <summary>
		/// Give all element in a array list with the state specified by parameter.
		/// </summary>
		/// <param name="State">State of element to found</param>
		/// <returns>Array list contening the elements found</returns>
		public static ArrayList GiveSpooledObject(StateSpooledObject State)
		{
			ArrayList spooledObjectFiltered = new ArrayList();

			_mutexQueueFIFO.WaitOne();
			try
			{
				if (_queueFIFO.Count > 0)
				{
					for (int i = 0 ; i < _queueFIFO.Count ; i++)
					{
						if (((SpooledObject)_queueFIFO[i]).State == State)
						{
							spooledObjectFiltered.Add(((SpooledObject)_queueFIFO[i]));
						}
					}
				}

			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();

			return spooledObjectFiltered;
		}

		/// <summary>
		/// Find a object in the queue
		/// </summary>
		/// <param name="Name">Name of object to find</param>
		/// <returns>the object found</returns>
		public static SpooledObject GiveSpooledObject(string Name)
		{
			SpooledObject spooledObject = null;

			_mutexQueueFIFO.WaitOne();
			try
			{
				if (_queueFIFO.Count > 0)
				{
					for (int i = 0 ; i < _queueFIFO.Count ; i++)
					{
						if (((SpooledObject)_queueFIFO[i]).Name == Name)
						{
							spooledObject = (((SpooledObject)_queueFIFO[i]));
							break;
						}
					}
				}

			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();

			return spooledObject;
		}


		/// <summary>
		/// Print the queue.
		/// </summary>
		public static void PrintQueue()
		{
			_mutexQueueFIFO.WaitOne();

			try
			{
				if (_queueFIFO.Count == 0)
					Global.Log.WriteEvent(LogType.debug,"[QUEUE STD] Queue is empty...");

				else
					foreach(SpooledObject Object in _queueFIFO)
					{
						Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Name : {0}",Object.Name));
						Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Object : {0}",Object.Object));
						Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Type : {0}",Object.Type));
						Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] State : {0}",Object.State));
						Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Sending date : {0}",Object.SendingDate));
						Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Added date : {0}",Object.AddedDate));
					}
			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();
		}

		/// <summary>
		/// Find an element in the queue.
		/// </summary>
		/// <param name="Name">Name of the element</param>
		/// <returns>True if it's found, otherwise return False</returns>
		public static bool FindInQueue(string Name)
		{
			_mutexQueueFIFO.WaitOne();

			try
			{
				foreach(SpooledObject Object in _queueFIFO)
					if (Object.Name == Name)
					{
						_mutexQueueFIFO.ReleaseMutex();
						return true;
					}
			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();

			return false;
		}

		/// <summary>
		/// Remove an element in the queue.
		/// </summary>
		/// <param name="Name">Name of the element</param>
		public static void Remove(string Name)
		{
			_mutexQueueFIFO.WaitOne();

			try
			{
				// check if the element is in the queue
				int index = -1;

				for(int i = 0 ; i < _queueFIFO.Count ; i++)
					if (((SpooledObject)_queueFIFO[i]).Name == Name)
					{
						index = i;
						break;
					}
					
				if (index >= 0)
				{
					// element have been found
					Global.Log.WriteEvent(LogType.debug,string.Format("[QUEUE STD] Removing '{0}'",Name));
					_queueFIFO.RemoveAt(index);
				}

				Thread.Sleep(100);

			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();
		}

		/// <summary>
		/// Indicates if the queue is empty.
		/// </summary>
		/// <returns>True if the queue is empty, otherwise, return False</returns>
		public static bool IsEmpty()
		{
			bool returnValue = false;

			_mutexQueueFIFO.WaitOne();

			try
			{
				if (_queueFIFO.Count == 0)
					returnValue = true;
				else 
					returnValue = false;
			}

			catch (Exception ex)
			{
				Global.Log.WriteError("[QUEUE STD] " + ex.Message);
				Global.Log.WriteError("[QUEUE STD] " + ex.StackTrace);
			}

			_mutexQueueFIFO.ReleaseMutex();

			return returnValue;
		}


		#endregion
	}

	#endregion
}
