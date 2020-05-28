namespace AppAssets.Scripts.UI
{
    public interface IScreenManager
    {
        bool IsActivated { get; }
        void ActivateScreen();
        void DeactivateScreen();
    }
}