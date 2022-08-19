using System;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class PrepareScript : MonoBehaviour
{
	// Token: 0x06002821 RID: 10273 RVA: 0x001301FF File Offset: 0x0012E3FF
	private void Awake()
	{
		if (SystemInfo.systemMemorySize <= 1024 && SystemInfo.processorCount == 1)
		{
			QualitySettings.masterTextureLimit = 1;
		}
		Application.targetFrameRate = 60;
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x00130222 File Offset: 0x0012E422
	private void Update()
	{
		if (StagesParser.stagesLoaded)
		{
			Application.LoadLevel(1);
		}
	}
}
