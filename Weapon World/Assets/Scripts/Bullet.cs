using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //public GameObject colorPicker;
    private GameObject go;

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
        //bulletColor = picker.TheColor;
        //ChangeColor();
       
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        //go = Instantiate(colorPicker) as GameObject;
        //picker = go.GetComponent<ColorPickerTriangle>();
        //picker.SetNewColor(bulletColor);
        GetComponent<Renderer>().material.color = bulletColor;
    }
}
