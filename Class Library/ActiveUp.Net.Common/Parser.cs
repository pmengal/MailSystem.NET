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

using ActiveUp.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
#if !PocketPC
using System.Security.Cryptography.Pkcs;
#endif
using System;
using System.Text;
using ActiveUp.Net.Security;
using System.IO;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// Base class for all Parser objects.
    /// </summary>
#if !PocketPC
	[System.Serializable]
#endif
    public class Parser
    {

        #region Methods

        #region Private and internal methods

        /// <summary>
        /// Gets the month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        internal static int GetMonth(string month)
        {
            switch (month)
            {
                case "Jan": return 1;
                case "Feb": return 2;
                case "Mar": return 3;
                case "Apr": return 4;
                case "May": return 5;
                case "Jun": return 6;
                case "Jul": return 7;
                case "Aug": return 8;
                case "Sep": return 9;
                case "Oct": return 10;
                case "Nov": return 11;
                case "Dec": return 12;
                default: return -1;
            }
        }

        /// <summary>
        /// Invs the get month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        internal static string InvGetMonth(int month)
        {
            switch (month)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return "???";
            }
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static ContentType GetContentType(string input)
        {
            ContentType field = new ContentType();
            field.MimeType = Regex.Match(input, @"(?<=: ?)\S+?(?=([;\s]|\Z))").Value;
            Match parammatch = Regex.Match(input, @"(?<=;\s+)[^;\s]*=[^;]*(?=(;|\Z))");
            while (parammatch.Success)
            {
                field.Parameters.Add(FormatFieldName(parammatch.Value.Substring(0, parammatch.Value.IndexOf('='))).ToLower(), parammatch.Value.Substring(parammatch.Value.IndexOf('=') + 1).Replace("\"", "").Trim('\r', '\n'));
                parammatch = parammatch.NextMatch();
            }
            return field;
        }

        /// <summary>
        /// Gets the content disposition.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static ContentDisposition GetContentDisposition(string input)
        {
            ContentDisposition field = new ContentDisposition();
            //TODO: include TAB detection in Regex
            field.Disposition = Regex.Match(input.Replace("\t", ""), @"(?<=: ?)\S+?(?=([;\s]|\Z))").Value;
            //TODO: include TAB detection in Regex
            System.Text.RegularExpressions.Match parammatch = System.Text.RegularExpressions.Regex.Match(input.Replace("\t", ""), @"(?<=;[ \t]?)[^;]*=[^;]*(?=(;|\Z))");
            for (; parammatch.Success; parammatch = parammatch.NextMatch()) field.Parameters.Add(FormatFieldName(parammatch.Value.Substring(0, parammatch.Value.IndexOf('='))), parammatch.Value.Substring(parammatch.Value.IndexOf('=') + 1).Replace("\"", "").Trim('\r', '\n'));
            return field;
        }

        /// <summary>
        /// Parses the sub parts.
        /// </summary>
        /// <param name="part">The part.</param>
        private static void ParseSubParts(ref MimePart part, Message message)
        {
            string boundary = part.ContentType.Parameters["boundary"];
            ActiveUp.Net.Mail.Logger.AddEntry("boundary : " + boundary);
            string[] arrpart = Regex.Split(part.OriginalContent, @"\r?\n?" + Regex.Escape("--" + boundary));
            for (int i = 1; i < arrpart.Length - 1; i++)
            {
                string strpart = arrpart[i];
                if (!strpart.StartsWith("--") && !string.IsNullOrEmpty(strpart))
                {
                    MimePart newpart = Parser.ParseMimePart(strpart, message);
                    newpart.ParentMessage = message;
                    newpart.Container = part;
                    part.SubParts.Add(newpart);
                }
            }
        }

        /// <summary>
        /// Dispatches the parts.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="message">The message.</param>
        private static void DispatchParts(MimePart root, ref Message message)
        {
            foreach (MimePart entity in root.SubParts)
            {
                DispatchPart(entity, ref message);
            }
        }

        /// <summary>
        /// Dispatches the parts.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void DispatchParts(ref Message message)
        {
            DispatchPart(message.PartTreeRoot, ref message);
        }

        private static void DispatchPart(MimePart part, ref Message message)
        {
            // This is a container part.
            if (part.SubParts.Count > 0) DispatchParts(part, ref message);
            // This is a leaf part.
            else
            {
                // If this part has to be displayed has an attachment, add it to the appropriate collection.
                if (part.ContentDisposition.Disposition.Equals("attachment")) message.Attachments.Add(part);
                // If this part has to be displayed at the same time as the main body, add it to the appropriate collection.
                else if (part.ContentDisposition.Disposition.Equals("inline")) message.EmbeddedObjects.Add(part);
                // Other parts are miscellaneous. How they are to be displayed is at the end-user's discretion.
                // Fix for avoid attach original mail message
                else if(!message.BodyText.ToMimePart().ContentTransferEncoding.Equals(part.ContentTransferEncoding))
                {
                    message.UnknownDispositionMimeParts.Add(part);
                }

                // We will consider the highest-level text parts that are not attachments to be the intended for display.
                // We know the highest-level parts will be set, because the parser first goes to the deepest level and returns top-level parts last.
                if (part.ContentType.Type.Equals("text") && !part.ContentDisposition.Disposition.Equals("attachment"))
                {
                    if (part.ContentType.SubType.Equals("plain"))
                    {
                        message.BodyText.Charset = part.Charset;
                        message.BodyText.Text = part.TextContent;
                    }
                    else if (part.ContentType.SubType.Equals("html"))
                    {
                        message.BodyHtml.Charset = part.Charset;
                        message.BodyHtml.Text = part.TextContent;
                    }
                }

                // Parse message/rfc822 parts as Message objects and place them in the appropriate collection.
                if (part.ContentType.MimeType.Equals("message/rfc822"))
                    message.SubMessages.Add(Parser.ParseMessage(part.BinaryContent));

                if (part.ContentType.MimeType.Equals("application/pkcs7-signature")
                    || part.ContentType.MimeType.Equals("application/x-pkcs7-signature"))
                {
                    string toDigest = part.Container.TextContent;
                    toDigest = Regex.Split(toDigest, "\r\n--" + part.Container.ContentType.Parameters["boundary"])[1];
                    toDigest = toDigest.TrimStart('\r', '\n');
                    //Match endDelimiter = Regex.Match(toDigest, "(?<=[^\r\n]\r\n)\r\n", RegexOptions.RightToLeft);
                    //int lastNonNewLine = Regex.Match(toDigest, "[^\r\n]", RegexOptions.RightToLeft).Index;
                    //if (endDelimiter.Index != -1 && endDelimiter.Index > lastNonNewLine) toDigest = toDigest.Remove(endDelimiter.Index);

                    //TODO: What should be done in PPC ?
#if !PocketPC
                    message.Signatures.Smime = new SignedCms(new ContentInfo(Encoding.ASCII.GetBytes(toDigest)), true);
                    message.Signatures.Smime.Decode(part.BinaryContent);
#endif
                }

                // Anyway, this is a leaf part of the message.
                message.LeafMimeParts.Add(part);
            }
        }

        /// <summary>
        /// Decodes the part body.
        /// </summary>
        /// <param name="part">The part.</param>
        private static void DecodePartBody(ref MimePart part)
        {
            // Let's see if a charset is specified. Otherwise we default to "iso-8859-1".
            string charset = (!string.IsNullOrEmpty(part.Charset) ? part.Charset : "iso-8859-1");

#if PocketPC
            if (charset.ToLower() == "iso-8859-1")
                charset = "windows-1252";
#endif
            // This is a Base64 encoded part body.
            if (part.ContentTransferEncoding.Equals(ContentTransferEncoding.Base64))
            {
#if !PocketPC
                try
                {
#endif
                    //We have the Base64 string so we can decode it.
                    part.BinaryContent = Convert.FromBase64String(part.TextContent);
#if !PocketPC
                }
                 catch (System.FormatException)
                {
                    part.TextContent = part.TextContent.Remove(part.TextContent.LastIndexOf("="));
                    part.BinaryContent = Convert.FromBase64String(part.TextContent);
                }
#endif

                if (part.ContentDisposition != ContentDisposition.Attachment)
                    part.TextContent = System.Text.Encoding.GetEncoding(charset).GetString(part.BinaryContent,0,part.BinaryContent.Length);
            }
            // This is a quoted-printable encoded part body.
            else if (part.ContentTransferEncoding.Equals(ContentTransferEncoding.QuotedPrintable))
            {
                //else if (part.Container != null && part.Container.Charset != null && part.Container.Charset.Length > 0)
                //    charset = part.Container.Charset;

                // Let's decode.
                part.TextContent = Codec.FromQuotedPrintable(part.TextContent, charset);
                // Knowing the charset, we can provide a binary version of this body data.
                part.BinaryContent = Encoding.GetEncoding(charset).GetBytes(part.TextContent);
            }
            // Otherwise, this is an unencoded part body and we keep the text version as it is.
            else
            {
                // Knowing the charset, we can provide a binary version of this body data.
                part.BinaryContent = Encoding.GetEncoding(charset).GetBytes(part.TextContent);
            }
        }

        /// <summary>
        /// Replaces the time zone.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static string ReplaceTimeZone(string input)
        {
            input = input.Replace("EDT", "-0400");
            input = input.Replace("EST", "-0500");
            input = input.Replace("CDT", "-0500");
            input = input.Replace("CST", "-0600");
            input = input.Replace("MDT", "-0600");
            input = input.Replace("MST", "-0700");
            input = input.Replace("PDT", "-0700");
            input = input.Replace("PST", "-0800");
            input = input.Replace("UT", "+0000");
            input = input.Replace("GMT", "+0000");
            return input;
        }

        /// <summary>
        /// Removes the white spaces.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        internal static string RemoveWhiteSpaces(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"\s+", "");
        }

        /// <summary>
        /// Formats the name of the field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        internal static string FormatFieldName(string fieldName)
        {
            return fieldName.ToLower().Trim();
        }

        /// <summary>
        /// Cleans the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        internal static string Clean(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"(\(((\\\))|[^)])*\))", "").Trim(' ');
        }

        #endregion

        #region Public methods

        #region Header folding

        /// <summary>
        /// Folds the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string Fold(string input)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] separated = input.Split(' ');
            string templine = string.Empty;
            for (int i = 0; i < separated.Length; i++)
            {
                if (templine.Length + separated[i].Length < 77) templine += separated[i] + " ";
                else
                {
                    sb.Append(templine + "\r\n ");
                    templine = string.Empty;
                }
            }
            sb.Append(templine);
            return sb.ToString();
        }

        /// <summary>
        /// Unfolds the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string Unfold(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"\r?\n(?=[ \t])", "");
        }

        #endregion
        
        #region Mime part parsing

        /// <summary>
        /// Delegate for body parsed event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="header">The header object.</param>
        public delegate void OnBodyParsedEvent(Object sender, Message message);

        /// <summary>
        /// Event handler for body parsed.
        /// </summary>
        public static event OnBodyParsedEvent BodyParsed;

        /// <summary>
        /// Parses the MIME part.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static MimePart ParseMimePart(string data, Message message)
        {
            MimePart part = new MimePart();
            part.ParentMessage = message;
            part.OriginalContent = data;

            try
            {
                // Separate header and body.
                int headerEnd = Regex.Match(data, @".(?=\r?\n\r?\n)").Index + 1;
                int bodyStart = Regex.Match(data, @"(?<=\r?\n\r?\n).").Index;

                //TODO: remove this workaround
                if (bodyStart == 0)
                {
                    //bodyStart = data.IndexOf("\r\n\r\n");
                    // Fix for a bug - the bodyStart was -1 (Invalid), MCALADO: 04/07/2008
                    int indexBody = data.IndexOf("\r\n\r\n");
                    if (indexBody > 0)
                    {
                        bodyStart = indexBody;
                    }
                }
                if (data.Length >= headerEnd)
                {

                    string header = data.Substring(0, headerEnd);

                    header = Parser.Unfold(header);
                    //header = header);

                    // The bodyStart need to be greather than data.length - MCALADO: 04/07/2008
                    string body = string.Empty;
                    if (bodyStart < data.Length)
                    {
                        body = data.Substring(bodyStart);
                    }

                    // Store the (maybe still encoded) body.
                    part.TextContent = body;




                    // Parse header fields and their parameters.
                    Match m = Regex.Match(header, @"(?<=((\r?\n)|\n)|\A)\S+:(.|(\r?\n[\t ]))+(?=((\r?\n)\S)|\Z)");
                    while (m.Success)
                    {
                        if (m.Value.ToLower().StartsWith("content-type:"))
                        {
                            part.ContentType = Parser.GetContentType(m.Value);
                        }
                        else if (m.Value.ToLower().StartsWith("content-disposition:"))
                        {
                            part.ContentDisposition = Parser.GetContentDisposition(m.Value);
                        }
                        part.HeaderFields.Add(FormatFieldName(m.Value.Substring(0, m.Value.IndexOf(':'))), Codec.RFC2047Decode(m.Value.Substring(m.Value.IndexOf(':') + 1).Trim(' ', '\r', '\n')));
                        part.HeaderFieldNames.Add(FormatFieldName(m.Value.Substring(0, m.Value.IndexOf(':'))), Codec.RFC2047Decode(m.Value.Substring(0, m.Value.IndexOf(':')).Trim(' ', '\r', '\n')));
                        m = m.NextMatch();
                    }

                    // Build the part tree.

                    // This is a container part.
                    if (part.ContentType.Type.ToLower().Equals("multipart"))
                    {
                        Parser.ParseSubParts(ref part, message);
                    }
                    // This is a nested message.
                    else if (part.ContentType.Type.ToLower().Equals("message"))
                    {
                        // TODO
                    }
                    // This is a leaf of the part tree
                    // Check necessary for single part emails (fix from alex294 on CodePlex)
                    // Why would we consider the body only to the first string?  Doesn't make sense - and fails
                    //else if (part.ContentType.Type.ToLower().Equals("text"))
                    //{
                    //    int BodyEnd = body.IndexOf(' ');
                    //    if (BodyEnd > 0)
                    //    {
                    //        part.TextContent = body.Substring(0, BodyEnd);
                    //    }
                    //}

                    DecodePartBody(ref part);

                    try
                    {
                        BodyParsed(null, message);
                    }
                    catch (Exception)
                    {
                        // event is not supported.
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ParsingException(ex.Message);
            }

            return part;
        }

        #endregion

        #region Header parsing

        /// <summary>
        /// Parses a Header from a file to a Header object.
        /// </summary>
        /// <param name="filePath">The path of the file to be parsed.</param>
        /// <returns>The parsed file as a Header object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Header Header = Parser.ParseHeader("C:\\My headers\\header.txt");
        /// //Expose the subject
        /// string subject = header.Subject;
        /// 
        /// VB.NET
        /// 
        /// Dim Header As Header = Parser.ParseHeader("C:\My headers\header.txt")
        /// 'Expose the subject
        /// Dim subject As String = header.Subject
        /// 
        /// JScript.NET
        /// 
        /// var header:Header = Parser.ParseHeader("C:\\My headers\\header.txt");
        /// //Expose the subject
        /// var subject:string = header.Subject;
        /// </code>
        /// </example> 
        public static Header ParseHeader(string filePath)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(filePath);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            Header hdr = new Header();
            hdr.OriginalData = data;
            Parser.ParseHeader(ref hdr);
            return hdr;
        }
        
        /// <summary>
        /// Parses a MemoryStream's content to a Header object.
        /// </summary>
        /// <param name="inputStream">The MemoryStream containing the Header data to be parsed.</param>
        /// <returns>The parsed Header as a Header object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Header Header = Parser.ParseHeader(someStream);
        /// //Expose the subject
        /// string subject = header.Subject;
        /// 
        /// VB.NET
        /// 
        /// Dim Header As Header = Parser.ParseHeader(someStream)
        /// 'Expose the subject
        /// Dim subject As String = header.Subject
        /// 
        /// JScript.NET
        /// 
        /// var header:Header = Parser.ParseHeader(someStream);
        /// //Expose the subject
        /// var subject:string = header.Subject;
        /// </code>
        /// </example> 
        public static Header ParseHeader(System.IO.MemoryStream inputStream)
        {
            byte[] buf = new byte[inputStream.Length];
            inputStream.Read(buf, 0, buf.Length);
            Header hdr = new Header();
            hdr.OriginalData = buf;
            Parser.ParseHeader(ref hdr);
            return hdr;
        }

        /// <summary>
        /// Parses a Header object from a byte array.
        /// </summary>
        /// <returns>The parsed Header as a Header object.</returns>
        /// <example>
        /// 	<code lang="CS">
        /// Header Header = Parser.ParseHeader(someBuffer);
        /// //Expose the subject
        /// string subject = header.Subject;
        ///     </code>
        /// 	<code lang="VB">
        /// Dim Header As Header = Parser.ParseHeader(someBuffer)
        /// 'Expose the subject
        /// Dim subject As String = header.Subject
        ///     </code>
        /// 	<code lang="J#">
        /// var header:Header = Parser.ParseHeader(someBuffer);
        /// //Expose the subject
        /// var subject:string = header.Subject;
        ///     </code>
        /// </example>
        /// <param name="data">The byte array containing the Header data to be parsed.</param>
        public static Header ParseHeader(byte[] data)
        {
            Header hdr = new Header();
            hdr.OriginalData = data;
            Parser.ParseHeader(ref hdr);
            return hdr;
        }

        /// <summary>
        /// Delegate for header field parsing event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="header">The header object.</param>
        public delegate void OnHeaderFieldParsingEvent(Object sender, Header header);

        /// <summary>
        /// Event handler for header field parsing.
        /// </summary>
        public static event OnHeaderFieldParsingEvent HeaderFieldParsing;

        /// <summary>
        /// Parses the header.
        /// </summary>
        /// <param name="header">The header.</param>
        public static void ParseHeader(ref Header header)
        {
#if !PocketPC
            string hdr = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(header.OriginalData,0,header.OriginalData.Length);
#else
            string hdr = Pop3Client.PPCEncode.GetString(header.OriginalData, 0, header.OriginalData.Length);
#endif
            hdr = System.Text.RegularExpressions.Regex.Match(hdr, @"[\s\S]+?((?=\r?\n\r?\n)|\Z)").Value;
            hdr = Parser.Unfold(hdr);
            //hdr = hdr);
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(hdr, @"(?<=((\r?\n)|\n)|\A)\S+:(.|(\r?\n[\t ]))+(?=((\r?\n)\S)|\Z)");
            while (m.Success)
            {
                string name = FormatFieldName(m.Value.Substring(0, m.Value.IndexOf(':')));
                string value = Codec.RFC2047Decode(m.Value.Substring(m.Value.IndexOf(":") + 1)).Trim('\r', '\n').TrimStart(' ');
                if (name.Equals("received")) header.Trace.Add(Parser.ParseTrace(m.Value.Trim(' ')));
                else if (name.Equals("to")) header.To = Parser.ParseAddresses(value);
                else if (name.Equals("cc")) header.Cc = Parser.ParseAddresses(value);
                else if (name.Equals("bcc")) header.Bcc = Parser.ParseAddresses(value);
                else if (name.Equals("reply-to")) header.ReplyTo = Parser.ParseAddress(value);
                else if (name.Equals("from")) header.From = Parser.ParseAddress(value);
                else if (name.Equals("sender")) header.Sender = Parser.ParseAddress(value);
                else if (name.Equals("content-type")) header.ContentType = Parser.GetContentType(m.Value);
                else if (name.Equals("content-disposition")) header.ContentDisposition = Parser.GetContentDisposition(m.Value);
                //else
                //{
                    header.HeaderFields.Add(name, value);
                    header.HeaderFieldNames.Add(name, m.Value.Substring(0, m.Value.IndexOf(':')));
                //}
                m = m.NextMatch();

                if (HeaderFieldParsing != null)
                    HeaderFieldParsing(null, header);
            }
        }

        /// <summary>
        /// Parses a Header from a string formatted accordingly to the RFC822.
        /// </summary>
        /// <param name="data">The string containing the Header data to be parsed.</param>
        /// <returns>The parsed message as a Header object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Header Header = Parser.ParseHeaderString(rfc822string);
        /// //Expose the subject
        /// string subject = header.Subject;
        /// 
        /// VB.NET
        /// 
        /// Dim Header As Header = Parser.ParseHeaderString(rfc822string)
        /// 'Expose the subject
        /// Dim subject As String = header.Subject
        /// 
        /// JScript.NET
        /// 
        /// var header:Header = Parser.ParseHeaderString(rfc822string);
        /// //Expose the subject
        /// var subject:string = header.Subject;
        /// </code>
        /// </example>
        public static Header ParseHeaderString(string data)
        {

#if !PocketPC
            return Parser.ParseHeader(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data));
#else
            return Parser.ParseHeader(Pop3Client.PPCEncode.GetBytes(data));
#endif
        }

        #endregion

        #region Message parsing

        /// <summary>
        /// Delegate for OnErrorParsingEvent.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="header">The exception object.</param>
        public delegate void OnErrorParsingEvent(Object sender, Exception ex);

        /// <summary>
        /// Event handler for error parsing.
        /// </summary>
        public static event OnErrorParsingEvent ErrorParsing;

        /// <summary>
        /// Parses the message.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static Message ParseMessage(byte[] data)
        {
            //string msg = System.Text.Encoding.ASCII.GetString(data);
#if !PocketPC
            string msg = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(data,0,data.Length);
#else
            string msg = Pop3Client.PPCEncode.GetString(data, 0, data.Length);
#endif

            Message message = new Message();

            try
            {
                // Build a part tree and get all headers. 
                MimePart part = ParseMimePart(msg, message);

                // Fill a new message object with the new information.
                message.OriginalData = data;
                message.HeaderFields = part.HeaderFields;
                message.HeaderFieldNames = part.HeaderFieldNames;

                // Dispatch header fields to corresponding object.
                foreach (string key in message.HeaderFields.AllKeys)
                {
                    string name = key;
                    string value = message.HeaderFields[key];
                    // TODO : Fix trace
                    if (name.Equals("received")) message.Trace.Add(Parser.ParseTrace(key + ": " + value));
                    else if (name.Equals("to")) message.To = Parser.ParseAddresses(value);
                    else if (name.Equals("cc")) message.Cc = Parser.ParseAddresses(value);
                    else if (name.Equals("bcc")) message.Bcc = Parser.ParseAddresses(value);
                    else if (name.Equals("reply-to")) message.ReplyTo = Parser.ParseAddress(value);
                    else if (name.Equals("from")) message.From = Parser.ParseAddress(value);
                    else if (name.Equals("sender")) message.Sender = Parser.ParseAddress(value);
                    else if (name.Equals("content-type")) message.ContentType = Parser.GetContentType(key + ": " + value);
                    else if (name.Equals("content-disposition")) message.ContentDisposition = Parser.GetContentDisposition(key + ": " + value);
                    else if (name.Equals("domainkey-signature")) message.Signatures.DomainKeys = Signature.Parse(key + ": " + value, message);
                }

                if (message.ContentType.MimeType.Equals("application/pkcs7-mime")
                    || message.ContentType.MimeType.Equals("application/x-pkcs7-mime"))
                {
                    if (message.ContentType.Parameters["smime-type"] != null
                        && message.ContentType.Parameters["smime-type"].Equals("enveloped-data")) message.IsSmimeEncrypted = true;
                    if (message.ContentType.Parameters["smime-type"] != null
                        && message.ContentType.Parameters["smime-type"].Equals("signed-data")) message.HasSmimeSignature = true;
                }

                if (message.ContentType.MimeType.Equals("multipart/signed")) message.HasSmimeDetachedSignature = true;

                // Keep a reference to the part tree within the new Message object.
                message.PartTreeRoot = part;

                DispatchParts(ref message);

                // Dispatch the part tree content to the appropriate collections and properties.
                // TODO

            }
            catch (Exception ex)
            {
                if (ErrorParsing != null)
                    ErrorParsing(null, ex);
            }
            return message;

        }

        /// <summary>
        /// Parses a MemoryStream's content to a Message object.
        /// </summary>
        /// <param name="inputStream">The MemoryStream containing the Header data to be parsed.</param>
        /// <returns>The parsed Header as a Message object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = Parser.ParseMessage(someStream);
        /// //Expose the subject
        /// string subject = message.Subject;
        /// 
        /// VB.NET
        /// 
        /// Dim message As Message = Parser.ParseMessage(someStream)
        /// 'Expose the subject
        /// Dim subject As String = message.Subject
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = Parser.ParseMessage(someStream);
        /// //Expose the subject
        /// var subject:string = message.Subject;
        /// </code>
        /// </example>
        public static Message ParseMessage(System.IO.MemoryStream inputStream)
        {
            byte[] buf = new byte[inputStream.Length];
            inputStream.Read(buf, 0, buf.Length);
            Message msg = new Message();
            msg.OriginalData = buf;
            Parser.ParseMessage(buf);//ref msg);
            return msg;
        }
                
        /// <summary>
        /// Parses a Message from a string formatted accordingly to the RFC822.
        /// </summary>
        /// <param name="data">The string containing the message data to be parsed.</param>
        /// <returns>The parsed message as a Message object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = Parser.ParseMessageString(rfc822string);
        /// //Expose the subject
        /// string subject = message.Subject;
        /// 
        /// VB.NET
        /// 
        /// Dim message As Message = Parser.ParseMessageString(rfc822string)
        /// 'Expose the subject
        /// Dim subject As String = message.Subject
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = Parser.ParseMessageString(rfc822string);
        /// //Expose the subject
        /// var subject:string = message.Subject;
        /// </code>
        /// </example>
        public static Message ParseMessage(string data)
        {
#if !PocketPC
            return Parser.ParseMessage(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data));
#else
            return Parser.ParseMessage(Pop3Client.PPCEncode.GetBytes(data));
#endif
        }

        /// <summary>
        /// Parses a message from a file to a Message object.
        /// </summary>
        /// <param name="filePath">The path of the file to be parsed.</param>
        /// <returns>The parsed message as a Message object.</returns>
        /// <example>
        /// <code>
        /// C#
        /// 
        /// Message message = Parser.ParseMessage("C:\\My messages\\message.txt");
        /// //Expose the subject
        /// string subject = message.Subject;
        /// 
        /// VB.NET
        /// 
        /// Dim message As Message = Parser.ParseMessage("C:\My messages\message.txt")
        /// 'Expose the subject
        /// Dim subject As String = message.Subject
        /// 
        /// JScript.NET
        /// 
        /// var message:Message = Parser.ParseMessage("C:\\My messages\\message.txt");
        /// //Expose the subject
        /// var subject:string = message.Subject;
        /// </code>
        /// </example> 
        public static Message ParseMessageFromFile(string filePath)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(filePath);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return Parser.ParseMessage(data);
        }

        #endregion

        #region Address parsing

        /// <summary>
        /// Parses a string containing addresses in the following formats :
        /// <list type="circle">
        /// <item>"John Doe" &lt;jdoe@myhost.com>,"Mike Johns" &lt;mjohns@otherhost.com></item>
        /// <item>"John Doe" &lt;jdoe@myhost.com>;"Mike Johns" &lt;mjohns@otherhost.com></item>
        /// <item>&lt;jdoe@myhost.com></item>
        /// <item>jdoe@myhost.com</item>
        /// </list>
        /// </summary>
        /// <param name="input">A string containing addresses in the formats desribed above.</param>
        /// <returns>An AddressCollection object containing the parsed addresses.</returns>
        public static AddressCollection ParseAddresses(string input)
        {
            //TODO: enforce parser to use regex
            AddressCollection addresses = new AddressCollection();

            string[] comma_separated = input.Split(',');
            for (int i = 0; i < comma_separated.Length; i++)
                if (comma_separated[i].IndexOf("@") == -1 && comma_separated.Length > (i + 1))
                    comma_separated[i + 1] = comma_separated[i] + comma_separated[i + 1];

            for (int i = 0; i < comma_separated.Length; i++) if(comma_separated[i].IndexOf("@")!=-1)
                addresses.Add(Parser.ParseAddress((comma_separated[i].IndexOf("<") != -1 && comma_separated[i].IndexOf(":") != -1 && comma_separated[i].IndexOf(":") < comma_separated[i].IndexOf("<")) ? ((comma_separated[i].Split(':')[0].IndexOf("\"") == -1) ? comma_separated[i].Split(':')[1] : comma_separated[i]) : comma_separated[i]));

            //MatchCollection matches = Regex.Matches(input, "(\"(?<name>.+?)\")*\\s*<?(?<email>[^<>,\"\\s]+)>?");

            //foreach (Match m in matches)
            //    addresses.Add(m.Groups["email"].Value, m.Groups["name"].Value);

            return addresses;
        }

        /// <summary>
        /// Parses the address.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static Address ParseAddress(string input)
        {
            Address address = new Address();
            input = input.TrimEnd(';');
            try
            {
                if (input.IndexOf("<") == -1) address.Email = Parser.RemoveWhiteSpaces(input);
                else
                {
                    address.Email = System.Text.RegularExpressions.Regex.Match(input, "<(.|[.])*>").Value.TrimStart('<').TrimEnd('>');
                    address.Name = input.Replace("<" + address.Email + ">", "");
                    address.Email = Parser.Clean(Parser.RemoveWhiteSpaces(address.Email));
                    if (address.Name.IndexOf("\"") == -1) address.Name = Parser.Clean(address.Name);
                    address.Name = address.Name.Trim(new char[] { ' ', '\"' });
                }
                return address;
            }
            catch { return new Address(input); }
        }

        #endregion

        #region Date parsing

        /// <summary>
        /// Parses as universal date time.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static System.DateTime ParseAsUniversalDateTime(string input)
        {
            try
            {
                input = Parser.ReplaceTimeZone(input);
                input = Parser.Clean(input);
                input = System.Text.RegularExpressions.Regex.Replace(input, @" +", " ");
                input = System.Text.RegularExpressions.Regex.Replace(input, @"( +: +)|(: +)|( +:)", ":");
                if (input.IndexOf(",") != -1)
                {
                    input = input.Replace(input.Split(',')[0] + ", ", "");
                }
                string[] parts = input.Replace("\t", string.Empty).Split(' ');
                int year = System.Convert.ToInt32(parts[2]);
                if (year < 100)
                {
                    if (year > 49) year += 1900;
                    else year += 2000;
                }
                int month = Parser.GetMonth(parts[1]);
                int day = System.Convert.ToInt32(parts[0]);
                string[] dateParts = parts[3].Split(':');
                int hour = System.Convert.ToInt32(dateParts[0]);
                int minute = System.Convert.ToInt32(dateParts[1]);
                int second = 0;
                if (dateParts.Length > 2) second = System.Convert.ToInt32(dateParts[2]);
                int offset_hours = System.Convert.ToInt32(parts[4].Substring(0, 3));
                int offset_minutes = System.Convert.ToInt32(parts[4].Substring(3, 2));
                System.DateTime date = new System.DateTime(year, month, day, hour, minute, second);
                date = date.AddHours(-offset_hours);
                date = date.AddMinutes(-offset_minutes);
                return date;
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region Trace parsing

        /// <summary>
        /// Parses the trace.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static TraceInfo ParseTrace(string input)
        {
            TraceInfo traceInfo = new TraceInfo();
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(input, @"from.+?(?=(from|by|via|with|for|id|;|\r?\n))");
            if (m.Success) traceInfo.From = m.Value.Trim(' ','\t');
            m = System.Text.RegularExpressions.Regex.Match(input, @"(?<=by ).+?(?= ?(from|by|via|with|for|id|;|\r?\n))");
            if (m.Success) traceInfo.By = m.Value.Trim(' ', '\t');
            m = System.Text.RegularExpressions.Regex.Match(input, @"(?<=via ).+?(?= ?(from|by|via|with|for|id|;|\r?\n))");
            if (m.Success) traceInfo.Via = m.Value.Trim(' ', '\t');
            m = System.Text.RegularExpressions.Regex.Match(input, @"(?<=with ).+?(?= ?(from|by|via|with|for|id|;|\r?\n))");
            if (m.Success) traceInfo.With = m.Value.Trim(' ', '\t');
            m = System.Text.RegularExpressions.Regex.Match(input, @"(?<=for ).+?(?= ?(from|by|via|with|for|id|;|\r?\n))");
            if (m.Success) traceInfo.For = m.Value.Trim(' ', '\t');
            m = System.Text.RegularExpressions.Regex.Match(input, @"(?<=id ).+?(?= ?(from|by|via|with|for|id|;|\r?\n))");
            if (m.Success) traceInfo.Id = m.Value.Trim(' ', '\t');
            traceInfo.Date = Parser.ParseAsUniversalDateTime(input.Substring(input.LastIndexOf(';') + 1));
            return traceInfo;
        }

        /// <summary>
        /// Parses the traces.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static TraceInfoCollection ParseTraces(string[] input)
        {
            string itemlow;
            TraceInfoCollection traceinfos = new TraceInfoCollection();
            TraceInfo traceinfo = new TraceInfo();
            try
            {
                foreach (string item in input)
                {
                    traceinfo = new TraceInfo();
                    //item = " " + Parser.Clean(item1);
                    itemlow = item.ToLower();
                    if (itemlow.IndexOf(" from ") != -1) traceinfo.From = item.Substring(itemlow.IndexOf(" from ") + 6, item.IndexOf(" ", itemlow.IndexOf(" from ") + 6) - (itemlow.IndexOf(" from ") + 6)).TrimEnd(';');
                    if (itemlow.IndexOf(" by ") != -1) traceinfo.By = item.Substring(itemlow.IndexOf(" by ") + 4, item.IndexOf(" ", itemlow.IndexOf(" by ") + 4) - (itemlow.IndexOf(" by ") + 4)).TrimEnd(';');
                    if (itemlow.IndexOf(" for ") != -1) traceinfo.For = item.Substring(itemlow.IndexOf(" for ") + 5, item.IndexOf(" ", itemlow.IndexOf(" for ") + 5) - (itemlow.IndexOf(" for ") + 5)).TrimEnd(';');
                    if (itemlow.IndexOf(" id ") != -1) traceinfo.Id = item.Substring(itemlow.IndexOf(" id ") + 4, item.IndexOf(" ", itemlow.IndexOf(" id ") + 4) - (itemlow.IndexOf(" id ") + 4)).TrimEnd(';');
                    if (itemlow.IndexOf(" via ") != -1) traceinfo.Via = item.Substring(itemlow.IndexOf(" via ") + 5, item.IndexOf(" ", itemlow.IndexOf(" via ") + 5) - (itemlow.IndexOf(" via ") + 5)).TrimEnd(';');
                    if (itemlow.IndexOf(" with ") != -1) traceinfo.With = item.Substring(itemlow.IndexOf(" with ") + 6, item.IndexOf(" ", itemlow.IndexOf(" with ") + 6) - (itemlow.IndexOf(" with ") + 6)).TrimEnd(';');
                    traceinfo.Date = Parser.ParseAsUniversalDateTime(item.Split(';')[item.Split(';').Length - 1].Trim(' '));
                    traceinfos.Add(traceinfo);
                }
            }
            catch { };
            return traceinfos;
        }

        #endregion

        #endregion

        #endregion

    }
}