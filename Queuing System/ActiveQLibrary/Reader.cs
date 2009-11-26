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
using System.IO; 
using System.Xml.Serialization;
using ActiveQLibrary.Serialization.ConfigTask;

namespace ActiveQLibrary
{
	#region class Reader

	/// <summary>
	/// Read spool each x seconds and put valid mail in the appropriate queue.
	/// </summary>
	public class Reader : IDisposable
	{
		#region Variables

		/// <summary>
		/// Thread reader mail.
		/// </summary>
		private Thread _threadReaderMail;  

		/// <summary>
		/// Flag to indicate if the thread 'reader mail' have to run or have to be stopped.
		/// </summary>
		private bool _isRunningMail = false;

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread 'reader mail' have to run or be stopped.
		/// </summary>
		private Mutex _mutexIsRunningMail = new Mutex(false,"_mutexReaderIsRunningMail");

		/// <summary>
		/// Thread reader xml.
		/// </summary>
		private Thread _threadReaderXml;

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread 'reader xml' have to run or be stopped.
		/// </summary>
		private bool _isRunningXml = false;

		/// <summary>
		/// Mutex to protect integrity of the flag to indicate that the thread 'reader xml' have to run or be stopped.
		/// </summary>
		private Mutex _mutexIsRunningXml  = new Mutex(false,"_mutexReaderIsRunningXml");

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Reader()
		{

		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the flag to indicate if the thread 'reader mail' have to run or be stopped.
		/// </summary>
		private bool IsRunningMail
		{
			get
			{
				return _isRunningMail;
			}

			set
			{
				_mutexIsRunningMail.WaitOne();
				_isRunningMail = value;
				_mutexIsRunningMail.ReleaseMutex();
			}
		}

		/// <summary>
		/// Get / set the flag to indicate if the thread 'reader xml' have to run or be stopped.
		/// </summary>
		private bool IsRunningXml
		{
			get
			{
				return _isRunningXml;
			}

			set
			{
				_mutexIsRunningXml.WaitOne();
				_isRunningXml = value;
				_mutexIsRunningXml.ReleaseMutex();
			}
		}

		#endregion

		#region Functions

		/// <summary>
		/// Starts the reader.
		/// </summary>
		public void StartReader()
		{
			try
			{
				_threadReaderMail = new Thread(new ThreadStart(FctThreadReaderMail));
				_threadReaderMail.Name = "READER MAIL";

				_threadReaderXml = new Thread(new ThreadStart(FctThreadReaderXml));
				_threadReaderXml.Name = "READER XML";

				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Starting {0}",_threadReaderMail.Name));
				// start the thread
				_threadReaderMail.Start();
				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Starting {0}",_threadReaderXml.Name));
				_threadReaderXml.Start();

			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[READER] " + ex.Message);
				Global.Log.WriteError("[READER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Stops the reader.
		/// </summary>
		public void StopReader()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Stopping {0}",_threadReaderMail.Name));
				
				IsRunningMail = false;
		
				if ((_threadReaderMail.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
				{
					try
					{
						_threadReaderMail.Resume();
						_threadReaderMail.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[READER] " + ex.Message);
						Global.Log.WriteError("[READER] " + ex.StackTrace);
					}
					
				}
					

				// Wait the end of the thread
				_threadReaderMail.Join(10000);
				
				// If the thread is already alive, kill it
				if (_threadReaderMail.IsAlive == true)
				{
					Global.Log.WriteError(string.Format("[READER] Time-out stopping {0}, thread is aborted",_threadReaderMail.Name));
					try
					{
						_threadReaderMail.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[READER] " + ex.Message);
						Global.Log.WriteError("[READER] " + ex.StackTrace);
					}
				}

				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Stopping {0}",_threadReaderXml.Name));
				IsRunningXml = false;

				if ((_threadReaderXml.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
				{
					try
					{
						_threadReaderXml.Resume();
						_threadReaderXml.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[READER] " + ex.Message);
						Global.Log.WriteError("[READER] " + ex.StackTrace);
					}
				}
				

				// Wait the end of the thread
				_threadReaderXml.Join(10000);
				// If the thread is already alive, kill it
				if (_threadReaderXml.IsAlive == true)
				{
					Global.Log.WriteError(string.Format("[READER] Time-out stopping {0}, thread is aborted",_threadReaderXml.Name));
					try
					{
						_threadReaderXml.Abort();
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[READER] " + ex.Message);
						Global.Log.WriteError("[READER] " + ex.StackTrace);
					}

				}
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[READER] " + ex.Message);
				Global.Log.WriteError("[READER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Pauses the reader.
		/// </summary>
		public void PauseReader()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Pausing {0}",_threadReaderMail.Name));
				//Pause the reader
				_threadReaderMail.Suspend();

				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Pausing {0}",_threadReaderXml.Name));
				_threadReaderXml.Suspend();

			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[READER] " + ex.Message);
				Global.Log.WriteError("[READER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Resumes the reader
		/// </summary>
		public void ContinueReader()
		{
			try
			{
				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Resuming thread {0}",_threadReaderMail.Name));
				// resume the thread
				_threadReaderMail.Resume();
				Global.Log.WriteEvent(LogType.normal,string.Format("[READER] Resuming thread {0}",_threadReaderXml.Name));
				_threadReaderXml.Resume();

			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[READER] " + ex.Message);
				Global.Log.WriteError("[READER] " + ex.StackTrace);
			}
		}

		/// <summary>
		/// Function used in the thread reader mail
		/// </summary>
		private void FctThreadReaderMail()
		{
			IsRunningMail = true;

			while (IsRunningMail == true)
			{
				//if (Global.ActiveMailAsm == null)
                if(Global.ActiveCommonAsm == null)
				{
					IsRunningMail = false;
				}

				else
				{
					try
					{
						foreach(string dir in Global.ConfigValue.MailPickupDirectories)
						{

							Global.Log.WriteEvent(LogType.normal,string.Format("[READER:MAIL] Scanning mail directory : {0}",dir));

							DirectoryInfo dirInfo = new DirectoryInfo(dir);
							// refrash information
							dirInfo.Refresh();
							
							if (Directory.Exists(dir))
							{

								FileInfo[] listingFile = dirInfo.GetFiles();
								
								foreach(FileInfo file in listingFile)
								{
									string ext = ".EML";

									string fileName = dir + @"\" + file.Name;

									if (File.Exists(fileName) && file.Length > 4 && file.ToString().Substring(file.ToString().Length - ext.Length).ToUpper() == ext)
									{
				                        
										FileInfo fileInfo = new FileInfo(fileName);
										fileInfo.Refresh();

										if (fileInfo.Exists)
										{
											// check the validity of the message
											Object message = Activator.CreateInstance(Global.ActiveCommonAsm.GetType("ActiveUp.Net.Mail.Message",true));
											
											bool isMessageValid = true;
											try
											{
                                                message = Global.ActiveCommonAsm.GetType("ActiveUp.Net.Mail.Parser").GetMethod("ParseMessageFromFile", new Type[] { Type.GetType("System.String") }).Invoke(null, new object[] { fileName });
                                                
												if (ValidateMailMessage(message) == false)
												{
													isMessageValid = false;
												}
											}
											catch (Exception ex)
											{
                                                Console.WriteLine(ex);
												isMessageValid = false;
											}

											if (isMessageValid == false)
											{
												Global.Log.WriteError(string.Format("[READER:MAIL] '{0}' is not a valid mail message or cannot be parsed, moving it to the error directory...",fileName));
												Global.MoveFileToError(fileName,"[READER:MAIL]");
											}

											else
											{
												/*message.GetType().GetProperty("To").PropertyType.GetMethod("Clear",new Type[] {}).Invoke(message.GetType().GetProperty("To").GetValue(message,null),null);
												message.GetType().GetProperty("To").PropertyType.GetMethod("Add",new Type[] {Type.GetType("System.String")}).Invoke(message.GetType().GetProperty("To").GetValue(message,null),new Object[] {"fdn@activeup.com"});
												message.GetType().GetProperty("Subject").SetValue(message,"ACTIVEMAIL",null);*/

                                                DateTime dateMessage = DateTime.Parse(((System.Collections.Specialized.NameValueCollection)message.GetType().GetProperty("HeaderFields").GetValue(message, null))["date"]);

												if (dateMessage <= DateTime.Now)
												{
													// message to send immediately
													if (QueueScheduled.FindInQueue(fileName) == true)
														Global.Log.WriteEvent(LogType.debug,string.Format("[READER:MAIL] '{0}' already in the scheduled queue, discarding...",fileName));
													else
													{
														if (QueueStandard.FindInQueue(fileName) == false)
														{

															SpooledObject newElement = new SpooledObject(fileName,TypeSpooledObject.standardMail,message);

															Global.Log.WriteEvent(LogType.normal,string.Format("[READER:MAIL] '{0}' added in the standard queue...",fileName));
															QueueStandard.AddElement(newElement);
															ActiveQLibrary.Form.ManageForm.AddElemStandardQueue(newElement.Name);
														}
														else
															Global.Log.WriteEvent(LogType.debug,string.Format("[READER:MAIL] '{0}' already in the standard queue, discarding...",fileName));
													}
													
												}

												else
												{
													// message scheduled
													if (QueueScheduled.FindInQueue(fileName) == false)
													{

														SpooledObject newElement = new SpooledObject(fileName,TypeSpooledObject.scheduledMail,message,dateMessage);

														Global.Log.WriteEvent(LogType.normal,string.Format("[READER:MAIL] '{0}' added in the scheduled queue...",fileName));
														QueueScheduled.AddElement(newElement);
														ActiveQLibrary.Form.ManageForm.AddElementScheduledQueue(newElement.Name);
													}
													else
														Global.Log.WriteEvent(LogType.debug,string.Format("[READER:MAIL] '{0}' already in the scheduled queue, discarding...",fileName));
												}
											}
										}
									}
									else
									{
										Global.Log.WriteError(string.Format("[READER:MAIL] File '{0}' is not valid or doesn't have .eml extention, it's moved to the error directory...",file.FullName));
										Global.MoveFileToError(file.FullName,"[READER:MAIL]");
									}
										
								}
							}
							else
							{
								Global.Log.WriteError(string.Format("[READER:MAIL] Directory : '{0}' doesn't exist, discarding...",dir));
							}
						}
					}

					catch(ThreadAbortException)
					{
					}

					catch(Exception ex)
					{
						Global.Log.WriteError("[READER:MAIL] " + ex.Message);
						Global.Log.WriteError("[READER:MAIL] " + ex.StackTrace);
						if (ex.InnerException != null)
						{
							Global.Log.WriteError("[READER:MAIL] " + ex.InnerException.Message);
							Global.Log.WriteError("[READER:MAIL] " + ex.InnerException.StackTrace);
						}
						Thread.Sleep(500);
					}

					Global.Log.WriteEvent(LogType.normal,string.Format("[READER:MAIL] Next scan mail in {0} seconds",Global.ConfigValue.Readers.MailPickUp));
					Thread.Sleep(Global.ConfigValue.Readers.MailPickUp * 1000);
				}
			}
		}

		/// <summary>
		/// Function used in the thread reader xml.
		/// </summary>
		private void FctThreadReaderXml()
		{
			IsRunningMail = true;

			while (IsRunningMail == true)
			{
				try
				{

					foreach(string file in Global.ConfigValue.XmlPickupFiles)
					{
						Global.Log.WriteEvent(LogType.normal,string.Format("[READER:XML] Scanning xml file : {0}",file));

						if (File.Exists(file))
						{
							
							// deserialize xml task file
							TextReader reader = new StreamReader(file);
							XmlSerializer serialize = new XmlSerializer(typeof(Tasks));
							Tasks tasks = (Tasks)serialize.Deserialize(reader);
							reader.Close();

							DateTime now = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
                            
							for (int i = 0 ; i < tasks.TasksList.Count ; i++)
							{
								Task t = (Task)tasks.TasksList[i];
								string fileName = string.Format("{0}?{1}",file,t.Id);

								if (QueueScheduled.FindInQueue(fileName) == true)
									Global.Log.WriteEvent(LogType.debug,string.Format("[READER:XML] '{0} (id:{1})' already in the scheduled queue, discarding...",file,t.Id));
									
								else
								{
									if (now <= t.DateEnd)
									{
									
										DateTime triggerDate = t.GetNextTriggered();
										if (triggerDate != DateTime.MinValue)
										{
											SpooledObject newElement = new SpooledObject(fileName, TypeSpooledObject.scheduledTask,t,triggerDate);
											QueueScheduled.AddElement(newElement);
											ActiveQLibrary.Form.ManageForm.AddElementTaskQueue(file,string.Format("Task(id:{0})",t.Id));
											Global.Log.WriteEvent(LogType.normal,string.Format("[READER:XML] '{0} (id:{1})' added in the scheduled queue...",file,t.Id));
										}
										else
											Global.Log.WriteEvent(LogType.debug,string.Format("[READER:XML] '{0} (id:{1})' up to date, discarding...",file,t.Id));

									}
									else
										Global.Log.WriteEvent(LogType.debug,string.Format("[READER:XML] '{0} (id:{1})' up to date, discarding...",file,t.Id));
								}
							}
							
						}

						else
							Global.Log.WriteError(string.Format("[READER:XML] File : '{0}' doesn't exist, discarding...",file));

					}
				}

				catch(ThreadAbortException)
				{
				}

				catch(Exception ex)
				{
					Global.Log.WriteError("[READER:XML] " + ex.Message);
					Global.Log.WriteError("[READER:XML] " + ex.StackTrace);

					Thread.Sleep(500);
				}

				Global.Log.WriteEvent(LogType.normal,string.Format("[READER:XML] Next scan xml in {0} seconds",Global.ConfigValue.Readers.XmlPickup));
				Thread.Sleep(Global.ConfigValue.Readers.XmlPickup * 1000);
			}
		}

		/// <summary>
		/// Dispose the reader
		/// </summary>
		public void Dispose()
		{
			this.StopReader();
		}

		/// <summary>
		/// Validate a mail message
		/// </summary>
		/// <param name="message">Message to validate</param>
		/// <returns>True if correct, otherwise, False</returns>
		private bool ValidateMailMessage(Object message)
		{
			bool isValidated = true;
			string headerValue = "";

			try
			{
				if (message == null)
				{
					isValidated = false;
					return isValidated;
				}


				// check 'from' value
                headerValue = ((System.Collections.Specialized.NameValueCollection)message.GetType().GetProperty("HeaderFields").GetValue(message, null))["from"];
				if (headerValue == null)
					isValidated = false;
				else if (headerValue.Trim() == "")
					isValidated = false;

			
				/*else if ((Boolean)Global.ActiveMailAsm.GetType("ActiveUp.Mail.Validation.Validator").GetMethod("Validate",new Type[] {Type.GetType("System.String")}).Invoke(null,new object[] {headerValue}) == false)
				{
					isValidated = false;
				}*/
			
				if (isValidated == false)
					return isValidated;

				// check 'to' value
                headerValue = ((System.Collections.Specialized.NameValueCollection)message.GetType().GetProperty("HeaderFields").GetValue(message, null))["to"];
				if (headerValue == null)
					isValidated = false;
				else if (headerValue.Trim() == "")
					isValidated = false;

				/*else
					foreach(Object adr in (System.Collections.CollectionBase)message.GetType().GetProperty("To").GetValue(message,null))
					{
						if (adr != null)
							if ((Boolean)Global.ActiveMailAsm.GetType("ActiveUp.Mail.Validation.Validator").GetMethod("Validate",new Type[] {Type.GetType("System.String")}).Invoke(null,new object[] {adr.GetType().GetProperty("Email").GetValue(adr,null)}) == false)
							{
								isValidated = false;
								break;
							}
					}*/

				if (isValidated == false)
					return isValidated;
																								
				// check 'message-id' value
                /*headerValue = ((System.Collections.Specialized.NameValueCollection)message.GetType().GetProperty("HeaderFields").GetValue(message, null))["message-id"];
				if (headerValue == null)
					isValidated = false;
				else if (headerValue.Trim() == "")
					isValidated = false;

				if (isValidated == false)
					return isValidated;*/

				// check 'date' value
                headerValue = ((System.Collections.Specialized.NameValueCollection)message.GetType().GetProperty("HeaderFields").GetValue(message, null))["date"];
				if (headerValue == null)
					isValidated = false;
				else if (headerValue.Trim() == "")
					isValidated = false;
				else
				{
					try
					{
						DateTime.Parse(headerValue);
					}

					catch
					{
						isValidated = false;
					}
				}

			}

			catch(ThreadAbortException)
			{
			}

			catch(Exception ex)
			{
				Global.Log.WriteError("[VALIDATE:MAIL] " + ex.Message);
				Global.Log.WriteError("[VALIDATE:MAIL] " + ex.StackTrace);
			}

			return isValidated;
		}

		#endregion
	}

	#endregion
}
