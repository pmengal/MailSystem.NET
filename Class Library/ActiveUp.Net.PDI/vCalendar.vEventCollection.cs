






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class vEventCollection : System.Collections.CollectionBase
	{
		public vEventCollection()
		{

		}
		public void Add(vEvent vevent)
		{
			this.InnerList.Add(vevent);
		}
		public vEvent this[int index]
		{
			get
			{
				return (vEvent)this.InnerList[index];
			}
			set
			{
				this.InnerList[index] = value;
			}
		}
	}

}
