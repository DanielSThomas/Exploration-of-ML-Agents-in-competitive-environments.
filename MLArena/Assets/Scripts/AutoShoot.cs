using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{

    [SerializeField] private int team; // 0 = Red Team  1 = Blue Team
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletobject;
    [SerializeField] private float firerate;
    private float nextShoot;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + firerate;

            canShoot = true;
        }

        if (canShoot == true)
        {
            GameObject _bullet = Instantiate(bulletobject, bulletSpawn.position, bulletSpawn.rotation);       
            _bullet.GetComponent<Bullet>().setbulletTeam(team);

            canShoot = false;
        }

    }





}
