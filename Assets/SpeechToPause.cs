#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToPause : MonoBehaviour
{
    private KeywordRecognizer _keywordRecognizer;
    private readonly Dictionary<string, Action> _actions = new Dictionary<string, Action>();

    private IEnumerator Start()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        _actions.Add("pause", PauseMenu.Instance.Pause);
        _actions.Add("stop", PauseMenu.Instance.Pause);
        
        _keywordRecognizer = new KeywordRecognizer(_actions.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        
        _keywordRecognizer.Start();

        print("Speech Loaded");
        Time.timeScale = 1;
    }
    
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        print("Recognized: " + speech.text);
        _actions[speech.text].Invoke();
    }

    private void OnDestroy()
    {
        if(!enabled) return;
        _keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
        _keywordRecognizer.Stop();
        _keywordRecognizer.Dispose();
    }
}
#endif
