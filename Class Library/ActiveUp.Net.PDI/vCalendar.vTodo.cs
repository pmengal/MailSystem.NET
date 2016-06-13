






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class vTodo : Entity
	{
		public vTodo()
		{

		}
		
		private System.DateTime _created,_completed,_due;

		public System.DateTime Created
		{
			get
			{
				return this._created;
			}
			set
			{
				this._created = value;
			}
		}
		public System.DateTime Completed
		{
			get
			{
				return this._completed;
			}
			set
			{
				this._completed = value;
			}
		}
		public System.DateTime Due
		{
			get
			{
				return this._due;
			}
			set
			{
				this._due = value;
			}
		}
	}
}