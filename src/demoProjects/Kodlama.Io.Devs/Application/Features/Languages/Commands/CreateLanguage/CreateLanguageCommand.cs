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

namespace Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand:IRequest<CreateLanguageDto>
    {
        public string Name { get; set; }    

        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreateLanguageDto>
        {
            private readonly ILanguageRepository languageRepository;

            private readonly IMapper mapper;

            private readonly LanguageBusinessRules languageRules;

            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageRules = languageRules;
            }

            public async Task<CreateLanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken) 
            {
                await languageRules.LanguageNameCanNotBeDuplicatedWhenInserted(request.Name);
                Language mapperLanguage = mapper.Map<Language>(request);
                Language createLanguage = await languageRepository.AddAsync(mapperLanguage);
                CreateLanguageDto createLanguageDto = mapper.Map<CreateLanguageDto>(createLanguage);    

                return createLanguageDto;   

            }

        }
    }
}
