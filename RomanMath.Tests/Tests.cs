using NUnit.Framework;
using System;
using RomanMath.Impl;

namespace RomanMath.Tests
{
	[TestFixture]
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
			Service.RomanNumbers();
		}

		[TestCase("XV+V-IV*II-X+XXII", 24)]
		[TestCase("L*XXX+VII*IV-M", 528)]
		[TestCase("D*II-XC+XL+LV-LX", 945)]
		[TestCase("V-II*XVIII*VI-XIV-XII", -237)]
		[TestCase("X*IX*XX-L+LXX", 1820)]
		[TestCase("C*II+I-XC", 111)]
		[TestCase("xxx*xxx-Dc+iI", 302)]
		public void Evaluate_EvaluateExpression_ResultInDecimalFormat(string expression, int expectedResult)
		{
			//Act
			int actual = Service.Evaluate(expression);
			//Assert
			Assert.AreEqual(expectedResult, actual, message: "Evaluate method works incorrectly.");
		}

		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		public void Evaluate_WhitespaceNullEmptyParameter_ThrowArgumentNullException(string expression)
		{
			// Arrange
			var expectedTypeError = typeof(ArgumentNullException);
			// Act
			Exception ex = Assert.Catch(() => Service.Evaluate(expression));
			// Assert
			Assert.AreEqual(expectedTypeError, ex.GetType(),
				message: "Evaluate method throw ArgumentNullException if parameter is empty, null or only whitespace.");
		}

		[TestCase("XV+V-I/V")]
		[TestCase("L+(M-IV)*V")]
		[TestCase("D* II - XC**")]
		[TestCase("V-22+18*L")]
		[TestCase("X*?+III-L+!")]
		[TestCase("C*+II*XC")]
		public void Evaluate_InvalidMathExpression_ThrowArgumentException(string expression)
		{
			// Arrange
			var expectedTypeError = typeof(ArgumentException);
			// Act
			Exception ex = Assert.Catch(() => Service.Evaluate(expression));
			// Assert
			Assert.AreEqual(expectedTypeError, ex.GetType(),
				message: "Evaluate method throw ArgumentException if parameter is invalid.");
		}


	}
}