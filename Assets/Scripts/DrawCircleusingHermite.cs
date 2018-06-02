using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleusingHermite : MonoBehaviour {

	public SteamVR_TrackedObject trackedObjRight;
	public SteamVR_TrackedObject trackedObjLeft;
	private MeshLineRenderer currLine;
	private int numClicks = 0;


	public GameObject startTangentPoint, endTangentPoint;
	public SteamVR_TrackedObject start, end;
	public Color colorstart = Color.white;
	public Color colorend = Color.white;
	public float width = 0.05f;
	public int numberOfPoints = 20;

	// normal line
	private LineRenderer line;
	private LineRenderer debugline;
	// mesh line
	//private MeshLineRenderer line;

	// Use this for initialization
	void Start () {
		debugline = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObjRight.index);
		Vector3 forwardleft = trackedObjLeft.transform.forward;
		Vector3 forwardright = trackedObjRight.transform.forward;

		Vector3 backwardleft = -1 * forwardleft;
		Vector3 backwardright = -1 * forwardright;
		start = trackedObjLeft;
		end = trackedObjRight;

		//debugline.startColor = colorstart;
		//debugline.endColor = colorend;
		//debugline.startWidth = width;
		//debugline.endWidth = width;
		//debugline.renderer.material.SetColor("_TintColor", new Color(1, 1, 1, 0.5f));
		if (numberOfPoints > 0)
		{
			// normal line comment me
			debugline.positionCount = numberOfPoints;
		}

		// set points of Hermite curve
		Vector3 p0 = start.transform.position;
		Vector3 p1 = end.transform.position;
		//Vector3 m0 = startTangentPoint.transform.position - start.transform.position;
		//Vector3 m1 = endTangentPoint.transform.position - end.transform.position;

		// checking tangent
		Vector3 m0 = forwardleft;
		Vector3 m1 = forwardright;

		Vector3 m2 = backwardleft;
		Vector3 m3 = backwardright;

		float t;
		Vector3 position;

		for(int i = 0; i < numberOfPoints; i++)
		{
			t = i / (numberOfPoints - 1.0f);
			position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 
				+ (t * t * t - 2.0f * t * t + t) * m0 
				+ (-2.0f * t * t * t + 3.0f * t * t) * p1 
				+ (t * t * t - t * t) * m1;

			// mesh line comment me
			//line.AddPoint(position);

			// normal line comment me
			debugline.SetPosition(i, position);
		}

	}
}
