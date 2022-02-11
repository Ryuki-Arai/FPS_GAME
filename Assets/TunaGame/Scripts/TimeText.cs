using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeText : MonoBehaviour
{
    [SerializeField] GameObject friezeIcon = default;
    [SerializeField] float deadTime = 90f;
    [SerializeField] float coreTime = 5f;
    float time, stoptime;
    bool _isTimer;
    TextMeshProUGUI timeText;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI str;
    [SerializeField] Color[] strColor;
    [SerializeField] GameObject rsltPanel;
    [SerializeField] AudioSource buzzer;
    void Start() 
    {
        time = deadTime;
        timeText = GetComponent<TextMeshProUGUI>(); 
        str = GameObject.Find(str.name).GetComponent<TextMeshProUGUI>();
        buzzer = GetComponent<AudioSource>();
    }
    void Update() 
    {
        if(!_isTimer && time>0f) time -= Time.deltaTime;
        else if(time <= 0f)
        {
            GameSet();
            Buzzer();
            str.text = "時間切れ";
            str.color = strColor[1];
        }
        timeText.text = ((int)time / 60).ToString("D") + ":" + ((int)time % 60).ToString("D2");
        if(_isTimer)
        {
            stoptime += Time.deltaTime;
            str.text = "時間停止:" + (Mathf.Abs((int)stoptime-5)).ToString() + "秒";
            str.color = strColor[0];
            if (stoptime >= coreTime)
            {
                _isTimer = false;
                GameObject.Find("Main System").GetComponent<Announcer>()._isPlay = false;
                str.text = "";
                stoptime = 0f;
            }
        }
    }
    void LateUpdate()
    {
        friezeIcon.SetActive(_isTimer);
        panel.SetActive(_isTimer);
    }
    public void OnStop()
    {
        _isTimer = true;
    }
    bool b = true;
    void Buzzer()
    {
        if (b && !buzzer.isPlaying) 
        { 
            buzzer.Play();
            b = false;
        }
    }

    void GameSet()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMove>().OnDead = true;
    }
}
