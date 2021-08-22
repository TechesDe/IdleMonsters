using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    public float difficulty = 1f;

    public float MaxDifficulty = 5f;

    [SerializeField]
    private float speedOfDifficulty = 0.1f;

    public int lvlDamage = 1;

    [SerializeField]
    private Text lvlText;

    public static GManager Instance;

    [SerializeField]
    private ScriptableInt monsterCount;

    [SerializeField]
    private EventListener _update;

    [SerializeField]
    private EventDispatcher _gameOver;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("GameManager already created");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable() {
        _update.OnEventHappened += IncreasingDifficulty;
    }

    private void OnDisable() {
        _update.OnEventHappened -= IncreasingDifficulty;
    }

    private void IncreasingDifficulty() {
        if(difficulty<MaxDifficulty)
            difficulty += speedOfDifficulty * Time.deltaTime;
        if (monsterCount.value > 10) {
            _gameOver.Dispatch();
        }
    }

    public void DamageUp(int value = 1) {
        lvlDamage += value;
        lvlText.text = lvlDamage.ToString();
    }

    public void Restart() {
        difficulty = 1f;
        lvlDamage = 1;
        lvlText.text = lvlDamage.ToString();
        monsterCount.value = 0;
    }
}
