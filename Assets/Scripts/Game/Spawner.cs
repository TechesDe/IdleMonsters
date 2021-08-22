using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private ScriptableInt monsterCount;

    [SerializeField]
    private GameObject monsterPrefab;

    [SerializeField]
    private Slider _hpSlider;

    [SerializeField]
    private float DelayTimer=0f;

    [SerializeField]
    private int Hp;

    private int defaulHp;

    [SerializeField]
    private EventListener _updateEvent;

    [SerializeField]
    private EventListener _restart;

    private void Awake() {
        monsterCount.value = 0;
        defaulHp = Hp;
    }

    private void OnEnable() {
        _updateEvent.OnEventHappened += UpdateBehaviour;
        _restart.OnEventHappened += Restart;
    }

    private void OnDisable() {
        _updateEvent.OnEventHappened -= UpdateBehaviour;
        _restart.OnEventHappened -= Restart;
    }

    private void UpdateBehaviour() {
        if (DelayTimer < GManager.Instance.MaxDifficulty + 1 - GManager.Instance.difficulty)
            DelayTimer += Time.deltaTime;
        else {
            DelayTimer = 0f;
            Spawn();
            Hp = (int)(GManager.Instance.difficulty * GManager.Instance.lvlDamage * 10+10 * GManager.Instance.lvlDamage);
        }
    }

    public void Spawn() {
        GameObject monsterInst = Instantiate(monsterPrefab);
        float x = Random.Range(transform.position.x - transform.localScale.x / 2 + monsterInst.transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2 - monsterInst.transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2 + monsterInst.transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2 - monsterInst.transform.localScale.y / 2);
        monsterInst.transform.position = new Vector3(x, y, monsterInst.transform.position.z);
        monsterInst.TryGetComponent(out Moster moster);
        
        moster.hpSlider = _hpSlider;
        moster.spawner = transform;
        moster.speed = 1 + GManager.Instance.difficulty;
        moster.setHP(Hp, true);
    }

    public void Restart() {
        Hp = defaulHp;
    }
}
