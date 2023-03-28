public class Door : Interactable
{
    public override void Interact()
    {
        PlayerManager.Instance.SetInteractPopupVisibility(false);
        LevelManager.Instance.LoadInterior();
    }
}
