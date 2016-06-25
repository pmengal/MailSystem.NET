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
using System.ServiceProcess;
using System.Threading;
using System.ComponentModel;

namespace ActiveQManager
{
	/// <summary>
	/// Summary description for ServiceOperation.
	/// </summary>
	public class ServiceOperation
	{

		#region Events

		public event FinishedServiceOperationEventHandler FinishedOperation;
		public event ErrorServiceOperationEventHandler ErrorOperation;
		
		#endregion

		#region Variables

		private ServiceController _srvController;	
		private Thread _threadOperation;
		private Operation _currentOperation;

		#endregion

		#region Constructors

		public ServiceOperation(ServiceController srvController)
		{
			_srvController = srvController;

		}

		#endregion

		#region Properties

		public ServiceController ServiceController
		{
			get
			{
				return _srvController;
			}
		}

		#endregion

		#region Functions

		public void Start()
		{
			_threadOperation = new Thread(new ThreadStart(startOperation));
			_threadOperation.Start();
			
		}

		public void Stop()
		{
			_threadOperation = new Thread(new ThreadStart(stopOperation));
			_threadOperation.Start();

		}

		public void Pause()
		{
			_threadOperation = new Thread(new ThreadStart(pauseOperation));
			_threadOperation.Start();
		}

		public void Continue()
		{
			_threadOperation = new Thread(new ThreadStart(continueOperation));
			_threadOperation.Start();
		}

		public void Refresh()
		{
			_srvController = new ServiceController(_srvController.ServiceName);
		}

		#endregion

		#region Threads functions

		private void startOperation()
		{

			_currentOperation = Operation.Start;

			try
			{
				_srvController.Start();
				_srvController.WaitForStatus(ServiceControllerStatus.Running);

				if (FinishedOperation != null)
					FinishedOperation(this, new FinishedServiceOperationEventArgs(_currentOperation));				
			}

			catch (Exception e)
			{
				if ((e is ThreadAbortException) == false)
					if (ErrorOperation != null)
						ErrorOperation(this, new ErrorServiceOperationEventArgs(_currentOperation,e));
			}
				
		}

		private void stopOperation()
		{

			_currentOperation = Operation.Stop;

			try
			{
				_srvController.Stop();
				_srvController.WaitForStatus(ServiceControllerStatus.Stopped);

				if (FinishedOperation != null)
					FinishedOperation(this, new FinishedServiceOperationEventArgs(_currentOperation));				
			}

			catch (Exception e)
			{
				if ((e is ThreadAbortException) == false)
					if (ErrorOperation != null)
						ErrorOperation(this, new ErrorServiceOperationEventArgs(_currentOperation,e));
			}
				
		}

		private void pauseOperation()
		{
			_currentOperation = Operation.Pause;

			try
			{
				_srvController.Pause();
				_srvController.WaitForStatus(ServiceControllerStatus.Paused);

				if (FinishedOperation != null)
					FinishedOperation(this, new FinishedServiceOperationEventArgs(_currentOperation));				
			}

			catch (Exception e)
			{
				if ((e is ThreadAbortException) == false)
					if (ErrorOperation != null)
						ErrorOperation(this, new ErrorServiceOperationEventArgs(_currentOperation,e));
			}
		}

		private void continueOperation()
		{
			_currentOperation = Operation.Continue;

			try
			{
				_srvController.Continue();
				_srvController.WaitForStatus(ServiceControllerStatus.Running);

				if (FinishedOperation != null)
					FinishedOperation(this, new FinishedServiceOperationEventArgs(_currentOperation));				
			}

			catch (Exception e)
			{
				if ((e is ThreadAbortException) == false)
					if (ErrorOperation != null)
						ErrorOperation(this, new ErrorServiceOperationEventArgs(_currentOperation,e));
			}			
		}

		#endregion

	}
}

