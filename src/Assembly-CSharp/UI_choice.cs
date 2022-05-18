using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005B6 RID: 1462
public class UI_choice : MonoBehaviour
{
	// Token: 0x060024E9 RID: 9449 RVA: 0x00129DA0 File Offset: 0x00127FA0
	private void Start()
	{
		this.cancel = (UnityAction)Delegate.Combine(this.cancel, new UnityAction(this.removeSelf));
		this.Cancel.onClick.AddListener(this.cancel);
		this.OK.onClick.AddListener(this.cancel);
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x0001DA45 File Offset: 0x0001BC45
	public void removeSelf()
	{
		base.transform.localScale = Vector3.zero;
		Object.Destroy(base.gameObject, 0.1f);
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x0001DA67 File Offset: 0x0001BC67
	public static UI_choice CreatUI_choice()
	{
		return Object.Instantiate<GameObject>(Resources.Load("uiPrefab/CanvasChoice") as GameObject).GetComponent<UI_choice>();
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x0001DA82 File Offset: 0x0001BC82
	public void OKAddListener(UnityAction unityAction)
	{
		this.OK.onClick.AddListener(unityAction);
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x0001DA95 File Offset: 0x0001BC95
	public void setText(string str)
	{
		this.desc.text = str;
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04001F94 RID: 8084
	private UnityAction ok;

	// Token: 0x04001F95 RID: 8085
	private UnityAction cancel;

	// Token: 0x04001F96 RID: 8086
	public Button OK;

	// Token: 0x04001F97 RID: 8087
	public Button Cancel;

	// Token: 0x04001F98 RID: 8088
	public Text desc;
}
