// VR Project 
// Hermite Spline and surfaces
// Himanshu Pahadia (2014045) | Ramya YS (2015)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawLineManager : MonoBehaviour {


	public SteamVR_TrackedObject trackedObj;
	private LineRenderer currLine;
	private int numClicks = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			GameObject line = new GameObject ();
			currLine = line.AddComponent<LineRenderer> ();
			currLine.SetWidth (0.1f, 0.1f);
			numClicks = 0;
		} else if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			currLine.SetVertexCount (numClicks + 1);
			Vector3 pos = trackedObj.transform.position;
			currLine.SetPosition (numClicks, pos);
			numClicks++;
		} 
	}
}
