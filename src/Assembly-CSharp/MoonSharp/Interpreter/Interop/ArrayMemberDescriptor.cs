using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D19 RID: 3353
	public class ArrayMemberDescriptor : ObjectCallbackMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x06005DBD RID: 23997 RVA: 0x00263D30 File Offset: 0x00261F30
		public ArrayMemberDescriptor(string name, bool isSetter, ParameterDescriptor[] indexerParams) : base(name, isSetter ? new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerSet) : new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerGet), indexerParams)
		{
			this.m_IsSetter = isSetter;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x00263D5E File Offset: 0x00261F5E
		public ArrayMemberDescriptor(string name, bool isSetter) : base(name, isSetter ? new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerSet) : new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerGet))
		{
			this.m_IsSetter = isSetter;
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x00263D8C File Offset: 0x00261F8C
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(base.Name));
			t.Set("setter", DynValue.NewBoolean(this.m_IsSetter));
			if (base.Parameters != null)
			{
				DynValue dynValue = DynValue.NewPrimeTable();
				t.Set("params", dynValue);
				int num = 0;
				foreach (ParameterDescriptor parameterDescriptor in base.Parameters)
				{
					DynValue dynValue2 = DynValue.NewPrimeTable();
					dynValue.Table.Set(++num, dynValue2);
					parameterDescriptor.PrepareForWiring(dynValue2.Table);
				}
			}
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x00263E3C File Offset: 0x0026203C
		private static int[] BuildArrayIndices(CallbackArguments args, int count)
		{
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = args.AsInt(i, "userdata_array_indexer");
			}
			return array;
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x00263E6C File Offset: 0x0026206C
		private static object ArrayIndexerSet(object arrayObj, ScriptExecutionContext ctx, CallbackArguments args)
		{
			Array array = (Array)arrayObj;
			int[] indices = ArrayMemberDescriptor.BuildArrayIndices(args, args.Count - 1);
			DynValue value = args[args.Count - 1];
			Type elementType = array.GetType().GetElementType();
			object value2 = ScriptToClrConversions.DynValueToObjectOfType(value, elementType, null, false);
			array.SetValue(value2, indices);
			return DynValue.Void;
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x00263EC0 File Offset: 0x002620C0
		private static object ArrayIndexerGet(object arrayObj, ScriptExecutionContext ctx, CallbackArguments args)
		{
			Array array = (Array)arrayObj;
			int[] indices = ArrayMemberDescriptor.BuildArrayIndices(args, args.Count);
			return array.GetValue(indices);
		}

		// Token: 0x0400540A RID: 21514
		private bool m_IsSetter;
	}
}
