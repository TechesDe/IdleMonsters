using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    private void Awake() {
        gameObject.TryGetComponent(out Button btn);
        btn.onClick.AddListener(LoadGame);
    }

    private void LoadGame() {
        LoadManager.Instance.Load("Game");
    }
}
