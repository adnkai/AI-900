
class Classify {
    public Classify() {

    }

    // Text Classification

    public static void SentimentAnalysisExample(TextAnalyticsClient client)
	{
		string inputText = "I had the best day of my life. I wish you were there with me.";
		DocumentSentiment documentSentiment = client.AnalyzeSentiment(inputText);
		Console.WriteLine($"Document sentiment: {documentSentiment.Sentiment}\n");

		foreach (var sentence in documentSentiment.Sentences)
		{
			Console.WriteLine($"\tText: \"{sentence.Text}\"");
			Console.WriteLine($"\tSentence sentiment: {sentence.Sentiment}");
			Console.WriteLine($"\tPositive score: {sentence.ConfidenceScores.Positive:0.00}");
			Console.WriteLine($"\tNegative score: {sentence.ConfidenceScores.Negative:0.00}");
			Console.WriteLine($"\tNeutral score: {sentence.ConfidenceScores.Neutral:0.00}\n");
		}
	}
	
    public static void LanguageDetectionExample(TextAnalyticsClient client)
	{
		DetectedLanguage detectedLanguage = client.DetectLanguage("Ce document est rdig en Franais.");
		Console.WriteLine("Language:");
		Console.WriteLine($"\t{detectedLanguage.Name}\tISO-6391: {detectedLanguage.Iso6391Name}\n");
	}
}