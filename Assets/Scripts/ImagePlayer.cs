using Managers;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ImagePlayer : MonoBehaviour
{
    public Sprite[] images;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<Image>().sprite = images[(int)GameManager.Current.Game];
    }
}
