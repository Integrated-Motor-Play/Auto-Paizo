using UnityEngine;
using Recognissimo.Core;
using UISystem; // PartialResult, Result
public class RecognitionLogger : MonoBehaviour
{
    private string _lastData = "";
    private bool _played = false;
    public void OnPartialResult(PartialResult partialResult)
    {
        Debug.Log($"<color=yellow>{partialResult.partial}</color>");
        if (!partialResult.partial.Contains("[unk]") && !_lastData.Contains("[unk]") &&
            partialResult.partial == _lastData)
        {
            _played = true;
            AudioManager.Instance.SFX.Play("voice_recognized", 1, 1);
        }
        _lastData = partialResult.partial;
    }
    
    public void OnResult(Result result)
    {
        Debug.Log($"<color=green>{result.text}</color>");
    }
    
    public void OnPause()
    {
        Debug.Log("Pause");
        if(!_played)
            AudioManager.Instance.SFX.Play("voice_recognized", 1, 1);
    }
    
    public void OnContinue()
    {
        Debug.Log("Continue");
        _played = false;
    }
}
