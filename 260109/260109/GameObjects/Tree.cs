namespace _260109.GameObjects;

public class Tree : Item, IInteractable
{

    public Tree() => Init();
    
    private void Init()
    {
        Symbol = 'T';
    }

    public override void Use()
    {
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddItem(this);
    }
}