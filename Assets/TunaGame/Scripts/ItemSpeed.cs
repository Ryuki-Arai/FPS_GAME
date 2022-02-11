using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : Items
{
    CharacterMove _human;
    public override void Activate()
    {
        _human = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMove>();
        _human.OnDash();
    }
}
