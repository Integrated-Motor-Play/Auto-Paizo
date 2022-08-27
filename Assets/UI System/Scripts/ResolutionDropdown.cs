using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace UISystem
{
    public class ResolutionDropdown : MonoBehaviour
    {
        public TMP_Dropdown dropdown;

        private Dictionary<int, int> options = new Dictionary<int, int>();
        IEnumerator Start()
        {
            // Wait for the localization system to initialize
            yield return Screen.resolutions;

            // Generate list of available Locales
            dropdown.ClearOptions();
            var optionsStr = new List<string>();
            int selected = 0;
            for (int i = 0; i < Screen.resolutions.Length; ++i)
            {
                string option = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height;
                if (!optionsStr.Contains(option) && Screen.resolutions[i].width / 16 == Screen.resolutions[i].height / 9)
                {
                    optionsStr.Add(option);
                    options.Add(Screen.resolutions[i].width, Screen.resolutions[i].height);
                }

                if (Screen.resolutions[i].width == Screen.width && Screen.resolutions[i].height == Screen.height)
                {
                    selected = i;
                }
            }
            dropdown.AddOptions(optionsStr);
            dropdown.RefreshShownValue();
            dropdown.value = selected;
            dropdown.onValueChanged.AddListener(ResolutionSelected);
        }

        void ResolutionSelected(int index)
        {
            Screen.SetResolution(options.Keys.ToList()[index], options.Values.ToList()[index], Screen.fullScreen);
            print("Set Resolution to:" + options.Keys.ToList()[index] + " x " + options.Values.ToList()[index]);
        }
    }
}