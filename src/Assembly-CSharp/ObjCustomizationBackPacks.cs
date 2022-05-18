using System;
using UnityEngine;

// Token: 0x02000754 RID: 1876
public class ObjCustomizationBackPacks : MonoBehaviour
{
	// Token: 0x06002FC3 RID: 12227 RVA: 0x00023678 File Offset: 0x00021878
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

	// Token: 0x06002FC4 RID: 12228 RVA: 0x0017DFAC File Offset: 0x0017C1AC
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

	// Token: 0x06002FC5 RID: 12229 RVA: 0x0017E0D0 File Offset: 0x0017C2D0
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

	// Token: 0x04002AFB RID: 11003
	public static bool CustomizationBackPacks;

	// Token: 0x04002AFC RID: 11004
	public static SwipeControlCustomizationBackPacks swipeCtrl;

	// Token: 0x04002AFD RID: 11005
	public Transform[] obj = new Transform[0];

	// Token: 0x04002AFE RID: 11006
	public static ObjCustomizationBackPacks ObjCustomizationInstance;

	// Token: 0x04002AFF RID: 11007
	public float minXPos;

	// Token: 0x04002B00 RID: 11008
	public float maxXPos = 115f;

	// Token: 0x04002B01 RID: 11009
	private float xDist;

	// Token: 0x04002B02 RID: 11010
	private float xDistFactor;

	// Token: 0x04002B03 RID: 11011
	public static int HatsNumber = 8;

	// Token: 0x04002B04 RID: 11012
	public static int ShirtsNumber = 8;

	// Token: 0x04002B05 RID: 11013
	public static int BackBacksNumber = 8;

	// Token: 0x04002B06 RID: 11014
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B07 RID: 11015
	public float xPosReal = -11f;

	// Token: 0x04002B08 RID: 11016
	private float rememberYPos;
}
