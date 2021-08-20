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

    [SerializeField]
    private EventListener _gameOver;

    [SerializeField]
    private ScriptableInt monsterCount;

    public Transform spawner;

    public int hp;
    public int bornHp = 100;
    public float speed = 1f;

    public Vector3 target; //Place where object going

    public float fault = 1; //delta distance to change target

    public Slider hpSlider;

    private void Awake() {
        if (bornHp <= 0)
            Destroy(gameObject);
        hp = bornHp;
        monsterCount.value++;
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
        randomTarget();
    }

    private void OnEnable() {
        _updateEvent.OnEventHappened += UpdateBehaviour;
        _gameOver.OnEventHappened += SelfDestroy;
    }

    private void OnDisable() {
        _updateEvent.OnEventHappened -= UpdateBehaviour;
        _gameOver.OnEventHappened -= SelfDestroy;
    }

    private void SelfDestroy() {
        Destroy(gameObject);
    }

    private void UpdateBehaviour() {
        #region Move
        float distance = (transform.position.x - target.x) * (transform.position.x - target.x) + (transform.position.y - target.y) * (transform.position.y - target.y);
        if (distance < fault) {
            randomTarget();
        } else {
            float x = transform.position.x + Time.deltaTime * speed * ((target.x - transform.position.x) / distance);//TODO: moving without accseleration
            float y = transform.position.y + Time.deltaTime * speed * ((target.y - transform.position.y) / distance);
            transform.position = new Vector3(x, y, transform.position.z);
        }
        #endregion Move
    }

    private void OnMouseDown() {
        if (GManager.Instance != null)
            hp -= GManager.Instance.lvlDamage;
        else
            hp--;
        if (hp <= 0) {
            //TODO: Destroy
            monsterCount.value--;
            Destroy(gameObject);
        }
        hpSlider.value = (float)hp / bornHp;
        Text text=hpSlider.GetComponentInChildren<Text>();
        if(hp>=0)
            text.text = hp + "/" + bornHp;
        else
            text.text = 0 + "/" + bornHp;
    }

    private void randomTarget() {
        float x = Random.Range(spawner.position.x - spawner.localScale.x / 2 + transform.localScale.x / 2, spawner.position.x + spawner.localScale.x / 2 - transform.localScale.x / 2);
        float y = Random.Range(spawner.position.y - spawner.localScale.y / 2 + transform.localScale.y / 2, spawner.position.y + spawner.localScale.y / 2 - transform.localScale.y / 2);
        target = new Vector3(x, y, transform.position.z);
    }

}
