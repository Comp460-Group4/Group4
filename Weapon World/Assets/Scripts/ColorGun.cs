using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGun : MonoBehaviour {

    public float damage = 10f;
    public float colrange = 100f;
    public int force = 30;

    public Camera myCam;
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ColorShoot();
        }
    }

    private void ColorShoot()
    {
        RaycastHit hit;
        GameObject projectile;
        if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, colrange))
        {
            Debug.Log(hit.transform.name);

            /*Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }*/

            //Instantiate
            projectile = Instantiate(bullet) as GameObject;
            projectile.transform.position = transform.position + myCam.transform.forward * 2;
            //projectile.transform.position = transform.position + Spread(cam.transform.forward, 10, 2);

            //Shooting forward
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.velocity = myCam.transform.forward * 10;

            //rigidbody.velocity = Spread(cam.transform.forward, 10, 2);
        }
        
    }

    /*
    Vector3 Spread(Vector3 aim, float distance, float variance)
    {
        aim.Normalize();
        Vector3 v3;
        do
        {
            v3 = Random.insideUnitSphere;
        } while (v3 == aim || v3 == -aim);

        v3 = Vector3.Cross(aim, v3);
        v3 = v3 * Random.Range(0f, variance);
        return aim * distance + v3;
    }
    */
}
