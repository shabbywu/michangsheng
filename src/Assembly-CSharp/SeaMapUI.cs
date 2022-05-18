using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029A RID: 666
public class SeaMapUI : MonoBehaviour
{
	// Token: 0x06001465 RID: 5221 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x00012E4E File Offset: 0x0001104E
	public void CloseSeaMapUI()
	{
		base.gameObject.SetActive(false);
		Tools.canClickFlag = true;
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x000B9A18 File Offset: 0x000B7C18
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

	// Token: 0x04000FD9 RID: 4057
	public Image image;
}
