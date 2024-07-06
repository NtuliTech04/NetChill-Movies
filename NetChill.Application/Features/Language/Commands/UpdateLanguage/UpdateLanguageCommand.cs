using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Language;
using NetChill.Shared;

namespace NetChill.Application.Features.Language.Commands.UpdateLanguage
{
    public record UpdateLanguageCommand : IRequest<Result<int>>
    {
        public int LanguageId { get; set; }
        public string SpokenLanguage { get; set; }
        public string LanguageNotes { get; set; }
    }


    internal class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateLanguageCommand command, CancellationToken cancellationToken)
        {
            var language = await _unitOfWork.Repository<MovieLanguage>().GetByIdAsync(command.LanguageId);

            if (language != null)
            {
                try
                {
                    //language.IntId = command.LanguageId;
                    language.BaseId = command.LanguageId;
                    language.SpokenLanguage = command.SpokenLanguage;
                    language.LanguageNotes = command.LanguageNotes;

                    await _unitOfWork.Repository<MovieLanguage>().UpdateAsync(language);
                    language.AddDomainEvent(new LanguageUpdatedEvent(language));

                    await _unitOfWork.Save(cancellationToken);

                    //return await Result<int>.SuccessAsync(language.IntId, "Language Updated Successfully.");
                    return await Result<int>.SuccessAsync(Convert.ToInt32(language.BaseId), "Language Updated Successfully.");

                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else 
            { 
                return await Result<int>.FailureAsync("Language Not Found."); 
            }
        }
    }
}
