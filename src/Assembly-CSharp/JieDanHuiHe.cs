using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000618 RID: 1560
public class JieDanHuiHe : MonoBehaviour
{
	// Token: 0x060026C4 RID: 9924 RVA: 0x0012FEC8 File Offset: 0x0012E0C8
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

	// Token: 0x060026C5 RID: 9925 RVA: 0x0012FF68 File Offset: 0x0012E168
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

	// Token: 0x04002111 RID: 8465
	public Text text;

	// Token: 0x04002112 RID: 8466
	private List<int> jieDanBuff;

	// Token: 0x04002113 RID: 8467
	private List<string> jieDanBuffName;
}
