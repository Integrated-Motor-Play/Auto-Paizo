using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace UISystem
{
    public class MouseClickNoClear : MonoBehaviour
    {
        PauseMenu pauseMenu;
        GameObject selectedObject;

        private void Awake()
        {
            pauseMenu = GetComponent<PauseMenu>();
        }

        void LateUpdate()
        {
            if (!pauseMenu.IsPaused) return;
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(selectedObject);
            }
            else
            {
                selectedObject = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
