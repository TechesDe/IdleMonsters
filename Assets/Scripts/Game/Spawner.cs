using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private ScriptableInt monsterCount;

    [SerializeField]
    private GameObject monsterPrefab;

    [SerializeField]
    private Slider _hpSlider;

    private void Awake() {
        monsterCount.value = 0;
    }
    
    public void Spawn() {
        GameObject monsterInst = Instantiate(monsterPrefab);
        float x = Random.Range(transform.position.x - transform.localScale.x / 2 + monsterInst.transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2 - monsterInst.transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2 + monsterInst.transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2 - monsterInst.transform.localScale.y / 2);
        monsterInst.transform.position = new Vector3(x, y, monsterInst.transform.position.z);
        monsterInst.TryGetComponent(out Moster moster);
        
        moster.hpSlider = _hpSlider;
        moster.spawner = transform;
        moster.setHP(150, true);
    }
}
