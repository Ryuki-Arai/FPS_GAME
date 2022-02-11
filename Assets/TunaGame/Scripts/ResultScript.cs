using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour
{
    [SerializeField] GameObject mainSystem;
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] Color[] scrColor;
    [SerializeField] Scene title;
    [SerializeField] AudioClip button;
    [SerializeField] GameObject newRecoad;
    GameObject[] item;
    bool rsl;
    new AudioSource audio = new AudioSource();
    bool sLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        mainSystem.SetActive(false);
        result = GameObject.Find(result.name).GetComponent<TextMeshProUGUI>();
        score = GameObject.Find(score.name).GetComponent<TextMeshProUGUI>();
        item = GameObject.FindGameObjectsWithTag("Item");
        rsl = GameObject.FindWithTag("Player").GetComponent<CharacterMove>().OnDead;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        result.text = rsl ? "Faild..." : "Success!";
        result.color = rsl ? scrColor[0] : scrColor[1];
        score.text = "$" + GameManager.Instance.Score.ToString("N0");
        if (GameManager.Instance.Score > GameManager.Instance.HighScore && !rsl) newRecoad.SetActive(true);
        OnDeleteItem();
        if (!audio.isPlaying && sLoad) SceneManager.LoadScene(title.handle);
    }

    public void OnClick()
    {
        if (!rsl) GameManager.Instance.HighScore = 0;
        Sound(true);
    }

    void Sound(bool boolean)
    {
        audio.PlayOneShot(button);
        sLoad = boolean;
    }

    void OnDeleteItem()
    {
        foreach(var go in item)
        {
            Destroy(go);
        }
    }
}
