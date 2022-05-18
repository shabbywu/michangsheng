using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010FD RID: 4349
	public class ArrayMemberDescriptor : ObjectCallbackMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x060068EC RID: 26860 RVA: 0x00047E9C File Offset: 0x0004609C
		public ArrayMemberDescriptor(string name, bool isSetter, ParameterDescriptor[] indexerParams) : base(name, isSetter ? new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerSet) : new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerGet), indexerParams)
		{
			this.m_IsSetter = isSetter;
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x00047ECA File Offset: 0x000460CA
		public ArrayMemberDescriptor(string name, bool isSetter) : base(name, isSetter ? new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerSet) : new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerGet))
		{
			this.m_IsSetter = isSetter;
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x0028C8A8 File Offset: 0x0028AAA8
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

		// Token: 0x060068EF RID: 26863 RVA: 0x0028C958 File Offset: 0x0028AB58
		private static int[] BuildArrayIndices(CallbackArguments args, int count)
		{
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = args.AsInt(i, "userdata_array_indexer");
			}
			return array;
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0028C988 File Offset: 0x0028AB88
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

		// Token: 0x060068F1 RID: 26865 RVA: 0x0028C9DC File Offset: 0x0028ABDC
		private static object ArrayIndexerGet(object arrayObj, ScriptExecutionContext ctx, CallbackArguments args)
		{
			Array array = (Array)arrayObj;
			int[] indices = ArrayMemberDescriptor.BuildArrayIndices(args, args.Count);
			return array.GetValue(indices);
		}

		// Token: 0x04006021 RID: 24609
		private bool m_IsSetter;
	}
}
