using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Audio;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;

    private void Start() {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(delegate { restart(); });
    }

    private void restart() {
        MusicManager.Instance.Restart();
    }
}
