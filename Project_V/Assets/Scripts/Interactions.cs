using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

    public Transform playerCamera;
    public float interactionRange;
    public LayerMask interactableLayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit raycastHit;

		if(Physics.Raycast(ray, out raycastHit, interactionRange, interactableLayer))
        {
            GameObject hitObject = raycastHit.collider.gameObject;
            print(hitObject.name);
        }

        Debug.DrawRay(playerCamera.position, (playerCamera.forward * interactionRange), Color.green);
	}
}
