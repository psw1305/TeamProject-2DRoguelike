using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TempSys : MonoBehaviour
{
    [SerializeField] private ItemBlueprint _goldCoin;
    [SerializeField] private ItemBlueprint _silverCoin;
    [SerializeField] private GameObject _pickupItem;

    private void Start()
    {
        Init1();
        Init2();
    }

    private void Init1()
    {
        PickupItem pickupItem = Instantiate(_pickupItem).GetComponent<PickupItem>();
        pickupItem.SetItem(_goldCoin);
    }

    private void Init2()
    {
        PickupItem pickupItem = Instantiate(_pickupItem).GetComponent<PickupItem>();
        pickupItem.SetItem(_silverCoin);
    }
}
