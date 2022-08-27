using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class InputCheck : MonoBehaviour
    {
        public GameObject checkMark;
        public Image downCover;
        public float duration = 1;

        public void CheckNameWords(string name)
        {
            var showButton = name.Length > 0;
            var alpha = showButton ? 0 : 0.8f;
            downCover.raycastTarget = !showButton;
            downCover.DOFade(alpha, duration).SetEase(Ease.InSine);
            checkMark.SetActive(showButton);
        }
    }
}
