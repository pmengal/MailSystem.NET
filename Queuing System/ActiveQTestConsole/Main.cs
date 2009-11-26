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
using ActiveQLibrary;
using ActiveQLibrary.Serialization.ConfigTask;
using ActiveQLibrary.Serialization.ConfigGlobal;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Forms;

namespace ActiveQTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			// 
			/*Logger.Initialize(LogType.debug);

			ConfigGlobal.CreateDefaultConfigFile();
			ConfigGlobal.Load();
			ManageForm.Initialize();
			ManageForm.ShowForm();

			Reader r = new Reader();
			//Console.ReadLine();
			r.StartReader();

			Processer p = new Processer();
			p.StartProcesser();

			Thread.Sleep(50000);

			r.StopReader();
			p.StopProcesser();*/

			/*Tasks ts = new Tasks();
			
			Task t1 = new Task();
			t1.Address = "http://www.google.com";
			t1.DateStart = DateTime.Now;
			t1.Method = "post";

			Task t2 = new Task();
			t2.Address = "http://www.yahoo.com";
			t2.DateStart = DateTime.Now;
			t2.DateEnd = DateTime.Now.AddDays(2);
			t2.Method = "get";

			Recurrence r1 = new Recurrence(TypeRecurrence.daily);
			Recurrence r2 = new Recurrence(TypeRecurrence.selectedDay,DayOfWeek.Monday.ToString(),new DateTime(2002,1,1,23,12,10));

			t1.AddRecurrence(r1);
			t1.AddRecurrence(r2);
			
			ts.TasksList.Add(t1);
			ts.TasksList.Add(t2);

			TextWriter writer1 = new StreamWriter("tasks.xml");
			XmlSerializer serializer1 = new XmlSerializer(typeof(Tasks));
			serializer1.Serialize(writer1, ts);
			writer1.Close();*/

			/*Config c = new Config();
			c.Threads = 10;

			Readers r = new Readers();
			r.MailPickUp = 10;
			r.XmlPickup = 5;
			c.Readers = r;

			c.MailPickupDirectories.Add(@"c:\spool1");	
			c.MailPickupDirectories.Add(@"c:\spool2");	
			c.MailPickupDirectories.Add(@"c:\temp\spool");	
		
			c.XmlPickupFiles.Add(@"c:\temp\test.xml");

			TextWriter writer2 = new StreamWriter("newconfig.xml");
			XmlSerializer serializer2 = new XmlSerializer(typeof(Config));
			serializer2.Serialize(writer2, c);
			writer2.Close();*/

			/*Global.InitializeLibrary(LogType.debug);
			Global.LoadConfig();
			
			ManageForm.Initialize(); 
			//ManageForm.ShowForm(); 
			

			Reader ra = new Reader();
			ra.StartReader(); 

			Processer p = new Processer();
			p.StartProcesser();

			ManageForm.HideForm();

			Thread.Sleep(180000);

			ra.StopReader();
			p.StopProcesser();*/

			/*ActiveQLibrary.Form.MainForm _main = new ActiveQLibrary.Form.MainForm();
			Application.Run(_main);*/

			//ActiveQLibrary.Form.ManageForm.Initialize();	

			try
			{
				Global.InitializeLibrary("Config.xml",LogType.normal);
				//Global.LoadConfig();

				//Thread.Sleep(1000);

				Global.StartActiveQ();

				//ActiveQLibrary.Form.ManageForm.Form.Visible = true;

				Thread.Sleep(200000);

				/*Global.PauseActiveQ();

				Thread.Sleep(10000);

				Global.StopActiveQ();

				Thread.Sleep(30000);

				Global.StartActiveQ();

				ActiveQLibrary.Form.ManageForm.Form.Visible = true;

				Thread.Sleep(30000);*/

				Global.StopActiveQ();

				
			}

			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}



		}

		public static void test()
		{
			ActiveQLibrary.Form.MainForm main = new ActiveQLibrary.Form.MainForm();
			
			Application.Run(main);
		}

	}
}
