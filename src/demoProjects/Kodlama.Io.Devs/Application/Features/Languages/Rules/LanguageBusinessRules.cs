using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Rules
{
    public class LanguageBusinessRules
    {
        private readonly ILanguageRepository languageRepository;

        public LanguageBusinessRules(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public async Task LanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Language> result = await languageRepository.GetListAsync(b => b.Name.ToLower() == name.ToLower());
            if (result.Items.Any()) throw new BusinessException("Language name already exists.");
        }

        public void LanguageShouldExistWhenRequested(Language language)
        {
            if (language == null) throw new BusinessException("Requested language does not exist");
        }

        public async Task LanguageTheRequestedIdDoesNotExist(int id)
        {
            Language? result = await languageRepository.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException("The requested id does not exist");
        }
    }
}
