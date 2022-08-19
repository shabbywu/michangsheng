using System;
using UnityEngine;

// Token: 0x020004E0 RID: 1248
public class ObjCustomizationShirts : MonoBehaviour
{
	// Token: 0x06002864 RID: 10340 RVA: 0x00131CFB File Offset: 0x0012FEFB
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

	// Token: 0x06002865 RID: 10341 RVA: 0x00131D3C File Offset: 0x0012FF3C
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

	// Token: 0x06002866 RID: 10342 RVA: 0x00131E60 File Offset: 0x00130060
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

	// Token: 0x04002389 RID: 9097
	public static bool CustomizationShirts;

	// Token: 0x0400238A RID: 9098
	public static SwipeControlCustomizationShirts swipeCtrl;

	// Token: 0x0400238B RID: 9099
	public Transform[] obj = new Transform[0];

	// Token: 0x0400238C RID: 9100
	public static ObjCustomizationShirts ObjCustomizationInstance;

	// Token: 0x0400238D RID: 9101
	public float minXPos;

	// Token: 0x0400238E RID: 9102
	public float maxXPos = 115f;

	// Token: 0x0400238F RID: 9103
	private float xDist;

	// Token: 0x04002390 RID: 9104
	private float xDistFactor;

	// Token: 0x04002391 RID: 9105
	public static int HatsNumber = 8;

	// Token: 0x04002392 RID: 9106
	public static int ShirtsNumber = 8;

	// Token: 0x04002393 RID: 9107
	public static int BackBacksNumber = 8;

	// Token: 0x04002394 RID: 9108
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002395 RID: 9109
	public float xPosReal = -11f;

	// Token: 0x04002396 RID: 9110
	private float rememberYPos;
}
