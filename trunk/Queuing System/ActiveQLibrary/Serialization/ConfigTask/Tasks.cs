using System;
using System.Collections;
using System.Xml.Serialization;

namespace ActiveQLibrary.Serialization.ConfigTask
{
	#region class Tasks

	/// <summary>
	/// Contains the list of Task.
	/// </summary> 
	/// 
	[System.Xml.Serialization.XmlRootAttribute("config", IsNullable=false)]
	public class Tasks
	{
		#region Variables
	
		/// <summary>
		/// List of task.
		/// </summary>
		private ArrayList _tasks = new ArrayList();

		#endregion

		#region Constructors

		public Tasks()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get / set the list of task.
		/// </summary>
		[System.Xml.Serialization.XmlArray("tasks")]
		[System.Xml.Serialization.XmlArrayItem("task",typeof(Task))]
		public ArrayList TasksList
		{
			get
			{
				return _tasks;
			}

			set
			{
				_tasks = value;
			}
		}

		#endregion

	}

	#endregion
}
	