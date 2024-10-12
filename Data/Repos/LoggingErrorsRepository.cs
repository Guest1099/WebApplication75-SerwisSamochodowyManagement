using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.LoggingErrors;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class LoggingErrorsRepository : ILoggingErrorsRepository
    {
        private readonly ApplicationDbContext _context;
        public LoggingErrorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoggingError>> GetAll()
        {
            return await _context.LoggingErrors.ToListAsync();
        }

        public async Task<LoggingError> Get(string id)
        {
            return await _context.LoggingErrors.FirstOrDefaultAsync(f => f.LoggingErrorId == id);
        }


        public async Task<LoggingErrorViewModel> Create(LoggingErrorViewModel model)
        {
            if (model != null)
            {
                try
                {
                    LoggingError loggingError = new LoggingError()
                    {
                        LoggingErrorId = Guid.NewGuid().ToString(),
                        Controller = model.LoggingError.Controller,
                        Method = model.LoggingError.Method,
                        Message = model.LoggingError.Message,
                        UserId = model.LoggingError.UserId,
                        DataUtworzenia = DateTime.Now.ToString()
                    };
                    _context.LoggingErrors.Add(loggingError);
                    await _context.SaveChangesAsync();

                    model.Success = true;
                }
                catch (Exception ex)
                {
                    model.Result = "Catch exception.";
                }
            }
            else
            {
                model.Result = "Model is null.";
            }
            return model;
        }



        public async Task<LoggingErrorViewModel> Update(LoggingErrorViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var loggingError = await _context.LoggingErrors.FirstOrDefaultAsync(f => f.LoggingErrorId == model.LoggingError.LoggingErrorId);

                    if (loggingError != null)
                    {
                        // aktualizacja danych logowania

                        loggingError.Controller = model.LoggingError.Controller;
                        loggingError.Method = model.LoggingError.Method;
                        loggingError.Message = model.LoggingError.Message;
                        loggingError.UserId = model.LoggingError.UserId;
                        loggingError.DataUtworzenia = model.LoggingError.DataUtworzenia;

                        _context.Entry(loggingError).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "Data is null.";
                    }
                }
                catch (Exception ex)
                {
                    model.Result = "Catch exception.";
                }
            }
            else
            {
                model.Result = "Model is null.";
            }
            return model;
        }



        public async Task<bool> Delete(string id)
        {
            bool deleteResult = false;
            try
            {
                var loggingError = await _context.LoggingErrors.FirstOrDefaultAsync(f => f.LoggingErrorId == id);
                if (loggingError != null)
                {
                    _context.LoggingErrors.Remove(loggingError);
                    await _context.SaveChangesAsync();

                    deleteResult = true;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            return deleteResult;
        }

    }
}
