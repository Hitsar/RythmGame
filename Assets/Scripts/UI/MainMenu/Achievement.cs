using UnityEngine;

namespace UI.MainMenu
{
    //показывает что ачивка активированна
    public class Achievement : MonoBehaviour
    {
        [SerializeField] private GameObject _checkMark;
        private void Start()
        {
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.ACHIEVEMENT) == 1) _checkMark.SetActive(true);
        }
    }
}