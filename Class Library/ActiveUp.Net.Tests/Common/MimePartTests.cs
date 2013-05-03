using System;
using System.Collections.Specialized;
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
        public void should_generate_unique_ContentId()
        {
            var firstContentId = new MimePart(_textfilePath, true).ContentId;
            var secondContentId = new MimePart(_textfilePath, true).ContentId;

            firstContentId.ShouldMatch("^<AMLv2[0-9]*@[^>]*>$");
            secondContentId.ShouldMatch("^<AMLv2[0-9]*@[^>]*>$");
            firstContentId.ShouldNotEqual(secondContentId);
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
        public void should_generage_header_string_with_contentId()
        {
            var mimePart = new MimePart(_textfilePath, true);

            var headerString = mimePart.GetHeaderString();

            headerString.ShouldMatch("Content-ID: <AMLv2[0-9]*@[^>]*>\r\n");
        }

        [Test]
        public void should_force_base64_encoding_if_specified()
        {
            var fileBytes = File.ReadAllBytes(_textfilePath);
            var mimePart = new MimePart(fileBytes, _textContentFileName);

            var headerString = mimePart.GetHeaderString(forceBase64Encoding: true);

            headerString.ShouldMatch("Content-Transfer-Encoding: base64");
        }

        [Test]
        public void should_handle_parts_with_no_charset_and_empty_binary_content_when_forcing_base64()
        {
            var mimePart = new MimePart(new byte[0], _textContentFileName) { TextContent = null };

            Assert.DoesNotThrow(() => mimePart.ToMimeString(true));
        }

        [Test]
        public void should_serialize_single_part_message_to_mime_string()
        {
            var textFileContent = File.ReadAllText(_textfilePath);
            var mimePart = new MimePart(_textfilePath, false);

            var mimeString = mimePart.ToMimeString();

            mimeString.ShouldMatch("Content-Type: text/plain;\r\n");
            mimeString.ShouldMatch(@"\r\n" + textFileContent + "$");
        }

        [Test]
        public void should_use_existing_boundary_if_provided_when_generating_mime_string()
        {
            var mimePart = CreateMultipartMimePart("MyAmazingBoundary");

            var mimeString = mimePart.ToMimeString();

            mimeString.ShouldMatch(@"	boundary=""MyAmazingBoundary""\r\n");
            Regex.Matches(mimeString, "MyAmazingBoundary").Count.ShouldEqual(4);
        }

        private static MimePart CreateMultipartMimePart(string boundaryOverride = null)
        {
            return new MimePart
                {
                    ContentType =
                        {
                            MimeType = "multipart/mixed",
                            Parameters = { { "boundary", boundaryOverride } }
                        },
                    SubParts = new MimePartCollection
                        {
                            new MimePart(_textfilePath, true),
                            new MimePart(_imagefilePath, false)
                        }
                };
        }

        [Test]
        public void should_serialize_multipart_message_to_mime_string()
        {
            var mimePart = CreateMultipartMimePart();

            var subParts = mimePart.ToMimeString().SplitMimeParts();

            subParts.Length.ShouldEqual(3);
            var messagePart = subParts[0];
            messagePart.ShouldMatch("^Content-Type: multipart/mixed;\r\n");
            messagePart.ShouldMatch(@"	boundary=""---AU_MimePart_[0-9]*""\r\n");
            var textPart = subParts[1];
            textPart.ShouldMatch("^Content-Type: text/plain;\r\n");
            textPart.ShouldMatch("Hello, World !\r\n\r\n$");
            var imagePart = subParts[2];
            imagePart.ShouldMatch("^Content-Type: image/gif");
            imagePart.ShouldMatch("-----AU_MimePart_[0-9]*--\r\n$");
        }

        [Test]
        public void should_serialize_to_mime_string_mutipart_message_with_nested_parts()
        {
            var mimePart = new MimePart
                {
                    ContentType =
                        {
                            MimeType = "multipart/mixed",
                            Parameters = { { "boundary", "OuterBoundary" } }
                        },
                    SubParts = new MimePartCollection
                        {
                            new MimePart
                                {
                                    ContentType =
                                        {
                                            MimeType = "multipart/mixed",
                                            Parameters = { { "boundary", "InnerBoundary" } }

                                        },
                                    SubParts = new MimePartCollection
                                        {
                                            new MimePart(_textfilePath, true),
                                            new MimePart(_imagefilePath, false)
                                        }
                                },
                            new MimePart(_imagefilePath, false)
                        }
                };

            var outerSubParts = mimePart.ToMimeString().SplitMimeParts("--OuterBoundary\r\n");
            outerSubParts.Length.ShouldEqual(3);
            var mixedPart = outerSubParts[1];
            mixedPart.ShouldMatch("^Content-Type: multipart/mixed;");
            mixedPart.ShouldMatch(@"	boundary=""InnerBoundary""\r\n");
            var imagePart = outerSubParts[2];
            imagePart.ShouldMatch("^Content-Type: image/gif");
            imagePart.ShouldMatch("--OuterBoundary--\r\n$");

            var innerSubParts = mixedPart.SplitMimeParts("--InnerBoundary\r\n");
            innerSubParts.Length.ShouldEqual(3);
            var innerTextPart = innerSubParts[1];
            innerTextPart.ShouldMatch("^Content-Type: text/plain;\r\n");
            innerTextPart.ShouldMatch("Hello, World !\r\n\r\n$");
            var innerImagePart = innerSubParts[2];
            innerImagePart.ShouldMatch("^Content-Type: image/gif");
            innerImagePart.ShouldMatch("--InnerBoundary--\r\n\r\n\r\n$");
        }

        [Test]
        public void should_force_base64_encoding_when_serializing_to_mime_string()
        {
            var mimePart = CreateMultipartMimePart();

            var subParts = mimePart.ToMimeString(forceBase64Encoding: true).SplitMimeParts();

            subParts.Length.ShouldEqual(3);
            var messagePart = subParts[0];
            messagePart.ShouldMatch("^Content-Type: multipart/mixed;\r\n");
            messagePart.ShouldMatch("Content-Transfer-Encoding: base64");
            var textPart = subParts[1];
            textPart.ShouldMatch("Content-Transfer-Encoding: base64");
            textPart.ShouldMatch("SGVsbG8sIFdvcmxkICE=\r\n\r\n$");
            var imagePart = subParts[2];
            imagePart.ShouldMatch("Content-Transfer-Encoding: base64");
        }

        [Test]
        public void should_return_proper_content_encoding()
        {
            var mimePart = new MimePart();

            mimePart.HeaderFields["content-transfer-encoding"] = "quoted-printable";
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.QuotedPrintable);
            mimePart.HeaderFields["content-transfer-encoding"] = "base64";
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.Base64);
            mimePart.HeaderFields["content-transfer-encoding"] = "8bit";
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.EightBits);
            mimePart.HeaderFields["content-transfer-encoding"] = "7bit";
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.SevenBits);
            mimePart.HeaderFields["content-transfer-encoding"] = "binary";
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.Binary);
            mimePart.HeaderFields["content-transfer-encoding"] = "bla";
            mimePart.ContentTransferEncoding.ShouldEqual(ContentTransferEncoding.Unknown);
        }

        [Test]
        public void should_set_proper_content_encoding()
        {
            var mimePart = new MimePart();

            mimePart.ContentTransferEncoding = ContentTransferEncoding.Binary;
            mimePart.HeaderFields["content-transfer-encoding"].ShouldEqual("binary");
            mimePart.ContentTransferEncoding = ContentTransferEncoding.QuotedPrintable;
            mimePart.HeaderFields["content-transfer-encoding"].ShouldEqual("quoted-printable");
            mimePart.ContentTransferEncoding = ContentTransferEncoding.SevenBits;
            mimePart.HeaderFields["content-transfer-encoding"].ShouldEqual("7bit");
            mimePart.ContentTransferEncoding = ContentTransferEncoding.EightBits;
            mimePart.HeaderFields["content-transfer-encoding"].ShouldEqual("8bit");
            mimePart.ContentTransferEncoding = ContentTransferEncoding.None;
            mimePart.HeaderFields["content-transfer-encoding"].ShouldEqual("base64");
        }

        [Test]
        public void should_content_transfer_encode_text()
        {
            var longTextMessage = "This is a long text message that is big enough to be wrapped and contains characters like \r\n or < that will be encoded";
            var mimePart = new MimePart(Encoding.UTF8.GetBytes(longTextMessage), _textContentFileName);

            mimePart.ContentTransferEncoding = ContentTransferEncoding.SevenBits;
            mimePart.TextContentTransferEncoded.ShouldEqual(longTextMessage);
            mimePart.ContentTransferEncoding = ContentTransferEncoding.EightBits;
            mimePart.TextContentTransferEncoded.ShouldEqual("This is a long text message that is big enough to be wrapped and contains=\r\n characters like =0D=0A or < that will be encoded");
            mimePart.ContentType.MimeType = "message/thing";
            mimePart.TextContentTransferEncoded.ShouldEqual("This is a long text message that is big enough to be wrapped and contains characters like \r\n or < that will be encoded");
            mimePart.ContentType.MimeType = "unknown";
            mimePart.TextContentTransferEncoded.ShouldEqual("VGhpcyBpcyBhIGxvbmcgdGV4dCBtZXNzYWdlIHRoYXQgaXMgYmlnIGVub3VnaCB0byBiZSB3cm\r\nFwcGVkIGFuZCBjb250YWlucyBjaGFyYWN0ZXJzIGxpa2UgDQogb3IgPCB0aGF0IHdpbGwgYmUg\r\nZW5jb2RlZA==");
        }
    }

    public static class ExtendString
    {
        public static string[] SplitMimeParts(this string mimeString, string boundary = "-----AU_MimePart_[0-9]*\r\n")
        {
            return Regex.Split(mimeString, boundary);
        }
    }
}