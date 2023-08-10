using UnityEngine;

public class ObjCustomization : MonoBehaviour
{
	public static bool Customization;

	public SwipeControlCustomization swipeCtrl;

	public Transform[] obj = (Transform[])(object)new Transform[0];

	public float minXPos;

	public float maxXPos = 115f;

	private float xDist;

	private float xDistFactor;

	public static int HatsNumber = 8;

	public static int ShirtsNumber = 8;

	public static int BackBacksNumber = 8;

	private float swipeSmoothFactor = 1f;

	public float xPosReal = -11f;

	private float rememberYPos;

	private void Awake()
	{
		Customization = false;
		if (Application.loadedLevel != 1)
		{
			minXPos -= 94.2f;
			maxXPos -= 94.2f;
			xPosReal = -33.5f;
		}
	}

	private void Start()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		xDist = maxXPos - minXPos;
		xDistFactor = 1f / xDist;
		if (!Object.op_Implicit((Object)(object)swipeCtrl))
		{
			swipeCtrl = ((Component)this).gameObject.AddComponent<SwipeControlCustomization>();
		}
		swipeCtrl.skipAutoSetup = true;
		swipeCtrl.clickEdgeToSwitch = false;
		swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height));
		swipeCtrl.maxValue = obj.Length - 1;
		swipeCtrl.currentValue = 0;
		swipeCtrl.startValue = 0;
		swipeCtrl.partWidth = Screen.width / swipeCtrl.maxValue;
		swipeCtrl.Setup();
		swipeSmoothFactor = 1f / (float)swipeCtrl.maxValue;
		rememberYPos = obj[0].position.y;
	}

	private void Update()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (!Customization)
		{
			return;
		}
		for (int i = 0; i < obj.Length; i++)
		{
			obj[i].position = new Vector3(xPosReal - Mathf.Clamp(Mathf.Abs((float)i - swipeCtrl.smoothValue), 0f, 1f), minXPos - (float)i * (xDist * swipeSmoothFactor) + swipeCtrl.smoothValue * swipeSmoothFactor * xDist, obj[i].position.z);
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				ShopManagerFull.AktivanItemSesir = swipeCtrl.currentValue;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				ShopManagerFull.AktivanItemMajica = swipeCtrl.currentValue;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				ShopManagerFull.AktivanItemRanac = swipeCtrl.currentValue;
			}
		}
	}
}
