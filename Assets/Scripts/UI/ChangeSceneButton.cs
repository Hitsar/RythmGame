using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    //по нажатию на кнопку включает сцену
    public class ChangeSceneButton : MonoBehaviour
    {
        public void ChangeScene(string scene) => SceneManager.LoadScene(scene);
    }
}