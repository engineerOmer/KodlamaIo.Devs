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

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest<UpdateLanguageDto>
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdateLanguageDto>
        {
            private readonly ILanguageRepository languageRepository;

            private readonly IMapper mapper;

            private readonly LanguageBusinessRules languageBusinessRules;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBusinessRules = languageBusinessRules; 
            }

            public async Task<UpdateLanguageDto> Handle(UpdateLanguageCommand request , CancellationToken cancellationToken)
            {
                await languageBusinessRules.LanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                Language? language = await languageRepository.GetAsync(b => b.Id == request.Id);
                language.Name = request.Name;   
                Language updateLanguage = await languageRepository.UpdateAsync(language);
                UpdateLanguageDto updateLanguageDto = mapper.Map<UpdateLanguageDto>(updateLanguage);

                return updateLanguageDto;
            }

        }


    }
}
