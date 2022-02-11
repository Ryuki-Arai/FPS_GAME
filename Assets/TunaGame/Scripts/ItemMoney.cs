using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoney : Items
{
    [SerializeField] int _score;
    public override void Activate()
    {
        GameManager.Instance.Score = _score;
    }
}
