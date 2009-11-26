using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Numeric data entry control
	/// </summary>
	public class NumEdit : TextBox
	{
		public enum NumEditType
		{
			Currency = 0,
			Decimal,
			Single,
			Double,
			SmallInteger,
			Integer,
			LargeInteger
		}	

		private NumEditType m_inpType = NumEditType.Integer;

		public NumEdit()
		{

			// set default input type
			this.InputType = NumEditType.Integer;

			// NOTE: Existing context menu allows Paste command, with no known
			//	method to intercept. Only option is to reset to empty.
			//	(setting to Null doesn't work)
			this.ContextMenu = new ContextMenu();
		}

		[Description("Sets the numeric type allowed"), Category("Behavior")]
		public NumEditType InputType
		{
			get{return m_inpType;}
			set
			{
				m_inpType = value;
			}
		}

		public override string Text
		{
			get{return base.Text;}
			set
			{
				if(IsValid(value, true))
					base.Text = value;
			}
		}

		private bool IsValid(string val, bool user)
		{
			// this method validates the ENTIRE string
			//	not each character
			// Rev 1: Added bool user param. This bypasses preliminary checks
			//	that allow -, this is used by the OnLeave event
			//	to prevent
			bool ret = true;

			if(val.Equals("")
				|| val.Equals(String.Empty))
				return ret;
			
			if(user)
			{
				// allow first char == '-'
				if(val.Equals("-"))
					return ret;
			}

			// parse into dataType, errors indicate invalid value
			// NOTE: parsing also validates data type min/max
			try
			{
				switch(m_inpType)
				{
					case NumEditType.Currency:
						decimal dec = decimal.Parse(val);
						int pos = val.IndexOf(".");
						if(pos != -1)
							ret = val.Substring(pos).Length <= 3;	// 2 decimals + "."
						break;
					case NumEditType.Single:
						float flt = float.Parse(val);
						break;
					case NumEditType.Double:
						double dbl = double.Parse(val);
						break;
					case NumEditType.Decimal:
						decimal dec2 = decimal.Parse(val);
						break;
					case NumEditType.SmallInteger:
						short s = short.Parse(val);
						break;
					case NumEditType.Integer:
						int i = int.Parse(val);
						break;
					case NumEditType.LargeInteger:
						long l = long.Parse(val);
						break;
					default:
						throw new ApplicationException();
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// trap Ctrl-V paste and prevent invalid values
			//	return false to allow further processing
			if(keyData == (Keys)Shortcut.CtrlV || keyData == (Keys)Shortcut.ShiftIns)
			{
				IDataObject iData = Clipboard.GetDataObject();
 
				// assemble new string and check IsValid
				string newText;
				newText = base.Text.Substring(0, base.SelectionStart)
					+ (string)iData.GetData(DataFormats.Text)
					+ base.Text.Substring(base.SelectionStart + base.SelectionLength);

				// check if data to be pasted is convertable to inputType
				if(!IsValid(newText, true))
					return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override void OnLeave(EventArgs e)
		{
			// handle - and leading zeros input since KeyPress handler must allow this
			if(base.Text != "")
			{
				if(!IsValid(base.Text, false))
					base.Text = "";
				else if(Double.Parse(base.Text) == 0)	// this used for -0, 000 and other strings
					base.Text = "0";
			}
			base.OnLeave(e);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			// assemble new text with new KeyStroke
			//	and pass to validation routine.

			// NOTES;
			//	1) Delete key is NOT passed here
			//	2) control passed here after ProcessCmdKey() is run

			char c = e.KeyChar;
			if(!Char.IsControl(c))	// not sure about this?? nothing in docs about what is Control char??
			{
				// prevent spaces
				if(c.ToString() == " ")
				{
					e.Handled = true;
					return;
				}

				string newText = base.Text.Substring(0, base.SelectionStart)
					+ c.ToString() + base.Text.Substring(base.SelectionStart + base.SelectionLength);
				
				if(!IsValid(newText, true))
					e.Handled = true;
			}
			base.OnKeyPress(e);
		}

	}
}
