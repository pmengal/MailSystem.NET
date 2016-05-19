using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests.Common
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void should_parse_simple_date()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 2013 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }

        [Test]
        public void should_clean_input()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("(noise\\input)Mon, 24 Jun 2013 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }

        [Test]
        public void should_return_resulting_date_in_utc()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 2013 10:37:36 +0100");

            utcDate.Kind.ShouldEqual(DateTimeKind.Utc);
        }
        
        [Test]
        public void should_parse_date_with_no_day_of_week()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("24 Jun 2013 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }
        
        [Test]
        public void should_parse_date_with_two_digits_year()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 13 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }
        
        [Test]
        public void should_parse_date_with_no_seconds()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 2013 10:37 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 00));
        }

        [Test]
        public void should_parse_basic_address()
        {
            var address = Parser.ParseAddress("here@there.com");
            address.Email.ShouldEqual("here@there.com");
            address.Name.ShouldEqual(string.Empty);
        }

        [Test]
        public void should_parse_quoted_address()
        {
            var address = Parser.ParseAddress("<\"here@there.com\">");
            address.Email.ShouldEqual("here@there.com");
            address.Name.ShouldEqual(string.Empty);
        }

        [Test]
        public void should_parse_address_with_quoted_display_name()
        {
            var address = Parser.ParseAddress("\"Display Name\" <display@name.de>");
            address.Email.ShouldEqual("display@name.de");
            address.Name.ShouldEqual("Display Name");
        }

        [Test]
        public void should_parse_address_with_non_quoted_display_name()
        {
            var address = Parser.ParseAddress("DisplayName <display@name.de>");
            address.Email.ShouldEqual("display@name.de");
            address.Name.ShouldEqual("DisplayName");
        }

        [Test]
        public void should_parse_address_with_chevrons_in_display_name()
        {
            var address = Parser.ParseAddress("\"Display Name <with Chevrons>\" <Chevrons@displayname.de>");
            address.Email.ShouldEqual("Chevrons@displayname.de");
            address.Name.ShouldEqual("Display Name <with Chevrons>");
        }

        [Test]
        public void should_parse_address_with_no_closing_quote_after_display_name()
        {
            var address = Parser.ParseAddress("\"Display Name only one quote <Chevrons@displayname.de>");
            address.Email.ShouldEqual("Chevrons@displayname.de");
            address.Name.ShouldEqual("Display Name only one quote");
        }

		/// <summary>
		/// [discussion:641270] - Created discussion to validate if this test is rigth.
		/// </summary>
        [Test]
        public void should_append_text_parts_with_inline_disposition()
        {
            var message = Parser.ParseMessageFromFile("resource\\text_multipart_email.eml");

            message.BodyText.Text.ShouldEqual("Good morning,\r\nThis is the body of the message.\r\n\r\nThis is the attached disclamer\r\n");
        }

		/// <summary>
		/// [discussion:641270] - Created discussion to validate if this test is rigth.
		/// </summary>
        [Test]
        public void should_append_html_parts_with_inline_disposition()
        {
            var message = Parser.ParseMessageFromFile("resource\\html_multipart_email.eml");

            message.BodyHtml.Text.ShouldEqual("Good morning,\r\n<em>This is the body of the message.</em>\r\n\r\nThis is the <em>attached</em> disclamer\r\n");
        }

        [Test]
        public void should_decode_content_name()
        {
            var message = Parser.ParseMessage(File.ReadAllText("resource\\japanese_email.eml"));

            message.Attachments[0].ContentName.ShouldEqual("大阪瓦斯9532.pdf");
        }

        /// <summary>
        ///  https://tools.ietf.org/html/rfc2387
        /// </summary>
        [Test(Description = "")]
        public void should_append_html_parts_with_multipart_related_content_type()
        {
            var message = Parser.ParseMessageFromFile("resource\\html_multipart_related_email_with_subparts.eml");
            message.BodyText.Text.ShouldEqual("Hi,\r\n\r\nI?m the first step of message to test.\r\n\r\nSender\r\nHelper\r\n[Descrição: Descrição: Descrição: Descrição: Imagem4]\r\nE-mail: sender@sender.com\r\nWeb: www.sender.com<http://www.sender.com/>\r\n\r\nCurta nossa página no Facebook<https://www.sender.com>\r\n\r\n\r\n\r\n\r\nDe: Destinatary - Client\r\nEnviada em: quarta-feira, 23 de março de 2016 18:39\r\nPara: Odare; suport@enterprise.com; suporte\r\nCc: contasapagar; Customer2 - Client; Customer3 - Client; Customer4 - Client\r\nAssunto: Email multipart related multilevel\r\n\r\nHi there,\r\n\r\nThis is the multipart original message.\r\n\r\n[cid:image003.jpg@01D185BB.A9907410]\r\n\r\n\r\nFollow the image of problem:\r\n\r\n[cid:image004.jpg@01D185BB.A9907410]\r\n\r\nAtt,\r\n\r\n\r\n\r\n\r\n\r\n");
            message.BodyHtml.Text.ShouldEqual("<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">\r\n<meta name=\"Generator\" content=\"Microsoft Word 14 (filtered medium)\">\r\n<!--[if !mso]><style>v\\:* {behavior:url(#default#VML);}\r\no\\:* {behavior:url(#default#VML);}\r\nw\\:* {behavior:url(#default#VML);}\r\n..sape {behavior:url(#default#VML);}\r\n</style><![endif]--><style><!--\r\n/* Font Definitions */\r\n@font-face\r\n	{font-family:Calibri;\r\n	panose-1:2 15 5 2 2 2 4 3 2 4;}\r\n@font-face\r\n	{font-family:Tahoma;\r\n	panose-1:2 11 6 4 3 5 4 4 2 4;}\r\n@font-face\r\n	{font-family:Verdana;\r\n	panose-1:2 11 6 4 3 5 4 4 2 4;}\r\n@font-face\r\n	{font-family:\"Century Gothic\";\r\n	panose-1:2 11 5 2 2 2 2 2 2 4;}\r\n/* Style Definitions */\r\np.MsoNormal, li.MsoNormal, div.MsoNormal\r\n	{margin:0cm;\r\n	margin-bottom:.0001pt;\r\n	font-size:11.0pt;\r\n	font-family:\"Calibri\",\"sans-serif\";\r\n	mso-fareast-language:EN-US;}\r\na:link, span.MsoHyperlink\r\n	{mso-style-priority:99;\r\n	color:blue;\r\n	text-decoration:underline;}\r\na:visited, span.MsoHyperlinkFollowed\r\n	{mso-style-priority:99;\r\n	color:purple;\r\n	text-decoration:underline;}\r\np.MsoAcetate, li.MsoAcetate, div.MsoAcetate\r\n	{mso-style-priority:99;\r\n	mso-style-link:\"Texto de balão Char\";\r\n	margin:0cm;\r\n	margin-bottom:.0001pt;\r\n	font-size:8.0pt;\r\n	font-family:\"Tahoma\",\"sans-serif\";\r\n	mso-fareast-language:EN-US;}\r\nspan.TextodebaloChar\r\n	{mso-style-name:\"Texto de balão Char\";\r\n	mso-style-priority:99;\r\n	mso-style-link:\"Texto de balão\";\r\n	font-family:\"Tahoma\",\"sans-serif\";}\r\nspan.EstiloDeEmail19\r\n	{mso-style-type:personal;\r\n	font-family:\"Calibri\",\"sans-serif\";\r\n	color:windowtext;}\r\nspan.EstiloDeEmail20\r\n	{mso-style-type:personal-reply;\r\n	font-family:\"Calibri\",\"sans-serif\";\r\n	color:#365F91;}\r\n..MoChpDefault\r\n	{mso-style-type:export-only;\r\n	font-size:10.0pt;}\r\n@page WordSection1\r\n	{size:612.0pt 792.0pt;\r\n	margin:70.85pt 3.0cm 70.85pt 3.0cm;}\r\ndiv.WordSection1\r\n	{page:WordSection1;}\r\n--></style><!--[if gte mso 9]><xml>\r\n<o:shapedefaults v:ext=\"edit\" spidmax=\"1026\" />\r\n</xml><![endif]--><!--[if gte mso 9]><xml>\r\n<o:shapelayout v:ext=\"edit\">\r\n<o:idmap v:ext=\"edit\" data=\"1\" />\r\n</o:shapelayout></xml><![endif]-->\r\n</head>\r\n<body lang=\"PT-BR\" link=\"blue\" vlink=\"purple\">\r\n<div class=\"WordSection1\">\r\n<p class=\"MsoNormal\"><span style=\"color:#365F91\">Hi,<o:p></o:p></span></p>\r\n<p class=\"MsoNormal\"><span style=\"color:#365F91\"><o:p>&nbsp;</o:p></span></p>\r\n<p class=\"MsoNormal\"><span style=\"color:#365F91\">This is the multipart original message.<o:p></o:p></span></p>\r\n<p class=\"MsoNormal\"><b><span style=\"font-family:&quot;Century Gothic&quot;,&quot;sans-serif&quot;;color:#002060;mso-fareast-language:PT-BR\"><o:p>&nbsp;</o:p></span></b></p>\r\n<p class=\"MsoNormal\"><b><span style=\"font-family:&quot;Century Gothic&quot;,&quot;sans-serif&quot;;color:#002060;mso-fareast-language:PT-BR\">Sender<o:p></o:p></span></b></p>\r\n<table class=\"MsoNormalTable\" border=\"0\" cellpadding=\"0\">\r\n<tbody>\r\n<tr>\r\n<td style=\"padding:.75pt .75pt .75pt .75pt\">\r\n<p class=\"MsoNormal\"><i><span style=\"font-size:10.0pt;font-family:&quot;Century Gothic&quot;,&quot;sans-serif&quot;;color:#002060;mso-fareast-language:PT-BR\">Helper                 </span></i><i><span style=\"font-size:10.0pt;font-family:&quot;Century Gothic&quot;,&quot;sans-serif&quot;;color:#002060;mso-fareast-language:PT-BR\"><o:p></o:p></span></i></p>\r\n<p class=\"MsoNormal\"><span style=\"color:#1F497D;mso-fareast-language:PT-BR\"><img width=\"175\" height=\"104\" id=\"Imagem_x0020_1\" src=\"cid:image001.jpg@01D185BB.A9907410\" alt=\"Descrição: Descrição: Descrição: Descrição: Imagem4\"></span><span style=\"font-size:12.0pt;color:#1F497D;mso-fareast-language:PT-BR\"><o:p></o:p></span></p>\r\n</td>");
        }

        /// <summary>
        ///  https://tools.ietf.org/html/rfc2387
        /// </summary>
        [Test(Description = "")]
        public void should_recognize_line_break_of_notepad_text_in_body()
        {
            var message = Parser.ParseMessageFromFile("resource\\quoted-printable-notepad-linebreak.eml");
            message.BodyText.Text.ShouldEqual("Alatur,\r\rFoi criada uma nova solicitação para TESTE SOLICITANTE.\r\rCliente: TESTE HOTEL\rEmpresa: TESTE\rC. Custo: TESTE TESTE\r\r\r>>> PASSAGEM AÉREA\rDescrição.: (GRU) Cumbica / (LAS) Las Vegas 04/Jan Manhã (06:00 às 12:00) (Econômica)\rHorário...: considerando saída\rPagamento.: FATURADO\r\rDescrição.: (LAS) Las Vegas / (GRU) Cumbica 07/Jan Manhã (06:00 às 12:00) (Econômica)\rHorário...: considerando saída\rPagamento.: FATURADO\r\r\r>>> SOLICITANTE\rteste solicitante (fulfillment@alatur.com)\r\r\rDestinatários que estão recebendo esse email: \rtms@argoit.com.br (tms@argoit.com.br)\rteste solicitante (fulfillment@alatur.com)\rtesteodare@encontact.com.br (testeodare@encontact.com.br)\rodare@encontact.com.br (odare@encontact.com.br)\r\rPara acessá-la clique em: \r<https://arb.alatur.com/alatur/default.aspx?Id=5a03cdaf-1503-e611-9406-90b11c25f027&LinkId=FLXMfbCeRo72PRAkakfyOg%3d%3d> \r\rEMAIL AUTOMÁTICO, NÃO RESPONDA ESSA MENSAGEM\r\n");
            message.BodyHtml.Text.ShouldEqual("");
        }

        /// <summary>
        /// TODO: Change test to fail process correct action in attachment without filename. Actualy the system process.
        /// </summary>
        [Test(Description = "Attachment without filename")]
        public void ParseAttachmentWitoutFilename()
        {
            Message message = Parser.ParseMessageFromFile("resource\\attachment-witout-file-name.eml");
            for (int i = 0; i < message.Attachments.Count; i++)
                Assert.IsNotNull(message.Attachments[i].Filename);
        }

    }
}
