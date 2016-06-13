





namespace ActiveUp.Net.Mail.PDI.vCard
{
	/// <summary>
	/// Contains one or more TelephoneNumber object(s).
	/// </summary>
	[System.Serializable]
	public class TelephoneNumberCollection : System.Collections.CollectionBase
	{
		public TelephoneNumberCollection()
		{
			
		}
		public void Add(ActiveUp.Net.Mail.PDI.vCard.TelephoneNumber number)
		{
			this.List.Add(number);
		}
        public void Add(string number)
        {
            this.List.Add(new TelephoneNumber(number));
        }
        public void Add(string number, TelephoneNumberSingleType type)
        {
            this.List.Add(new TelephoneNumber(number, type));
        }
		public ActiveUp.Net.Mail.PDI.vCard.TelephoneNumber this[int index]
		{
			get
			{
				return (ActiveUp.Net.Mail.PDI.vCard.TelephoneNumber)this.List[index];
			}
		}
		/// <summary>
		/// Returns the first object in the collection whose Type is Prefered.
		/// </summary>
		/// <returns>The first object in the collection whose Type is Prefered.</returns>
		public ActiveUp.Net.Mail.PDI.vCard.TelephoneNumber GetPrefered()
		{
			foreach(ActiveUp.Net.Mail.PDI.vCard.TelephoneNumber number in this)	if(number.IsPrefered) return number;
			return null;
		}
	}
}

