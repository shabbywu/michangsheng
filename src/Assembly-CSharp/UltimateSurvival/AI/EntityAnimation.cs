using System;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x02000976 RID: 2422
	[Serializable]
	public class EntityAnimation
	{
		// Token: 0x06003DED RID: 15853 RVA: 0x0002C969 File Offset: 0x0002AB69
		public void Initialize(AIBrain brain)
		{
			this.m_Brain = brain;
			this.m_Animator = this.m_Brain.GetComponent<Animator>();
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x001B6370 File Offset: 0x001B4570
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

		// Token: 0x06003DEF RID: 15855 RVA: 0x0002C983 File Offset: 0x0002AB83
		public void SetTrigger(string paramName)
		{
			if (!this.ParameterExists(paramName))
			{
				return;
			}
			this.m_Animator.SetTrigger(paramName);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x0002C99B File Offset: 0x0002AB9B
		public void ToggleBool(string paramName, bool value)
		{
			if (!this.ParameterExists(paramName))
			{
				return;
			}
			this.m_Animator.SetBool(paramName, value);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		public bool IsBoolToggled(string paramName)
		{
			if (!this.ParameterExists(paramName))
			{
				Debug.LogError("Parameter with name " + paramName + " does not exist.");
				return false;
			}
			return this.m_Animator.GetBool(paramName);
		}

		// Token: 0x04003815 RID: 14357
		private AIBrain m_Brain;

		// Token: 0x04003816 RID: 14358
		private Animator m_Animator;
	}
}
