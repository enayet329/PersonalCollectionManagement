using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Data.Repositories;
using PersonalCollectionManager.Domain.Entities;

public class AlgoliaItemService
{
    private readonly ISearchIndex _index;
    private readonly IMapper _mapper;

    public AlgoliaItemService(IConfiguration configuration, IMapper mapper)
    {
        var client = new SearchClient(configuration["Algolia:ApplicationId"], configuration["Algolia:AdminApiKey"]);
        _index = client.InitIndex(configuration["Algolia:IndexNameItems"]);
        _mapper = mapper;
    }

    public async Task IndexItemAsync<T>(T item, string objectId) where T : class
    {
        try
        {
            var algoliaItem = _mapper.Map<AlgoliaItemDto>(item);

            algoliaItem.ObjectID = objectId;


            var response = await _index.SaveObjectsAsync( new List<AlgoliaItemDto> { algoliaItem });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error indexing item: {ex.Message}");
        }
    }

    public async Task<SearchResponse<ItemDto>> SearchAsync<T>(string query) where T : class
    {
        try
        {
            return await _index.SearchAsync<ItemDto>(new Query(query));
        }
        catch (Exception ex)
        { 
            Console.WriteLine($"Error searching for items: {ex.Message}");
            return null;
        }
    }

    public async Task UpdateItemAsync<T>(T item, string objectId) where T : class
    {
        try
        {
            await IndexItemAsync(item, objectId);
        }
        catch (Exception ex)
        { 
            Console.WriteLine($"Error updating item: {ex.Message}");
        }
    }

    public async Task DeleteItemAsync(string objectId)
    {
        try
        {
            await _index.DeleteObjectAsync(objectId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting item: {ex.Message}");
        }
    }
}