using UnityEngine;

public class EasyTouchInput
{
	private Vector2[] oldMousePosition = (Vector2[])(object)new Vector2[2];

	private int[] tapCount = new int[2];

	private float[] startActionTime = new float[2];

	private float[] deltaTime = new float[2];

	private float[] tapeTime = new float[2];

	private bool bComplex;

	private Vector2 deltaFingerPosition;

	private Vector2 oldFinger2Position;

	private Vector2 complexCenter;

	public int TouchCount()
	{
		return getTouchCount(realTouch: false);
	}

	private int getTouchCount(bool realTouch)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		if (realTouch || EasyTouch.instance.enableRemote)
		{
			result = Input.touchCount;
		}
		else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
		{
			result = 1;
			if (Input.GetKey((KeyCode)308) || Input.GetKey(EasyTouch.instance.twistKey) || Input.GetKey((KeyCode)306) || Input.GetKey(EasyTouch.instance.swipeKey))
			{
				result = 2;
			}
			if (Input.GetKeyUp((KeyCode)308) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp((KeyCode)306) || Input.GetKeyUp(EasyTouch.instance.swipeKey))
			{
				result = 2;
			}
		}
		return result;
	}

	public Finger GetMouseTouch(int fingerIndex, Finger myFinger)
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		Finger finger;
		if (myFinger != null)
		{
			finger = myFinger;
		}
		else
		{
			finger = new Finger();
			finger.gesture = EasyTouch.GestureType.None;
		}
		if (fingerIndex == 1 && (Input.GetKeyUp((KeyCode)308) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp((KeyCode)306) || Input.GetKeyUp(EasyTouch.instance.swipeKey)))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = oldFinger2Position;
			finger.deltaPosition = finger.position - oldFinger2Position;
			finger.tapCount = tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - deltaTime[fingerIndex];
			finger.phase = (TouchPhase)3;
			return finger;
		}
		if (Input.GetMouseButton(0))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = GetPointerPosition(fingerIndex);
			if ((double)(Time.realtimeSinceStartup - tapeTime[fingerIndex]) > 0.5)
			{
				tapCount[fingerIndex] = 0;
			}
			if (Input.GetMouseButtonDown(0) || (fingerIndex == 1 && (Input.GetKeyDown((KeyCode)308) || Input.GetKeyDown(EasyTouch.instance.twistKey) || Input.GetKeyDown((KeyCode)306) || Input.GetKeyDown(EasyTouch.instance.swipeKey))))
			{
				finger.position = GetPointerPosition(fingerIndex);
				finger.deltaPosition = Vector2.zero;
				tapCount[fingerIndex]++;
				finger.tapCount = tapCount[fingerIndex];
				startActionTime[fingerIndex] = Time.realtimeSinceStartup;
				deltaTime[fingerIndex] = startActionTime[fingerIndex];
				finger.deltaTime = 0f;
				finger.phase = (TouchPhase)0;
				if (fingerIndex == 1)
				{
					oldFinger2Position = finger.position;
				}
				else
				{
					oldMousePosition[fingerIndex] = finger.position;
				}
				if (tapCount[fingerIndex] == 1)
				{
					tapeTime[fingerIndex] = Time.realtimeSinceStartup;
				}
				return finger;
			}
			finger.deltaPosition = finger.position - oldMousePosition[fingerIndex];
			finger.tapCount = tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - deltaTime[fingerIndex];
			if (((Vector2)(ref finger.deltaPosition)).sqrMagnitude < 1f)
			{
				finger.phase = (TouchPhase)2;
			}
			else
			{
				finger.phase = (TouchPhase)1;
			}
			oldMousePosition[fingerIndex] = finger.position;
			deltaTime[fingerIndex] = Time.realtimeSinceStartup;
			return finger;
		}
		if (Input.GetMouseButtonUp(0))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = GetPointerPosition(fingerIndex);
			finger.deltaPosition = finger.position - oldMousePosition[fingerIndex];
			finger.tapCount = tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - deltaTime[fingerIndex];
			finger.phase = (TouchPhase)3;
			oldMousePosition[fingerIndex] = finger.position;
			return finger;
		}
		return null;
	}

	public Vector2 GetSecondFingerPosition()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 result = default(Vector2);
		((Vector2)(ref result))._002Ector(-1f, -1f);
		if ((Input.GetKey((KeyCode)308) || Input.GetKey(EasyTouch.instance.twistKey)) && (Input.GetKey((KeyCode)306) || Input.GetKey(EasyTouch.instance.swipeKey)))
		{
			if (!bComplex)
			{
				bComplex = true;
				deltaFingerPosition = Vector2.op_Implicit(Input.mousePosition) - oldFinger2Position;
			}
			result = GetComplex2finger();
			return result;
		}
		if (Input.GetKey((KeyCode)308) || Input.GetKey(EasyTouch.instance.twistKey))
		{
			result = GetPinchTwist2Finger();
			bComplex = false;
			return result;
		}
		if (Input.GetKey((KeyCode)306) || Input.GetKey(EasyTouch.instance.swipeKey))
		{
			result = GetComplex2finger();
			bComplex = false;
			return result;
		}
		return result;
	}

	private Vector2 GetPointerPosition(int index)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (index == 0)
		{
			return Vector2.op_Implicit(Input.mousePosition);
		}
		return GetSecondFingerPosition();
	}

	private Vector2 GetPinchTwist2Finger()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 result = default(Vector2);
		if (complexCenter == Vector2.zero)
		{
			result.x = (float)Screen.width / 2f - (Input.mousePosition.x - (float)Screen.width / 2f);
			result.y = (float)Screen.height / 2f - (Input.mousePosition.y - (float)Screen.height / 2f);
		}
		else
		{
			result.x = complexCenter.x - (Input.mousePosition.x - complexCenter.x);
			result.y = complexCenter.y - (Input.mousePosition.y - complexCenter.y);
		}
		oldFinger2Position = result;
		return result;
	}

	private Vector2 GetComplex2finger()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = default(Vector2);
		val.x = Input.mousePosition.x - deltaFingerPosition.x;
		val.y = Input.mousePosition.y - deltaFingerPosition.y;
		complexCenter = new Vector2((Input.mousePosition.x + val.x) / 2f, (Input.mousePosition.y + val.y) / 2f);
		oldFinger2Position = val;
		return val;
	}
}
