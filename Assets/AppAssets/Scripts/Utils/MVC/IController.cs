namespace ARMuseum.Utils.MVC
{
    public interface IController
    {
        IController GetController();

        void SetModel(IModel model);
    }
}