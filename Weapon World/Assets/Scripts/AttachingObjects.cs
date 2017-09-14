using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachingObjects : MonoBehaviour {

	public GameObject Prefab;
	private bool collide = false;

	// Use this for initialization
	void Start () {
		
	}

	private bool IsColliding(Collider col)
	{
		bool check;
		if(Prefab || !col.GetComponent<Rigidbody>())
		{
			check = false;
		}
		return check = true;
	}

	public void OnTriggerEnter(Collider other)
	{
		collide = IsColliding(other);
	}

	public void OnTriggerStay(Collider other)
	{
		collide = IsColliding(other);
	}

	public void OnTriggerExit(Collider other)
	{
		if (!Prefab)
		{
			return;
		}
		Prefab = null;
	}

	private void Attach()
	{
		Prefab.transform.parent = Prefab.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(collide == true)
		{
			Attach();
		}
		else
		{

		}
	}
}
