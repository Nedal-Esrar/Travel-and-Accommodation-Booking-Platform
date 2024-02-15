using System.Linq.Expressions;

namespace TABP.Application.Extensions;

/// <summary>
///   Provides extension methods for <see cref="Expression" /> objects.
/// </summary>
public static class ExpressionExtensions
{
  /// <summary>
  ///   Combines two <see cref="Expression{TDelegate}" /> objects into a single expression
  ///   representing the logical AND operation between them.
  /// </summary>
  /// <typeparam name="T">The type of the parameter in the expressions.</typeparam>
  /// <param name="left">The left-hand side expression.</param>
  /// <param name="right">The right-hand side expression.</param>
  /// <returns>
  ///   A new <see cref="Expression{TDelegate}" /> representing the logical AND operation
  ///   between the provided expressions.
  /// </returns>
  /// <remarks>
  ///   This method combines the bodies of the provided expressions using the logical AND (&&) operator.
  ///   It creates a new lambda expression with the combined body and the parameters from the left expression.
  /// </remarks>
  public static Expression<Func<T, bool>> And<T>(
    this Expression<Func<T, bool>> left,
    Expression<Func<T, bool>> right)
  {
    var andExpression = Expression.AndAlso(left.Body,
      Expression.Invoke(right, left.Parameters[0]));

    var lambda = Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters);

    return lambda;
  }
}