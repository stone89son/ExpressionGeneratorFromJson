using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Program
    {
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        static void Main()
        {
            var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30 },
            new Person { Name = "Bob", Age = 40 },
            new Person { Name = "Charlie", Age = 35 },
            new Person { Name = "Charlie2", Age = 37 }
        };

            // Biểu thức: p => p.Age > 34
            ParameterExpression paramP = Expression.Parameter(typeof(Person), "p");
            MemberExpression ageProperty = Expression.Property(paramP, "Age");
            ConstantExpression ageValue = Expression.Constant(34);
            BinaryExpression ageCondition = Expression.GreaterThan(ageProperty, ageValue);

            // Biểu thức: p => p.Name.Contains("Charlie")
            MemberExpression nameProperty = Expression.Property(paramP, "Name");
            MethodCallExpression nameContainsMethod = Expression.Call(
                nameProperty,
                typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                Expression.Constant("Charlie")
            );

            // Kết hợp các điều kiện với AND: (p => p.Age > 34) AND (p => p.Name.Contains("Charlie"))
            BinaryExpression combinedCondition = Expression.AndAlso(ageCondition, nameContainsMethod);

            var lambda = Expression.Lambda<Func<Person, bool>>(combinedCondition, paramP).Compile();

            var result = people.Where(lambda).ToList();

            foreach (var person in result)
            {
                Console.WriteLine(person.Name); // Output: Charlie, Charlie2
            }
            Console.ReadKey();
        }
    }
}
