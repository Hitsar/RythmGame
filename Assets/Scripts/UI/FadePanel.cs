using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    //при старте игры убирает затемнение
    public class FadePanel : MonoBehaviour
    {
        private void Start() => GetComponent<Image>().DOFade(0, 0.6f).OnKill(() => gameObject.SetActive(false));
    }
}