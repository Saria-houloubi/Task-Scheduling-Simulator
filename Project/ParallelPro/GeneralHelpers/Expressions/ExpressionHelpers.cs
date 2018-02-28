
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GeneralHelpers
{
    /// <summary>
    /// Helper for Expressions
    /// </summary>
   public static class ExpressionHelpers
    {
        /// <summary>
        /// Complies an expression and gets the function return value
        /// </summary>
        /// <typeparam name="T">The type of the return value</typeparam>
        /// <param name="lambda">The expression to compile</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda,T value)
        {

            //Converts a ( lambda () => some.property ), to ( some.property )
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // Get the property infomrantion so we can set it
            var propertyInfo = (PropertyInfo)expression.Member;

            // Getting the target to set the value to
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            //setting the value
            propertyInfo.SetValue(target, value);
        }
    }
}
