using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E0 RID: 736
public static class GameObejetUtils
{
	// Token: 0x06001988 RID: 6536 RVA: 0x000B67DC File Offset: 0x000B49DC
	public static GameObject Inst(this GameObject obj, Transform parent = null)
	{
		if (parent != null)
		{
			return Object.Instantiate<GameObject>(obj, parent);
		}
		return Object.Instantiate<GameObject>(obj);
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x000B67F5 File Offset: 0x000B49F5
	public static bool IsEqualTo(this float a, float b, float margin)
	{
		return Math.Abs(a - b) < margin;
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x000B6802 File Offset: 0x000B4A02
	public static bool IsEqualTo(this float a, double b)
	{
		return Math.Abs((double)a - b) < 1.401298464324817E-45;
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x000B6818 File Offset: 0x000B4A18
	public static Transform SetPostionX(this Transform transform, float x)
	{
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
		return transform;
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x000B683D File Offset: 0x000B4A3D
	public static Transform SetPostionY(this Transform transform, float y)
	{
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
		return transform;
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x000B6862 File Offset: 0x000B4A62
	public static Transform SetPostionZ(this Transform transform, float z)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
		return transform;
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x000B6887 File Offset: 0x000B4A87
	public static Transform SetLocalPositionX(this Transform transform, float x)
	{
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		return transform;
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x000B68AC File Offset: 0x000B4AAC
	public static Transform SetLocalPositionY(this Transform transform, float y)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
		return transform;
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x000B68D1 File Offset: 0x000B4AD1
	public static Transform SetLocalPositionZ(this Transform transform, float z)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		return transform;
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x000B68F6 File Offset: 0x000B4AF6
	public static void SetText(this Text text, object content)
	{
		if (content is string)
		{
			text.text = ((string)content).ToCN();
			return;
		}
		text.text = content.ToString().ToCN();
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x000B6924 File Offset: 0x000B4B24
	public static void SetText(this Text text, object content, string color)
	{
		if (!color.Contains("#"))
		{
			color = "#" + color;
		}
		if (content is string)
		{
			text.text = string.Concat(new string[]
			{
				"<color=",
				color,
				">",
				((string)content).ToCN(),
				"</color>"
			});
			return;
		}
		text.text = string.Concat(new string[]
		{
			"<color=",
			color,
			">",
			content.ToString().ToCN(),
			"</color>"
		});
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x000B69CC File Offset: 0x000B4BCC
	public static void AddText(this Text text, object content, string color)
	{
		if (!color.Contains("#"))
		{
			color = "#" + color;
		}
		if (content is string)
		{
			text.text = string.Concat(new string[]
			{
				text.text,
				"<color=",
				color,
				">",
				((string)content).ToCN(),
				"</color>"
			});
			return;
		}
		text.text = string.Concat(new string[]
		{
			text.text,
			"<color=",
			color,
			">",
			content.ToString().ToCN(),
			"</color>"
		});
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x000B6A88 File Offset: 0x000B4C88
	public static void AddText(this Text text, object content)
	{
		if (content is string)
		{
			text.text += Tools.Code64((string)content);
			return;
		}
		text.text += content.ToString().ToCN();
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x000B6AD6 File Offset: 0x000B4CD6
	public static void Hide(this Image img)
	{
		img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x000B6B09 File Offset: 0x000B4D09
	public static void Show(this Image img)
	{
		img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
	}
}
