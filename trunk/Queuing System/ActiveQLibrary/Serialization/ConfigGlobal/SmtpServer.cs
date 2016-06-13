using System;

namespace ActiveQLibrary.Serialization.ConfigGlobal
{
	#region class SmtpServer

	/// <summary>
	/// Smtp server element in the configuration file.
	/// </summary>
	public class SmtpServer
	{
		#region Variables

		/// <summary>
		/// Server host.
		/// </summary>
		private string _host;

		/// <summary>
		/// Port, by default is 25.
		/// </summary>
		private int _port;

		/// <summary>
		/// Username using in case of authentification.
		/// </summary>
		private string _username;

		/// <summary>
		/// Password using in case of authentification.
		/// </summary>
		private string _password;

		#endregion

		#region Constructors

		public SmtpServer()
		{
			_host = "";
			_port = 25;
			_username = "";
			_password = "";
		}

		public SmtpServer(string host)
		{
			_host = host;
			_port = 25;
			_username = "";
			_password = "";
		}

		public SmtpServer(string host, int port)
		{
			_host = host;

			if (port < 0)
				throw new ArgumentException("Cannot be < 0","port");

			_port = port;

			_username = "";
			_password = "";
		}

		public SmtpServer(string host, int port, string username, string password)
		{
			_host = host;
			_port = port;
			_username = username;
			_password = password;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the host.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("host",DataType="string")]
		public string Host
		{
			get
			{
				return _host;
			}

			set
			{
				_host = value;
			}
		}

		/// <summary>
		/// Get / set the port.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("port",DataType="int")]
		public int Port
		{
			get
			{
				return _port;
			}

			set
			{
				_port = value;
			}
		}

		/// <summary>
		/// Get / set the username
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("username",DataType="string")]
		public string Username
		{
			get
			{
				return _username;
			}

			set
			{
				_username = value;
			}

		}

		/// <summary>
		/// Get / set the password.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("password",DataType="string")]
		public string Password
		{
			get
			{
				return _password;
			}

			set
			{
				_password = value;
			}
		}

		#endregion

	}

	#endregion
}
