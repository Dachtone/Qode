using System;
using Xunit;
using Qode.LinearAlgebra;
using System.Numerics;

namespace Qode.Tests.LinearAlgebra
{
    public class MatrixArithmeticTests
    {
        [Theory]
        [InlineData(15, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 }, new[] { 33, 30, 33 })]
        [InlineData(-7, new[] { 1, 2 }, new[] { 3, 4 })]
        public void Add_EqualDimensions_ElementsAreSummed(int sum, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> a = array;

            Matrix<int> b = new Matrix<int>(a.Rows, a.Columns);
            for (int row = 0; row < a.Rows; row++)
            {
                for (int column = 0; column < a.Columns; column++)
                {
                    b[row, column] = a[row, column] + sum;
                }
            }

            Matrix<int> matrix = a + b;

            for (int row = 0; row < a.Rows; row++)
            {
                for (int column = 0; column < a.Columns; column++)
                {
                    Assert.Equal(a[row, column] + b[row, column], matrix[row, column]);
                }
            }
        }

        [Theory]
        [InlineData(2, 3, 2, 2)]
        [InlineData(5, 5, 4, 4)]
        public void Add_DifferentDimensions_Throw(int rowsA, int columnsA, int rowsB, int columnsB)
        {
            Matrix<int> a = new Matrix<int>(rowsA, columnsA);
            Matrix<int> b = new Matrix<int>(rowsB, columnsB);
            Matrix<int> matrix;

            Assert.Throws<MatriciesAreNotOfTheSameSizeException>(() =>
            {
                matrix = a + b;
            });
        }

        [Fact] // Due to xUnit limitations on InlineData parameters, this test will have to be a Fact
        public void Multiply_MatriciesWithCorrectDimensions_MatrixProduct()
        {
            Matrix<int> expected = new int[,]
            {
                { 24, 15 },
                { 13, -2 },
                { -93, 12 },
                { 27, 24 }
            };

            Matrix<int> a = new int[,]
            {
                { 2, 3, -1 },
                { 1, 2, -4 },
                { -1, -12, 14 },
                { 2, 3, 2 }
            };

            Matrix<int> b = new int[,]
            {
                { -1, 6 },
                { 9, 2 },
                { 1, 3 }
            };

            Matrix<int> actual = a * b;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 3, 2, 2)]
        [InlineData(1, 3, 2, 2)]
        [InlineData(4, 4, 3, 4)]
        public void Multiply_MatriciesWithIncorrectDimensions_Throw(int rowsA, int columnsA, int rowsB, int columnsB)
        {
            Matrix<int> a = new Matrix<int>(rowsA, columnsA);
            Matrix<int> b = new Matrix<int>(rowsB, columnsB);
            Matrix<int> matrix;

            Assert.Throws<NumberOfColumnsDoesNotMatchNumberOfRowsException>(() =>
            {
                matrix = a * b;
            });
        }

        [Fact]
        public void Multiply_ComplexMatrix_MatrixProduct()
        {
            Matrix<Complex> expected = new Complex[,]
            {
                { new(6.75, 6.75), new(2, 2) },
                { new(1.25, -1.25), new(2, -2) },
                { new(2, 1), new(1, 0.5) },
            };

            Matrix<Complex> a = new Complex[,]
            {
                { new(1, 1), new(1, -1) },
                { new(1, -1), new(1, 1) },
                { new(0.5, 0.25), new(0, 0) },
            };

            Matrix<Complex> b = new Complex[,]
            {
                { 4, 2 },
                { new(0, 2.75), 0 },
            };

            Matrix<Complex> actual = a * b;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TensorProduct_IntegerMatrix_MatrixTensorProduct()
        {
            Matrix<int> expected = new int[,]
            {
                { 0, 12, 0, 8, 0, 3, 0, 2 },
                { 16, 8, 24, 4, 4, 2, 6, 1 },
                { 0, 30, 0, 20, 0, 9, 0, 6 },
                { 40, 20, 60, 10, 12, 6, 18, 3 },
                { 0, 9, 0, 6, 0, 6, 0, 4 },
                { 12, 6, 18, 3, 8, 4, 12, 2 },
            };

            Matrix<int> a = new int[,]
            {
                { 4, 1, },
                { 10, 3, },
                { 3, 2, },
            };

            Matrix<int> b = new int[,]
            {
                { 0, 3, 0, 2 },
                { 4, 2, 6, 1 },
            };

            Matrix<int> actual = a.TensorProduct(b);

            Assert.Equal(expected, actual);
        }
    }
}
