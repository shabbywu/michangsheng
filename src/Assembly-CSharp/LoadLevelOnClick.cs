using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x060004A5 RID: 1189 RVA: 0x00008126 File Offset: 0x00006326
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			Application.LoadLevel(this.levelName);
		}
	}

	// Token: 0x040002F2 RID: 754
	public string levelName;
}
