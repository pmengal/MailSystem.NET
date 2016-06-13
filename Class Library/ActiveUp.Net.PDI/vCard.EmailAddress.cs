





namespace ActiveUp.Net.Mail.PDI.vCard
{
	/// <summary>
	/// Description résumée de vCard.
	/// </summary>
	[System.Serializable]
	public class EmailAddress
	{
		public EmailAddress()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

        public EmailAddress(string address)
        {
            _address = address;
            _isInternet = true;
        }

        public EmailAddress(string address, bool isInternet, bool isPrefered)
        {
            _address = address;
            _isInternet = isInternet;
            _isPrefered = IsPrefered;
        }
        
        private string _address;
		private bool _isInternet,_isX400,_isPrefered;

		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}
		public bool IsInternet
		{
			get
			{
				return this._isInternet;
			}
			set
			{
				this._isInternet = value;
			}
		}
		public bool IsX400
		{
			get
			{
				return this._isX400;
			}
			set
			{
				this._isX400 = value;
			}
		}
		public bool IsPrefered
		{
			get
			{
				return this._isPrefered;
			}
			set
			{
				this._isPrefered = value;
			}
		}

		public string GetFormattedLine()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("EMAIL;");
			if(this.IsInternet || this.IsPrefered || this.IsX400) sb.Append("TYPE=");
			if(this.IsInternet) sb.Append("internet,");
			if(this.IsPrefered) sb.Append("pref,");
			if(this.IsX400) sb.Append("x400,");
			sb.Remove(sb.Length-1,1);
			sb.Append(":");
			if(this.Address!=null) sb.Append(this.Address);
			return sb.ToString();
		}
	}
}
