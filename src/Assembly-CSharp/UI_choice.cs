using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000406 RID: 1030
public class UI_choice : MonoBehaviour
{
	// Token: 0x06002137 RID: 8503 RVA: 0x000E8158 File Offset: 0x000E6358
	private void Start()
	{
		this.cancel = (UnityAction)Delegate.Combine(this.cancel, new UnityAction(this.removeSelf));
		this.Cancel.onClick.AddListener(this.cancel);
		this.OK.onClick.AddListener(this.cancel);
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x000E81B3 File Offset: 0x000E63B3
	public void removeSelf()
	{
		base.transform.localScale = Vector3.zero;
		Object.Destroy(base.gameObject, 0.1f);
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x000E81D5 File Offset: 0x000E63D5
	public static UI_choice CreatUI_choice()
	{
		return Object.Instantiate<GameObject>(Resources.Load("uiPrefab/CanvasChoice") as GameObject).GetComponent<UI_choice>();
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x000E81F0 File Offset: 0x000E63F0
	public void OKAddListener(UnityAction unityAction)
	{
		this.OK.onClick.AddListener(unityAction);
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x000E8203 File Offset: 0x000E6403
	public void setText(string str)
	{
		this.desc.text = str;
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001AD8 RID: 6872
	private UnityAction ok;

	// Token: 0x04001AD9 RID: 6873
	private UnityAction cancel;

	// Token: 0x04001ADA RID: 6874
	public Button OK;

	// Token: 0x04001ADB RID: 6875
	public Button Cancel;

	// Token: 0x04001ADC RID: 6876
	public Text desc;
}
