using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public Transform moveToPosition;

    private float speed = 0.5f;
	// Update is called once per frame
	void Update ()
    {
        if (Manager.startGame == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPosition.position, speed * Time.deltaTime);
        }
    }
}
