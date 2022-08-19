using System;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x0200066B RID: 1643
	[Serializable]
	public class EntityAnimation
	{
		// Token: 0x06003441 RID: 13377 RVA: 0x0016D882 File Offset: 0x0016BA82
		public void Initialize(AIBrain brain)
		{
			this.m_Brain = brain;
			this.m_Animator = this.m_Brain.GetComponent<Animator>();
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x0016D89C File Offset: 0x0016BA9C
		public bool ParameterExists(string paramName)
		{
			if (this.m_Animator.parameterCount == 0)
			{
				return false;
			}
			bool result = false;
			AnimatorControllerParameter[] parameters = this.m_Animator.parameters;
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].name == paramName)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x0016D8E7 File Offset: 0x0016BAE7
		public void SetTrigger(string paramName)
		{
			if (!this.ParameterExists(paramName))
			{
				return;
			}
			this.m_Animator.SetTrigger(paramName);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x0016D8FF File Offset: 0x0016BAFF
		public void ToggleBool(string paramName, bool value)
		{
			if (!this.ParameterExists(paramName))
			{
				return;
			}
			this.m_Animator.SetBool(paramName, value);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x0016D918 File Offset: 0x0016BB18
		public bool IsBoolToggled(string paramName)
		{
			if (!this.ParameterExists(paramName))
			{
				Debug.LogError("Parameter with name " + paramName + " does not exist.");
				return false;
			}
			return this.m_Animator.GetBool(paramName);
		}

		// Token: 0x04002E7A RID: 11898
		private AIBrain m_Brain;

		// Token: 0x04002E7B RID: 11899
		private Animator m_Animator;
	}
}
