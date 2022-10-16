using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public int level;
    public float maxHealth;
    public float health;
    public float attack;
    public float modifier;
    public double criticalChance;
    public List<string> items;

    private DifficultySetter difficultySetter;
    public float enemyModifier = 1;


    public float GetAttack()
    {
        DiffChecker();
        System.Random random = new System.Random();
        int playerChance = random.Next(0, 100);
        if (gameObject.CompareTag("Enemy") && criticalChance > playerChance)
        {
            return (attack + modifier) * 1.5f * enemyModifier;
        }
        if (criticalChance > playerChance)
        {
            return (attack + modifier) * 1.5f;
        }
        if (gameObject.CompareTag("Enemy"))
        {
            return (attack + modifier) * enemyModifier;
        }
        else { 
            return attack + modifier;
        }
    }

    public void DiffChecker()
    {
        if (DifficultySetter.isDiffEasy == true)
        {
            enemyModifier = 0.75f;
        }
        if (DifficultySetter.isDiffNormal == true)
        {
            enemyModifier = 1f;
        }
        if (DifficultySetter.isDiffHard == true)
        {
            enemyModifier = 1.25f;
        }
    }

    public void UseItem(string item)
    {
        string[] selectedItem = items.Where(x=>x.Equals(item)).ToList()[0].ToString().Split('+');
        string typeOfItem = selectedItem[0];
        int valueOfItem = int.Parse(selectedItem[1]);
        if (selectedItem[0].Equals("a"))
        {
            modifier += valueOfItem;
        } 
        else 
        {
            if(maxHealth > modifier + health)
            {
                health += modifier;
            }
            else
            {
                health = maxHealth;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
