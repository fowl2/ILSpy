using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ICSharpCode.Decompiler.DebugInfo
{
	public struct Variable
	{
		public Variable(int index, string name)
		{
			Index = index;
			Name = name;
		}

		public int Index { get; }
		public string Name { get; }
	}

	public interface IDebugInfoProvider
	{
		string Description { get; }
		IList<SequencePoint> GetSequencePoints(MethodDefinitionHandle method);
		IList<Variable> GetVariables(MethodDefinitionHandle method);
		bool TryGetName(MethodDefinitionHandle method, int index, out string name);
		string SourceFileName { get; }
	}

	public sealed class NullDebugInfoProvider : IDebugInfoProvider
	{
		public static NullDebugInfoProvider Instance { get; } = new NullDebugInfoProvider("Not Available");

		public NullDebugInfoProvider(string description, string sourceFileName = null)
		{
			this.Description = description ?? throw new ArgumentNullException(nameof(description));
			this.SourceFileName = sourceFileName;
		}

		public string Description { get; }

		IList<SequencePoint> IDebugInfoProvider.GetSequencePoints(MethodDefinitionHandle method)
			=> Array.Empty<SequencePoint>();

		IList<Variable> IDebugInfoProvider.GetVariables(MethodDefinitionHandle method)
			=> Array.Empty<Variable>();

		bool IDebugInfoProvider.TryGetName(MethodDefinitionHandle method, int index, out string name)
		{
			name = null;
			return false;
		}

		public string SourceFileName { get; }
	}
}
