using System;
using UnityEngine;

// Token: 0x020004DF RID: 1247
public class ObjCustomizationHats : MonoBehaviour
{
	// Token: 0x0600285F RID: 10335 RVA: 0x00131A7B File Offset: 0x0012FC7B
	private void Awake()
	{
		ObjCustomizationHats.CustomizationHats = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x00131ABC File Offset: 0x0012FCBC
	private void Start()
	{
		ObjCustomizationHats.ObjCustomizationInstance = this;
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!ObjCustomizationHats.swipeCtrl)
		{
			ObjCustomizationHats.swipeCtrl = base.gameObject.AddComponent<SwipeControlCustomizationHats>();
		}
		ObjCustomizationHats.swipeCtrl.skipAutoSetup = true;
		ObjCustomizationHats.swipeCtrl.clickEdgeToSwitch = false;
		ObjCustomizationHats.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height));
		ObjCustomizationHats.swipeCtrl.maxValue = this.obj.Length - 1;
		ObjCustomizationHats.swipeCtrl.currentValue = this.obj.Length - 1;
		ObjCustomizationHats.swipeCtrl.startValue = this.obj.Length - 1;
		ObjCustomizationHats.swipeCtrl.partWidth = (float)(Screen.width / ObjCustomizationHats.swipeCtrl.maxValue);
		ObjCustomizationHats.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)ObjCustomizationHats.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x00131BE0 File Offset: 0x0012FDE0
	private void Update()
	{
		if (ObjCustomizationHats.CustomizationHats)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.xPosReal, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - ObjCustomizationHats.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
				if (ShopManagerFull.AktivanCustomizationTab == 1 && ShopManagerFull.AktivanItemSesir != ObjCustomizationHats.swipeCtrl.maxValue - ObjCustomizationHats.swipeCtrl.currentValue)
				{
					ShopManagerFull.AktivanItemSesir = ObjCustomizationHats.swipeCtrl.maxValue - ObjCustomizationHats.swipeCtrl.currentValue;
					ShopManagerFull.ShopObject.PreviewItem();
				}
			}
		}
	}

	// Token: 0x0400237B RID: 9083
	public static bool CustomizationHats;

	// Token: 0x0400237C RID: 9084
	public static SwipeControlCustomizationHats swipeCtrl;

	// Token: 0x0400237D RID: 9085
	public Transform[] obj = new Transform[0];

	// Token: 0x0400237E RID: 9086
	public static ObjCustomizationHats ObjCustomizationInstance;

	// Token: 0x0400237F RID: 9087
	public float minXPos;

	// Token: 0x04002380 RID: 9088
	public float maxXPos = 115f;

	// Token: 0x04002381 RID: 9089
	private float xDist;

	// Token: 0x04002382 RID: 9090
	private float xDistFactor;

	// Token: 0x04002383 RID: 9091
	public static int HatsNumber = 8;

	// Token: 0x04002384 RID: 9092
	public static int ShirtsNumber = 8;

	// Token: 0x04002385 RID: 9093
	public static int BackBacksNumber = 8;

	// Token: 0x04002386 RID: 9094
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002387 RID: 9095
	public float xPosReal = -11f;

	// Token: 0x04002388 RID: 9096
	private float rememberYPos;
}
