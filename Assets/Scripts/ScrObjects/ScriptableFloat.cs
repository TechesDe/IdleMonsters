using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Remeber Float", menuName = "Remeber Float")]
public class ScriptableFloat : ScriptableObject
{
    public float value;

    public void set(float v) {
        value = v;
    }
}
