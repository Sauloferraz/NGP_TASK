using System;
using System.Collections;
using Saving;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuController : MonoBehaviour
{
        [SerializeField] private GameObject continueButton;
        
        private Button _button;
        private CanvasGroup _buttonCanvasGroup;
        private CanvasGroup _menuCanvasGroup;

        private void Awake()
        { 
                _menuCanvasGroup = GetComponent<CanvasGroup>();
        }
        
        private IEnumerator Start()
        { 
                yield return new WaitUntil(() => SaveSystem.Instance);
                
                _menuCanvasGroup.alpha = 1f;
                _menuCanvasGroup.interactable = true;
                _menuCanvasGroup.blocksRaycasts = true;

                if (continueButton)
                {
                        _button = continueButton.GetComponent<Button>();
                        _buttonCanvasGroup = continueButton.GetComponent<CanvasGroup>();
                }
                
                // Disable the continue button if no save file exists
                if (SaveSystem.Instance.HasSaveFile())
                {
                        _button.interactable = true;
                        _buttonCanvasGroup.alpha = 1f;
                }
                else
                {
                        _button.interactable = false;
                        _buttonCanvasGroup.alpha = 0.5f;
                }
        }
        
        public void OnClickNewGame()
        {
                SaveSystem.Instance.DeleteSaveFile();

                HideMenu();
        }
        
        public void OnClickContinue()
        {
                SaveSystem.Instance.Load();

                HideMenu();
        }
        
        private void HideMenu()
        {
                _menuCanvasGroup.alpha = 0f;
                _menuCanvasGroup.interactable = false;
                _menuCanvasGroup.blocksRaycasts = false;
        }
}