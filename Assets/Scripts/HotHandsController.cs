using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class HotHandsController : MonoBehaviour
{
    public GameObject GameOverPanel, AddPointPanel;
    public Slider sensSlider;
    public TextMeshProUGUI sensText, EMSResultText;
    public Text accText;
    public float EMSTime = 2;
    private float accRange = 0.1f;
    private bool triggered;
    private bool isStop;
    private bool dOn;
    private int listIndex;
    private AudioSource audioSource;
    public AudioClip pre;
    public AudioClip start;
    public AudioClip start0;
    public float preMoveTime = 0.5f;

    public int normalMax = 3;
    private int maxPrecount;

    bool preTrigger;
    int precount;
    float timeRequired;

    private bool gameStarted;

    private bool emsToggleOn;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        accRange = 0.1f / (((float)sensSlider.value + 1) / sensSlider.maxValue);
        sensText.text = ((float)sensSlider.value / sensSlider.maxValue).ToString("f2");
    }

    private void OnEnable()
    {
        maxPrecount = normalMax;
        SetWalkPrecount();
    }

    private void SetWalkPrecount()
    {
        if (ModeManager.currentMode == ModeManager.GameMode.Walk)
        {
            var numbers = new int[3] { 2, 5, 8 };
            var randomIndex = Random.Range(0, 3);
            maxPrecount = numbers[randomIndex];
            print("maxPrecount: " + maxPrecount);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (precount > maxPrecount)
        {
            gameStarted = true;
        }
        if (!ScoreManager.Instance.ScoreChanged) return;
        if (isStop)
        {
            Invoke(nameof(EnableGamePanel), 0);
        }
        if (!gameStarted && !isStop)
        {
            timeRequired += Time.deltaTime;
            if (Mathf.Abs(Input.acceleration.magnitude - 1) >= 0.5f && timeRequired >= 0f)
            {
                if (!preTrigger)
                {
                    timeRequired = -0.5f;
                    preTrigger = true;
                    precount++;
                    audioSource.clip = pre;
                    audioSource.Play();
                    if (precount <= maxPrecount && emsToggleOn)
                        StartCoroutine(PreMove(preMoveTime));
                }
            }
            else
            {
                preTrigger = false;
            }
            return;
        }
        //Mathf.Abs(acc - 1) >= accRange
        float acc = Input.acceleration.magnitude;
        if (Mathf.Abs(acc - 1) >= accRange && !triggered && !isStop)
        {
            if (listIndex < ModeManager.playList.Length)
            {
                audioSource.clip = (listIndex == ModeManager.playList.Length - 1) ? start : start0;
                audioSource.Play();
                CalibrationController.SendBluetoothData(GetChar(ModeManager.playList[listIndex], true));
                Invoke(nameof(EMSOff), EMSTime);
                SetWalkPrecount();
            }
            //print(acc);
            SetEMSResultText();
            accText.text = accRange.ToString();
            triggered = true;
            listIndex += 1;
            gameStarted = false;
            precount = 0;
            ScoreManager.Instance.ScoreChanged = false;
            AddPointPanel.SetActive(true);
            print("Index: " + listIndex);
        }
    }

    public void GameOver()
    {
        print("Stop the Game");
        isStop = true;
    }

    public bool CheckGameOver()
    {
        if (listIndex >= ModeManager.playList.Length)
            return true;
        print(ScoreManager.Instance.EMSPanel.score + 1 + " : " + ModeManager.playList.Length);
        if (ScoreManager.Instance.EMSPanel.score + 1 > ModeManager.playList.Length * 0.5f ||
        ScoreManager.Instance.PlayerPanel.score + 1 > ModeManager.playList.Length * 0.5f)
            return true;
        return false;
    }

    private void EMSOff()
    {
        if (listIndex > 0)
            CalibrationController.SendBluetoothData(GetChar(ModeManager.playList[listIndex - 1], false));
        triggered = false;
        accText.text = "";
    }

    public void SetSensitivity(float sensitivity)
    {
        accRange = 0.3f / ((sensitivity + 1) / sensSlider.maxValue);
        sensText.text = (sensitivity / sensSlider.maxValue).ToString("f2");
    }

    private string GetChar(char num, bool on)
    {
        if (dOn && !on)
        {
            dOn = false;
            return "d";
        }
        if (GameManager.currentMode == GameManager.GameMode.OddsAndEvens && on)
        {
            float chance = Random.Range(0f, 1f);
            print(chance + " in 0.25");
            if (chance < 0.25f)
            {
                dOn = true;
                return "D";
            }
        }
        switch (num)
        {
            //case '0':
            //if (on) { return "A"; } else { return "a"; }
            case '1':
                if (on) { return "A"; } else { return "a"; }
            case '2':
                if (on) { return "B"; } else { return "b"; }
            case '3':
                if (on) { return "C"; } else { return "c"; }
            default:
                return "";
        }
    }

    private IEnumerator PreMove(float time)
    {
        CalibrationController.SendBluetoothData("D");
        yield return new WaitForSeconds(time);
        CalibrationController.SendBluetoothData("d");
    }

    private void EnableGamePanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        isStop = false;
        listIndex = 0;
        ScoreManager.Instance.ScoreChanged = true;
        GameOverPanel.SetActive(false);
        ScoreManager.Instance.ResetScore();
        triggered = false;
    }

    public void EMSPreToggleChange(bool isOn)
    {
        emsToggleOn = isOn;
    }

    private void OnDisable()
    {
        CalibrationController.SendBluetoothData("r");
    }

    private void SetEMSResultText()
    {
        if (GameManager.currentMode == GameManager.GameMode.RockPaperScissors)
        {
            switch (ModeManager.playList[listIndex])
            {
                case '0':
                    EMSResultText.text = "Water";
                    return;
                case '1':
                    EMSResultText.text = "Air";
                    return;
                case '2':
                    EMSResultText.text = "Fire";
                    return;
                default:
                    return;
            }
        }

        if (GameManager.currentMode == GameManager.GameMode.OddsAndEvens)
        {
            if (dOn)
            {
                EMSResultText.text = "0";
                return;
            }
            switch (ModeManager.playList[listIndex])
            {
                case '0':
                    EMSResultText.text = "5";
                    return;
                case '1':
                    EMSResultText.text = "4";
                    return;
                case '2':
                    EMSResultText.text = "3";
                    return;
                default:
                    return;
            }
        }
    }
}
