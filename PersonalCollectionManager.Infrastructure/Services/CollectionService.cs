
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<Collection> _logger;
        public CollectionService(ICollectionRepository collection,IMapper mapper, ILogger<Collection> loger)
        {
            _collectionRepository = collection;
            _mapper = mapper;
            _logger = loger;
        }
        public async Task<OperationResult> AddCollectionAsync(CollectionRequestDto collection)
        {
            try
            {
                var collectionEntity = _mapper.Map<Collection>(collection);
                await _collectionRepository.AddAsync(collectionEntity);

                return new OperationResult(true, "Collection added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding collection");
                return new OperationResult(false, $"Error adding collection: {ex.Message}");
            }
        }

        public async Task<OperationResult> DeleteCollectionAsync(Guid id)
        {
            try
            {
                var collection = await _collectionRepository.GetByIdAsync(id);
                _collectionRepository.Remove(collection);

                return  new OperationResult(true, "Collection Deleting successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting collection");
                return new OperationResult(false, "Error Deleting collection");
            }
        }

        public async Task<IEnumerable<CollectionDTO>> GetAllCollectionsAsync()
        {
            try
            {
                var user = _mapper.Map<IEnumerable<CollectionDTO>>(await _collectionRepository.GetAllAsync());
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all collections");
                throw;
            }
        }

        public async Task<CollectionDTO> GetCollectionByIdAsync(Guid id)
        {
            try
            {
                var user  = _mapper.Map<CollectionDTO>(await _collectionRepository.GetByIdAsync(id));
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting collection by id");
                throw;
            }
        }

        public async Task<OperationResult> UpdateCollectionAsync(CollectionRequestDto collectionDTO)
        {
            try
            {
                var user = _mapper.Map<Collection>(collectionDTO);
                _collectionRepository.Update(user);

                return await Task.FromResult(new OperationResult(true, "Collection updated successfully"));
            }
            catch
            {
                _logger.LogError("Error updating collection");
                return await Task.FromResult(new OperationResult(false, "Error updating collection"));
            }
        }
    }
}
