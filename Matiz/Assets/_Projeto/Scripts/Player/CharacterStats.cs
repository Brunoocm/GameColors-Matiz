using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("stats")]
    public int health;
    public float timeInvencible;
    private float m_timeInvencible;
    public SpriteRenderer sprite;
   

    [Header("Shield")]
    public bool hasShield;
    public bool canDamage;
    public GameObject dropPrisma;
    public GameObject shieldObj;
    public Transform primaPos;
  
    void Start()
    {
        hasShield = true;
        canDamage = true;
        m_timeInvencible = timeInvencible;
    }

    void Update()
    {
        timeInvencible -= Time.deltaTime;

        if (timeInvencible <= 0)
        {
            canDamage = true;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (canDamage)
            sprite.color = Color.white;
        else
            sprite.color = Color.cyan;


    }

    public void DamageVoid(int dano)
    {
        if(hasShield)
        {
            shieldObj.SetActive(false);
            SpawnPrisma();
            hasShield = false;
        }
        else if(!hasShield)
        {
            if (canDamage)
            {
                health -= dano;
                timeInvencible = m_timeInvencible;
                canDamage = false;
            }
        }
   
    }

    public void GiveShield()
    {
        shieldObj.SetActive(true);
        hasShield = true;
    }

    public void SpawnPrisma()
    {
        GameObject prisma = Instantiate(dropPrisma, primaPos.position, Quaternion.identity);
        prisma.GetComponent<PrismaBase>().characterStats = gameObject.GetComponent<CharacterStats>();

    }
}
