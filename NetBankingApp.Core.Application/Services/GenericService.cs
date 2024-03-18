using AutoMapper;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;

namespace NetBankingApp.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        protected readonly IGenericRepository<Entity> _repo;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> CreateAsync(SaveViewModel viewModel)
        {
            Entity entity = await _repo.CreateAsync(_mapper.Map<Entity>(viewModel));
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(await _repo.GetByIdAsync(id));
        }

        public virtual async Task<List<ViewModel>> GetAllAsync()
        {
            return _mapper.Map<List<ViewModel>>(await _repo.GetAllAsync());
        }


        public virtual async Task<SaveViewModel> GetByIdSaveViewModelAsync(int id)
        {
            return _mapper.Map<SaveViewModel>(await _repo.GetByIdAsync(id));

        }

        public virtual async Task<ViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<ViewModel>(await _repo.GetByIdAsync(id));
        }

        public virtual async Task<SaveViewModel> UpdateAsync(SaveViewModel viewModel, int id)
        {
            Entity entity = await _repo.UpdateAsync(_mapper.Map<Entity>(viewModel), id);
            return _mapper.Map<SaveViewModel>(entity);
        }
    }
}
