using UnityEngine;

namespace General
{
    public class InputCheck : MonoBehaviour
    {
        public GameObject beginButton, checkMark;

        public void CheckNameWords(string name)
        {
            beginButton.SetActive(name.Length > 0);
            checkMark.SetActive(name.Length > 0);
        }
    }
}
