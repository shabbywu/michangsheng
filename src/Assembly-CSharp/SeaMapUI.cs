using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019E RID: 414
public class SeaMapUI : MonoBehaviour
{
	// Token: 0x060011BE RID: 4542 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x0006B879 File Offset: 0x00069A79
	public void CloseSeaMapUI()
	{
		base.gameObject.SetActive(false);
		Tools.canClickFlag = true;
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x0006B890 File Offset: 0x00069A90
	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			this.image.rectTransform.sizeDelta *= 1.03f;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			this.image.rectTransform.sizeDelta /= 1.03f;
		}
		if (this.image.rectTransform.sizeDelta.x < 1920f)
		{
			this.image.rectTransform.sizeDelta = new Vector2(1920f, 1080f);
			return;
		}
		if (this.image.rectTransform.sizeDelta.x > 3840f)
		{
			this.image.rectTransform.sizeDelta = new Vector2(3840f, 2160f);
		}
	}

	// Token: 0x04000CB5 RID: 3253
	public Image image;
}
