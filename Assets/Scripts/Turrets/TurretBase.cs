using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    bool hasTarget;
    public int range;
    int maxAngle;
    public int ammo;
    public int maxAmmo;
    public int cooldown;

    public int upgradeLevel;

    public int baseCooldown = 60;
    public int baseDamage = 1;
    public int baseSpeed = 50;

    private int shotTimer = 0;

    float targetRotation;

    public GameObject turret;
    public GameObject turretBase;
    public GameObject aimOrigin;
    public GameObject enemyManager;
    public GameObject bulletManager;
    public GameObject bullet;
    public GameObject targetEnemy;


    EnemyBase targetEnemyScript;
    Animator turretAnim;
    Animator turretBaseAnim;
    

    void Start()
    {
        turretAnim = turret.GetComponent<Animator>();
        turretBaseAnim = turretBase.GetComponent<Animator>();
        targetEnemyScript = targetEnemy.GetComponent<EnemyBase>();
    }
    void Update()
    {
        shotTimer--;

        targetEnemy = null;

        RaycastHit2D hit;

        foreach(Transform t in enemyManager.transform)
        {
            float dist = Vector2.Distance(transform.position, t.position);
            if(dist < range)
            {
                if(targetEnemy == null || t.position.x > targetEnemy.transform.position.x)
                {
                    // Check that you can hit. Mask removes bullets from check
                    hit = Physics2D.Raycast(transform.position, t.position - transform.position, range, ~(1<<11));
                    // Check that the enemy is in line of sight
                    if (hit.transform.tag == "Enemy")
                    {
                        targetEnemy = t.gameObject;
                    }
                }
            }
        }

        if(targetEnemy != null)
        {
            aim();
            if(ammo > 0)
            {
                turretAnim.SetTrigger("hasTarget");
                fire();
            } else {
                reload();
            }
        } else
        {
            turret.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
    }

    void aim()
    {
        Vector2 turretPos = new Vector2(transform.position.x, transform.position.y);
        Debug.Log("turret position: " + turretPos);
        // Enemy position is enemy's current location + lead ahead (x axis only)
        Vector2 enemyPos = new Vector2(targetEnemy.transform.position.x, targetEnemy.transform.position.y)
            // Lead ahead is : V_enemy * (dist/V_bullet)
            + (Vector2) targetEnemy.GetComponent<Pathfinding.AIPath>().desiredVelocity * Vector2.Distance(transform.position, targetEnemy.transform.position) / bullet.GetComponent<bulletBase>().speed;

        // Get direction vector
        Vector2 dir = enemyPos - turretPos;
        // Get target angle
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        // Rotate turret to angle
        turret.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    void fire()
    {
        if(turretBaseAnim != null && shotTimer <= 0)
        {
            turretBaseAnim.SetInteger("ammo", ammo - 1);
            GameObject b = Instantiate(bullet, aimOrigin.transform.position, turret.transform.rotation, bulletManager.transform);
            shotTimer = baseCooldown/(upgradeLevel*4);
            b.GetComponent<bulletBase>().damage = baseDamage * upgradeLevel / 3;
            b.GetComponent<bulletBase>().speed = baseSpeed * upgradeLevel / 2;
        }
        
    }
    void reload()
    {
        if (turretBaseAnim != null)
        {
            turretBaseAnim.SetInteger("ammo", maxAmmo);
            ammo = maxAmmo;
        }
    }

}
