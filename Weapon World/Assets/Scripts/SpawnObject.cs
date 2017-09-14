using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	public GameObject Prefab;
	//public Transform spawnPoint;

	/*
	// Use this for initialization
	void Start () {
		
	}*/

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	private void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		
	}

	private void Spawn()
	{
		Instantiate(Prefab, trackedObj.transform.position, trackedObj.transform.rotation);	
	}

	// Update is called once per frame
	void Update () {
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
			Spawn();
		}
	}
}
