using System;
using System.IO;
using System.Text;
using ActiveUp.Net.Common.Rfc2047;
using ActiveUp.Net.Mail;
using NUnit.Framework;
using System.Linq;

namespace ActiveUp.Net.Tests.Common.RFC2047
{
    public partial class Rfc2047DecoderTests
    {
        public class DecodingSample
        {
            public string SampleEncodedHeader { get; private set; }
            public string ExpectedDecodedHeader { get; private set; }

            public DecodingSample(string sampleEncodedHeader, string expectedDecodedHeader)
            {
                SampleEncodedHeader = sampleEncodedHeader;
                ExpectedDecodedHeader = expectedDecodedHeader;
            }
            public override string ToString()
            {
                return SampleEncodedHeader + "  =>  " + ExpectedDecodedHeader;
            }
        }

        /// <summary>
        /// Headers to be decoded
        /// </summary>
        private static DecodingSample[] sampleEncodedHeaders = new[]
        {
            new DecodingSample("", ""),
            new DecodingSample(" ", " "),
            new DecodingSample("word", "word"),
            new DecodingSample("From: =?US-ASCII?Q?Keith_Moore?= <moore@cs.utk.edu>", "From: Keith Moore <moore@cs.utk.edu>"),
            new DecodingSample("To: =?ISO-8859-1?Q?Keld_J=F8rn_Simonsen?= <keld@dkuug.dk>", "To: Keld Jørn Simonsen <keld@dkuug.dk>"),
            new DecodingSample("CC: =?ISO-8859-1?Q?Andr=E9?= Pirard <PIRARD@vm1.ulg.ac.be>", "CC: André Pirard <PIRARD@vm1.ulg.ac.be>"),
            new DecodingSample("Subject: =?ISO-8859-1?B?SWYgeW91IGNhbiByZWFkIHRoaXMgeW8=?=\n=?ISO-8859-2?B?dSB1bmRlcnN0YW5kIHRoZSBleGFtcGxlLg==?=", "Subject: If you can read this you understand the example."),
            new DecodingSample("From: =?ISO-8859-1?Q?Olle_J=E4rnefors?= <ojarnef@admin.kth.se>", "From: Olle Järnefors <ojarnef@admin.kth.se>"),
            new DecodingSample("To: ietf-822@dimacs.rutgers.edu, ojarnef@admin.kth.se", "To: ietf-822@dimacs.rutgers.edu, ojarnef@admin.kth.se"),
            new DecodingSample("Subject: Time for ISO 10646?", "Subject: Time for ISO 10646?"),
            new DecodingSample("To: Dave Crocker <dcrocker@mordor.stanford.edu>", "To: Dave Crocker <dcrocker@mordor.stanford.edu>"),
            new DecodingSample("Cc: ietf-822@dimacs.rutgers.edu, paf@comsol.se", "Cc: ietf-822@dimacs.rutgers.edu, paf@comsol.se"),
            new DecodingSample("From: =?ISO-8859-1?Q?Patrik_F=E4ltstr=F6m?= <paf@nada.kth.se>", "From: Patrik Fältström <paf@nada.kth.se>"),
            new DecodingSample("Subject: Re: RFC-HDR care and feeding", "Subject: Re: RFC-HDR care and feeding"),
            new DecodingSample("To: Greg Vaudreuil <gvaudre@NRI.Reston.VA.US>, Ned Freed\n <ned@innosoft.com>, Keith Moore <moore@cs.utk.edu>", "To: Greg Vaudreuil <gvaudre@NRI.Reston.VA.US>, Ned Freed\n <ned@innosoft.com>, Keith Moore <moore@cs.utk.edu>"),
            new DecodingSample("Subject: Test of new header generator", "Subject: Test of new header generator"),
            new DecodingSample("MIME-Version: 1.0", "MIME-Version: 1.0"),
            new DecodingSample("Content-type: text/plain; charset=ISO-8859-1", "Content-type: text/plain; charset=ISO-8859-1" ),
            new DecodingSample("(=?ISO-8859-1?Q?a?=)", "(a)"),
            new DecodingSample("(=?ISO-8859-1?Q?a?= b)", "(a b)"),
            new DecodingSample("(=?ISO-8859-1?Q?a?= =?ISO-8859-1?Q?b?=)", "(ab)"),
            new DecodingSample("(=?ISO-8859-1?Q?a?=  =?ISO-8859-1?Q?b?=)", "(ab)"),
            new DecodingSample("(=?ISO-8859-1?Q?a?=\n       =?ISO-8859-1?Q?b?=)", "(ab)"),
            new DecodingSample("(=?ISO-8859-1?Q?a_b?=)", "(a b)"),
            new DecodingSample("(=?ISO-8859-1?Q?a?= =?ISO-8859-2?Q?_b?=)", "(a b)"),
        };

        [Test]
        [TestCaseSource("sampleEncodedHeaders")]
        public void should_handle_examples_from_the_rfc_2047(DecodingSample decodingSample)
        {
            var decodedHeader = Rfc2047Codec.Decode(decodingSample.SampleEncodedHeader);
                
            decodedHeader.ShouldEqual(decodingSample.ExpectedDecodedHeader);
        }

        [Test]
        public void should_decode_RFC2047_encoded_words()
        {
            // Second, ensure that an encoded sentence from Codec.RFC2047Encode is correctly decoded
            var sampleText = "Je suis Liégeois et je suis prêt à rencontrer Asger Jørnow";
            var encodedText = Rfc2047Codec.Encode(sampleText, "iso-8859-1");
            sampleText.ShouldEqual(Rfc2047Codec.Decode(encodedText));
        }

        /// <summary>
        /// This case is forbidden by RFC2047, but some senders don't respect it
        /// </summary>
        [Test]
        public void should_handle_multi_byte_unicode_chars_spanning_over_multiple_words()
        {
            var decodedString = Rfc2047Codec.Decode(
@"=?utf-8?B?0JjQpNClIC0gKtCT0LDQt9C/0YDQvtC8KiDQvtGC0LzQtdC9?=
 =?utf-8?B?0LjQuyDRgtC10L3QtNC10YDRiyDQvdCwINC30LDQutGD0L/QutGDINGC?=
 =?utf-8?B?0YDRg9CxINCx0L7Qu9GM0YjQvtCz0L4g0LTQuNCw0LzQtdGC0YDQsCDQ?=
 =?utf-8?B?vdCwIDEyINC80LvRgNC0INGA0YPQsdC70LXQuS4=?=");

            decodedString.ShouldEqual("ИФХ - *Газпром* отменил тендеры на закупку труб большого диаметра на 12 млрд рублей.");
        }

        [Test]
        public void should_handle_mixed_content()
        {
            var decodedString = Rfc2047Codec.Decode("This is some russian:\t=?utf-8?B?0JjQpNClIC0gKtCT0LDQt9C/0YDQvtC8KiDQvtGC0LzQtdC9?= wasn't that cool?");

            decodedString.ShouldEqual("This is some russian:\tИФХ - *Газпром* отмен wasn't that cool?");
        }

        [Test]
        public void should_handle_strings_with_space_in_front()
        {
            var input = " test@example.com\r";

            var decodedString = Rfc2047Codec.Decode(input);

            decodedString.ShouldEqual(" test@example.com\r");
        }

        [Test]
        public void should_remove_trailing_equals_when_grouping_base64_encoded_words()
        {
            var input = " =?UTF-8?B?KDIpICpCT01CQVJESUVSIENFTyBTQVlTIExPT0tJTkcgRg==?= =?UTF-8?B?T1JXQVJEIFRPIEhJR0gtU1BFRUQgVFJBSU5TIElOIFUuUw==?=";

            var decodedString = Rfc2047Codec.Decode(input);

            decodedString.ShouldEqual("(2) *BOMBARDIER CEO SAYS LOOKING FORWARD TO HIGH-SPEED TRAINS IN U.S");
        }
        
        // This test only makes sure the new header parser is able to deserialize all the samples
        // To test for regressions, use the following test
        [Test]
        public void should_handle_sample_headers()
        {
            var allHeaders = File.ReadAllText("resource\\sample_headers.txt").Split(new[] { "\r\n\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var header in allHeaders)
                Assert.DoesNotThrow(() => Parser.ParseHeaderString(header));
        }

        // This test is an example of how to do A/B testing for a header parser modification
        [Test, Ignore]
        public void parser_evolution_example()
        {
            var allHeaders = File.ReadAllText("resource\\sample_headers.txt").Split(new[] { "\r\n\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var header in allHeaders)
            {
                var headerFromNewParser = Parser.ParseHeaderString(header);
                var headerFromReferenceParser = Parser.ParseHeaderString(header); // Would use the old implementation
                headerFromNewParser.ShouldEqual(headerFromReferenceParser);
            }
        }
    }
}