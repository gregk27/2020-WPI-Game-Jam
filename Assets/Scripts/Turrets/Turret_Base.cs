using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Base : MonoBehaviour
{
    bool hasTarget;
    int range;
    int maxAngle;
    int ammo;
    int maxAmmo;
    int bulletPenetration;

    float targetRotation;

    public GameObject turret;
    public GameObject turretBase;
    public GameObject aimOrigin;
    public GameObject enemyManager;
    GameObject targetEnemy;



    Animator turretAnim;
    Animator turretBaseAnim;

    void Start()
    {
        turretAnim = turret.GetComponent<Animator>();
        turretBaseAnim = turretBase.GetComponent<Animator>();
    }
    void Update()
    {
        //EnemyBase enemyBaseScript = GetComponent<EnemyBase>;
        if(targetEnemy != null)
        {
            if(1 == 1) {
                foreach (Transform child in enemyManager.transform)
                {
                    if (Vector2.Distance(transform.position, child.position) < Vector2.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = child.gameObject;
                    }
                }
            }
        }
        //Physics2D.Raycast(aimOrigin, );


        if (hasTarget && ammo > 0)
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
