namespace _260109.utils;

public class QuestData
{
    public string title { get; set; }
    public string description { get; set; }
    public Item requiredItem { get; set; }
    public int requiredItemCount { get; set; }

    public QuestData( string title, string description, Item requiredItem, int requiredItemCount)
    {
        this.title = title;
        this.description = description;
        this.requiredItem = requiredItem;
        this.requiredItemCount = requiredItemCount;
    }
}