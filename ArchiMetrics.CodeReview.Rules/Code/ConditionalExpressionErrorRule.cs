namespace ArchiMetrics.CodeReview.Rules.Code
{
	using System.Linq;
	using ArchiMetrics.Common.CodeReview;
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.CSharp;
	using Microsoft.CodeAnalysis.CSharp.Syntax;

	internal class ConditionalExpressionErrorRule : CodeEvaluationBase
	{
		public override SyntaxKind EvaluatedKind
		{
			get
			{
				return SyntaxKind.MethodDeclaration;
			}
		}

		public override string Title
		{
			get
			{
				return "Conditional Expressions";
			}
		}

		public override string Suggestion
		{
			get
			{
				return "Consider replacing the condition with an explicit if statement.";
			}
		}

		public override CodeQuality Quality
		{
			get
			{
				return CodeQuality.NeedsRefactoring;
			}
		}

		public override QualityAttribute QualityAttribute
		{
			get
			{
				return QualityAttribute.Conformance;
			}
		}

		public override ImpactLevel ImpactLevel
		{
			get
			{
				return ImpactLevel.Member;
			}
		}

		protected override EvaluationResult EvaluateImpl(SyntaxNode node)
		{
			var methodDeclaration = (MethodDeclarationSyntax)node;
			var conditionalExpressions = methodDeclaration.DescendantNodes()
														  .Where(n => n.IsKind(SyntaxKind.ConditionalExpression))
														  .ToArray();
			if (conditionalExpressions.Any())
			{
				return new EvaluationResult
						   {
							   Snippet = string.Join("\r\n", conditionalExpressions.Select(n => n.ToFullString())), 
							   ErrorCount = conditionalExpressions.Length
						   };
			}

			return null;
		}
	}
}
