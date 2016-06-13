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
using System.Configuration.Install; 
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using ActiveQLibrary;

namespace ActiveQInstallCreateDefaultConfig
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class MainClass
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			string dir = "";
            
			for(int i = 0 ; i < args.Length ; i++)
			{
				if (i == 0)
					dir += args[i];
				else 
				{
					dir += " ";
					dir += args[i];
				}
			}

			if (dir[dir.Length-1] == 92)
				dir = dir.Substring(0,dir.Length-1);

            if (dir.Trim() == string.Empty)
                dir = System.Environment.CurrentDirectory;

			Global.CreateDefaultConfig(dir + @"\Config.xml", dir + @"\Pickup",false);

		}
	}

/*	/// <summary>
	/// Summary description for CreateDefaultConfig.
	/// </summary>
	[RunInstaller(true)]
	public class CreateDefaultConfig : System.Configuration.Install.Installer
	{
		public CreateDefaultConfig()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void Install(System.Collections.IDictionary stateServer)
		{
			try
			{
				base.Install(stateServer); 
				Global.CreateDefaultConfig(System.Environment.SystemDirectory + @"\Config.xml",Global.GetImagePath(Context.Parameters["assemblypath"]) + @"\Pickup");
			}

			catch(Exception ex)
			{
				EventLog ev = new EventLog();
				ev.Source = "ActiveQ";
	
				ev.WriteEntry(string.Format("ActiveQInstallCreateDefaultConfig:\n{0}.",ex.ToString()),EventLogEntryType.Error);

				throw ex;				
			}
		}
	}
*/
}
