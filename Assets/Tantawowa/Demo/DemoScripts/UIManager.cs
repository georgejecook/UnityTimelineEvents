using System.Collections.Generic;
using UnityEngine;

namespace Tantawowa.Demo.DemoScripts
{
    public class UIManager : MonoBehaviour
    {
        public List<GameObject> UIElements;
        public GameObject Light;

        public void ChangeUI(UIType type)
        {
            for (int i = 0; i <= (int) UIType.Finish; i++)
            {
                UIElements[i].SetActive(i == (int) type);
            }
        }

        public void ToggleLight(bool isOn)
        {
            Light.SetActive(isOn);
        }
    }
}