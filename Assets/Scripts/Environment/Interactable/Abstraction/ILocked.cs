namespace Environment.Interactable.Abstraction
{
    public interface ILocked
    {
        Key RequiredKey { get; set; }
        Key Key { get; set; }
    }
}