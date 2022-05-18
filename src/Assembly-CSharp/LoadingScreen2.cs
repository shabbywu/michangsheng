using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006BD RID: 1725
public class LoadingScreen2 : MonoBehaviour
{
	// Token: 0x06002B21 RID: 11041 RVA: 0x000214D8 File Offset: 0x0001F6D8
	private void Start()
	{
		base.StartCoroutine(this.LoadNextLevel());
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x000214E7 File Offset: 0x0001F6E7
	private IEnumerator LoadNextLevel()
	{
		GC.Collect();
		Resources.UnloadUnusedAssets();
		yield return new WaitForSeconds(3f);
		Application.LoadLevelAsync(1);
		yield break;
	}
}
