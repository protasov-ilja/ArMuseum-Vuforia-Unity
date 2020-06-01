using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Utils;
using UnityEngine;
using Zenject;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "GlobalInstaller", menuName = "ARMuseum/Zenject/GlobalInstaller", order = 0)]
    public class GlobalInstaller : ScriptableObjectInstaller<GlobalInstaller>
    {
        [SerializeField] private GlobalDataContainer _globalDataContainer = default;

        public override void InstallBindings()
        {
            Container.Bind<GenericSceneManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<GlobalDataContainer>().FromComponentInNewPrefab(_globalDataContainer).AsSingle().NonLazy();
        }
    }
}