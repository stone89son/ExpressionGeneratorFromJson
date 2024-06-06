using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ObjectHelper
    {
        public static Func<T, TResult> MakeGetter<T, TResult>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "obj");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<T, TResult>>(property, parameter);

            return lambda.Compile();
        }

        public static Action<T, TValue> MakeSetter<T, TValue>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "obj");
            var value = Expression.Parameter(typeof(TValue), "value");
            var property = Expression.Property(parameter, propertyName);
            var assign = Expression.Assign(property, value);
            var lambda = Expression.Lambda<Action<T, TValue>>(assign, parameter, value);

            return lambda.Compile();
        }
    }
}
