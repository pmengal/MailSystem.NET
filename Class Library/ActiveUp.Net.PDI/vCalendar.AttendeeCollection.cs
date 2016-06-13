






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class AttendeeCollection : System.Collections.CollectionBase
	{
		public AttendeeCollection()
		{

		}
		public void Add(Attendee attendee)
		{
			this.InnerList.Add(attendee);
		}
		public Attendee this[int index]
		{
			get
			{
				return (Attendee)this.InnerList[index];
			}
			set
			{
				this.InnerList[index] = value;
			}
		}
	}

}
