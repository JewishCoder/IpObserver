using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace IPObserver.DataStorage
{
	internal sealed class LambdaConverter : ExpressionVisitor
	{
		/// <summary>Словарь типов предназначенных для преобразования.</summary>
		/// <remarks>
		/// Key - исходный тип.
		/// Value - тип в который нужно преобразовать.
		/// </remarks>
		internal Dictionary<Type, Type> TypeTransformation { get; set; }

		/// <summary>Словарь параметров лямбды выражения.</summary>
		/// <remarks>
		/// Key - исходный параметр.
		/// Value - параметр в который необходимо преобразовать.
		/// </remarks>
		internal Dictionary<ParameterExpression, ParameterExpression> Parameters { get; }

		internal LambdaConverter()
		{
			TypeTransformation = new Dictionary<Type, Type>();
			Parameters = new Dictionary<ParameterExpression, ParameterExpression>();
		}

		/// <summary>Проходит по всему дереву выражений.</summary>
		/// <param name="node">Дерево выражений.</param>
		/// <returns>Возвращает новое дерево возвращение.</returns>
		public override Expression Visit(Expression node)
		{
			switch(node)
			{
				//Если выражение является лямбдой.
				case LambdaExpression lambda:
					return Expression.Lambda(Visit(lambda.Body), ModifyParameters(lambda.Parameters));
				// Если выражение является методом, то происходит модификация
				case MethodCallExpression call when call.Object != null:
					return Expression.Call(Visit(call.Object), ModifyMethod(call.Method), Visit(call.Arguments));
				// Если выражение является статическим методом, то происходит модификация
				case MethodCallExpression call when call.Object == null:
					return Expression.Call(ModifyMethod(call.Method), Visit(call.Arguments));
				//Если выражение является свойством, то происходит модификация.
				case MemberExpression member:
					return Expression.MakeMemberAccess(Visit(member.Expression), ModifyMemberInfo(member.Member));
				//Если выражение является параметром, то происходит модификация.
				case ParameterExpression parameter:
					return GetParameter(parameter);
				//Если выражение является бинарным, то происходит модификация.
				case BinaryExpression binaryExpression:
					return BinaryReducer(binaryExpression);
				default: return base.Visit(node);
			}
		}

		/// <summary>Пытается получить константные выражение в виде конечного результата.</summary>
		/// <param name="binaryExpression">Бинарное выражение.</param>
		/// <returns>Возвращает с оптимизированное выражение.</returns>
		private Expression BinaryReducer(BinaryExpression binaryExpression)
		{
			Expression left;
			Expression right;

			var leftLambda = Expression.Lambda(binaryExpression.Left);
			try
			{
				//Если это константа, то произойдет вызов методов, что бы получить конечное значение.
				//При не удаче идет по дереву дальше.
				var lamda = leftLambda.Compile();
				left = Expression.Convert(Expression.Constant(lamda.DynamicInvoke()), leftLambda.ReturnType);
			}
			catch
			{
				left = Visit(binaryExpression.Left);
			}
			var rightLambda = Expression.Lambda(binaryExpression.Right);
			try
			{
				var rl = rightLambda.Compile();
				right = Expression.Convert(Expression.Constant(rl.DynamicInvoke()), rightLambda.ReturnType);
			}
			catch
			{
				right = Visit(binaryExpression.Right);
			}
			return Expression.MakeBinary(binaryExpression.NodeType, left, right);
		}

		/// <summary>Возвращает необходимый параметр.</summary>
		/// <param name="originalParameters">Параметр текущей лямбды.</param>
		/// <returns>Возвращает параметр для новой лямбды.</returns>
		private ParameterExpression GetParameter(ParameterExpression originalParameters)
		{
			if(Parameters.TryGetValue(originalParameters, out var p)) return p;

			var parameters = Expression.Parameter(TransformType(originalParameters.Type), originalParameters.Name);
			Parameters.Add(originalParameters, parameters);
			return parameters;
		}

		/// <summary>Преобразует тип для свойства.</summary>
		/// <param name="originalMemberInfo">исходная информация о свойстве.</param>
		/// <returns>Возвращает преобразованную информацию о свойстве.</returns>
		private MemberInfo ModifyMemberInfo(MemberInfo originalMemberInfo)
		{
			var membersInfo = TransformType(originalMemberInfo.DeclaringType)
				.GetMember(originalMemberInfo.Name);
	
			return membersInfo.Length == 0 ? originalMemberInfo : membersInfo[0];
		}

		/// <summary>Преобразует информацию о методе для нужного типа. <seealso cref="TypeTransformation"/></summary>
		/// <param name="methodInfo">Исходная информация от методе.</param>
		/// <returns>Возвращает преобразованную информацию о методе.</returns>
		private MethodInfo ModifyMethod(MethodInfo methodInfo)
		{
			if(methodInfo.IsGenericMethod)
			{
				return methodInfo.DeclaringType
					.GetMethods()
					.Where(m => m.Name == methodInfo.Name)
					.Select(m => m.MakeGenericMethod(TransformTypes(methodInfo.GetGenericArguments())))
					.FirstOrDefault(m => GetMethodsArgs(m).SequenceEqual(TransformTypes(GetMethodsArgs(methodInfo))));
			}

			return methodInfo.DeclaringType
					.GetMethods()
					.Where(m => m.Name == methodInfo.Name)
					.FirstOrDefault(m => GetMethodsArgs(m).SequenceEqual(TransformTypes(GetMethodsArgs(methodInfo))));
		}

		/// <summary>Получает массив типов аргументов метода.</summary>
		/// <param name="methodInfo">Информация о методе.</param>
		/// <returns>Возвращает массив типов аргументов метода.</returns>
		private Type[] GetMethodsArgs(MethodInfo methodInfo)
		{
			return methodInfo.GetParameters()
				.Select(p => p.ParameterType)
				.ToArray();
		}

		/// <summary>Добавление параметров, если это необходимо.</summary>
		/// <param name="originalParameters">Исходные параметры.</param>
		/// <returns>Возвращает новые параметры.</returns>
		private IReadOnlyList<ParameterExpression> ModifyParameters(IReadOnlyList<ParameterExpression> originalParameters)
		{
			return originalParameters.Select(GetParameter).ToList();
		}
		

		/// <summary>Преобразует тип параметра для новой лямбды. <seealso cref="TypeTransformation"/>.</summary>
		/// <param name="originalType">Исходный тип.</param>
		/// <returns>Новый тип.</returns>
		private Type TransformType(Type originalType)
		{
			if(originalType.IsGenericType)
			{
				originalType = originalType
					.GetGenericTypeDefinition()
					.MakeGenericType(TransformTypes(originalType.GenericTypeArguments));
			}

			if(TypeTransformation.TryGetValue(originalType, out var transformType))
			{
				return transformType;
			}
			
			return originalType;
		}

		/// <summary>Преобразует массив типов. <seealso cref="TransformType"/>.</summary>
		/// <param name="originalTypes">Исходные типы.</param>
		/// <returns>Возвращает массив преобразованных типов.</returns>
		private Type[] TransformTypes(Type[] originalTypes)
		{
			return originalTypes.Select(TransformType).ToArray();
		}
	}
}
