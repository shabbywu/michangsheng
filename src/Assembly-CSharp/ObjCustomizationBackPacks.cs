using System;
using UnityEngine;

// Token: 0x020004DE RID: 1246
public class ObjCustomizationBackPacks : MonoBehaviour
{
	// Token: 0x0600285A RID: 10330 RVA: 0x001317FB File Offset: 0x0012F9FB
	private void Awake()
	{
		ObjCustomizationBackPacks.CustomizationBackPacks = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x0013183C File Offset: 0x0012FA3C
	private void Start()
	{
		ObjCustomizationBackPacks.ObjCustomizationInstance = this;
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!ObjCustomizationBackPacks.swipeCtrl)
		{
			ObjCustomizationBackPacks.swipeCtrl = base.gameObject.AddComponent<SwipeControlCustomizationBackPacks>();
		}
		ObjCustomizationBackPacks.swipeCtrl.skipAutoSetup = true;
		ObjCustomizationBackPacks.swipeCtrl.clickEdgeToSwitch = false;
		ObjCustomizationBackPacks.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height));
		ObjCustomizationBackPacks.swipeCtrl.maxValue = this.obj.Length - 1;
		ObjCustomizationBackPacks.swipeCtrl.currentValue = this.obj.Length - 1;
		ObjCustomizationBackPacks.swipeCtrl.startValue = this.obj.Length - 1;
		ObjCustomizationBackPacks.swipeCtrl.partWidth = (float)(Screen.width / ObjCustomizationBackPacks.swipeCtrl.maxValue);
		ObjCustomizationBackPacks.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)ObjCustomizationBackPacks.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x00131960 File Offset: 0x0012FB60
	private void Update()
	{
		if (ObjCustomizationBackPacks.CustomizationBackPacks)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.xPosReal, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - ObjCustomizationBackPacks.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
				if (ShopManagerFull.AktivanCustomizationTab == 3 && ShopManagerFull.AktivanItemRanac != ObjCustomizationBackPacks.swipeCtrl.maxValue - ObjCustomizationBackPacks.swipeCtrl.currentValue)
				{
					ShopManagerFull.AktivanItemRanac = ObjCustomizationBackPacks.swipeCtrl.maxValue - ObjCustomizationBackPacks.swipeCtrl.currentValue;
					ShopManagerFull.ShopObject.PreviewItem();
				}
			}
		}
	}

	// Token: 0x0400236D RID: 9069
	public static bool CustomizationBackPacks;

	// Token: 0x0400236E RID: 9070
	public static SwipeControlCustomizationBackPacks swipeCtrl;

	// Token: 0x0400236F RID: 9071
	public Transform[] obj = new Transform[0];

	// Token: 0x04002370 RID: 9072
	public static ObjCustomizationBackPacks ObjCustomizationInstance;

	// Token: 0x04002371 RID: 9073
	public float minXPos;

	// Token: 0x04002372 RID: 9074
	public float maxXPos = 115f;

	// Token: 0x04002373 RID: 9075
	private float xDist;

	// Token: 0x04002374 RID: 9076
	private float xDistFactor;

	// Token: 0x04002375 RID: 9077
	public static int HatsNumber = 8;

	// Token: 0x04002376 RID: 9078
	public static int ShirtsNumber = 8;

	// Token: 0x04002377 RID: 9079
	public static int BackBacksNumber = 8;

	// Token: 0x04002378 RID: 9080
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002379 RID: 9081
	public float xPosReal = -11f;

	// Token: 0x0400237A RID: 9082
	private float rememberYPos;
}
