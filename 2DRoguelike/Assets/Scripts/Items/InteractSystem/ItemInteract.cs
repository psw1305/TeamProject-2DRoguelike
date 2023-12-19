using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public InteractableItem CurrentItem { get; set;}
    private EquipSystem _equipSystem;
    private bool _isGet;

    private void Awake()
    {
        _equipSystem = new EquipSystem();
    }
    
    public void OnInteract()
    {
        if(CurrentItem == null) return;

        _isGet = CurrentItem.PlayerGetThis();
        if (_isGet)
        {
            _equipSystem.GetItemStatus(CurrentItem.GetBlueprint());
            Destroy(CurrentItem.gameObject);
        }
    }
}
