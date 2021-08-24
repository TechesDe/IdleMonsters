using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class PauseBonus : Bonus
{
    public float timer = 2f;

    protected override void OnMouseDown() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1000);
        StartCoroutine(Pause());
    }

    private IEnumerator Pause() {
        UpdateManager.Instance.isPause = true;
        GameObject.FindGameObjectWithTag("MainCamera").TryGetComponent(out FrostEffect frost);
        MusicManager.Instance.Frost();
        float time = 0f;
        float halfTimer = timer / 4f;
        while (time < halfTimer) {
            time += Time.deltaTime;
            frost.FrostAmount = Mathf.Lerp(0f, 0.25f, (time) / halfTimer);
            yield return null;
        }
        while (time < timer) {
            time += Time.deltaTime;
            if (timer - time < halfTimer)
                frost.FrostAmount = Mathf.Lerp(0.25f, 0f, (halfTimer - (timer - time)) / halfTimer);
            yield return null;
        }
        frost.FrostAmount = 0f;
        UpdateManager.Instance.isPause = false;
        Destroy(gameObject);
    }
}
