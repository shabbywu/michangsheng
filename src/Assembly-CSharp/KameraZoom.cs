using UnityEngine;

public class KameraZoom : MonoBehaviour
{
	public int speed = 4;

	public Camera selectedCamera;

	public float MINSCALE = 2f;

	public float MAXSCALE = 5f;

	public float minPinchSpeed = 5f;

	public float varianceInDistances = 5f;

	private float touchDelta;

	private Vector2 prevDist = new Vector2(0f, 0f);

	private Vector2 curDist = new Vector2(0f, 0f);

	private float speedTouch0;

	private float speedTouch1;

	private void Start()
	{
	}

	private void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Invalid comparison between Unknown and I4
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		if (Input.touchCount != 2)
		{
			return;
		}
		Touch touch = Input.GetTouch(0);
		if ((int)((Touch)(ref touch)).phase != 1)
		{
			return;
		}
		touch = Input.GetTouch(1);
		if ((int)((Touch)(ref touch)).phase == 1)
		{
			touch = Input.GetTouch(0);
			Vector2 position = ((Touch)(ref touch)).position;
			touch = Input.GetTouch(1);
			curDist = position - ((Touch)(ref touch)).position;
			touch = Input.GetTouch(0);
			Vector2 position2 = ((Touch)(ref touch)).position;
			touch = Input.GetTouch(0);
			Vector2 val = position2 - ((Touch)(ref touch)).deltaPosition;
			touch = Input.GetTouch(1);
			Vector2 position3 = ((Touch)(ref touch)).position;
			touch = Input.GetTouch(1);
			prevDist = val - (position3 - ((Touch)(ref touch)).deltaPosition);
			touchDelta = ((Vector2)(ref curDist)).magnitude - ((Vector2)(ref prevDist)).magnitude;
			touch = Input.GetTouch(0);
			Vector2 deltaPosition = ((Touch)(ref touch)).deltaPosition;
			float magnitude = ((Vector2)(ref deltaPosition)).magnitude;
			touch = Input.GetTouch(0);
			speedTouch0 = magnitude / ((Touch)(ref touch)).deltaTime;
			touch = Input.GetTouch(1);
			deltaPosition = ((Touch)(ref touch)).deltaPosition;
			float magnitude2 = ((Vector2)(ref deltaPosition)).magnitude;
			touch = Input.GetTouch(1);
			speedTouch1 = magnitude2 / ((Touch)(ref touch)).deltaTime;
			if (touchDelta + varianceInDistances <= 1f && speedTouch0 > minPinchSpeed && speedTouch1 > minPinchSpeed)
			{
				selectedCamera.orthographicSize = Mathf.Clamp(selectedCamera.orthographicSize + (float)speed, 5f, 10f);
			}
			if (touchDelta + varianceInDistances > 1f && speedTouch0 > minPinchSpeed && speedTouch1 > minPinchSpeed)
			{
				selectedCamera.orthographicSize = Mathf.Clamp(selectedCamera.fieldOfView - (float)speed, 5f, 10f);
			}
		}
	}
}
