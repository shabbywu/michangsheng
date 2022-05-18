using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000DF4 RID: 3572
	public class FightFaBaoShow : MonoBehaviour
	{
		// Token: 0x0600562C RID: 22060 RVA: 0x0003DACB File Offset: 0x0003BCCB
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x0023EDCC File Offset: 0x0023CFCC
		private void OnDestroy()
		{
			if (this.isPlayer)
			{
				MessageMag.Instance.Remove(FightFaBaoShow.PlayerUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
				return;
			}
			MessageMag.Instance.Remove(FightFaBaoShow.NPCUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x0023EE18 File Offset: 0x0023D018
		private void SetAnimCtl(string type)
		{
			RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FightFaBao/Anim/FightFaBaoAnimCtl_" + type);
			this.animator.runtimeAnimatorController = runtimeAnimatorController;
			if (!this.bindAvatar.isPlayer())
			{
				this.animator.speed = 30f;
				base.Invoke("StopJiaSu", 0.05f);
			}
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0003DAD9 File Offset: 0x0003BCD9
		private void StopJiaSu()
		{
			this.animator.speed = 1f;
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x0003DAEB File Offset: 0x0003BCEB
		public static string GetEquipFightShowPath(int equipType, int equipShuXing, int equipQuality)
		{
			return string.Format("FightFaBao/MCS_fabao_{0}_{1}_{2}", FightFaBaoShow.EquipFightShowTypeDict[equipType], equipShuXing, equipQuality);
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0023EE70 File Offset: 0x0023D070
		private void SetEquipFightShow(int equipType, int equipShuXing, int equipQuality)
		{
			string equipFightShowPath = FightFaBaoShow.GetEquipFightShowPath(equipType, equipShuXing, equipQuality);
			Sprite sprite = ResManager.inst.LoadSprite(equipFightShowPath);
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

		// Token: 0x06005632 RID: 22066 RVA: 0x0023EF3C File Offset: 0x0023D13C
		private void SetEquipFightShow(string fabaoType, int equipQuality)
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

		// Token: 0x06005633 RID: 22067 RVA: 0x0023F03C File Offset: 0x0023D23C
		public void SetWeapon(Avatar avatar, ITEM_INFO weapon)
		{
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x0023F04C File Offset: 0x0023D24C
		public void SetNPCWeapon(Avatar avatar, JSONObject weapon)
		{
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0023F05C File Offset: 0x0023D25C
		private void BindMessage()
		{
			if (this.bindAvatar.isPlayer())
			{
				MessageMag.Instance.Register(FightFaBaoShow.PlayerUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
				return;
			}
			MessageMag.Instance.Register(FightFaBaoShow.NPCUseWeaponMsgKey, new Action<MessageData>(this.OnUseWeapon));
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0003DB0E File Offset: 0x0003BD0E
		private void OnUseWeapon(MessageData data)
		{
			this.animator.Play("Use");
		}

		// Token: 0x040055D0 RID: 21968
		private Animator animator;

		// Token: 0x040055D1 RID: 21969
		public List<SpriteRenderer> SpriteRendererList;

		// Token: 0x040055D2 RID: 21970
		private Avatar bindAvatar;

		// Token: 0x040055D3 RID: 21971
		private bool isPlayer;

		// Token: 0x040055D4 RID: 21972
		public static string PlayerUseWeaponMsgKey = "PlayerUseWeapon";

		// Token: 0x040055D5 RID: 21973
		public static string NPCUseWeaponMsgKey = "NPCUseWeapon";

		// Token: 0x040055D6 RID: 21974
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

		// Token: 0x040055D7 RID: 21975
		private static Dictionary<int, Color> EquipFightShowLightColorDict = new Dictionary<int, Color>
		{
			{
				0,
				Color.white
			},
			{
				1,
				Color.white
			},
			{
				2,
				Color.white
			},
			{
				3,
				Color.white
			},
			{
				4,
				Color.white
			},
			{
				5,
				Color.white
			},
			{
				6,
				Color.white
			},
			{
				7,
				Color.white
			}
		};
	}
}
