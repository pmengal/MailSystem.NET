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

namespace ActiveQManager
{
	/// <summary>
	/// Summary description for ServiceOperationEvent.
	/// </summary>
	/// 

	public delegate void FinishedServiceOperationEventHandler(object sender,FinishedServiceOperationEventArgs e);
	public delegate void ErrorServiceOperationEventHandler(object sender,ErrorServiceOperationEventArgs e);

	public enum Operation
	{
		Start = 0,
		Stop,
		Pause,
		Continue
	}

	public abstract class ServiceOperationEventArgs : EventArgs
	{
		private Operation _operation;

		public ServiceOperationEventArgs()
		{

		}

		public ServiceOperationEventArgs(Operation operation)
		{
			_operation = operation;
		}
		
		public Operation Operation 
		{
			get
			{
				return _operation;
			}
		}

	}

	public class FinishedServiceOperationEventArgs : ServiceOperationEventArgs
	{

		public FinishedServiceOperationEventArgs(Operation operation) : base(operation)
		{
			
		}
	}

	public class ErrorServiceOperationEventArgs : ServiceOperationEventArgs
	{
		private Exception _exception;

		public ErrorServiceOperationEventArgs(Operation operation, Exception exception) : base(operation)
		{
			_exception = exception;
		}

		public Exception Exception
		{
			get
			{
				return _exception;
			}
		}
	}

}
