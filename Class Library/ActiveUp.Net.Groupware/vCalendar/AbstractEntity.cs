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

namespace ActiveUp.Net.Groupware.vCalendar
{
    public abstract class AbstractEntity
    {
        private AttachmentCollection _attachments;
        private AttendeeCollection _attendees;

        public AttachmentCollection Attachments
        {
            get
            {
                if (_attachments == null)
                    _attachments = new AttachmentCollection();
                return _attachments;
            }
            set
            {
                _attachments = value;
            }
        }
        public AttendeeCollection Attendees
        {
            get
            {
                if (_attendees == null)
                    _attendees = new AttendeeCollection();
                return _attendees;
            }
            set
            {
                _attendees = value;
            }
        }
        public AudioReminder AudioReminder { get; set; } = new AudioReminder();
        public string[] Categories { get; set; }
        public Classification Classification { get; set; }
        public string Description { get; set; }
        public DateTime[] Exceptions { get; set; }
        public string ExceptionRule { get; set; }
        public DateTime LastModified { get; set; }
        public int Occurences { get; set; }
        public int Priority { get; set; }
        public EntityCollection RelatedEntities { get; set; } = new EntityCollection();
        public string RecurrenceRule { get; set; }
        public DateTime[] RecurrenceDates { get; set; }
        public string[] Ressources { get; set; }
        public int Revisions { get; set; }
        public DateTime Start { get; set; }
        public Status Status { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public bool BlocksTime { get; set; }
        public string Url { get; set; }
        public string Uid { get; set; }
    }
}