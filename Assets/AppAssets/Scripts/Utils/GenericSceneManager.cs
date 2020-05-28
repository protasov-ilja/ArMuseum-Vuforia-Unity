using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARMuseum.Utils
{
    public class GenericSceneManager : MonoBehaviour
    {
        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }

        IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);
            
            while (!loadAsync.isDone)
            {
                yield return null;
            }
            
            Debug.Log($"Loaded scene { sceneName }");
        }
    }
}