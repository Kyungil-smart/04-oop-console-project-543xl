using System.Reflection.Metadata.Ecma335;
using _260109.GameObjects;
using _260109.Managers;
using _260109.utils;

namespace _260109.Scenes;

public class QuestScene : Scene
{
    private PlayerCharacter _player;
    private Npc _npc;
    private Quest _quest;
    
    public MenuList _menu { get; set; }

    public QuestScene(PlayerCharacter player)
    {
        _player = player;
    }
    public override void Enter()
    {
        // npc 정보를 바탕으로 해당 npc의 퀘스트를 가져오고 싶음
        // npc 정보를 몰라도 해당 npc의 퀘스트를 가져오게 변경
        // questmanager에서 처리한 후 해당 퀘스트만 전달
        _quest = QuestManager._currentQuest;
        
        
        _menu = new MenuList();
        _menu.Clear();

        _menu.Add("수락", Accept);

        if (_quest.State == QuestState.Accepted)
        {
            _menu.Remove();
            _menu.Add("완료",Complete);
        }
        _menu.Add("거절", Refuse);
        _menu.Add("돌아가기", Quit);
        
    }
    
    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _menu.SelectUp();
        } 
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _menu.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _menu.Select();
        }
        
    }

    public override void Render()
    {
        // 퀘스트 내용 출력
        PrintQuest(_quest);

        // 수락/포기 버튼 존재
        _menu.Render(8, 5);
    }

    public override void Exit()
    {
        // 퀘스트 신 종료시에 플레이어 위치가 이어서 작동되도록
    }

    public void PrintQuest(Quest quest)
    {
        Console.Clear();

        // 퀘스트 내용
        Console.WriteLine($"[퀘스트] {quest.Data.title}");
        Console.WriteLine();
        Console.WriteLine(quest.Data.description);
        Console.WriteLine();

        // 상태 출력
        Console.WriteLine($"상태: {quest.State}");
        Console.WriteLine();
        
        Console.WriteLine("목표:");
        Console.WriteLine($"- {quest.Data.requiredItem.Name} {quest.Data.requiredItemCount}개");
        Console.WriteLine();
    }

    public void Refuse()
    {
        if (_quest.State == QuestState.Accepted) _quest.State = QuestState.NotAccepted;
        
    }
    public void Quit()
    {
        SceneManager.ChangePrevScene();
        // 퀘스트신 종료할때 플레이어가 기존에 있던 위치에서 출력되게 추가
    }

    public void Accept()
    {
        _quest.Accept();
    }

    public void Complete()
    {
        _quest.Complete(_player._inventory);
    }
}