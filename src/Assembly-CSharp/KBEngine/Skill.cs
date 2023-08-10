using UnityEngine;
using YSGame;

namespace KBEngine;

public class Skill
{
	public string name;

	public string descr;

	public int id;

	public float canUseDistMin;

	public float canUseDistMax = 3f;

	public float coolTime = 0.5f;

	public Skill_Type Skill_Type;

	public Skill_DisplayType displayType = Skill_DisplayType.SkillDisplay_Event_Bullet;

	public string skillEffect;

	public float restCoolTimer;

	public int validCast(Entity caster, SCObject target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if (Vector3.Distance(target.getPosition(), caster.position) > canUseDistMax)
		{
			return 1;
		}
		if (restCoolTimer < coolTime)
		{
			return 2;
		}
		if ((sbyte)caster.getDefinedProperty("state") == 1)
		{
			return 3;
		}
		UI_Target uITarget = World.instance.getUITarget();
		if (!uITarget.canAtack() && jsonData.instance.skillJsonData[string.Concat(id)]["script"].str == "SkillAttack")
		{
			return 4;
		}
		if (uITarget.canAtack() && jsonData.instance.skillJsonData[string.Concat(id)]["script"].str == "SkillHeal")
		{
			return 5;
		}
		return 0;
	}

	public int validCast(Entity caster)
	{
		if (restCoolTimer < coolTime)
		{
			return 2;
		}
		if ((sbyte)caster.getDefinedProperty("state") == 1)
		{
			return 3;
		}
		return 0;
	}

	public void updateTimer(float second)
	{
		restCoolTimer += second;
	}

	public void use(Entity caster, SCObject target)
	{
		if (Skill_Type == Skill_Type.Directional_skill)
		{
			caster.cellCall("useTargetSkill", id, ((SCEntityObject)target).targetID);
		}
		SkillBox.inst.showUpdateCD(id);
		restCoolTimer = 0f;
	}

	public void use(Entity caster)
	{
		if (jsonData.instance.skillJsonData[string.Concat(id)]["script"].str == "SkillSelf")
		{
			caster.cellCall("useSelfSkill", id);
		}
		else
		{
			caster.cellCall("usePostionSkill", id);
		}
		SkillBox.inst.showUpdateCD(id);
		restCoolTimer = 0f;
	}

	public void displaySkill(Entity caster, Entity target)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (!ResManager.inst.CheckHasSkillEffect(skillEffect))
		{
			YSFuncList.Ints.Continue();
		}
		else if (displayType == Skill_DisplayType.SkillDisplay_Event_Bullet)
		{
			Object obj = Object.Instantiate(ResManager.inst.LoadSkillEffect(skillEffect));
			EffectFlying component = ((GameObject)((obj is GameObject) ? obj : null)).GetComponent<EffectFlying>();
			if (Object.op_Implicit((Object)(object)component))
			{
				component.FromPos = caster.position;
				component.ToPos = target.position;
			}
		}
		else if (displayType == Skill_DisplayType.SkillDisplay_Event_Effect)
		{
			_ = target.position;
			Object obj2 = Object.Instantiate(ResManager.inst.LoadSkillEffect(skillEffect));
			Object obj3 = ((obj2 is GameObject) ? obj2 : null);
			((GameObject)obj3).transform.parent = ((GameObject)target.renderObj).transform;
			((GameObject)obj3).transform.localPosition = Vector3.zero;
			((GameObject)obj3).transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		}
	}

	public void displaySkill(Entity caster)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (displayType == Skill_DisplayType.SkillDisplay_Event_Bullet)
		{
			Object obj = Object.Instantiate(ResManager.inst.LoadSkillEffect(skillEffect));
			Object obj2 = ((obj is GameObject) ? obj : null);
			EffectFlying component = ((GameObject)obj2).GetComponent<EffectFlying>();
			if (Object.op_Implicit((Object)(object)component))
			{
				component.FromPos = caster.position;
				Transform component2 = ((GameObject)caster.renderObj).GetComponent<Transform>();
				Vector3 val = Quaternion.Euler(0f, -90f, 0f) * component2.rotation * new Vector3(component.MaxDistance, 0f, 0f) + component2.position;
				Debug.DrawLine(val, Vector3.zero, Color.red);
				component.ToPos = val;
			}
			SkillTrigge component3 = ((GameObject)obj2).GetComponent<SkillTrigge>();
			if (Object.op_Implicit((Object)(object)component3))
			{
				component3.caster = caster;
				component3.skillID = id;
			}
		}
	}

	public void cast(object renderObj, float distance)
	{
	}
}
