using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdaterFromSlider : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField]
    private string preText;
    [SerializeField]
    private int initialValue;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = string.Format("{0}{1}", preText, initialValue);
    }
    public void OnSliderValueChanged(float value)
    {
        text.text = string.Format("{0}{1}", preText, (int)value);
    }
}
