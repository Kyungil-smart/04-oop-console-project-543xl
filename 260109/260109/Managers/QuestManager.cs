using _260109.GameObjects;
using _260109.utils;

namespace _260109.Managers;

public static class QuestManager
{
    public static Quest _currentQuest{get; set;}
    
    public static void FromNpc(Npc npc)
    {
        _currentQuest = npc.Quest;
        // npc정보를 바탕으로 퀘스트 정보 발견
        // 해당 퀘스트를 씬 전환시에 넘겨줌
        // 퀘스트 씬에서는 npc가 누군지 몰라도 해당 퀘스트를 출력할수 있음.
        SceneManager.Change("Quest");
    }
}