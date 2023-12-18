using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    private CharacterController _controller;
    public InteractableItem CurrentItem { get; set;}

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // TODO => Event 연결
        // _controller.OnInteractEvent += GetItem;
    }

    public void GetItem()
    {
        if(CurrentItem == null) return;

        CurrentItem.OnInteract();
    }
}
