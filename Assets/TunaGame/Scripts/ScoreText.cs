using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI scoteText;
    void Start() { scoteText = GetComponent<TextMeshProUGUI>(); }
    void FixedUpdate() { scoteText.text = "$" + GameManager.Instance.Score.ToString("N0"); }
}
