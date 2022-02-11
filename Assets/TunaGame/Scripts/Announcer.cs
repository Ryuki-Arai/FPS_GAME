using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Announcer : MonoBehaviour
{
    bool starter;
    [SerializeField] SignalControler signal;
    [SerializeField] AudioSource daruma;
    [SerializeField] AudioMixer mixer;
    [SerializeField] TextMeshProUGUI str;
    [SerializeField] Color[] strColor;
    [SerializeField] Animator _anm;

    public bool Mode { get => starter; }

    CharacterMove player = default;
    GameObject[] human = default;

    float time = -5.5f;
    bool back;

    // Start is called before the first frame update
    void Start()
    {
        daruma = GetComponent<AudioSource>();
        str = GameObject.Find(str.name).GetComponent<TextMeshProUGUI>();
        signal = GameObject.Find(signal.name).GetComponent<SignalControler>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMove>();
        human = GameObject.FindGameObjectsWithTag("Human");
        _anm = GameObject.Find(_anm.name).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        starter = time>3;//Random.Range(0,1000) == Random.Range(0, 1000);//
        OnPlayOrPause();
        if (starter && !daruma.isPlaying)
        {
            time = 0;
            var pitch = Random.Range(0.5f, 2.0f);
            daruma.pitch = pitch;
            mixer.SetFloat("MixPitch", 1f / pitch);
            daruma.Play();
        }
        if (daruma.isPlaying || _isPlay) 
        {
            back = true;
            signal.Sig = true;
            str.text = "進め!!!";
            str.color = strColor[0];
        }
        else
        {
            back = false;
            signal.Sig = false;
            time += Time.deltaTime;
            starter = false;
            str.text = "止まれ!!!";
            str.color = strColor[1];
            Judge();
        }
    }

    void FixedUpdate()
    {
        _anm.SetBool("Back", back);
    }

    public bool _isPlay;
    void OnPlayOrPause()
    {
        if (_isPlay)
        {
            if (daruma.isPlaying)
            {
                daruma.Pause();
            }
            else if (daruma.time == 0)
            {
                daruma.time = 0;
                daruma.Play();
            }
            //else daruma.UnPause();
        } 
        else daruma.UnPause();
    }

    void Judge()
    {
        var pMgn = player.Magnitude;
        if(pMgn != 0) player.OnDead = true;
        //foreach (var go in human)
        //{
        //    var mgn = go.GetComponent<CharacterMove>().Magnitude;
        //    if (mgn != 0) go.GetComponent<CharacterMove>().OnDead = true;
        //}
    }
}
