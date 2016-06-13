using System;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests.Common
{
    public class CodecTests
    {
        /// <summary>
        /// Headers to be decoded
        /// </summary>
        private static string[] sampleEncodedHeaders = new []
        {
            "From: =?US-ASCII?Q?Keith_Moore?= <moore@cs.utk.edu>",
            "To: =?ISO-8859-1?Q?Keld_J=F8rn_Simonsen?= <keld@dkuug.dk>",
            "CC: =?ISO-8859-1?Q?Andr=E9?= Pirard <PIRARD@vm1.ulg.ac.be>",
            "Subject: =?ISO-8859-1?B?SWYgeW91IGNhbiByZWFkIHRoaXMgeW8=?=\n=?ISO-8859-2?B?dSB1bmRlcnN0YW5kIHRoZSBleGFtcGxlLg==?=",
            "From: =?ISO-8859-1?Q?Olle_J=E4rnefors?= <ojarnef@admin.kth.se>",
            "To: ietf-822@dimacs.rutgers.edu, ojarnef@admin.kth.se",
            "Subject: Time for ISO 10646?",
            "To: Dave Crocker <dcrocker@mordor.stanford.edu>",
            "Cc: ietf-822@dimacs.rutgers.edu, paf@comsol.se",
            "From: =?ISO-8859-1?Q?Patrik_F=E4ltstr=F6m?= <paf@nada.kth.se>",
            "Subject: Re: RFC-HDR care and feeding",
            "To: Greg Vaudreuil <gvaudre@NRI.Reston.VA.US>, Ned Freed\n <ned@innosoft.com>, Keith Moore <moore@cs.utk.edu>",
            "Subject: Test of new header generator",
            "MIME-Version: 1.0",
            "Content-type: text/plain; charset=ISO-8859-1",
            "(=?ISO-8859-1?Q?a?=)",
            "(=?ISO-8859-1?Q?a?= b)",
               "(=?ISO-8859-1?Q?a?= =?ISO-8859-1?Q?b?=)",
            "(=?ISO-8859-1?Q?a?=  =?ISO-8859-1?Q?b?=)",
            "(=?ISO-8859-1?Q?a?=\n       =?ISO-8859-1?Q?b?=)",
            "(=?ISO-8859-1?Q?a_b?=)",
            "(=?ISO-8859-1?Q?a?= =?ISO-8859-2?Q?_b?=)"
        };

        /// <summary>
        /// Decoded headers
        /// </summary>
        private static string[] expectedDecodedHeaders = new [] 
        { 
            "From: Keith Moore <moore@cs.utk.edu>",
            "To: Keld Jørn Simonsen <keld@dkuug.dk>",
            "CC: André Pirard <PIRARD@vm1.ulg.ac.be>",
            "Subject: If you can read this you understand the example.",
            "From: Olle Järnefors <ojarnef@admin.kth.se>",
            "To: ietf-822@dimacs.rutgers.edu, ojarnef@admin.kth.se",
            "Subject: Time for ISO 10646?",
            "To: Dave Crocker <dcrocker@mordor.stanford.edu>",
            "Cc: ietf-822@dimacs.rutgers.edu, paf@comsol.se",
            "From: Patrik Fältström <paf@nada.kth.se>",
            "Subject: Re: RFC-HDR care and feeding",
            "To: Greg Vaudreuil <gvaudre@NRI.Reston.VA.US>, Ned Freed\n <ned@innosoft.com>, Keith Moore <moore@cs.utk.edu>",
            "Subject: Test of new header generator",
            "MIME-Version: 1.0",
            "Content-type: text/plain; charset=ISO-8859-1",
            "(a)",
            "(a b)",
            "(ab)",
            "(ab)",
            "(ab)",
            "(a b)",
            "(a b)"
        };

        [Test]
        public void should_handle_examples_from_the_rfc_2047()
        {
            for (var i = 0; i < sampleEncodedHeaders.GetLength(0); i++)
                Codec.RFC2047Decode(sampleEncodedHeaders[i]).ShouldEqual(expectedDecodedHeaders[i]);
        }

        [Test]
        public void should_decode_RFC2047_encoded_words()
        {
            // Second, ensure that an encoded sentence from Codec.RFC2047Encode is correctly decoded
            var sampleText = "Je suis Liégeois et je suis prêt à rencontrer Asger Jørnow";
            var encodedText = Codec.RFC2047Encode(sampleText, "iso-8859-1");
            sampleText.ShouldEqual(Codec.RFC2047Decode(encodedText));
        }

        [Test]
        public void toto()
        {
            var decodedString = Codec.RFC2047Decode(
@"=?utf-8?B?0JjQpNClIC0gKtCT0LDQt9C/0YDQvtC8KiDQvtGC0LzQtdC9?=
 =?utf-8?B?0LjQuyDRgtC10L3QtNC10YDRiyDQvdCwINC30LDQutGD0L/QutGDINGC?=
 =?utf-8?B?0YDRg9CxINCx0L7Qu9GM0YjQvtCz0L4g0LTQuNCw0LzQtdGC0YDQsCDQ?=
 =?utf-8?B?vdCwIDEyINC80LvRgNC0INGA0YPQsdC70LXQuS4=?=");

            decodedString.ShouldEqual("ИФХ - *Газпром* отменил тендеры на закупку труб большого диаметра на 12 млрд рублей.");
        }

        [Test]
        public void should_handle_mixed_content()
        {
            var decodedString = Codec.RFC2047Decode("This is some russian:\t=?utf-8?B?0JjQpNClIC0gKtCT0LDQt9C/0YDQvtC8KiDQvtGC0LzQtdC9?= wasn't that cool?");

            decodedString.ShouldEqual("This is some russian:\tИФХ - *Газпром* отмен wasn't that cool?");
        }

        [Test]
        public void should_support_different_encoding_after_line_break()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void should_test_one_word()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void should_test_one_space()
        {
            throw new NotImplementedException();
        }
    }
}