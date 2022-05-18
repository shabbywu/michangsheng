using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000040 RID: 64
public class LoadingTips : MonoBehaviour
{
	// Token: 0x06000432 RID: 1074 RVA: 0x0006D4CC File Offset: 0x0006B6CC
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

	// Token: 0x06000433 RID: 1075 RVA: 0x00007B46 File Offset: 0x00005D46
	private void Update()
	{
		this.cd -= Time.deltaTime;
		if (this.cd < 0f)
		{
			this.cd = this.UpdateTime;
			this.RandomTip();
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x0006D5A0 File Offset: 0x0006B7A0
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

	// Token: 0x04000267 RID: 615
	public float UpdateTime = 4f;

	// Token: 0x04000268 RID: 616
	public Text TipText;

	// Token: 0x04000269 RID: 617
	[Multiline]
	public string Tips = "";

	// Token: 0x0400026A RID: 618
	private List<string> _tips = new List<string>();

	// Token: 0x0400026B RID: 619
	private List<int> playIndexs = new List<int>();

	// Token: 0x0400026C RID: 620
	private float cd;
}
