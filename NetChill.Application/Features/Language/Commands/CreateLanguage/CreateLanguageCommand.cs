using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Common.Mappings;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Language;
using NetChill.Shared;

namespace NetChill.Application.Features.Language.Commands.CreateLanguage
{
    public record CreateLanguageCommand : IRequest<Result<int>>,  IMapFrom<MovieLanguage>
    {
        public string SpokenLanguage { get; set; }
        public string LanguageNotes { get; set; }
    }

    internal class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper  _mapper;

        public CreateLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateLanguageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var language = new MovieLanguage()
                {
                    SpokenLanguage = command.SpokenLanguage,
                    LanguageNotes = command.LanguageNotes
                };

                await _unitOfWork.Repository<MovieLanguage>().InsertAsync(language);
                language.AddDomainEvent(new LanguageCreatedEvent(language));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(language.LanguageId, "Language Created Successfully.");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ResponseConstants.Error520, ex);
            }
        }
    }
}
