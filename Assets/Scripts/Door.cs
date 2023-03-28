public class Door : Interactable
{
    public override void Interact()
    {
        LevelManager.Instance.LoadInterior();
    }
}
