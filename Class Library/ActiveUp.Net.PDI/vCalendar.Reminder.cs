






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	public abstract class Reminder : Property
	{
		private System.DateTime _runTime;
		private Duration _snoozeTime;
		private int _repeatCount;

		public Duration SnoozeTime
		{
			get
			{
				return this._snoozeTime;
			}
			set
			{
				this._snoozeTime = value;
			}
		}
		public System.DateTime RunTime
		{
			get
			{
				return this._runTime;
			}
			set
			{
				this._runTime = value;
			}
		}
		public int RepeatCount
		{
			get
			{
				return this._repeatCount;
			}
			set
			{
				this._repeatCount = value;
			}
		}
	}

}