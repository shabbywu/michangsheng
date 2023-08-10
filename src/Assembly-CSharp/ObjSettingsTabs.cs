using UnityEngine;

public class ObjSettingsTabs : MonoBehaviour
{
	public static bool SettingsTabs;

	public SwipeControlSettingsTabs swipeCtrl;

	public Transform[] obj = (Transform[])(object)new Transform[0];

	public float minXPos;

	public float maxXPos = 40f;

	private float xDist;

	private float xDistFactor;

	private float swipeSmoothFactor = 1f;

	private float rememberYPos;

	private void Awake()
	{
		SettingsTabs = false;
	}

	private void Start()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		xDist = maxXPos - minXPos;
		xDistFactor = 1f / xDist;
		if (!Object.op_Implicit((Object)(object)swipeCtrl))
		{
			swipeCtrl = ((Component)this).gameObject.AddComponent<SwipeControlSettingsTabs>();
		}
		swipeCtrl.skipAutoSetup = true;
		swipeCtrl.clickEdgeToSwitch = false;
		swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		swipeCtrl.maxValue = obj.Length - 1;
		swipeCtrl.currentValue = obj.Length - 2;
		swipeCtrl.startValue = obj.Length - 2;
		swipeCtrl.partWidth = Screen.width / swipeCtrl.maxValue;
		swipeCtrl.Setup();
		swipeSmoothFactor = 1f / (float)swipeCtrl.maxValue;
		rememberYPos = obj[0].position.y;
	}

	private void Update()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (SettingsTabs)
		{
			for (int i = 0; i < obj.Length; i++)
			{
				obj[i].position = new Vector3(obj[i].position.x, minXPos - (float)i * (xDist * swipeSmoothFactor) - swipeCtrl.smoothValue * swipeSmoothFactor * xDist, obj[i].position.z);
			}
		}
	}
}
