using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackMenu : MonoBehaviour
{
    private void Awake() {
        gameObject.TryGetComponent(out Button btn);
        btn.onClick.AddListener(LoadMenu);
    }

    private void LoadMenu() {
        LoadManager.Instance.Load("Menu");
    }
}
