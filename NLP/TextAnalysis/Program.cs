class Program 
{

private static readonly AzureKeyCredential credentials = new AzureKeyCredential("SUBSCRIPTION_KEY");
private static readonly Uri endpoint = new Uri("ENDPOINT_URI");

	static void Main(string[] args)
	{
		var client = new TextAnalyticsClient(endpoint, credentials);

        // Text Extraction
		// TextExtraction.EntityRecognitionExample(client);
		// TextExtraction.EntityLinkingExample(client);
		// TextExtraction.RecognizePIIExample(client); // Personally Identifying Information
		// TextExtraction.KeyPhraseExtractionExample(client);

        // // Laguage Detection
        // Classify.LanguageDetectionExample(client);
        // Classify.SentimentAnalysisExample(client);

        // Text Summarizing
        Summarize.TextSummarizationExample(client).GetAwaiter().GetResult();


		Console.Write("Press any key to exit.");
		Console.ReadKey();
	}
	
}