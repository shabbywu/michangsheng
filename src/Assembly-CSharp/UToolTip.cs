using GUIPackage;
using UnityEngine;
using UnityEngine.UI;

public class UToolTip : MonoBehaviour
{
	private static UToolTip inst;

	private static RectTransform myRT;

	private static string prefabPath = "UToolTip";

	private static bool needShow;

	private static bool startShow;

	private static string showText1 = "";

	private static Vector2 widthHeight;

	public static string debugData = "";

	public Text Text1;

	public static GameObject BindObj;

	private static Inventory2 ItemTip;

	private static Skill_UI SkillTip;

	private static Skill_UIST GongFaTip;

	private static TooltipItem OldTooltip;

	private void Awake()
	{
		if ((Object)(object)inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)((Component)this).transform.parent).gameObject);
		}
		inst = this;
		Transform transform = ((Component)this).transform;
		myRT = (RectTransform)(object)((transform is RectTransform) ? transform : null);
		Object.DontDestroyOnLoad((Object)(object)((Component)((Component)this).transform.parent).gameObject);
	}

	private void Update()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		if (needShow)
		{
			Text1.text = showText1;
			((Component)((Transform)myRT).GetChild(0)).gameObject.SetActive(true);
			myRT.sizeDelta = widthHeight;
			needShow = false;
			startShow = true;
		}
		if (!startShow)
		{
			return;
		}
		float num = 1080f / (float)Screen.height;
		if (myRT.sizeDelta.y != Text1.preferredHeight + 40f)
		{
			myRT.sizeDelta = new Vector2(myRT.sizeDelta.x, Text1.preferredHeight + 40f);
		}
		float num2 = 0f;
		Vector3 val = Input.mousePosition * num;
		Vector2 val2 = new Vector2((float)Screen.width, (float)Screen.height) * num;
		if (val.x > val2.x / 2f)
		{
			if (val.x + myRT.sizeDelta.x / 2f + 20f > val2.x)
			{
				num2 = 0f - (myRT.sizeDelta.x / 2f - (val2.x - val.x)) - 20f;
			}
		}
		else if (val.x < myRT.sizeDelta.x / 2f - 20f)
		{
			num2 = myRT.sizeDelta.x / 2f - val.x + 20f;
		}
		float num3 = ((!(val.y > val2.y / 2f)) ? (100f + Text1.preferredHeight / 2f) : (-100f - Text1.preferredHeight / 2f));
		myRT.anchoredPosition = Vector2.op_Implicit(val + new Vector3(num2, num3));
		if ((Object)(object)BindObj != (Object)null && !BindObj.activeInHierarchy)
		{
			Close();
		}
		if (IsShouldCloseInput())
		{
			Close();
		}
	}

	public static bool IsShouldCloseInput()
	{
		if (Input.anyKeyDown)
		{
			if (Input.GetKeyDown((KeyCode)304))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)306))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)308))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)311))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)303))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)305))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)307))
			{
				return false;
			}
			if (Input.GetKeyDown((KeyCode)312))
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public static void Close()
	{
		startShow = false;
		if ((Object)(object)myRT != (Object)null)
		{
			((Component)((Transform)myRT).GetChild(0)).gameObject.SetActive(false);
		}
		CloseOldTooltip();
	}

	public static void Show(string text, float width = 600f, float height = 200f)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)inst == (Object)null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(prefabPath));
		}
		BindObj = null;
		showText1 = text;
		debugData = text;
		widthHeight = new Vector2(width, height);
		needShow = true;
	}

	private static void Init()
	{
		GameObject val = PanelMamager.inst.UISceneGameObject;
		if ((Object)(object)val == (Object)null)
		{
			if ((Object)(object)TooltipMag.inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("Tooltip").Inst();
			}
			val = ((Component)TooltipMag.inst).gameObject;
		}
		if ((Object)(object)val != (Object)null)
		{
			if ((Object)(object)ItemTip == (Object)null)
			{
				ItemTip = val.GetComponentInChildren<Inventory2>(true);
			}
			if ((Object)(object)SkillTip == (Object)null)
			{
				SkillTip = val.GetComponentInChildren<Skill_UI>(true);
			}
			if ((Object)(object)GongFaTip == (Object)null)
			{
				GongFaTip = val.GetComponentInChildren<Skill_UIST>(true);
			}
			if ((Object)(object)OldTooltip == (Object)null)
			{
				OldTooltip = val.GetComponentInChildren<TooltipItem>(true);
			}
		}
	}

	public static void OpenItemTooltip(item item, int money = 0)
	{
		Init();
		ItemTip.Show_Tooltip(item, money);
		debugData = item.ToString();
		if ((Object)(object)OldTooltip != (Object)null)
		{
			OldTooltip.showTooltip = true;
		}
	}

	public static void OpenSkillTooltip(Skill skill)
	{
		Init();
		SkillTip.Show_Tooltip(skill);
		debugData = skill.ToString();
		if ((Object)(object)OldTooltip != (Object)null)
		{
			OldTooltip.showTooltip = true;
		}
	}

	public static void OpenStaticSkillTooltip(Skill skill)
	{
		Init();
		GongFaTip.Show_Tooltip(skill);
		debugData = skill.ToString();
		if ((Object)(object)OldTooltip != (Object)null)
		{
			OldTooltip.showTooltip = true;
		}
	}

	public static void CloseOldTooltip()
	{
		if ((Object)(object)OldTooltip != (Object)null)
		{
			OldTooltip.showTooltip = false;
		}
	}
}
