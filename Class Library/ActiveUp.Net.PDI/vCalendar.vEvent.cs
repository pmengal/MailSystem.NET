namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class vEvent : Entity
	{
		public vEvent()
		{

		}

		private System.DateTime _end;

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
	}
}