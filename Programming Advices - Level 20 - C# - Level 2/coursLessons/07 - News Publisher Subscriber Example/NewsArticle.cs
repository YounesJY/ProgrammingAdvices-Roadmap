using System;

/*
    This class doesn't inherit from EventArgs
    Its' not required, but it's the convention.
    Breaking it makes your code less familiar to other developers.
*/
public class NewsArticle
{
    public string Title { get; }
    public string Content { get; }


    public NewsArticle(string Title, string Content)
    {
        this.Title = Title;
        this.Content = Content;
    }
}
