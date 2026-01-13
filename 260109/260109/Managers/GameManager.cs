using System;
using _260109.GameObjects;
using _260109.Scenes;
using _260109.utils;

public class GameManager
{
    public static bool IsGameOver { get; set; }
    public const string GameName = "아무튼 RPG";
    private PlayerCharacter _player;
    private Npc _npc;

    public void Run()
    {
        Init();
        
        while (!IsGameOver)
        {
            // 렌더링
            Console.Clear();
            SceneManager.Render();
            // 키입력 받고
            InputManager.GetUserInput();

            if (InputManager.GetKey(ConsoleKey.L))
            {
                SceneManager.Change("Log");
            }

            // 데이터 처리
            SceneManager.Update();
        }
    }

    private void Init()
    {
        IsGameOver = false;
        SceneManager.OnChangeScene += InputManager.ResetKey;
        _player = new PlayerCharacter();
        
        Item questItem = new Tree
        {
            Name = "나무"
        };
        
        QuestData questData = new QuestData(
            title: "나무 가져오기",
            description: "마을에서 나무 하나를 가져다줘.",
            requiredItem: questItem,
            requiredItemCount: 1
        );

        Quest quest = new Quest(questData);
        
        _npc = new Npc
        {
            Name = "NPC",
            Quest = quest
        };
        
        SceneManager.AddScene("Title", new TitleScene());
        SceneManager.AddScene("Story", new StoryScene());
        SceneManager.AddScene("Town", new TownScene(_player,_npc,questItem));
        SceneManager.AddScene("Log", new LogScene());
        SceneManager.AddScene("Quest", new QuestScene(_player));
        
        SceneManager.Change("Title");
        
        Debug.Log("게임 데이터 초기화 완료");
    }
}