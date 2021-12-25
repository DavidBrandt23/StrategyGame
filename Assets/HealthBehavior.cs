using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        UpdateUI();
    }

    public void Damage(int damage)
    {
        CurrentHP -= damage;
        if(CurrentHP < 0)
        {
            CurrentHP = 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        text.text = CurrentHP + " / " + MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsDead()
    {
        return CurrentHP <= 0;
    }
}
