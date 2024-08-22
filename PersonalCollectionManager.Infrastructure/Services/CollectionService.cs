
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class CollectionService : ICollectionService
    {

        private readonly ICollectionRepository _collectionRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<Collection> _logger;

        public CollectionService(ICollectionRepository collection,IConfiguration configuration,IMapper mapper, ILogger<Collection> loger)
        {
            _collectionRepository = collection;
            _configuration = configuration;
            _mapper = mapper;
            _logger = loger;
        }
        public async Task<CollectionDto> AddCollectionAsync(CollectionRequestDto collection)
        {
            try
            {
                var collectionEntity = _mapper.Map<Collection>(collection);
                var result = await _collectionRepository.AddAsync(collectionEntity);

                return _mapper.Map<CollectionDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding collection");
                return null;
            }
        }

        public async Task<OperationResult> DeleteCollectionAsync(Guid id)
        {
            try
            {
                var collection = await _collectionRepository.GetByIdAsync(id);
                await _collectionRepository.Remove(collection);

                return  new OperationResult(true, "Collection Deleting successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting collection");
                return new OperationResult(false, "Error Deleting collection");
            }
        }

        public async Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync()
        {
            try
            {
                var collection = _mapper.Map<IEnumerable<CollectionDto>>(await _collectionRepository.GetAllCollectionAsync());
                return collection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all collections");
                return null; 
            }
        }

        public async Task<IEnumerable<CollectionDto>> GetAllCollectionsByUserIdAsync(Guid id)
        {
            try
            {
                var collerctions = await _collectionRepository.GetCollectionsByUserIdAsync(id);
                var collection = _mapper.Map<IEnumerable<CollectionDto>>(collerctions);
                return collection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all collections by user id");
                return null;
            }
                
        }

        public async Task<CollectionDto> GetCollectionByIdAsync(Guid id)
        {
            try
            {
                var collection  = _mapper.Map<CollectionDto>(await _collectionRepository.GetCollectionByIdAsync(id));
                return collection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting collection by id");
                return null;
            }
        }

        public async Task<CollectionDto> GetCollectionByUserIdAsync(Guid id)
        {
            try
            {
                var collection = await _collectionRepository.GetCollectionByUserIdAsync(id);
                var collectionDTO = _mapper.Map<CollectionDto>(collection);
                return collectionDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting collection by user id");
                return null;
            }
        }

        public async Task<IEnumerable<CollectionDto>> GetLargestCollecitonAsync()
        {
            try
            {
                var collectionCountStr = _configuration.GetSection("PaginationSettings:NumberOfColleciton").Value;
                int collectionCount = 8;


                if (!string.IsNullOrEmpty(collectionCountStr) && int.TryParse(collectionCountStr, out int parsedCount))
                {
                    collectionCount = parsedCount;
                }

                var collectios = await _collectionRepository.GetLargestCollectionsAsync(collectionCount);
                var collectionDTOs = _mapper.Map<IEnumerable<CollectionDto>>(collectios);
                return collectionDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting largest collections");
                return null;

            }
        }


        public async Task<CollectionDto> UpdateCollectionAsync(CollectionUpdateDto collectionDTO)
        {
            try
            {
                var collection = _mapper.Map<Collection>(collectionDTO);
                var result = await _collectionRepository.Update(collection);

                return _mapper.Map<CollectionDto>(result);
            }
            catch
            {
                _logger.LogError("Error updating collection");
                return null;
            }
        }
    }
}
