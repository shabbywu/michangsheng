using System;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
public class ObjLeaderboard : MonoBehaviour
{
	// Token: 0x06002871 RID: 10353 RVA: 0x00132390 File Offset: 0x00130590
	private void Awake()
	{
		ObjLeaderboard.Leaderboard = false;
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x00132398 File Offset: 0x00130598
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!ObjLeaderboard.swipeCtrl)
		{
			ObjLeaderboard.swipeCtrl = base.gameObject.AddComponent<SwipeControlLeaderboard>();
		}
		ObjLeaderboard.swipeCtrl.skipAutoSetup = true;
		ObjLeaderboard.swipeCtrl.clickEdgeToSwitch = false;
		ObjLeaderboard.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		ObjLeaderboard.swipeCtrl.maxValue = this.obj.Length - 1;
		ObjLeaderboard.swipeCtrl.currentValue = this.obj.Length - 2;
		ObjLeaderboard.swipeCtrl.startValue = this.obj.Length - 2;
		ObjLeaderboard.swipeCtrl.partWidth = (float)(Screen.width / ObjLeaderboard.swipeCtrl.maxValue);
		ObjLeaderboard.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)ObjLeaderboard.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x001324B4 File Offset: 0x001306B4
	private void Update()
	{
		if (ObjLeaderboard.Leaderboard)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - ObjLeaderboard.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x040023AA RID: 9130
	public static bool Leaderboard;

	// Token: 0x040023AB RID: 9131
	public static SwipeControlLeaderboard swipeCtrl;

	// Token: 0x040023AC RID: 9132
	public Transform[] obj = new Transform[0];

	// Token: 0x040023AD RID: 9133
	public float minXPos;

	// Token: 0x040023AE RID: 9134
	public float maxXPos = 55f;

	// Token: 0x040023AF RID: 9135
	private float xDist;

	// Token: 0x040023B0 RID: 9136
	private float xDistFactor;

	// Token: 0x040023B1 RID: 9137
	private float swipeSmoothFactor = 1f;

	// Token: 0x040023B2 RID: 9138
	private float rememberYPos;
}
