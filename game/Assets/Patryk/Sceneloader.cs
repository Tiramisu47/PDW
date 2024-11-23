using UnityEngine.SceneManagement;
using UnityEngine;
public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Exit()
    {
      
        
            Debug.Log("Exiting game..."); // Do debugowania w edytorze
            Application.Quit();
        
    }
}

/* ladowanie scen z loading screenem
public class SceneLoader : MonoBehaviour
{
    public void LoadWithLoadingScreen(string loadingScene, string targetScene)
    {
        StartCoroutine(LoadScenesWithLoading(loadingScene, targetScene));
    }

    private IEnumerator LoadScenesWithLoading(string loadingScene, string targetScene)
    {
        // Za�aduj ekran �adowania
        SceneManager.LoadScene(loadingScene);

        // W tle �aduj g��wn� scen�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");

            // Aktywuj scen�, gdy jest gotowa
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}*/