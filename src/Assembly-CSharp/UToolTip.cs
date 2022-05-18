using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004E8 RID: 1256
public class UToolTip : MonoBehaviour
{
	// Token: 0x060020B9 RID: 8377 RVA: 0x00113E04 File Offset: 0x00112004
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

	// Token: 0x060020BA RID: 8378 RVA: 0x00113E60 File Offset: 0x00112060
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

	// Token: 0x060020BB RID: 8379 RVA: 0x0011408C File Offset: 0x0011228C
	public static bool IsShouldCloseInput()
	{
		return Input.anyKeyDown && !Input.GetKeyDown(304) && !Input.GetKeyDown(306) && !Input.GetKeyDown(308) && !Input.GetKeyDown(311) && !Input.GetKeyDown(303) && !Input.GetKeyDown(305) && !Input.GetKeyDown(307) && !Input.GetKeyDown(312);
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x0001AEB7 File Offset: 0x000190B7
	public static void Close()
	{
		UToolTip.startShow = false;
		if (UToolTip.myRT != null)
		{
			UToolTip.myRT.GetChild(0).gameObject.SetActive(false);
		}
		UToolTip.CloseOldTooltip();
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x00114114 File Offset: 0x00112314
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

	// Token: 0x060020BE RID: 8382 RVA: 0x00114164 File Offset: 0x00112364
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

	// Token: 0x060020BF RID: 8383 RVA: 0x0001AEE7 File Offset: 0x000190E7
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

	// Token: 0x060020C0 RID: 8384 RVA: 0x0001AF1E File Offset: 0x0001911E
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

	// Token: 0x060020C1 RID: 8385 RVA: 0x0001AF53 File Offset: 0x00019153
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

	// Token: 0x060020C2 RID: 8386 RVA: 0x0001AF89 File Offset: 0x00019189
	public static void CloseOldTooltip()
	{
		if (UToolTip.OldTooltip != null)
		{
			UToolTip.OldTooltip.showTooltip = false;
		}
	}

	// Token: 0x04001C40 RID: 7232
	private static UToolTip inst;

	// Token: 0x04001C41 RID: 7233
	private static RectTransform myRT;

	// Token: 0x04001C42 RID: 7234
	private static string prefabPath = "UToolTip";

	// Token: 0x04001C43 RID: 7235
	private static bool needShow;

	// Token: 0x04001C44 RID: 7236
	private static bool startShow;

	// Token: 0x04001C45 RID: 7237
	private static string showText1 = "";

	// Token: 0x04001C46 RID: 7238
	private static Vector2 widthHeight;

	// Token: 0x04001C47 RID: 7239
	public static string debugData = "";

	// Token: 0x04001C48 RID: 7240
	public Text Text1;

	// Token: 0x04001C49 RID: 7241
	public static GameObject BindObj;

	// Token: 0x04001C4A RID: 7242
	private static Inventory2 ItemTip;

	// Token: 0x04001C4B RID: 7243
	private static Skill_UI SkillTip;

	// Token: 0x04001C4C RID: 7244
	private static Skill_UIST GongFaTip;

	// Token: 0x04001C4D RID: 7245
	private static TooltipItem OldTooltip;
}
