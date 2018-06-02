using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleManager : MonoBehaviour {
	public SteamVR_TrackedObject trackedObjRight;
	public SteamVR_TrackedObject trackedObjLeft;

	private LineRenderer line;
	private LineRenderer debugline;

	public SteamVR_TrackedObject start, end;
	private GameObject center;

	public int segments;
	private float xradius;
	private float yradius;
	GameObject sphere;
	// Use this for initialization
	void Start () {

		debugline = GetComponent<LineRenderer> ();
		segments = 100;
		debugline.positionCount = segments + 1;
		//sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObjRight.index);
		start = trackedObjLeft;
		end = trackedObjRight;

		float x, y, z = 0f;
		float angle = 20f;
		float diameter = Vector3.Distance (start.transform.position, end.transform.position);
		xradius = yradius = diameter / 2.0f;

		Vector3 one = start.transform.position;
		Vector3 two = end.transform.position;
		Vector3 center = new Vector3 ((one.x + two.x) / 2f, (one.y + two.y) / 2f, (one.z + two.z) / 2f);
		debugline.transform.SetPositionAndRotation (center, start.transform.rotation);
		//sphere.transform.position = center;
		for(int i=0;i<(segments+1);i++){
			x = Mathf.Sin(Mathf.Deg2Rad *angle)*xradius;
			y = Mathf.Cos(Mathf.Deg2Rad *angle)*yradius;
			z = start.transform.position.z;
			debugline.SetPosition(i,new Vector3(x,y,z));
			angle += (360f / segments);
		}
		
	}
}
