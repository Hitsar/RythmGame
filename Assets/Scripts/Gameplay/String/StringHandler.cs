using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.String
{
    //класс для работы струны
    public class StringHandler : MonoBehaviour, IPointerDownHandler
    {
        private Collider2D _collider;
        private bool _isHited, _isLastNice, _isClicked;
        private StringSfx _stringSfx;
        
        private void Start() => _stringSfx = GetComponent<StringSfx>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            _isHited = true;
            _collider = other;
            _isClicked = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isHited = false;
            _collider = null;
            if (!_isClicked) StringEvents.InvokeAway();
        }
        //во время нажатия на струну проверяется дистанция, и в зависимости от неё добавляются очки
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isHited)
            {
                StringEvents.InvokeAway();
                return;
            }
            
            switch (Vector2.Distance(transform.position, _collider.transform.position))
            {
                case < 0.5f and >= 0.2f:
                    //если в прошлый раз было nice click то будет perfect click
                    if (_isLastNice) StringEvents.InvokePerfect();
                    else StringEvents.InvokeNice();
                    _isLastNice = !_isLastNice;

                    _stringSfx.Play();
                    break;
                case < 0.2f and >= 0:
                    StringEvents.InvokePerfect();
                    _stringSfx.Play();
                    break;
                case >= 0.5f:
                    StringEvents.InvokeAway();
                    break;
            }
            
            _isClicked = true;
            _collider.gameObject.SetActive(false);
        }
    }
}