using System;

public class NewsPublisher
{
    public event EventHandler<NewsArticle> NewNewsPublished;


    protected virtual void OnNewNewsPublished(NewsArticle Article)
    {
        NewNewsPublished?.Invoke(this, Article);
    }
    public void PublishNews(string Title, string Content)
    {
        OnNewNewsPublished(new NewsArticle(Title, Content));
    }
}
