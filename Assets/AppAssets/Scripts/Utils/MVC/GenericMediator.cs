using UnityEngine;

namespace ARMuseum.Utils.MVC
{
    public abstract class GenericMediator<TModel, TView, TController> : MonoBehaviour
        where TModel : IModel
        where TView : IView
        where TController : IController
    {
        [SerializeField] protected TController _controller;
        [SerializeField] protected TView _view;
        
        protected TModel _model;

        protected void Awake()
        {
            Register();
            Mediate(_model, _view, _controller);
        }

        private void Mediate(IModel model, IView view, IController controller)
        {
            view.SetModel(model);
            view.SetController(controller);
            controller.SetModel(model);
        }

        public virtual void Register() { }
    }
}