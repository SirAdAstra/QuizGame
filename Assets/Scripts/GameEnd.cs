using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private FadeHandler _restartPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private FadeHandler _loadingPanel;
    [SerializeField] private GameStart _gameStart;

    public void StartRestart()
    {
        _restartPanel.FadeIn();
        _restartButton.gameObject.SetActive(true);
        _restartButton.onClick.AddListener(StartRestartCoroutine);
    }

    private void StartRestartCoroutine()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        _restartPanel.FadeOut();
        _restartButton.gameObject.SetActive(false);

        _loadingPanel.FadeIn();

        yield return new WaitForSeconds(2);

        _gameStart.Restart();
        _loadingPanel.FadeOut();
    }
}
