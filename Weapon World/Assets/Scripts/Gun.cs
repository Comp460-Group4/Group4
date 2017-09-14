using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public int force = 30;

    public Camera cam;
    public GameObject spawn;
    public GameObject bullet;
    //public GameObject cPicker;

    private SteamVR_TrackedObject trackedObj;
    private bool isSpawn;
    private GameObject collidingObject;
    private string objectname;

    public ColorManager manager;
    private Color c;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        if(objectname == "SpawnGun")
        {
            if (Controller.GetHairTriggerDown())
            {
                Spawn();
            }
        }
        else if(objectname == "PretendGun")
        {
            isSpawn = false;
            if (Controller.GetHairTriggerDown())
            {
                ChangeColor();
                Shoot();
            }
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "SpawnGun")
        {
            objectname = "SpawnGun";
            Debug.Log(objectname);
        }
        else if(other.gameObject.name == "PretendGun")
        {
            objectname = "PretendGun";
            Debug.Log(objectname);
        }
        else
        {
            Debug.Log(other.gameObject.name);
        }
    }
    private void Shoot()
    {
        RaycastHit hit;
        GameObject projectile;
        if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            if(hit.transform.name == "SpawnGun")
            {
                isSpawn = true;
                Debug.Log(isSpawn);
            }
            else { isSpawn = false; Debug.Log(isSpawn); }

            /*Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }*/

            //Instantiate
            projectile = Instantiate(bullet) as GameObject;
            projectile.transform.position = transform.position + trackedObj.transform.forward;
            //projectile.transform.position = transform.position + Spread(cam.transform.forward, 10, 2);

            //Shooting forward
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.velocity = trackedObj.transform.forward * 20;
            //rigidbody.velocity = Spread(cam.transform.forward, 10, 2);
        }
    }

    private void Spawn()
    {
        RaycastHit hit;
        GameObject projectile;
        if(Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            projectile = Instantiate(spawn) as GameObject;
            projectile.transform.position = transform.position + trackedObj.transform.forward;

            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.velocity = trackedObj.transform.forward * 10;
        }
    }

    public Color ChangeColor()
    {
        c = manager.color;
        return c;
    }

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
}
