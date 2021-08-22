using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject _left;

    [SerializeField]
    private GameObject _right;

    [SerializeField]
    private GameObject[] _toActive;

    [SerializeField]
    private EventListener _gameOver;
    [SerializeField]
    private EventListener _restart;

    public float time = 0.2f;

    private void OnEnable() {
        _gameOver.OnEventHappened += Appear;
        _restart.OnEventHappened += Disappear;
    }

    private void OnDisable() {
        _gameOver.OnEventHappened -= Appear;
        _restart.OnEventHappened -= Disappear;
    }

    public void Appear() { 
        StopAllCoroutines();
        StartCoroutine(AppearCoroutine());
    }

    public void Disappear() {
        StopAllCoroutines();
        StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator AppearCoroutine() {
        float timer = 0f;
        _left.TryGetComponent(out RectTransform rectL);
        _right.TryGetComponent(out RectTransform rectR);
        float rectLRotate = rectL.rotation.eulerAngles.y;
        float rectRRotate = rectR.rotation.eulerAngles.y;
        while (timer < time) {
            timer += Time.deltaTime;
            float newYL = (rectLRotate + 0f) * (1 - timer / time);
            float newYR = (rectRRotate + 0f) * (1 - timer / time);
            rectL.rotation = Quaternion.Euler(new Vector3(rectL.rotation.eulerAngles.x, newYL, rectL.rotation.eulerAngles.z));
            rectR.rotation = Quaternion.Euler(new Vector3(rectR.rotation.eulerAngles.x, newYR, rectR.rotation.eulerAngles.z));
            yield return null;
        }
        rectL.rotation = Quaternion.Euler(new Vector3(rectL.rotation.eulerAngles.x, 0f, rectL.rotation.eulerAngles.z));
        rectR.rotation = Quaternion.Euler(new Vector3(rectR.rotation.eulerAngles.x, 0f, rectR.rotation.eulerAngles.z));
        for (int i = 0; i < _toActive.Length; i++)
            _toActive[i].SetActive(true);
    }

    private IEnumerator DisappearCoroutine() {
        float timer = 0f;
        for (int i = 0; i < _toActive.Length; i++)
            _toActive[i].SetActive(false);
        _left.TryGetComponent(out RectTransform rectL);
        _right.TryGetComponent(out RectTransform rectR);
        float rectLRotate = rectL.rotation.eulerAngles.y;
        float rectRRotate = rectR.rotation.eulerAngles.y;
        while (timer < time) {
            timer += Time.deltaTime;
            float newYL = (rectLRotate + 90f) * (timer / time);
            float newYR = (rectRRotate + 90f) * (timer / time);
            rectL.rotation = Quaternion.Euler(new Vector3(rectL.rotation.eulerAngles.x, newYL, rectL.rotation.eulerAngles.z));
            rectR.rotation = Quaternion.Euler(new Vector3(rectR.rotation.eulerAngles.x, newYR, rectR.rotation.eulerAngles.z));
            yield return null;
        }
        rectL.rotation = Quaternion.Euler(new Vector3(rectL.rotation.eulerAngles.x, 90f, rectL.rotation.eulerAngles.z));
        rectR.rotation = Quaternion.Euler(new Vector3(rectR.rotation.eulerAngles.x, 90f, rectR.rotation.eulerAngles.z));
    }
}
