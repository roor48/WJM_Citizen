using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace A
{
    public class MainMenu : MonoBehaviour
    {
        public Animator menuBackAnim;
        public Animator storyAnim;
        public Animator settingAnim;

        public GameObject checkBGM;
        public GameObject checkSFX;

        private void Start()
        {
            checkBGM.SetActive(true);
            checkSFX.SetActive(true);
        }

        public void BtnStart()
        {
            SceneManager.LoadScene("Play");
        }

        public void BtnExit()
        {
            Application.Quit();
        }

        public void BtnStory()
        {
            menuBackAnim.SetTrigger("Close");
            Invoke(nameof(OpenStory), 1.5f);
        }

        public void BtnSetting()
        {
            menuBackAnim.SetTrigger("Close");
            Invoke(nameof(OpenSetting), 1.5f);
        }

        private void OpenMenuBack()
        {
            menuBackAnim.SetTrigger("Open");
        }
        private void OpenStory()
        {
            storyAnim.SetTrigger("Open");
        }

        private void OpenSetting()
        {
            settingAnim.SetTrigger("Open");
        }

        public void BtnBGM()
        {
            checkBGM.SetActive(!checkBGM.activeInHierarchy);
        }
        public void BtnSFX()
        {
            checkSFX.SetActive(!checkSFX.activeInHierarchy);
        }

        public void BtnBack(int num)
        {
            switch (num)
            {
                case 0: // Story
                    storyAnim.SetTrigger("Close");
                    break;
                case 1: // Setting
                    settingAnim.SetTrigger("Close");
                    break;
            }
            Invoke(nameof(OpenMenuBack), 1.5f);
        }
    }
}
