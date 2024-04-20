using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationLeadIn : MonoBehaviour
{
    [EnumToggleButtons]
    public Direction direction;
    public float distance;
    public float time = 0.3f;
    [EnumPaging]
    public Ease ease = Ease.OutBack;

    public enum Direction
    {
        Horizontal,
        Vertical
    }
    
    private void OnEnable()
    {
        if(distance == 0) return;
        var amount = distance * ((direction == Direction.Horizontal? Screen.width : Screen.height));
        var temp = transform.position;
        var iniPos = transform.position;
        if(direction == Direction.Horizontal)
            temp.x -= amount;
        else
            temp.y -= amount;
        transform.position = temp;
        transform.DOKill();
        transform.DOMove(iniPos, time).SetEase(ease).SetUpdate(true);
        //print("Ini Pos: " + iniPos);
    }
}
