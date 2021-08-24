using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.UI;
using Audio;

public class GManager : MonoBehaviour
{
    public float difficulty = 1f;

    public float MaxDifficulty = 5f;

    public int costDefault = 5;
    private int cost;

    [SerializeField]
    private float _speedOfDifficulty = 0.1f;

    public int lvlDamage = 1;

    [SerializeField]
    private Text _lvlText;

    [SerializeField]
    private Text _costText;

    [SerializeField]
    private GamePanel _gamePanel;

    public static GManager Instance;

    [SerializeField]
    public ScriptableInt _money;

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

    [SerializeField]
    private EventDispatcher _lvlUp;

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
            MusicManager.Instance.Stop();
            MusicManager.Instance.GameOver();
        }
    }

    public void DamageUp(int value = 1) {
        if (_money.value > cost) {
            _money.value -= cost;
            cost+=(int)(1.6*cost);
            _costText.text = cost.ToString();
            lvlDamage += value;
            _lvlUp.Dispatch();
            _lvlText.text = lvlDamage.ToString();
        } else {
            StartCoroutine(NotEnoughCoroutine(2f));
        }
    }

    private IEnumerator NotEnoughCoroutine(float time) {
        float timer = 0f;
        Color d = _costText.color;
        Color r = new Color(255, 0, 0);
        while (timer < time) {
            if ((((int)(timer*2))%2)==1)
                _costText.color = r;
            else
                _costText.color = d;
            timer += Time.deltaTime;
            yield return null;
        }
        _costText.color = d;
    }

    public void Restart() {
        cost = costDefault;
        _costText.text = cost.ToString();
        _money.value = 0;
        difficulty = 1f;
        lvlDamage = 1;
        _lvlText.text = lvlDamage.ToString();
        _monsterCount.value = 0;
        _score.value = 0;
        _gamePanel.gameObject.SetActive(true);
        UpdateManager.Instance.PauseToogle(false);
        MusicManager.Instance.PlayGameMusic();
    }
}
