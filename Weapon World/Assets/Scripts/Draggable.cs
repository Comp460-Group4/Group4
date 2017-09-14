using UnityEngine;

public class Draggable : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public Transform minBound;

	public bool fixX;
	public bool fixY;
	public Transform thumb;	
	bool dragging;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

	void FixedUpdate()
	{
		if (Controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			dragging = false;

            Ray r = new Ray(trackedObject.transform.position, trackedObject.transform.forward);
			RaycastHit hit;
			if (GetComponent<Collider>().Raycast(r, out hit, 100)) {
				dragging = true;
			}
		}
		if (Controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) dragging = false;
        if (dragging && Controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Ray r = new Ray(trackedObject.transform.position, trackedObject.transform.forward);
            RaycastHit hit;
            if (GetComponent<Collider>().Raycast(r, out hit, 100))
            {
                var point = hit.point;
                point = GetComponent<Collider>().ClosestPointOnBounds(point);
                SetThumbPosition(point);
                SendMessage("OnDrag", Vector3.one - (thumb.localPosition - minBound.localPosition) / GetComponent<BoxCollider>().size.x);

            }
        }
	}

	void SetDragPoint(Vector3 point)
	{
		point = (Vector3.one - point) * GetComponent<Collider>().bounds.size.x + GetComponent<Collider>().bounds.min;
		SetThumbPosition(point);
	}

	void SetThumbPosition(Vector3 point)
	{
        Vector3 x = thumb.localPosition;
        thumb.position = point;
		thumb.localPosition = new Vector3(fixX ? x.x : thumb.localPosition.x, fixY ? x.y : thumb.localPosition.y, thumb.localPosition.z - 1);
	}
}
