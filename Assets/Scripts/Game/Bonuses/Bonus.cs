using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private EventListener _update;

    [SerializeField]
    private EventListener _gameOver;

    public float lifeTime;

    private void OnEnable() {
        _update.OnEventHappened += Life;
        _gameOver.OnEventHappened += SelfDestroy;
    }

    private void OnDisable() {
        _update.OnEventHappened -= Life;
        _gameOver.OnEventHappened -= SelfDestroy;
    }

    private void SelfDestroy() {
        Destroy(gameObject);
    }

    protected void Life() {
        if (lifeTime > 0f) {
            lifeTime -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    protected virtual void OnMouseDown() {
        Destroy(gameObject);
    }
}
