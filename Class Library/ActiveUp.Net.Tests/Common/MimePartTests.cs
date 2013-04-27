using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests.Common
{
    [TestFixture]
    public class MimePartTests
    {
        private const string _textfilePath = @"resource\test_textfile.txt";
        private const string _imagefilePath = @"resource\test_picture.gif";
        private const string _textContentFileName = ".txt";
        private const string _binaryContentFileName = ".gif";

        [Test]
        public void should_initialize_from_byte_array_for_text_content()
        {
            var content = "My MIME part content";
            var binaryContent = Encoding.UTF8.GetBytes(content);

            var mimePart = new MimePart(binaryContent, _textContentFileName);

            mimePart.BinaryContent.ShouldEqual(binaryContent);
            mimePart.ContentType.MimeType.ShouldEqual("text/plain");
            mimePart.ContentDisposition.FileName.ShouldEqual("\".txt\"");
            mimePart.ContentName.ShouldEqual(".txt");
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.QuotedPrintable);
            mimePart.TextContent.ShouldEqual(content);
            mimePart.Charset.ShouldBeNull();
        }

        [Test]
        public void should_initialize_from_byte_array_for_binary_content()
        {
            var binaryContent = new byte[] { 01, 02, 03, 04, 05 };

            var mimePart = new MimePart(binaryContent, _binaryContentFileName);

            mimePart.BinaryContent.ShouldEqual(binaryContent);
            mimePart.ContentType.MimeType.ShouldEqual("image/gif");
            mimePart.ContentDisposition.FileName.ShouldEqual("\".gif\"");
            mimePart.ContentName.ShouldEqual(".gif");
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.Base64);
            mimePart.TextContent.ShouldEqual("AQIDBAU=");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void should_initialize_from_file(bool generateContentId)
        {
            var fileBytes = File.ReadAllBytes(_imagefilePath);
            
            var mimePart = new MimePart(_imagefilePath, generateContentId);

            mimePart.BinaryContent.ShouldEqual(fileBytes);
            mimePart.ContentType.MimeType.ShouldEqual("image/gif");
            mimePart.ContentDisposition.FileName.ShouldEqual("\"test_picture.gif\"");
            mimePart.ContentName.ShouldEqual("test_picture.gif");
            if (generateContentId)
                mimePart.ContentId.ShouldNotBeNull();
            else
                mimePart.ContentId.ShouldBeNull();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void should_initialize_from_file_with_charset(bool generateContentId)
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);

            var mimePart = new MimePart(_textfilePath, generateContentId, "ASCII");

            mimePart.BinaryContent.ShouldEqual(fileBytes);
            mimePart.ContentType.MimeType.ShouldEqual("text/plain");
            mimePart.ContentDisposition.FileName.ShouldEqual("\"test_textfile.txt\"");
            mimePart.ContentName.ShouldEqual("test_textfile.txt");
            mimePart.Charset.ShouldEqual("ASCII");
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.QuotedPrintable);
            mimePart.TextContent.ShouldEqual("Hello, World !");
            if (generateContentId)
                mimePart.ContentId.ShouldNotBeNull();
            else
                mimePart.ContentId.ShouldBeNull();
        }

        [Test]
        public void should_initialize_from_file_with_content_id()
        {
            var fileBytes = File.ReadAllBytes(_imagefilePath);
            const string contentId = "ContentId";

            var mimePart = new MimePart(_imagefilePath, contentId);

            mimePart.BinaryContent.ShouldEqual(fileBytes);
            mimePart.ContentType.MimeType.ShouldEqual("image/gif");
            mimePart.ContentDisposition.FileName.ShouldEqual("\"test_picture.gif\"");
            mimePart.ContentName.ShouldEqual("test_picture.gif");
            mimePart.Charset.ShouldBeNull();
            mimePart.ContentId.ShouldEqual("<" + contentId + ">");
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.Base64);
        }

        [Test]
        public void should_initialize_from_file_with_content_id_and_charset()
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);
            const string contentId = "ContentId";
            
            var mimePart = new MimePart(_textfilePath, contentId, "ASCII");

            mimePart.BinaryContent.ShouldEqual(fileBytes);
            mimePart.ContentType.MimeType.ShouldEqual("text/plain");
            mimePart.ContentDisposition.FileName.ShouldEqual("\"test_textfile.txt\"");
            mimePart.ContentName.ShouldEqual("test_textfile.txt");
            mimePart.Charset.ShouldEqual("ASCII");
            mimePart.ContentId.ShouldEqual("<" + contentId + ">");
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.QuotedPrintable);
        }

        [Test]
        public void should_save_to_file()
        {
            var binaryContent = Encoding.UTF8.GetBytes("My MIME part content");
            var mimePart = new MimePart(binaryContent, _textContentFileName);
            var tempFilePath = Path.GetTempFileName();

            var returnValue = mimePart.StoreToFile(tempFilePath);

            returnValue.ShouldEqual(returnValue);
            File.ReadAllBytes(tempFilePath).ShouldEqual(binaryContent);
        }

        [Test]
        public void should_erase_the_file_before_writing_in_it_to_remove_eventual_leftovers_when_overwriting()
        {
            var binaryContent = Encoding.UTF8.GetBytes("My MIME part content");
            var shortBinaryContent = Encoding.UTF8.GetBytes("My MIME part");
            var tempFilePath = Path.GetTempFileName();

            new MimePart(binaryContent, _textContentFileName).StoreToFile(tempFilePath);
            new MimePart(shortBinaryContent, _textContentFileName).StoreToFile(tempFilePath);

            File.ReadAllBytes(tempFilePath).ShouldEqual(shortBinaryContent);
        }

        [Test]
        public void should_generate_basic_header_string()
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);
            var mimePart = new MimePart(fileBytes, _textContentFileName);

            var headerString = mimePart.GetHeaderString();

            headerString.ShouldMatch("Content-Type: text/plain;\r\n");
            headerString.ShouldMatch("Content-Name: .txt");
            headerString.ShouldMatch("Content-Transfer-Encoding: quoted-printable");
            headerString.ShouldMatch("\r\n$");
        }

        [Test]
        public void should_generate_header_string_with_content_disposition()
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);
            var mimePart = new MimePart(fileBytes, _textContentFileName) { ContentDisposition = { Disposition = "foo" } };

            var headerString = mimePart.GetHeaderString();

            headerString.ShouldMatch("Content-Disposition: foo;\r\n");
        }

        [Test]
        public void should_not_repeat_content_disposition_nor_content_type_if_they_were_added_to_the_header_fields()
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);
            var mimePart = new MimePart(fileBytes, _textContentFileName) { ContentDisposition = { Disposition = "foo" } };
            mimePart.HeaderFields.Add("content-disposition", "foo");
            mimePart.HeaderFields.Add("content-type", "foo");

            var headerString = mimePart.GetHeaderString();

            Regex.Matches(headerString, "Content-Disposition").Count.ShouldEqual(1);
            Regex.Matches(headerString, "Content-Type").Count.ShouldEqual(1);
        }

        [Test]
        public void should_force_base64_encoding_if_specified()
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);
            var mimePart = new MimePart(fileBytes, _textContentFileName);

            var headerString = mimePart.GetHeaderString(forceBase64Encoding: true);

            headerString.ShouldMatch("Content-Transfer-Encoding: base64");
        }
    }
}