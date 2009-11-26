






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class vCalendar
	{
		public vCalendar()
		{
		
		}
		private string _version,_prodId,_timeZone;
		private GeographicalPosition _geo = new GeographicalPosition();
		private DayLightSavings _daylight = new DayLightSavings();
		private vEventCollection _events;
		private vTodoCollection _todos;
		
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}
		public GeographicalPosition GeographicalPosition
		{
			get
			{
				return this._geo;
			}
			set
			{
				this._geo = value;
			}
		}
		public DayLightSavings DayLightSavings
		{
			get
			{
				return this._daylight;
			}
			set
			{
				this._daylight = value;
			}
		}
		public string GeneratorId
		{
			get
			{
				return this._prodId;
			}
			set
			{
				this._prodId = value;
			}
		}
		public string TimeZone
		{
			get
			{
				return this._timeZone;
			}
			set
			{
				this._timeZone = value;
			}
		}
		public vEventCollection Events
		{
			get
			{
				return this._events;
			}
			set
			{
				this._events = value;
			}
		}
		public vTodoCollection Todos
		{
			get
			{
				return this._todos;
			}
			set
			{
				this._todos = value;
			}
		}
	}
}
