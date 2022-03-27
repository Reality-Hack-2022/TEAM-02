/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/
// Original script: "MagneticPinch.cs" modified by unitycoder.com to just get the finger position & directions

using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class GetLeapFingers : MonoBehaviour
{
	HandModel hand_model;
	Hand leap_hand;
	public LineRenderer lineRenderer;
	public GameObject raycastTarget;
	private bool isPinched;
	private GameObject raycastHitObj;

	void Start()
	{
		hand_model = GetComponent<HandModel>();
		leap_hand = hand_model.GetLeapHand();
		if (leap_hand == null) Debug.LogError("No leap_hand founded");

		// set the color of the line
		lineRenderer.startColor = Color.white;
		lineRenderer.endColor = Color.white;

		// set width of the renderer
		lineRenderer.startWidth = 0.003f;
		lineRenderer.endWidth = 0.003f;

		isPinched = false;
	}

	void Update()
	{

		//FingerModel finger = hand_model.fingers[1];

		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		//if (Physics.Raycast(finger.GetTipPosition(), finger.GetRay().direction, out hit, Mathf.Infinity))
		if (Physics.Raycast(hand_model.GetPalmPosition(), hand_model.GetPalmNormal(), out hit, Mathf.Infinity))
		{
			//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			// draw ray from finger tips (enable Gizmos in Game window to see)
			//Debug.DrawRay(finger.GetTipPosition(), finger.GetRay().direction, Color.red);
			//Debug.Log("Did Hit");

			if (hit.transform.tag == "raycast")
            {
				if (isPinched)
                {
					if (raycastHitObj == null)
                    {
						raycastHitObj = hit.transform.gameObject;
						raycastHitObj.transform.parent = transform;
						//raycastHitObj.GetComponent<Rigidbody>().useGravity = false;
						//raycastHitObj.GetComponent<Rigidbody>().isKinematic = true;
					}
					
					
					lineRenderer.gameObject.SetActive(true);
					lineRenderer.SetPosition(0, hand_model.GetPalmPosition());
					lineRenderer.SetPosition(1, raycastHitObj.transform.position);

				} else
                {
					if (raycastHitObj)
                    {
						raycastHitObj.transform.parent = null;
						//raycastHitObj.GetComponent<Rigidbody>().useGravity = true;
						//raycastHitObj.GetComponent<Rigidbody>().isKinematic = false;
						raycastHitObj = null;
					}
					
					lineRenderer.gameObject.SetActive(true);
					lineRenderer.SetPosition(0, hand_model.GetPalmPosition());
					lineRenderer.SetPosition(1, hit.point);
				}
				//raycastTarget.transform.position = hit.point;
				//
			}

			
		}
	}

	public void RaycastGrab()
    {
		isPinched = true;
    }

	public void RaycastRelease()
    {
		isPinched = false;
	}
}