using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Events;

#if(UNITY_EDITOR)
[CustomEditor(typeof(EventDispatcher))]
public class EventDispatchInspector : Editor {
    private EventDispatcher _thisInspector;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        _thisInspector = (EventDispatcher)target;

        if (GUILayout.Button("Dispatch")) {
            _thisInspector.Dispatch();
        }

    }
}
#endif