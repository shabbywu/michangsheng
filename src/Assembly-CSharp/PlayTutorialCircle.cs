using System;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class PlayTutorialCircle : MonoBehaviour
{
	// Token: 0x06001EA9 RID: 7849 RVA: 0x000D77D9 File Offset: 0x000D59D9
	private void Awake()
	{
		PlayTutorialCircle.Inst = this;
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x000D77E1 File Offset: 0x000D59E1
	public void SetShow(bool show)
	{
		this.BG.SetActive(show);
		this.Hand.SetActive(show);
	}

	// Token: 0x04001920 RID: 6432
	public static PlayTutorialCircle Inst;

	// Token: 0x04001921 RID: 6433
	public GameObject BG;

	// Token: 0x04001922 RID: 6434
	public GameObject Hand;
}
