# Word2Vec implementation in C#

Years ago, I discovered a C# version of Word2Vec (see [an illustrated description of the training process](https://jalammar.github.io/illustrated-word2vec/) for an intro, if you need one!) on a site robosoup.com, which sadly seems to be no more (it redirects to [https://wakefield.ai/](https://wakefield.ai/)) but I'd love for this codeto be able to continue to live on!

This is a gently-massaged version of that code, I hope that I don't end up angering the original author by broaching any copying / sharing restrictions (if I have, please get in touch and let me know!)

To run it, you need to drop a txt file into the solution root. The file should contain sentences, one per line. I recommend the 2023 one million sentences extracted from English news articles that's available here: [https://wortschatz.uni-leipzig.de/en/download/English](https://wortschatz.uni-leipzig.de/en/download/English) (click the 1M link, download the compressed data, then extract the text file "eng_news_2023_1M-sentences.txt" and drop it in the root).

If you want to experiment with a different file, then be sure to change the filename specified on lin 5 of Program.cs -

```
const string corpusTrainingFilePath = "eng_news_2023_1M-sentences.txt";
```

The first time that it runs, it will have to build a model file from the text data. It will write that model to disk, so that subsequent runs will ask you whether you want to build it again from scratch, or whether you want to use the existing model file.

With the model built, it will prompt you to enter words and then show the word in its vocabulary that are closest to it. Using the 1M English news articles sentences, words such as "car", "computer", "helicopter" yield sensible results - but try anything!

The original 2013 paper from Google that presented this concept can be found at [https://arxiv.org/pdf/1301.3781](https://arxiv.org/pdf/1301.3781).
