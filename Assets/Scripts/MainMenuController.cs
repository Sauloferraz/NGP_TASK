using System;
using System.Collections;
using Saving;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuController : MonoBehaviour
{
        public Button ContinueButton;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        { 
                _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        private IEnumerator Start()
        {
                yield return new WaitUntil(() => SaveSystem.Instance);
                
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;

                // Disable the continue button if no save file exists
                ContinueButton.interactable = SaveSystem.Instance.HasSaveFile();
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
                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
        }
}