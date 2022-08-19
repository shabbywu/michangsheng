using System;
using UnityEngine;
using YSGame;

namespace KBEngine
{
	// Token: 0x02000C86 RID: 3206
	public class Skill
	{
		// Token: 0x0600596F RID: 22895 RVA: 0x00255BA8 File Offset: 0x00253DA8
		public int validCast(Entity caster, SCObject target)
		{
			if (Vector3.Distance(target.getPosition(), caster.position) > this.canUseDistMax)
			{
				return 1;
			}
			if (this.restCoolTimer < this.coolTime)
			{
				return 2;
			}
			if ((sbyte)caster.getDefinedProperty("state") == 1)
			{
				return 3;
			}
			UI_Target uitarget = World.instance.getUITarget();
			if (!uitarget.canAtack() && jsonData.instance.skillJsonData[string.Concat(this.id)]["script"].str == "SkillAttack")
			{
				return 4;
			}
			if (uitarget.canAtack() && jsonData.instance.skillJsonData[string.Concat(this.id)]["script"].str == "SkillHeal")
			{
				return 5;
			}
			return 0;
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x00255C89 File Offset: 0x00253E89
		public int validCast(Entity caster)
		{
			if (this.restCoolTimer < this.coolTime)
			{
				return 2;
			}
			if ((sbyte)caster.getDefinedProperty("state") == 1)
			{
				return 3;
			}
			return 0;
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x00255CB1 File Offset: 0x00253EB1
		public void updateTimer(float second)
		{
			this.restCoolTimer += second;
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x00255CC4 File Offset: 0x00253EC4
		public void use(Entity caster, SCObject target)
		{
			if (this.Skill_Type == Skill_Type.Directional_skill)
			{
				caster.cellCall("useTargetSkill", new object[]
				{
					this.id,
					((SCEntityObject)target).targetID
				});
			}
			SkillBox.inst.showUpdateCD(this.id);
			this.restCoolTimer = 0f;
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00255D28 File Offset: 0x00253F28
		public void use(Entity caster)
		{
			if (jsonData.instance.skillJsonData[string.Concat(this.id)]["script"].str == "SkillSelf")
			{
				caster.cellCall("useSelfSkill", new object[]
				{
					this.id
				});
			}
			else
			{
				caster.cellCall("usePostionSkill", new object[]
				{
					this.id
				});
			}
			SkillBox.inst.showUpdateCD(this.id);
			this.restCoolTimer = 0f;
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x00255DCC File Offset: 0x00253FCC
		public void displaySkill(Entity caster, Entity target)
		{
			if (!ResManager.inst.CheckHasSkillEffect(this.skillEffect))
			{
				YSFuncList.Ints.Continue();
				return;
			}
			if (this.displayType == Skill_DisplayType.SkillDisplay_Event_Bullet)
			{
				EffectFlying component = (Object.Instantiate(ResManager.inst.LoadSkillEffect(this.skillEffect)) as GameObject).GetComponent<EffectFlying>();
				if (component)
				{
					component.FromPos = caster.position;
					component.ToPos = target.position;
					return;
				}
			}
			else if (this.displayType == Skill_DisplayType.SkillDisplay_Event_Effect)
			{
				Vector3 position = target.position;
				GameObject gameObject = Object.Instantiate(ResManager.inst.LoadSkillEffect(this.skillEffect)) as GameObject;
				gameObject.transform.parent = ((GameObject)target.renderObj).transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			}
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x00255EB8 File Offset: 0x002540B8
		public void displaySkill(Entity caster)
		{
			if (this.displayType == Skill_DisplayType.SkillDisplay_Event_Bullet)
			{
				GameObject gameObject = Object.Instantiate(ResManager.inst.LoadSkillEffect(this.skillEffect)) as GameObject;
				EffectFlying component = gameObject.GetComponent<EffectFlying>();
				if (component)
				{
					component.FromPos = caster.position;
					Transform component2 = ((GameObject)caster.renderObj).GetComponent<Transform>();
					Vector3 vector = Quaternion.Euler(0f, -90f, 0f) * component2.rotation * new Vector3(component.MaxDistance, 0f, 0f) + component2.position;
					Debug.DrawLine(vector, Vector3.zero, Color.red);
					component.ToPos = vector;
				}
				SkillTrigge component3 = gameObject.GetComponent<SkillTrigge>();
				if (component3)
				{
					component3.caster = caster;
					component3.skillID = this.id;
				}
			}
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x00004095 File Offset: 0x00002295
		public void cast(object renderObj, float distance)
		{
		}

		// Token: 0x0400521B RID: 21019
		public string name;

		// Token: 0x0400521C RID: 21020
		public string descr;

		// Token: 0x0400521D RID: 21021
		public int id;

		// Token: 0x0400521E RID: 21022
		public float canUseDistMin;

		// Token: 0x0400521F RID: 21023
		public float canUseDistMax = 3f;

		// Token: 0x04005220 RID: 21024
		public float coolTime = 0.5f;

		// Token: 0x04005221 RID: 21025
		public Skill_Type Skill_Type;

		// Token: 0x04005222 RID: 21026
		public Skill_DisplayType displayType = Skill_DisplayType.SkillDisplay_Event_Bullet;

		// Token: 0x04005223 RID: 21027
		public string skillEffect;

		// Token: 0x04005224 RID: 21028
		public float restCoolTimer;
	}
}
