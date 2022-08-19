using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Shader Quality")]
public class ShaderQuality : MonoBehaviour
{
	// Token: 0x06000466 RID: 1126 RVA: 0x000184FC File Offset: 0x000166FC
	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (this.mCurrent != num)
		{
			this.mCurrent = num;
			Shader.globalMaximumLOD = this.mCurrent;
		}
	}

	// Token: 0x04000292 RID: 658
	private int mCurrent = 600;
}
