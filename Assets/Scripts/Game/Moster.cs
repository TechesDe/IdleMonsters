using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class Moster : MonoBehaviour {

    [SerializeField]
    private EventListener _updateEvent; //Using to Update or Pause
    //[SerializeField]
    //private EventListener _fixedUpdateEvent; //Using to Fixed Update or Pause
    public float time;

    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        _updateEvent.OnEventHappened += UpdateBehaviour;
    }

    private void OnDisable() {
        _updateEvent.OnEventHappened -= UpdateBehaviour;
    }


    private void UpdateBehaviour() {
        time = Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {

    }
}
