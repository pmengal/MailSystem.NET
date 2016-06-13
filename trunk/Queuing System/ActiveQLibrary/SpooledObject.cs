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

namespace ActiveQLibrary
{
	#region enum

	/// <summary>
	/// Type of spooled object
	/// </summary>
	public enum TypeSpooledObject
	{
		standardMail = 0,
		scheduledMail,
		scheduledTask
	}
	
	/// <summary>
	/// State of spooled object
	/// </summary>
	public enum StateSpooledObject
	{
		queued = 0,
		dispacher,
		sent
	}

	#endregion

	/// <summary>
	/// Spooled object contening necessary object for the worker to send or execute it.
	/// </summary>
	public class SpooledObject : IComparable
	{
		#region Variables

		/// <summary>
		/// Name of the object, often the name of the file
		/// </summary>
		private string _name;

		/// <summary>
		/// The object identified by the Type.
		/// For example : Mail, ....
		/// </summary>
		private object _object;

		/// <summary>
		/// Type of object to send or execute.
		/// </summary>
		private TypeSpooledObject _type;

		/// <summary>
		/// State of the Object in this life cycle.
		/// </summary>
		private StateSpooledObject _state;

		/// <summary>
		/// Date of creation.
		/// </summary>
		private DateTime _addedDate;

		/// <summary>
		/// Date indicates when the object have to be sent or executed.
		/// </summary>
		private DateTime _sendingDate;

		#endregion
	
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Name">Name</param>
		/// <param name="Type">Type</param>
		/// <param name="ObjectToExecute">Object to execute</param>
		public SpooledObject(string Name, TypeSpooledObject Type, Object ObjectToExecute)
		{
			_name = Name;
			_type = Type;
			_object = ObjectToExecute;
			_state = StateSpooledObject.queued;
			_addedDate = DateTime.Now;
			_sendingDate = DateTime.Now;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Name">Name</param>
		/// <param name="Type">Type</param>
		/// <param name="ObjectToExecute">Object to execute</param>
		/// <param name="SendingDate">Sending date</param>
		public SpooledObject(string Name, TypeSpooledObject Type, Object ObjectToExecute, DateTime SendingDate)
		{
			_name = Name;
			_type = Type;
			_object = ObjectToExecute;
			_state = StateSpooledObject.queued;
			_addedDate = DateTime.Now;
			_sendingDate = SendingDate;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the name
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}

			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Get / set the spooled object
		/// </summary>
		public object Object
		{
			get
			{
				return _object;
			}

			set
			{
				_object = value;
			}
		}

		/// <summary>
		/// Get / set the type.
		/// </summary>
		public TypeSpooledObject Type
		{
			get
			{
				return _type;
			}

			set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Get / set the state.
		/// </summary>
		public StateSpooledObject State
		{
			get
			{
				return _state;
			}

			set
			{
				_state = value;
			}
		}

		/// <summary>
		/// Get the added date.
		/// </summary>
		public DateTime AddedDate
		{
			get
			{
				return _addedDate;
			}
		}

		/// <summary>
		/// Get / set the sending date.
		/// </summary>
		public DateTime SendingDate
		{
			get
			{
				return _sendingDate;
			}

			set
			{
				_sendingDate = value;
			}
		}

		#endregion

		#region Interface

		/// <summary>
		/// Compares the current instance with another object of the same type.
		/// </summary>
		/// <param name="o">An object to compare with this instance</param>
		/// <returns>A 32-bit signed integer that indicates the relative order of the comparands</returns>
		int IComparable.CompareTo(object o)
		{
			SpooledObject objToCompare = (SpooledObject)o;
			
			long ticksInstance = _sendingDate.Ticks;
			long ticksObject = objToCompare.SendingDate.Ticks;

			if (ticksInstance > ticksObject)
				return 1;

			if (ticksInstance < ticksObject)
				return -1;

			return 0;
		}

		#endregion
	}
}
