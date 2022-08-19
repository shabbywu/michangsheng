using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002F3 RID: 755
public class MyDropdown : MonoBehaviour
{
	// Token: 0x06001A69 RID: 6761 RVA: 0x000BC524 File Offset: 0x000BA724
	private void Start()
	{
		this.startBtn.onClick.AddListener(new UnityAction(this.Open));
		this.bigBtn.onClick.AddListener(new UnityAction(this.Close));
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x000BC55E File Offset: 0x000BA75E
	private void Open()
	{
		this.content.SetActive(true);
		this.startBtn.gameObject.SetActive(false);
		this.bigBtn.gameObject.SetActive(true);
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x000BC58E File Offset: 0x000BA78E
	public void Close()
	{
		this.startBtn.gameObject.SetActive(true);
		this.content.SetActive(false);
		this.bigBtn.gameObject.SetActive(false);
	}

	// Token: 0x0400153C RID: 5436
	[SerializeField]
	private GameObject content;

	// Token: 0x0400153D RID: 5437
	[SerializeField]
	private Button startBtn;

	// Token: 0x0400153E RID: 5438
	[SerializeField]
	private Button bigBtn;
}
