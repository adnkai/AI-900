
class Program {
    // Add your Computer Vision subscription key and endpoint
    static string subscriptionKey = "SUBSCRIPTION_KEY";
    static string endpoint = "ENDPOINT";
    private const string ANALYZE_URL_IMAGE = "https://IMAGE_TO_ANALYZE";

    static List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
        {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
            VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
            VisualFeatureTypes.Objects
        };

    static async Task Main(string []args) {
        Console.WriteLine($"Analyzing the image {Path.GetFileName(ANALYZE_URL_IMAGE)}...");
        Console.WriteLine();
        var client = Authenticate(endpoint, subscriptionKey);
        // Analyze the URL image 
        ImageAnalysis results = await client.AnalyzeImageAsync(ANALYZE_URL_IMAGE, visualFeatures: features);
        Summarizes(results);
        Categories(results);
        Tags(results);
        Objects(results);
        Brands(results);
        Faces(results);
        IsRacy(results);
        Colors(results);
        Celebrities(results);
        Landmarks(results);
        Type(results);
        DrawBoundingbox(results);
    }

    public static ComputerVisionClient Authenticate(string endpoint, string key)
    {
        ComputerVisionClient client =
        new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
        { Endpoint = endpoint };
        return client;
    }

    public static void Summarizes(ImageAnalysis results) {
        // Sunmarizes the image content.
        Console.WriteLine("Summary:");
        foreach (var caption in results.Description.Captions)
        {
            Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
        }
        Console.WriteLine();
    }

    public static void Categories(ImageAnalysis results) {
        // Display categories the image is divided into.
        Console.WriteLine("Categories:");
        foreach (var category in results.Categories)
        {
            Console.WriteLine($"{category.Name} with confidence {category.Score}");
        }
        Console.WriteLine();
    }

    public static void Tags(ImageAnalysis results) {
        // Image tags and their confidence score
        Console.WriteLine("Tags:");
        foreach (var tag in results.Tags)
        {
            Console.WriteLine($"{tag.Name} {tag.Confidence}");
        }
        Console.WriteLine();
    }

    public static void Objects(ImageAnalysis results) {
        // Objects
        Console.WriteLine("Objects:");
        foreach (var obj in results.Objects)
        {
            Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " +
            $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
        }
        Console.WriteLine();
    }

    public static void Brands(ImageAnalysis results) {
        
        // Well-known (or custom, if set) brands.
        Console.WriteLine("Brands:");
        foreach (var brand in results.Brands)
        {
            Console.WriteLine($"Logo of {brand.Name} with confidence {brand.Confidence} at location {brand.Rectangle.X}, " +
            $"{brand.Rectangle.X + brand.Rectangle.W}, {brand.Rectangle.Y}, {brand.Rectangle.Y + brand.Rectangle.H}");
        }
        Console.WriteLine();
    }

    public static void Faces(ImageAnalysis results) {
        // Faces
        Console.WriteLine("Faces:");
        foreach (var face in results.Faces)
        {
            Console.WriteLine($"A {face.Gender} of age {face.Age} at location {face.FaceRectangle.Left}, " +
            $"{face.FaceRectangle.Left}, {face.FaceRectangle.Top + face.FaceRectangle.Width}, " +
            $"{face.FaceRectangle.Top + face.FaceRectangle.Height}");
        }
        Console.WriteLine();
    }

    public static void IsRacy(ImageAnalysis results) {
        // Adult or racy content, if any.
        Console.WriteLine("Adult:");
        Console.WriteLine($"Has adult content: {results.Adult.IsAdultContent} with confidence {results.Adult.AdultScore}");
        Console.WriteLine($"Has racy content: {results.Adult.IsRacyContent} with confidence {results.Adult.RacyScore}");
        Console.WriteLine($"Has gory content: {results.Adult.IsGoryContent} with confidence {results.Adult.GoreScore}");
        Console.WriteLine();
    }

    public static void Colors(ImageAnalysis results) {
        // Identifies the color scheme.
        Console.WriteLine("Color Scheme:");
        Console.WriteLine("Is black and white?: " + results.Color.IsBWImg);
        Console.WriteLine("Accent color: " + results.Color.AccentColor);
        Console.WriteLine("Dominant background color: " + results.Color.DominantColorBackground);
        Console.WriteLine("Dominant foreground color: " + results.Color.DominantColorForeground);
        Console.WriteLine("Dominant colors: " + string.Join(",", results.Color.DominantColors));
        Console.WriteLine();
    }
    public static void Celebrities(ImageAnalysis results) {
        // Celebrities in image, if any.
        Console.WriteLine("Celebrities:");
        foreach (var category in results.Categories)
        {
            if (category.Detail?.Celebrities != null)
            {
                foreach (var celeb in category.Detail.Celebrities)
                {
                    Console.WriteLine($"{celeb.Name} with confidence {celeb.Confidence} at location {celeb.FaceRectangle.Left}, " +
                    $"{celeb.FaceRectangle.Top}, {celeb.FaceRectangle.Height}, {celeb.FaceRectangle.Width}");
                }
            }
        }
        Console.WriteLine();
    }

    public static void Landmarks(ImageAnalysis results) {
        // Popular landmarks in image, if any.
        Console.WriteLine("Landmarks:");
        foreach (var category in results.Categories)
        {
            if (category.Detail?.Landmarks != null)
            {
                foreach (var landmark in category.Detail.Landmarks)
                {
                    Console.WriteLine($"{landmark.Name} with confidence {landmark.Confidence}");
                }
            }
        }
        Console.WriteLine();
    }

    public static void Type(ImageAnalysis results) {
        // Detects the image types.
        Console.WriteLine("Image Type:");
        Console.WriteLine("Clip Art Type: " + results.ImageType.ClipArtType);
        Console.WriteLine("Line Drawing Type: " + results.ImageType.LineDrawingType);
        Console.WriteLine();
    }


    public static void DrawBoundingbox(ImageAnalysis results) {
        // Use OCR API to read text in image
        var ANALYZE_URL_IMAGE2 = @"LOCAL_IMG_TO_ANALYZE";
        using (var imageData = File.OpenRead(ANALYZE_URL_IMAGE2))
        {    
            // Prepare image for drawing
            Image image = Image.FromFile(ANALYZE_URL_IMAGE2);
            Graphics graphics = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Magenta, 3);

            /*
            
            foreach (var face in results.Faces)
            {
               Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location 
               {obj.Rectangle.X}, " +
            $"{obj.Rectangle.X + obj.Rectangle.W}, 
            {obj.Rectangle.Y}, 
            {obj.Rectangle.Y + obj.Rectangle.H}");
            }
            */

            foreach(var obj in results.Objects)
            {


                    // Show the position of the line of text
                Rectangle rect = new Rectangle(obj.Rectangle.X, obj.Rectangle.Y, obj.Rectangle.W, obj.Rectangle.H);
                graphics.DrawRectangle(pen, rect);

            }

            // Save the image with the text locations highlighted
            String output_file = "ocr_results.jpg";
            image.Save(output_file);
            Console.WriteLine("Results saved in " + output_file);
        }
    }

}