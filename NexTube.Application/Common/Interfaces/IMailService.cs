namespace NexTube.Application.Common.Interfaces {
    public interface IMailService {
        Task SendMailAsync(string message, string recipient);
        string GeneratePassword(int length);
    }
}
