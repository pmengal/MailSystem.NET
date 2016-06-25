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
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;

using ActiveQLibrary.Serialization.ConfigGlobal;
using ActiveQLibrary.Serialization.ConfigTask;
using ActiveQLibrary.Form;

namespace ActiveQLibrary
{
	#region class Global

	/// <summary>
	/// Global object for the ActiveQ library.
	/// </summary>
	public class Global
	{
		#region Variables

		/// <summary>
		/// The name of the config file.
		/// </summary>
		private static string _configFileName = "Config.xml";

		/// <summary>
		/// Config object, contening all informations about configuration.
		/// </summary>
		private static Config _config;

		/// <summary>
		/// Logger object, using for log informations (event and error).
		/// </summary>
		private static Logger _log;

		/// <summary>
		/// Reader, read each directories and files and enqueue it.
		/// </summary>
		private static Reader _reader;

		/// <summary>
		/// Dispach elements.
		/// </summary>
		private static Processer _processer;

		/// <summary>
		/// Assembly from active common.
		/// </summary>
		private static Assembly _activeCommon;

        /// <summary>
        /// Assembly from active smtp.
        /// </summary>
        private static Assembly _activeSmtp;

        /// <summary>
        /// Assembly from activemail.
        /// </summary>
        private static Assembly _activeMail;

		/// <summary>
		/// The name of library file.
		/// </summary>
		private static string _libraryMailFile = "ActiveUp.Net.Mail.dll";

        /// <summary>
        /// The name of common library file.
        /// </summary>
        private static string _libraryCommonFile = "ActiveUp.Net.Common.dll";

        /// <summary>
        /// The name of smtp library file.
        /// </summary>
        private static string _librarySmtpFile = "ActiveUp.Net.Smtp.dll";

		/// <summary>
		/// Indicate if the application is initialized
		/// </summary>
		private static bool _isInitialized = false;

		/// <summary>
		/// List of smtp servers
		/// </summary>
		private static object _smtpServers = null;

		/// <summary>
		/// Path of the error directory
		/// </summary>
		private static string _pathDirError = "";

		/// <summary>
		/// Path of the processed directory
		/// </summary>
		private static string _pathDirProcessed = "";

		/// <summary>
		/// Callback function called when the timer occurs.
		/// </summary>
        private static TimerCallback _timerDelegate = new TimerCallback(MidnigthEvent);

		/// <summary>
		/// Timer used for the midnight event.
		/// </summary>
		private static System.Threading.Timer _timeMidnight = new System.Threading.Timer(_timerDelegate,null,System.Threading.Timeout.Infinite,System.Threading.Timeout.Infinite);

		#endregion

		#region Properties

		/// <summary>
		/// Get the name of the config file.
		/// </summary>
		public static string ConfigFileName
		{
			get
			{
				return _configFileName;
			}
		}

		/// <summary>
		/// Get / set the config value.
		/// </summary>
		public static Config ConfigValue
		{
			get
			{
				return _config;
			}

			set
			{
				_config = value;
			}
		}

		/// <summary>
		/// Get / set the logger.
		/// </summary>
		public static Logger Log
		{
			get
			{
				return _log;
			}

			set
			{
				_log = value;
			}
		}

		/// <summary>
		/// Get / set the activemail assembly.
		/// </summary>
		public static Assembly ActiveMailAsm
		{
			get
			{
				return _activeMail;
			}

			set
			{
				_activeMail = value;
			}
		}

        /// <summary>
        /// Get / set the active common assembly.
        /// </summary>
        public static Assembly ActiveCommonAsm
        {
            get
            {
                return _activeCommon;
            }

            set
            {
                _activeCommon = value;
            }
        }

        /// <summary>
        /// Get / set the active Smtp assembly.
        /// </summary>
        public static Assembly ActiveSmtpAsm
        {
            get
            {
                return _activeSmtp;
            }

            set
            {
                _activeSmtp = value;
            }
        }

		/// <summary>
		/// Get / set the flag to indicate if the application is initialized
		/// </summary>
		public static bool IsInitialized
		{
			get
			{
				return _isInitialized;
			}

			set
			{
				_isInitialized = value;
			}
		}

		/// <summary>
		/// Get / set the list of smtp servers
		/// </summary>
		public static object SmtpServers
		{
			get
			{
				return _smtpServers;
			}

			set
			{
				_smtpServers = value;
			}
		}

		/// <summary>
		/// Get / set the path of the error directory
		/// </summary>
		public static string PathDirError
		{
			get
			{
				return _pathDirError;
			}

			set
			{
				_pathDirError = value;
			}
		}

		/// <summary>
		/// Get / set the path of the processed directory
		/// </summary>
		public static string PathDirProcessed
		{
			get
			{
				return _pathDirProcessed;
			}

			set
			{
				_pathDirProcessed = value;
			}
		}

		#endregion

		#region Functions

		/// <summary>
		/// Initialize the library.
		/// Be careful : this method is mandatory for the good work of the application.
		/// </summary>
		public static void InitializeLibrary()
		{
			InitializeLibrary(_configFileName,null,LogType.normal);
		}

		/// <summary>
		/// Initialize the library.
		/// Be careful : this method is mandatory for the good work of the application.
		/// </summary>
		/// <param name="defaultLevel">The default level of loggin</param>
		public static void InitializeLibrary(LogType defaultLevel)
		{
			InitializeLibrary(_configFileName,null,defaultLevel);
		}

		/// <summary>
		/// Initialize the library.
		/// Be careful : this method is mandatory for the good work of the application.
		/// </summary>
		/// <param name="ConfigFile">The name of the config file</param>
		public static void InitializeLibrary(string ConfigFile)
		{
			InitializeLibrary(ConfigFile,null,LogType.normal);
		}

		/// <summary>
		/// Initialize the library.
		/// Be careful : this method is mandatory for the good work of the application.
		/// </summary>
		/// <param name="ConfigFile">The name of the config file</param>
		/// <param name="defaultLevel">The default level of loggin</param>
		public static void InitializeLibrary(string ConfigFile, LogType defaultLevel)
		{
			InitializeLibrary(ConfigFile,null,defaultLevel);
		}

		/// <summary>
		/// Initialize the library.
		/// Be careful : this method is mandatory for the good work of the application.
		/// </summary>
		/// <param name="ConfigFile">The name of the config file</param>
		/// <param name="Prefix">The prefix of the config file</param>
		/// <param name="defaultLevel">The default level of loggin</param>
		public static void InitializeLibrary(string ConfigFile, string Prefix, LogType defaultLevel)
		{
			if (ConfigFile == null)
				throw new ArgumentNullException("ConfigFile");

			if (ConfigFile.Trim() == "")
				throw new ArgumentException("Cannot be blank","ConfigFile");

			_config = new Config();
			_configFileName = ConfigFile;
			_log = new Logger(Prefix,defaultLevel);

			EventLog ev = new EventLog();
			ev.Source = "ActiveQ";

                       
            string path = GetImagePath(Assembly.GetExecutingAssembly().Location);

			// Create the error directory
			_pathDirError = path + @"\Error";
			Directory.CreateDirectory(_pathDirError);

			// Create the processed directory
			_pathDirProcessed = path + @"\Processed";
			Directory.CreateDirectory(_pathDirProcessed);
            
			try
			{
				// loading library                
                _activeMail = null;
                _activeCommon = null;
                _activeSmtp = null;
				try
				{                    
                    _activeMail = Assembly.LoadFrom(_libraryMailFile);
                    _activeCommon = Assembly.LoadFrom(_libraryCommonFile);
                    _activeSmtp = Assembly.LoadFrom(_librarySmtpFile);
				}
				catch (FileNotFoundException fe)
				{
                    
				}

				if (_activeMail == null)
				{
                    String dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    _activeMail = Assembly.LoadFrom(Path.Combine(dir, _libraryMailFile));
                    
				}
                if (_activeCommon == null)
                {
                    String dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    _activeCommon = Assembly.LoadFrom(Path.Combine(dir, _libraryCommonFile));
                    
                }
                if (_activeSmtp == null)
                {
                    String dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    _activeSmtp = Assembly.LoadFrom(Path.Combine(dir, _librarySmtpFile));

                }

                object _message = Activator.CreateInstance(_activeCommon.GetType("ActiveUp.Net.Mail.Message",true));
                _smtpServers = Activator.CreateInstance(_activeCommon.GetType("ActiveUp.Net.Mail.ServerCollection",true));
                LoadConfig();
				//_activeMail.GetType("ActiveUp.Mail.Common.LicenseInfo").GetProperty("License").SetValue(typeof(string),_config.ActiveMailLicense,null);
                string imageDir = GetImagePath(Assembly.GetExecutingAssembly().Location);
                
				if (imageDir.Trim() != "")
					_libraryMailFile = imageDir + @"\" + _libraryMailFile;

                
			}

			catch(FileNotFoundException)
			{
				Log.WriteError(string.Format("[INITIALIZE] Unable to find '{0}'.",_libraryMailFile));
				Log.WriteError(string.Format("[INITIALIZE] Mailing operation are disabled"));
				
				ev.WriteEntry(string.Format("Unable to find '{0}'.\nMailing operation are disabled.",_libraryMailFile),EventLogEntryType.Error); 
				
				string keyString = @"SOFTWARE\Active Up\ActiveQ";			
				RegistryKey key = Registry.LocalMachine.OpenSubKey(keyString); 
				if (key != null)
				{
					if ((string)key.GetValue("ActiveMailNotFound") == (string)"True")
					{
						PageActiveMailNotFound pageMailNotFound = new PageActiveMailNotFound();
						pageMailNotFound.ShowDialog();
					}

					key.Close();
				}
				else
				{
					PageActiveMailNotFound pageMailNotFound = new PageActiveMailNotFound();
					pageMailNotFound.ShowDialog();
				}
				
			}

			catch(Exception ex)
			{
				Type trialExpired = _activeMail.GetType("ActiveUp.Licensing.TrialException",true);
				if (ex.InnerException.GetType() == trialExpired)
				{
					Log.WriteError(string.Format("[INITIALIZE] Trial version of ActiveMail has expired, please register at www.activeup.com."));
					Log.WriteError(string.Format("[INITIALIZE] Mailing operation are disabled."));

					ev.WriteEntry("Trial version of ActiveMail has expired, please register at www.activeup.com.\nMailing operation are disabled.",EventLogEntryType.Error);

					PageActiveMailExpired pageMailExpired = new PageActiveMailExpired();
					pageMailExpired.ShowDialog();

					_activeMail = null;
				}
				else
				{
					if (ex.InnerException != null)
					{
						Log.WriteError(string.Format("[INITIALIZE] Error loading '{0}'",_libraryMailFile));
						Log.WriteError("[INITIALIZE] " + ex.InnerException.Message);
						Log.WriteError("[INITIALIZE] " + ex.InnerException.StackTrace);
					}
					else
					{
						Log.WriteError(string.Format("[INITIALIZE] Error loading '{0}'",_libraryMailFile));
						Log.WriteError("[INITIALIZE] " + ex.Message);
						Log.WriteError("[INITIALIZE] " + ex.StackTrace);
					}
				}

			}

			// Initalization for the midnigth event
			DateTime now = DateTime.Now;
			DateTime midnight = new DateTime(now.Year,now.Month,now.Day,0,0,0,0);
			midnight = midnight.AddDays(1);

			TimeSpan t = midnight - now;
			Log.WriteEvent(LogType.normal,string.Format("[INITIALIZE] Midnigth event occurs in {0}",t));
			_timeMidnight.Change(t,new TimeSpan(-1));

			_reader = new Reader();
			_processer = new Processer();
            
			ActiveQLibrary.Form.ManageForm.Initialize();
            
		}

		/// <summary>
		/// Get the image path of the application
		/// </summary>
		/// <returns>the image path</returns>
		public static string GetImagePath(string fullPath)
		{
			string imagePath = fullPath;
			string imageDir = "";
			if (imagePath[0] == 34) 
				if (imagePath.Length > 2)
					imagePath = imagePath.Substring(1,imagePath.Length - 2);
			int index = -1;
			for(int i = imagePath.Length-1 ; i >= 0 ; i--)
			{
				if (imagePath[i] == 92)
				{
					index = i;
					break;
				}			 

			}

			if (index != -1)
			{
				imageDir = imagePath.Substring(0,index);
			}
            EventLog.WriteEntry("TODO: ActiveUpTesting", "GetImagePath() returns: " + imageDir);
			return imageDir;
		}

		/// <summary>
		/// Load the config value from the config file.
		/// </summary>
		public static void LoadConfig()
		{
			try
			{
				string configFullPath = GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\" + _configFileName;

				Global.Log.WriteEvent(LogType.normal,string.Format("[GLOBAL] Loading configuration value from '{0}'",configFullPath));			
			
				TextReader reader = new StreamReader(configFullPath);
				XmlSerializer serialize = new XmlSerializer(typeof(Config));
				_config = (Config)serialize.Deserialize(reader);
				reader.Close(); 

				_config.LogFiles.MaxSizeEvent *= 1000000;
				_config.LogFiles.MaxSizeError *= 1000000;

				if (_activeMail != null)
					foreach(SmtpServer smtp in _config.SmtpServers)
					{
						if (smtp.Host.Trim() != "" && smtp.Port > 0 && smtp.Password.Trim() == "")
							_smtpServers.GetType().GetMethod("Add", new Type[] {Type.GetType("System.String"),Type.GetType("System.Int32")}).Invoke(_smtpServers,new object[] {smtp.Host,smtp.Port});
						else if (smtp.Host.Trim() != "" && smtp.Port > 0 && smtp.Password.Trim() != "")
						{
							Object server = Activator.CreateInstance(_activeCommon.GetType("ActiveUp.Net.Mail.Server",true),new object[] {smtp.Host,smtp.Port,smtp.Username,Encryption.Decrypt(smtp.Password)});
							_smtpServers.GetType().GetMethod("Add", new Type[] {_activeCommon.GetType("ActiveUp.Net.Mail.Server",true)}).Invoke(_smtpServers,new object[] {server});
						}
					}
			}

			catch(FileNotFoundException)
			{
				CreateDefaultConfig();	
				LoadConfig();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[GLOBAL] " + ex.Message);
				Global.Log.WriteError("[GLOBAL] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Create the default configuration file.
		/// </summary>
		public static void CreateDefaultConfig()
		{
			CreateDefaultConfig(_configFileName,null,true);
		}

		/// <summary>
		/// Create the default configuration file.
		/// </summary>
		/// <param name="defaultPickupDir">Default pickup dir</param>
		/// <param name="ConfigFile">The name of the config file.</param>
		/// <param name="useExecutingAssemblyPath">If the exectuting assembly path must be used.</param>
		public static void CreateDefaultConfig(string ConfigFile,string defaultPickupDir, bool useExecutingAssemblyPath)
		{
			try
			{
				Config cfg = new Config();
				cfg.Threads = 10;
				cfg.Readers.MailPickUp = 15;
				cfg.Readers.XmlPickup = 60;
				cfg.DeleteMailWhenProcessed = false;
				cfg.LogFiles.MaxSizeEvent = 0;
				cfg.LogFiles.MaxSizeError = 0;
			
				if (defaultPickupDir != null)
				{
					cfg.MailPickupDirectories.Add(defaultPickupDir);
					Directory.CreateDirectory(defaultPickupDir);
				}
				
				TextWriter writer = null;	

				if (useExecutingAssemblyPath == true)
					writer = new StreamWriter(GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\" + _configFileName);
				else
					writer = new StreamWriter(ConfigFile);
				XmlSerializer serializer = new XmlSerializer(typeof(Config));
				serializer.Serialize(writer, cfg);
				writer.Close();
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[GLOBAL:DEFAULTCONFIG] " + ex.Message);
				Global.Log.WriteError("[GLOBAL:DEFAULTCONFIG] " + ex.StackTrace);
			}
		
		}

		/// <summary>
		/// Start the library
		/// </summary>
		public static void StartActiveQ()
		{
			int i = 0;
			while (_isInitialized == false)
			{
				System.Threading.Thread.Sleep(200);
				i++;
			}
	
			//ActiveQLibrary.Form.ManageForm.Form.Visible = false;		

			_reader.StartReader();
			_processer.StartProcesser();

			for (i = 0 ; i < _processer.Dispacher.Workers.Count ; i++)
			{
				ActiveQLibrary.Form.ManageForm.AddElementProgress(new ActiveQLibrary.Form.ProgressItem(((Worker)_processer.Dispacher.Workers[i]).Name));
			}
		}

		/// <summary>
		/// Stop the library
		/// </summary>
		public static void StopActiveQ()
		{
			_reader.StopReader();
			_processer.StopProcesser();
			ManageForm.Form.Close();
			
		}

		/// <summary>
		/// Pause the library.
		/// </summary>
		public static void PauseActiveQ()
		{
			_reader.PauseReader();
			_processer.PauseProcesser();
		}

		/// <summary>
		/// Continue the library after a pause
		/// </summary>
		public static void ContinueActiveQ()
		{
			_reader.ContinueReader();
			_processer.ContinueProcesser();
		}

		/// <summary>
		/// Delete a standard mail 
		/// </summary
		/// <param name="Elem">The mail to delete</param>
		/// <returns>True if deleted successfully, otherwise, False</returns>
		public static bool DeleteStandardMail(string Elem)
		{
			SpooledObject objToDel = QueueStandard.GiveSpooledObject(Elem);
			bool flagIsDeleted = false;

			switch (objToDel.State)
			{
				case StateSpooledObject.queued:
				{
					QueueStandard.Remove(Elem);
					File.Delete(Elem);
					ActiveQLibrary.Form.ManageForm.RemoveElemStandardQueue(Elem);
					flagIsDeleted = true;
				} break;

				case StateSpooledObject.dispacher:
				{
					if (_processer.Dispacher.Delete(Elem) == true)
					{
						QueueStandard.Remove(Elem);
						File.Delete(Elem);
						ActiveQLibrary.Form.ManageForm.RemoveElemStandardQueue(Elem);
						flagIsDeleted = true;
					}
					else
						flagIsDeleted = false;

				} break;

				case StateSpooledObject.sent:
				{
					flagIsDeleted = false;
				} break;

			}

			return flagIsDeleted;
		}

		/// <summary>
		/// Delete a scheduled mail.
		/// </summary>
		/// <param name="Elem">The mail to delete</param>
		/// <returns>True if deleted successfully, otherwise, False</returns>
		public static bool DeleteScheduledMail(string Elem)
		{
			SpooledObject objToDel = QueueScheduled.GiveSpooledObject(Elem);
			bool flagIsDeleted = false;

			switch (objToDel.State)
			{
				case StateSpooledObject.queued:
				{
					QueueScheduled.Remove(Elem);
					File.Delete(Elem);
					ActiveQLibrary.Form.ManageForm.RemoveElemScheduledQueue(Elem);
					flagIsDeleted = true;
				} break;

				case StateSpooledObject.dispacher:
				{
					if (_processer.Dispacher.Delete(Elem) == true)
					{
						QueueScheduled.Remove(Elem);
						File.Delete(Elem);
						ActiveQLibrary.Form.ManageForm.RemoveElemScheduledQueue(Elem);
						flagIsDeleted = true;
					}
                    else
						flagIsDeleted = false;

				} break;

				case StateSpooledObject.sent:
				{
					flagIsDeleted = false;
				} break;

			}

			return flagIsDeleted;
		}

		/// <summary>
		/// Delete a scheduled task.
		/// </summary>
		/// <param name="File">File name</param>
		/// <param name="Id">id</param>
		/// <returns>True if deleted successfully, otherwise, False</returns>
		public static bool DeleteScheduledTask(string File, string Id)
		{
			string ElemToDel = string.Format("{0}?{1}",File,Id);
			SpooledObject objToDel = QueueScheduled.GiveSpooledObject(ElemToDel);
			bool flagIsDeleted = false;

			switch (objToDel.State)
			{
				case StateSpooledObject.queued:
				{
					QueueScheduled.Remove(ElemToDel);
					DeleteTaskInFile(File,Id);
					flagIsDeleted = true;
				} break;

				case StateSpooledObject.dispacher:
				{
					if (_processer.Dispacher.Delete(ElemToDel) == true)
					{
						QueueScheduled.Remove(ElemToDel);
						DeleteTaskInFile(File,Id);
						flagIsDeleted = true;
					}
					else
						flagIsDeleted = false;

				} break;

				case StateSpooledObject.sent:
				{
					flagIsDeleted = false;
				} break;

			}

			return flagIsDeleted;
		}

		/// <summary>
		/// Delete a task physically
		/// </summary>
		/// <param name="File">Name of file</param>
		/// <param name="Id">Id of the task</param>
		private static void DeleteTaskInFile(string File, string Id)
		{
			try
			{
				// deserialization of the task file
				TextReader reader = new StreamReader(File);
				XmlSerializer serialize = new XmlSerializer(typeof(Tasks));
				Tasks tasks = (Tasks)serialize.Deserialize(reader);
				reader.Close();

				// check and if found, deleting of the task
				for(int i = 0 ; i < tasks.TasksList.Count ; i++)
				{
					if (((Task)tasks.TasksList[i]).Id == Id)
					{
						tasks.TasksList.RemoveAt(i);
						break;
					}
				}

				// if no more task, deleting file
				if (tasks.TasksList.Count == 0)
					System.IO.File.Delete(File);
				else
				{
					// serialize the file without the task deleted
					TextWriter writer = new StreamWriter(File);
					serialize.Serialize(writer, tasks);
					writer.Close();
				}

			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[GLOBAL:DELTASK] " + ex.Message);
				Global.Log.WriteError("[GLOBAL:DELTASK] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Reload the configuration file.
		/// </summary>
		public static void ReloadConfig()
		{
			LoadConfig();
		}

		/// <summary>
		/// Give the new file name if the file already exist in the directory specified
		/// </summary>
		/// <param name="fileName">the full path file</param>
		/// <param name="Directory">the directory where we can check if the file exists</param>
		/// <returns>the new full path file</returns>
		public static string GetNewFileNamePath(string fileName, string Directory)
		{
			string pathFile = Global.GetImagePath(fileName);
			string file = fileName.Substring(pathFile.Length+1);
			string ext = file.Substring(file.Length - 3);

			bool fileExist = true;
			int i = 0;
			string baseFileName = Directory + @"\" + file.Substring(0,file.Length-4);
			string newFileName = Directory + @"\" + file;

			while (fileExist == true)
			{
				if (File.Exists(newFileName) == false)
					fileExist = false;
				else
				{
					newFileName = string.Format("{0}({1}).{2}",baseFileName,i,ext);
					i++;
				}
			}

			return newFileName;
		}

		/// <summary>
		/// Move a file to the process directory
		/// </summary>
		/// <param name="fileName">the full file path</param>
		public static void MoveFileToProcessed(string fileName)
		{
			MoveFileToProcessed(fileName,null);
		}

		/// <summary>
		/// Move a file to the process directory
		/// </summary>
		/// <param name="fileName">the full file path</param>
		/// <param name="errorLocation">the error location to indicate it in the logfile</param>
		public static void MoveFileToProcessed(string fileName,string errorLocation)
		{
			try
			{
				string newFileName = GetNewFileNamePath(fileName,PathDirProcessed);
				File.Move(fileName,newFileName);

				if (errorLocation != null)
				{
					Log.WriteEvent(LogType.normal,string.Format("[{0}] File '{1}' moved to '{2}'",errorLocation,fileName,newFileName));
				}
			}

			catch(FileNotFoundException)
			{
				Log.WriteError(string.Format("[GLOBAL:MOVEFILETOPRECESSED] File '{0}' not found, unable to move it to the process directory.",fileName));
			}

			catch(Exception ex)
			{
				if (errorLocation != null)
				{
					Global.Log.WriteError(string.Format("[{0}] {1}",errorLocation,ex.Message));
					Global.Log.WriteError(string.Format("[{0}] {1}",errorLocation,ex.StackTrace));
				}

				else
				{
					Global.Log.WriteError(string.Format("[GLOBAL:MOVEFILETOPRECESSED] {0}",ex.Message));
					Global.Log.WriteError(string.Format("[GLOBAL:MOVEFILETOPRECESSED] {0}",ex.StackTrace));
				}
			}
		}

		/// <summary>
		/// Move a file to the error directory
		/// </summary>
		/// <param name="fileName">the full file path</param>
		public static void MoveFileToError(string fileName)
		{
			MoveFileToError(fileName,null);
		}
		
		/// <summary>
		/// Move a file to the error directory
		/// </summary>
		/// <param name="fileName">the full file path</param>
		/// <param name="errorLocation">the error location to indicate it in the logfile</param>
		public static void MoveFileToError(string fileName,string errorLocation)
		{
			try
			{
				string newFileName = GetNewFileNamePath(fileName,PathDirError);
				File.Move(fileName,newFileName);

				if (errorLocation != null)
				{
					Log.WriteEvent(LogType.normal,string.Format("[{0}] File '{1}' moved to '{2}'",errorLocation,fileName,newFileName));
				}
			}

			catch(FileNotFoundException)
			{
				Log.WriteError(string.Format("[GLOBAL:MOVEFILETOERROR] File '{0}' not found, unable to move it to the error directory.",fileName));
			}

			catch(Exception ex)
			{
				if (errorLocation != null)
				{
					Global.Log.WriteError(string.Format("[{0}] {1}",errorLocation,ex.Message));
					Global.Log.WriteError(string.Format("[{0}] {1}",errorLocation,ex.StackTrace));
				}

				else
				{
					Global.Log.WriteError(string.Format("[GLOBAL:MOVEFILETOERROR] {0}",ex.Message));
					Global.Log.WriteError(string.Format("[GLOBAL:MOVEFILETOERROR] {0}",ex.StackTrace));
				}
			}
		}

		/// <summary>
		/// Callback used with the midnigth event.
		/// </summary>
		/// <param name="state"></param>
		static void MidnigthEvent(Object state)
		{
			_log.Initialize(_log.Prefix,_log.Level);

			ManageForm.Form.PMainLog.ClearLog();

			DateTime now = DateTime.Now;
			DateTime midnight = new DateTime(now.Year,now.Month,now.Day,0,0,0,0);
			midnight = midnight.AddDays(1);

			TimeSpan t = midnight - now;
			Log.WriteEvent(LogType.normal,string.Format("[INITIALIZE] Next midnigth event occurs in {0}",t));

			_timeMidnight.Change(t,new TimeSpan(-1));

		}

		#endregion
	}

	#endregion
}
