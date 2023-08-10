using System.Collections;
using UnityEngine;

public class SwipeControlSettingsTabs : MonoBehaviour
{
	public static bool controlEnabled;

	public bool skipAutoSetup;

	public bool allowInput = true;

	public bool clickEdgeToSwitch = true;

	public float partWidth;

	private float partFactor = 1f;

	public int startValue;

	public int currentValue;

	public int maxValue;

	public Rect mouseRect;

	public Rect leftEdgeRectForClickSwitch;

	public Rect rightEdgeRectForClickSwitch;

	public Matrix4x4 matrix = Matrix4x4.identity;

	private bool touched;

	private int[] fingerStartArea = new int[5];

	private int mouseStartArea;

	public float smoothValue;

	private float smoothStartPos;

	private float smoothDragOffset = 0.2f;

	private float lastSmoothValue;

	private float[] prevSmoothValue = new float[5];

	private float realtimeStamp;

	private float xVelocity;

	public float maxSpeed = 20f;

	private Vector2 mStartPos;

	private Vector3 pos;

	private Vector2 tPos;

	public bool debug;

	private IEnumerator Start()
	{
		if (clickEdgeToSwitch && !allowInput)
		{
			Debug.LogWarning((object)"You have enabled clickEdgeToSwitch, but it will not work because allowInput is disabled!", (Object)(object)this);
		}
		yield return (object)new WaitForSeconds(0.2f);
		if (!skipAutoSetup)
		{
			Setup();
		}
	}

	public void Setup()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		partFactor = 1f / partWidth;
		smoothValue = currentValue;
		currentValue = startValue;
		if (mouseRect != new Rect(0f, 0f, 0f, 0f))
		{
			SetMouseRect(mouseRect);
		}
		if (leftEdgeRectForClickSwitch == new Rect(0f, 0f, 0f, 0f))
		{
			CalculateEdgeRectsFromMouseRect();
		}
		if (matrix == Matrix4x4.zero)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			matrix = ((Matrix4x4)(ref identity)).inverse;
		}
	}

	public void SetMouseRect(Rect myRect)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		mouseRect = myRect;
	}

	public void CalculateEdgeRectsFromMouseRect()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CalculateEdgeRectsFromMouseRect(mouseRect);
	}

	public void CalculateEdgeRectsFromMouseRect(Rect myRect)
	{
		((Rect)(ref leftEdgeRectForClickSwitch)).x = ((Rect)(ref myRect)).x;
		((Rect)(ref leftEdgeRectForClickSwitch)).y = ((Rect)(ref myRect)).y;
		((Rect)(ref leftEdgeRectForClickSwitch)).width = ((Rect)(ref myRect)).width * 0.5f;
		((Rect)(ref leftEdgeRectForClickSwitch)).height = ((Rect)(ref myRect)).height;
		((Rect)(ref rightEdgeRectForClickSwitch)).x = ((Rect)(ref myRect)).x + ((Rect)(ref myRect)).width * 0.5f;
		((Rect)(ref rightEdgeRectForClickSwitch)).y = ((Rect)(ref myRect)).y;
		((Rect)(ref rightEdgeRectForClickSwitch)).width = ((Rect)(ref myRect)).width * 0.5f;
		((Rect)(ref rightEdgeRectForClickSwitch)).height = ((Rect)(ref myRect)).height;
	}

	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		leftEdgeRectForClickSwitch = leftRect;
		rightEdgeRectForClickSwitch = rightRect;
	}

	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	private void Update()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		if (!controlEnabled)
		{
			return;
		}
		touched = false;
		if (allowInput && (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
		{
			Vector3 mousePosition = Input.mousePosition;
			float num = ((Vector3)(ref mousePosition))[0];
			mousePosition = Input.mousePosition;
			pos = new Vector3(num, ((Vector3)(ref mousePosition))[1], 0f);
			Matrix4x4 inverse = ((Matrix4x4)(ref matrix)).inverse;
			tPos = Vector2.op_Implicit(((Matrix4x4)(ref inverse)).MultiplyPoint3x4(pos));
			if (Input.GetMouseButtonDown(0) && ((Rect)(ref mouseRect)).Contains(tPos))
			{
				mouseStartArea = 1;
			}
			if (mouseStartArea == 1)
			{
				touched = true;
				if (Input.GetMouseButtonDown(0))
				{
					mStartPos = tPos;
					smoothStartPos = smoothValue + tPos.y * partFactor;
					FillArrayWithValue(prevSmoothValue, smoothValue);
				}
				smoothValue = smoothStartPos - tPos.y * partFactor;
				if (smoothValue < -0.12f)
				{
					smoothValue = -0.12f;
				}
				else if (smoothValue > (float)maxValue + 0.12f)
				{
					smoothValue = (float)maxValue + 0.12f;
				}
				if (Input.GetMouseButtonUp(0))
				{
					Vector2 val = tPos - mStartPos;
					if (((Vector2)(ref val)).sqrMagnitude < 25f)
					{
						if (clickEdgeToSwitch)
						{
							if (((Rect)(ref leftEdgeRectForClickSwitch)).Contains(tPos))
							{
								currentValue--;
								if (currentValue < 0)
								{
									currentValue = 0;
								}
							}
							else if (((Rect)(ref rightEdgeRectForClickSwitch)).Contains(tPos))
							{
								currentValue++;
								if (currentValue > maxValue)
								{
									currentValue = maxValue;
								}
							}
						}
					}
					else if ((float)currentValue - (smoothValue + (smoothValue - GetAvgValue(prevSmoothValue))) > smoothDragOffset || (float)currentValue - (smoothValue + (smoothValue - GetAvgValue(prevSmoothValue))) < 0f - smoothDragOffset)
					{
						currentValue = (int)Mathf.Round(smoothValue + (smoothValue - GetAvgValue(prevSmoothValue)));
						xVelocity = smoothValue - GetAvgValue(prevSmoothValue);
						if (currentValue > maxValue)
						{
							currentValue = maxValue;
						}
						else if (currentValue < 0)
						{
							currentValue = 0;
						}
					}
					mouseStartArea = 0;
				}
				for (int i = 1; i < prevSmoothValue.Length; i++)
				{
					prevSmoothValue[i] = prevSmoothValue[i - 1];
				}
				prevSmoothValue[0] = smoothValue;
			}
		}
		if (!touched)
		{
			smoothValue = Mathf.SmoothDamp(smoothValue, (float)currentValue, ref xVelocity, 0.3f, maxSpeed, Time.realtimeSinceStartup - realtimeStamp);
		}
		realtimeStamp = Time.realtimeSinceStartup;
	}
}
