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
        
        if(targetEnemy == null || Vector2.Distance(transform.position, targetEnemy.transform.position) > range)
        {
            targetEnemy = enemyManager.transform.GetChild(0).gameObject;
            foreach (Transform child in enemyManager.transform)
            {
                if (Vector2.Distance(transform.position, child.position) < Vector2.Distance(transform.position, targetEnemy.transform.position))
                {
                    targetEnemy = child.gameObject;
                }
            }
            
        }

        aim();

        //RaycastHit2D thingItHit = Physics2D.Raycast(aimOrigin.transform.position, targetEnemy.transform.position, range);

        /*if(thingItHit.collider.gameObject == targetEnemy)
        {
            aim();
        }
        */
        if(hasTarget && ammo > 0)
        {
            turretAnim.SetTrigger("hasTarget");
            //aim();
        }
        else if(hasTarget && ammo <= 0)
        {
            reload();
        }
    }

    void aim()
    {
        Vector2 turretPos = new Vector2(transform.position.x, transform.position.y);
        Debug.Log("turret position: " + turretPos);
        Vector2 enemyPos = new Vector2(targetEnemy.transform.position.x, targetEnemy.transform.position.y);
        Debug.Log("enemy position: " + enemyPos);

        // Get direction vector
        Vector2 dir = enemyPos - turretPos;
        // Get target angle
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        // Rotate turret to angle
        turret.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        fire();
    }

    void fire()
    {
        if(turretBaseAnim != null && shotTimer <= 0)
        {
            turretBaseAnim.SetInteger("ammo", ammo - 1);
            Instantiate(bullet, aimOrigin.transform.position, turret.transform.rotation, bulletManager.transform);
            shotTimer = cooldown;
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
