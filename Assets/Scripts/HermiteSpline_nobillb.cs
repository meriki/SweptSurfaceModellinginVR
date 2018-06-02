using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSpline_nobillb : MonoBehaviour {

	public SteamVR_TrackedObject trackedObjRight;
	public SteamVR_TrackedObject trackedObjLeft;
	private MeshLineRenderer currLine;
	private int numClicks = 0;


	public GameObject startTangentPoint, endTangentPoint;
	public SteamVR_TrackedObject start, end;
	public Color color = Color.white;
	public float width = 0.2f;
	public int numberOfPoints = 20;

	LineRenderer line;
	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		line.useWorldSpace = true;
		line.material = new Material(Shader.Find("Particles/Additive"));
	}

	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObjRight.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("Inside");
			start = trackedObjLeft;
			end = trackedObjRight;

			//line.AddComponent<LineRenderer> ();
			// update line renderer

			line.startColor = color;
			line.endColor = color;
			line.startWidth = width;
			line.endWidth = width;
			if (numberOfPoints > 0)
			{
				line.positionCount = numberOfPoints;
			}

			// set points of Hermite curve
			Vector3 p0 = start.transform.position;
			Vector3 p1 = end.transform.position;
			Vector3 m0 = startTangentPoint.transform.position - start.transform.position;
			Vector3 m1 = endTangentPoint.transform.position - end.transform.position;
			float t;
			Vector3 position;

			for(int i = 0; i < numberOfPoints; i++)
			{
				t = i / (numberOfPoints - 1.0f);
				position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 
					+ (t * t * t - 2.0f * t * t + t) * m0 
					+ (-2.0f * t * t * t + 3.0f * t * t) * p1 
					+ (t * t * t - t * t) * m1;
				line.SetPosition(i, position);
			}
		}


	}
}
