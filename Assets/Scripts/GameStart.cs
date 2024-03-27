using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private LevelBundleData _levelBundleData;
    
    [SerializeField] private GameEnd _gameEnd;
    private GridSpawner _gridSpawner;
    private GoalContainer _goalContainer;
    private int _currentLevel = 0;
    private int _maxLevel;
    private List<CardData> _generatedCards;

    private void Start()
    {
        _gridSpawner = GetComponent<GridSpawner>();
        _goalContainer = GetComponent<GoalContainer>();

        _maxLevel = _levelBundleData.LevelData.Count();

        GenerateLevel(_currentLevel);
    }

    private void GenerateLevel(int levelIndex)
    {
        Vector2 gridSize = _levelBundleData.LevelData[levelIndex].GridSize;
        CardBundleData[] cardBundleData = _levelBundleData.LevelData[levelIndex].CardBundleData;
        CardBundleData randomData = cardBundleData[Random.Range(0, cardBundleData.Count())];

        _generatedCards = _gridSpawner.CreateGrid(gridSize, randomData, levelIndex == 0);
        
        _goalContainer.SetGoal(_generatedCards);
    }

    public void NextLevel()
    {
        _currentLevel++;

        if (_currentLevel >= _maxLevel)
            _gameEnd.StartRestart();
        else
            GenerateLevel(_currentLevel);
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        _currentLevel = 0;

        _gridSpawner.ClearGrid();
        _goalContainer.HideTextContainer();

        yield return new WaitForSeconds(2f);

        _goalContainer.Restart();
        GenerateLevel(_currentLevel);
    }
}
