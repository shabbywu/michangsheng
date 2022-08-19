using System;
using UnityEngine;

// Token: 0x020004E5 RID: 1253
public class ObjSettingsTabs : MonoBehaviour
{
	// Token: 0x06002879 RID: 10361 RVA: 0x00132797 File Offset: 0x00130997
	private void Awake()
	{
		ObjSettingsTabs.SettingsTabs = false;
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x001327A0 File Offset: 0x001309A0
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

	// Token: 0x0600287B RID: 10363 RVA: 0x001328C8 File Offset: 0x00130AC8
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

	// Token: 0x040023BD RID: 9149
	public static bool SettingsTabs;

	// Token: 0x040023BE RID: 9150
	public SwipeControlSettingsTabs swipeCtrl;

	// Token: 0x040023BF RID: 9151
	public Transform[] obj = new Transform[0];

	// Token: 0x040023C0 RID: 9152
	public float minXPos;

	// Token: 0x040023C1 RID: 9153
	public float maxXPos = 40f;

	// Token: 0x040023C2 RID: 9154
	private float xDist;

	// Token: 0x040023C3 RID: 9155
	private float xDistFactor;

	// Token: 0x040023C4 RID: 9156
	private float swipeSmoothFactor = 1f;

	// Token: 0x040023C5 RID: 9157
	private float rememberYPos;
}
