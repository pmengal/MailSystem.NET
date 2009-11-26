






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class Attendee : Property
	{
		public Attendee()
		{

		}
		private Role _role;
		private Status _status;
		private bool _reply;
		private Expectation _expectation;
		private Address _contact;

		public Role Role
		{
			get
			{
				return this._role;
			}
			set
			{
				this._role = value;
			}
		}
		public Status Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}
		public bool ReplyRequested
		{
			get
			{
				return this._reply;
			}
			set
			{
				this._reply = value;
			}
		}
		public Expectation Expectation
		{
			get
			{
				return this._expectation;
			}
			set
			{
				this._expectation = value;
			}
		}
		public Address Contact
		{
			get
			{
				return this._contact;
			}
			set
			{
				this._contact = value;
			}
		}
	}

}