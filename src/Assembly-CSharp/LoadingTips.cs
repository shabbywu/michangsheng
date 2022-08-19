using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002D RID: 45
public class LoadingTips : MonoBehaviour
{
	// Token: 0x060003EA RID: 1002 RVA: 0x00015B50 File Offset: 0x00013D50
	private void Awake()
	{
		bool flag = false;
		try
		{
			string[] array = ModResources.LoadText("tips").Split(new char[]
			{
				'\n'
			});
			if (array.Length != 0)
			{
				foreach (string text in array)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						this._tips.Add(text);
					}
				}
			}
			else
			{
				flag = true;
				Debug.Log("未读取到外部tips，使用内置tips");
			}
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("未读取到外部tips，使用内置tips\n{0}", arg));
			flag = true;
		}
		if (flag)
		{
			foreach (string text2 in this.Tips.Split(new char[]
			{
				'\n'
			}))
			{
				if (!string.IsNullOrWhiteSpace(text2))
				{
					this._tips.Add(text2);
				}
			}
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00015C24 File Offset: 0x00013E24
	private void Update()
	{
		this.cd -= Time.deltaTime;
		if (this.cd < 0f)
		{
			this.cd = this.UpdateTime;
			this.RandomTip();
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00015C58 File Offset: 0x00013E58
	public void RandomTip()
	{
		if (this._tips.Count == 0)
		{
			return;
		}
		if (this._tips.Count == 1)
		{
			this.TipText.text = this._tips[0];
			return;
		}
		if (this.playIndexs.Count == 0)
		{
			for (int i = 0; i < this._tips.Count; i++)
			{
				this.playIndexs.Add(i);
			}
			this.playIndexs.RandomSort<int>();
		}
		int index = this.playIndexs[0];
		this.playIndexs.RemoveAt(0);
		this.TipText.text = this._tips[index];
	}

	// Token: 0x04000221 RID: 545
	public float UpdateTime = 4f;

	// Token: 0x04000222 RID: 546
	public Text TipText;

	// Token: 0x04000223 RID: 547
	[Multiline]
	public string Tips = "";

	// Token: 0x04000224 RID: 548
	private List<string> _tips = new List<string>();

	// Token: 0x04000225 RID: 549
	private List<int> playIndexs = new List<int>();

	// Token: 0x04000226 RID: 550
	private float cd;
}
