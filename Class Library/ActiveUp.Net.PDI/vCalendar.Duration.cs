






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class Duration
	{
		public Duration()
		{
			
		}
		private int _years,_months,_weeks,_days,_hours,_minutes,_seconds;

		public int Years
		{
			get
			{
				return this._years;
			}
			set
			{
				this._years = value;
			}
		}
		public int Months
		{
			get
			{
				return this._months;
			}
			set
			{
				this._months = value;
			}
		}
		public int Weeks
		{
			get
			{
				return this._weeks;
			}
			set
			{
				this._weeks = value;
			}
		}
		public int Days
		{
			get
			{
				return this._days;
			}
			set
			{
				this._days = value;
			}
		}
		public int Hours
		{
			get
			{
				return this._hours;
			}
			set
			{
				this._hours = value;
			}
		}
		public int Minutes
		{
			get
			{
				return this._minutes;
			}
			set
			{
				this._minutes = value;
			}
		}
		public int Seconds
		{
			get
			{
				return this._seconds;
			}
			set
			{
				this._seconds = value;
			}
		}

		
	}

}