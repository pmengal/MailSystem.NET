





namespace ActiveUp.Net.Mail.PDI.vCard
{
	/// <summary>
	/// Contains one or more EmailAddress object(s).
	/// </summary>
	[System.Serializable]
	public class EmailAddressCollection : System.Collections.CollectionBase
	{
		public EmailAddressCollection()
		{
			
		}
		public void Add(ActiveUp.Net.Mail.PDI.vCard.EmailAddress email)
		{
			this.List.Add(email);
        }
        public void Add(string address)
        {
            this.List.Add(new EmailAddress(address));
        }
        public void Add(string address, bool isInternet)
        {
            this.List.Add(new EmailAddress(address, isInternet, false));
        }

        public ActiveUp.Net.Mail.PDI.vCard.EmailAddress this[int index]
		{
			get
			{
				return (ActiveUp.Net.Mail.PDI.vCard.EmailAddress)this.List[index];
			}
		}
		/// <summary>
		/// Returns the first object in the collection whose Type is Prefered.
		/// </summary>
		/// <returns>The first object in the collection whose Type is Prefered.</returns>
		public ActiveUp.Net.Mail.PDI.vCard.EmailAddress GetPrefered()
		{
			foreach(ActiveUp.Net.Mail.PDI.vCard.EmailAddress email in this) if(email.IsPrefered) return email;
			return null;
		}
	}
}
