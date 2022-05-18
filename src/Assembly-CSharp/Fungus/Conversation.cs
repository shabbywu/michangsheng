using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011FD RID: 4605
	[CommandInfo("Narrative", "Conversation", "Do multiple say and portrait commands in a single block of text. Format is: [character] [portrait] [stage position] [hide] [<<< | >>>] [clear | noclear] [wait | nowait] [fade | nofade] [: Story text]", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Conversation : Command
	{
		// Token: 0x060070BB RID: 28859 RVA: 0x0004C8F9 File Offset: 0x0004AAF9
		protected virtual void Start()
		{
			this.conversationManager.PopulateCharacterCache();
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x0004C906 File Offset: 0x0004AB06
		protected virtual IEnumerator DoConversation()
		{
			string conv = this.GetFlowchart().SubstituteVariables(this.conversationText.Value);
			this.conversationManager.ClearPrev = this.clearPrevious;
			this.conversationManager.WaitForInput = this.waitForInput;
			this.conversationManager.FadeDone = this.fadeWhenDone;
			this.conversationManager.WaitForSeconds = this.waitForSeconds;
			yield return base.StartCoroutine(this.conversationManager.DoConversation(conv));
			this.Continue();
			yield break;
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x0004C915 File Offset: 0x0004AB15
		public override void OnEnter()
		{
			base.StartCoroutine(this.DoConversation());
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x0004C924 File Offset: 0x0004AB24
		public override string GetSummary()
		{
			return this.conversationText.Value;
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x002A3208 File Offset: 0x002A1408
		public override bool HasReference(Variable variable)
		{
			return this.clearPrevious.booleanRef == variable || this.waitForInput.booleanRef == variable || this.waitForSeconds.floatRef == variable || this.fadeWhenDone.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006330 RID: 25392
		[SerializeField]
		protected StringDataMulti conversationText;

		// Token: 0x04006331 RID: 25393
		protected ConversationManager conversationManager = new ConversationManager();

		// Token: 0x04006332 RID: 25394
		[SerializeField]
		protected BooleanData clearPrevious = new BooleanData(true);

		// Token: 0x04006333 RID: 25395
		[SerializeField]
		protected BooleanData waitForInput = new BooleanData(true);

		// Token: 0x04006334 RID: 25396
		[Tooltip("a wait for seconds added to each item of the conversation.")]
		[SerializeField]
		protected FloatData waitForSeconds = new FloatData(0f);

		// Token: 0x04006335 RID: 25397
		[SerializeField]
		protected BooleanData fadeWhenDone = new BooleanData(true);
	}
}
