using System;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace ActiveQConfiguration
{
	public enum StateLibraryActiveMail
	{
		notfound = 0,
		expired,
		ok
	};		

	/// <summary>
	/// Description résumée de Global.
	/// </summary>
	public class Global
	{
		#region Variables

        /// <summary>
        /// Assembly from active common.
        /// </summary>
        private static Assembly _activeCommon = null;

        /// <summary>
        /// Assembly from active smtp.
        /// </summary>
        private static Assembly _activeSmtp = null;
        
        /// <summary>
		/// Assembly for using Active mail.
		/// </summary>
		private static Assembly _activeMail = null;

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
		/// State of the library.
		/// </summary>
		private static StateLibraryActiveMail _stateActiveMail;

		#endregion

		#region Constructors

		public Global()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		#endregion

		#region Properties

		public static StateLibraryActiveMail StateActiveMail
		{
			get
			{
				return _stateActiveMail;
			}
		}

		public static Assembly ActiveMail
		{
			get
			{
				return _activeMail;
			}
		}

		#endregion

		#region Functions

		public static void Initialize()
		{
			string imageDir = ActiveQLibrary.Global.GetImagePath(Assembly.GetExecutingAssembly().Location);

            if (imageDir.Trim() != "")
            {
                _libraryCommonFile = Path.Combine(Path.GetDirectoryName(imageDir), _libraryCommonFile);
                _libraryMailFile = Path.Combine(Path.GetDirectoryName(imageDir), _libraryMailFile);
                _librarySmtpFile = Path.Combine(Path.GetDirectoryName(imageDir), _librarySmtpFile);
            }

			try
			{
				_activeMail = Assembly.LoadFrom(_libraryMailFile);
                _activeCommon = Assembly.LoadFrom(_libraryCommonFile);
                _activeSmtp = Assembly.LoadFrom(_librarySmtpFile);
				
				object message = Activator.CreateInstance(_activeCommon.GetType("ActiveUp.Net.Mail.Message",true));
				message = null;

				_stateActiveMail = StateLibraryActiveMail.ok;

			}

			catch (FileNotFoundException)
			{
				_stateActiveMail = StateLibraryActiveMail.notfound;
			}

			catch (Exception ex)
			{
				/*Type trialExpired = _activeMail.GetType("ActiveUp.Mail.Common.TrialExpiredException",true);
				if (ex.InnerException.GetType() == trialExpired)
				{
					_stateActiveMail = StateLibraryActiveMail.expired;
					_activeMail = null;
				}

				else
				{
					MessageBox.Show(ex.Message,"Error loading ActiveUp.Net.Mail.dll",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}*/
                MessageBox.Show(ex.Message, "Error loading ActiveUp.Net.Mail.dll", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static void TestSmtpServer(string server, int port, string username, string password)
		{
			if (_stateActiveMail == StateLibraryActiveMail.notfound)
			{
				PageActiveMailNotFound activeMailNotFoundDlg = new PageActiveMailNotFound();
				activeMailNotFoundDlg.ShowDialog();
				return;
			}

			try
			{
				Object smtpClient = Activator.CreateInstance(_activeSmtp.GetType("ActiveUp.Net.Mail.SmtpClient",true));	
				smtpClient.GetType().GetMethod("Connect", new Type[] {Type.GetType("System.String"), Type.GetType("System.Int32")}).Invoke(smtpClient,new object[] {server,port});
				
				if (username.Trim() != "")
				{
					try
					{
						smtpClient.GetType().GetMethod("Ehlo",new Type[] {Type.GetType("System.String")}).Invoke(smtpClient,new object[] {""});
					}

					catch
					{
						smtpClient.GetType().GetMethod("Helo",new Type[] {Type.GetType("System.String")}).Invoke(smtpClient,new object[] {""});
					}

					object SASLmechanism = (Enum)(_activeCommon.CreateInstance("ActiveUp.Net.Mail.SaslMechanism"));
					SASLmechanism = (Enum)(Enum.Parse(SASLmechanism.GetType(),"Login"));
					smtpClient.GetType().GetMethod("Authenticate", new Type[] {Type.GetType("System.String"),Type.GetType("System.String"),_activeSmtp.GetType("ActiveUp.Net.Mail.SaslMechanism",true)}).Invoke(smtpClient,new object[] {username,password,SASLmechanism});

				}

				smtpClient.GetType().GetMethod("Disconnect", new Type[] {}).Invoke(smtpClient,null);

				MessageBox.Show("All the parameters of the smtp server works","Test successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}

			catch(Exception ex)
			{
				if (ex.InnerException != null)
				MessageBox.Show(ex.InnerException.Message,"Test failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
				else
				MessageBox.Show(ex.Message,"Test failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}	

		#endregion
	}
}
