using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction.Input;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<Button> flightButtons;
        //[SerializeField] private TouchScreenKeyboard overlayKeyboard;
        [SerializeField] private OVRVirtualKeyboard overlayKeyboard;

        //[SerializeField] public Text inputText;

        public void Start()
        {
            //overlayKeyboard.TextCommitField.keyboardType = (TouchScreenKeyboardType)(-1);
            overlayKeyboard = new OVRVirtualKeyboard();
        }

        public void OpenVirtualKeyboard()
        {
            //flightButtons = new List<Button>();
            Debug.Log("Ready to open keyboard");
            //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            Debug.Log("Where is keyboard?");

            if (overlayKeyboard != null) 
            {
                //Debug.Log("keyboard text?"+ overlayKeyboard.text.ToString());
                var go = new GameObject();
                overlayKeyboard = go.AddComponent<OVRVirtualKeyboard>();
                overlayKeyboard.gameObject.SetActive(true);
                overlayKeyboard.transform.position = new Vector3(100f, 100f, 100f);
                overlayKeyboard.transform.localScale  = new Vector3(10f, 10f, 10f);
                //inputText.text = overlayKeyboard.text; 
            }


        }

    }
}
