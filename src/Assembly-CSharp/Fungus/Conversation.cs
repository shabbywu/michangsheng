using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DC4 RID: 3524
	[CommandInfo("Narrative", "Conversation", "Do multiple say and portrait commands in a single block of text. Format is: [character] [portrait] [stage position] [hide] [<<< | >>>] [clear | noclear] [wait | nowait] [fade | nofade] [: Story text]", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Conversation : Command
	{
		// Token: 0x06006442 RID: 25666 RVA: 0x0027E1F1 File Offset: 0x0027C3F1
		protected virtual void Start()
		{
			this.conversationManager.PopulateCharacterCache();
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x0027E1FE File Offset: 0x0027C3FE
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

		// Token: 0x06006444 RID: 25668 RVA: 0x0027E20D File Offset: 0x0027C40D
		public override void OnEnter()
		{
			base.StartCoroutine(this.DoConversation());
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x0027E21C File Offset: 0x0027C41C
		public override string GetSummary()
		{
			return this.conversationText.Value;
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x0027E22C File Offset: 0x0027C42C
		public override bool HasReference(Variable variable)
		{
			return this.clearPrevious.booleanRef == variable || this.waitForInput.booleanRef == variable || this.waitForSeconds.floatRef == variable || this.fadeWhenDone.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04005634 RID: 22068
		[SerializeField]
		protected StringDataMulti conversationText;

		// Token: 0x04005635 RID: 22069
		protected ConversationManager conversationManager = new ConversationManager();

		// Token: 0x04005636 RID: 22070
		[SerializeField]
		protected BooleanData clearPrevious = new BooleanData(true);

		// Token: 0x04005637 RID: 22071
		[SerializeField]
		protected BooleanData waitForInput = new BooleanData(true);

		// Token: 0x04005638 RID: 22072
		[Tooltip("a wait for seconds added to each item of the conversation.")]
		[SerializeField]
		protected FloatData waitForSeconds = new FloatData(0f);

		// Token: 0x04005639 RID: 22073
		[SerializeField]
		protected BooleanData fadeWhenDone = new BooleanData(true);
	}
}
