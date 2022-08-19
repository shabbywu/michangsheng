using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA7 RID: 3495
	public class EvaluateResponseBody : ResponseBody
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600634F RID: 25423 RVA: 0x0027A8D7 File Offset: 0x00278AD7
		// (set) Token: 0x06006350 RID: 25424 RVA: 0x0027A8DF File Offset: 0x00278ADF
		public string result { get; private set; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06006351 RID: 25425 RVA: 0x0027A8E8 File Offset: 0x00278AE8
		// (set) Token: 0x06006352 RID: 25426 RVA: 0x0027A8F0 File Offset: 0x00278AF0
		public string type { get; set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06006353 RID: 25427 RVA: 0x0027A8F9 File Offset: 0x00278AF9
		// (set) Token: 0x06006354 RID: 25428 RVA: 0x0027A901 File Offset: 0x00278B01
		public int variablesReference { get; private set; }

		// Token: 0x06006355 RID: 25429 RVA: 0x0027A90A File Offset: 0x00278B0A
		public EvaluateResponseBody(string value, int reff = 0)
		{
			this.result = value;
			this.variablesReference = reff;
		}
	}
}
