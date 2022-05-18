using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000433 RID: 1075
public static class GameObejetUtils
{
	// Token: 0x06001CA1 RID: 7329 RVA: 0x00017E94 File Offset: 0x00016094
	public static GameObject Inst(this GameObject obj, Transform parent = null)
	{
		if (parent != null)
		{
			return Object.Instantiate<GameObject>(obj, parent);
		}
		return Object.Instantiate<GameObject>(obj);
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x00017EAD File Offset: 0x000160AD
	public static bool IsEqualTo(this float a, float b, float margin)
	{
		return Math.Abs(a - b) < margin;
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00017EBA File Offset: 0x000160BA
	public static bool IsEqualTo(this float a, double b)
	{
		return Math.Abs((double)a - b) < 1.401298464324817E-45;
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00017ED0 File Offset: 0x000160D0
	public static Transform SetPostionX(this Transform transform, float x)
	{
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
		return transform;
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00017EF5 File Offset: 0x000160F5
	public static Transform SetPostionY(this Transform transform, float y)
	{
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
		return transform;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00017F1A File Offset: 0x0001611A
	public static Transform SetPostionZ(this Transform transform, float z)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
		return transform;
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00017F3F File Offset: 0x0001613F
	public static Transform SetLocalPositionX(this Transform transform, float x)
	{
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		return transform;
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x00017F64 File Offset: 0x00016164
	public static Transform SetLocalPositionY(this Transform transform, float y)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
		return transform;
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x00017F89 File Offset: 0x00016189
	public static Transform SetLocalPositionZ(this Transform transform, float z)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		return transform;
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x00017FAE File Offset: 0x000161AE
	public static void SetText(this Text text, object content)
	{
		if (content is string)
		{
			text.text = ((string)content).ToCN();
			return;
		}
		text.text = content.ToString().ToCN();
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x000FC6CC File Offset: 0x000FA8CC
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

	// Token: 0x06001CAC RID: 7340 RVA: 0x000FC774 File Offset: 0x000FA974
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

	// Token: 0x06001CAD RID: 7341 RVA: 0x000FC830 File Offset: 0x000FAA30
	public static void AddText(this Text text, object content)
	{
		if (content is string)
		{
			text.text += Tools.Code64((string)content);
			return;
		}
		text.text += content.ToString().ToCN();
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00017FDB File Offset: 0x000161DB
	public static void Hide(this Image img)
	{
		img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x0001800E File Offset: 0x0001620E
	public static void Show(this Image img)
	{
		img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
	}
}
