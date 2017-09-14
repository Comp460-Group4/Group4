using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Combine : MonoBehaviour {

	private GameObject block;
	private SteamVR_TrackedObject trackedObj;

	public GameObject laserPrefab;
	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;

	public Transform cameraRigTransform; 
	public GameObject teleportReticlePrefab;
	private GameObject reticle;
	private Transform teleportReticleTransform;
	public Transform headTransform;
	public Vector3 teleportReticleOffset;
	public LayerMask teleportMask;
	private bool shouldTeleport;



	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;

		reticle = Instantiate(teleportReticlePrefab);
		teleportReticleTransform = reticle.transform;
	}

	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true);
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
		laserTransform.LookAt(hitPoint);
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
	}

	public void combine(GameObject b){
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter> ();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		Destroy(this.gameObject.GetComponent<MeshCollider>());

		int i = 0;
		Debug.Log (meshFilters.Length);
		while (i < meshFilters.Length) {
			combine[i].mesh = meshFilters [i].sharedMesh;
			combine [i].transform = meshFilters [i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.SetActive(false);
			i++;
		}
		transform.GetComponent<MeshFilter> ().mesh = new Mesh ();
		transform.GetComponent<MeshFilter> ().mesh.CombineMeshes (combine, true);
		transform.GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
		transform.GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
		//transform.GetComponent<MeshFilter> ().mesh.Optimize();

		this.gameObject.AddComponent<MeshCollider> ();
		transform.gameObject.SetActive (true);

		Destroy(b);
	}

	private void Teleport(){
		shouldTeleport = false;
		reticle.SetActive (false);
		Vector3 difference = cameraRigTransform.position - headTransform.position;
		difference.y = 0;
		cameraRigTransform.position = hitPoint + difference;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
			
			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
			{
				hitPoint = hit.point;
				ShowLaser(hit);


				Vector3 objPos = hit.point + hit.normal/2.0f;

				objPos.x = (float) Math.Round(objPos.x, MidpointRounding.AwayFromZero);
				objPos.y = (float) Math.Round(objPos.y, MidpointRounding.AwayFromZero);
				objPos.z = (float) Math.Round(objPos.z, MidpointRounding.AwayFromZero);

				if (Controller.GetHairTriggerDown())
				{
					GameObject newBlock = Instantiate(block, objPos, Quaternion.identity);
					newBlock.transform.parent = this.transform;
					combine(newBlock);
				}
				reticle.SetActive(true);
				teleportReticleTransform.position = hitPoint + teleportReticleOffset;
				shouldTeleport = true;
			}
		}
		else
		{
			laser.SetActive(false);
			reticle.SetActive(false);
		}


		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
		{
			Teleport();
		}

	}
}
