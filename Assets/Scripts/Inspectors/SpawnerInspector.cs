using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Events;

#if(UNITY_EDITOR)
[CustomEditor(typeof(Spawner))]
public class SpawnerInspector : Editor {
    private Spawner _thisInspector;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        _thisInspector = (Spawner)target;

        if (GUILayout.Button("Spawn")) {
            _thisInspector.Spawn();
        }
    }
}
#endif