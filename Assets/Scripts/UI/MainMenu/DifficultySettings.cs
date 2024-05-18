using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    //по нажанию на кнопку меняется сохранёное число обозначающее сложность
    public class DifficultySettings : MonoBehaviour
    {
        [SerializeField] private Button _normal, _hard;

        private void Start()
        {
            _normal.onClick.AddListener(() => Change(false));
            _hard.onClick.AddListener(() => Change(true));

            //если сложность поменяли на сложную это отбразится после перезапуска сцены
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.DIFFICULTY, 0) == 1)
            {
                _normal.interactable = true;
                _hard.interactable = false;
            }
        }

        private void Change(bool isHard) => PlayerPrefs.SetInt(PlayerPrefsKeys.DIFFICULTY, isHard ? 1 : 0);
    }
}