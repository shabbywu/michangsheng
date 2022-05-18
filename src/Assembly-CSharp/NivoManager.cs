using System;
using UnityEngine;

// Token: 0x02000732 RID: 1842
public class NivoManager : MonoBehaviour
{
	// Token: 0x06002EB5 RID: 11957 RVA: 0x00022ADD File Offset: 0x00020CDD
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x040029D5 RID: 10709
	public int currentLevel;
}
