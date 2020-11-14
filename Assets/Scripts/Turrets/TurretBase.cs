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
            aim(thingItHit.transform.position.x, thingItHit.transform.position.y);
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
    void aim(float enemyX, float enemyY)
    {
        targetRotation = Mathf.Atan2(enemyY, enemyX);

        turret.transform.Rotate(0, 0, targetRotation * Mathf.Rad2Deg);
    }

    void fire()
    {
        turretBaseAnim.SetInteger("ammo", ammo - 1); 
    }
    void reload()
    {
        turretBaseAnim.SetInteger("ammo", maxAmmo);
        ammo = maxAmmo;
    }
}
