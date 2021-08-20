using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class UpdateManager : MonoBehaviour
{
    [SerializeField]
    private EventDispatcher _update;

    [SerializeField]
    private EventDispatcher _fixedUpdate;

    public bool isPause = false;

    private void Update() {
        if(!isPause)
            _update.Dispatch();
    }
}
