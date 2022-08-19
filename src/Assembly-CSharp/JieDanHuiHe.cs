using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200045E RID: 1118
public class JieDanHuiHe : MonoBehaviour
{
	// Token: 0x06002311 RID: 8977 RVA: 0x000EF99C File Offset: 0x000EDB9C
	private void Start()
	{
		this.jieDanBuff = new List<int>
		{
			4013,
			4012,
			4011
		};
		this.jieDanBuffName = new List<string>();
		foreach (int key in this.jieDanBuff)
		{
			this.jieDanBuffName.Add(_BuffJsonData.DataDict[key].name);
		}
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x000EFA3C File Offset: 0x000EDC3C
	private void Update()
	{
		Avatar player = PlayerEx.Player;
		for (int i = 0; i < this.jieDanBuff.Count; i++)
		{
			if (player.buffmag.HasBuff(this.jieDanBuff[i]))
			{
				this.text.text = this.jieDanBuffName[i];
				return;
			}
		}
	}

	// Token: 0x04001C41 RID: 7233
	public Text text;

	// Token: 0x04001C42 RID: 7234
	private List<int> jieDanBuff;

	// Token: 0x04001C43 RID: 7235
	private List<string> jieDanBuffName;
}
