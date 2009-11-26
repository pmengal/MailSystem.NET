





namespace ActiveUp.Net.Mail.PDI.vCard
{
	/// <summary>
	/// Description résumée de vCard.
	/// </summary>
	[System.Serializable]
	public class Label
	{
		public Label()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}
		private string _label;
		private bool _isDomestic,_isInternational,_isPostal,_isParcel,_isWork,_isHome,_isPrefered = false;

		public string Value
		{
			get
			{
				return this._label;
			}
			set
			{
				this._label = value;
			}
		}
		public bool IsDomestic
		{
			get
			{
				return this._isDomestic;
			}
			set
			{
				this._isDomestic = value;
			}
		}
		public bool IsInternational
		{
			get
			{
				return this._isInternational;
			}
			set
			{
				this._isInternational = value;
			}
		}
		public bool IsPostal
		{
			get
			{
				return this._isPostal;
			}
			set
			{
				this._isPostal = value;
			}
		}
		public bool IsParcel
		{
			get
			{
				return this._isParcel;
			}
			set
			{
				this._isParcel = value;
			}
		}
		public bool IsWork
		{
			get
			{
				return this._isWork;
			}
			set
			{
				this._isWork = value;
			}
		}
		public bool IsHome
		{
			get
			{
				return this._isHome;
			}
			set
			{
				this._isHome = value;
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
			sb.Append("LABEL;");
			if(this.IsDomestic || this.IsHome || this.IsParcel || this.IsPostal || this.IsPrefered || this.IsWork) sb.Append("TYPE=");
			if(this.IsDomestic) sb.Append("dom,");
			if(this.IsHome) sb.Append("home,");
			if(this.IsParcel) sb.Append("parcel,");
			if(this.IsPostal) sb.Append("postal,");
			if(this.IsPrefered) sb.Append("pref,");
			if(this.IsWork) sb.Append("work,");
			sb.Remove(sb.Length-1,1);
			sb.Append(":");
			sb.Append(ActiveUp.Net.Mail.PDI.vCard.Parser.Escape(this.Value));
			return sb.ToString();
		}
	}
}
