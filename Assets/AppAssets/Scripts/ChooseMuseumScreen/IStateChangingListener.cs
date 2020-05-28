using AppAssets.Scripts.UI.Enums;

namespace ARMuseum.ChooseMuseumScreen
{
    public interface IStateChangingListener
    {
        void StateChanged(ScreenState newState);
    }
}