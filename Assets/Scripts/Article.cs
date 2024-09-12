using UnityEngine;

public enum ArticleCategory
{
    Theatre,
    Contacts,
    Social
}

[CreateAssetMenu(menuName = "Evidence", fileName = "SO_Evidence_Name", order = 0)]
public class Article : ScriptableObject
{
    public string Label;
    public string Title;
    public string Description;
    public AudioClip Audio;
    public ArticleCategory Category;
    
    public void CloneFrom(Article newArticle)
    {
        Label = newArticle.Label;
        Title = newArticle.Title;
        Description = newArticle.Description;
        Audio = newArticle.Audio;
        Category = newArticle.Category;
    }
}
