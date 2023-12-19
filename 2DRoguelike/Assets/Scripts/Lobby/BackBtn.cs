using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    private GameObject _interactionUIBundle;
    private void Start()
    {
        _interactionUIBundle = transform.parent.gameObject;
    }

    public void InactiveUIBundle()
    {
        _interactionUIBundle.SetActive(false);
    }
}
