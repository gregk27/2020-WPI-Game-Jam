using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    bool hasTarget;
    int range;
    int maxAngle;
    int ammo;
    int maxAmmo;

    float targetRotation;

    public GameObject turret;
    public GameObject turretBase;
    public GameObject aimOrigin;
    public GameObject enemyManager;
    public GameObject bulletManager;
    public GameObject bullet;
    GameObject targetEnemy;


    EnemyBase targetEnemyScript;
    Animator turretAnim;
    Animator turretBaseAnim;
    

    void Start()
    {
        turretAnim = turret.GetComponent<Animator>();
        turretBaseAnim = turretBase.GetComponent<Animator>();
    }
    void Update()
    {
        targetEnemyScript = targetEnemy.GetComponent<EnemyBase>();
        if(targetEnemy != null)
        {
            if(targetEnemyScript.health > 0) {
                foreach (Transform child in enemyManager.transform)
                {
                    if (Vector2.Distance(transform.position, child.position) < Vector2.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = child.gameObject;
                    }
                }
            }
        }
        RaycastHit2D thingItHit = Physics2D.Raycast(aimOrigin.transform.position, targetEnemy.transform.position, range);

        if(thingItHit.collider.gameObject == targetEnemy)
        {
            aim();
        }

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
        Vector2 enemyPos = new Vector2(targetEnemy.transform.position.x, targetEnemy.transform.position.y);

        targetRotation = Vector2.Angle(turretPos, enemyPos);

        turret.transform.Rotate(0, 0, targetRotation);
    }
    void fire()
    {
        if(turretBaseAnim != null)
        {
            turretBaseAnim.SetInteger("ammo", ammo - 1);
            Instantiate(bullet, aimOrigin.transform.position, transform.rotation, bulletManager.transform);
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
