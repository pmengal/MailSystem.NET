






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class MailReminder : Reminder
	{
		public MailReminder()
		{

		}
		private Address _sendTo;
		private string _note;

		public Address SendTo
		{
			get
			{
				return this._sendTo;
			}
			set
			{
				this._sendTo = value;
			}
		}
		public string Note
		{
			get
			{
				return this._note;
			}
			set
			{
				this._note = value;
			}
		}
	}
}