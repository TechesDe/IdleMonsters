using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private EventListener _gameOver;

    private void OnEnable() {
        _gameOver.OnEventHappened += hide;
    }

    private void OnDisable() {
        _gameOver.OnEventHappened -= hide;
    }

    public void hide() {
        gameObject.SetActive(false);
    }
}
