using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WzorceLib;

namespace WzorceProjekoweZadania.ExpressionTrees
{
    public class ExpressionTree : IExampleRunnable
    {
        public void Run()
        {
            Console.WriteLine("Lambda : (x,y) => 3x^2 + 4y^2");
            Func<double, double, double> lambdaFunc = (x, y) => 3 * x * x + 4 * y * y;

            double a = 10;
            double b = 12;

            Console.WriteLine(lambdaFunc);

            // dynamicznie zbudowana lambdaFunc

            ParameterExpression paramX = Expression.Parameter(typeof(int), "x");
            ParameterExpression paramY = Expression.Parameter(typeof(int), "y");
            ConstantExpression constantA = Expression.Constant(3);
            ConstantExpression constantB = Expression.Constant(4);

            LambdaExpression dynamicLambdaExpression;

            dynamicLambdaExpression = Expression.Lambda(
                Expression.Add(
                    Expression.Multiply(constantA, Expression.Multiply(paramX, paramX)),
                    Expression.Multiply(constantB, Expression.Multiply(paramY, paramY))
                    ), new List<ParameterExpression>()
                    {
                        paramX,
                        paramY
                    }
                );

            var dynamicLambdaFunc = dynamicLambdaExpression.Compile();

            Console.WriteLine(dynamicLambdaExpression);

            Console.WriteLine("Static result : {0}", lambdaFunc(2,2));
            Console.WriteLine("Dynamic result : {0}", dynamicLambdaFunc.DynamicInvoke(2,2));

        }
    }
}
