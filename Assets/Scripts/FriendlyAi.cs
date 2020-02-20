﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAi : MonoBehaviour {

    [SerializeField]
    Transform target;

    [SerializeField]
    [Range(60.0f, 80.0f)]
    float speed = 50f;

    [SerializeField]
    float targetTime;

    [SerializeField]
    float rotationalDamp = 0.3f;

    [SerializeField]
    float raycastOffset = 10.0f;

    [SerializeField]
    float detectionDistance = 20.0f;

    public GameObject waypoint;
    public GameObject enemyTarget;

    private bool gotTarget;
    private bool lostTarget;

    private void Start()
    {
        CreateWaypoint();
    }

    private void Update()
    {
        Pathfinding();
        Move();
        if (target == null)
        {
            CreateWaypoint();
        }

        float distance = Vector3.Distance(waypoint.transform.position, transform.position);
        if (distance <= 50)
        {
            Destroy(waypoint);
            CreateWaypoint();
        }

        if (lostTarget == true)
        {
            targetTime -= Time.deltaTime;
            if (targetTime < 0)
            {
                gotTarget = false;
                target = null;
                lostTarget = false;
            }
        }
    }

    void Turn()
    {
        if (waypoint != null && gotTarget == false)
        {
            target = waypoint.transform;
        }
        if (target == null)
        {
            CreateWaypoint();
        }
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Pathfinding()
    {
        RaycastHit hit;
        Vector3 rayOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * raycastOffset;
        Vector3 right = transform.position + transform.right * raycastOffset;
        Vector3 up = transform.position + transform.up * raycastOffset;
        Vector3 down = transform.position - transform.up * raycastOffset;

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance + 1))
        {
            rayOffset += Vector3.right;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            rayOffset -= Vector3.right;
        }

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance + 1))
        {
            rayOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            rayOffset += Vector3.up;
        }

        if (rayOffset != Vector3.zero)
        {
            transform.Rotate(rayOffset * 5f * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }

    private void CreateWaypoint()
    {
        waypoint = new GameObject("Waypoint");
        waypoint.transform.position = new Vector3(UnityEngine.Random.Range(-400, 400), UnityEngine.Random.Range(-400, 400), UnityEngine.Random.Range(-400, 400));

        target = waypoint.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (gotTarget == false)
            {
                target = null;
                target = other.transform;
                gotTarget = true;
                lostTarget = false;
            }
            else
            {
                if (other.transform == target)
                {
                    lostTarget = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.position == target.position)
        {
            targetTime = 20.0f;
            lostTarget = true;
        }
    }
}
