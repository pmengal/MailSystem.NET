// ActiveToolbar 1.x
// Copyright (c) 2004 Active Up SPRL - http://www.activeup.com
//
// LIMITATION OF LIABILITY
// The software is supplied "as is". Active Up cannot be held liable to you
// for any direct or indirect damage, or for any loss of income, loss of
// profits, operating losses or any costs incurred whatsoever. The software
// has been designed with care, but Active Up does not guarantee that it is
// free of errors.

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.ComponentModel;

namespace ActiveUp.MailSystem
{
	#region class ToolBase

	/// <summary>
	/// Base class for each item in the <see cref="Toolbar"/>.
	/// </summary>
	[
		TypeConverter(typeof(ExpandableObjectConverter)),
		Serializable,
		//ToolboxItem(false)
	]

	public abstract class ToolBase : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		#region Variables

		/// <summary>
		/// 
		/// </summary>
		private string _clientScriptBlock;

		/// <summary>
		/// 
		/// </summary>
		private string _clientScriptKey;

		/// <summary>
		/// 
		/// </summary>
		private string _startupScriptBlock;

		/// <summary>
		/// 
		/// </summary>
		private string _startupScriptKey;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ToolBase() : base()
		{
			_clientScriptBlock = string.Empty;
			_clientScriptKey = string.Empty;
			_startupScriptBlock = string.Empty;
			_startupScriptKey = string.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolBase"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		public ToolBase(string id) : base()
		{
			_clientScriptBlock = string.Empty;
			_clientScriptKey = string.Empty;
			_startupScriptBlock = string.Empty;
			_startupScriptKey = string.Empty;
			ID = id;
		}

		#endregion

		#region Properties
		/*[
		Browsable(false),
		Bindable(false)
		]
		public Toolbar ParentToolbar
		{
			get
			{
				if (this.Parent != null && this.Parent.GetType().ToString() == "ActiveUp.WebControls.Toolbar")
					return (Toolbar)this.Parent;
				else
					return null;
			}
		}*/

		/// <summary>
		/// Javascript code to execture client-side when the tool is clicked.
		/// </summary>
		[
		Bindable(true), 
		Category("Event"), 
		DefaultValue(""),
		Description("The javascript to execute client-side when the tool is clicked.")
		] 
		public string ClientSideClick
		{
			get
			{
				string _clientSideClick;
				_clientSideClick = ((string)ViewState["_clientSideClick"]);
				if (_clientSideClick != null)
					return _clientSideClick; 
				return string.Empty;
			}
			set
			{
				ViewState["_clientSideClick"] = value;
			}
		}

		


		/// <summary>
		/// Text to display with the tool.
		/// </summary>
		[
		Bindable(true), 
		Category("Data"), 
		DefaultValue(""),
		Description("The text to display with the tool.")
		] 
		public virtual string Text
		{
			get
			{
				string _text;
				_text = ((string) base.ViewState["_text"]);
				if (_text != null)
					return _text; 
				return string.Empty;
			}
			set
			{
				ViewState["_text"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the key that will be used when registering the client-side script on the ASPX page.
		/// </summary>
		[Bindable(true),
		Browsable(false),
		Category("Appearance"), 
		DefaultValue("Gets or sets the key that will be used when registering the client-side script on the ASPX page.")]
		public string ClientScriptKey 
		{
			get
			{
				return _clientScriptKey;
			}

			set
			{
				_clientScriptKey = value;
			}
		}

		/// <summary>
		/// Gets or sets the client-side script block to register with the tool.
		/// </summary>
		[Bindable(true), 
		Browsable(false),
		Category("Appearance"), 
		DefaultValue("Gets or sets the client-side script block to register with the tool.")] 
		public string ClientScriptBlock 
		{
			get
			{
				return _clientScriptBlock;
			}

			set
			{
				_clientScriptBlock = value;
			}
		}

		/// <summary>
		/// Gets or sets the startup client-side script to register with the tool.
		/// </summary>
		[Bindable(true), 
		Browsable(false),
		Category("Appearance"), 
		DefaultValue("Gets or sets the startup client-side script to register with the tool.")]
		public string StartupScriptBlock
		{
			get
			{
				return _startupScriptBlock;
			}

			set
			{
				_startupScriptBlock = value;
			}
		}

		/// <summary>
		/// Gets or sets the key that will be used when registering the startup client-side script on the ASPX page.
		/// </summary>
		[Bindable(true), 
		Browsable(false),
		Category("Appearance"), 
		DefaultValue("Gets or sets the key that will be used when registering the startup client-side script on the ASPX page.")]
		public string StartupScriptKey
		{
			get
			{
				return _startupScriptKey;
			}

			set
			{
				_startupScriptKey = value;
			}
		}

		/// <summary>
		/// Indicates if you want to allow rollover.
		/// </summary>
		[
		Bindable(true),
		Browsable(true),
		Category("Behavior"),
		DefaultValue(true),
		Description("Allow rollover."),
		NotifyParentProperty(true)
		]
		public bool AllowRollOver
		{
			get
			{
				object allowRollOver = ViewState["_allowRollOver"];
				if (allowRollOver != null)
					return (bool)allowRollOver;
				else
					return true;
			}

			set
			{
				ViewState["_allowRollOver"] = value;
			}
		}

		/// <summary>
		/// Image used as background of the tool.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Browsable(true),
		NotifyParentProperty(true),
		Description("Image used as background of the tool.")
		]
		public string BackImage
		{
			get
			{
				object backImage = ViewState["_backImage"];
				if (backImage != null)
					return (string)backImage;
				else
					return string.Empty;
			}

			set
			{
				ViewState["_backImage"] = value;
			}
		}

		/// <summary>
		/// Set to true if you need to use the control in a secure web page.
		/// </summary>
		[Bindable(false),
		Category("Behavior"),
		DefaultValue(false),
		Description("Set it to true if you need to use the control in a secure web page.")	]
		public bool EnableSsl
		{
			get
			{
				object enableSsl = ViewState["_enableSsl"];
				if (enableSsl != null)
					return (bool)enableSsl;
				return false;
			}

			set
			{
				ViewState["_enableSsl"] = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Renders at the design time.
		/// </summary>
		/// <param name="output">The output.</param>
		public virtual void RenderDesign(HtmlTextWriter output)
		{
			this.RenderControl(output);
		}

		/// <summary>
		/// Notifies the Popup control to perform any necessary prerendering steps prior to saving view state and rendering content.
		/// </summary>
		/// <param name="e">An EventArgs object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e) 
		{
			base.OnPreRender(e);

			if (this.Enabled)
			{
				if(ClientScriptBlock != string.Empty && !Page.IsClientScriptBlockRegistered(ClientScriptKey))
					Page.RegisterClientScriptBlock(ClientScriptKey, ClientScriptBlock);

				if(StartupScriptBlock != string.Empty && !Page.IsStartupScriptRegistered(StartupScriptKey))
					Page.RegisterStartupScript(StartupScriptKey, StartupScriptBlock);

			}
		}
		#endregion
		
	}

	#endregion
}
