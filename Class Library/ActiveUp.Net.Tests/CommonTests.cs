using System;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests {
    /// <summary>
    ///    ActiveUp.Net.Common library related tests.
    /// </summary>
    [TestFixture(Description = "ActiveUp.Net.Common library related tests")]
    public class CommonTests {
        [Test(Description = "Verify the correct parsing of multiple trace entries in a mail and let not affect the message.Date")]
        public void ParseMultipleMessageTraceMessageDate() {
            DateTime expected = new DateTime(2013, 01, 04, 03, 04, 04);
            Message message = Parser.ParseMessageFromFile("resource\\testmail.eml");
            Assert.AreEqual(expected, message.Date);
        }

        [Test(Description = "Verify the correct parsing of multiple trace entries in a mail and use the latest one as message.ReceivedDate")]
        public void ParseMultipleMessageTraceReceivedDate() {
            //relevant entry is "Mon, 7 Jan 2013 07:07:07 +0100" so Parser.ParseAsUniversalDateTime will give the following datetime as Result
            DateTime expected = new DateTime(2013, 01, 07, 06, 07, 07);
            Message message = Parser.ParseMessageFromFile("resource\\testmail.eml");
            Assert.AreEqual(expected, message.ReceivedDate);
        }

        [Test(Description = "Email with content-transfer-encoding 'QUOTED-PRINTABLE'")]
        public void ParseEncodeQuotedPrintable() {
            #region Expected Body
            var expectedData = @"------- Mensagem encaminhada -------
De: origin@email.com
Para: Financial Enterprise financialc@customer.com
CC:
Assunto: Re: COMUNICADO IMPORTANTE - XXXXXXX XXXXXX
Data: 13/08/2015 13h32min29s UTC

Bom Dia!

Diante da comunicação enviada, e com os novos valores de convenio médico apresentados,  informamos que não daremos continuidade ao contrato e não aceitaremos a migração.

Qualquer duvida estamos a disposição.

Assinatura do cliente empresa XXX LTDA ME
Fulanos da Silva Mestre - 00000-0000





Em Sex 7/08/15 10:04, Financial Enterprise financialc@customer.com escreveu:

Bom dia!

Segue anexo comunicado referente ao reajuste (XXXXXXX XXXXXX) e relação de beneficiários com readequação de preços.
Att,
Caroline 
Financial Department   
+55 11 0000-0000


".Replace("\r\n", "");
            #endregion
            Message message = Parser.ParseMessageFromFile("resource\\testmail-quoted-printable.eml");
            Assert.AreEqual(expectedData, message.BodyHtml.TextStripped.Replace("\r\n", "").Replace("\r", "").Replace("\n",""));
        }
    }
}