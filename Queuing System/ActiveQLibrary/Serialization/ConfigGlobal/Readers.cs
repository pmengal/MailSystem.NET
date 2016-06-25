using System;

namespace ActiveQLibrary.Serialization.ConfigGlobal
{
	#region class Readers

	/// <summary>
	/// Readers section in the global configuration file.
	/// </summary>
	public class Readers
	{
		#region Variables

		/// <summary>
		/// Number of seconds between each scan of mail file.
		/// </summary>
		private int _mailPickup;

		/// <summary>
		/// Number of seconds between each scan of xml file.
		/// </summary>
		private int _xmlPickup;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public Readers()
		{
			//
			// TODO: Add constructor logic here
			//
			_mailPickup = 0;
			_xmlPickup = 0;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="MailPickup">MailPickup</param>
		/// <param name="XmlPickup">XmlPickup</param>
		public Readers(int MailPickup, int XmlPickup)
		{
			if (MailPickUp < 0)
				throw new ArgumentException("Cannot be < 0","MailPickup");
			_mailPickup = MailPickUp;

			if (XmlPickup < 0)
				throw new ArgumentException("Cannot be < 0","XmlPickup");
			_xmlPickup = XmlPickup;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the number of seconds between each scan mail file.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("mailpickup",DataType="int")]
		public int MailPickUp
		{
			get
			{
				return _mailPickup;
			}

			set
			{
				if (value < 0)
					throw new ArgumentException("Cannot be < 0","value");
				_mailPickup = value;
			}
		}

		/// <summary>
		/// Get / set the number of seconds between each scan xml file.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("xmlpickup",DataType="int")]
		public int XmlPickup
		{
			get
			{
				return _xmlPickup;
			}

			set
			{
				if (value < 0)
					throw new ArgumentException("Cannot be < 0","value");
				_xmlPickup = value;
			}
		}

		#endregion

	}

	#endregion
}
