class TextExtraction {
    public TextExtraction() {
        
    }

    // Extract Information
	public static void EntityRecognitionExample(TextAnalyticsClient client)
	{
		var response = client.RecognizeEntities("I had a wonderful trip to Seattle last week.");
		Console.WriteLine("Named Entities:");
		foreach (var entity in response.Value)
		{
			Console.WriteLine($"\tText: {entity.Text}\tCategory: {entity.Category}\tSub-Category: {entity.SubCategory}");
			Console.WriteLine($"\tScore: {entity.ConfidenceScore:F2}\tLength: {entity.Length}\tOffset: {entity.Offset}\n");
		}
	}
	
	public static void RecognizePIIExample(TextAnalyticsClient client)
	{
		string document = "A developer with SSN 859-98-0987 whose phone number is 800-102-1100 is building tools with our APIs.";

		PiiEntityCollection entities = client.RecognizePiiEntities(document).Value;

		Console.WriteLine($"Redacted Text: {entities.RedactedText}");
		if (entities.Count > 0)
		{
			Console.WriteLine($"Recognized {entities.Count} PII entit{(entities.Count > 1 ? "ies" : "y")}:");
			foreach (PiiEntity entity in entities)
			{
				Console.WriteLine($"Text: {entity.Text}, Category: {entity.Category}, SubCategory: {entity.SubCategory}, Confidence score: {entity.ConfidenceScore}");
			}
		}
		else
		{
			Console.WriteLine("No entities were found.");
		}
	}
	
	
	public static void EntityLinkingExample(TextAnalyticsClient client)
	{
		var response = client.RecognizeLinkedEntities(
			"Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975 " +
			"to develop and sell BASIC interpreters for the Altair 8800. " +
			"During his career at Microsoft, Gates held the positions of chairman " +
			"chief executive officer, president and chief software architect " +
			"while also being the largest individual shareholder until May 2014.");
		Console.WriteLine("Linked Entities:");
		foreach (var entity in response.Value)
		{
			Console.WriteLine($"\tName: {entity.Name}\tID: {entity.DataSourceEntityId}\tURL: {entity.Url}\tData Source: {entity.DataSource}");
			Console.WriteLine("\tMatches:");
			foreach (var match in entity.Matches)
			{
				Console.WriteLine($"\t\tText: {match.Text}");
				Console.WriteLine($"\t\tScore: {match.ConfidenceScore:F2}");
				Console.WriteLine($"\t\tLength: {match.Length}");
				Console.WriteLine($"\t\tOffset: {match.Offset}\n");
			}
		}
	}
	
	public static void KeyPhraseExtractionExample(TextAnalyticsClient client)
	{
		var response = client.ExtractKeyPhrases("My cat might need to see a veterinarian.");

		// Printing key phrases
		Console.WriteLine("Key phrases:");

		foreach (string keyphrase in response.Value)
		{
			Console.WriteLine($"\t{keyphrase}");
		}
	}
}