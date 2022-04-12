using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health;
    public float timeInvencible;
    public SpriteRenderer sprite;
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

        if ( canDamage )
            sprite.color = Color.white;
        else
            sprite.color = Color.cyan;
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
