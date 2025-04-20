﻿using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Fase04.Infra.Message.Settings;
using Fase04.Infra.Messages.Helpers;

namespace Fase04.Infra.Message.Helpers;

/// <summary>
/// Classe para envio de emails
/// </summary>
public class MailHelper : IMailHelper
{
    private readonly MailSettings _mailSettings;

    public MailHelper(IOptions<MailSettings> mailSettings) 
        => _mailSettings = mailSettings.Value;

    /// <summary>
    /// Método para envio de emails
    /// </summary>
    public void Send(string mailTo, string subject, string body)
    {
        #region Escrevendo o email

        var mailMessage = new MailMessage("alexandrephausmann@hotmail.com", mailTo);
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;

        #endregion

        #region Enviando o email

        var smtpClient = new SmtpClient(_mailSettings.Smtp, _mailSettings.Port.Value);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password);
        smtpClient.Send(mailMessage);

        #endregion
    }
}