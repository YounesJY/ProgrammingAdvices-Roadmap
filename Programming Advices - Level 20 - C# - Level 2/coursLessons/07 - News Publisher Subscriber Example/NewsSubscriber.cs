using System;

public class NewsSubscriber
{
    public string Name { get; }


    public NewsSubscriber(string name)
    {
        Name = name;
    }


    public void Subscribe(NewsPublisher publisher)
    {
        publisher.NewNewsPublished += HandleNewNews;
    }
    public void UnSubscribe(NewsPublisher publisher)
    {
        publisher.NewNewsPublished -= HandleNewNews;
    }

    public void HandleNewNews(object sender, NewsArticle article)
    {
        Console.WriteLine($"{Name} received a new news article:");
        Console.WriteLine($"Title  : {article.Title}.");
        Console.WriteLine($"Content: {article.Content}.");
        Console.WriteLine();
    }
}
