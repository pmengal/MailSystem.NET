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

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections.Specialized;
#if !PocketPC
using System.Security.Cryptography.Pkcs;
#endif
using System;
using System.Linq;

namespace ActiveUp.Net.Mail 
{
    /// <summary>
	/// The base class used by the Attachment and EmbeddedObject classes.
	/// </summary>
#if !PocketPC
	[Serializable]
#endif
    public class MimePart
    {
        public MimePart()
		{
            ContentDisposition = new ContentDisposition();
            ContentType = new ContentType();
            BinaryContent = new byte[0];
            HeaderFields = new NameValueCollection();
            HeaderFieldNames = new NameValueCollection();
            SubParts = new MimePartCollection();
		}

        public MimePart(byte[] attachment, string fileExtension)
            : this(attachment, MimeTypesHelper.GetMimeqType(fileExtension), fileExtension)
        {
        }

        /// <summary>
        /// Creates a MimePart object with the content of the file located at the given path.
        /// </summary>
        /// <param name="path">File containing the content of the MimePart.</param>
        /// <param name="generateContentId">If true, a Content-ID Header field will be added to allow referencing of this part in the message.</param>
        /// <param name="charset">If the file contains text, the charset of the text can be provided to ensure better handling.</param>
        public MimePart(string path, bool generateContentId, string charset = null)
            : this(File.ReadAllBytes(path), MimeTypesHelper.GetMimeqType(Path.GetExtension(path)), Path.GetFileName(path), charset)
        {
            if (generateContentId)
                SetContentId();
        }

        /// <summary>
        /// Creates a MimePart object with the content of the file located at the given path.
        /// </summary>
        /// <param name="path">File containing the content of the MimePart.</param>
        /// <param name="contentId">The Content-ID Header field will be used for the part.</param>
        /// <param name="charset">If the file contains text, the charset of the text can be provided to ensure better handling.</param>
        public MimePart(string path, string contentId, string charset = null)
            : this(File.ReadAllBytes(path), MimeTypesHelper.GetMimeqType(Path.GetExtension(path)), Path.GetFileName(path), charset)
        {
            ContentId = contentId;
        }

        private MimePart(byte[] content, string mimeType, string fileName, string charset = null)
            : this()
        {
            BinaryContent = content;
            ContentType.MimeType = mimeType;
            ContentDisposition.FileName = fileName;
            ContentName = fileName;

            BuildTextContent(charset);
        }

        private void BuildTextContent(string charset = null)
        {
            if (IsText)
            {
                Charset = charset;
                ContentTransferEncoding = ContentTransferEncoding.QuotedPrintable;
                TextContent = Encoding.GetEncoding(charset ?? Encoding.UTF8.BodyName).GetString(BinaryContent, 0, BinaryContent.Length);
                return;
            }
            
            ContentTransferEncoding = ContentTransferEncoding.Base64;
            TextContent = Convert.ToBase64String(BinaryContent);
        }

        /// <summary>
		/// Generates a new Content-ID for the part.
		/// </summary>
		public void SetContentId()
		{
			ContentId = "AMLv2" + Codec.GetUniqueString() + "@" + System.Net.Dns.GetHostName();
		}

		/// <summary>
		/// Set a new Content-ID for the part.
		/// </summary>
		public void SetContentId(string contentID)
		{
			ContentId = contentID;
		}

        public string GetCidReference()
        {
            return "cid:" + ContentId.Trim('<','>');
        }

        /// <summary>
        /// Stores the part's BinaryContent to the specified path.
        /// </summary>
        /// <param name="destinationPath">The path the part has to be saved at.</param>
        /// <returns></returns>
        public string StoreToFile(string destinationPath)
        {
            File.WriteAllBytes(destinationPath, BinaryContent);
            return destinationPath;
        }

        /// <summary>
        /// The MIME string.
        /// </summary>
        /// <param name="forceBase64Encoding">if set to <c>true</c> forces inner elements to be base64 encoded</param>
        /// <returns></returns>
        public string ToMimeString(bool forceBase64Encoding = false)
        {
            var content = string.Empty;
            if (ContentType.Type != "multipart")
            {
                content = forceBase64Encoding ? Base64EncodeAndWrap() : TextContentTransferEncoded;
                return GetHeaderString(forceBase64Encoding) + Codec.CrLf + content;
            }

            InitializeBoundaryIfNotProvided();
            var boundary = ContentType.Parameters["boundary"];
            content += GetHeaderString(forceBase64Encoding);

            foreach (MimePart subpart in SubParts)
            {
                content += "\r\n\r\n--" + boundary + Codec.CrLf;
                content += subpart.ToMimeString(forceBase64Encoding);
            }

            content += "\r\n\r\n" + "--" + boundary + "--" + Codec.CrLf;

            return content;
        }

        private void InitializeBoundaryIfNotProvided()
        {
            if (!string.IsNullOrEmpty(ContentType.Parameters["boundary"]))
                return;

            // TODO: Factorize the boundary name
            var newBoundary = "---AU_MimePart_" + Codec.GetUniqueString();
            ContentType.Parameters.Add("boundary", newBoundary);
        }

#if !PocketPC
        public static MimePart GetSignaturePart(SignedCms cms)
        {
            if (!cms.Detached)
                throw new ArgumentException("The CMS object is not a detached signature.");

            var part = new MimePart();

            part.ContentType.MimeType = "application/x-pkcs7-signature";
            part.ContentType.Parameters.Add("name", "\"smime.p7s\"");
            part.ContentTransferEncoding = ContentTransferEncoding.Base64;
            part.ContentDisposition.Disposition = "attachment";
            part.ContentDisposition.FileName = "smime.p7s";

            part.BinaryContent = cms.Encode();

            return part;
        }
#endif
        public string GetHeaderString(bool forceBase64Encoding = false)
        {
            var builder = new StringBuilder();
            builder.Append(ContentType + Codec.CrLf);

            if (ContentDisposition.Disposition.Length > 0)
                builder.Append(ContentDisposition + Codec.CrLf);

            AppendContentEncoding(builder, forceBase64Encoding);

            var headersToAppend = HeaderFields.AllKeys.Except(new[] { "content-type", "content-disposition", "content-transfer-encoding" });
            AppendGivenHeaderFields(builder, headersToAppend);
            
            return builder.ToString().Trim('\r', '\n') + Codec.CrLf;
        }

        private void AppendContentEncoding(StringBuilder builder, bool forceBase64Encoding)
        {
            var headerValue = forceBase64Encoding ? "base64" : HeaderFields["content-transfer-encoding"];
            builder.AppendFormat("{0}: {1}\r\n", Codec.GetFieldName("content-transfer-encoding"), headerValue);
        }

        private void AppendGivenHeaderFields(StringBuilder builder, IEnumerable<string> headerNames)
        {
            foreach (var headerName in headerNames)
                builder.AppendFormat("{0}: {1}\r\n", Codec.GetFieldName(headerName), HeaderFields[headerName]);
        }


        public MimePartCollection SubParts { get; set; }

        public Message ParentMessage { get; set; }

        /// <summary>
		/// All Header fields names.
		/// Key is the Header name in lower case and value is the Header name as it was it the original message.
		/// </summary>
        public NameValueCollection HeaderFieldNames { get; set; }

        /// <summary>
        /// All Header fields.
        /// Key is the Header name in lower case and value is the Header value.
        /// </summary>
        /// <example>
        /// //This exposes the mail client used to send the message.
        /// message.HeaderFields["x-mailer"]
        /// </example>
        public NameValueCollection HeaderFields { get; set; }
        
		/// <summary>
		/// The ContentName.
		/// </summary>
		public string ContentName
		{
            get
            {
                if (HeaderFields["content-name"] != null)
                    return HeaderFields.GetValues("content-name")[0];
                if (ContentType != null && ContentType.Parameters["name"] != null)
                    return ContentType.Parameters["name"];
                return null;
            }
            set
            {
                HeaderFields["content-name"] = value;
            }
		}

		/// <summary>
		/// The ContentId.
		/// </summary>
		public string ContentId
		{
			get
			{
                if (HeaderFields["content-id"] != null)
                    return "<" + HeaderFields.GetValues("content-id")[0].Trim('<', '>') + ">";
				return null;
			}
			set
			{
                HeaderFields["content-id"] = "<" + value.Trim('<', '>') + ">";
			}
		}

        public string EmbeddedObjectLink
        {
            get
            {
                if (EmbeddedObjectContentId != null)
                    return "cid:" + EmbeddedObjectContentId;
                return null;
            }
        }

        public string EmbeddedObjectContentId
        {
            get
            {
                if (HeaderFields["content-id"] != null)
                    return HeaderFields.GetValues("content-id")[0].Trim('<', '>');
                return null;
            }
        }

		/// <summary>
		/// The Content-Description.
		/// </summary>
		public string ContentDescription
		{
			get
			{
				if(HeaderFields["content-description"] != null)
                    return HeaderFields.GetValues("content-description")[0];
				return null;
			}
			set
			{
                HeaderFields["content-description"] = value;
			}
		}

		/// <summary>
		/// The text content of a MIME Part.
		/// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// The text content of a MIME Part.
        /// </summary>
        public string TextContentTransferEncoded
        {
           get
            {
                if (ContentTransferEncoding == ContentTransferEncoding.SevenBits)
                    return TextContent;

                if (ContentTransferEncoding == ContentTransferEncoding.Base64)
                    return Base64EncodeAndWrap();

                if (IsText)
                    return Codec.ToQuotedPrintable(TextContent, Charset ?? "us-ascii");
                
                if (MimeType.Contains("message/") || MimeType.Contains("image/") || MimeType.Contains("application/"))
                    return TextContent;
                
                return Codec.Wrap(Convert.ToBase64String(BinaryContent), 77);
            }
        }

        private string Base64EncodeAndWrap()
        {
            if (IsBinary)
                return Codec.Wrap(Convert.ToBase64String(BinaryContent), 78);
            return Codec.Wrap(Convert.ToBase64String(Encoding.GetEncoding(Charset ?? Encoding.UTF8.BodyName).GetBytes(TextContent ?? string.Empty)), 78);
        }

        /// <summary>
        /// The multipart typed part containing this part.
        /// </summary>
        public MimePart Container { get; set; }

        /// <summary>
        /// The original content of a parsed MIME Part.
        /// </summary>
        public string OriginalContent { get; set; }
		
        /// <summary>
		/// The Content-Type of the MimePart.
		/// </summary>
        public ContentType ContentType { get; set; }

        public string MimeType
        {
            get { return ContentType.MimeType.ToLower(); }
        }

        public bool IsText
        {
            get { return MimeType.Contains("text/"); }
        }

		/// <summary>
		/// The Charset of the MimePart.
		/// </summary>
		public string Charset
		{
			get
			{
                var result = ContentType.Parameters["charset"];
                //PocketPC may or may not support iso-8859 depending on their region. Hence we take codePage 1252 as standard which is
                //superset of iso-8859
#if PocketPC
                if (result != null && result.ToLower().IndexOf("iso-8859") > -1)
                {
                    result = "windows-1252";
                }
#endif
                return result;
            }
			set
			{
                ContentType.Parameters["charset"] = value;
			}
		}
		
		/// <summary>
		/// The Content-Disposition of the MimePart.
		/// </summary>
        public ContentDisposition ContentDisposition { get; set; }
		
        /// <summary>
		/// The Content-Transfer-Encoding of the MimePart.
		/// </summary>
		public ContentTransferEncoding ContentTransferEncoding
		{
			get
			{
			    switch (HeaderFields["content-transfer-encoding"])
			    {
			        case "quoted-printable":
                        return ContentTransferEncoding.QuotedPrintable;
                    case "base64":
                        return ContentTransferEncoding.Base64;
                    case "8bit":
                        return ContentTransferEncoding.EightBits;
                    case "7bit":
                        return ContentTransferEncoding.SevenBits;
                    case "binary":
                        return ContentTransferEncoding.Binary;
                    default:
                        return ContentTransferEncoding.Unknown;
			    }
			}
			set
			{
			    switch (value)
			    {
			        case ContentTransferEncoding.Binary:
			            HeaderFields["content-transfer-encoding"] = "binary";
			            break;
			        case ContentTransferEncoding.QuotedPrintable:
                        HeaderFields["content-transfer-encoding"] = "quoted-printable";
			            break;
			        case ContentTransferEncoding.SevenBits:
                        HeaderFields["content-transfer-encoding"] = "7bit";
			            break;
			        case ContentTransferEncoding.EightBits:
                        HeaderFields["content-transfer-encoding"] = "8bit";
			            break;
			        default:
                        HeaderFields["content-transfer-encoding"] = "base64";
			            break;
			    }
			}
		}

		/// <summary>
		/// The binary data of the part.
		/// </summary>
        public byte[] BinaryContent { get; set; }

        public bool IsBinary { get { return BinaryContent.Length > 0; } }

		/// <summary>
		/// The Content-Location.
		/// </summary>
		public string ContentLocation
		{
			get { return HeaderFields["content-location"]; }
		    set { HeaderFields["content-location"] = value; }
        }

        /// <summary>
        /// Approximate size of the file in octets.
        /// </summary>
        public int Size
        {
            get  { return IsBinary ? BinaryContent.Length : TextContent.Length; }
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value>The filename.</value>
        public string Filename
        {
            get
            {
                var filename = string.Empty;

                if (HeaderFields["filename"] != null)
                    filename = HeaderFields.GetValues("filename")[0];
                else if (ContentDisposition != null && ContentDisposition.FileName != null)
                    filename = ContentDisposition.FileName;
                else if (ContentDisposition.Parameters["filename"] != null)
                    filename = ContentDisposition.Parameters["filename"];
                else if (!string.IsNullOrEmpty(ContentName))
                    filename = ContentName;

                filename = filename.Replace("\"", string.Empty);

#if TRIAL
                return ProductHelper.GetTrialString(Codec.RFC2047Decode(filename), TrialStringType.ShortText);
#else
                return Codec.RFC2047Decode(filename);
#endif
            }
            set
            {
                if (HeaderFields["filename"] != null)
                    HeaderFields["filename"] = value;
                else AddHeaderField("filename", value);

                ContentDisposition.FileName = value;
            }
        }

        /// <summary>
        /// Adds the header field.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        private void AddHeaderField(string name, string value)
        {
            var key = name.ToLower();
            HeaderFields[key] = value;
            HeaderFieldNames[key] = name;
        }
    }
}