using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IPObserver.DataStorage
{
	/// <summary>Заменяет параметры в дереве выражения.</summary>
	internal sealed class LambdaParameterPeplacer : ExpressionVisitor
	{
		/// <summary>Параметры на которые нужно заменить.</summary>
		internal List<ParameterExpression> ReplacmentParameters { get; set; }

		internal LambdaParameterPeplacer()
		{
			ReplacmentParameters = new List<ParameterExpression>();
		}

		public override Expression Visit(Expression node)
		{
			switch(node)
			{
				case ParameterExpression parameter:
					return GetParameter(parameter);
				default: return base.Visit(node);
			}
		}

		/// <summary>Находит параметр в списке параметров для замены, либо возвращает текущий.</summary>
		/// <param name="originalParameters">Текущий параметр.</param>
		/// <returns>Возвращает замененный параметр.</returns>
		private ParameterExpression GetParameter(ParameterExpression originalParameters)
		{
			var replaceParameter = ReplacmentParameters.FirstOrDefault(x => x.Name == originalParameters.Name);

			if(replaceParameter == null) return originalParameters;
			return replaceParameter;
		}
	}
}
