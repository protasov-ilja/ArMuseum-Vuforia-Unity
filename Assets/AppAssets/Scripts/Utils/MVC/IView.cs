namespace ARMuseum.Utils.MVC
{
    public interface IView
    {
        IView GetView();

        void SetController(IController controller);

        void SetModel(IModel model);
    }
}