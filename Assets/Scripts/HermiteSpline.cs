using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSpline : MonoBehaviour {

	public SteamVR_TrackedObject trackedObjRight;
	public SteamVR_TrackedObject trackedObjLeft;
	private LineRenderer currLine;
	private int numClicks = 0;


	public GameObject startTangentPoint, endTangentPoint;
	public SteamVR_TrackedObject start, end;
	public Color colorstart = Color.black;
	public Color colorend = Color.white;
	public float width = 0.05f;
	public int numberOfPoints = 20;

	private int choice;

	public Material a, b, c;

	private int materialnum = 1;

	// normal line
	private LineRenderer line;
	private LineRenderer debugline;
	// mesh line
	//private MeshLineRenderer line;


	private bool hermiteTrueNormalFalse;


	void Start () {
		hermiteTrueNormalFalse = true;
		debugline = GetComponent<LineRenderer> ();
		line.material = c;
		currLine.material = c;
		choice = 0;
	}


	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObjRight.index);
		Vector3 forwardleft = trackedObjLeft.transform.forward;
		Vector3 forwardright = trackedObjRight.transform.forward;
		start = trackedObjLeft;
		end = trackedObjRight;

		int last_choice = 0;

		if (device.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			
			Vector2 touchpad = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
			if (touchpad.y > 0.7) {
				for (float i = 0; i < 2000; i += 1) {
					Debug.Log ('k');
					device.TriggerHapticPulse (3999);
				}
				Debug.Log ("UP");
				choice = 0;
			} else if (touchpad.y < -0.7) {
				for (float i = 0; i < 2000; i += 1) {
					Debug.Log ('k');
					device.TriggerHapticPulse (3999);
				}
				last_choice = choice;
				choice = 2;
				Debug.Log ("Down");
			} else if (touchpad.x > 0.7) {
				for (float i = 0; i < 2000; i += 1) {
					Debug.Log ('k');
					device.TriggerHapticPulse (3999);
				}
				Debug.Log ("right");
				choice = 1;
			} else if (touchpad.x < -0.7) {
				for (float i = 0; i < 2000; i += 1) {
					Debug.Log ('k');
					device.TriggerHapticPulse (3999);
				}
				Debug.Log ("Left");
				choice = 3;
			}
		}

		// set points of Hermite curve
		Vector3 p0 = start.transform.position;
		Vector3 p1 = end.transform.position;
		//Vector3 m0 = startTangentPoint.transform.position - start.transform.position;
		//Vector3 m1 = endTangentPoint.transform.position - end.transform.position;

		// checking tangent
		Vector3 m0 = forwardleft;
		Vector3 m1 = forwardright;

		float t;
		Vector3 position;

		switch (choice) {
		case 0:
			debugline.enabled = true;
			// for closed loop
			//debugline.loop = true;

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

			break;
		case 1:
			debugline.enabled = true;
			// for closed loop
			debugline.loop = true;

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
			break;
		case 2:
			materialnum = (materialnum + 1) % 3;
			choice = last_choice;
			Debug.Log (materialnum);
			break;
		case 3:
			debugline.enabled = false;
			break;
		}

		if (choice == 3) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				GameObject line = new GameObject ();
				currLine = line.AddComponent<LineRenderer> ();
				currLine.SetWidth (0.1f, 0.1f);
				numClicks = 0;
			}
		}

		if(device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)){
			switch (choice) {
			case 0:
				
				GameObject newline = new GameObject ();


				line = newline.AddComponent<LineRenderer> ();
				switch (materialnum) {
				case 0:
					line.material = a;
					break;
				case 1:
					line.material = b;
					break;
				case 2:
					line.material = c;
					break;
				}
				// for closed curve
				//line.loop = true;
				//start = trackedObjLeft;
				//end = trackedObjRight;

				// mesh line comment me
				//line.setWidth(0.1f);

				// normal line comment me
				line.startColor = colorstart;
				line.endColor = colorend;
				line.startWidth = width;
				line.endWidth = width;

				if (numberOfPoints > 0)
				{
					// normal line comment me
					line.positionCount = numberOfPoints;
				}

				// set points of Hermite curve
				p0 = start.transform.position;
				p1 = end.transform.position;

				// checking tangent
				m0 = forwardleft;
				m1 = forwardright;



				for(int i = 0; i < numberOfPoints; i++)
				{
					t = i / (numberOfPoints - 1.0f);
					position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 
						+ (t * t * t - 2.0f * t * t + t) * m0 
						+ (-2.0f * t * t * t + 3.0f * t * t) * p1 
						+ (t * t * t - t * t) * m1;

					// normal line comment me
					line.SetPosition(i, position);
				}
				break;
			case 1:
				GameObject newlinex = new GameObject ();


				line = newlinex.AddComponent<LineRenderer> ();
				switch (materialnum) {
				case 0:
					line.material = a;
					break;
				case 1:
					line.material = b;
					break;
				case 2:
					line.material = c;
					break;
				}
				// for closed curve
				line.loop = true;
				//start = trackedObjLeft;
				//end = trackedObjRight;

				// mesh line comment me
				//line.setWidth(0.1f);

				// normal line comment me
				line.startColor = colorstart;
				line.endColor = colorend;
				line.startWidth = width;
				line.endWidth = width;

				if (numberOfPoints > 0)
				{
					// normal line comment me
					line.positionCount = numberOfPoints;
				}

				// set points of Hermite curve
				p0 = start.transform.position;
				p1 = end.transform.position;

				// checking tangent
				m0 = forwardleft;
				m1 = forwardright;



				for(int i = 0; i < numberOfPoints; i++)
				{
					t = i / (numberOfPoints - 1.0f);
					position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 
						+ (t * t * t - 2.0f * t * t + t) * m0 
						+ (-2.0f * t * t * t + 3.0f * t * t) * p1 
						+ (t * t * t - t * t) * m1;

					// normal line comment me
					line.SetPosition(i, position);
				}
				break;
			case 2:
				break;
			case 3:
				switch (materialnum) {
				case 1:
					currLine.material = a;
					break;
				case 2:
					currLine.material = b;
					break;
				case 3:
					currLine.material = c;
					break;
				}
				currLine.SetVertexCount (numClicks + 1);
				Vector3 pos = trackedObjRight.transform.position;
				currLine.SetPosition (numClicks, pos);
				numClicks++;
				break;
			}

		}
	}
}
