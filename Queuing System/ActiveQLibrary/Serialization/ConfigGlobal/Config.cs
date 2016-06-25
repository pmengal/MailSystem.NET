using System;
using System.Collections;

namespace ActiveQLibrary.Serialization.ConfigGlobal
{
	#region class Config

	/// <summary>
	/// Global configuration file.
	/// </summary>
	/// 
	[System.Xml.Serialization.XmlRootAttribute("config", IsNullable=false)]
	public class Config
	{

		#region Variables

		/// <summary>
		/// Number of worker threads.
		/// </summary>
		private int _threads;

		/// <summary>
		/// Flag to indicate if a mail have to be deleted when it's processed.
		/// </summary>
		private bool _deleteMailWhenProcessed = false;

		/// <summary>
		/// Readers parameters, contening number of seconds between each scan.
		/// </summary>
		private Readers _readers = new Readers();

		/// <summary>
		/// Logfiles parameters, contening the maximum of byte for each file.
		/// </summary>
		private Logfiles _logfiles = new Logfiles();

		/// <summary>
		/// List of directories have to be scanned by the reader.
		/// </summary>
		private ArrayList _mailPickupDirectories = new ArrayList();

		/// <summary>
		/// List of files have to be scanned by the reader.
		/// </summary>
		private ArrayList _xmlPickupFiles = new ArrayList();

		/// <summary>
		/// List of smtp servers.
		/// </summary>
		private ArrayList _smtpServers = new ArrayList();

		/// <summary>
		/// The ActiveMail license contents
		/// </summary>
		private string _activeMailLicense = string.Empty;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Config()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the number of worker threads.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("threads",DataType="int")]
		public int Threads
		{
			get
			{
				return _threads;
			}

			set
			{
				_threads = value;
			}
		}

		/// <summary>
		/// Get / set the flag to indicate if a mail have to be deleted when it's processed.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("deletemailwhenprocessed",DataType="boolean")]
		public bool DeleteMailWhenProcessed
		{
			get
			{
				return _deleteMailWhenProcessed;
			}

			set
			{
				_deleteMailWhenProcessed = value;
			}
		}

		/// <summary>
		/// Get / set the contents of the license file of active mail. 
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("activemaillicense",DataType="string")]
		public string ActiveMailLicense
		{
			get
			{
				return _activeMailLicense;
			}

			set
			{
				_activeMailLicense = value;
			}
		}

		/// <summary>
		/// Get / set the parameters, contening number of seconds between each scan.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("logfiles")]
		public Logfiles LogFiles
		{
			get
			{
				return _logfiles;
			}

			set
			{
				_logfiles = value;
			}
		}

		/// <summary>
		/// Get / set the parameters, contening number of seconds between each scan.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("readers")]
		public Readers Readers
		{
			get
			{
				return _readers;
			}

			set
			{
				_readers = value;
			}
		}

		/// <summary>
		/// Get / set the list of directories to be scanned by the reader.
		/// </summary>
		[System.Xml.Serialization.XmlArray("mailpickupdirectories")]
		[System.Xml.Serialization.XmlArrayItem("dir",typeof(string))]
		public ArrayList MailPickupDirectories
		{
			get
			{
				return _mailPickupDirectories;
			}

			set
			{
				_mailPickupDirectories = value;
			}
		}

		/// <summary>
		/// Get / set the list of files to be scanned by the reader.
		/// </summary>
		[System.Xml.Serialization.XmlArray("xmlpickupfiles")]
		[System.Xml.Serialization.XmlArrayItem("file",typeof(string))]
		public ArrayList XmlPickupFiles
		{
			get
			{
				return _xmlPickupFiles;
			}

			set
			{
				_xmlPickupFiles = value;
			}
		}

		/// <summary>
		/// Get / set the list of smtp servers.
		/// </summary>
		[System.Xml.Serialization.XmlArray("smtpservers")]
		[System.Xml.Serialization.XmlArrayItem("server",typeof(SmtpServer))]
		public ArrayList SmtpServers
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

		#endregion

	}

	#endregion
}
