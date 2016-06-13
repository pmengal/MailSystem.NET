






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class ProcedureReminder : Reminder
	{
		public ProcedureReminder()
		{

		}
		private string _procedure;

		public string Procedure
		{
			get
			{
				return this._procedure;
			}
			set
			{
				this._procedure = value;
			}
		}
	}
}