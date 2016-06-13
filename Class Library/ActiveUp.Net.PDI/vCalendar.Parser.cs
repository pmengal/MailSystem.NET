using System.Collections;
using System;

namespace ActiveUp.Net.Mail.PDI.vCalendar
{
	[System.Serializable]
	public class Parser
	{
		public Parser()
		{
			
		}
		public static vCalendar Parse(string data)
		{
			vCalendar cal = new vCalendar();
			data = ActiveUp.Net.Mail.PDI.vCard.Parser.Unfold(data);
			cal.Events = ActiveUp.Net.Mail.PDI.vCalendar.Parser.GetEvents(data);
			cal.Todos = ActiveUp.Net.Mail.PDI.vCalendar.Parser.GetTodos(data);
			if((cal.Events.Count+cal.Todos.Count)>0) data = data.Substring(data.IndexOf("\r\n"),data.IndexOf("BEGIN:",data.IndexOf("\r\n"))-data.IndexOf("\r\n"));
			foreach(string line in System.Text.RegularExpressions.Regex.Split(data,"\r\n"))
			{
				string fulltype = line.Split(':')[0];
				string type = fulltype.Split(';')[0].ToUpper();
				switch(type)
				{
					case "VERSION": Parser.SetVersion(cal,line);
						break;
					case "DAYLIGHT": Parser.SetDayLight(cal,line);
						break;
					case "GEO": Parser.SetGeo(cal,line);
						break;
					case "TZ": Parser.SetTimeZone(cal,line);
						break;
					case "PRODID": Parser.SetGeneratorId(cal,line);
						break;
				}
			}
			return cal;
		}
		private static void SetVersion(vCalendar cal, string line)
		{
			cal.Version = line.Split(':')[1];
		}
		private static void SetDayLight(vCalendar cal, string line)
		{
			DayLightSavings savings = new DayLightSavings();
			string[] compounds = line.Split(':')[1].Split(';');
			if(compounds[0].ToUpper()=="TRUE")
			{
				savings.IsObserved = true;
				savings.Offset = System.Int32.Parse(compounds[1]);
				savings.Start = ActiveUp.Net.Mail.PDI.vCard.Parser.ParseDate(compounds[2]);
				savings.End = ActiveUp.Net.Mail.PDI.vCard.Parser.ParseDate(compounds[3]);
				savings.StandardTimeDesignation = compounds[4];
				savings.Designation = compounds[5];
			}
			else savings.IsObserved = false;
			cal.DayLightSavings = savings;
		}
		private static void SetGeo(vCalendar cal, string line)
		{
			GeographicalPosition geo = new GeographicalPosition();
			string val = line.Split(':')[1];
			string[] values = val.Split(';');
			geo.Latitude = System.Convert.ToDecimal(values[0]);
			geo.Longitude = System.Convert.ToDecimal(values[1]);
			cal.GeographicalPosition = geo;
		}
		private static void SetTimeZone(vCalendar cal, string line)
		{
			cal.TimeZone = line.Replace(line.Split(':')[0]+":","");
		}
		private static void SetGeneratorId(vCalendar cal, string line)
		{
			cal.GeneratorId = line.Replace(line.Split(':')[0]+":","");
		}
		private static vEventCollection GetEvents(string data)
		{
			int lastPosition = 0;
			vEventCollection events = new vEventCollection();
			LookForFurtherEvents:
				string eventData = data.Substring(data.ToUpper().IndexOf("BEGIN:VEVENT",lastPosition),data.ToUpper().IndexOf("END:VEVENT",lastPosition)+10-data.ToUpper().IndexOf("BEGIN:VEVENT",lastPosition));
			lastPosition = data.ToUpper().IndexOf("END:VEVENT",lastPosition)+10;
			events.Add(ActiveUp.Net.Mail.PDI.vCalendar.Parser.ParseEvent(eventData));
			if(data.ToUpper().IndexOf("BEGIN:VEVENT",lastPosition)!=-1) goto LookForFurtherEvents;
			return events;
		}
		private static vTodoCollection GetTodos(string data)
		{
			int lastPosition = 0;
			vTodoCollection todos = new vTodoCollection();

			LookForFurtherTodos:
            if (data.IndexOf("BEGIN:VTODO") > -1)
            {
                string todoData = data.Substring(data.ToUpper().IndexOf("BEGIN:VTODO", lastPosition), data.ToUpper().IndexOf("END:VTODO", lastPosition) + 10 - data.ToUpper().IndexOf("BEGIN:VTODO", lastPosition));
                lastPosition = data.ToUpper().IndexOf("END:VTODO", lastPosition) + 10;
                todos.Add(ActiveUp.Net.Mail.PDI.vCalendar.Parser.ParseTodo(todoData));
                if (data.ToUpper().IndexOf("BEGIN:VTODO", lastPosition) != -1) goto LookForFurtherTodos;
            }
        
			return todos;
		}
        public static System.DateTime ParseDate(string input)
        {
            try { return System.DateTime.Parse(input); }
            catch
            {
                if (input.Length == 8)
                {
                    input = input.Insert(4, "-");
                    input = input.Insert(7, "-");
                }
                else if (input.Length == 16)
                {
                    input = input.Insert(4, "-");
                    input = input.Insert(7, "-");
                    input = input.Insert(13, ":");
                    input = input.Insert(16, ":");
                }
            }
            return System.DateTime.Parse(input);
        }
		public static vEvent ParseEvent(string data)
		{
			vEvent even = new vEvent();
       
			foreach(string line in System.Text.RegularExpressions.Regex.Split(data,"\r\n"))
			{
				string fulltype = line.Split(':')[0];
				string type = fulltype.Split(';')[0].ToUpper();
				switch(type)
				{
					case "ATTACH": ActiveUp.Net.Mail.PDI.vCalendar.Parser.AddAttachment(even,line);
						break;
					case "ATTENDEE": ActiveUp.Net.Mail.PDI.vCalendar.Parser.AddAttendee(even,line);
						break;
                    //case "LOCATION": 
                    case "DTSTART":
                        even.Start = Parser.ParseDate(line.Split(':')[1]);
                        break;
                    case "DTEND":
                        even.End = Parser.ParseDate(line.Split(':')[1]);
                        break;
                    case "LOCATION":
                        even.Location = line.Split(':')[1];
                        break;
                    //case "DTSTAMP":
                    case "DESCRIPTION":
                        even.Description = line.Split(':')[1];
                        break;
                    case "SUMMARY":
                        even.Summary = line.Split(':')[1];
                        break;
                    case "PRIORITY":
                        even.Priority = Convert.ToInt32(line.Split(':')[1]);
                        break;
					//case "GEO": ActiveUp.Net.Mail.PDI.vCalendar.Parser.SetGeo(even,line);
					//	break;
					//case "TZ": ActiveUp.Net.Mail.PDI.vCalendar.Parser.SetTimeZone(even,line);
					//	break;
					//case "PRODID": ActiveUp.Net.Mail.PDI.vCalendar.Parser.SetGeneratorId(even,line);
					//	break;
				}
			}
			return even;
		}
		private static void SetValueAndType(Property property, string line)
		{
			string uppercase = line.Split(':')[0].ToUpper();
			if(uppercase.IndexOf("CID")!=-1 || uppercase.IndexOf("CONTENT-ID")!=-1) property.ValueType = ValueType.ContentId;
			else if(uppercase.IndexOf("URL")!=-1) property.ValueType = ValueType.Url;
			string charset = "utf-8";
			if(uppercase.IndexOf("CHARSET")!=-1) charset = uppercase.Substring(uppercase.IndexOf("CHARSET="),uppercase.IndexOf(";",uppercase.IndexOf("CHARSET="))-uppercase.IndexOf("CHARSET="));
			if(uppercase.IndexOf("ENCODING=QUOTED-PRINTABLE")!=-1) property.Value = FromQuotedPrintable(line.Replace(line.Split(':')[0]+":",""),charset);
			else if(uppercase.IndexOf("ENCODING=BASE64")!=-1) property.Value = System.Text.Encoding.GetEncoding(charset).GetString(System.Convert.FromBase64String(line.Replace(line.Split(':')[0]+":","")));
			else property.Value = line.Replace(line.Split(':')[0]+":","");
		}
		private static void AddAttachment(Entity entity, string line)
		{
			ActiveUp.Net.Mail.PDI.vCalendar.Attachment attach = new ActiveUp.Net.Mail.PDI.vCalendar.Attachment();
			ActiveUp.Net.Mail.PDI.vCalendar.Parser.SetValueAndType(attach,line);
			entity.Attachments.Add(attach);
		}
		private static void AddAttendee(Entity entity, string line)
		{
			Attendee attendee = new Attendee();
			Parser.SetValueAndType(attendee,line);
			attendee.Contact = new Address(attendee.Value);
			string uppercase = line.Split(':')[0].ToUpper();
			if(uppercase.IndexOf("EXPECT=REQUIRE")!=-1) attendee.Expectation = Expectation.Required;
			else if(uppercase.IndexOf("EXPECT=REQUEST")!=-1) attendee.Expectation = Expectation.Requested;
			else if(uppercase.IndexOf("EXPECT=IMMEDIATE")!=-1) attendee.Expectation = Expectation.ImmediateResponse;
			if(uppercase.IndexOf("ROLE=OWNER")!=-1) attendee.Role = Role.Owner;
			else if(uppercase.IndexOf("ROLE=ORGANIZER")!=-1) attendee.Role = Role.Organizer;
			else if(uppercase.IndexOf("ROLE=DELEGATE")!=-1) attendee.Role = Role.Delegate;
			if(uppercase.IndexOf("STATUS=ACCEPTED")!=-1) attendee.Status = Status.Accepted;
			else if(uppercase.IndexOf("STATUS=SENT")!=-1) attendee.Status = Status.Sent;
			else if(uppercase.IndexOf("STATUS=TENTATIVE")!=-1) attendee.Status = Status.Tentative;
			else if(uppercase.IndexOf("STATUS=CONFIRMED")!=-1) attendee.Status = Status.Confirmed;
			else if(uppercase.IndexOf("STATUS=DECLINED")!=-1) attendee.Status = Status.Declined;
			else if(uppercase.IndexOf("STATUS=COMPLETED")!=-1) attendee.Status = Status.Completed;
			else if(uppercase.IndexOf("STATUS=DELEGATED")!=-1) attendee.Status = Status.Delegated;
			if(uppercase.IndexOf("RVSP=YES")!=-1) attendee.ReplyRequested = true;
			entity.Attendees.Add(attendee);
		}
		public static vTodo ParseTodo(string data)
		{
			vTodo todo = new vTodo();
			todo.Summary = data;
			return todo;
		}
        public static string FromQuotedPrintable(string input, string toCharset)
        {
            try
            {
                input = input.Replace("=\r\n", "") + "=3D=3D";
                System.Collections.ArrayList arr = new System.Collections.ArrayList();
                int i = 0;
                byte[] decoded = new byte[0];
                while (true)
                {
                    if (i <= (input.Length) - 3)
                    {
                        if (input[i] == '=' && input[i + 1] != '=')
                        {
                            arr.Add(System.Convert.ToByte(System.Int32.Parse(String.Concat((char)input[i + 1], (char)input[i + 2]), System.Globalization.NumberStyles.HexNumber)));
                            i += 3;
                        }
                        else
                        {
                            arr.Add((byte)input[i]);
                            i++;
                        }
                    }
                    else break;
                }
                decoded = new byte[arr.Count];
                for (int j = 0; j < arr.Count; j++) decoded[j] = (byte)arr[j];
                return System.Text.Encoding.GetEncoding(toCharset).GetString(decoded).TrimEnd('=');
            }
            catch { return input; }
        }
	}
}
