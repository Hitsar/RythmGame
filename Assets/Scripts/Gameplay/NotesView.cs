using System.Linq;
using DG.Tweening;
using ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    //отображает ноты
    public class NotesView : MonoBehaviour, IService
    {
        [SerializeField] private Image[] _images;
        
        public void Display(MelodyCreator.NoteData data)
        {
            //берём выключеный image, включаем и меняем спрайт
            Image image = _images.FirstOrDefault(x => x.gameObject.activeSelf == false);
            image.gameObject.SetActive(true);
            image.sprite = data.Sprite;
            
            image.DOFade(0, 1.7f).SetDelay(1.3f).OnKill(() =>
            {
                image.gameObject.SetActive(false);
                image.color = Color.white;
            });
        }
    }
}