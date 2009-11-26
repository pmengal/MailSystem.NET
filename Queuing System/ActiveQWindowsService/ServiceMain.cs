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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using ActiveQLibrary;

namespace ActiveQWindowsService
{
	public class ActiveQ : System.ServiceProcess.ServiceBase
	{
		private System.ServiceProcess.ServiceController serviceController1;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ActiveQ()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
			this.CanShutdown = true;
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new ActiveQ() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceController1 = new System.ServiceProcess.ServiceController();
			// 
			// ActiveQ
			// 
			this.CanPauseAndContinue = true;
			this.ServiceName = "ActiveQ";

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
			
			try
			{
				Global.InitializeLibrary("Config.xml",LogType.normal);
				
				Thread.Sleep(1000);
				Global.StartActiveQ();
			}

			catch(Exception ex)
			{
				EventLog.WriteEntry(string.Format("Initialization failed.\nSource : {0}\nMessage : {1}\nStack : {2}",ex.Source,ex.Message,ex.StackTrace)
					,EventLogEntryType.Error);


				Global.Log.WriteError("[SERVICE:START] " + ex.Message);
				Global.Log.WriteError("[SERVICE:START] " + ex.StackTrace);

				this.OnStop();
			}
		}
 
		protected override void OnPause()
		{
			Global.Log.WriteEvent(LogType.debug,"[SERVICE] OnPause");
			Global.PauseActiveQ();
		}

		protected override void OnContinue()
		{
			Global.Log.WriteEvent(LogType.debug,"[SERVICE] OnContinue");
			Global.ContinueActiveQ();
		}

		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
			Global.Log.WriteEvent(LogType.debug,"[SERVICE] OnStop");
			Global.StopActiveQ();
		}

		/// <summary>
		/// Shutdown this service
		/// </summary>
		protected override void OnShutdown()
		{
			Global.Log.WriteEvent(LogType.debug,"[SERVICE] OnShutdown");
			Global.StopActiveQ();
		}

		protected override void OnCustomCommand(int command)
		{
			switch(command)
			{
				case 200:
				{
					Global.Log.WriteEvent(LogType.debug,string.Format("[CUSTOM COMMAND] Custom command received : '{0}' (show form)",command));
					
					if (ActiveQLibrary.Form.ManageForm.Form.Visible == false)
					{
						ActiveQLibrary.Form.ManageForm.Form.Visible = true;
					}
					ActiveQLibrary.Form.ManageForm.Form.WindowState = System.Windows.Forms.FormWindowState.Normal;
					

				} break;

				case 221:
				{
					Global.Log.WriteEvent(LogType.debug,string.Format("[CUSTOM COMMAND] Custom command received : '{0}' (stop)",command));
					Global.StopActiveQ();

				} break;

				case 230:
				{
					Global.Log.WriteEvent(LogType.debug,string.Format("[CUSTOM COMMAND] Custom command received : '{0}' (reload config)",command));

					Global.ReloadConfig();
				} break;

				default : 
				{
					Global.Log.WriteError(string.Format("[CUSTOM COMMAND] Unkown command '{0}'",command));
				} break;
			}
		}
	}
}
