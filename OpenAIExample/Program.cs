using System.Net.Http.Json;

public class OpenAIExample
{
    private static string apiKey = "---YOUR API KEY here---";
    private static readonly HttpClient client = new();

    public static async Task Main(string[] args)
    {
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        await ImageGeneration();
        await Completions();
        await Embedding();
        await Classification();
    }

    /// <summary>
    /// The image generations endpoint allows you to create an original image given a text prompt.
    /// By default, images are generated at standard quality, but when using DALL·E 3 you can set quality: "hd" for enhanced detail.
    /// Square, standard quality images are the fastest to generate.
    /// </summary>
    public static async Task ImageGeneration()
    {
        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/images/generations", new
        {
            model = "dall-e-3",
            prompt = "a white siamese cat",
            size = "1024x1024",
            n = 1,
        });

        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
    }

    /// <summary>
    /// This example shows how to generate text using the HttpClient class.
    /// </summary>
    public static async Task Completions()
    {
        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/completions", new
        {
            model = "gpt-3.5-turbo",
            prompt = "This is a test. The AI will continue writing after this sentence.",
            max_tokens = 50,
            temperature = 0.7
        });

        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
    }

    /// <summary>
    /// Fine-tuning Models C# doesn't directly support fine-tuning through a library as the Python OpenAI library does.
    /// Fine-tuning typically requires uploading a dataset to OpenAI, initiating the fine-tuning process,
    /// and then polling for its completion.
    /// This process is more involved and might be better managed through OpenAI's web interface
    /// or using the OpenAI CLI tool. However, you can still interact with the API for fine-tuning operations using HttpClient.
    /// </summary>
    public static async Task Embedding()
    {
        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/embeddings", new
        {
            model = "text-similarity-davinci-001",
            input = "This is a test sentence for embedding."
        });

        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
    }

    /// <summary>
    /// This example demonstrates how to use the OpenAI API for classification.
    /// You would typically use the search endpoint or embeddings for this, but for simplicity,
    /// we'll use the completions endpoint to simulate a classification task.
    /// </summary>
    public static async Task Classification()
    {
        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/completions", new
        {
            model = "gpt-3.5-turbo",
            prompt = "Classify the following text as positive, neutral, or negative sentiment: 'I love sunny days but hate the rain.'",
            temperature = 0,
            max_tokens = 5
        });

        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
    }
}