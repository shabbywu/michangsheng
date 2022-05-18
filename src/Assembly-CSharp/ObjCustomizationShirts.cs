using System;
using UnityEngine;

// Token: 0x02000756 RID: 1878
public class ObjCustomizationShirts : MonoBehaviour
{
	// Token: 0x06002FCD RID: 12237 RVA: 0x00023788 File Offset: 0x00021988
	private void Awake()
	{
		ObjCustomizationShirts.CustomizationShirts = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x06002FCE RID: 12238 RVA: 0x0017E39C File Offset: 0x0017C59C
	private void Start()
	{
		ObjCustomizationShirts.ObjCustomizationInstance = this;
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!ObjCustomizationShirts.swipeCtrl)
		{
			ObjCustomizationShirts.swipeCtrl = base.gameObject.AddComponent<SwipeControlCustomizationShirts>();
		}
		ObjCustomizationShirts.swipeCtrl.skipAutoSetup = true;
		ObjCustomizationShirts.swipeCtrl.clickEdgeToSwitch = false;
		ObjCustomizationShirts.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height));
		ObjCustomizationShirts.swipeCtrl.maxValue = this.obj.Length - 1;
		ObjCustomizationShirts.swipeCtrl.currentValue = this.obj.Length - 1;
		ObjCustomizationShirts.swipeCtrl.startValue = this.obj.Length - 1;
		ObjCustomizationShirts.swipeCtrl.partWidth = (float)(Screen.width / ObjCustomizationShirts.swipeCtrl.maxValue);
		ObjCustomizationShirts.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)ObjCustomizationShirts.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002FCF RID: 12239 RVA: 0x0017E4C0 File Offset: 0x0017C6C0
	private void Update()
	{
		if (ObjCustomizationShirts.CustomizationShirts)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.xPosReal, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - ObjCustomizationShirts.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
				if (ShopManagerFull.AktivanCustomizationTab == 2 && ShopManagerFull.AktivanItemMajica != ObjCustomizationShirts.swipeCtrl.maxValue - ObjCustomizationShirts.swipeCtrl.currentValue)
				{
					ShopManagerFull.AktivanItemMajica = ObjCustomizationShirts.swipeCtrl.maxValue - ObjCustomizationShirts.swipeCtrl.currentValue;
					ShopManagerFull.ShopObject.PreviewItem();
				}
			}
		}
	}

	// Token: 0x04002B17 RID: 11031
	public static bool CustomizationShirts;

	// Token: 0x04002B18 RID: 11032
	public static SwipeControlCustomizationShirts swipeCtrl;

	// Token: 0x04002B19 RID: 11033
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B1A RID: 11034
	public static ObjCustomizationShirts ObjCustomizationInstance;

	// Token: 0x04002B1B RID: 11035
	public float minXPos;

	// Token: 0x04002B1C RID: 11036
	public float maxXPos = 115f;

	// Token: 0x04002B1D RID: 11037
	private float xDist;

	// Token: 0x04002B1E RID: 11038
	private float xDistFactor;

	// Token: 0x04002B1F RID: 11039
	public static int HatsNumber = 8;

	// Token: 0x04002B20 RID: 11040
	public static int ShirtsNumber = 8;

	// Token: 0x04002B21 RID: 11041
	public static int BackBacksNumber = 8;

	// Token: 0x04002B22 RID: 11042
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B23 RID: 11043
	public float xPosReal = -11f;

	// Token: 0x04002B24 RID: 11044
	private float rememberYPos;
}
