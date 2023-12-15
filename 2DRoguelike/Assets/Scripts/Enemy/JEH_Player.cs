using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class JEH_Player : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    

    public string _tag = "Player";

    int _hp = 100;

    private void Awake()
    {
        hpText.text = $"{_hp}";
    }
    public void Damaged(int damage)
    {
        _hp -= damage;

        hpText.text = $"{_hp}";

    }



}
