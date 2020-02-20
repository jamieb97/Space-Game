using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class Shoot : MonoBehaviour {

    public float damage = 25.0f;
    public float range = 1000.0f;
    public float firerate = 3f;
    public GameObject particle;
    public Camera fpsCam;
    public Transform gunEnd;
    private LineRenderer laser;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private float nextTimeToFire = 0f;

    Controller controller;
    Hand firsthand;
    Hand SecondHand;
    public List<Hand> hands = new List<Hand>();

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }
    // Update is called once per frame
    void Update ()
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        if (frame.Hands.Count == 2)
        {
            firsthand = hands[0];
            SecondHand = hands[1];
        }

        int extendedFingers = 0;

        if (firsthand.IsRight)
        {
            for (int f = 0; f < firsthand.Fingers.Count; f++)
            {
                Finger digit = firsthand.Fingers[f];
                if (digit.IsExtended)
                    extendedFingers++;
            }
        }
        else if (SecondHand.IsRight)
        {
            for (int f = 0; f < SecondHand.Fingers.Count; f++)
            {
                Finger digit = SecondHand.Fingers[f];
                if (digit.IsExtended)
                    extendedFingers++;
            }
        }

        if (extendedFingers == 0 && Time.time >= nextTimeToFire && ChangeCharacter.choosingCharacter == false)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shooting();
            StartCoroutine(ShotEffect());
        }

	}

    private void Shooting()
    {
        RaycastHit hit;

        laser.SetPosition(0, gunEnd.position);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            laser.SetPosition(1, hit.point);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(particle, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        else
        {
            laser.SetPosition(1, fpsCam.transform.position + (fpsCam.transform.forward * range));
        }
    }

    private IEnumerator ShotEffect()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }

    
}
