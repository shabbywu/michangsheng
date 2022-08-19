using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
public class LianaAnimationEvent : MonoBehaviour
{
	// Token: 0x06002644 RID: 9796 RVA: 0x00109FD4 File Offset: 0x001081D4
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x00109FEB File Offset: 0x001081EB
	public void OtkaciMajmuna()
	{
		base.StartCoroutine(this.SacekajIOtkaciMajmuna());
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x00109FFA File Offset: 0x001081FA
	private IEnumerator SacekajIOtkaciMajmuna()
	{
		yield return new WaitForSeconds(0.6f);
		this.player.OtkaciMajmuna();
		yield break;
	}

	// Token: 0x04001F8C RID: 8076
	private MonkeyController2D player;

	// Token: 0x04001F8D RID: 8077
	public Transform lijanaTarget;
}
