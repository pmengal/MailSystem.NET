






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class EntityCollection : System.Collections.CollectionBase
	{
		public EntityCollection()
		{

		}
		public void Add(Entity entity)
		{
			this.InnerList.Add(entity);
		}
		public Entity this[int index]
		{
			get
			{
				return (Entity)this.InnerList[index];
			}
			set
			{
				this.InnerList[index] = value;
			}
		}
	}

}