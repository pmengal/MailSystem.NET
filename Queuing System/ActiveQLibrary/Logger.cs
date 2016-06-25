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
using System.IO;
using System.Threading;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using ActiveQLibrary.Form;

namespace ActiveQLibrary
{
	#region enum
	/// <summary>
	/// Type of log, indicates the level of log
	/// </summary>
	public enum LogType
	{
		debug = 0,	// all messages : debug mode
		normal,		// normal messages : default 
		important	// only important messages
	}
	#endregion

	#region class Logger
	/// <summary>
	/// Log any event and error into a file and print it on screen (if configured).
	/// </summary>
	public class Logger
	{
		#region Variables

		/// <summary>
		/// Name of the event file.
		/// </summary>
		private string _fileNameEvent;

		/// <summary>
		/// Name of the error file.
		/// </summary>
		private string _fileNameError;

		/// <summary>
		/// Indicates if the stream using for the event is ready.
		/// </summary>
		private bool _isReadyEvent = false;

		/// <summary>
		/// Indicates if the stream using for the error is ready.
		/// </summary>
		private bool _isReadyError = false;

		/// <summary>
		/// Stream used for write in the event.
		/// </summary>
		private StreamWriter _streamEvent;

		/// <summary>
		/// Stream used for write in the error.
		/// </summary>
		private StreamWriter _streamError;

		/// <summary>
		/// Prefix for event and error file, this prefix the standard filename.
		/// </summary>
		private string _prefix = null;

		/// <summary>
		/// Log have to be appear on screen.
		/// </summary>
		private bool _logOnScreen = true;

		/// <summary>
		/// Log have to be written into a file.
		/// </summary>
		private bool _logOnFile = true;

		/// <summary>
		/// Indicate the default level log.
		/// </summary>
		private LogType _level = LogType.normal;

		/// <summary>
		/// Mutex to protect integrity of the event file.
		/// </summary>
		private Mutex _mutexFileEvent = new Mutex(false,"_mutexFileEvent");

		/// <summary>
		/// Mutex to protect integrity of the error file.
		/// </summary>
		private Mutex _mutexFileError = new Mutex(false,"_mutexFileError");

		/// <summary>
		/// Indicate the number of try when the size of the event file have been reached.
		/// </summary>
		private int _numberOfTryOverflowEvent = 20;

		/// <summary>
		/// Indicate if the size of the event file have been reached and already written in the event viewer.
		/// </summary>
		private bool _isOverflowEventWritten = false;

		/// <summary>
		/// Indicate if the size of the error file have been reached and already written in the event viewer.
		/// </summary>
		private bool _isOverflowErrorWritten = false;

		/// <summary>
		/// Event logger.
		/// </summary>
		private EventLog _eventLog = new EventLog();

		/// <summary>
		/// The file extension of the log file.
		/// </summary>
		private string _extension = ".log";

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public Logger()
		{
			Initialize();
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Prefix">Prefix of log files</param>
		public Logger(string Prefix)
		{
			Initialize(Prefix);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="LogLevel">Default level of log</param>
		public Logger(LogType LogLevel)
		{
			Initialize(LogLevel);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Prefix">Prefix of log files</param>
		/// <param name="LogLevel">Default level of log</param>
		public Logger(string Prefix, LogType LogLevel)
		{
			Initialize(Prefix,LogLevel);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get the name of the event file.
		/// </summary>
		public string FileNameEvent
		{
			get
			{
				return _fileNameEvent;
			}
		}

		/// <summary>
		/// Get the name of the error file.
		/// </summary>
		public string FileNameError
		{
			get
			{
				return _fileNameError;
			}
		}

		/// <summary>
		/// Get/set the flag to indicates if the messages have to be written on file.
		/// </summary>
		public bool LogOnFile
		{
			get
			{
				return _logOnFile;
			}

			set
			{
				_logOnFile = value;
			}
		}

		/// <summary>
		/// Get/set the flag to indicates if the messages have to be written on screen.
		/// </summary>
		public bool LogOnScreen
		{
			get
			{
				return _logOnScreen;
			}

			set
			{
				_logOnScreen = value;
			}
		}

		/// <summary>
		/// Get/set the default level of log.
		/// </summary>
		public LogType Level
		{
			get
			{
				return _level;
			}

			set
			{
				_level = value;
			}
		}

		/// <summary>
		/// Get/set the prefix of each log files.
		/// </summary>
		public string Prefix
		{
			get
			{
				return _prefix;
			}

			set
			{
				_prefix = value;
			}
		}

		#endregion

		#region Functions

		/// <summary>
		/// Initializes all the parameters for the logfiles.
		///		. Creating of directories
		///		. Creating files (format : <prefix>Events.<date yyyyMMdd>)
		/// </summary>
		public void Initialize()
		{
			Initialize(null,LogType.normal);
		}

		/// <summary>
		/// Initializes all the parameters for the logfiles.
		///		. Creating of directories
		///		. Creating files (format : <prefix>Events.<date yyyyMMdd>)
		/// </summary>
		/// <param name="Prefix">Prefix of log files</param>
		public void Initialize(string Prefix)
		{
			Initialize(Prefix,LogType.normal);
		}

		/// <summary>
		/// Initializes all the parameters for the logfiles.
		///		. Creating of directories
		///		. Creating files (format : <prefix>Events.<date yyyyMMdd>)
		/// </summary>
		/// <param name="LogLevel">Default level of log</param>
		public void Initialize(LogType LogLevel)
		{
			Initialize(null,LogLevel);
		}

		/// <summary>
		/// Initializes all the parameters for the logfiles.
		///		. Creating of directories
		///		. Creating files (format : <prefix>Events.<date yyyyMMdd>)
		/// </summary>
		/// <param name="Prefix">Prefix of log files.</param>
		/// <param name="LogLevel">Default level of log</param>
		public void Initialize(string Prefix, LogType LogLevel)
		{
			_eventLog.Source = "ActiveQ";

			_prefix = Prefix;

			string path = Global.GetImagePath(Assembly.GetExecutingAssembly().Location);

			// Create file name
			if (_prefix == null)
			{
				_fileNameEvent = path + @"\logfiles\" + "Events" + DateTime.Now.ToString("yyyyMMdd") + _extension; 
				_fileNameError = path + @"\logfiles\" + "Errors" + DateTime.Now.ToString("yyyyMMdd") + _extension; 
			}

			else
			{
				_fileNameEvent = path + @"\logfiles\" + _prefix + "Events" + DateTime.Now.ToString("yyyyMMdd") + _extension; 
				_fileNameError = path + @"\logfiles\" + _prefix + "Errors" + DateTime.Now.ToString("yyyyMMdd") + _extension; 

			}

			// Create the logfiles directory
			Directory.CreateDirectory(path + @"\Logfiles");

			if (File.Exists(_fileNameEvent) == false)
				WriteEvent(LogType.important,string.Format("Creating event logfile {0}",DateTime.Now.ToString()));

			if (File.Exists(_fileNameError) == false)
				WriteError(string.Format("Creating error logfile {0}",DateTime.Now.ToString()));

			_level = LogLevel;

		}

		/// <summary>
		/// Open the event file and initilizes the stream.
		/// </summary>
		private void OpenFileEvent()
		{
			try
			{
				_streamEvent = File.AppendText(_fileNameEvent);
				_isReadyEvent = true;
			}

			catch
			{
				_isReadyEvent = false;
				Console.WriteLine("Unable to open {0} file",_fileNameEvent);
			}
		}

		/// <summary>
		/// Close the event file and the stream.
		/// </summary>
		private void CloseFileEvent()
		{
			if(_isReadyEvent) 
			{
				try 
				{
					_streamEvent.Close();
				} 
				catch 
				{
					Console.WriteLine("Unable to close {0} file",_fileNameEvent);
				}
			}
		}

		/// <summary>
		/// Write event into file and on screen if configured.
		/// This method uses the default level of log.
		/// </summary>
		/// <param name="Message">Message to write</param>
		public void WriteEvent(string Message)
		{
			WriteEvent(_level,Message);
		}

		/// <summary>
		/// Write event into file and on screen if configured.
		/// </summary>
		/// <param name="Type">Level of log</param>
		/// <param name="Message">Message to write</param>
		public void WriteEvent(LogType Type, string Message)
		{
			_mutexFileEvent.WaitOne();

			// add additional information to the message
			string CompleteMessage = DateTime.Now.ToString("[dd-MM-yyyy @ HH:mm:ss] ");
			CompleteMessage += Message;

			if (LogOnFile == true)
			{
				if (_level <= Type)
				{
					ManageForm.AddElementInMainLog(CompleteMessage);

					if (File.Exists(_fileNameEvent) == true)
					{
						FileInfo fileInfo = new FileInfo(_fileNameEvent);
						
						if (Global.ConfigValue.LogFiles.MaxSizeEvent == 0)
						{
							OpenFileEvent();
							if (_isReadyEvent)
									_streamEvent.WriteLine(CompleteMessage);
							else
								WriteError(string.Format("Unable to write in {0} file",_fileNameEvent));
							CloseFileEvent();
						}

						else if (fileInfo.Length < Global.ConfigValue.LogFiles.MaxSizeEvent)
						{

							OpenFileEvent();
							if (_isReadyEvent)
								if (fileInfo.Length + CompleteMessage.Length < Global.ConfigValue.LogFiles.MaxSizeEvent)
									_streamEvent.WriteLine(CompleteMessage);
								else
									_streamEvent.WriteLine(CompleteMessage.Substring(0,(int)(Global.ConfigValue.LogFiles.MaxSizeEvent - (int)fileInfo.Length)));
							else
								WriteError(string.Format("Unable to write in {0} file",_fileNameEvent));
							CloseFileEvent();
						}
						else
						{
							if (_isOverflowEventWritten == false)
							{
								_eventLog.WriteEntry(string.Format("Unable to write in the event logfile '{0}'.\nThe maximum size ({1}) has been reached.",_fileNameEvent,Global.ConfigValue.LogFiles.MaxSizeEvent),EventLogEntryType.Error);
								_isOverflowEventWritten = true;
							}

							if (_numberOfTryOverflowEvent >= 20)
							{
								WriteError(string.Format("Unable to write in the event file '{0}', the maximum size({1}) has been reached.",_fileNameEvent,Global.ConfigValue.LogFiles.MaxSizeEvent));
								_numberOfTryOverflowEvent = 0;
							}
							_numberOfTryOverflowEvent++;
						}
					}
					else
					{
						OpenFileEvent();
						if (_isReadyEvent)
							_streamEvent.WriteLine(CompleteMessage);
						else
							WriteError(string.Format("Unable to write in {0} file",_fileNameEvent));
						CloseFileEvent();
					}

				}
			}

			if (LogOnScreen == true)
			{
				if (_level <= Type)
					Console.WriteLine(CompleteMessage);
			}

			_mutexFileEvent.ReleaseMutex();
		}

		/// <summary>
		/// Open the error file and initilizes the stream.
		/// </summary>
		private void OpenFileError()
		{
			try
			{
				_streamError = File.AppendText(_fileNameError);
				_isReadyError = true;
			}

			catch
			{
				_isReadyError = false;
				Console.WriteLine("Unable to open {0} file",_fileNameError);
			}
		}

		/// <summary>
		/// Close the error file and the stream.
		/// </summary>
		private void CloseFileError()
		{
			if(_isReadyError) 
			{
				try 
				{
					_streamError.Close();
				} 
				catch 
				{
					Console.WriteLine("Unable to close {0} file",_fileNameError);
				}
			}
		}

		/// <summary>
		/// Write error into file and on screen if configured.
		/// </summary>
		/// <param name="Message">Message to write</param>
		public void WriteError(string Message)
		{
			_mutexFileError.WaitOne();
		
			// add additional information to the message
			string CompleteMessage = DateTime.Now.ToString("[dd-MM-yyyy @ HH:mm:ss] ");
			CompleteMessage += Message;

			if (LogOnFile == true)
			{
				ManageForm.AddElementInMainLog(CompleteMessage);

				if (File.Exists(_fileNameError) == true)
				{
					FileInfo fileInfo = new FileInfo(_fileNameError);
						
					if (Global.ConfigValue.LogFiles.MaxSizeError == 0)
					{
						OpenFileError();
						if (_isReadyError)
							_streamError.WriteLine(CompleteMessage);
						else
							WriteError(string.Format("Unable to write in {0} file",_fileNameError));
						CloseFileError();
					}

					else if (fileInfo.Length < Global.ConfigValue.LogFiles.MaxSizeError)
					{

						OpenFileError();
						if (_isReadyError)
							if (fileInfo.Length + CompleteMessage.Length < Global.ConfigValue.LogFiles.MaxSizeError)
								_streamError.WriteLine(CompleteMessage);
							else
								_streamError.WriteLine(CompleteMessage.Substring(0,(int)(Global.ConfigValue.LogFiles.MaxSizeError - (int)fileInfo.Length)));
						else
							WriteError(string.Format("Unable to write in {0} file",_fileNameError));
						CloseFileError();
					}
					else
					{
						if (_isOverflowErrorWritten == false)
						{
							_eventLog.WriteEntry(string.Format("Unable to write in the error logfile '{0}'.\nThe maximum size ({1}) has been reached.",_fileNameError,Global.ConfigValue.LogFiles.MaxSizeError),EventLogEntryType.Error);
							_isOverflowErrorWritten = true;
						}
					}
				}
				else
				{
					OpenFileError();
					if (_isReadyError)
						_streamError.WriteLine(CompleteMessage);
					else
						WriteError(string.Format("Unable to write in {0} file",_fileNameError));
					CloseFileError();
				}
			}

			if (LogOnScreen == true)
			{
				Console.WriteLine(CompleteMessage);
			}

			_mutexFileError.ReleaseMutex();
		}

		#endregion
	}

	#endregion
}


