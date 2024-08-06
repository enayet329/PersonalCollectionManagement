using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

public class AlgoliaService
{
    private readonly ISearchIndex _index;
    private readonly IMapper _mapper;

    public AlgoliaService(IConfiguration configuration, IMapper mapper)
    {
        var client = new SearchClient(configuration["Algolia:ApplicationId"], configuration["Algolia:AdminApiKey"]);
        _index = client.InitIndex(configuration["Algolia:IndexName"]);

        _mapper = mapper;
    }

    public async Task IndexItemAsync<T>(T item, string objectId) where T : class
    {
        try
        {
            var algoliaItem = _mapper.Map<ItemAlgoliaDto>(item);

            algoliaItem.ObjectID = objectId;


            var response = await _index.SaveObjectsAsync( new List<ItemAlgoliaDto> { algoliaItem });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error indexing item: {ex.Message}");
        }
    }

    public async Task<SearchResponse<T>> SearchAsync<T>(string query) where T : class
    {
        try
        {
            return await _index.SearchAsync<T>(new Query(query));
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
            // Handle the exception  
            Console.WriteLine($"Error deleting item: {ex.Message}");
        }
    }
}