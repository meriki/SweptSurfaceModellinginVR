// VR Project 
// Hermite Spline and surfaces
// Himanshu Pahadia (2014045) | Ramya YS (2015)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawMeshLineManager : MonoBehaviour {


	public SteamVR_TrackedObject trackedObj;
	private MeshLineRenderer currLine;
	private int numClicks = 0;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			GameObject line = new GameObject ();

			line.AddComponent<MeshFilter> ();
			line.AddComponent<MeshRenderer> ();
			currLine = line.AddComponent<MeshLineRenderer> ();
			currLine.setWidth (0.02f);
			numClicks = 0;
		} else if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			//currLine.SetVertexCount (numClicks + 1);
			Vector3 pos = trackedObj.transform.position;
			//currLine.SetPosition (numClicks, pos);
			currLine.AddPoint(pos);

			numClicks++;
		} 
	}
}

