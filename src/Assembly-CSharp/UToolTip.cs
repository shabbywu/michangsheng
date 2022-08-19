using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036A RID: 874
public class UToolTip : MonoBehaviour
{
	// Token: 0x06001D4A RID: 7498 RVA: 0x000CF664 File Offset: 0x000CD864
	private void Awake()
	{
		if (UToolTip.inst != null)
		{
			Object.Destroy(base.transform.parent.gameObject);
		}
		UToolTip.inst = this;
		UToolTip.myRT = (base.transform as RectTransform);
		Object.DontDestroyOnLoad(base.transform.parent.gameObject);
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x000CF6C0 File Offset: 0x000CD8C0
	private void Update()
	{
		if (UToolTip.needShow)
		{
			this.Text1.text = UToolTip.showText1;
			UToolTip.myRT.GetChild(0).gameObject.SetActive(true);
			UToolTip.myRT.sizeDelta = UToolTip.widthHeight;
			UToolTip.needShow = false;
			UToolTip.startShow = true;
		}
		if (UToolTip.startShow)
		{
			float num = 1080f / (float)Screen.height;
			if (UToolTip.myRT.sizeDelta.y != this.Text1.preferredHeight + 40f)
			{
				UToolTip.myRT.sizeDelta = new Vector2(UToolTip.myRT.sizeDelta.x, this.Text1.preferredHeight + 40f);
			}
			float num2 = 0f;
			Vector3 vector = Input.mousePosition * num;
			Vector2 vector2 = new Vector2((float)Screen.width, (float)Screen.height) * num;
			if (vector.x > vector2.x / 2f)
			{
				if (vector.x + UToolTip.myRT.sizeDelta.x / 2f + 20f > vector2.x)
				{
					num2 = -(UToolTip.myRT.sizeDelta.x / 2f - (vector2.x - vector.x)) - 20f;
				}
			}
			else if (vector.x < UToolTip.myRT.sizeDelta.x / 2f - 20f)
			{
				num2 = UToolTip.myRT.sizeDelta.x / 2f - vector.x + 20f;
			}
			float num3;
			if (vector.y > vector2.y / 2f)
			{
				num3 = -100f - this.Text1.preferredHeight / 2f;
			}
			else
			{
				num3 = 100f + this.Text1.preferredHeight / 2f;
			}
			UToolTip.myRT.anchoredPosition = vector + new Vector3(num2, num3);
			if (UToolTip.BindObj != null && !UToolTip.BindObj.activeInHierarchy)
			{
				UToolTip.Close();
			}
			if (UToolTip.IsShouldCloseInput())
			{
				UToolTip.Close();
			}
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x000CF8EC File Offset: 0x000CDAEC
	public static bool IsShouldCloseInput()
	{
		return Input.anyKeyDown && !Input.GetKeyDown(304) && !Input.GetKeyDown(306) && !Input.GetKeyDown(308) && !Input.GetKeyDown(311) && !Input.GetKeyDown(303) && !Input.GetKeyDown(305) && !Input.GetKeyDown(307) && !Input.GetKeyDown(312);
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x000CF973 File Offset: 0x000CDB73
	public static void Close()
	{
		UToolTip.startShow = false;
		if (UToolTip.myRT != null)
		{
			UToolTip.myRT.GetChild(0).gameObject.SetActive(false);
		}
		UToolTip.CloseOldTooltip();
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x000CF9A4 File Offset: 0x000CDBA4
	public static void Show(string text, float width = 600f, float height = 200f)
	{
		if (UToolTip.inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(UToolTip.prefabPath));
		}
		UToolTip.BindObj = null;
		UToolTip.showText1 = text;
		UToolTip.debugData = text;
		UToolTip.widthHeight = new Vector2(width, height);
		UToolTip.needShow = true;
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x000CF9F4 File Offset: 0x000CDBF4
	private static void Init()
	{
		GameObject gameObject = PanelMamager.inst.UISceneGameObject;
		if (gameObject == null)
		{
			if (TooltipMag.inst == null)
			{
				ResManager.inst.LoadPrefab("Tooltip").Inst(null);
			}
			gameObject = TooltipMag.inst.gameObject;
		}
		if (gameObject != null)
		{
			if (UToolTip.ItemTip == null)
			{
				UToolTip.ItemTip = gameObject.GetComponentInChildren<Inventory2>(true);
			}
			if (UToolTip.SkillTip == null)
			{
				UToolTip.SkillTip = gameObject.GetComponentInChildren<Skill_UI>(true);
			}
			if (UToolTip.GongFaTip == null)
			{
				UToolTip.GongFaTip = gameObject.GetComponentInChildren<Skill_UIST>(true);
			}
			if (UToolTip.OldTooltip == null)
			{
				UToolTip.OldTooltip = gameObject.GetComponentInChildren<TooltipItem>(true);
			}
		}
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x000CFAB0 File Offset: 0x000CDCB0
	public static void OpenItemTooltip(item item, int money = 0)
	{
		UToolTip.Init();
		UToolTip.ItemTip.Show_Tooltip(item, money, 0);
		UToolTip.debugData = item.ToString();
		if (UToolTip.OldTooltip != null)
		{
			UToolTip.OldTooltip.showTooltip = true;
		}
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x000CFAE7 File Offset: 0x000CDCE7
	public static void OpenSkillTooltip(Skill skill)
	{
		UToolTip.Init();
		UToolTip.SkillTip.Show_Tooltip(skill);
		UToolTip.debugData = skill.ToString();
		if (UToolTip.OldTooltip != null)
		{
			UToolTip.OldTooltip.showTooltip = true;
		}
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x000CFB1C File Offset: 0x000CDD1C
	public static void OpenStaticSkillTooltip(Skill skill)
	{
		UToolTip.Init();
		UToolTip.GongFaTip.Show_Tooltip(skill, 0);
		UToolTip.debugData = skill.ToString();
		if (UToolTip.OldTooltip != null)
		{
			UToolTip.OldTooltip.showTooltip = true;
		}
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x000CFB52 File Offset: 0x000CDD52
	public static void CloseOldTooltip()
	{
		if (UToolTip.OldTooltip != null)
		{
			UToolTip.OldTooltip.showTooltip = false;
		}
	}

	// Token: 0x040017EA RID: 6122
	private static UToolTip inst;

	// Token: 0x040017EB RID: 6123
	private static RectTransform myRT;

	// Token: 0x040017EC RID: 6124
	private static string prefabPath = "UToolTip";

	// Token: 0x040017ED RID: 6125
	private static bool needShow;

	// Token: 0x040017EE RID: 6126
	private static bool startShow;

	// Token: 0x040017EF RID: 6127
	private static string showText1 = "";

	// Token: 0x040017F0 RID: 6128
	private static Vector2 widthHeight;

	// Token: 0x040017F1 RID: 6129
	public static string debugData = "";

	// Token: 0x040017F2 RID: 6130
	public Text Text1;

	// Token: 0x040017F3 RID: 6131
	public static GameObject BindObj;

	// Token: 0x040017F4 RID: 6132
	private static Inventory2 ItemTip;

	// Token: 0x040017F5 RID: 6133
	private static Skill_UI SkillTip;

	// Token: 0x040017F6 RID: 6134
	private static Skill_UIST GongFaTip;

	// Token: 0x040017F7 RID: 6135
	private static TooltipItem OldTooltip;
}
