﻿using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ICollectionService 
    {
        Task<IEnumerable<CollectionDTO>> GetAllCollectionsAsync();

        // TODO: Implement
        Task<IEnumerable<CollectionDTO>> GetAllCollectionsByUserIdAsync(Guid id);
        Task<IEnumerable<CollectionDTO>> GetLargestCollecitonAsync();
        Task<IEnumerable<CollectionDTO>> GetLatestCollectionAsync();
        Task<CollectionDTO> GetCollectionByUserIdAsync(Guid id);

        Task<CollectionDTO> GetCollectionByIdAsync(Guid id);
        Task<OperationResult> AddCollectionAsync(CollectionRequestDto collection);
        Task<OperationResult> UpdateCollectionAsync(CollectionRequestDto collection);
        Task<OperationResult> DeleteCollectionAsync(Guid id);
    }
}
