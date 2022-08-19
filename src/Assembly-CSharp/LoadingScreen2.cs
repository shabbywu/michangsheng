using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004BC RID: 1212
public class LoadingScreen2 : MonoBehaviour
{
	// Token: 0x06002655 RID: 9813 RVA: 0x0010A542 File Offset: 0x00108742
	private void Start()
	{
		base.StartCoroutine(this.LoadNextLevel());
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x0010A551 File Offset: 0x00108751
	private IEnumerator LoadNextLevel()
	{
		GC.Collect();
		Resources.UnloadUnusedAssets();
		yield return new WaitForSeconds(3f);
		Application.LoadLevelAsync(1);
		yield break;
	}
}
