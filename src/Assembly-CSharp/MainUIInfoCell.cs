using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200048E RID: 1166
public class MainUIInfoCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001F19 RID: 7961 RVA: 0x0010B9E8 File Offset: 0x00109BE8
	public void UpdateNum(string content, bool isLinGen = false)
	{
		this.text.text = content;
		if (!isLinGen)
		{
			if (this.curNum > this.baseNum)
			{
				this.text.color = new Color(0.7921569f, 0.9764706f, 0.24705882f);
				return;
			}
			if (this.curNum < this.baseNum)
			{
				this.text.color = new Color(1f, 0.7529412f, 0.6313726f);
				return;
			}
			this.text.color = new Color(1f, 0.9882353f, 0.84313726f);
			return;
		}
		else
		{
			if (this.curNum > this.baseNum)
			{
				this.text.color = new Color(1f, 0.7529412f, 0.6313726f);
				return;
			}
			if (this.curNum < this.baseNum)
			{
				this.text.color = new Color(0.7921569f, 0.9764706f, 0.24705882f);
				return;
			}
			this.text.color = new Color(1f, 0.9882353f, 0.84313726f);
			return;
		}
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x0010BAFC File Offset: 0x00109CFC
	public void OnPointerEnter(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Show(this.desc, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z));
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x00019C25 File Offset: 0x00017E25
	public void OnPointerExit(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Hide();
	}

	// Token: 0x04001A91 RID: 6801
	[SerializeField]
	private Text text;

	// Token: 0x04001A92 RID: 6802
	public int baseNum;

	// Token: 0x04001A93 RID: 6803
	public int curNum;

	// Token: 0x04001A94 RID: 6804
	public string desc;
}
