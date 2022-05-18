using System;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public class PrepareScript : MonoBehaviour
{
	// Token: 0x06002F78 RID: 12152 RVA: 0x000232EB File Offset: 0x000214EB
	private void Awake()
	{
		if (SystemInfo.systemMemorySize <= 1024 && SystemInfo.processorCount == 1)
		{
			QualitySettings.masterTextureLimit = 1;
		}
		Application.targetFrameRate = 60;
	}

	// Token: 0x06002F79 RID: 12153 RVA: 0x0002330E File Offset: 0x0002150E
	private void Update()
	{
		if (StagesParser.stagesLoaded)
		{
			Application.LoadLevel(1);
		}
	}
}
