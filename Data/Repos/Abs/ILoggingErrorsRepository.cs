using Domain.Models;
using Domain.ViewModels.LoggingErrors;

namespace Data.Repos.Abs
{
    public interface ILoggingErrorsRepository
    {
        Task<List<LoggingError>> GetAll();
        Task<LoggingError> Get(string id);
        Task<LoggingErrorViewModel> Create(LoggingErrorViewModel model);
        Task<LoggingErrorViewModel> Update(LoggingErrorViewModel model);
        Task<bool> Delete(string id);
    }
}
