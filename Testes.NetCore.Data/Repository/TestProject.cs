using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Testes.NetCore.Domain.Interface;

namespace Testes.NetCore.Data.Repository
{
    public class TestProject : ITestProject
    {
        public void SendEmail()
        {
            string to = "receiver@workmail.com";
            string from = "sender@workmail.com";
            var message = new MailMessage(from, to);
            message.Subject = "Relatório LeadMachine.";
            message.Body = @"Using this new feature, you can send an email message from an application very easily.";
            var clientSmtp = new SmtpClient();
            clientSmtp.Credentials = new NetworkCredential("sender@workmail.com", "password@123");
            clientSmtp.Host = "192.168.0.0.0";
            clientSmtp.Port = 99;

            try
            {
                clientSmtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught in SendMail(): {ex}");
            }
        }

        public void TratarTelefones()
        {
            //valor inicial = " asdas asda 01as5d9as9da9sd8a87766as"
            string telefoneTeste = " asdas asda 01as5d9as9da9sd8a87766as ";

            //resultado = "15999887766"
            string telefoneTratado = Regex.Replace(telefoneTeste, @"(\D)", "");

            //resultado = "15"
            int ddd = string.IsNullOrWhiteSpace(telefoneTratado) || telefoneTratado.Length < 10 ? 0 : telefoneTratado.StartsWith("0") ? Convert.ToInt32(telefoneTratado.Substring(1, telefoneTratado.Length - 1).Substring(0, 2)) : Convert.ToInt32(telefoneTratado.Substring(0, 2));

            //resultado = "999887766"
            int telefone = string.IsNullOrWhiteSpace(telefoneTratado) || telefoneTratado.Length < 10 ? 0 : telefoneTratado.StartsWith("0") ? Convert.ToInt32(telefoneTratado.Substring(1, telefoneTratado.Length - 1).Substring(2, telefoneTratado.Length - 3)) : Convert.ToInt32(telefoneTratado.Substring(2, telefoneTratado.Length - 2));

            //resultado = "(15)999887766"
            Console.WriteLine($"({ddd}){telefone}");
        }
    }
}