// Copyright (c) 2021 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

#nullable enable

using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.ILSpy.Options;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.ILSpy.ViewModels;

namespace ICSharpCode.ILSpy.Metadata
{
	sealed class DebugInfoProviderTreeNode : ILSpyTreeNode
	{
		readonly Decompiler.DebugInfo.IDebugInfoProvider? debugInfoProvider;

		public DebugInfoProviderTreeNode(Decompiler.DebugInfo.IDebugInfoProvider debugInfoProvider)
		{
			this.debugInfoProvider = debugInfoProvider;
		}

		public override object Text => $"Debug Metadata ({debugInfoProvider?.Description ?? "Not Available"})";

		public override object Icon => Images.Library;

		public override void Decompile(Language language, ITextOutput output, DecompilationOptions options)
		{
			if (debugInfoProvider is null)
			{
				language.WriteCommentLine(output, Text.ToString());
				return;
			}

			language.WriteCommentLine(output, debugInfoProvider.GetType().Name);
			language.WriteCommentLine(output, "Source File Name:");
			language.WriteCommentLine(output, debugInfoProvider.SourceFileName);
		}
	}
}
