using DG.Tweening;
using UnityEngine;

namespace UI
{
    //класс для анимации всплывающего окна (выбора врага, найстроек и тп)
    public class PopupAnimator : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.7f;
        [SerializeField] private Transform _startPosition;
        
        private void OnEnable() => transform.DOLocalMove(Vector3.zero, _speed).SetEase(Ease.OutCubic);
        
        public void Hide() => transform.DOLocalMove(_startPosition.localPosition, _speed).SetEase(Ease.InCubic).OnKill(() => gameObject.SetActive(false));
    }
}