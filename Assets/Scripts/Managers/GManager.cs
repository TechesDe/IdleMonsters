using System;
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
    private float _speedOfDifficulty = 0.1f;

    public int lvlDamage = 1;

    [SerializeField]
    private Text _lvlText;

    [SerializeField]
    private GamePanel _gamePanel;

    public static GManager Instance;


    [SerializeField]
    private ScriptableInt _monsterCount;

    [SerializeField]
    private ScriptableRecords _records;

    [SerializeField]
    private ScriptableInt _score;

    [SerializeField]
    private EventListener _update;

    [SerializeField]
    private EventListener _restart;

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

    private void Start() {
        Restart();
    }

    private void OnEnable() {
        _update.OnEventHappened += IncreasingDifficulty;
        _restart.OnEventHappened += Restart;
    }

    private void OnDisable() {
        _update.OnEventHappened -= IncreasingDifficulty;
        _restart.OnEventHappened -= Restart;
    }

    private void IncreasingDifficulty() {
        if(difficulty<MaxDifficulty)
            difficulty += _speedOfDifficulty * Time.deltaTime;
        if (_monsterCount.value > 10) {
            UpdateManager.Instance.PauseToogle(true);
            ScriptableRecords.Record record;
            record.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            record.score = _score.value;
            int i = _records.records.Count - 1;
            while (i >= 0 && _records.records[i].score < record.score) {
                i--;
            }
            i++;
            _records.records.Insert(i, record);
            _gameOver.Dispatch();
        }
    }

    public void DamageUp(int value = 1) {
        lvlDamage += value;
        _lvlText.text = lvlDamage.ToString();
    }

    public void Restart() {
        difficulty = 1f;
        lvlDamage = 1;
        _lvlText.text = lvlDamage.ToString();
        _monsterCount.value = 0;
        _score.value = 0;
        _gamePanel.gameObject.SetActive(true);
        UpdateManager.Instance.PauseToogle(false);
    }
}
