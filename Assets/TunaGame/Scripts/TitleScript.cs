using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HighScoreText;
    [SerializeField] TextMeshProUGUI UserText;
    [SerializeField] TMP_InputField FieldText;
    [SerializeField] TMP_InputField column;
    [SerializeField] Scene MainScene;
    [SerializeField] Transform head;
    [SerializeField] AudioClip button;
    Animator _anim;
    new AudioSource audio = new AudioSource();
    bool sLoad = false;

    void Start()
    {
        HighScoreText.GetComponent<TextMeshProUGUI>();
        UserText.GetComponent<TextMeshProUGUI>();
        _anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        HighScoreText.text = "HighScore:$" + GameManager.Instance.HighScore.ToString("N0");
        UserText.text = "User:" + GameManager.Instance.UserName;
        head.Rotate(new Vector3(2,0,0));
        if (_anim) _anim.SetFloat("Running", Mathf.Abs(head.eulerAngles.x-360f));
        if (!audio.isPlaying && sLoad) SceneManager.LoadScene(MainScene.handle);
    }

    public void TextApply()
    {
        FieldText = GameObject.Find(FieldText.name).GetComponent<TMP_InputField>();
        Sound(false);
        if ((FieldText.text != null) && (FieldText.text.Trim().Length != 0))
        {
            GameManager.Instance.UserName = FieldText.text.Length < 20 ? FieldText.text : FieldText.text.Substring(0,20);
            column = GameObject.Find(column.name).GetComponent<TMP_InputField>();
            column.text = "";
        }
    }

    public void OnClick()
    {
        GameManager.Instance.SetZero();
        Sound(true);
    }

    void Sound(bool boolean)
    {
        audio.PlayOneShot(button);
        sLoad = boolean;
    }

    public void Erasure()
    {
        Sound(false);
        GameManager.Instance.Reset();
    }
}
