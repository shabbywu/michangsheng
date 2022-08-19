using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000328 RID: 808
public class MainUIInfoCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001BD7 RID: 7127 RVA: 0x000C6884 File Offset: 0x000C4A84
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

	// Token: 0x06001BD8 RID: 7128 RVA: 0x000C6998 File Offset: 0x000C4B98
	public void OnPointerEnter(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Show(this.desc, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z));
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000C69EF File Offset: 0x000C4BEF
	public void OnPointerExit(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Hide();
	}

	// Token: 0x0400166C RID: 5740
	[SerializeField]
	private Text text;

	// Token: 0x0400166D RID: 5741
	public int baseNum;

	// Token: 0x0400166E RID: 5742
	public int curNum;

	// Token: 0x0400166F RID: 5743
	public string desc;
}
