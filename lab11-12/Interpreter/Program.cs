using System;
using System.Collections.Generic;

namespace InterpreterTask
{
    class Context
    {
        Dictionary<string, int> variables;

        public Context()
        {
            variables = new Dictionary<string, int>();
        }

        public int GetVariable(string name)
        {
            return variables[name];
        }

        public void SetVariable(string name, int value)
        {
            if (variables.ContainsKey(name))
                variables[name] = value;
            else
                variables.Add(name, value);
        }
    }

    interface IExpression
    {
        int Interpret(Context context);
    }

    class NumberExpression : IExpression
    {
        string name;

        public NumberExpression(string variableName)
        {
            name = variableName;
        }

        public int Interpret(Context context)
        {
            return context.GetVariable(name);
        }
    }

    class MultiplyExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public int Interpret(Context context)
        {
            return leftExpression.Interpret(context) * rightExpression.Interpret(context);
        }
    }

    class DivideExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public DivideExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public int Interpret(Context context)
        {
            int rightVal = rightExpression.Interpret(context);

            if (rightVal == 0)
            {
                Console.WriteLine("Помилка: Спроба дiлення на нуль!");
                return 0;
            }

            return leftExpression.Interpret(context) / rightVal;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Шаблон Interpreter (Множення та Дiлення) ===\n");

            Context context = new Context();

            context.SetVariable("a", 20);
            context.SetVariable("b", 4);
            context.SetVariable("c", 2);

            Console.WriteLine("Значення змiнних: a = 20, b = 4, c = 2\n");

            IExpression multiplyExpr = new MultiplyExpression(new NumberExpression("a"), new NumberExpression("b"));
            Console.WriteLine($"Результат (a * b): {multiplyExpr.Interpret(context)}");

            IExpression divideExpr = new DivideExpression(multiplyExpr, new NumberExpression("c"));
            Console.WriteLine($"Результат (a * b) / c: {divideExpr.Interpret(context)}");

            Console.ReadKey();
        }
    }
}