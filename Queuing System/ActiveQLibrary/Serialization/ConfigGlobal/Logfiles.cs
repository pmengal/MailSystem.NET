using System;

namespace ActiveQLibrary.Serialization.ConfigGlobal
{
	#region class Logfiles

	/// <summary>
	/// Logfiles section in the global configuration file.
	/// </summary>
	public class Logfiles
	{
		#region Variables

		/// <summary>
		/// Number of maximum bytes for the event file.
		/// </summary>
		private int _maxSizeEvent;

		/// <summary>
		/// Number of maximum bytes for the error file.
		/// </summary>
		private int _maxSizeError;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public Logfiles()
		{
			//
			// TODO: Add constructor logic here
			//
			_maxSizeEvent = 0;
			_maxSizeError = 0;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="MaxSizeEvent">MaxSizeEvent</param>
		/// <param name="MaxSizeError">MaxSizeError</param>
		public Logfiles(int MaxSizeEvent, int MaxSizeError)
		{
			if (MaxSizeEvent < 0)
				throw new ArgumentException("Cannot be < 0","MaxSizeEvent");
			_maxSizeEvent = MaxSizeEvent;

			if (MaxSizeError < 0)
				throw new ArgumentException("Cannot be < 0","MaxSizeError");
			_maxSizeError = MaxSizeError;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the maximum number of byte for the event log.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("maxsizeevent",DataType="int")]
		public int MaxSizeEvent
		{
			get
			{
				return _maxSizeEvent;
			}

			set
			{
				if (value < 0)
					throw new ArgumentException("Cannot be < 0","value");
				_maxSizeEvent = value;
			}
		}

		/// <summary>
		/// Get / set the maximum number of byte for the error log.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("maxsizeerror",DataType="int")]
		public int MaxSizeError
		{
			get
			{
				return _maxSizeError;
			}

			set
			{
				if (value < 0)
					throw new ArgumentException("Cannot be < 0","value");
				_maxSizeError = value;
			}
		}

		#endregion

	}

	#endregion
}
