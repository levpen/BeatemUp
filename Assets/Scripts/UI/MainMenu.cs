using UnityEngine;
using UnityEngine.SceneManagement;

namespace Beatemup.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void ChangeScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void QuitApp()
        {
            Application.Quit();
            Debug.Log("App quitted");
        }
    }
}