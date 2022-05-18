using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000450 RID: 1104
public class MyDropdown : MonoBehaviour
{
	// Token: 0x06001D8F RID: 7567 RVA: 0x00018931 File Offset: 0x00016B31
	private void Start()
	{
		this.startBtn.onClick.AddListener(new UnityAction(this.Open));
		this.bigBtn.onClick.AddListener(new UnityAction(this.Close));
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x0001896B File Offset: 0x00016B6B
	private void Open()
	{
		this.content.SetActive(true);
		this.startBtn.gameObject.SetActive(false);
		this.bigBtn.gameObject.SetActive(true);
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x0001899B File Offset: 0x00016B9B
	public void Close()
	{
		this.startBtn.gameObject.SetActive(true);
		this.content.SetActive(false);
		this.bigBtn.gameObject.SetActive(false);
	}

	// Token: 0x04001949 RID: 6473
	[SerializeField]
	private GameObject content;

	// Token: 0x0400194A RID: 6474
	[SerializeField]
	private Button startBtn;

	// Token: 0x0400194B RID: 6475
	[SerializeField]
	private Button bigBtn;
}
