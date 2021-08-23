using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;

public class Money : MonoBehaviour
{
    [SerializeField]
    private Text _moneyText;

    [SerializeField]
    private ScriptableInt _money;

    [SerializeField]
    private EventListener[] _eventListeners;

    private void OnEnable() {
        for(int i = 0; i < _eventListeners.Length; i++) {
            _eventListeners[i].OnEventHappened += UpdateMoney;
        }
    }

    private void OnDisable() {
        for (int i = 0; i < _eventListeners.Length; i++) {
            _eventListeners[i].OnEventHappened -= UpdateMoney;
        }
    }

    private void UpdateMoney() {
        _moneyText.text = _money.value.ToString();
    }
}
