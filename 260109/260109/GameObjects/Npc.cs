using _260109.Managers;
using _260109.Scenes;
using _260109.utils;

namespace _260109.GameObjects;

public class Npc : GameObject, IInteractable
{
    public string Name { get; set; }
    public Quest Quest {  get; set; }
    public Npc() => Init();
    
    public void Init()
    {
        Symbol = 'N';
    }

    public void Interact(PlayerCharacter player)
    {
        QuestManager.FromNpc(this);
    }
}