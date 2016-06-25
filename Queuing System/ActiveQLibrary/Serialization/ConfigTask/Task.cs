using System;
using System.Xml.Serialization;
using System.Collections;
using System.Text.RegularExpressions;

namespace ActiveQLibrary.Serialization.ConfigTask
{

	#region class Task

	/// <summary>
	/// Information about a task.
	/// </summary>
	[System.Xml.Serialization.XmlTypeAttribute()]
	public class Task
	{

		#region Variables

		/// <summary>
		/// Id of the task.
		/// </summary>
		private string _id	;

		/// <summary>
		/// Adress of the task to do.
		/// </summary>
		private string _address;

		/// <summary>
		/// Method, can be only 'post' or 'get'.
		/// </summary>
		private string _method;

		/// <summary>
		/// Date when the task start.
		/// </summary>
		private DateTime _dateStart;

		/// <summary>
		/// Date when the task end.
		/// </summary>
		private DateTime _dateEnd;
		
		/// <summary>
		/// Recurrences of the task.
		/// </summary>
		private ArrayList _recurrences = new ArrayList();

		/// <summary>
		/// Regular expression for internet address.
		/// </summary>
		//private string _expressionInternetAddress = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public Task()
		{
			_id = "";
			_dateStart = DateTime.Now;
			_dateEnd = DateTime.Now;
			_method = "post";
		}

		/// <summary>
		/// Constructors
		/// </summary>
		/// <param name="Id">Id</param>
		/// <param name="Address">Address</param>
		/// <param name="Method">Method</param>
		public Task(string Id, string Address, string Method)
		{
			if (Id == null)
				throw new ArgumentNullException("Id");

			_id = Id;

			if (Address == null)
				throw new ArgumentNullException("Address");

			/* TO DO if (Address.Trim() != "")
				if (Regex.IsMatch(Address,_expressionInternetAddress) == false)
					throw new ArgumentException("Invalid internet address format","Address");*/
		
			_address = Address;

			if (Method == null)
				throw new ArgumentNullException("Method");

			if (Method.Trim() == "")
				throw new ArgumentException("Cannot be blank","Method");

			if (Method != "post" && Method != "get")
				throw new ArgumentException("Only 'post' and 'get' are accepted as valid values","Method");

			_method = Method;

			_dateStart = DateTime.Now;
			_dateEnd = DateTime.Now;

		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Id">Id</param>
		/// <param name="Address">Address</param>
		/// <param name="Method">Method</param>
		/// <param name="DateStart">DateStart</param>
		public Task(string Id, string Address, string Method, DateTime DateStart)
		{
			if (Id == null)
				throw new ArgumentNullException("Id");

			_id = Id;

			if (Address == null)
				throw new ArgumentNullException("Address");

			/* TO DO if (Address.Trim() != "")
				if (Regex.IsMatch(Address,_expressionInternetAddress) == false)
					throw new ArgumentException("Invalid internet address format","Address"); */
		
			_address = Address;

			if (Method == null)
				throw new ArgumentNullException("Method");

			if (Method.Trim() == "")
				throw new ArgumentException("Cannot be blank","Method");

			if (Method != "post" && Method != "get")
				throw new ArgumentException("Only 'post' and 'get' are accepted as valid values","Method");

			_method = Method;

			_dateStart = DateStart; 
			_dateEnd = DateEnd;

		}

		/// <summary>
		/// Constructor/
		/// </summary>
		/// <param name="Id">Id</param>
		/// <param name="Address">Address</param>
		/// <param name="Method">Method</param>
		/// <param name="DateStart">DateStart</param>
		/// <param name="DateEnd">DateEnd</param>
		public Task(string Id,string Address, string Method, DateTime DateStart, DateTime DateEnd)
		{
			if (Id == null)
				throw new ArgumentNullException("Id");

			_id = Id;

			if (Address == null)
				throw new ArgumentNullException("Address");

			/* TO DO if (Address.Trim() != "")
				if (Regex.IsMatch(Address,_expressionInternetAddress) == false)
					throw new ArgumentException("Invalid internet address format","Address"); */
		
			_address = Address;

			if (Method == null)
				throw new ArgumentNullException("Method");

			if (Method.Trim() == "")
				throw new ArgumentException("Cannot be blank","Method");

			if (Method != "post" && Method != "get")
				throw new ArgumentException("Only 'post' and 'get' are accepted as valid values","Method");

			_method = Method;

			_dateStart = DateStart;

			if (_dateEnd < DateTime.Now)
				throw new ArgumentException("Cannot be less than the DateStart","DateEnd");

			_dateEnd = DateEnd;

		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Id">Id</param>
		/// <param name="Address">Address</param>
		/// <param name="Method">Method</param>
		/// <param name="DateStart">DateStart</param>
		/// <param name="DateEnd">DateEnd</param>
		/// <param name="Recurrences">Recurrences</param>
		public Task(string Id,string Address, string Method, DateTime DateStart, DateTime DateEnd, ArrayList Recurrences)
		{
			if (Id == null)
				throw new ArgumentNullException("Id");
			_id = Id;

			if (Address == null)
				throw new ArgumentNullException("Address");

			/* TO DO if (Address.Trim() != "")
				if (Regex.IsMatch(Address,_expressionInternetAddress) == false)
					throw new ArgumentException("Invalid internet address format","Address");*/
		
			_address = Address;

			if (Method == null)
				throw new ArgumentNullException("Method");

			if (Method.Trim() == "")
				throw new ArgumentException("Cannot be blank","Method");

			if (Method != "post" && Method != "get")
				throw new ArgumentException("Only 'post' and 'get' are accepted as valid values","Method");

			_method = Method;

			_dateStart = DateStart;

			if (_dateEnd < DateTime.Now)
				throw new ArgumentException("Cannot be less than the DateStart","DateEnd");

			_dateEnd = DateEnd;

			_recurrences = Recurrences;

		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the method.
		/// Be careful : only 'post' and 'get' are accepted as valid values.
		/// </summary>
		[System.Xml.Serialization.XmlAttributeAttribute ("method")]
		public string Method
		{
			get
			{
				return _method;
			}

			set
			{
				if (value == null)
					throw new ArgumentNullException("Method");

				if (value.Trim() == "")
					throw new ArgumentException("Cannot be blank","Method");

				if (value != "post" && value != "get" && value != "file")
					throw new ArgumentException("Only 'post', 'get' and 'file' are accepted as valid values","Method");

				_method = value;
			}
		}

		/// <summary>
		/// Get / set the id.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("id", DataType="string")]		
		public string Id
		{
			get
			{
				return _id;
			}

			set
			{
				if (value == null)
					throw new ArgumentNullException("Id");

				_id = value;
			}
		}	

		/// <summary>
		/// Get / set the address.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("address", DataType="string")]		
		public string Address
		{
			get
			{
				return _address;
			}

			set
			{
				if (value == null)
					throw new ArgumentNullException("Address");

				/* TO DO if (value.Trim() != "")
					if (Regex.IsMatch(value,_expressionInternetAddress) == false)
						throw new ArgumentException("Invalid internet address format","Address");*/

				_address = value;
			}
		}

		/// <summary>
		/// Get / set the DateStart.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("datestart", DataType="date")]		
		public DateTime DateStart
		{
		
			get
			{ 
				return _dateStart; 
			}

			set 
			{ 
				_dateStart = value; 
			}
		}

		/// <summary>
		/// Get / set the DateEnd.
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("dateend", DataType="date")]		
		public DateTime DateEnd
		{
		
			get
			{ 
				return _dateEnd; 
			}

			set 
			{ 
				_dateEnd = value; 
			}
		}

		/// <summary>
		/// Get / set the list of Recurrence object.
		/// </summary>
		[System.Xml.Serialization.XmlArray("recurrences")]
		[System.Xml.Serialization.XmlArrayItem("recurrence",typeof(Recurrence))]
		public ArrayList RecurrenceList
		{
			get
			{
				return _recurrences;
			}

			set
			{
				_recurrences = value;
			}
		}

		#endregion

		#region Functions

		/// <summary>
		/// Add a Recurrence object to the list of Recurrence.
		/// </summary>
		/// <param name="Recur">Recurrence to add</param>
		public void AddRecurrence(Recurrence Recur)
		{
			_recurrences.Add(Recur);
		}

		/// <summary>
		/// Get the next triggerded date
		/// </summary>
		/// <returns>DateTime contening the next trigger</returns>
		public DateTime GetNextTriggered()
		{
			DateTime dateTriggered = DateTime.MinValue;

			for (int i = 0 ; i < _recurrences.Count ; i++)
			{
				DateTime dateRecurrence = DateTime.MinValue;
				Recurrence r = (Recurrence)_recurrences[i]; 

				switch (r.Type)
				{
					case TypeRecurrence.daily:
					{
						dateRecurrence = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,r.Hour.Hour,r.Hour.Minute,r.Hour.Second,r.Hour.Millisecond);

						if (dateRecurrence < DateTime.Now)
							dateRecurrence = dateRecurrence.AddDays(1);

					} break;

					case TypeRecurrence.selectedDay:
					{
						dateRecurrence = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,r.Hour.Hour,r.Hour.Minute,r.Hour.Second,r.Hour.Millisecond);
						DayOfWeek dayParameter;
						
						switch (r.Parameter)
						{
							case "Sunday":
							{
								dayParameter = DayOfWeek.Sunday;
							} break;

							case "Monday":
							{
								dayParameter = DayOfWeek.Monday;
							} break;

							case "Tuesday":
							{
								dayParameter = DayOfWeek.Thursday;
							} break;

							case "Wednesday":
							{
								dayParameter = DayOfWeek.Wednesday;
							} break;

							case "Thursday":
							{
								dayParameter = DayOfWeek.Thursday;
							} break;

							case "Friday":
							{
								dayParameter = DayOfWeek.Friday;
							} break;

							case "Saturday":
							{
								dayParameter = DayOfWeek.Saturday;
							} break;

							default:
							{
								dayParameter = DayOfWeek.Monday;
							} break;
						}

						if (dayParameter > DateTime.Now.DayOfWeek)
						{
							dateRecurrence = dateRecurrence.AddDays(dayParameter - DateTime.Now.DayOfWeek);

						}

						else if (dayParameter < DateTime.Now.DayOfWeek)
						{
							dateRecurrence = dateRecurrence.AddDays(7 - (DateTime.Now.DayOfWeek - dayParameter));
						}

						else
						{
							if (DateTime.Now > dateRecurrence)
								dateRecurrence = dateRecurrence.AddDays(7);

						}

					} break;

					case TypeRecurrence.selectedDate:
					{
						dateRecurrence = new DateTime(DateTime.Now.Year,DateTime.Now.Month,Int32.Parse(r.Parameter),r.Hour.Hour,r.Hour.Minute,r.Hour.Second,r.Hour.Millisecond);

						if (DateTime.Now.Day > Int32.Parse(r.Parameter))
						{
							dateRecurrence = dateRecurrence.AddMonths(1);
						}

						else if (DateTime.Now.Day == Int32.Parse(r.Parameter))
						{
							if (DateTime.Now > dateRecurrence)
								dateRecurrence = dateRecurrence.AddMonths(1);
						}

					} break;

					case TypeRecurrence.once:
					default:
					{
						dateRecurrence = new DateTime(_dateStart.Year,_dateStart.Month,_dateStart.Day,r.Hour.Hour,r.Hour.Minute,r.Hour.Second,r.Hour.Millisecond);

						if (DateTime.Now > dateRecurrence)
							dateRecurrence = DateTime.MinValue;

					} break;
		
				}

				if (dateTriggered == DateTime.MinValue)
					dateTriggered = dateRecurrence;

				else if (dateRecurrence != DateTime.MinValue)
					if (dateRecurrence < dateTriggered)
						dateTriggered = dateRecurrence;

				if (new DateTime(dateTriggered.Year,dateTriggered.Month,dateTriggered.Day,0,0,0) > new DateTime(_dateEnd.Year,_dateEnd.Month,_dateEnd.Day,0,0,0))
					dateTriggered = DateTime.MinValue;

			}

			return dateTriggered;

		}

		#endregion
	}

	#endregion
}
