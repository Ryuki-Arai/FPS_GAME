using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStop : Items
{
    public override void Activate()
    {
        TimeText timer = GameObject.Find("Time").GetComponent<TimeText>();
        Announcer sound = GameObject.Find("Main System").GetComponent<Announcer>();
        timer.OnStop();
        sound._isPlay = true;
    }
}
