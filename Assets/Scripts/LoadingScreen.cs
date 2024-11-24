using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Sprite[] loadingProgress;
    [SerializeField] private Image loadingImage;
    public void LoadScene(int ind)
    {
        loadingScreen.SetActive(true);
        
        StartCoroutine(LoadSceneAsync(ind));
    }

    IEnumerator LoadSceneAsync(int ind)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ind);

        while (!asyncLoad.isDone)
        {
            loadingImage.sprite = asyncLoad.progress > 0f ? loadingProgress[0] :
                asyncLoad.progress > 0.5 ? loadingProgress[1] : loadingProgress[2];

            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
