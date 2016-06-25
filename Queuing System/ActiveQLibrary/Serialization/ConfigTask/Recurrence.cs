using System;
using System.Xml.Serialization;

namespace ActiveQLibrary.Serialization.ConfigTask
{
	#region enum

	/// <summary>
	/// Type of recurrence
	/// </summary>
	public enum TypeRecurrence
	{
		once = 0,
		daily,
		selectedDay,
		selectedDate
	}

	#endregion

	#region class Recurrence

	/// <summary>
	/// Indicate the recurrence for each tasks.
	/// </summary>
	public class Recurrence
	{

		#region Variables

		/// <summary>
		/// Type.
		/// </summary>
		private TypeRecurrence _type;	 	

		/// <summary>
		/// Parameter, can be optional.
		/// </summary>
		private string _parameter;

		/// <summary>
		/// Hour.
		/// </summary>
		private DateTime _hour;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Recurrence()
		{
			_type = TypeRecurrence.once;
			_parameter = null;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Type">Type</param>
		public Recurrence(TypeRecurrence Type)
		{
			_type = Type;
			_parameter = null;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Type">Type</param>
		/// <param name="Parameter">Parameter</param>
		public Recurrence(TypeRecurrence Type, string Parameter)
		{
			_type = Type;
			_parameter = Parameter;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Type">Type</param>
		/// <param name="Parameter">Parameter</param>
		/// <param name="Hour">Hour</param>
		public Recurrence(TypeRecurrence Type, string Parameter, DateTime Hour)
		{
			_type = Type;
			_parameter = Parameter;
			_hour = Hour;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the type.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("type")]		
		public TypeRecurrence Type
		{
			get
			{
				return _type;
			}

			set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Get / set the parameter.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("parameter")]
		public string Parameter
		{
			get
			{
				return _parameter;
			}

			set
			{
				_parameter = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("hour",DataType="time")]
		public DateTime Hour
		{
			get
			{
				return _hour;
			}

			set
			{
				_hour = value;
			}
		}

		#endregion
	}

	#endregion
}
