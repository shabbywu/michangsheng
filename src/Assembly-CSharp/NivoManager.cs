using System;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class NivoManager : MonoBehaviour
{
	// Token: 0x060027A7 RID: 10151 RVA: 0x00128CE2 File Offset: 0x00126EE2
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x04002287 RID: 8839
	public int currentLevel;
}
