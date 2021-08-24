using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class GoldBonus : Bonus
{
    public int minValue = 100;
    public int maxValue = 1000;

    private int value=100;

    [SerializeField]
    private EventDispatcher _moneyEvents;

    [SerializeField]
    private ScriptableInt _money;

    private void Awake() {
        value = Random.Range(minValue, maxValue);
    }

    protected override void OnMouseDown() {
        _money.value+=value;
        _moneyEvents.Dispatch();
        base.OnMouseDown();
    }
}
