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

namespace Application.Features.Languages.Queries.GetByIdLanguage
{
    public class GetByIdLanguageQuery : IRequest<LanguageGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, LanguageGetByIdDto>
        {
            private readonly ILanguageRepository languageRepository;
            private readonly IMapper mapper;
            private readonly LanguageBusinessRules languageBusinessRules;

            public GetByIdLanguageQueryHandler(ILanguageRepository languageRepository, IMapper mapper,LanguageBusinessRules languageBusinessRules )
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBusinessRules = languageBusinessRules;
            }

            public async Task<LanguageGetByIdDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
            {
                Language? language = await languageRepository.GetAsync(b => b.Id == request.Id);

                languageBusinessRules.LanguageShouldExistWhenRequested(language);

                LanguageGetByIdDto brandGetByIdDto = mapper.Map<LanguageGetByIdDto>(language);
                return brandGetByIdDto;
            }
        }
    }
}
