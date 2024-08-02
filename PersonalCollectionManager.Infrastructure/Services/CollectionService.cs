﻿
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

        // TODO: Implement this method
        public async Task<IEnumerable<CollectionDto>> GetLargestCollecitonAsync()
        {
            try
            {
                var collectios = await _collectionRepository.GetLargestCollectionsAsync(5);
                var collectionDTOs = _mapper.Map<IEnumerable<CollectionDto>>(collectios);
                return collectionDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting largest collections");
                return null;

            }
        }


        public async Task<OperationResult> UpdateCollectionAsync(CollectionDto collectionDTO)
        {
            try
            {
                var collection = _mapper.Map<Collection>(collectionDTO);
                await _collectionRepository.Update(collection);

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
