// Code initially courtesy of https://web.archive.org/web/20181015004944/http://www.robosoup.com/2016/02/word2vec-lightweight-port-c.html
using Word2Vec;

// Good corpus text files can be found here: https://wortschatz.uni-leipzig.de/en/download/English
const string corpusTrainingFilePath = "eng_news_2023_1M-sentences.txt";
const string modelFilePath = corpusTrainingFilePath + ".model.bin";

bool shouldBuildModel;
if (!File.Exists(modelFilePath))
{
    if (!File.Exists(corpusTrainingFilePath))
    {
        Console.WriteLine("Neither training file nor model file exist, exiting..");
        return;
    }

    Console.WriteLine("Model file does not exist, will construct it from the corpus..");
    shouldBuildModel = true;
}
else
{
    if (!File.Exists(corpusTrainingFilePath))
    {
        Console.WriteLine("The model file exists, but the training file does not, so there is no option to rebuild - the previously-built model will be used");
        shouldBuildModel = false;
    }
    else
    {
        Console.Write("Both the training file and model file are present - does the model need to rebuilt? {Y/N} ");
        shouldBuildModel = Console.ReadKey().Key == ConsoleKey.Y;
        Console.WriteLine();
    }
}
Console.WriteLine();

if (shouldBuildModel)
{
    // Train vector model and save to file.
    var word2vec = new Trainer();
    word2vec.Train(corpusTrainingFilePath, modelFilePath, Normaliser.Normalise);
}

// Load model from file.
var model = new Model();
model.LoadVectors(modelFilePath);

// Get 10 nearest words to user input
while (true)
{
    Console.Write("Input > ");
    var word = Console.ReadLine();
    var nearest = model.NearestWords(Normaliser.Normalise(word), 10);
    if ((nearest is null) || !nearest.Any())
    {
        Console.WriteLine("No matches");
    }
    else
    {
        foreach (var item in nearest)
            Console.WriteLine("{0:0.000}\t{1}", item.Value, item.Key);
    }
    Console.WriteLine();
}