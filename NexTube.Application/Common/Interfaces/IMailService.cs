using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.Common.Interfaces
{
    public interface IMailService
    {
        Task SendMail(string message,string recipient);

         string GeneratePassword(int length);

        
    }
}
