using UnityEngine;

public class CharacterSkillDeployer : MonoBehaviour
{
	public GameObject[] Skill;

	public int[] ManaCost;

	public Texture2D[] SkillIcon;

	private CharacterStatus character;

	private CharacterAttack characterAttack;

	private bool attackingSkill;

	public int indexSkill;

	private void Start()
	{
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<CharacterStatus>()))
		{
			character = ((Component)this).gameObject.GetComponent<CharacterStatus>();
		}
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<CharacterAttack>()))
		{
			characterAttack = ((Component)this).gameObject.GetComponent<CharacterAttack>();
		}
	}

	public void DeploySkill(int index)
	{
		indexSkill = index;
		DeploySkill();
	}

	public void DeploySkill()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (Skill.Length != 0 && (Object)(object)Skill[indexSkill] != (Object)null && (Object)(object)character != (Object)null && character.SP >= ManaCost[indexSkill])
		{
			Object.Instantiate<GameObject>(Skill[indexSkill], ((Component)this).transform.position, ((Component)this).transform.rotation).transform.forward = ((Component)this).transform.forward;
			character.SP -= ManaCost[indexSkill];
		}
	}

	public void DeployWithAttacking(int index)
	{
		indexSkill = index;
		attackingSkill = true;
	}

	public void DeployWithAttacking()
	{
		attackingSkill = true;
	}

	private void Update()
	{
		if (attackingSkill && characterAttack.Activated)
		{
			DeploySkill();
			attackingSkill = false;
			characterAttack.Activated = false;
		}
	}
}
