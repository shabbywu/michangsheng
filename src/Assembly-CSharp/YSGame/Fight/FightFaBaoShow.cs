using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000AB7 RID: 2743
	public class FightFaBaoShow : MonoBehaviour
	{
		// Token: 0x06004CDF RID: 19679 RVA: 0x0020E1F3 File Offset: 0x0020C3F3
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0020E204 File Offset: 0x0020C404
		private void OnDestroy()
		{
			if (this.isPlayer)
			{
				MessageMag.Instance.Remove(FightFaBaoShow.PlayerUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
				return;
			}
			MessageMag.Instance.Remove(FightFaBaoShow.NPCUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0020E250 File Offset: 0x0020C450
		private void SetAnimCtl(string type, bool isEditorTest = false)
		{
			RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FightFaBao/Anim/FightFaBaoAnimCtl_" + type);
			this.animator.runtimeAnimatorController = runtimeAnimatorController;
			if (!isEditorTest && !this.bindAvatar.isPlayer())
			{
				this.animator.speed = 30f;
				base.Invoke("StopJiaSu", 0.05f);
			}
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0020E2AA File Offset: 0x0020C4AA
		private void StopJiaSu()
		{
			this.animator.speed = 1f;
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0020E2BC File Offset: 0x0020C4BC
		public static string GetEquipFightShowPath(int equipType, int equipShuXing, int equipQuality)
		{
			return string.Format("FightFaBao/MCS_fabao_{0}_{1}_{2}", FightFaBaoShow.EquipFightShowTypeDict[equipType], equipShuXing, equipQuality);
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0020E2DF File Offset: 0x0020C4DF
		public void TestFaBao(int 装备类型 = 1, int 装备属性 = 0, int 装备品阶 = 1, bool 男 = true)
		{
			this.SetEquipFightShow(装备类型, 装备属性, 装备品阶, true, 男);
			this.SetAnimCtl(FightFaBaoShow.EquipFightShowTypeDict[装备类型], true);
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0020E300 File Offset: 0x0020C500
		private void SetEquipFightShow(int equipType, int equipShuXing, int equipQuality, bool isEditorTest = false, bool isNan = true)
		{
			if (isNan)
			{
				this.SetOffset(this.NanOffset, equipType);
			}
			else
			{
				this.SetOffset(this.NvOffset, equipType);
			}
			string equipFightShowPath = FightFaBaoShow.GetEquipFightShowPath(equipType, equipShuXing, equipQuality);
			Sprite sprite;
			if (isEditorTest)
			{
				sprite = Resources.Load<Sprite>(equipFightShowPath);
			}
			else
			{
				sprite = ResManager.inst.LoadSprite(equipFightShowPath);
			}
			if (sprite == null)
			{
				Debug.LogError("没有找到贴图" + equipFightShowPath);
			}
			if (equipType == 4 || equipType == 2)
			{
				for (int i = 0; i < this.SpriteRendererList.Count; i++)
				{
					this.SpriteRendererList[i].sprite = sprite;
					this.SpriteRendererList[i].material.SetColor("_LtColor", FightFaBaoShow.EquipFightShowLightColorDict[equipShuXing]);
				}
				return;
			}
			this.SpriteRendererList[0].sprite = sprite;
			this.SpriteRendererList[0].material.SetColor("_LtColor", FightFaBaoShow.EquipFightShowLightColorDict[equipShuXing]);
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0020E3FC File Offset: 0x0020C5FC
		private void SetEquipFightShow(string fabaoType, int equipQuality, bool isNan)
		{
			string text = string.Format("FightFaBao/MCS_fabao_{0}_{1}", fabaoType, equipQuality);
			Sprite sprite = ResManager.inst.LoadSprite(text);
			if (sprite == null)
			{
				Debug.LogError("没有找到贴图" + text);
			}
			int key = int.Parse(fabaoType.Split(new char[]
			{
				'_'
			})[1]);
			if (fabaoType.Contains("zhen") || fabaoType.Contains("zhong"))
			{
				for (int i = 0; i < this.SpriteRendererList.Count; i++)
				{
					this.SpriteRendererList[i].sprite = sprite;
					this.SpriteRendererList[i].material.SetColor("_LtColor", FightFaBaoShow.EquipFightShowLightColorDict[key]);
				}
				return;
			}
			this.SpriteRendererList[0].sprite = sprite;
			this.SpriteRendererList[0].material.SetColor("_LtColor", FightFaBaoShow.EquipFightShowLightColorDict[key]);
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0020E4FC File Offset: 0x0020C6FC
		public void SetWeapon(Avatar avatar, ITEM_INFO weapon)
		{
			this.bindAvatar = avatar;
			this.isPlayer = this.bindAvatar.isPlayer();
			if (weapon.itemId >= 18001 && weapon.itemId <= 18010)
			{
				int num = weapon.itemId - 18000;
				int i = weapon.Seid["quality"].I;
				int equipShuXing = weapon.Seid["AttackType"].ToList()[0];
				this.SetEquipFightShow(num, equipShuXing, i, false, avatar.Sex == 1);
				this.SetAnimCtl(FightFaBaoShow.EquipFightShowTypeDict[num], false);
			}
			else
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[weapon.itemId];
				if (string.IsNullOrWhiteSpace(itemJsonData.FaBaoType))
				{
					Debug.LogError(string.Format("{0} id:{1}没有配FaBaoType", itemJsonData.name, itemJsonData.id));
					return;
				}
				this.SetEquipFightShow(itemJsonData.FaBaoType, itemJsonData.quality, avatar.Sex == 1);
				string[] array = itemJsonData.FaBaoType.Split(new char[]
				{
					'_'
				});
				this.SetAnimCtl(array[0], false);
			}
			this.BindMessage();
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0020E624 File Offset: 0x0020C824
		public void SetNPCWeapon(Avatar avatar, JSONObject weapon)
		{
			this.bindAvatar = avatar;
			this.isPlayer = this.bindAvatar.isPlayer();
			int i = weapon["ItemID"].I;
			if (i >= 18001 && i <= 18010)
			{
				int num = i - 18000;
				int i2 = weapon["quality"].I;
				int equipShuXing = weapon["AttackType"].ToList()[0];
				this.SetEquipFightShow(num, equipShuXing, i2, false, avatar.Sex == 1);
				this.SetAnimCtl(FightFaBaoShow.EquipFightShowTypeDict[num], false);
			}
			else
			{
				Debug.LogError("SetNPCWeapon出现非炼器装备");
			}
			this.BindMessage();
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0020E6D4 File Offset: 0x0020C8D4
		public void SetOffset(FightFaBaoOffset offset, int equipType)
		{
			switch (equipType)
			{
			case 1:
				base.transform.parent.localPosition = offset.JianOffset;
				return;
			case 2:
				base.transform.parent.localPosition = offset.ZhongOffset;
				return;
			case 3:
				base.transform.parent.localPosition = offset.HuanOffset;
				return;
			case 4:
				base.transform.parent.localPosition = offset.ZhenOffset;
				return;
			case 5:
				base.transform.parent.localPosition = offset.XiaOffset;
				return;
			default:
				return;
			}
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0020E770 File Offset: 0x0020C970
		private void BindMessage()
		{
			if (this.bindAvatar.isPlayer())
			{
				MessageMag.Instance.Register(FightFaBaoShow.PlayerUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
				return;
			}
			MessageMag.Instance.Register(FightFaBaoShow.NPCUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0020E7C1 File Offset: 0x0020C9C1
		public void TestPlayAnim()
		{
			this.animator.Play("Use");
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0020E7C1 File Offset: 0x0020C9C1
		private void OnUseWeapon(MessageData data)
		{
			this.animator.Play("Use");
		}

		// Token: 0x04004BF2 RID: 19442
		private Animator animator;

		// Token: 0x04004BF3 RID: 19443
		public List<SpriteRenderer> SpriteRendererList;

		// Token: 0x04004BF4 RID: 19444
		private Avatar bindAvatar;

		// Token: 0x04004BF5 RID: 19445
		private bool isPlayer;

		// Token: 0x04004BF6 RID: 19446
		public FightFaBaoOffset NanOffset;

		// Token: 0x04004BF7 RID: 19447
		public FightFaBaoOffset NvOffset;

		// Token: 0x04004BF8 RID: 19448
		public static string PlayerUseWeaponMsgKey = "PlayerUseWeapon";

		// Token: 0x04004BF9 RID: 19449
		public static string NPCUseWeaponMsgKey = "NPCUseWeapon";

		// Token: 0x04004BFA RID: 19450
		private static Dictionary<int, string> EquipFightShowTypeDict = new Dictionary<int, string>
		{
			{
				1,
				"jian"
			},
			{
				2,
				"zhong"
			},
			{
				3,
				"huan"
			},
			{
				4,
				"zhen"
			},
			{
				5,
				"xia"
			},
			{
				6,
				"pao"
			},
			{
				7,
				"jia"
			},
			{
				8,
				"zhu"
			},
			{
				9,
				"ling"
			},
			{
				10,
				"yin"
			}
		};

		// Token: 0x04004BFB RID: 19451
		private static Dictionary<int, Color> EquipFightShowLightColorDict = new Dictionary<int, Color>
		{
			{
				0,
				new Color(0.75686276f, 0.7019608f, 0.14901961f)
			},
			{
				1,
				new Color(0.14901961f, 0.75686276f, 0.5176471f)
			},
			{
				2,
				new Color(0.14901961f, 0.6784314f, 0.75686276f)
			},
			{
				3,
				new Color(0.75686276f, 0.28235295f, 0.14901961f)
			},
			{
				4,
				new Color(0.67058825f, 0.5058824f, 0.15294118f)
			},
			{
				5,
				new Color(0.40784314f, 0.2509804f, 0.12156863f)
			},
			{
				6,
				new Color(0.08235294f, 0.29803923f, 0.32156864f)
			},
			{
				7,
				new Color(0.078431375f, 0.32156864f, 0.15294118f)
			}
		};
	}
}
