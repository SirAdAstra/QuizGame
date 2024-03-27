using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private string _identifier;
    [SerializeField] private SpriteRenderer _cellSprite;
    [SerializeField] private ParticleSystem _particleSystem;

    private GoalContainer _goalContainer;
    private Tween _tween;

    private void Awake()
    {
        _goalContainer = transform.parent.GetComponent<GoalContainer>();
    }

    public void SetData(CardData cardData)
    {
        _identifier = cardData.Identifier;
        _cellSprite.sprite = cardData.Sprite;

        if (cardData.NeedRotate)
            _cellSprite.transform.eulerAngles = new Vector3(0, 0, -90);
    }

    private IEnumerator RightAnswer()
    {
        transform.parent.GetComponent<GridSpawner>().DisableGridButtons();

        _particleSystem.Play();

        _tween?.Kill();
        _cellSprite.transform.DOScale(0.1f, 0.3f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.3f);

        _cellSprite.transform.DOScale(0.23f, 0.7f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.7f);

        _cellSprite.transform.DOScale(0.18f, 0.5f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.5f);

        _tween?.Kill();
        transform.parent.GetComponent<GameStart>().NextLevel();
    }

    private IEnumerator WrongAnswer()
    {
        _tween?.Kill();
        _cellSprite.transform.DOLocalMoveX(-0.02f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        _cellSprite.transform.DOLocalMoveX(0.02f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        _cellSprite.transform.DOLocalMoveX(-0.08f, 0.3f);

        yield return new WaitForSeconds(0.3f);

        _cellSprite.transform.DOLocalMoveX(0.08f, 0.3f);

        yield return new WaitForSeconds(0.3f);

        _cellSprite.transform.DOLocalMoveX(-0.15f, 0.4f);

        yield return new WaitForSeconds(0.4f);

        _cellSprite.transform.DOLocalMoveX(0.15f, 0.4f);

        yield return new WaitForSeconds(0.4f);

        _cellSprite.transform.DOLocalMoveX(0, 0.4f);
    }

    private void OnMouseDown()
    {
        if (_goalContainer.CheckAnswer(_identifier))
            StartCoroutine(RightAnswer());
        else
            StartCoroutine(WrongAnswer());
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}
