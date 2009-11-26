





namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class DayLightSavings
	{
		public DayLightSavings()
		{
			
		}
		private bool _isObserved;
		private int _offset;
		private string _standard,_savings;
		private System.DateTime _start,_end;

		public string StandardTimeDesignation
		{
			get
			{
				return this._standard;
			}
			set
			{
				this._standard = value;
			}
		}
		public string Designation
		{
			get
			{
				return this._savings;
			}
			set
			{
				this._savings = value;
			}
		}
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}
		public System.DateTime Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}
		public System.DateTime End
		{
			get
			{
				return this._end;
			}
			set
			{
				this._end = value;
			}
		}
		public bool IsObserved
		{
			get
			{
				return this._isObserved;
			}
			set
			{
				this._isObserved = value;
			}
		}
	}
}
