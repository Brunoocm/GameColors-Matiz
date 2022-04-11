using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health;
    public float timeInvencible;
    private float m_timeInvencible;

    [SerializeField] public bool canDamage;
  
    void Start()
    {
        canDamage = true;
        m_timeInvencible = timeInvencible;

    }

    void Update()
    {
        timeInvencible -= Time.deltaTime;
        if(timeInvencible <= 0)
        {
            canDamage = true;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageVoid(int dano)
    {
        if (canDamage)
        {
            health -= dano;
            canDamage = false;
            timeInvencible = m_timeInvencible;
        }
    }
}
