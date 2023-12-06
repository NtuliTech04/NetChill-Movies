using AutoMapper;
using MediatR;
using NetChill.Application.Common.Mappings;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Language;
using NetChill.Shared;

namespace NetChill.Application.Features.Language.Commands.DeleteLanguage
{
    public record DeleteLanguageCommand : IRequest<Result<int>>, IMapFrom<MovieLanguage>
    {
        public int LanguageId { get; set; }

        public DeleteLanguageCommand()
        {
            
        }

        public DeleteLanguageCommand(int languageId)
        {
            LanguageId = languageId;
        }
    }

    internal class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<int>> Handle(DeleteLanguageCommand command, CancellationToken cancellationToken)
        {
            var language = await _unitOfWork.Repository<MovieLanguage>().GetByIntIdAsync(command.LanguageId);

            if (language != null)
            {
                await _unitOfWork.Repository<MovieLanguage>().DeleteAsync(language);
                language.AddDomainEvent(new LanguageDeletedEvent(language));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(language.LanguageId, "Language Deleted Successfully.");
            }
            else
            {
                return await Result<int>.FailureAsync("Language Not Found.");
            }
        }
    }
}
