using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    #region Field
    private GameObject _interactionUIBundle;
    #endregion

    #region UnityFlow
    private void Start()
    {
        _interactionUIBundle = transform.parent.gameObject;
    }
    #endregion

    #region Method
    public void InactiveUIBundle()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        _interactionUIBundle.SetActive(false);
    }
    #endregion
}
