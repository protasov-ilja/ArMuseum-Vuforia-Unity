using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Utils;
using UnityEngine;
using Zenject;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "GlobalInstaller", menuName = "ARMuseum/Zenject/GlobalInstaller", order = 0)]
    public class GlobalInstaller : ScriptableObjectInstaller<GlobalInstaller>
    {
        [SerializeField] private GlobalDataContainer _globalDataContainer;

        public override void InstallBindings()
        {
            Container.Bind<GenericSceneManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<GlobalDataContainer>().FromComponentInNewPrefab(_globalDataContainer).AsSingle().NonLazy();
            
            //Container.Bind<GameManager>().FromComponentInNewPrefab(_gameManager).AsSingle().NonLazy();
            //Container.Bind<FacebookSDKController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            //Container.Bind<ISaveSystem>().To<PrefsSaver>().AsSingle();
            // Container.Bind<GlobalData>().FromNew().AsSingle().NonLazy();
            //Container.Bind<ColorsModeSaveSystem>().FromNew().AsSingle();
            //Container.Bind<PicturesModeSaveSystem>().FromNew().AsSingle();
            //Container.Bind<GlobalDataSO>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}