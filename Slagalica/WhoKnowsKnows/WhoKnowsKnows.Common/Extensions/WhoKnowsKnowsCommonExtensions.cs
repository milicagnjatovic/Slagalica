using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoKnowsKnows.Common.Data;
using WhoKnowsKnows.Common.Entities;
using WhoKnowsKnows.Common.Repositories;
using WhoKnowsKnows.Common.Repositories.Interfaces;

namespace WhoKnowsKnows.Common.Extensions
{
    public static class WhoKnowsKnowsCommonExtensions
    {
        public static void AddWhoKnowsKnowsCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IQuestionContext, QuestionContext>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddAutoMapper(configuration => {
                configuration.CreateMap<GetQuestionDTO, Question>().ReverseMap();
            });
        }
    }
}
