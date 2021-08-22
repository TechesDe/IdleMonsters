using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance;

    [SerializeField]
    private EventDispatcher _update;

    [SerializeField]
    private EventDispatcher _fixedUpdate;

    public bool isPause = false;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("UpdateManager already created");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PauseToogle(bool value) {
        isPause = value;
    }

    private void Update() {
        if(!isPause)
            _update.Dispatch();
    }
}
