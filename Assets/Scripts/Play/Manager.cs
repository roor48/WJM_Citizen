using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace A
{
    public class Manager : Singleton<Manager>
    {
        public bool isPause = false;
        public bool isLive = true;
        public Menu menu;
        public enum GameStatus
        {
            None,
            Pause,
            Clear,
            GameOver,
            Continue,
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause)
                {
                    PauseGame();
                }
                else
                {
                    ContinueGame();
                }
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            isPause = true;

            menu.SetMenu(Menu.MenuStatus.Pause);
        }
        public void ContinueGame()
        {
            Time.timeScale = 1;
            isPause = false;

            menu.SetMenu(Menu.MenuStatus.Continue);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            isPause = false;

            SceneManager.LoadScene("Game");
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            isPause = false;

            SceneManager.LoadScene("MainMenu");
        }
    }
}
