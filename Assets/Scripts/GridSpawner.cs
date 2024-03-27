using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [Header("Cell Prefab")]
    [SerializeField] private GameObject _cell;

    [SerializeField] private float _offset;

    private int _width;
    private int _height;
    private Vector2 _cellSize = new Vector2();

    private void Awake()
    {
        _cellSize = _cell.GetComponent<SpriteRenderer>().size;
    }

    public List<CardData> CreateGrid(Vector2 gridSize, CardBundleData cardBundleData, bool doAnim)
    {
        ClearGrid();

        Vector2 cellPosition = new Vector2(0, 0);
        Cell newCell;

        List<CardData> cardDatas = cardBundleData.CardData.ToList();
        List<CardData> _generatedCards = new List<CardData>();

        _width = (int)gridSize.x;
        _height = (int)gridSize.y;

        for (int y = 0; y < _height; y++)
        {
            cellPosition.y = _cellSize.y / 2 + y + _offset * y;
            
            for (int x = 0; x < _width; x++)
            {
                CardData randomData = GetRandomCardData(cardDatas);
                cardDatas.Remove(randomData);
                _generatedCards.Add(randomData);

                cellPosition.x = _cellSize.x / 2 + x + _offset * x;
                newCell = Instantiate(_cell, cellPosition, Quaternion.identity, transform).GetComponent<Cell>();
                
                if (doAnim)
                {
                    newCell.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    newCell.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
                }
                
                newCell.SetData(randomData);
            }
        }

        PlaceGridInCenter();

        return _generatedCards;
    }

    public void DisableGridButtons()
    {
        foreach (Transform child in transform)
            child.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        
        transform.position = Vector3.zero;
    }

    private void PlaceGridInCenter()
    {
        Vector2 gridCenter = new Vector2(
            (_cellSize.x * _width + _offset * (_width - 1)) / 2,
            (_cellSize.y * _height + _offset * (_height - 1)) / 2);
        
        transform.position = gridCenter * -1;
    }

    private CardData GetRandomCardData(List<CardData> fromList)
    {
        CardData randomData = fromList[Random.Range(0, fromList.Count)];
        return randomData;
    }


}
