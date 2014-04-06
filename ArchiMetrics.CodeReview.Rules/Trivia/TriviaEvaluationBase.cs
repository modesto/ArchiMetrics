namespace ArchiMetrics.CodeReview.Rules.Trivia
{
	using ArchiMetrics.CodeReview.Rules.Code;
	using ArchiMetrics.Common.CodeReview;
	using Microsoft.CodeAnalysis;

	internal abstract class TriviaEvaluationBase : EvaluationBase, ITriviaEvaluation
	{
		public EvaluationResult Evaluate(SyntaxTrivia node)
		{
			var result = EvaluateImpl(node);
			if (result != null)
			{
				var sourceTree = node.GetLocation().SourceTree;
				var filePath = sourceTree.FilePath;
				var typeDefinition = GetNodeType(node.Token.Parent);
				var unitNamespace = GetNamespace(node.Token.Parent);
				if (result.ErrorCount == 0)
				{
					result.ErrorCount = 1;
				}

				result.LinesOfCodeAffected = 0;
				result.Namespace = unitNamespace;
				result.TypeKind = typeDefinition.Item1;
				result.TypeName = typeDefinition.Item2;
				result.Title = Title;
				result.Suggestion = Suggestion;
				result.Quality = Quality;
				result.QualityAttribute = QualityAttribute;
				result.ImpactLevel = ImpactLevel;
				result.FilePath = filePath;
			}

			return result;
		}

		protected abstract EvaluationResult EvaluateImpl(SyntaxTrivia node);
	}
}
