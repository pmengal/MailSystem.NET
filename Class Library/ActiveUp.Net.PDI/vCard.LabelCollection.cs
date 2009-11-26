





namespace ActiveUp.Net.Mail.PDI.vCard
{
	/// <summary>
	/// Contains one or more Label object(s).
	/// </summary>
	[System.Serializable]
	public class LabelCollection : System.Collections.CollectionBase
	{
		public LabelCollection()
		{
			
		}
		public void Add(ActiveUp.Net.Mail.PDI.vCard.Label number)
		{
			this.List.Add(number);
		}
		public ActiveUp.Net.Mail.PDI.vCard.Label this[int index]
		{
			get
			{
				return (ActiveUp.Net.Mail.PDI.vCard.Label)this.List[index];
			}
		}
	}
}
