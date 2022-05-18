using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x02001136 RID: 4406
	public abstract class HardwiredMethodMemberDescriptor : FunctionMemberDescriptorBase
	{
		// Token: 0x06006A9A RID: 27290 RVA: 0x00290FD4 File Offset: 0x0028F1D4
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanExecute, obj);
			List<int> list = null;
			object[] pars = base.BuildArgumentList(script, obj, context, args, out list);
			object obj2 = this.Invoke(script, obj, pars, this.CalcArgsCount(pars));
			return DynValue.FromObject(script, obj2);
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x00291014 File Offset: 0x0028F214
		private int CalcArgsCount(object[] pars)
		{
			int num = pars.Length;
			for (int i = 0; i < pars.Length; i++)
			{
				if (base.Parameters[i].HasDefaultValue && pars[i] is DefaultValue)
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x06006A9C RID: 27292
		protected abstract object Invoke(Script script, object obj, object[] pars, int argscount);
	}
}
