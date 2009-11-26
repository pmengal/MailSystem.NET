using System;
using System.Windows.Forms;
using System.Drawing;

using ActiveQLibrary.CustomControl;

namespace ActiveQLibrary.Form
{
	#region class ProgressItem

	/// <summary>
	/// Item using with the progress page.
	/// </summary>
	public class ProgressItem
	{
		#region Variables

		/// <summary>
		/// Name of the job
		/// </summary>
		private string _job;

		/// <summary>
		/// Progressbar of the item
		/// </summary>
		private ProgressBarVS _progressBar;

		#endregion

		#region Constructor

		public ProgressItem(string job)
		{
			_job = job;

			_progressBar = new ProgressBarVS(Color.LightSteelBlue);

			_progressBar.Name = string.Format("{0}@ProgressBar",_job);
			_progressBar.TabIndex = 0;
			_progressBar.Value = 0;
			_progressBar.BorderColor = Color.Black;
			_progressBar.BackgroundColor = Color.SteelBlue;
			_progressBar.ForegroundColor = Color.LightSteelBlue;
			_progressBar.Smooth = true;
			_progressBar.Enable3DBorder = true;
			_progressBar.Border3D = Border3DStyle.Sunken;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the name of the job.
		/// </summary>
		public string job
		{
			get
			{
				return _job;
			}

			set
			{
				_job = value;
			}
		}

		/// <summary>
		/// Get / set the progress bar control.
		/// </summary>
		public ProgressBarVS ProgressBarControl
		{
			get
			{
				return _progressBar;
			}

			set
			{
				_progressBar = value;
			}
		}

		#endregion
	}

	#endregion
}
