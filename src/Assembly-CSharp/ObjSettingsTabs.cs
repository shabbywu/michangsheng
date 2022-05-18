using System;
using UnityEngine;

// Token: 0x0200075B RID: 1883
public class ObjSettingsTabs : MonoBehaviour
{
	// Token: 0x06002FE2 RID: 12258 RVA: 0x0002395C File Offset: 0x00021B5C
	private void Awake()
	{
		ObjSettingsTabs.SettingsTabs = false;
	}

	// Token: 0x06002FE3 RID: 12259 RVA: 0x0017EC68 File Offset: 0x0017CE68
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!this.swipeCtrl)
		{
			this.swipeCtrl = base.gameObject.AddComponent<SwipeControlSettingsTabs>();
		}
		this.swipeCtrl.skipAutoSetup = true;
		this.swipeCtrl.clickEdgeToSwitch = false;
		this.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		this.swipeCtrl.maxValue = this.obj.Length - 1;
		this.swipeCtrl.currentValue = this.obj.Length - 2;
		this.swipeCtrl.startValue = this.obj.Length - 2;
		this.swipeCtrl.partWidth = (float)(Screen.width / this.swipeCtrl.maxValue);
		this.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)this.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002FE4 RID: 12260 RVA: 0x0017ED90 File Offset: 0x0017CF90
	private void Update()
	{
		if (ObjSettingsTabs.SettingsTabs)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - this.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x04002B4B RID: 11083
	public static bool SettingsTabs;

	// Token: 0x04002B4C RID: 11084
	public SwipeControlSettingsTabs swipeCtrl;

	// Token: 0x04002B4D RID: 11085
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B4E RID: 11086
	public float minXPos;

	// Token: 0x04002B4F RID: 11087
	public float maxXPos = 40f;

	// Token: 0x04002B50 RID: 11088
	private float xDist;

	// Token: 0x04002B51 RID: 11089
	private float xDistFactor;

	// Token: 0x04002B52 RID: 11090
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B53 RID: 11091
	private float rememberYPos;
}
