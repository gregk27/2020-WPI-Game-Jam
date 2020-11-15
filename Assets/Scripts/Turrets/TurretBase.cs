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

        turret.transform.LookAt(enemyPos);
        /*targetRotation = Vector2.Angle(turretPos, enemyPos);
        Debug.Log(targetRotation);
        Debug.DrawLine(transform.position, targetEnemy.transform.position);

        turret.transform.eulerAngles = new Vector3(0, 0, targetRotation + 90);
        */
        fire();
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
