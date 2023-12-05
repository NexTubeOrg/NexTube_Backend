namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoAccessModificatorService
    {
        Task CreateAccessModificatorAsync(string modificatorName);
    }
}
