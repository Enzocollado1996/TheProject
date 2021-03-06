using Microsoft.Extensions.Logging;
using System.Entities;
using System.Helper.Models;
using System;
using System.IO;
using System.Net.Mail;

namespace System.Helper
{
    public class SendNotificacion
    {
        private readonly ILogger _logger;
        readonly string urlBackend = Parametro.GetValue("BaseUrlBackend");
        readonly string path = Parametro.GetValue("PathMailing");

        public SendNotificacion(ILogger logger)
        {
            _logger = logger;
        }

        public void SendMailForgotPassword(MailUsuarioModel usuario)
        {
            try
            {
                var mail = ArmadoMailForgotPassword(usuario);
                EmailHelper.SendMail(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el mail de olvide mi contraseņa");
                throw;
            }
        }

        public void SendMailNewUsuario(MailUsuarioModel usuario)
        {
            try
            {
                var mail = ArmadoMailNewUsuario(usuario);
                EmailHelper.SendMail(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el mail de nuevo usuario");
                throw;
            }
        }

        private MailMessage ArmadoMailForgotPassword(MailUsuarioModel usuario)
        {
            string urlChangePass = urlBackend + "Account/ChangePassByMail?token=" + usuario.TokenUrl;
            string body = File.ReadAllText(path + "/resetForgotPass.html");
            string subject = "Solicitud para restablecer contraseņa olvidada";
            string from = Parametro.GetValue("Remitente");
            string fromName = Parametro.GetValue("RemitenteNombre");

            body = body.Replace("[@url-change-pass]", urlChangePass);
            body = body.Replace("[@nombre-usuario]", usuario.UsuarioNombre + " " + usuario.UsuarioApellido);

            return CreateMailMessage(from, fromName, subject, body, usuario.Email);
        }

        public MailMessage ArmadoMailNewUsuario(MailUsuarioModel usuario)
        {
            string subjectAlta = String.Empty;
            string urlChangePass = urlBackend + "Account/ChangePassByMail?token=" + usuario.TokenUrl;
            string body = File.ReadAllText(path + "/nuevoUsuario.html");
            string from = Parametro.GetValue("Remitente");
            string fromName = Parametro.GetValue("RemitenteNombre");
            string contentUserType = String.Empty;
            MailMessage mail;

            if (usuario.EsAdministrador)
            {
                subjectAlta = "Tu usuario administrador ya esta disponible!";
                contentUserType = "<li>Usuario-Admin: <b>" + usuario.Usuario + "</b></li>";
                body = body.Replace("[@base-url]", urlBackend);
                body = body.Replace("[@url-change-pass]", urlChangePass);
                body = body.Replace("[@password]", usuario.Password);
                body = body.Replace("[@nombre-usuario]", usuario.UsuarioNombre + " " + usuario.UsuarioApellido);
                body = body.Replace("[@seccion-user-type]", contentUserType);

                mail = new MailMessage
                {
                    IsBodyHtml = true,
                    Body = body,
                    Subject = subjectAlta,
                    From = new MailAddress(from, fromName),
                };
                mail.To.Add(usuario.Email);
                return mail;
            }

            subjectAlta = "Tu usuario ya esta disponible!";
            contentUserType = "<li>Usuario: <b>" + usuario.Usuario + "</b></li>";
            body = body.Replace("[@base-url]", urlBackend);
            body = body.Replace("[@url-change-pass]", urlChangePass);
            body = body.Replace("[@password]", usuario.Password);
            body = body.Replace("[@nombre-usuario]", usuario.UsuarioNombre + " " + usuario.UsuarioApellido);
            body = body.Replace("[@seccion-user-type]", contentUserType);

            mail = new MailMessage
            {
                IsBodyHtml = true,
                Body = body,
                Subject = subjectAlta,
                From = new MailAddress(from, fromName)
            };
            mail.To.Add(usuario.Email);
            return mail;
        }

        private MailMessage CreateMailMessage (string from, string fromName, string subject, string body, string to)
        {
            if (!string.IsNullOrEmpty(to.Trim()))
            {
                var mail = new MailMessage
                {
                    IsBodyHtml = true,
                    Body = body,
                    Subject = subject,
                    From = new MailAddress(from, fromName)
                };

                var emailArray = to.Split(',');

                foreach (var email in emailArray)
                {
                    mail.To.Add(email);
                }

                return mail;
            }
            else
            {
                throw new ArgumentNullException("to", "Debe contar con al menos un destinatario para enviar el mail");
            }
        }
    }
}
