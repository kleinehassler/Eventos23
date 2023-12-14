using System.Net.Mail;
using System.Net;

namespace Utilidades
{
    static public class Correo
    {
        public static bool esCorreoValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string enviar(string email, string asunto, string cuerpo)
        {
            string respuesta;
            //Cofigurar el protocolo de seguridad

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("nuntoncesar@nubecix.com");//aqui cambiar
            mail.To.Add(email);

            //set the content 
            mail.Subject = asunto;
            mail.Body = cuerpo;
            mail.IsBodyHtml = true;
            //send the message 
            SmtpClient smtp = new SmtpClient("mail5019.site4now.net");//aqui cambiar

            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            NetworkCredential Credentials = new NetworkCredential("nuntoncesar@nubecix.com", "Cesar@2023");//aqui cambiar
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = Credentials;
            smtp.Port = 25;    //aqui cambiar
            smtp.EnableSsl = false; //aqui cambiar
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(mail);
                respuesta = "";
            }
            catch (Exception ex)
            {
                respuesta = "Error: " + ex.Message;
            }
            finally
            {
                smtp.Dispose();
            }

            return respuesta;

        }
    }
}