using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        
        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }

    }

    public void OnLevelUp()
    {
        unitLevel++;
        damage = damage + 3;
        maxHP = maxHP + 10;
    }

    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
}
