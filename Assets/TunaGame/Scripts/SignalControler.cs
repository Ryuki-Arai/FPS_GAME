using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalControler : MonoBehaviour
{
    bool sig = false;
    public bool Sig { set { sig = value; } }
    [SerializeField] Renderer RedSig;
    [SerializeField] Renderer GreenSig;
    [SerializeField] Light RedSigL;
    [SerializeField] Light GreenSigL;
    
    // Start is called before the first frame update
    void Start()
    {
        RedSig = GameObject.Find(RedSig.name).GetComponent<Renderer>();
        GreenSig = GameObject.Find(GreenSig.name).GetComponent<Renderer>();
        RedSigL = GameObject.Find(RedSigL.name).GetComponent<Light>();
        GreenSigL = GameObject.Find(GreenSigL.name).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sig)
        {
            RedSig.material.DisableKeyword("_EMISSION");
            RedSigL.enabled = false;
            GreenSig.material.EnableKeyword("_EMISSION");
            GreenSigL.enabled = true;
        }
        else
        {
            RedSig.material.EnableKeyword("_EMISSION");
            RedSigL.enabled = true;
            GreenSig.material.DisableKeyword("_EMISSION");
            GreenSigL.enabled = false;
        }
    }
}
