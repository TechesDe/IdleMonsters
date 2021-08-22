using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour
{
    [SerializeField]
    private ScriptableRecords _records;

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private GameObject _rowPrefab;

    private void OnEnable() {
        for(int i = 0; i < _records.records.Count; i++) {
            if (_content.transform.childCount <= i) {
                GameObject row = Instantiate(_rowPrefab, _content.transform);
                row.transform.Find("Position").TryGetComponent(out Text text);
                row.transform.Find("Date").TryGetComponent(out Text date);
                row.transform.Find("Score").TryGetComponent(out Text score);
                text.text = (i + 1).ToString();
                date.text = _records.records[i].date;
                score.text = _records.records[i].score.ToString();
            } else {
                Debug.Log(_content.transform.childCount.ToString() + " i=" + i.ToString());
                _content.transform.GetChild(i).transform.Find("Position").TryGetComponent(out Text text);
                _content.transform.GetChild(i).transform.Find("Date").TryGetComponent(out Text date);
                _content.transform.GetChild(i).transform.Find("Score").TryGetComponent(out Text score);
                text.text = (i + 1).ToString();
                date.text = _records.records[i].date;
                score.text = _records.records[i].score.ToString();
            }
        }
    }

    private void OnDisable() {
        
    }
}
