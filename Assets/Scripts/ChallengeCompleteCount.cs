using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ChallengeCompleteCount : MonoBehaviour
{
    public GameObject ChallengePanel;
    public Transform ChallengeParent;
    public TextMeshProUGUI CountText, PointText;

    private void Awake()
    {
        ChallengePanel.SetActive(true);
    }

    private void OnEnable()
    {
        DataRecord.GenerateNewFile("Challenge", "csv", "Index,");
        int count = ChallengeParent.childCount;
        int completedCount = 0;
        int completedPoint = 0;
        foreach (Transform challenge in ChallengeParent)
        {
            AchievementSave achievement = challenge.GetComponent<AchievementSave>();
            if (achievement.toggle.isOn)
            {
                completedCount++;
                completedPoint += achievement.Points;
                DataRecord.AppendFile("Challenge", "csv", achievement.Index.ToString() + ",");
            }
        }
        CountText.text = completedCount.ToString() + "/" + count.ToString();
        PointText.text = "Points: " + completedPoint.ToString();
        ChallengePanel.SetActive(false);
        DataRecord.AppendFile("Challenge", "csv", "\nTotal Counts," + completedCount.ToString());
        DataRecord.AppendFile("Challenge", "csv", "\nTotal Points," + completedPoint.ToString());
    }
}
