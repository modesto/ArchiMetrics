namespace ArchiMetrics.CodeReview.Rules.Code
{
	using ArchiMetrics.Common.CodeReview;
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.CSharp;
	using Microsoft.CodeAnalysis.CSharp.Syntax;

	internal class NoPublicConstantRule : CodeEvaluationBase
	{
		public override SyntaxKind EvaluatedKind
		{
			get
			{
				return SyntaxKind.FieldDeclaration;
			}
		}

		public override string Title
		{
			get
			{
				return "No Public Constants";
			}
		}

		public override string Suggestion
		{
			get
			{
				return "Expose public constants as public static readonly instead in order to avoid that they get compiled into a calling assembly.";
			}
		}

		public override CodeQuality Quality
		{
			get
			{
				return CodeQuality.Broken;
			}
		}

		public override QualityAttribute QualityAttribute
		{
			get
			{
				return QualityAttribute.Modifiability;
			}
		}

		public override ImpactLevel ImpactLevel
		{
			get
			{
				return ImpactLevel.Project;
			}
		}

		protected override EvaluationResult EvaluateImpl(SyntaxNode node)
		{
			var syntax = (FieldDeclarationSyntax)node;
			if (syntax.Modifiers.Any(SyntaxKind.PublicKeyword) && syntax.Modifiers.Any(SyntaxKind.ConstKeyword))
			{
				return new EvaluationResult
						   {
							   Snippet = node.ToFullString()
						   };
			}

			return null;
		}
	}
}
