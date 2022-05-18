using System;
using UnityEngine;
using YSGame;

namespace KBEngine
{
	// Token: 0x02001049 RID: 4169
	public class Skill
	{
		// Token: 0x0600642C RID: 25644 RVA: 0x00280E94 File Offset: 0x0027F094
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

		// Token: 0x0600642D RID: 25645 RVA: 0x00044EB0 File Offset: 0x000430B0
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

		// Token: 0x0600642E RID: 25646 RVA: 0x00044ED8 File Offset: 0x000430D8
		public void updateTimer(float second)
		{
			this.restCoolTimer += second;
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x00280F78 File Offset: 0x0027F178
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

		// Token: 0x06006430 RID: 25648 RVA: 0x00280FDC File Offset: 0x0027F1DC
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

		// Token: 0x06006431 RID: 25649 RVA: 0x00281080 File Offset: 0x0027F280
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

		// Token: 0x06006432 RID: 25650 RVA: 0x0028116C File Offset: 0x0027F36C
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

		// Token: 0x06006433 RID: 25651 RVA: 0x000042DD File Offset: 0x000024DD
		public void cast(object renderObj, float distance)
		{
		}

		// Token: 0x04005DC7 RID: 24007
		public string name;

		// Token: 0x04005DC8 RID: 24008
		public string descr;

		// Token: 0x04005DC9 RID: 24009
		public int id;

		// Token: 0x04005DCA RID: 24010
		public float canUseDistMin;

		// Token: 0x04005DCB RID: 24011
		public float canUseDistMax = 3f;

		// Token: 0x04005DCC RID: 24012
		public float coolTime = 0.5f;

		// Token: 0x04005DCD RID: 24013
		public Skill_Type Skill_Type;

		// Token: 0x04005DCE RID: 24014
		public Skill_DisplayType displayType = Skill_DisplayType.SkillDisplay_Event_Bullet;

		// Token: 0x04005DCF RID: 24015
		public string skillEffect;

		// Token: 0x04005DD0 RID: 24016
		public float restCoolTimer;
	}
}
