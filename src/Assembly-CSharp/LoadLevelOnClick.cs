using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x06000457 RID: 1111 RVA: 0x00017F3D File Offset: 0x0001613D
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			Application.LoadLevel(this.levelName);
		}
	}

	// Token: 0x04000282 RID: 642
	public string levelName;
}
