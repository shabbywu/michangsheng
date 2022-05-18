using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Shader Quality")]
public class ShaderQuality : MonoBehaviour
{
	// Token: 0x060004B4 RID: 1204 RVA: 0x0006F8A8 File Offset: 0x0006DAA8
	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (this.mCurrent != num)
		{
			this.mCurrent = num;
			Shader.globalMaximumLOD = this.mCurrent;
		}
	}

	// Token: 0x04000302 RID: 770
	private int mCurrent = 600;
}
