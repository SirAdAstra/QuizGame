using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField]
    private string _identifier;

    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private bool _needRotate = false;

    public string Identifier => _identifier;
    public Sprite Sprite => _sprite;
    public bool NeedRotate => _needRotate;
}
