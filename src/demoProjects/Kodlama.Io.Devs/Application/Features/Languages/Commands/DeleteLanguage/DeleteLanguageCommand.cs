using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest<DeleteLanguageDto>
    {   
        public int Id { get; set; } 

        public class CreateLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, DeleteLanguageDto>
        {
            private readonly ILanguageRepository languageRepository;

            private readonly IMapper mapper;

            private readonly LanguageBusinessRules languageBusinessRules;


            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBusinessRules = languageBusinessRules; 
            }

            public async Task<DeleteLanguageDto> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                await languageBusinessRules.LanguageTheRequestedIdDoesNotExist(request.Id);

                Language? language = await languageRepository.GetAsync(b => b.Id == request.Id);         
                Language deleteLanguage = await languageRepository.DeleteAsync(language);
                DeleteLanguageDto deleteLanguageDto = mapper.Map<DeleteLanguageDto>(deleteLanguage);

                return deleteLanguageDto;

            }

        }
    }
}
