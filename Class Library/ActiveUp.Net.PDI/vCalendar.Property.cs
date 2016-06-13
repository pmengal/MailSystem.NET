






namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	public abstract class Property
	{
		public Property()
		{

		}
		private string _val;
		private ValueType _type;

		public string Value
		{
			get
			{
				return this._val;
			}
			set
			{
				this._val = value;
			}
		}
		public ValueType ValueType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}
	}
}