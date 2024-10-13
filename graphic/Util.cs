using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace graphic
{
    internal class Util
    {
        public static string consoleString = "\n";
        static double fact(int nz)
        {
            
            int mz = nz - 1;
            if (mz > 0)
            {
                double aifct = nz;
                do
                {
                    aifct *= mz;
                    mz--;
                }
                while (mz > 0);
                return aifct;
            }
            return 1;
        }

        public static double binom(int mz, int nz)
        {
            if (mz - nz < 0)
            {
                return 0;
            }
            if (mz - nz == 0)
            {
                return 1;
            }

            double aifct1 = fact(mz);
            double aifct2 = fact(nz);
            int k = mz - nz;
            double aifct3 = fact(k);
            return aifct1 / (aifct2 * aifct3);
        }
        public static Complex[] Reverse(Complex[] array)
        {
            int j = 0;
            Complex[] result = new Complex[array.Length];
            for (int i = array.Length - 1; i >= 0; i--) { 
                result[j]=array[i];
                j++;
            }
            return result;
        }
        public static List<List<Complex>> print(Complex[] arr, Complex[,] matrix, bool isReal, string efpr)
        {
            
            Complex[,] one = new Complex[(matrix.GetUpperBound(0) + 1) / 2, matrix.GetUpperBound(1) + 1];
            List<List<Complex>> list = new List<List<Complex>>();
            for (int i1 = arr.Length - 1; i1 >= 0; i1--)
            {
                List<Complex> temp = new List<Complex>();
                //Console.Write("W^" + i1 + "  ");
                for (int j = arr.Length - 1; j >= 0; j--)
                {

                    try
                    {
                        if (efpr == "R" && j == arr.Length - 1)
                        {
                            continue;
                        }
                        temp.Add(matrix[i1, j]);
                        //Console.Write(matrix[i1, j].Real + "     ");
                    }
                    catch (Exception e)
                    {

                        temp.Add(0);
                        //Console.Write(0);
                    }
                }
                //Console.WriteLine();
                list.Add(temp);


            }

            /*
            Console.WriteLine("List/////////////");
            foreach (var element in list)
            {
                foreach (var el in element)
                {
                    Console.Write(el + "     ");
                }
            }
            ///////////
            Console.WriteLine("EndofList/////////////");
            */

            list = Util.CutZerosOmega(list);
            list = Util.CutZerosSigma(list);

            int n;
            try
            {
                n = list[0].Count();
            }
            catch (Exception e) {
                n = 0;
            
            }
            
            
            int m = list.Count();

            int counter1=0;
            int counter2 = 0;
            consoleString += "          ";
            try
            {
                foreach (var element in list[0])
                {

                    consoleString += "sig^" + (n - counter1 - 1) + "     ";
                    counter1++;


                }
                consoleString += "\n";
                foreach (var element in list)
                {
                    if (efpr == "E" || efpr == "P")
                    {

                        if ((counter2 + 1) % 2 != 0)
                        {
                            consoleString += "w^" + (m - counter2 - 1) + "   ";
                            foreach (var el in element)
                            {
                                consoleString += el + "     ";
                                Console.Write(el + "     ");
                            }
                            consoleString += "     \n";
                            Console.WriteLine("     ");

                        }
                        counter2++;

                    }
                    else
                    {
                        //consoleString += "|" + (counter2) + " , " + m + "|";
                        if ((counter2) % 2 == 0)
                        {
                            consoleString += "w^" + (m - counter2 - 1) + "   ";
                            foreach (var el in element)
                            {
                                consoleString += el + "     ";
                                Console.Write(el + "     ");
                            }
                            consoleString += "     \n";
                            Console.WriteLine("     ");
                        }
                        counter2++;


                    }

                }
            }
            catch (Exception ex) { 
            
            
            
            }
            


            return list;
        }

        public static Complex[,] MatrixSum(List<List<Complex>> matrixA, List<List<Complex>> matrixB, bool isSum)
        {
            matrixA = equalizematricesForSum(matrixA, matrixB);
            matrixB = equalizematricesForSum(matrixB, matrixA);
            //Console.WriteLine("MATRIX SUM matrixA");
            //foreach (var element in matrixA)
            //{
            //    foreach (var el in element)
            //    {
            //        Console.Write(el + "     ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("MATRIX SUM matrixB");
            //foreach (var element in matrixB)
            //{
            //    foreach (var el in element)
            //    {
            //        Console.Write(el + "     ");
            //    }
            //    Console.WriteLine();
            //}
            int aColumns = matrixA[0].Count();
            int bColumns = matrixB[0].Count();
            int aRows = matrixA.Count();
            int bRows = matrixB.Count();

            Complex[,] matrixC = new Complex[aRows, bColumns];

            for (var i = 0; i < aRows; i++)
            {
                for (var j = 0; j < bColumns; j++)
                {
                    if (isSum == false)
                    {
                        matrixC[i, j] = matrixA[i][j] - matrixB[i][j];
                    }
                    else {
                        matrixC[i, j] = matrixA[i][j] + matrixB[i][j];
                    }
                    
                }
            }
            //consoleString += "ER-FP+\n";
            //Print(matrixC);
            return matrixC;
        }

        public static void Print(Complex[,] arr)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                consoleString += "        sig^" + j;
                Console.Write("        sig^" + j);
            }
            consoleString += "\n";
            Console.WriteLine();
            for (int i = 0; i < arr.GetLength(0); i++, Console.WriteLine())
            {
                consoleString += " W^" + (i+1);
                Console.Write(" W^" + (i+1));
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    consoleString += "   " + arr[i, j].Real + "         ";
                    Console.Write("   " + arr[i, j].Real + "         ");
                }
                consoleString+="\n";
            }
        }

        public static List<List<Complex>> equalizematricesForSum(List<List<Complex>> matrixA, List<List<Complex>> matrixB)
        {
            List<List<Complex>> matrixC, matrixD;
            int aRows = matrixA.Count();
            int bRows = matrixB.Count();
            int aColumns = matrixA[0].Count();
            int bColumns = matrixB[0].Count();
            if (aColumns >= bColumns && aRows >= bRows)
            {
                return matrixA;
            }
            if (aRows < bRows)
            {
                int difference = bRows - aRows;
                List<Complex> temp = new List<Complex>();
                for (int j = 0; j < aColumns; j++)
                {
                    temp.Add(new Complex(0, 0));
                }
                for (int i = 0; i < difference; i++)
                {
                    matrixA.Insert(0, temp);
                }
            }
            if (aColumns < bColumns)
            {
                int difference = bColumns - aColumns;
                for (int i = 0; i < aRows; i++)
                {
                    for (int j = 0; j < difference; j++)
                    {
                        matrixA[i].Add(new Complex(0, 0));
                    }

                }

            }

            return matrixA;
        }

        public static Complex[,] TrueMatrixMultiplication(List<List<Complex>> matrixA, List<List<Complex>> matrixB, bool isDiv)
        {
            int aColumns = matrixA[0].Count();
            int bColumns;
            try
            {
                bColumns = matrixB[0].Count();
            }
            catch (Exception) {

                bColumns = 1;
            }
            


            int aRows = matrixA.Count();
            int bRows = matrixB.Count();
            int n = aRows - 1 + bRows - 1 + 1;
            int m = bColumns - 1 + aColumns - 1 + 1;
            Complex[,] result = new Complex[n, m];
            //Console.WriteLine("TRUE MULTIPLICATION");
            //Console.WriteLine("aColumns: "+ aColumns+" , bColumns: "+bColumns);
            //Console.WriteLine("aRows: " + aRows + " , bRows: " + bRows);

            //Console.WriteLine("//////MATRIX A///////");
            //foreach (var element in matrixA)
            //{
            //    foreach (var el in element)
            //    {
            //        Console.Write(el + "     ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("//////MATRIX B///////");

            //foreach (var element in matrixB)
            //{
            //    foreach (var el in element)
            //    {
            //        Console.Write(el + "     ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("/////////////");
            //Console.WriteLine("Количество рядов в ответе:{0}, Количество колонок:{1}", n, m);

            for (int ia = 0; ia < aRows; ia++)
            {
                for (int ja = 0; ja < aColumns; ja++)
                {



                    for (int ib = 0; ib < bRows; ib++)
                    {
                        for (int jb = 0; jb < bColumns; jb++)
                        {
                            Complex temp;
                            if (isDiv==false)
                            {
                                 temp= matrixA[ia][ja] * matrixB[ib][jb];
                            }
                            else {
                                if(matrixB[ib][jb]!=0)
                                {
                                    temp = matrixA[ia][ja] / matrixB[ib][jb];
                                }
                                else
                                {
                                    temp = 0;

                                }
                                
                            }
                            

                            result[ia + ib, ja + jb] += temp;
                        }
                    }
                }
            }
            for (int j = 0; j < n; j++)
            {
                consoleString += "      sig^" + (n-j-1);
                Console.Write("      sig^" + (n - j - 1));
            }
            consoleString += "\n";
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                if ((i+1)%2!=0 ) {
                    consoleString += " W^" + (n - i - 1) + "  ";
                    Console.Write(" W^" + (n - i - 1) + "  ");
                    for (int j = 0; j < m; j++)
                    {
                        consoleString += result[i, j].Real + "          ";
                        Console.Write(result[i, j] + "          ");
                    }
                    consoleString += "\n";
                    Console.WriteLine();

                }
                
            }

            return result;
        }

        public static Complex[,] NegateMatrix(List<List<Complex>> matrixA)
        {
            int aColumns = matrixA[0].Count();
            int aRows = matrixA.Count();
           
            Complex[,] result = new Complex[aRows, aColumns];
           
            for (int ia = 0; ia < aRows; ia++)
            {
                for (int ja = 0; ja < aColumns; ja++)
                {   
                            var temp = matrixA[ia][ja];
                            result[ia, ja] += temp;
                }
            }
            return result;
        }


        static double EPSILON = 0.0001;
        public static Complex[,] equa;

        static double func(double y, double x)
        {
            //-Math.Pow(x, 2) - Math.Pow(y, 2) - 6 * x - 3
            //return -Math.Pow(x, 2)-6 * x - 3-Math.Pow(y,2);
            double result = 0;
            for (int i = 0; i < equa.GetLength(0); i++)
            {
                for (int j = 0; j < equa.GetLength(1); j++)
                {
                    result += equa[i, j].Real * Math.Pow(x, j) * Math.Pow(y, i);
                }

            }
            //Console.WriteLine(-Math.Pow(x, 2) - 6 * x - 3 - Math.Pow(y, 2));
            //Print(equa);
            //Console.WriteLine(equa[0,2]);
            return result;
        }


        static double derivFunc(double x, double y)
        {
            double result = 0;
            for (int i = 0; i < equa.GetLength(0); i++)
            {
                for (int j = 0; j < equa.GetLength(1); j++)
                {
                    result += equa[i, j].Real * j * Math.Pow(x, j - 1) * Math.Pow(y, i);
                }

            }
            return -2 * x;
        }


        public static void NewtonRaphson()
        {
            double x = -2;
            //double i = 2.4;
            for (double y = -6; y < 0; y += 0.01)
            {
                double h = func(x, y) / derivFunc(x, y);
                int iteration = 0;
                while (Math.Abs(h) >= EPSILON)
                {
                    h = func(x, y) / derivFunc(x, y);

                    // x(i+1) = x(i) - f(x) / f'(x)
                    x = x - h;
                    //Console.WriteLine("x = " + x);
                    iteration++;
                    if (iteration > 15)
                    {
                        x = -999;
                        break;
                    }
                }
                if (x != -999)
                {
                    Console.WriteLine("y= " + y + ", The value of the"
                                + " root is : " +
                                +Math.Round(x * 100.0) / 100.0);
                }
            }



        }


        public static Complex[,] DeleteOmega(Complex[,] matrix)
        {


            Complex[,] result = new Complex[matrix.GetLength(0) - 1, matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j];

                }
            }
            //Console.WriteLine("????????????????RESULT??????????");
            result = Rotate(result);
            result = Rotate(result);
            consoleString += "ER-FP\n";
            Print(result);
            return result;
        }
        public static Complex[,] Rotate(Complex[,] m)
        {
            var result = new Complex[m.GetLength(1), m.GetLength(0)];

            for (int i = 0; i < m.GetLength(1); i++)
                for (int j = 0; j < m.GetLength(0); j++)
                    result[i, j] = m[m.GetLength(0) - j - 1, i];

            return result;
        }
        public static List<List<Complex>> CutZerosOmega(List<List<Complex>> m)
        {
            Console.WriteLine("PROVERKA");
            foreach (var elem in m) {
                foreach (var elem2 in elem) {
                    Console.WriteLine(elem2.ToString());
                }

            }
            int Columns;
            int Rows;
            try {
                 Columns= m[0].Count();
                 Rows= m.Count();
            }
            catch {
                return m;
            }
            


            int isZero = 1;
            List<List<Complex>> result = new List<List<Complex>>();
            for (int i = 0; i < Rows; i++)
            {


                for (int j = 0; j < Columns; j++)
                {
                    if (m[i][j] != 0)
                    {
                        isZero = 0;
                        return m;
                    }

                }
                if (isZero == 1)
                {
                    break;
                }

            }
            for (int i = 1; i < Rows; i++)
            {
                List<Complex> temp = new List<Complex>();

                for (int j = 0; j < Columns; j++)
                {
                    temp.Add(m[i][j]);
                }
                result.Add(temp);
            }

            result = CutZerosOmega(result);
            return result;
        }
        public static List<List<Complex>> CutZerosSigma(List<List<Complex>> m)
        {
            //int Columns = m[0].Count();
            //int Rows = m.Count();
            int Columns;
            int Rows;
            try
            {
                Columns = m[0].Count();
                Rows = m.Count();
            }
            catch
            {
                return m;
            }

            int isZero = 1;
            List<List<Complex>> result = new List<List<Complex>>();
            for (int i = 0; i < Rows; i++)
            {
                //Console.WriteLine("   m[i][0]  " + m[i][0]);
                if (m[i][0] != 0)
                {
                    isZero = 0;
                    return m;
                }



            }
            for (int i = 0; i < Rows; i++)
            {
                List<Complex> temp = new List<Complex>();

                for (int j = 1; j < Columns; j++)
                {
                    temp.Add(m[i][j]);
                }
                result.Add(temp);
            }
            //Console.WriteLine("List2/////////////");
            //foreach (var element in result)
            //{
            //    foreach (var el in element)
            //    {
            //        Console.Write(el + "     ");
            //    }
            //    Console.WriteLine("     ");
            //}
            result = CutZerosSigma(result);
            return result;
        }
    }
}
