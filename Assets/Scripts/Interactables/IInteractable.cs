namespace Interactables
{
    public interface IInteractable
    {
        public bool CanInteract { get; set; }
        
        void Interact();
    }
}