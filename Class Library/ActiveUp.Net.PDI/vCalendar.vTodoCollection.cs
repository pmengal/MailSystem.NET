






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class vTodoCollection : System.Collections.CollectionBase
	{
		public vTodoCollection()
		{

		}
		public void Add(vTodo vtodo)
		{
			this.InnerList.Add(vtodo);
		}
		public vTodo this[int index]
		{
			get
			{
				return (vTodo)this.InnerList[index];
			}
			set
			{
				this.InnerList[index] = value;
			}
		}
	}

}
