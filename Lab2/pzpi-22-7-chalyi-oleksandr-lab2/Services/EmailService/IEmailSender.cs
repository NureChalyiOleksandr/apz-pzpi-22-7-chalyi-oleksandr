using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLightSense.Services.EmailService
{
    public interface IEmailSender
    {
        Task SendEmail(Message message);
    }
}
