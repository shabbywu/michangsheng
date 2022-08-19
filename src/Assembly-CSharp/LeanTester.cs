using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class LeanTester : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x00004291 File Offset: 0x00002491
	public void Start()
	{
		base.StartCoroutine(this.timeoutCheck());
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000042A0 File Offset: 0x000024A0
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

	// Token: 0x04000064 RID: 100
	public float timeout = 15f;
}
