namespace _260109.utils;

public enum QuestState
{
    NotAccepted,
    Accepted,
    Completed
}

public class Quest
{
    // 퀘스트 정보
    public QuestData Data;

    // 퀘스트 상태
    public QuestState State;
    
    public Quest(QuestData data)
    {
        Data = data;
        State = QuestState.NotAccepted;
        // 퀘스트 생성시에 수락 안된 상태로 초기화?
    }

    // 퀘스트 수락
    public void Accept()
    {
        if (State != QuestState.NotAccepted) return;

        State = QuestState.Accepted;
    }
    
    // 퀘스트 완료 가능한지 확인
    public bool CanComplete(Inventory inventory)
    {
        if (State != QuestState.Accepted) return false;

        // 인벤토리에 해당 아이템이 있는지만 확인
        return inventory.Contain(Data.requiredItem);
    }

    // 퀘스트 완료
    public void Complete(Inventory inventory)
    {
        if (!CanComplete(inventory)) return;

        // 아이템 제거
        inventory.Remove(Data.requiredItem);

        // 상태 변경
        State = QuestState.Completed;

        // 보상
        inventory.Add(new Potion { Name = "rewardPotion" });
    }
}