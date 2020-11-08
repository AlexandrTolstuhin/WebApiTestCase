using WebApiTestCase.Application.Exceptions;
using WebApiTestCase.Data;

namespace WebApiTestCase.Application.Services.Common
{
    public abstract class ApplicationContextService
    {
        protected readonly ApplicationContext Context;

        protected ApplicationContextService(ApplicationContext context)
        {
            Context = context;
        }

        protected async System.Threading.Tasks.Task SaveChangesAsync()
        {
            var result = await Context.SaveChangesAsync();

            if (result < 1)
                throw new BadRequestException("Отсутствуют сохраненные изменения");
        }
    }
}