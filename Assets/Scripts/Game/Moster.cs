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
    private ScriptableInt _monsterCount;

    [SerializeField]
    private EventDispatcher _die;

    [SerializeField]
    private ScriptableInt _score;

    [SerializeField]
    private ScriptableInt _money;

    [SerializeField]
    private GameObject _monsterModel;

    private int cost = 0;

    public Transform spawner;

    public int hp;
    public int bornHp = 100;
    public float speed = 1f;
    public float difficultCostFactor = 5f;

    private bool isDie = false;

    public Vector3 target; //Place where object going

    public float fault = 1; //delta distance to change target

    public Slider hpSlider;

    private void Awake() {
        if (bornHp <= 0)
            Destroy(gameObject);
        hp = bornHp;
        _monsterCount.value++;
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
        if (!isDie) {
            if (GManager.Instance != null) {
                cost = (int)(cost + difficultCostFactor * GManager.Instance.difficulty);
                hp -= GManager.Instance.lvlDamage;
            } else
                hp--;
            if (hp <= 0) {
                _monsterCount.value--;
                _score.value += bornHp;
                _money.value += cost;
                _die.Dispatch();
                isDie = true;
                StartCoroutine(AnimationCoroutine(1f));
            }
            hpSlider.value = ((float)hp) / bornHp;
            Text text = hpSlider.GetComponentInChildren<Text>();
            if (hp >= 0)
                text.text = NumText(hp) + "/" + NumText(bornHp);
            else
                text.text = 0 + "/" + NumText(bornHp);
        }
    }

    private IEnumerator AnimationCoroutine(float time) {
        _monsterModel.TryGetComponent(out Animation_Test anime);
        anime.DeathAni();
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void randomTarget() {
        float x = Random.Range(spawner.position.x - spawner.localScale.x / 2 + transform.localScale.x / 2, spawner.position.x + spawner.localScale.x / 2 - transform.localScale.x / 2);
        float y = Random.Range(spawner.position.y - spawner.localScale.y / 2 + transform.localScale.y / 2, spawner.position.y + spawner.localScale.y / 2 - transform.localScale.y / 2);
        target = new Vector3(x, y, transform.position.z);
    }

    private string NumText(int value) {
        if (value < 0)
            return "-";
        if (value >= 0 && value < 100)
            return value.ToString();
        if (value > 100 && value < 10000)
            return (value / 1000).ToString() + "." + ((value / 100) % 10).ToString() + "k";
        if (value > 10000 && value < 1000000)
            return (value / 1000).ToString() + "k";
        else {
            return (value / 1000000).ToString() + "m";
        }
    }

}
