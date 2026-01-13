

using System.Runtime.InteropServices.Marshalling;
using _260109.GameObjects;
using _260109.utils;

public class PlayerCharacter : GameObject
{
    public ObservableProperty<int> Health = new ObservableProperty<int>(5);
    public ObservableProperty<int> Mana = new ObservableProperty<int>(5);
    public string _healthGauge;
    public string _manaGauge;
    
    public Tile[,] Field { get; set; }
    public Inventory _inventory;
    private Npc _npc;
    public bool IsActiveControl { get; private set; }

    public PlayerCharacter() => Init();

    public void Init()
    {
        Symbol = 'P';
        IsActiveControl = true;
        Health.AddListener(SetHealthGauge);
        Mana.AddListener(SetManaGauge);
        _healthGauge = "■■■■■";
        _manaGauge = "■■■■■";
        _inventory = new Inventory(this);
    }

    public void Update()
    {
        if (InputManager.GetKey(ConsoleKey.I))
        {
            HandleControl();
        }
        
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            Move(Vector.Up);
            _inventory.SelectUp();
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            Move(Vector.Down);
            _inventory.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.LeftArrow))
        {
            Move(Vector.Left);
        }

        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            Move(Vector.Right);
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _inventory.Select();
        }

        if (InputManager.GetKey(ConsoleKey.T))
        {
            Health.Value--;
        }
        
        if (InputManager.GetKey(ConsoleKey.E))
        {
            CheckNpc();
        }
    }

    public void HandleControl()
    {
        _inventory.IsActive = !_inventory.IsActive;
        IsActiveControl = !_inventory.IsActive;
        Debug.LogWarning($"{_inventory._itemMenu.CurrentIndex}");
    }

    private void Move(Vector direction)
    {
        if (Field == null || !IsActiveControl) return;
        
        Vector nextPos = Position + direction;
        
        if (nextPos.Y < 0 || nextPos.Y >= Field.GetLength(0)
                          || nextPos.X < 0 || nextPos.X >= Field.GetLength(1) )// npc 접근 불가 기능 추가
        {
            return;
        }
        
        GameObject nextTileObject = Field[nextPos.Y, nextPos.X].OnTileObject;
        
        // 1. 맵 바깥은 아닌지?
        // 2. 벽인지?
        
        
        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);
            }
        }

        Field[Position.Y, Position.X].OnTileObject = null;
        Field[nextPos.Y, nextPos.X].OnTileObject = this;
        Position = nextPos;
    }

    public void Render()
    {
        _inventory.Render();
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }

    public void SetHealthGauge(int health)
    {
        switch (health)
        {
            case 5:
                _healthGauge = "■■■■■";
                break;
            case 4:
                _healthGauge = "■■■■□";
                break;
            case 3:
                _healthGauge = "■■■□□";
                break;
            case 2:
                _healthGauge = "■■□□□";
                break;
            case 1:
                _healthGauge = "■□□□□";
                break;
        }
    }

    public void SetManaGauge(int mana)
    {
        switch (mana)
        {
            case 5:
                _manaGauge = "■■■■■";
                break;
            case 4:
                _manaGauge = "■■■■□";
                break;
            case 3:
                _manaGauge = "■■■□□";
                break;
            case 2:
                _manaGauge = "■■□□□";
                break;
            case 1:
                _manaGauge = "■□□□□";
                break;
        }
    }

    public void Heal(int value)
    {
        Health.Value += value;
    }

    public void CheckNpc()
    {
        Vector[] directions =
        {
            new Vector(0, -1), 
            new Vector(0, 1),  
            new Vector(-1, 0), 
            new Vector(1, 0)   
        };

        foreach (Vector d in directions)
        {
            Vector checkPos = Position + d;
                
            // 해당 타일의 오브젝트 가져오기
            GameObject obj = Field[checkPos.Y, checkPos.X].OnTileObject;
                
            if (obj is Npc npc)
            {
                npc.Interact(this);
                break; 
            }
        }
    }
}