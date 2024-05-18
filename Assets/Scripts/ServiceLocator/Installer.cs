using UnityEngine;

namespace ServiceLocator
{
    //аттрибут позводит скрипт скабатывать раньше всех остальных
    [DefaultExecutionOrder(-1)]
    //базовый класс инсталера, в нашем случае инстайлер 1 поэтому смысла в нём нет
    public abstract class Installer : MonoBehaviour { }
}