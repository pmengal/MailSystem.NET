





namespace ActiveUp.Net.Mail.PDI.vCard
{
	/// <summary>
	/// Contains one or more Address object(s).
	/// </summary>
	[System.Serializable]
	public class AddressCollection : System.Collections.CollectionBase
	{
		public AddressCollection()
		{
			
		}
		public void Add(ActiveUp.Net.Mail.PDI.vCard.Address address)
		{
			this.List.Add(address);
		}
		public ActiveUp.Net.Mail.PDI.vCard.Address this[int index]
		{
			get
			{
				return (ActiveUp.Net.Mail.PDI.vCard.Address)this.List[index];
			}
		}
		/// <summary>
		/// Returns the first object in the collection whose Type is Prefered.
		/// </summary>
		/// <returns>The first object in the collection whose Type is Prefered.</returns>
		public ActiveUp.Net.Mail.PDI.vCard.Address GetPrefered()
		{
			foreach(ActiveUp.Net.Mail.PDI.vCard.Address address in this) if(address.IsPrefered) return address;
			return null;
		}
	}
}
