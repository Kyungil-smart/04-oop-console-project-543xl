using _260109.GameObjects;

namespace _260109.utils;

public class QuestDatabase
{
    public static Item questItem = new Tree
    {
        Name = "나무"
    };

    // 퀘스트 데이터
    public static QuestData WoodQuestData = new QuestData
    (
        title: "나무 가져오기",
        description: "마을에서 나무 하나를 가져다줘.",
        requiredItem: questItem,
        requiredItemCount: 1
    );
}