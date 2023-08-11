using UnityEngine;

public class ObjCustomizationHats : MonoBehaviour
{
	public static bool CustomizationHats;

	public static SwipeControlCustomizationHats swipeCtrl;

	public Transform[] obj = (Transform[])(object)new Transform[0];

	public static ObjCustomizationHats ObjCustomizationInstance;

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
		CustomizationHats = false;
		if (Application.loadedLevel != 1)
		{
			minXPos -= 94.2f;
			maxXPos -= 94.2f;
			xPosReal = -33.5f;
		}
	}

	private void Start()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		ObjCustomizationInstance = this;
		xDist = maxXPos - minXPos;
		xDistFactor = 1f / xDist;
		if (!Object.op_Implicit((Object)(object)swipeCtrl))
		{
			swipeCtrl = ((Component)this).gameObject.AddComponent<SwipeControlCustomizationHats>();
		}
		swipeCtrl.skipAutoSetup = true;
		swipeCtrl.clickEdgeToSwitch = false;
		swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height));
		swipeCtrl.maxValue = obj.Length - 1;
		swipeCtrl.currentValue = obj.Length - 1;
		swipeCtrl.startValue = obj.Length - 1;
		swipeCtrl.partWidth = Screen.width / swipeCtrl.maxValue;
		swipeCtrl.Setup();
		swipeSmoothFactor = 1f / (float)swipeCtrl.maxValue;
		rememberYPos = obj[0].position.y;
	}

	private void Update()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (!CustomizationHats)
		{
			return;
		}
		for (int i = 0; i < obj.Length; i++)
		{
			obj[i].position = new Vector3(xPosReal, minXPos - (float)i * (xDist * swipeSmoothFactor) - swipeCtrl.smoothValue * swipeSmoothFactor * xDist, obj[i].position.z);
			if (ShopManagerFull.AktivanCustomizationTab == 1 && ShopManagerFull.AktivanItemSesir != swipeCtrl.maxValue - swipeCtrl.currentValue)
			{
				ShopManagerFull.AktivanItemSesir = swipeCtrl.maxValue - swipeCtrl.currentValue;
				ShopManagerFull.ShopObject.PreviewItem();
			}
		}
	}
}
