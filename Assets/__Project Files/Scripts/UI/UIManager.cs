
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Nasser.io
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameState state;
        [Header("Ui objects")]
        [SerializeField] GameObject WelcomPanel;
        [SerializeField] GameObject deathPanel;
        [Space]
        [SerializeField] TMP_Text textWeapons;
        [SerializeField] TMP_Text enemyKilled;
        [SerializeField] GameObject damageEffet;
        [Space]
        [SerializeField] Button startButton;
        [SerializeField] Button restartButton;
        [SerializeField] TMP_Text countDown;

        [SerializeField] GameObject androidUI;

        private Vector3 localScale;
        private int enemyKilledInt = 0;

        WaitForSeconds Wait1Sec = new WaitForSeconds(1);
        WaitForSeconds Wait5Sec = new WaitForSeconds(.5f);
        private void Awake()
        {
            state.currentState = GameState.State.Start;
        }
        void Start()
        {
            localScale = startButton.transform.localScale;
            startButton.transform.DOScale(localScale * 1.25f, 1.25f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
            restartButton.transform.DOScale(localScale * 1.25f, 1.25f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
            WelcomPanel.SetActive(true);
            androidUI.SetActive(false);
            deathPanel.SetActive(false);
            damageEffet.SetActive(false);
#if UNITY_ANDROID
            androidUI.SetActive(true);
            textWeapons.gameObject.SetActive(false);    
#endif
        }



        public void StartButtonClick()
        {
            StartCoroutine(Countdown(3));
        }
        private void StartGame()
        {
            countDown.gameObject.SetActive(false);
            state.currentState = GameState.State.Play;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LostGameGame()
        {
            state.currentState = GameState.State.End;
        }
        public void AddEnemy()
        {
            enemyKilledInt++;
            enemyKilled.text = $"Enemy Killed: {enemyKilledInt}";
        }

        public void PlayerDamage()
        {
            damageEffet.SetActive(true);
            StartCoroutine(Wait5SecFunc());
        }


        IEnumerator Countdown(int seconds)
        {
            int count = seconds;

            while (count > 0)
            {
                countDown.text = $"{count}";
                yield return Wait1Sec;
                count--;
            }
            StartGame();
        }

        IEnumerator Wait5SecFunc()
        {
            yield return Wait5Sec;
            damageEffet.SetActive(false);
        }
    }
}
