using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.UI;

public class Moster : MonoBehaviour {

    [SerializeField]
    private EventListener _updateEvent; //Using to Update or Pause
    //[SerializeField]
    //private EventListener _fixedUpdateEvent; //Using to Fixed Update or Pause
    
    public int hp;
    public int bornHp = 100;

    [SerializeField]
    private Slider _hpSlider;

    private void Awake() {
        if (bornHp <= 0)
            Destroy(gameObject);
        hp = bornHp;
    }

    public void setHP(int value,bool isBorn = false) {
        if (value < 0) {
            Debug.LogError("Value cannot be less than 0");
            return;
        }
        if (isBorn) {
            bornHp = value;
            hp = bornHp;
        } else {
            hp = value;
        }
    }

    private void OnEnable() {
        _updateEvent.OnEventHappened += UpdateBehaviour;
    }

    private void OnDisable() {
        _updateEvent.OnEventHappened -= UpdateBehaviour;
    }


    private void UpdateBehaviour() {
        
    }

    private void OnMouseDown() {
        hp--;

        _hpSlider.value = (float)hp / bornHp;
        Text text=_hpSlider.GetComponentInChildren<Text>();
        text.text = hp + "/" + bornHp;
    }

}
