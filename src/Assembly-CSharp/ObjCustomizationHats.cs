using System;
using UnityEngine;

// Token: 0x02000755 RID: 1877
public class ObjCustomizationHats : MonoBehaviour
{
	// Token: 0x06002FC8 RID: 12232 RVA: 0x00023700 File Offset: 0x00021900
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

	// Token: 0x06002FC9 RID: 12233 RVA: 0x0017E1A4 File Offset: 0x0017C3A4
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

	// Token: 0x06002FCA RID: 12234 RVA: 0x0017E2C8 File Offset: 0x0017C4C8
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

	// Token: 0x04002B09 RID: 11017
	public static bool CustomizationHats;

	// Token: 0x04002B0A RID: 11018
	public static SwipeControlCustomizationHats swipeCtrl;

	// Token: 0x04002B0B RID: 11019
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B0C RID: 11020
	public static ObjCustomizationHats ObjCustomizationInstance;

	// Token: 0x04002B0D RID: 11021
	public float minXPos;

	// Token: 0x04002B0E RID: 11022
	public float maxXPos = 115f;

	// Token: 0x04002B0F RID: 11023
	private float xDist;

	// Token: 0x04002B10 RID: 11024
	private float xDistFactor;

	// Token: 0x04002B11 RID: 11025
	public static int HatsNumber = 8;

	// Token: 0x04002B12 RID: 11026
	public static int ShirtsNumber = 8;

	// Token: 0x04002B13 RID: 11027
	public static int BackBacksNumber = 8;

	// Token: 0x04002B14 RID: 11028
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B15 RID: 11029
	public float xPosReal = -11f;

	// Token: 0x04002B16 RID: 11030
	private float rememberYPos;
}
