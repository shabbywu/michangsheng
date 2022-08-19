using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x02000D2E RID: 3374
	public abstract class HardwiredMethodMemberDescriptor : FunctionMemberDescriptorBase
	{
		// Token: 0x06005EC8 RID: 24264 RVA: 0x00268A8C File Offset: 0x00266C8C
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanExecute, obj);
			List<int> list = null;
			object[] pars = base.BuildArgumentList(script, obj, context, args, out list);
			object obj2 = this.Invoke(script, obj, pars, this.CalcArgsCount(pars));
			return DynValue.FromObject(script, obj2);
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x00268ACC File Offset: 0x00266CCC
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

		// Token: 0x06005ECA RID: 24266
		protected abstract object Invoke(Script script, object obj, object[] pars, int argscount);
	}
}
