using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    GameObject _player = default;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void LateUpdate()
    {
        if (_player)
        {
            this.transform.forward = _player.transform.position - this.transform.position;
        }
    }

    public abstract void Activate();

    private void OnTriggerEnter(Collider other)
    {
        Activate();
        Destroy(this.gameObject);
    }

}
