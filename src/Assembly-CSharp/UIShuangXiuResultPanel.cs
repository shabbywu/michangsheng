using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200033F RID: 831
public class UIShuangXiuResultPanel : MonoBehaviour
{
	// Token: 0x06001871 RID: 6257 RVA: 0x000D9F28 File Offset: 0x000D8128
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

	// Token: 0x06001872 RID: 6258 RVA: 0x00015382 File Offset: 0x00013582
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			this.Close();
		}
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x00015393 File Offset: 0x00013593
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

	// Token: 0x06001874 RID: 6260 RVA: 0x000D9FBC File Offset: 0x000D81BC
	public void Show(string str, UnityAction onOk = null)
	{
		this.ShowText.text = str;
		this.OkAction = onOk;
		this.OKBtn.mouseUpEvent.RemoveAllListeners();
		this.OKBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
	}

	// Token: 0x04001394 RID: 5012
	public Text ShowText;

	// Token: 0x04001395 RID: 5013
	public FpBtn OKBtn;

	// Token: 0x04001396 RID: 5014
	private UnityAction OkAction;

	// Token: 0x04001397 RID: 5015
	public Transform panel;
}
