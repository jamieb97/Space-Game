using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float damage = 20.0f;
    public float range = 1000.0f;
    public float firerate = 1f;
    public GameObject particle;
    public Transform gunEnd;
    private LineRenderer laser;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private float nextTimeToFire = 0f;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitTarget;
        Vector3 center = transform.position;

        Debug.DrawRay(center, transform.forward * range, Color.cyan);

        if (Physics.Raycast(center, transform.forward, out hitTarget, 40) && Time.time >= nextTimeToFire)
        {
            if (hitTarget.transform.tag == "Player")
            {
                nextTimeToFire = Time.time + 1f / firerate;
                Shooting();
                StartCoroutine(ShotEffect());
            }

        }

    }

    private void Shooting()
    {
        RaycastHit hit;

        laser.SetPosition(0, gunEnd.position);
        if (Physics.Raycast(gunEnd.transform.position, gunEnd.transform.forward, out hit, range))
        {
            laser.SetPosition(1, hit.point);
            Player target = hit.transform.GetComponent<Player>();
            Friendly target1 = hit.transform.GetComponent<Friendly>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            else if(target1 != null)
            {
                target1.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(particle, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        else
        {
            laser.SetPosition(1, gunEnd.transform.position + (gunEnd.transform.forward * range));
        }
    }

    private IEnumerator ShotEffect()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }

}
