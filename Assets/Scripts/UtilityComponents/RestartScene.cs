using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] private int sceneNum = 0;

    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneNum);
    }
}
