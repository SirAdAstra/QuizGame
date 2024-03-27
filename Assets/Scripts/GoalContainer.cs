using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _goalText;

    private string _identifier;
    private List<string> _goals;
    
    private void Start()
    {
        Restart();
    }

    public void SetGoal(List<CardData> generatedCards)
    {
        CardData cardData = generatedCards[Random.Range(0, generatedCards.Count)];
        
        while (TrySetGoal(cardData.Identifier) != true)
            cardData = generatedCards[Random.Range(0, generatedCards.Count)];
    }

    private bool TrySetGoal(string target)
    {
        if (_goals.Contains(target))
            return false;
        else
        {
            _identifier = target;
            _goals.Add(target);
            _goalText.text = $"Find {target}";
            return true;
        }
    }

    public bool CheckAnswer(string target)
    {
        if (_identifier == target)
            return true;
        else
            return false;
    }

    public void Restart()
    {
        _goalText.transform.parent.GetComponent<FadeHandler>().FadeIn();
        _goals = new List<string>();
    }

    public void HideTextContainer()
    {
        _goalText.transform.parent.GetComponent<CanvasGroup>().alpha = 0;
    }
}
