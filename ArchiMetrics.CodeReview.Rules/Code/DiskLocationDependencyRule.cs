namespace ArchiMetrics.CodeReview.Rules.Code
{
	using System.Text.RegularExpressions;
	using ArchiMetrics.Common.CodeReview;
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.CSharp;
	using Microsoft.CodeAnalysis.CSharp.Syntax;

	internal class DiskLocationDependencyRule : CodeEvaluationBase
	{
		private static readonly Regex DiskLocationRegex = new Regex(@"\w:\\", RegexOptions.Compiled);

		public override SyntaxKind EvaluatedKind
		{
			get
			{
				return SyntaxKind.SimpleAssignmentExpression;
			}
		}
		
		public override string Title
		{
			get
			{
				return "Disk Location Dependency";
			}
		}

		public override string Suggestion
		{
			get
			{
				return "Replace the dependency on a specific disk location with an abstraction.";
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
				return QualityAttribute.Modifiability | QualityAttribute.Testability;
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
			var assignExpression = (BinaryExpressionSyntax)node;
			var right = assignExpression.Right as LiteralExpressionSyntax;
			if (right != null)
			{
				var assignmentToken = right.Token.ToFullString();
				if (DiskLocationRegex.IsMatch(assignmentToken))
				{
					return new EvaluationResult
							   {
								   Snippet = FindMethodParent(node).ToFullString()
							   };
				}
			}

			return null;
		}
	}
}
