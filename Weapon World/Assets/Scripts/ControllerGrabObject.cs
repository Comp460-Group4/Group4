using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	// Use this for initialization
	/*void Start () {
		
	}*/

	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject;
	private GameObject objectInHand;
    private GameObject combinedObject;
	//SteamVR_Controller.Device device;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	private void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private void SetCollidingObject(Collider col)
	{
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}
		collidingObject = col.gameObject;
	}
		
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}
		collidingObject = null;
	}

	private void GrabObject()
	{
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}
		objectInHand = null;
	}

    /*
	private void Spawn()
	{
		Instantiate(objectInHand, trackedObj.transform.position, trackedObj.transform.rotation);
	}
		*/
	// Update is called once per frame
	void Update () {


		if (Controller.GetHairTriggerDown ()) {
			if (collidingObject) {
				GrabObject ();

				//Vector2 axis;

				//float numX = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).x;
				//float numY = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).y;

				//if (Controller.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
				//axis = Controller.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

				//if (axis.y > 0.2f || axis.y < -0.2f) {

				/*if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
					Vector3 temp = objectInHand.gameObject.transform.localScale;
					temp.x += 5f;
					objectInHand.gameObject.transform.localScale = temp;
				}*/

				//}
				}
		}

		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand)
			{
				ReleaseObject();
				/*Vector3 temp = objectInHand.gameObject.transform.localScale;
				temp.x = objectInHand.transform.localScale.x;
				objectInHand.gameObject.transform.localScale = temp;
*/
			}
		}
	}
}
