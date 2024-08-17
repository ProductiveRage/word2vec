namespace Word2Vec;

// Code initially courtesy of https://web.archive.org/web/20181015004944/http://www.robosoup.com/2016/02/word2vec-lightweight-port-c.html
public class Model
{
    private int Dimensions;
    private readonly Dictionary<string, float[]> model = new();
    private int wordCount;
    
    public Dictionary<string, float> NearestWords(string word, int count)
    {
        var vec = WordVector(word);
        if (vec == null) return new Dictionary<string, float>();
        var bestd = new float[count];
        var bestw = new string[count];
        for (var n = 0; n < count; n++) bestd[n] = -1;
        foreach (var key in model.Keys)
        {
            var dist = 0f;
            for (var i = 0; i < Dimensions; i++) dist += vec[i] * model[key][i];
            for (var c = 0; c < count; c++)
                if (dist > bestd[c])
                {
                    for (var i = count - 1; i > c; i--)
                    {
                        bestd[i] = bestd[i - 1];
                        bestw[i] = bestw[i - 1];
                    }
                    bestd[c] = dist;
                    bestw[c] = key;
                    break;
                }
        }
        var result = new Dictionary<string, float>();
        for (var i = 0; i < count; i++) result.Add(bestw[i], bestd[i]);
        return result;
    }
    
    public float[] WordVector(string word)
    {
        if (!model.ContainsKey(word)) return null;
        return model[word];
    }
    
    public void LoadVectors(string model_file)
    {
        var file = model_file;
        using var br = new BinaryReader(File.Open(file, FileMode.Open));
        wordCount = br.ReadInt32();
        Dimensions = br.ReadInt32();
        for (var w = 0; w < wordCount; w++)
        {
            var word = br.ReadString();
            var vec = new float[Dimensions];
            for (var d = 0; d < Dimensions; d++) vec[d] = br.ReadSingle();
            Normalise(vec);
            model[word] = vec;
        }
    }
    
    private void Normalise(float[] vec)
    {
        var len = 0f;
        for (var i = 0; i < Dimensions; i++) len += vec[i] * vec[i];
        len = (float)Math.Sqrt(len);
        for (var i = 0; i < Dimensions; i++) vec[i] /= len;
    }
}