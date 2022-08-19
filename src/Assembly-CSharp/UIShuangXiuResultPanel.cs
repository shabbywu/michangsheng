using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000223 RID: 547
public class UIShuangXiuResultPanel : MonoBehaviour
{
	// Token: 0x060015B9 RID: 5561 RVA: 0x00091460 File Offset: 0x0008F660
	private void Awake()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		PanelMamager.CanOpenOrClose = false;
		this.panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(this.panel, Vector3.one, 0.5f);
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000914F3 File Offset: 0x0008F6F3
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			this.Close();
		}
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x00091504 File Offset: 0x0008F704
	public void Close()
	{
		Object.Destroy(base.gameObject);
		Tools.canClickFlag = true;
		PanelMamager.CanOpenOrClose = true;
		if (this.OkAction != null)
		{
			this.OkAction.Invoke();
		}
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x00091530 File Offset: 0x0008F730
	public void Show(string str, UnityAction onOk = null)
	{
		this.ShowText.text = str;
		this.OkAction = onOk;
		this.OKBtn.mouseUpEvent.RemoveAllListeners();
		this.OKBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
	}

	// Token: 0x0400103C RID: 4156
	public Text ShowText;

	// Token: 0x0400103D RID: 4157
	public FpBtn OKBtn;

	// Token: 0x0400103E RID: 4158
	private UnityAction OkAction;

	// Token: 0x0400103F RID: 4159
	public Transform panel;
}
