using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Domain.Events.Language
{
    public class LanguageCreatedEvent : BaseEvent
    {
        public MovieLanguage Language { get; }

        public LanguageCreatedEvent(MovieLanguage movieLanguage)
        {
            Language = movieLanguage;
        }
    }
}
