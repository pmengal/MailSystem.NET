// Copyright 2001-2010 - Active Up SPRLU (http://www.agilecomponents.com)
//
// This file is part of MailSystem.NET.
// MailSystem.NET is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// MailSystem.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections;
using System.IO;

namespace ActiveUp.Net.Groupware.vCard
{
    #if !PocketPC
    [Serializable]
    #endif
    public class vCard
    {
        public static vCard LoadFromFile(string filename)
        {
            StreamReader streamReader = new StreamReader(filename);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            return Parser.Parse(content);
        }

        public void SaveToFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename);
            streamWriter.Write(GetData());
            streamWriter.Close();
        }

        /// <summary>
        /// The displayable, presentation text associated with the source for the vCard, as specified in the SOURCE property.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// To specify the formatted text corresponding to the name of the object the vCard represents.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Information    how to find the source for the vCard.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Components of the name of the object the vCard represents.
        /// </summary>
        public Name Name { get; set; }
        /// <summary>
        /// The nickname of    the object the vCard represents.
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Electronic mail software that is used by the individual associated with the vCard.
        /// </summary>
        public string Mailer { get; set; }
        /// <summary>
        /// Information related to the time zone (UTC offset) of the object the vCard represents.
        /// </summary>
        public string TimeZone { get; set; }
        /// <summary>
        /// The job title, functional position or function of the object the vCard represents.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Information concerning the role, occupation, or business category of the object the vCard represents.
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Supplemental information or a comment that is associated with the vCard.
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// The identifier for the product that created    the vCard object. (ISO 9070)
        /// </summary>
        public string GeneratorId { get; set; }
        /// <summary>
        /// Specify the family name or given name text to be used for national-language-specific sorting of the Full Name and Name properties.
        /// </summary>
        public string SortText { get; set; }
        /// <summary>
        /// A value that represents a globally unique identifier corresponding to the individual or resource associated with the vCard.
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// Specify a uniform resource locator associated with the object that the vCard refers to.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The version of the vCard specification used to format this vCard.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The access classification for a vCard object. (i.e. PRIVATE, PUBLIC, CONFIDENTIAL)
        /// </summary>
        public string AccessClass { get; set; }
        /// <summary>
        /// The birth date of the object the vCard represents.
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Revision information about the current vCard.
        /// The value distinguishes the current revision of    the information in this vCard for other renditions of the information.
        /// </summary>
        public DateTime Revision { get; set; }
        /// <summary>
        /// The organizational name and units associated with the vCard.
        /// </summary>
        public ArrayList Organization { get; set; } = new ArrayList();
        /// <summary>
        /// Application categories information about the vCard.
        /// </summary>
        public ArrayList Categories { get; set; } = new ArrayList();
        /// <summary>
        /// The components of the delivery addresses for the vCard object.
        /// </summary>
        public AddressCollection Addresses { get; set; } = new AddressCollection();
        /// <summary>
        /// The telephone numbers for telephony communication with the object the vCard represents.
        /// </summary>
        public TelephoneNumberCollection TelephoneNumbers = new TelephoneNumberCollection();
        /// <summary>
        /// The formatted text corresponding to delivery addresses of the object the vCard represents.
        /// </summary>
        public LabelCollection Labels { get; set; } = new LabelCollection();
        /// <summary>
        /// Information related to the global positioning of the object the vCard represents.
        /// </summary>
        public GeographicalPosition GeographicalPosition = new GeographicalPosition();
        /// <summary>
        /// The electronic mail addresses for communication with the object the vCard represents.
        /// </summary>
        public EmailAddressCollection EmailAddresses { get; set; } = new EmailAddressCollection();
        /// <summary>
        /// An image or photograph information that    annotates some aspect of the object the vCard represents.
        /// </summary>
        public byte[] Photo { get; set; }
        /// <summary>
        /// A graphic image of a logo associated with the object the vCard represents.
        /// </summary>
        public byte[] Logo { get; set; }
        /// <summary>
        /// A digital sound content information that annotates some aspect of the vCard. By default this type is used to specify the proper pronunciation of the name type value of the vCard.
        /// </summary>
        public byte[] Sound { get; set; }
        /// <summary>
        /// A public key or authentication certificate associated with the object that the vCard represents.
        /// </summary>
        public byte[] Key { get; set; }

        public string GetData()
        {

            //TODO : CRLFSPACE 
            StringWriter sw = new StringWriter();
            sw.WriteLine("BEGIN:VCARD");
            sw.WriteLine("VERSION:3.0");
            if(FullName != null)
                sw.WriteLine("FN:"+ Parser.Escape(FullName));
            if(DisplayName != null)
                sw.WriteLine("NAME:"+ Parser.Escape(DisplayName));
            if(GeneratorId != null)
                sw.WriteLine("PRODID:"+ Parser.Escape(GeneratorId));
            if(Mailer != null)
                sw.WriteLine("MAILER:"+ Parser.Escape(Mailer));
            if(Nickname != null)
                sw.WriteLine("NICKNAME:"+ Parser.Escape(Nickname));
            if(Note != null)
                sw.WriteLine("NOTE:"+ Parser.Escape(Note));
            if(Role != null)
                sw.WriteLine("ROLE:"+ Parser.Escape(Role));
            if(SortText != null)
                sw.WriteLine("SORT-STRING:"+ Parser.Escape(SortText));
            if(Source != null)
                sw.WriteLine("SOURCE:"+ Parser.Escape(Source));
            if(TimeZone != null)
                sw.WriteLine("TZ:"+ Parser.Escape(TimeZone));
            if(Title != null)
                sw.WriteLine("TITLE:"+ Parser.Escape(Title));
            if(AccessClass != null)
                sw.WriteLine("CLASS:"+ Parser.Escape(AccessClass));
            if(Addresses.Count>0)
                foreach (Address address in Addresses)
                    sw.WriteLine(address.GetFormattedLine());
            if(EmailAddresses.Count>0)
                foreach (EmailAddress email in EmailAddresses)
                    sw.WriteLine(email.GetFormattedLine());
            if(Labels.Count>0)
                foreach (Label label in Labels)
                    sw.WriteLine(label.GetFormattedLine());
            if(TelephoneNumbers.Count>0)
                foreach (TelephoneNumber number in TelephoneNumbers)
                    sw.WriteLine(number.GetFormattedLine());
            if(Name != null)
                sw.WriteLine(Name.GetFormattedLine());
            if(Birthday != DateTime.MinValue)
                sw.WriteLine("BDAY:"+ Birthday.ToString("yyyy-MM-dd"));
            if(GeographicalPosition != null && (GeographicalPosition.Latitude!=0 && GeographicalPosition.Longitude!=0))
                sw.WriteLine("GEO:"+ GeographicalPosition.Latitude.ToString()+";"+ GeographicalPosition.Longitude.ToString());
            //if(this.Organization!=null && this.Organization.Length>0)
            if(Organization != null && Organization.Count>0)
            {
                string organization = "ORG:";
                foreach(string str in Organization) organization += Parser.Escape(str)+",";
                organization = organization.TrimEnd(',');
                sw.WriteLine(organization);
            }
            //if (this.Categories != null && this.Categories.Length > 0)
            if (Categories != null && Categories.Count > 0)
            {
                string categories = "CATEGORIES:";
                foreach(string str in Categories) categories += Parser.Escape(str)+",";
                categories = categories.TrimEnd(',');
                sw.WriteLine(categories);
            }
            if(Photo != null && Photo.Length>0)
            {
                string binary = "PHOTO:"+ Convert.ToBase64String(Photo);
                sw.WriteLine(binary);
            }
            sw.WriteLine("REV:"+ DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            sw.WriteLine("END:VCARD");
            return Parser.Fold(sw.ToString());
        }
    }
}
