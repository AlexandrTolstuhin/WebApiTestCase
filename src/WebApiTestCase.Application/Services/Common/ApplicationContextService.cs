using System;
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
            int result;
            try
            {
                result = await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new BadRequestException("Ошибка при попытке сохранения изменений в базу данных", e);
            }

            if (result < 1)
                throw new BadRequestException("Отсутствуют сохраненные изменения");
        }
    }
}