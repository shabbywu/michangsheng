using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.Fight;

public class FightFaBaoShow : MonoBehaviour
{
	private Animator animator;

	public List<SpriteRenderer> SpriteRendererList;

	private Avatar bindAvatar;

	private bool isPlayer;

	public FightFaBaoOffset NanOffset;

	public FightFaBaoOffset NvOffset;

	public static string PlayerUseWeaponMsgKey = "PlayerUseWeapon";

	public static string NPCUseWeaponMsgKey = "NPCUseWeapon";

	private static Dictionary<int, string> EquipFightShowTypeDict = new Dictionary<int, string>
	{
		{ 1, "jian" },
		{ 2, "zhong" },
		{ 3, "huan" },
		{ 4, "zhen" },
		{ 5, "xia" },
		{ 6, "pao" },
		{ 7, "jia" },
		{ 8, "zhu" },
		{ 9, "ling" },
		{ 10, "yin" }
	};

	private static Dictionary<int, Color> EquipFightShowLightColorDict = new Dictionary<int, Color>
	{
		{
			0,
			new Color(0.75686276f, 0.7019608f, 0.14901961f)
		},
		{
			1,
			new Color(0.14901961f, 0.75686276f, 44f / 85f)
		},
		{
			2,
			new Color(0.14901961f, 0.6784314f, 0.75686276f)
		},
		{
			3,
			new Color(0.75686276f, 24f / 85f, 0.14901961f)
		},
		{
			4,
			new Color(57f / 85f, 43f / 85f, 13f / 85f)
		},
		{
			5,
			new Color(0.40784314f, 0.2509804f, 0.12156863f)
		},
		{
			6,
			new Color(7f / 85f, 0.29803923f, 0.32156864f)
		},
		{
			7,
			new Color(4f / 51f, 0.32156864f, 13f / 85f)
		}
	};

	private void Awake()
	{
		animator = ((Component)this).GetComponent<Animator>();
	}

	private void OnDestroy()
	{
		if (isPlayer)
		{
			MessageMag.Instance.Remove(PlayerUseWeaponMsgKey, OnUseWeapon);
		}
		else
		{
			MessageMag.Instance.Remove(NPCUseWeaponMsgKey, OnUseWeapon);
		}
	}

	private void SetAnimCtl(string type, bool isEditorTest = false)
	{
		RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FightFaBao/Anim/FightFaBaoAnimCtl_" + type);
		animator.runtimeAnimatorController = runtimeAnimatorController;
		if (!isEditorTest && !bindAvatar.isPlayer())
		{
			animator.speed = 30f;
			((MonoBehaviour)this).Invoke("StopJiaSu", 0.05f);
		}
	}

	private void StopJiaSu()
	{
		animator.speed = 1f;
	}

	public static string GetEquipFightShowPath(int equipType, int equipShuXing, int equipQuality)
	{
		return $"FightFaBao/MCS_fabao_{EquipFightShowTypeDict[equipType]}_{equipShuXing}_{equipQuality}";
	}

	public void TestFaBao(int 装备类型 = 1, int 装备属性 = 0, int 装备品阶 = 1, bool 男 = true)
	{
		SetEquipFightShow(装备类型, 装备属性, 装备品阶, isEditorTest: true, 男);
		SetAnimCtl(EquipFightShowTypeDict[装备类型], isEditorTest: true);
	}

	private void SetEquipFightShow(int equipType, int equipShuXing, int equipQuality, bool isEditorTest = false, bool isNan = true)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (isNan)
		{
			SetOffset(NanOffset, equipType);
		}
		else
		{
			SetOffset(NvOffset, equipType);
		}
		string equipFightShowPath = GetEquipFightShowPath(equipType, equipShuXing, equipQuality);
		Sprite val = null;
		val = ((!isEditorTest) ? ResManager.inst.LoadSprite(equipFightShowPath) : Resources.Load<Sprite>(equipFightShowPath));
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("没有找到贴图" + equipFightShowPath));
		}
		if (equipType == 4 || equipType == 2)
		{
			for (int i = 0; i < SpriteRendererList.Count; i++)
			{
				SpriteRendererList[i].sprite = val;
				((Renderer)SpriteRendererList[i]).material.SetColor("_LtColor", EquipFightShowLightColorDict[equipShuXing]);
			}
		}
		else
		{
			SpriteRendererList[0].sprite = val;
			((Renderer)SpriteRendererList[0]).material.SetColor("_LtColor", EquipFightShowLightColorDict[equipShuXing]);
		}
	}

	private void SetEquipFightShow(string fabaoType, int equipQuality, bool isNan)
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		string text = $"FightFaBao/MCS_fabao_{fabaoType}_{equipQuality}";
		Sprite val = ResManager.inst.LoadSprite(text);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("没有找到贴图" + text));
		}
		int key = int.Parse(fabaoType.Split(new char[1] { '_' })[1]);
		if (fabaoType.Contains("zhen") || fabaoType.Contains("zhong"))
		{
			for (int i = 0; i < SpriteRendererList.Count; i++)
			{
				SpriteRendererList[i].sprite = val;
				((Renderer)SpriteRendererList[i]).material.SetColor("_LtColor", EquipFightShowLightColorDict[key]);
			}
		}
		else
		{
			SpriteRendererList[0].sprite = val;
			((Renderer)SpriteRendererList[0]).material.SetColor("_LtColor", EquipFightShowLightColorDict[key]);
		}
	}

	public void SetWeapon(Avatar avatar, ITEM_INFO weapon)
	{
		bindAvatar = avatar;
		isPlayer = bindAvatar.isPlayer();
		if (weapon.itemId >= 18001 && weapon.itemId <= 18010)
		{
			int num = weapon.itemId - 18000;
			int i = weapon.Seid["quality"].I;
			int equipShuXing = weapon.Seid["AttackType"].ToList()[0];
			SetEquipFightShow(num, equipShuXing, i, isEditorTest: false, avatar.Sex == 1);
			SetAnimCtl(EquipFightShowTypeDict[num]);
		}
		else
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[weapon.itemId];
			if (string.IsNullOrWhiteSpace(itemJsonData.FaBaoType))
			{
				Debug.LogError((object)$"{itemJsonData.name} id:{itemJsonData.id}没有配FaBaoType");
				return;
			}
			SetEquipFightShow(itemJsonData.FaBaoType, itemJsonData.quality, avatar.Sex == 1);
			string[] array = itemJsonData.FaBaoType.Split(new char[1] { '_' });
			SetAnimCtl(array[0]);
		}
		BindMessage();
	}

	public void SetNPCWeapon(Avatar avatar, JSONObject weapon)
	{
		bindAvatar = avatar;
		isPlayer = bindAvatar.isPlayer();
		int i = weapon["ItemID"].I;
		if (i >= 18001 && i <= 18010)
		{
			int num = i - 18000;
			int i2 = weapon["quality"].I;
			int equipShuXing = weapon["AttackType"].ToList()[0];
			SetEquipFightShow(num, equipShuXing, i2, isEditorTest: false, avatar.Sex == 1);
			SetAnimCtl(EquipFightShowTypeDict[num]);
		}
		else
		{
			Debug.LogError((object)"SetNPCWeapon出现非炼器装备");
		}
		BindMessage();
	}

	public void SetOffset(FightFaBaoOffset offset, int equipType)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		switch (equipType)
		{
		case 1:
			((Component)this).transform.parent.localPosition = offset.JianOffset;
			break;
		case 2:
			((Component)this).transform.parent.localPosition = offset.ZhongOffset;
			break;
		case 3:
			((Component)this).transform.parent.localPosition = offset.HuanOffset;
			break;
		case 4:
			((Component)this).transform.parent.localPosition = offset.ZhenOffset;
			break;
		case 5:
			((Component)this).transform.parent.localPosition = offset.XiaOffset;
			break;
		}
	}

	private void BindMessage()
	{
		if (bindAvatar.isPlayer())
		{
			MessageMag.Instance.Register(PlayerUseWeaponMsgKey, OnUseWeapon);
		}
		else
		{
			MessageMag.Instance.Register(NPCUseWeaponMsgKey, OnUseWeapon);
		}
	}

	public void TestPlayAnim()
	{
		animator.Play("Use");
	}

	private void OnUseWeapon(MessageData data)
	{
		animator.Play("Use");
	}
}
