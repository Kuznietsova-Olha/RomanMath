using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RomanMath.Impl
{
	public static class Service
	{
		public static Dictionary<char, int> RomanNumbersDictionary;

		public static void RomanNumbers()
		{
			RomanNumbersDictionary = new Dictionary<char, int>
			{
				{ 'I', 1 },
				{ 'V', 5 },
				{ 'X', 10 },
				{ 'L', 50 },
				{ 'C', 100 },
				{ 'D', 500 },
				{ 'M', 1000 }
			};
		}

		private static int ConvertRomanNumeralsToNumbers(string numeral)
		{
			int result = 0;
			for (int index = 0; index < numeral.Length; index++)
			{
				int current = RomanNumbersDictionary[numeral[index]];
				if (index + 1 < numeral.Length)
				{
					int next = RomanNumbersDictionary[numeral[index + 1]];
					if (current >= next)
						result += current;
					else
					{ 
						result += next - current;
						index++; 
					}
				}
				else
				{
					result += current;
					index++;
				}
			}
			return result;
		}

		private static int GetPriorityOfMathOperation(string operation)
		{
			int result = 0;
			switch (operation)
			{
				case "*": result = 2; break;
				case "+": result = 1; break;
				case "-": result = 1; break;
			}
			return result;
		}

		private static string[] GetExpressionInPostfixOrder(string[] subExpressions)
        {
			List<string> postfixExpression = new List<string>();
			Stack<string> stackOperations = new Stack<string>();

			for (int i = 0; i < subExpressions.Length; i++)
			{
				if (subExpressions[i] != "+" && subExpressions[i] != "-" && subExpressions[i] != "*")
				{
					postfixExpression.Add(ConvertRomanNumeralsToNumbers(subExpressions[i]).ToString());
				}
				else
				{
					if (stackOperations.Count == 0)
					{
						stackOperations.Push(subExpressions[i]);
					}
					else
					{
						while (stackOperations.Count != 0 && GetPriorityOfMathOperation(stackOperations.Last()) >= GetPriorityOfMathOperation(subExpressions[i]))
						{
							postfixExpression.Add(stackOperations.Pop());
						}
						stackOperations.Push(subExpressions[i]);
					}
				}
			}

			while (stackOperations.Count != 0)
				postfixExpression.Add(stackOperations.Pop());

			return postfixExpression.ToArray();
		}

		/// <summary>
		/// See TODO.txt file for task details.
		/// Do not change contracts: input and output arguments, method name and access modifiers
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static int Evaluate(string expression)
		{
			if (String.IsNullOrWhiteSpace(expression))
				throw new ArgumentNullException(nameof(expression), "The expression passed is invalid. It can not be Null, Empty or Whitespace.");

			if (!IsValidMathExpression(expression))
				throw new ArgumentException("The expression passed is invalid. It should be a valid math expression.");

			string[] subExpressions = SplitExpressionIntoParts(expression.ToUpper());

			string[] postfixOrder = GetExpressionInPostfixOrder(subExpressions);

			Stack<string> temporaryStack = new Stack<string>();

			for (int i = 0; i < postfixOrder.Length; i++)
            {
				if (postfixOrder[i] != "+" && postfixOrder[i] != "-" && postfixOrder[i] != "*")
				{
					temporaryStack.Push(postfixOrder[i]);
				}
                else
                {
					int firstOperand = Convert.ToInt32(temporaryStack.Pop());
					int secondOperand = Convert.ToInt32(temporaryStack.Pop());
					temporaryStack.Push(DoMathOperation(postfixOrder[i], secondOperand, firstOperand).ToString());
				}
			}

			return Convert.ToInt32(temporaryStack.Pop());
		}

		private static bool IsValidMathExpression(string expression)
        {
			string pattern = @"^([IVXLCDM]+\s*[-+*]\s*)+[IVXLCDM]+$";
			return Regex.IsMatch(expression, pattern, RegexOptions.IgnoreCase);
		}

		private static string[] SplitExpressionIntoParts(string expression)
		{
			string pattern = @"\s*([+*-])\s*";
			string[] exprArray = Regex.Split(expression, pattern);
			return exprArray;
		}

		private static int DoMathOperation(string operation, int firstOperand, int secondOperand)
		{
			int result = 0;
			switch (operation)
			{
				case "+":
					result = AddNumbers(firstOperand, secondOperand);
					break;
				case "-":
					result = SubtractNumbers(firstOperand, secondOperand);
					break;
				case "*":
					result = MultiplyNumbers(firstOperand, secondOperand);
					break;
			}
			return result;
		}

		private static int AddNumbers(int a, int b) => a + b;
		private static int SubtractNumbers(int a, int b) => a - b;
		private static int MultiplyNumbers(int a, int b) => a * b;

	}
}
