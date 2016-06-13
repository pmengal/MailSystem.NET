






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class AttachmentCollection : System.Collections.CollectionBase
	{
		public AttachmentCollection()
		{

		}
		public void Add(Attachment attachment)
		{
			this.InnerList.Add(attachment);
		}
		public Attachment this[int index]
		{
			get
			{
				return (Attachment)this.InnerList[index];
			}
			set
			{
				this.InnerList[index] = value;
			}
		}
	}

}
