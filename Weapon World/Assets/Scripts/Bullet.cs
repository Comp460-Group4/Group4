using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //public GameObject colorPicker;
    private GameObject go;
    public Gun gun;

    //private ColorPickerTriangle picker;
    private Color bulletColor;
    private Rigidbody body;

	// Use this for initialization
	void Start () {

        // bulletColor = GetComponent<MeshRenderer>().material.color;
        body = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ChangeColor();
	}

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
        collision.gameObject.GetComponent<Renderer>().material.color = gun.ChangeColor();
        Debug.Log(collision.gameObject.name);

    }

    public void ChangeColor()
    {
        //go = Instantiate(colorPicker) as GameObject;
        //picker = go.GetComponent<ColorPickerTriangle>();
        //picker.SetNewColor(bulletColor);
        //GetComponent<Renderer>().material.color = gun.ChangeColor();
    }
}
