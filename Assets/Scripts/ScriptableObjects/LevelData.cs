using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    [SerializeField] private string _name;
    [SerializeField] private Vector2 _gridSize;
    [SerializeField] private CardBundleData[] _cardBundleData;

    public Vector2 GridSize => _gridSize;
    public CardBundleData[] CardBundleData => _cardBundleData;
}
