
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using AutoMapper;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TagService(ITagRepository tagRepository,IMapper mapper,ILogger<TagService> logger)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<OperationResult> AddTagAsync(TagRequestDto tag)
        {
            try
            {
                var tagEntity = _mapper.Map<Tag>(tag);
                _tagRepository.AddAsync(tagEntity);

                return Task.FromResult(new OperationResult(true, "Tag added successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tag.");
                return Task.FromResult(new OperationResult(false, "Error adding tag."));
            }
        }

        public async Task<OperationResult> DeleteTagAsync(Guid id)
        {
            try
            {
                var tagEntity = await GetTagByIdAsync(id);
                if (tagEntity != null)
                {
                    var tag = _mapper.Map<Tag>(tagEntity);
                    _tagRepository.Remove(tag); 
                    return new OperationResult(true, "Tag deleted successfully.");
                }

                return new OperationResult(false, "Tag not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tag.");
                return new OperationResult(false, "Error deleting tag.");
            }
        }


        public async Task<IEnumerable<TagDTO>> GetAllTagAsync()
        {
            try
            {
                var tags = await _tagRepository.GetAllAsync();
                
                return _mapper.Map<IEnumerable<TagDTO>>(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tags.");
                throw; 
            }
        }

        public async Task<TagDTO> GetTagByIdAsync(Guid id)
        {
            try
            {
                var tag = await _tagRepository.GetByIdAsync(id);
                return _mapper.Map<TagDTO>(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tag by id.");
                throw;
            }
        }

        public Task<OperationResult> UpdateTagAsync(TagRequestDto tag)
        {
            try
            {
                var tagEntity = _mapper.Map<Tag>(tag);
                _tagRepository.Update(tagEntity);

                return Task.FromResult(new OperationResult(true, "Tag updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag.");
                return Task.FromResult(new OperationResult(false, "Error updating tag."));
            }
        }
    }
}
