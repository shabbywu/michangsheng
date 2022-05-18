using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class LeanTester : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x000043CA File Offset: 0x000025CA
	public void Start()
	{
		base.StartCoroutine(this.timeoutCheck());
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000043D9 File Offset: 0x000025D9
	private IEnumerator timeoutCheck()
	{
		float pauseEndTime = Time.realtimeSinceStartup + this.timeout;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		if (!LeanTest.testsFinished)
		{
			Debug.Log(LeanTest.formatB("Tests timed out!"));
			LeanTest.overview();
		}
		yield break;
	}

	// Token: 0x0400006F RID: 111
	public float timeout = 15f;
}
