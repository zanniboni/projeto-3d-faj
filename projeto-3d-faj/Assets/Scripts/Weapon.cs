using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Configurações da Arma")]
    public float damage;
    public float range;
    public float firerate;
    public float waitToFirerate;
    public Camera cam;
    public ParticleSystem armoParticle;
    public GameObject impact;
    public bool hold = false;
    [Header("Ammo")]
    public int maxAmmoInMagazine;
    public int AmmoInMagazine;
    public int Ammo;
    public int timeToReload;
    [Header("HUD")]
    public Text ammoShow;
    public Slider reloadShow;
    public GameObject reloadNow;

    private bool reloadb = false;
    private int timetr;

    // Update is called once per frame
    void Update()
    {
        reloadShow.value = timetr;
        ammoShow.text = AmmoInMagazine + "/" + Ammo;


        if (Input.GetButtonDown("Fire1"))
        {
            hold = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            hold = false;
        }

        if (hold == true)
        {
            waitToFirerate += 1;
        }

        if (waitToFirerate > firerate && AmmoInMagazine > 0)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && AmmoInMagazine != maxAmmoInMagazine && Ammo != 0 && reloadb == false)
        {
            reloadb = true;
        }

        if (reloadb == true)
        {
            if (timetr > timeToReload)
            {
                for (int i = 0; i < maxAmmoInMagazine; i++)
                {
                    if (AmmoInMagazine < maxAmmoInMagazine && Ammo > 0)
                    {
                        Ammo -= 1;
                        AmmoInMagazine += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                reloadb = false;
                timetr = timeToReload;
                reloadNow.SetActive(false);
            }
            else
            {
                timetr += 1;
            }
        }
    }

    void Start()
    {
        reloadShow.maxValue = timeToReload;
    }

    void Shoot()
    {
        waitToFirerate = 0;
        AmmoInMagazine -= 1;
        armoParticle.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("Mirando em:" + hit.transform.name);
            GameObject impact60 = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}