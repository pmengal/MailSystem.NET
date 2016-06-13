






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class DisplayReminder : Reminder
	{
		public DisplayReminder()
		{

		}
		private string _displayString;

		public string DisplayString
		{
			get
			{
				return this._displayString;
			}
			set
			{
				this._displayString = value;
			}
		}
	}
}