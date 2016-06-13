






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class AudioReminder : Reminder
	{
		public AudioReminder()
		{

		}
		private string _content;

		public string Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}
	}
}