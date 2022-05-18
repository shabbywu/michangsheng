using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006B5 RID: 1717
public class LianaAnimationEvent : MonoBehaviour
{
	// Token: 0x06002AF2 RID: 10994 RVA: 0x00021394 File Offset: 0x0001F594
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	// Token: 0x06002AF3 RID: 10995 RVA: 0x000213AB File Offset: 0x0001F5AB
	public void OtkaciMajmuna()
	{
		base.StartCoroutine(this.SacekajIOtkaciMajmuna());
	}

	// Token: 0x06002AF4 RID: 10996 RVA: 0x000213BA File Offset: 0x0001F5BA
	private IEnumerator SacekajIOtkaciMajmuna()
	{
		yield return new WaitForSeconds(0.6f);
		this.player.OtkaciMajmuna();
		yield break;
	}

	// Token: 0x0400252E RID: 9518
	private MonkeyController2D player;

	// Token: 0x0400252F RID: 9519
	public Transform lijanaTarget;
}
