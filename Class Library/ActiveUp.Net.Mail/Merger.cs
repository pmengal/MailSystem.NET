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
using System.ComponentModel;
using System.Data;
#if !PocketPC
using System.Web.UI;
#endif
using System.Text.RegularExpressions;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// Merge the messages and it's properties with datasources.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class Merger
    {
        /// <summary>
        /// The default constructor.
        /// </summary>
        public Merger()
        {
            //
        }

        /// <summary>
        /// Instanciante the merger and auto-fill it's properties with templater data.
        /// </summary>
        /// <param name="templater">The Templater object that contains the data.</param>
        public Merger(Templater templater)
        {
            Conditions = templater.Conditions;
            Regions = templater.Regions;
            FieldsFormats = templater.FieldsFormats;
            SmtpServers = templater.SmtpServers;
            Message = templater.Message;
            ListTemplates = templater.ListTemplates;
        }

        /// <summary>
        /// Merge a text string with the specified datasource.
        /// </summary>
        /// <param name="text">The text to bind.</param>
        /// <param name="dataSource">The datasource to use.</param>
        /// <param name="repeat">Specify if you want the text to be repeated with each item in the datasource.</param>
        /// <returns>The merged text string.</returns>
        public string MergeText(string text, object dataSource, bool repeat)
        {
            Logger.AddEntry(GetType(), "Binding text.", 1);

            string newTemplate = string.Empty;

            // Define the string that will be used to store the data binder property value
            string dataBinderVal = string.Empty;

            // Get the main datasource
            IEnumerator items = GetEnumerator(dataSource);

            // If the IEnumerator items is not null, proceed
            if (items != null)
            {
                Logger.AddEntry(GetType(), "Datasource contain valid items for databinding.", 0);
                // Get the first occurence in the datasource
                while(items.MoveNext())
                {
                    Logger.AddEntry(GetType(), "Processing item.", 0);
                    if (repeat)
                        newTemplate += MergeText(text, items.Current);
                    else
                    {
                        text = MergeText(text, items.Current);
                        newTemplate = text;
                    }
                }
            }
            else
                newTemplate = text;

            // Return the binder template
            return newTemplate;
        }

        //Support taken out because DataBinder does not exist in PPC.
        /// <summary>
        /// Merge a text string with the specified item.
        /// </summary>
        /// <param name="text">The text string to merge.</param>
        /// <param name="item">The item to use for merging.</param>
        /// <returns>The merged text string.</returns>
        public string MergeText(string text, object item)
        {
            Logger.AddEntry(GetType(), "Binding text with item.", 0);
            
            string dataBinderVal = string.Empty, dataBinderKey = string.Empty;

            ArrayList fields = GetFields(text);

            Logger.AddEntry(GetType(), "Number of fields found in text: " + fields.Count.ToString(), 0);

            // For each found propertie, try to find the value in the datasource
            foreach(string field in fields)
            {
                Logger.AddEntry(GetType(), "Processing field: " + field, 0);

                // Try as standard object with properties
                try
                {
                    dataBinderVal = string.Empty;
                    if (FieldsFormats.Contains(field))
                    {
                        FieldFormat fieldFormat = FieldsFormats[field];

                        Logger.AddEntry(GetType(), "StringFormat to apply: " + fieldFormat.Format, 0);

                        dataBinderVal = Convert.ToString(DataBinder.GetPropertyValue(item, field, fieldFormat.Format));
                    }
                    else
                    {
                        dataBinderVal = Convert.ToString(DataBinder.GetPropertyValue(item, field));
                    }
                    Logger.AddEntry(GetType(), "Field value after binding: " + dataBinderVal, 0);
                    //validate the condition if exists at all
                    Conditions.Validate(field, dataBinderVal);

                    text = ReplaceField(text, field, dataBinderVal);
                }
                catch
                {
                    Logger.AddEntry(GetType(), "DataBinder Eval failed. Probable standard datasource doesn't contain the Field '" + field + "'.", 0);
                }

                // Try as a key/pair object type.
                if (dataBinderVal == string.Empty)
                {
                    Logger.AddEntry(GetType(), "Trying key/pair object type.", 2);

                    try
                    {
                        dataBinderKey = Convert.ToString(DataBinder.Eval(item, "Key"));

                        Logger.AddEntry(GetType(), "Field value after binding (key): " + dataBinderVal, 0);
                        
                        if (dataBinderKey.ToUpper() == field.ToUpper())
                        {
                            if (FieldsFormats.Contains(field))
                            {
                                FieldFormat fieldFormat = FieldsFormats[field];
                                dataBinderVal = Convert.ToString(DataBinder.Eval(item, "Value", fieldFormat.Format));
                            }
                            else
                            {
                                dataBinderVal = Convert.ToString(DataBinder.Eval(item, "Value"));
                            }
                            //validate the condition if exists at all
                            Conditions.Validate(field, dataBinderVal);
                            text = ReplaceField(text, field, dataBinderVal);
                        }
                    
                    }
                    catch
                    {
                        Logger.AddEntry(GetType(), "DataBinder (Indexed) Eval failed. Probable indexed datasource doesn't contain the Field '" + field + "'.", 0);
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Merge a message with the specified item.
        /// </summary>
        /// <returns>The merged the message.</returns>
        public Message MergeMessage()
        {
            if (RemoveUnusedTags) 
                Message.BodyText.Text = CleanUnusedTags(Message.BodyText.Text);
            Message.BodyText.Text = ProcessIDs(Message.BodyText.Text, null, false);
            if (RemoveUnusedTags) 
                Message.BodyHtml.Text = CleanUnusedTags(Message.BodyHtml.Text);
            Message.BodyHtml.Text = ProcessIDs(Message.BodyHtml.Text, null, false);

            return Message;
        }

        /// <summary>
        /// Merge a message with the specified item.
        /// </summary>
        /// <param name="message">The Message object to merge.</param>
        /// <param name="item">The item to use for merging.</param>
        /// <returns>The merged the message.</returns>
        public Message MergeMessage(Message message, object item)
        {
            // Process the To
            message.To = MergeAddresses(message.To, item);
            
            // Process the Cc
            message.Cc = MergeAddresses(message.Cc, item);

            // Process the Bcc
            message.Bcc = MergeAddresses(message.Bcc, item);
            Conditions.Validate(item);

            // Process the Custom Collection
            message.CustomCollection = MergeCustomItem(message.CustomCollection, item);

            // Process the From
            if (message.From != null)
                message.From = MergeAddress(message.From, item);

            // Process the Reply-to
            if (message.ReplyTo != null)
                message.ReplyTo = MergeAddress(message.ReplyTo, item);

            // Process the Return receipt
            if (message.ReplyTo != null)
                message.ReturnReceipt = MergeAddress(message.ReturnReceipt, item);

            // Process the Subject
            message.Subject = MergeText(message.Subject, item);
            foreach(Region region in Regions)
            {
                region.URL = MergeText(region.URL, item);
            }

            message.BodyText.Text = ProcessIDs(Message.BodyText.Text, item, false);
            message.BodyText.Text = MergeText(message.BodyText.Text, item);
            if (RemoveUnusedTags) 
                message.BodyText.Text = CleanUnusedTags(Message.BodyText.Text);
            //message.BodyText.Text = Regex.Replace(Message.BodyText.Text,@"\$(.*?)\$",string.Empty);
            message.BodyHtml.Text = ProcessIDs(Message.BodyHtml.Text, item, false);
            message.BodyHtml.Text = MergeText(message.BodyHtml.Text, item);
            if (RemoveUnusedTags) 
                message.BodyHtml.Text = CleanUnusedTags(Message.BodyHtml.Text);
            //message.BodyHtml.Text = Regex.Replace(Message.BodyHtml.Text,@"\$(.*?)\$",string.Empty);

            return message;
        }

        /// <summary>
        /// Whether to remove unused tags in the message after merging the datasource.
        /// </summary>
        public bool RemoveUnusedTags { get; set; }
        /// <summary>
        /// Clean up unused tags in the message.
        /// </summary>
        /// <param name="Body">The body text to use for merging.</param>
        /// <returns>The merged message.</returns>
        private string CleanUnusedTags(string Body)
        {
            Regex rx = new Regex(@"\$(.*?)\$");
            MatchCollection mc;
            mc = rx.Matches(Body);
            if (mc.Count > 0)
                foreach (Match m in mc)
                    Body = ReplaceField(Body,m.Value,string.Empty);
            return Body;
        }

        /// <summary>
        /// Process IDs in the message.
        /// </summary>
        /// <param name="Body">The body text to use for merging.</param>
        /// <param name="dataSource"></param>
        /// <param name="repeat"></param>
        /// <returns>The merged message.</returns>
        private string ProcessIDs(string Body, object dataSource, bool repeat)
        {
            
            //process conditions
            foreach(Condition condition in Conditions)
            {
                if (condition.Match)
                {
                    Regex rx = new Regex(@"(?<completetag><(?<tag>[^\s>]*)[^>]*?(?i:ID=""" + condition.RegionID + @""")(?<result>.[^>]+>))(?<contents>.*?)(?<endtag></\k<tag>>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc;
                    mc = rx.Matches(Body);
                    if (mc.Count > 0)
                    {
                        foreach (Match m in mc)
                        {
                            Body = Body.Replace(m.Value,condition.NullText);
                        }
                    }
                    rx = new Regex(@"(?<completetag><(?<tag>[^\s>]*)[^>]*?(?i:ID=" + condition.RegionID + @")(?<result>.[^>]+>))(?<contents>.*?)(?<endtag></\k<tag>>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mc = rx.Matches(Body);
                    if (mc.Count > 0)
                    {
                        foreach (Match m in mc)
                        {
                            Body = Body.Replace(m.Value,condition.NullText);
                        }
                    }
                }
            }

            //process regions
            foreach(Region region in Regions)
            {
                Regex rx = new Regex(@"(?<completetag><(?<tag>[^\s>]*)[^>]*?(?i:ID=""" + region.RegionID + @""")(?<result>.[^>]+>))(?<contents>.*?)(?<endtag></\k<tag>>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection mc;
                mc = rx.Matches(Body);
                if (mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        Body = Body.Replace(m.Value,m.Groups["completetag"].Value + MergeText(region.Content,dataSource,repeat) + m.Groups["endtag"].Value);
                    }
                }
                rx = new Regex(@"(?<completetag><(?<tag>[^\s>]*)[^>]*?(?i:ID=" + region.RegionID + @")(?<result>.[^>]+>))(?<contents>.*?)(?<endtag></\k<tag>>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                mc = rx.Matches(Body);
                if (mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        Body = Body.Replace(m.Value,m.Groups["completetag"].Value + MergeText(region.Content,dataSource,repeat) + m.Groups["endtag"].Value);
                    }
                }
            }

            //remove empty list templates
            foreach(ListTemplate listTemplate in ListTemplates)
            {
                if ((listTemplate.Count == 0) && (listTemplate.RegionID != ""))
                {
                    Regex rx = new Regex(@"(?<completetag><(?<tag>[^\s>]*)[^>]*?(?i:ID=""" + listTemplate.RegionID + @""")(?<result>.[^>]+>))(?<contents>.*?)(?<endtag></\k<tag>>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc;
                    mc = rx.Matches(Body);
                    if (mc.Count > 0)
                    {
                        foreach (Match m in mc)
                        {
                            Body = Body.Replace(m.Value, MergeText(listTemplate.NullText,dataSource,repeat));
                        }
                    }
                    rx = new Regex(@"(?<completetag><(?<tag>[^\s>]*)[^>]*?(?i:ID=" + listTemplate.RegionID + @")(?<result>.[^>]+>))(?<contents>.*?)(?<endtag></\k<tag>>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mc = rx.Matches(Body);
                    if (mc.Count > 0)
                    {
                        foreach (Match m in mc)
                        {
                            Body = Body.Replace(m.Value, MergeText(listTemplate.NullText,dataSource,repeat));
                        }
                    }
                }
            }

            return Body;
        }

        /// <summary>
        /// Merge the Message property with the specified datasource.
        /// </summary>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <returns>The merged message.</returns>
        public Message MergeMessage(object dataSource)
        {
            Message = MergeMessage(Message, dataSource, false);
            return Message;
        }

        /// <summary>
        /// Merge the Message property with the specified datasource.
        /// </summary>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <param name="repeat">Specify if the text template will be repeated or not.</param>
        /// <returns>The merged message.</returns>
        public Message MergeMessage(object dataSource, bool repeat)
        {
            Message = MergeMessage(Message, dataSource, repeat);
            return Message;
        }

        /*public Message MergeMessage(Message message, object dataSource)
        {
            return MergeMessage(message, dataSource, false);
        }*/

        /// <summary>
        /// Merge a Message object with the specified datasource.
        /// </summary>
        /// <param name="message">The message to merge.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <param name="repeat">Specify if the text template will be repeated or not.</param>
        /// <returns>The merge message.</returns>
        public Message MergeMessage(Message message, object dataSource, bool repeat)
        {
            // Process the To
            message.To = MergeAddresses(message.To, dataSource, repeat);

            // Process the Cc
            message.Cc = MergeAddresses(message.Cc, dataSource, repeat);

            // Process the Bcc
            message.Bcc = MergeAddresses(message.Bcc, dataSource, repeat);

            Conditions.Validate(dataSource);
            // Process the Custom Collection
            message.CustomCollection = MergeCustom(message.CustomCollection, dataSource, repeat);

            // Process the From
            if (message.From != null)
                message.From = MergeAddress(message.From, dataSource);

            // Process the Reply-to
            if (message.ReplyTo != null)
                message.ReplyTo = MergeAddress(message.ReplyTo, dataSource);

            // Process the Return receipt
            if (message.ReplyTo != null)
                message.ReturnReceipt = MergeAddress(message.ReturnReceipt, dataSource);

            // Process the Subject
            message.Subject = MergeText(message.Subject, dataSource, repeat);
            foreach(Region region in Regions)
                region.URL = MergeText(region.URL, dataSource, repeat);

            message.BodyText.Text = MergeText(message.BodyText.Text, dataSource, repeat);
            if (RemoveUnusedTags) 
                message.BodyText.Text = CleanUnusedTags(Message.BodyText.Text);
            message.BodyText.Text = ProcessIDs(Message.BodyText.Text, dataSource, repeat);
            message.BodyHtml.Text = MergeText(message.BodyHtml.Text, dataSource, repeat);
            if (RemoveUnusedTags) 
                message.BodyHtml.Text = CleanUnusedTags(Message.BodyHtml.Text);
            message.BodyHtml.Text = ProcessIDs(Message.BodyHtml.Text, dataSource, repeat);

            return message;
        }

        /// <summary>
        /// Merge the custom collection with the specified datasource.
        /// </summary>
        /// <param name="customcollection">The custom collection to use for merging.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <param name="repeat">Specify if the texts will be repeated or not.</param>
        /// <returns>The merged Custom collection</returns>
        public Hashtable MergeCustom(Hashtable customcollection, object dataSource, bool repeat)
        {
            Hashtable _changes = new Hashtable();
            foreach (DictionaryEntry itmX in customcollection) 
            {
                string aValue = MergeText((string)itmX.Value, dataSource, repeat);
                if (aValue != (string)itmX.Value) _changes.Add(itmX.Key,aValue);
            }
            foreach (DictionaryEntry itmX in _changes)
            {
                customcollection[itmX.Key] = itmX.Value;
            }
            return customcollection;
        }
        /// <summary>
        /// Merge the custom collection with the specified datasource.
        /// </summary>
        /// <param name="customcollection">The custom collection to use for merging.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <returns>The merged Custom collection</returns>
        public Hashtable MergeCustom(Hashtable customcollection, object dataSource)
        {
            Hashtable _changes = new Hashtable();
            foreach (DictionaryEntry itmX in customcollection)
            {
                string aValue = MergeText((string)itmX.Value, dataSource, false);
                if (aValue != (string)itmX.Value) _changes.Add(itmX.Key,aValue);
            }
            foreach (DictionaryEntry itmX in _changes)
            {
                customcollection[itmX.Key] = itmX.Value;
            }
            return customcollection;
        }
        /// <summary>
        /// Merge the custom collection with the specified item.
        /// </summary>
        /// <param name="customcollection">The custom collection to use for merging.</param>
        /// <param name="item">The item to use for merging.</param>
        /// <returns>The merged custom colection.</returns>
        public Hashtable MergeCustomItem(Hashtable customcollection, object item)
        {
            foreach (DictionaryEntry itmX in customcollection)
            {
                string aValue = MergeText((string)itmX.Value, item);
                customcollection[itmX.Key] = aValue;
            }
            return customcollection;
        }


        /// <summary>
        /// Merge the Address collection with the specified datasource.
        /// </summary>
        /// <param name="addresses">The addresses to merge.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <param name="repeat">Specify if the texts will be repeated or not.</param>
        /// <returns>The merged Address collection</returns>
        public AddressCollection MergeAddresses(AddressCollection addresses, object dataSource, bool repeat)
        {
            int index;

            for(index=0;index<addresses.Count;index++)
            {
                addresses[index].Email = MergeText(addresses[index].Email, dataSource, repeat);
                addresses[index].Name = MergeText(addresses[index].Name, dataSource, repeat);
            }

            return addresses;
        }

        /// <summary>
        /// Merge the Address collection with the specified datasource.
        /// </summary>
        /// <param name="address">The address to merge.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <returns>The merged Address collection</returns>
        public Address MergeAddress(Address address, object dataSource)
        {
            if (address != null)
            {
                address.Name = MergeText(address.Name, dataSource,false);
                address.Email = MergeText(address.Email, dataSource,false);

                return address;
            }

            return null;
        }


        /// <summary>
        /// Merge the address collection with the specified item.
        /// </summary>
        /// <param name="addresses">The address to merge.</param>
        /// <param name="item">The item to use for merging.</param>
        /// <returns>The merged Address colection.</returns>
        public AddressCollection MergeAddresses(AddressCollection addresses, object item)
        {
            int index;

            for(index=0;index<addresses.Count;index++)
            {
                addresses[index].Email = MergeText(addresses[index].Email, item);
                addresses[index].Name = MergeText(addresses[index].Name, item);
            }

            return addresses;
        }

        /// <summary>
        /// Merge the specified list template with the datasource.
        /// </summary>
        /// <param name="name">The name of the list template.</param>
        /// <param name="dataSource">The datasource to merge with.</param>
        /// <returns>The merged message.</returns>
        public Message MergeListTemplate(string name, object dataSource)
        {
            return MergeListTemplate(Message, name, ListTemplates, dataSource);
        }

        /// <summary>
        /// Merge the specified list template with the datasource.
        /// </summary>
        /// <param name="name">The name of the list template.</param>
        /// <param name="listTemplates">The ListTemplates to use.</param>
        /// <param name="dataSource">The datasource to merge with.</param>
        /// <returns>The merged message.</returns>
        public Message MergeListTemplate(string name, ListTemplateCollection listTemplates, object dataSource)
        {
            return MergeListTemplate(Message, name, listTemplates, dataSource);
        }

        /// <summary>
        /// Merge the specified list template with the datasource.
        /// </summary>
        /// <param name="message">The message that contain the list template field.</param>
        /// <param name="name">The name if the list template.</param>
        /// <param name="listTemplates">The ListTemplates to use.</param>
        /// <param name="dataSource">The datasource to merge with.</param>
        /// <returns>The merged message.</returns>
        public Message MergeListTemplate(Message message, string name, ListTemplateCollection listTemplates, object dataSource)
        {
            Logger.AddEntry(GetType(), "Merging Template " + name + ".", 1);

            foreach(ListTemplate listTemplate in listTemplates)
            {
                if (listTemplate.Name.ToUpper() == name.ToUpper())
                {
                    listTemplate.DataSource = dataSource;
                    message.BodyText.Text = ReplaceField(message.BodyText.Text, listTemplate.Name, MergeText(listTemplate.Content, dataSource, true));
                    message.BodyHtml.Text = ReplaceField(message.BodyHtml.Text, listTemplate.Name, MergeText(listTemplate.Content, dataSource, true));
                }
            }

            return message;
        }

        /// <summary>
        /// Create a collection of message based on the merging of the message and a datasource.
        /// </summary>
        /// <param name="message">The message to use as a base for merging.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <returns>The merged MessageCollection.</returns>
        public MessageCollection MergeCollection(Message message, object dataSource)
        {
            return MergeCollection(message, dataSource, false);
        }

        /// <summary>
        /// Create a collection of message based on the merging of the Message property and a datasource.
        /// </summary>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <returns>The merged MessageCollection.</returns>
        public MessageCollection MergeCollection(object dataSource)
        {
            return MergeCollection(Message, dataSource, false);
        }

        /// <summary>
        /// Create a collection of message based on the merging of the message and a datasource.
        /// </summary>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <param name="send">Specify if you want to send the message when merged.</param>
        /// <returns>The merged MessageCollection.</returns>
        public MessageCollection MergeCollection(object dataSource, bool send)
        {
            return MergeCollection(Message, dataSource, send);
        }

        /// <summary>
        /// Create a collection of message based on the merging of the message and a datasource.
        /// </summary>
        /// <param name="message">The message to use as a base for merging.</param>
        /// <param name="dataSource">The datasource to use for merging.</param>
        /// <param name="send">Specify if you want to send the message when merged.</param>
        /// <returns>The merged MessageCollection.</returns>
        public MessageCollection MergeCollection(Message message, object dataSource, bool send)
        {
            MessageCollection messages = new MessageCollection();

            if (dataSource.GetType().ToString() == "System.Data.DataSet")
                dataSource = ((DataSet)dataSource).Tables[0];

            IEnumerator items = GetEnumerator(dataSource);

            // Determine max
            int total = 0;//, messageNumber = 0, messageSent = 0;

            while(items.MoveNext())
                total++;

            items.Reset();
        
            if (items != null)
            {
                while(items.MoveNext())
                {
                    Message newMessage = message.Clone();
                    MergeMessage(newMessage, items.Current);
                    messages.Add(newMessage);

                    /*string file = @"c:\temp\_amail_\test.eml";
                    if (File.Exists(file))
                        File.Delete(file);
                    newMessage.StoreToFile(file);*/

                    if (send)
                    {
                        if (SmtpServers.Count > 0)
                        {
                            //newMessage.Send(this.SmtpServers);
                            SmtpClient.Send(newMessage, SmtpServers);
                        }
                        else
                        {
                            //newMessage.DirectSend();
                            SmtpClient.DirectSend(newMessage);
                        }
                    }
                }

                Logger.AddEntry(GetType(), "Message created successfully.", 2);
            }

            return messages;
        }

        /// <summary>
        /// Get all fields contained in the specified string.
        /// </summary>
        /// <param name="source">The string containing the fields.</param>
        /// <returns>An arraylist filled with the fields.</returns>
        private ArrayList GetFields(string source)
        {
            ArrayList fields = new ArrayList();
            int startIndex = 0;

            if (!string.IsNullOrEmpty(source))
            {

                while(source.IndexOf("$", startIndex) > -1)
                {
                    // Find a "$" char
                    startIndex = source.IndexOf("$", startIndex);

                    // Is a field ?
                    if (source.IndexOf("$", startIndex + 1) > -1)
                    {
                        int nextOccurence = source.IndexOf("$", startIndex + 1);
                        string field = source.Substring(startIndex + 1, nextOccurence - startIndex - 1);

                        if (field.IndexOf(" ") == -1 && field.IndexOf("\n") == -1 && field.Length >= 2)
                        {
                            startIndex = nextOccurence;
                            if (!fields.Contains(field))
                                fields.Add(field);
                        }
                    }

                    startIndex++;
                }
            }

            return fields;

        }

        /// <summary>
        /// Get the enumerator from the specified data source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <returns>The enumerator.</returns>
        private IEnumerator GetEnumerator(object dataSource)
        {
            if (dataSource == null) return null;
            Logger.AddEntry(GetType(), "Getting IEnumerator. DataSource type: " + dataSource, 0);

            // Set the IEnumerator object
            IEnumerator items = null;
            
            // IEnumerable & IListSource specific methods
            if (dataSource is IEnumerable)
            {
                items = ((IEnumerable)dataSource).GetEnumerator();
                Logger.AddEntry(GetType(), "DataSource is IEnumerable.", 0);
            }

            if (dataSource is IListSource)
            {
                items = ((IListSource)dataSource).GetList().GetEnumerator();
                Logger.AddEntry(GetType(), "DataSource is IListSource.", 0);
            } 
    
            if (dataSource is DataRow)
            {
                
                DataView dataView = new DataView(((DataRow)dataSource).Table);
                ArrayList sourceList = new ArrayList();

                foreach (DataRowView drv in dataView)
                {
                    if (drv.Row == dataSource)
                    {
                        sourceList.Add(drv);
                        break;
                    }
                }
                //sourceList.Add(dataView[0]);
                items = sourceList.GetEnumerator();
                Logger.AddEntry(GetType(), "DataSource is DataRow.", 0);
            }

            // Return the IEnumerator
            return items;
        }

        /// <summary>
        /// Checks if supplied expression is a numeric value. Supports multiple languages.
        /// </summary>
        /// <param name="expression">An expression that needs to be checked.</param>
        /// <returns>A boolean response that is True if it is a numeric value.</returns>
        public bool IsNumeric(object expression)
        {
            double retNum;
            return double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }
        
        /// <summary>
        /// Replace all specified fields of the specified string with the specified replacement string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="field">The field to search for.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>The string with replaced content.</returns>
        public string ReplaceField(string source, string field, string replacement)
        {
            if (source != null)
            {
                if (FieldsFormats.Contains(field))
                {
                    FieldFormat fieldFormat = FieldsFormats[field];

                    if (fieldFormat.Format.Length > 0)
                        replacement = IsNumeric(replacement) ? string.Format(fieldFormat.Format, double.Parse(replacement)) : string.Format(fieldFormat.Format, replacement);

                    if (fieldFormat.TotalWidth > 0)
                    {
                        replacement = fieldFormat.PaddingDir == PaddingDirection.Left ? replacement.PadLeft(fieldFormat.TotalWidth, fieldFormat.PaddingChar) : replacement.PadRight(fieldFormat.TotalWidth, fieldFormat.PaddingChar);
                    }
                }

                Conditions.Validate(field, replacement);
                source = source.Replace("$" + field + "$", replacement);
                return source;
            }
            return "";
        }

        /// <summary>
        /// Replace all specified fields of the message body with the specified replacement.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="field">The field to search for.</param>
        /// <param name="replacement">The replacement.</param>
        public Message ReplaceField(Message message, string field, string replacement)
        {
            message.Bcc = ReplaceInAddresses(message.Bcc, field, replacement);
            message.Cc = ReplaceInAddresses(message.Cc, field, replacement);
            if (message.From != null) 
            {
                message.From.Email = message.From.Email.Replace("$" + field + "$", replacement);
                message.From.Name = message.From.Name.Replace("$" + field + "$", replacement);
            }
            if (message.Subject != null)
                message.Subject = message.Subject.Replace("$" + field + "$", replacement);
            message.To = ReplaceInAddresses(message.To, field, replacement);
            message.BodyText.Text = ReplaceField(message.BodyText.Text, field, replacement);
            message.BodyHtml.Text = ReplaceField(message.BodyHtml.Text, field, replacement);
            foreach(Region region in Regions)
            {
                region.URL = ReplaceField(region.URL, field, replacement);
            }
            return message;
        }

        
        /// <summary>
        /// Replace in an AddressCollection.
        /// </summary>
        /// <param name="addresses">The AddressCollection.</param>
        /// <param name="field">The field.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>The AddressCollection with replaced content.</returns>
        private AddressCollection ReplaceInAddresses(AddressCollection addresses, string field, string replacement)
        {
            foreach (Address t in addresses)
            {
                t.Email = ReplaceField(t.Email, field, replacement);
                t.Name = ReplaceField(t.Name, field, replacement);
            }

            return addresses;
        }

        /// <summary>
        /// Gets or sets the region collection.
        /// </summary>
        public RegionCollection Regions { get; set; } = new RegionCollection();

        /// <summary>
        /// Gets or sets the conditional collection.
        /// </summary>
        public ConditionalCollection Conditions { get; set; } = new ConditionalCollection();

        /// <summary>
        /// Gets or sets the fields format options.
        /// </summary>
        public FieldFormatCollection FieldsFormats { get; set; } = new FieldFormatCollection();

        /// <summary>
        /// Gets or sets the SMTP servers.
        /// </summary>
        public ServerCollection SmtpServers { get; set; } = new ServerCollection();

        /// <summary>
        /// Gets or sets the logging settings.
        /// </summary>
        public Message Message { get; set; } = new Message();

        /// <summary>
        /// Gets or sets the List templates.
        /// </summary>
        public ListTemplateCollection ListTemplates { get; set; } = new ListTemplateCollection();
    }
}
