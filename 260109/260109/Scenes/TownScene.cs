

using _260109.GameObjects;
using _260109.Managers;

public class TownScene : Scene
{
    private Tile[,] _field = new Tile[10, 20];
    private PlayerCharacter _player;
    private Npc _npc;
    private Item _questItem;
    public Vector _nextStartPosition;
    
    public Vector setPlayerPosition = new Vector(4, 2);
    
    public TownScene(PlayerCharacter player,Npc npc,Item item) => Init(player, npc, item);

    public void Init(PlayerCharacter player, Npc npc, Item item)
    {
        _player = player;
        _npc = npc;
        _questItem = item;
        
        for (int y = 0; y < _field.GetLength(0); y++)
        {
            for (int x = 0; x < _field.GetLength(1); x++)
            {
                Vector pos = new Vector(x, y);
                _field[y, x] = new Tile(pos);
            }
        }
        
        _field[3, 5].OnTileObject = new Potion() {Name = "Potion1"};
        _field[2, 15].OnTileObject = new Potion() {Name = "Potion2"};
        _field[7, 3].OnTileObject = new Potion() {Name = "Potion3"};
        _field[9, 19].OnTileObject = new Potion() {Name = "Potion4"};
        _field[5, 5].OnTileObject = _npc;
        _field[9, 10].OnTileObject = _questItem;
    }

    public override void Enter()
    {
        _player.Field = _field;
        if (_nextStartPosition.Equals(new Vector(-1,-1))) _player.Position = setPlayerPosition;
        else _player.Position = _nextStartPosition;
        _field[_player.Position.Y, _player.Position.X].OnTileObject = _player;
        
        Debug.Log("타운 씬 진입");
    }

    public override void Update()
    {
        _player.Update();
    }

    public override void Render()
    {
        PrintUI();
        PrintField();
        _player.Render();
    }

    public override void Exit()
    {
        _nextStartPosition = _player.Position;
    }
    
    private void PrintUI()
    {
        // 화면 맨 위 고정
        Console.SetCursorPosition(0, 0);
        _player._healthGauge.Print(ConsoleColor.Red);
    
        Console.SetCursorPosition(0, 1);
        _player._manaGauge.Print(ConsoleColor.Blue);
        
        // UI와 맵 사이 한 줄 띄우기
        Console.SetCursorPosition(0, 3);
    }

    private void PrintField()
    {
        for (int y = 0; y < _field.GetLength(0); y++)
        {
            for (int x = 0; x < _field.GetLength(1); x++)
            {
                _field[y, x].Print();
            }
            Console.WriteLine();
        }
    }
}