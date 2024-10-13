using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace graphic
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        public Complex[] rootsFiGlobal;
        public Complex[] rootsPsiGlobal;

        public AnalysisAndSynthesis analysisAndSynthesis = new AnalysisAndSynthesis();
        
        private void button1_Click(object sender, EventArgs e)
        {

            label11.Text = "";  
            this.chart.Series[0].Points.Clear();
            this.chart.Series[1].Points.Clear();
            this.chart.Series[2].Points.Clear();
            this.chart.Series[3].Points.Clear();
            this.chart.Series[4].Points.Clear();
            this.chart.Series[5].Points.Clear();
            this.chart.Series[6].Points.Clear();
            this.chart.Series[7].Points.Clear();
            this.chart.Series[8].Points.Clear();
            this.chart.Series[9].Points.Clear();
            this.chart.Series[10].Points.Clear();
            Util.consoleString = "\n";
            richTextBox1.Clear();

            Complex[] poly1;
            Complex[] poly2;
            // Для полинома Харитонова со степенью 4
            Complex[] poly3=new Complex[3];
            int selector = 1;
            int power = 0;
            int haritonovPower = 0;
            bool isHaritonov = radioHaritonov.Checked;
            if (radioButton1.Checked)
            {
                Console.WriteLine("Введите корни пси");
                //string numbers = Console.ReadLine();

                //string numbers = textBox1.Text;
                string numbers = "";
                //for (int i=0; i< dataGridView1.RowCount;i++) {
                //    numbers += Convert.ToString(dataGridView1[0, i].Value);
                //    numbers += " ";
                //    Console.WriteLine(Convert.ToString(dataGridView1[0, i].Value));

                //}
                //numbers = numbers.Substring(2, numbers.Length-2);
                //Console.WriteLine("\\\\\\\\\\\\\\\\\\\\NUMBERS"+numbers);


                //Console.WriteLine(numbers);


                Complex[] rootsPsi = new Complex[dataGridView1.RowCount - 1];
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    numbers += Convert.ToString(dataGridView1[0, i].Value) + " ";
                    Console.WriteLine(Convert.ToString(dataGridView1[0, i].Value));
                    this.chart.Series[2].Points.AddXY(Int32.Parse(Convert.ToString(dataGridView1[0, i].Value)), 0);
                    rootsPsi[i] = new Complex(Int32.Parse(Convert.ToString(dataGridView1[0, i].Value)), 0);

                }
                numbers = numbers.Trim();
                Console.WriteLine("\\\\\\\\\\\\\\\\\\\\NUMBERS" + numbers);
                var ints = numbers.Split(' ').Select(Int32.Parse).ToArray();
                Console.WriteLine("Введите корни фи");
                //string numbers1 = Console.ReadLine();
                //string numbers1 = textBox2.Text;
                string numbers1 = "";
                Console.WriteLine(numbers1);

                Complex[] rootsFi = new Complex[dataGridView2.RowCount - 1];

                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    numbers1 += Convert.ToString(dataGridView2[0, i].Value) + " ";
                    Console.WriteLine(Convert.ToString(dataGridView2[0, i].Value));
                    this.chart.Series[8].Points.AddXY(Int32.Parse(Convert.ToString(dataGridView2[0, i].Value)), 0);
                    rootsFi[i] = new Complex(Int32.Parse(Convert.ToString(dataGridView2[0, i].Value)), 0);

                }
                numbers1 = numbers1.Trim();
                var ints1 = numbers1.Split(' ').Select(Int32.Parse).ToArray();
                double asimpCentre = 0;
                double asimptCount = ints1.Length - ints.Length;
                foreach (var number in ints) {
                    asimpCentre += -number;

                }
                foreach (var number in ints1)
                {
                    asimpCentre += -number;

                }
                asimpCentre = asimpCentre / asimptCount;

                //for (int i=-10; i<10;i++) {
                for (double j = 1; j <= asimptCount; j += 1)
                {
                    double asimptAngle = 180 * j / asimptCount;
                    Console.WriteLine(asimpCentre + "   ASIMPTOTS    " + asimptAngle);
                    for (double i = -10; i < 10; i += 0.1) {
                        if (asimptAngle == 180)
                        {
                            if (i > 5) {
                                continue;
                            }
                            this.chart.Series[7].Points.AddXY(i, i * Math.Round(Math.Sin(asimptAngle * Math.PI / 180), 5));

                        }
                        else if (asimptAngle == 90) {

                            this.chart.Series[7].Points.AddXY(-asimpCentre * Math.Round(Math.Sin(asimptAngle * Math.PI / 180), 5), i);
                        }
                        else
                        {
                            if (asimptAngle > 90)
                            {
                                this.chart.Series[7].Points.AddXY(i * Math.Round(Math.Cos(asimptAngle * Math.PI / 180), 5) - asimpCentre, i);
                            }
                            else
                            {
                                this.chart.Series[7].Points.AddXY(i * Math.Round(Math.Cos(asimptAngle * Math.PI / 180), 5) - asimpCentre, i);
                            }
                        }
                        //Console.WriteLine(i+"  ANGLE  "+ Math.Round(Math.Sin(asimptAngle * Math.PI / 180), 5));
                    }

                }


                //}

                //Complex[] rootsPsi = { -3 };
                //Complex[] rootsFi = { -2,-3 };


                /////REIMK
                rootsFiGlobal = rootsFi;
                rootsPsiGlobal = rootsPsi;

                power = rootsFi.Length;
                //labelPower.Text = power.ToString();
                poly1 = FromRoots(rootsPsi);
                poly2 = FromRoots(rootsFi);
                labelPower.Text = (poly2.Length-1).ToString()+"\n";

            }
            else {
                if (isHaritonov)
                {
                    string numbers = "";

                    Complex[] rootsPsi = new Complex[dataGridViewUpChisl.RowCount - 1];
                    

                    for (int i = 0; i < dataGridViewUpChisl.RowCount - 1; i++)
                    {
                        numbers += Convert.ToString(dataGridViewUpChisl[0, i].Value) + " ";
                        Console.WriteLine(Convert.ToString(dataGridViewUpChisl[0, i].Value));
                       
                        rootsPsi[i] = new Complex(Int32.Parse(Convert.ToString(dataGridViewUpChisl[0, i].Value)), 0);

                    }
                    numbers = numbers.Trim();






                    //////////// Вычисление корней нижней границы знаменателя
                    string numbers1 = "";
                    Complex[] rootsFi = new Complex[dataGridViewDownZnam.RowCount - 1];

                    for (int i = 0; i < dataGridViewDownZnam.RowCount - 1; i++)
                    {
                        numbers1 += Convert.ToString(dataGridViewDownZnam[0, i].Value) + " ";
                        Console.WriteLine(Convert.ToString(dataGridViewDownZnam[0, i].Value));
                       
                        rootsFi[i] = new Complex(Int32.Parse(Convert.ToString(dataGridViewDownZnam[0, i].Value)), 0);

                    }
                    numbers1 = numbers1.Trim();
                    int[] fiNiz = numbers1.Split(' ').Select(Int32.Parse).ToArray();
                    /////////////////

                    //////////// Вычисление корней верхней границы знаменателя
                    string numbers2 = "";
                    Complex[] rootsFiUp = new Complex[dataGridViewUpZnam.RowCount - 1];

                    for (int i = 0; i < dataGridViewUpZnam.RowCount - 1; i++)
                    {
                        numbers2 += Convert.ToString(dataGridViewUpZnam[0, i].Value) + " ";
                        Console.WriteLine(Convert.ToString(dataGridViewUpZnam[0, i].Value));    
                        rootsFiUp[i] = new Complex(Int32.Parse(Convert.ToString(dataGridViewUpZnam[0, i].Value)), 0);

                    }
                    numbers2 = numbers2.Trim();
                    int[] fiVerh = numbers2.Split(' ').Select(Int32.Parse).ToArray();
                    ////////////////







                    int[] psi = numbers.Split(' ').Select(Int32.Parse).ToArray();
                    // poly1 - числитель(нет верха и низа, только одно значение)


                    Array.Reverse(fiNiz);
                    Array.Reverse(fiVerh);
                    
                    Array.Reverse(psi);


                    poly1 = new Complex[psi.Length];

                    for (int i = 0; i < psi.Length; i++)
                    {
                        poly1[i] = psi[i];
                    }


                    labelPower.Text = (fiVerh.Length-1).ToString()+" ."+"\n";
                    haritonovPower = fiVerh.Length - 1;

                    if (haritonovPower == 3)
                    {
                        // poly2 - должен состоять из верхней и нижней
                        poly2 = new Complex[fiNiz.Length];
                        
                        for (int i = 0; i < fiNiz.Length; i++)
                        {
                            if (i == 3)
                            {
                                poly2[i] = fiVerh[i];


                            }
                            else {
                                poly2[i] = fiNiz[i];

                            }
                            
                            richTextBox1.Text+= poly2[i].Real.ToString()+" ";
                            
                        }

                        double a3 = poly2[1].Real * poly2[2].Real;
                        analysisAndSynthesis.AnFor3= a3;
                        analysisAndSynthesis.AnMin = a3;
                        richTextBox1.Text += "  Параметр КГ в т. пересечения с w: " + a3;
                        if (a3 > Int32.Parse(textBoxParameter2.Text) && a3 > Int32.Parse(textBoxParameter1.Text)) {
                            analysisAndSynthesis.isRobust = "Да";


                        }
                        else {
                            analysisAndSynthesis.isRobust = "Нет";
                            


                        }

                    }
                    else if (haritonovPower == 4)
                    {
                        // степень уравнения =4, поэтому должно быть 2 полинома 1- низ низ низ верх верх
                        // 2- верх верх низ низ верх
                        // poly2 - должен состоять из верхней и нижней
                        poly2 = new Complex[fiNiz.Length];
                        poly3 = new Complex[fiNiz.Length];
                        
                        for (int i = 0; i < fiNiz.Length; i++)
                        {
                            if (i == 3 || i==4)
                            {
                                poly2[i] = fiVerh[i];


                            }
                            else
                            {
                                poly2[i] = fiNiz[i];

                            }

                            richTextBox1.Text += poly2[i].Real.ToString() + " ";
                        }

                        richTextBox1.Text += "\n";
                        for (int i = 0; i < fiNiz.Length; i++)
                        {
                            if (i == 0 || i == 1 || i==4)
                            {
                                poly3[i] = fiVerh[i];


                            }
                            else
                            {
                                poly3[i] = fiNiz[i];

                            }

                            richTextBox1.Text += poly3[i].Real.ToString() + " ";
                        }

                        double a4_1 = -(Math.Pow(poly2[3].Real, 2) / Math.Pow(poly2[1].Real, 2)) + poly2[2].Real * poly2[3].Real / poly2[1].Real;
                        double a4_2 = -(Math.Pow(poly3[3].Real, 2) / Math.Pow(poly3[1].Real, 2)) + poly3[2].Real * poly3[3].Real / poly3[1].Real;
                        double a4 = Math.Min(a4_1,a4_2);

                        analysisAndSynthesis.AnFor3 = a4_1;
                        analysisAndSynthesis.AnFor4 = a4_2;
                        analysisAndSynthesis.AnMin = a4;
                        
                        if (a4 > Int32.Parse(textBoxParameter2.Text) && a4 > Int32.Parse(textBoxParameter1.Text))
                        {
                            analysisAndSynthesis.isRobust = "Да";
                           


                        }
                        else
                        {
                            analysisAndSynthesis.isRobust = "Нет";


                        }

                    }
                    else if (haritonovPower > 4)
                    {
                        // poly2 - должен состоять из верхней и нижней
                        poly2 = new Complex[fiNiz.Length];

                        for (int i = 0; i < fiNiz.Length; i++)
                        {
                            if (i == 3)
                            {
                                poly2[i] = fiVerh[i];


                            }
                            else
                            {
                                poly2[i] = fiNiz[i];

                            }

                            richTextBox1.Text += poly2[i].ToString() + " ";
                        }

                        richTextBox1.Text += "Программа не поддерживает системы выше 4 порядка";
                    }
                    else {
                        // poly2 - должен состоять из верхней и нижней
                        poly2 = new Complex[fiNiz.Length];

                        for (int i = 0; i < fiNiz.Length; i++)
                        {
                            if (i == 3)
                            {
                                poly2[i] = fiVerh[i];


                            }
                            else
                            {
                                poly2[i] = fiNiz[i];

                            }

                            richTextBox1.Text += poly2[i].ToString() + " ";
                        }
                        richTextBox1.Text += "Порядок ниже третьего. Программа поддерживает уравнения 3 и 4 порядка";

                    }

                }
                else
                {



                    string numbers = "";

                    Complex[] rootsPsi = new Complex[dataGridView1.RowCount - 1];
                    //rootsPsi = Util.Reverse(rootsPsi);
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        numbers += Convert.ToString(dataGridView1[0, i].Value) + " ";
                        Console.WriteLine(Convert.ToString(dataGridView1[0, i].Value));
                        //this.chart.Series[8].Points.AddXY(Int32.Parse(Convert.ToString(dataGridView1[0, i].Value)), 0);
                        rootsPsi[i] = new Complex(Int32.Parse(Convert.ToString(dataGridView1[0, i].Value)), 0);

                    }
                    numbers = numbers.Trim();



                    string numbers1 = "";
                    Complex[] rootsFi = new Complex[dataGridView2.RowCount - 1];
                    //rootsFi= Util.Reverse(rootsFi);
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        numbers1 += Convert.ToString(dataGridView2[0, i].Value) + " ";
                        Console.WriteLine(Convert.ToString(dataGridView2[0, i].Value));
                        //this.chart.Series[2].Points.AddXY(Int32.Parse(Convert.ToString(dataGridView2[0, i].Value)), 0);
                        rootsFi[i] = new Complex(Int32.Parse(Convert.ToString(dataGridView2[0, i].Value)), 0);

                    }
                    numbers1 = numbers1.Trim();
                    int[] fi = numbers1.Split(' ').Select(Int32.Parse).ToArray();
                    int[] psi = numbers.Split(' ').Select(Int32.Parse).ToArray();

                    Array.Reverse(fi);
                    Array.Reverse(psi);





                    poly1 = new Complex[psi.Length];
                    poly2 = new Complex[fi.Length];
                    for (int i = 0; i < psi.Length; i++)
                    {
                        poly1[i] = psi[i];
                    }
                    for (int i = 0; i < fi.Length; i++)
                    {
                        poly2[i] = fi[i];
                    }

                    richTextBox1.Text = (poly2.Length - 1).ToString();


                    Complex[,] rootsForPoly = new Complex[1,poly2.Length];
                    for (int i = 0; i < poly2.Length; i++)
                    {
                        rootsForPoly[0, i] = poly2[i];

                    }


                    //Нахождения корней для уравнения заданного явно
                    //equa2 = rootsForPoly;

                    //Console.WriteLine("///////TEST///////////////////////");
                    //HashSet<double> roots = TrueNewtonRaphson();
                    //foreach (var root in roots) { 
                    //    Console.WriteLine(root.ToString());
                    
                    
                    //}
                }

            }
            
            
            // Вывод в консоль
            //Console.WriteLine("Коэффициенты пси:");
            //foreach (Complex p in poly1)
            //{
            //    Console.Write(p + " ");
            //}
            //Console.WriteLine("\nКоэффициенты фи:");
            //foreach (Complex p in poly2)
            //{
            //    Console.Write(p + " ");
            //}
            ///////


            //Этот участок ничего не делает, но на всякий случай оставлю в в виде комментария
            //for (int i = 0; i < poly2.Length; i++)
            //{
            //    poly2[i] = Complex.Multiply(poly2[i], 1);
            //}

            int n = poly2.Length;
            int m = poly1.Length;
            
            Complex[,] temp1 = new Complex[n, n];
            Complex[,] temp2 = new Complex[m, m];
            List<List<List<Complex>>> EF;
            List<List<List<Complex>>> PR;
            EF = CalculateProm(poly2, temp1, false, "EF");
            PR = CalculateProm(poly1, temp2, false, "PR");
            Util.consoleString += "RESULT E*R\n";
            Console.WriteLine("//////RESULT E*R///////");
            Complex[,] ERMul = Util.TrueMatrixMultiplication(EF[0], PR[1],false);
            Util.consoleString += "RESULT F*P\n";
            Console.WriteLine("//////RESULT F*P///////");
            Complex[,] FPMul = Util.TrueMatrixMultiplication(EF[1], PR[0],false);


            //Блок нахождения U   -(EP+FR/P^2+R^2) для обозначения цвета 
            //Complex[,] EPMul = Util.TrueMatrixMultiplication(EF[0], PR[0],false);
            //Complex[,] FRMul = Util.TrueMatrixMultiplication(EF[1], PR[1],false);

            //Complex[,] PPMul = Util.TrueMatrixMultiplication(PR[0], PR[0],false);
            //Complex[,] RRMul = Util.TrueMatrixMultiplication(PR[1], PR[1],false);

            //List<List<Complex>> EP2 = FromArrayToList(EPMul);
            //List<List<Complex>> FR2 = FromArrayToList(FRMul);
            //List<List<Complex>> PP2 = FromArrayToList(PPMul);
            //List<List<Complex>> RR2 = FromArrayToList(RRMul);

            
            //Complex[,] EPPlusFR = Util.MatrixSum(EP2, FR2,true);
            //Complex[,] P2PlusR2 = Util.MatrixSum(PP2, RR2, true);


            //List<List<Complex>> EPPlusFRList = FromArrayToList(EPPlusFR);
            //List<List<Complex>> P2PlusR2List = FromArrayToList(P2PlusR2);

            ////Complex[,] EPFRDivP2R2 = Util.TrueMatrixMultiplication(EPPlusFRList, P2PlusR2List,true);
            ////List<List<Complex>> EPFRDivP2RList = FromArrayToList(EPFRDivP2R2);
            //Complex[,] TopLocal = Util.NegateMatrix(EPPlusFRList);
            //Complex[,] BottomLocal = Util.NegateMatrix(P2PlusR2List);
            ////Bottom= Util.NegateMatrix(P2PlusR2List);
            ////Console.WriteLine("///////EPFRDivP2R2/////////");
            ////Util.Print(U);
            //////////////////////////////////////

            List<List<Complex>> ER2 = FromArrayToList(ERMul);
            List<List<Complex>> FP2 = FromArrayToList(FPMul);

            

            Console.WriteLine("///////TEST/////////");
            foreach (var elem in FP2) {
                foreach (var el in elem) {
                    Console.Write(el);

                }
                Console.WriteLine();

            }

            Console.WriteLine("/////////////////ER-FP");

            Complex[,] erminfp = Util.MatrixSum(ER2, FP2,false);

            Console.WriteLine("NEWTON RAPHSON");
            equa = Util.DeleteOmega(erminfp);
            // U - для определения цвета
            //Top = TopLocal;
            //Bottom = BottomLocal;
            //U= Util.DeleteOmega(U);
            NewtonRaphson();


            //Для полинома Харитонова 4 порядка
            if (haritonovPower==4) {
                EF = CalculateProm(poly3, temp1, false, "EF");
                
                
                ERMul = Util.TrueMatrixMultiplication(EF[0], PR[1], false);
                
                FPMul = Util.TrueMatrixMultiplication(EF[1], PR[0], false);

                ER2 = FromArrayToList(ERMul);
                FP2 = FromArrayToList(FPMul);
                erminfp = Util.MatrixSum(ER2, FP2, false);
                equa = Util.DeleteOmega(erminfp);
                color = 1;
                NewtonRaphson();
                color = 0;
            }
















            string numbers4 = Console.ReadLine();
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            Axis ax = new Axis();
            ax.Title = "Ось σ";
            chart.ChartAreas[0].AxisX = ax;

            Axis ay = new Axis();
            ay.Title = "Ось iω";
            chart.ChartAreas[0].AxisY = ay;


            //NewtonRaphson();
            //NewtonRaphson1();
            //this.chart.Series[2].Points.AddXY(-6,-6);
            //this.chart.Series[2].Points.AddXY(6, 6);
            if (radioButton1.Checked)
            {
                List<Complex> one = rootsFiGlobal.ToList();
                List<Complex> two = rootsPsiGlobal.ToList();

                one.AddRange(two);
                List<double> roots = new List<double>();
                for (int j = 0; j < one.Count; j++)
                {
                    roots.Add(one[j].Real);

                }
                roots.Sort();
                //roots.Reverse();
                int startX = -10;
                int finishX = 6;

                int click = -1;

                if (roots.Count % 2 == 0)
                {

                    click = 1;
                }

                roots.Add(finishX);
                double currentStart = startX;
                double currentEnd = roots[0];
                for (int j = 0; j < roots.Count; j++)
                {
                    currentEnd = roots[j];
                    click *= -1;
                    for (double i = currentStart; i < currentEnd; i += 0.01)
                    {
                        if (click == 1)
                        {
                            this.chart.Series[9].Points.AddXY(i, 0.5);
                            //Console.WriteLine(click + "   point " + i);


                        }
                        if (click == -1)
                        {
                            this.chart.Series[10].Points.AddXY(i, 0.5);

                            //Console.WriteLine(click + "   point " + i);

                        }


                    }

                    currentStart = currentEnd;


                }
            }
            int index = 0;
            
            for (int i = -10; i < 7; i++)
            {
                //for (int j=0;j<one.Count;j++) {

                //    if (i==one[j].Real)
                //    {
                //        if (click == 0)
                //        {
                //            this.chart.Series[3].Color = Color.Red;
                //            click = 1;
                //            break;

                //        }
                //        else
                //        {
                //            this.chart.Series[3].Color = Color.Blue;
                //            click = 0;
                //            break;

                //        }

                //    }

                //}


                
                this.chart.Series[3].Points.AddXY(i, 0);
                //this.chart.Series[4].Points.AddXY(0, i);
            }
            for (int i = -10; i < 10; i++)
            {
                //this.chart.Series[3].Points.AddXY(i, 0);
                this.chart.Series[4].Points.AddXY(0, i);
            }

            this.chart.ChartAreas[0].AxisX.Interval = 0.5;
            this.chart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            this.chart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            this.chart.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;
            this.chart.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;
            this.chart.ChartAreas[0].AxisY.Interval = 0.5;
            //richTextBox1.Text += Util.consoleString;

            
       

        }

        public List<List<Complex>> FromArrayToList(Complex[,] arr) {
            

            List<List<Complex>> list = new List<List<Complex>>();
            

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                List<Complex> temp = new List<Complex>();
                for (int j = 0; j < arr.GetLength(1); j++)
                {


                    temp.Add(arr[i, j]);



                }
                list.Add(temp);
            }
            return list;

        }
        public static Complex[] FromRoots(Complex[] roots)
        {
            int n = roots.Length;
            Complex[] coeffs = new Complex[n + 1];
            coeffs[n] = 1.0;

            for (int i = 0; i < n; i++)
            {
                for (int j = n - i - 1; j < n; j++)
                {
                    coeffs[j] = coeffs[j] - roots[i] * coeffs[j + 1];
                }
            }

            Array.Reverse(coeffs);
            return coeffs;
        }

        public static List<List<List<Complex>>> CalculateProm(Complex[] arr, Complex[,] matrix, Boolean isPsi, String efpr)
        {
            int n = arr.Length;
            Complex[,] real = new Complex[n, n];
            Complex[,] imaginary = new Complex[n, n];

            int power = arr.Length - 1;
            int powerBin = arr.Length - 1;
            //Console.WriteLine(Convert.ToInt32(isPsi));

            ///// s= psi+iw
            for (int i = 0; i < arr.Length; i++)
            {
                for (int a = 0; a <= powerBin; a++)
                {

                    if (a >1 && a%4!=0)
                    {
                        matrix[a, power] = Complex.Multiply(arr[i], Util.binom(powerBin, a) * (isPsi == false ? -1 : 1));
                    }
                    
                    else
                    {
                        matrix[a, power] = Complex.Multiply(arr[i], Util.binom(powerBin, a));
                    }
                    power--;
                }
                powerBin--;
                power = powerBin;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    //if (i==0) {
                    //    imaginary[i, j] = 0;
                    //}
                    if (i % 2 != 0)
                    {
                        imaginary[i, j] = matrix[i, j];
                    }
                    else
                    {
                        real[i, j] = matrix[i, j];
                    }
                }
            }
            String letters;
            if (isPsi)
            {
                letters = "Psi P R";
            }
            else
            {
                letters = "Fi E F";
            }
            String[] EFPR = letters.Split();
            //Console.WriteLine("     "+ EFPR[0]);
            //print(arr, matrix);
            List<List<Complex>> E;
            List<List<Complex>> F;
            List<List<Complex>> P;
            List<List<Complex>> R;
            List<List<List<Complex>>> result = new List<List<List<Complex>>>();
            if (n % 2 == 0)
            {
                if (efpr == "PR")
                {
                    Util.consoleString += "\n P\n";
                    Console.WriteLine("\n P\n");
                }
                else {
                    Util.consoleString += "\n E\n";
                    Console.WriteLine("\n E\n");

                }
                
                P = Util.print(arr, real, false, efpr[0]+"");
                if (efpr == "PR")
                {
                    Util.consoleString += "\n R\n";
                    Console.WriteLine("\n R\n");
                }
                else {

                    Util.consoleString += "\n F\n";
                    Console.WriteLine("\n F\n");

                }



                R = Util.print(arr, imaginary, true, efpr[1] + "");
                result.Add(P);
                result.Add(R);
                return result;
            }
            else
            {
                Util.consoleString += "\n E\n";
                Console.WriteLine("\n " + EFPR[1]);
                E = Util.print(arr, real, true, "E");
                Util.consoleString += "\n F\n";
                Console.WriteLine("\n " + EFPR[2]);
                F = Util.print(arr, imaginary, false, "F");
                
                result.Add(E);
                result.Add(F);
                return result;
            }

        }

        

        static double EPSILON = 0.0001;
        public static Complex[,] equa;
        public static Complex[,] equa2;
        public static Complex[,] U;
        public static Complex[,] Top;
        public static Complex[,] Bottom;
        static double func(double y, double x)
        {
            //-Math.Pow(x, 2) - Math.Pow(y, 2) - 6 * x - 3
            //return -Math.Pow(x, 2)-6 * x - 3-Math.Pow(y,2);
            double result = 0;
            for (int i = 0; i < equa.GetLength(0); i++)
            {
                for (int j = 0; j < equa.GetLength(1); j++)
                {
                    result += equa[i, j].Real * Math.Pow(y, j) * Math.Pow(x, i);
                }

            }
       
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
            return result;
        }
        static double func2(double y, double x)
        {
            //-Math.Pow(x, 2) - Math.Pow(y, 2) - 6 * x - 3
            //return -Math.Pow(x, 2)-6 * x - 3-Math.Pow(y,2);
            double result = 0;
            for (int i = 0; i < equa2.GetLength(0); i++)
            {
                for (int j = 0; j < equa2.GetLength(1); j++)
                {
                    result += equa2[i, j].Real * Math.Pow(y, j) * Math.Pow(x, i);
                }

            }

            return result;
        }


        static double derivFunc2(double x, double y)
        {
            double result = 0;
            for (int i = 0; i < equa2.GetLength(0); i++)
            {
                for (int j = 0; j < equa2.GetLength(1); j++)
                {
                    result += equa2[i, j].Real * j * Math.Pow(x, j - 1) * Math.Pow(y, i);
                }

            }
            return result;
        }
        public HashSet<double> TrueNewtonRaphson()
        {
            HashSet<double> roots = new HashSet<double>(); 
            
            int i = 0;

            for (double x = -10.1; x < 10; x+=0.1)
            {
                double y = 0;
                double xn = x;
                double h = func2(x, y) / derivFunc2(x, y);
                int iteration = 0;

                while (Math.Abs(h) >= EPSILON)
                {
                    h = func2(xn, y) / derivFunc2(xn, y);

                    xn = xn - h;

                    iteration++;

                    if (iteration > 10)
                    {
                        xn = -999;
                        break;
                    }

                }
                if (xn != -999)
                {
                    roots.Add( Math.Round(xn * 100.0) / 100.0);
                    i++;
                }
            }
            return roots;
        }
        static int color = 0;
        public void NewtonRaphson()
        {

            
            double x1 = Double.Parse(sigmaFrom.Text);
 
            HashSet<Series> list =new HashSet<Series>();
            
            for (double x = Int32.Parse(sigmaFrom.Text)+0.0001; x < Int32.Parse(sigmaTo.Text); x+= 1)
            {
                
                //Series series = new Series();
                //series.Color = Color.Red;
                //series.ChartType = SeriesChartType.Spline;
                //series.BorderWidth = 3;
                //this.chart.Series[0].Color = Color.Crimson;
                for (double y = Int32.Parse(omegaFrom.Text); y < Int32.Parse(omegaTo.Text); y += Double.Parse(sigmaStep.Text))
                {
                    double xn = x;
                    double h = func(x, y) / derivFunc(x, y);
                    int iteration = 0;
                    while (Math.Abs(h) >= EPSILON)
                    {
                        h = func(xn, y) / derivFunc(xn, y);

                        xn = xn - h;
  
                        iteration++;

                        if (iteration > 10)
                        {
                            xn = -999;
                            break;
                        }
                    }
                    if (xn != -999)
                    {
                        
                        if (y == 0)
                        {
                            continue;
                        }
                        else {
                            // Смена цвета 
                            //double color = -funcTop(y, (Math.Round(xn * 100.0) / 100.0)) /funcBottom(y, (Math.Round(xn * 100.0) / 100.0));
                            //if (color<0)
                            //{
                            //    this.chart.Series[10].Points.AddXY((Math.Round(xn * 100.0) / 100.0), y);

                            //}
                            //if (color > 0)
                            //{
                            //    this.chart.Series[0].Points.AddXY((Math.Round(xn * 100.0) / 100.0), y);
                            //}
                            if ((Math.Round(xn * 100.0) / 100.0)==0) {

                                continue;
                            }
                            if (color == 0)
                            {
                                this.chart.Series[0].Points.AddXY((Math.Round(xn * 100.0) / 100.0), y);


                            }
                            else {
                                this.chart.Series[10].Points.AddXY((Math.Round(xn * 100.0) / 100.0), y);

                            }
                            


                        }
                        
                        //this.chart.Series[0].Color = Color.Crimson;
                        //this.chart.Series[0].MarkerSize = 3;
                        //Console.WriteLine((Math.Round(xn * 100.0) / 100.0)+"       "+y);
                        //this.chart.Series[0] = series;

                    }
                }
            }
            
            

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            
            Util.consoleString = "\n";
            richTextBox1.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

            dataGridViewUpChisl.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridViewUpZnam.Rows.Clear();
            dataGridViewDownZnam.Rows.Clear();

            dataGridViewUpChisl.Refresh();
            dataGridView3.Refresh();
            dataGridViewUpZnam.Refresh();
            dataGridViewDownZnam.Refresh();




            textBoxAnalys1.Text = "";
            textBoxAnalys2.Text = "";
            textBoxAnalysis3.Text = "";

            label11.Text = "";


            textBoxSynth1.Text = "";
            textBoxSynth2.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.chart.Series[0].Points.Clear();
            this.chart.Series[1].Points.Clear();
            this.chart.Series[2].Points.Clear();
            this.chart.Series[3].Points.Clear();
            this.chart.Series[4].Points.Clear();
            this.chart.Series[5].Points.Clear();
            this.chart.Series[6].Points.Clear();
            this.chart.Series[7].Points.Clear();
            this.chart.Series[8].Points.Clear();
            this.chart.Series[9].Points.Clear();
            this.chart.Series[10].Points.Clear();
        }

        private void textBoxFields_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = Int32.Parse(textBoxFields.Text);

                dataGridView1.RowCount = n+1;
                dataGridView3.RowCount =n+1;
                dataGridViewUpChisl.RowCount = n+1;

            }
            catch (Exception) {

                dataGridView1.RowCount = 1;
                dataGridView3.RowCount = 1;
                dataGridViewUpChisl.RowCount = 1;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = Int32.Parse(textBox1.Text);

                dataGridView2.RowCount = n+1;
                dataGridViewDownZnam.RowCount = n+1;
                dataGridViewUpZnam.RowCount = n+1;

            }
            catch (Exception)
            {
                dataGridView2.RowCount = 1;
                dataGridViewDownZnam.RowCount = 1;
                dataGridViewUpZnam.RowCount = 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxSynth1.Text = textBoxParameter1.Text;
            if (analysisAndSynthesis.isRobust == "Нет")
            {

                textBoxSynth2.Text = Math.Round(analysisAndSynthesis.AnMin) + "";
            }
            else {
                textBoxSynth2.Text = textBoxParameter2.Text;
            
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxAnalys1.Text = Math.Round(analysisAndSynthesis.AnFor3) + "";
            textBoxAnalys2.Text = Math.Round(analysisAndSynthesis.AnFor4) + "";
            textBoxAnalysis3.Text = Math.Round(analysisAndSynthesis.AnMin) + "";

            if (analysisAndSynthesis.AnMin < Int32.Parse(textBoxParameter2.Text)
                || analysisAndSynthesis.AnMin < Int32.Parse(textBoxParameter1.Text))
            {

                analysisAndSynthesis.isRobust = "Нет";
            }
            else {
                analysisAndSynthesis.isRobust = "Да";
            
            }


            label11.Text = analysisAndSynthesis.isRobust;
        }
    }
}
