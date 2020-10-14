using System;

namespace Determinante
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Unesite stupanj determinante: ");
            int stage = int.Parse(Console.ReadLine());
            Console.Write("\n");
            if (stage > 0) //in case stage is higher than 1
            {
                float[,] mainDeterminant = new float[stage, stage]; //create main determinant 
                for (int i = 0; i < stage; i++)
                {
                    for (int j = 0; j < stage; j++)
                    {
                        Console.Write("Enter element on [" + (i + 1) + "]" + "[" + (j + 1) + "]: ");
                        mainDeterminant[i, j] = float.Parse(Console.ReadLine()); //input to i, j determinant coordinate
                    }
                }

                //print whole determinant
                Console.WriteLine("Entered Determinant: ");
                for (int i = 0; i < stage; i++)
                {
                    for (int j = 0; j < stage; j++)
                    {
                        Console.Write(mainDeterminant[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }

                //call Determinant() method and print the resault
                Console.Write($"\nResault: {Determinant(mainDeterminant)}");
            }
            else //in case stage is lower than 1
            {
                Console.Write("Wrong input!");
            }
            Console.ReadKey();
        }

        static int isStageEven(int a, int b)
        {
            return (a + b) % 2 == 0 ? 1 : -1; //return 1 if stage is even, else -1
        }

        static float Determinant(float[,] mainDeterminant) //main calculation function that switches to three branches - "grater than 2", "equal to 2" and "lower than 2"
        {
            int stageOfDeterminant = (int)Math.Round((double)Math.Sqrt(mainDeterminant.Length)); //get the stage of mainDeterminant
            if (stageOfDeterminant > 2) //if current stage is greater than two calculate by laplace's expansion
            {
                float resault = 0; //define the resault
                for (int j = 0; j < stageOfDeterminant; j++)
                {
                    float[,] minimizedDeterminant = smallerDeterminant(mainDeterminant, 0, j); //minimizing the current determinant to the stage-1 
                    resault += mainDeterminant[0, j] * (isStageEven(0, j) * Determinant(minimizedDeterminant)); //add to resault by current by formula
                }
                return resault;
            }
            else if (stageOfDeterminant == 2) //if current stage is 2, calculate by 2nd stage formula
            {
                return secondStageDeterminant(mainDeterminant);
            }
            else //otherwise (if stage is equal to 1) return the only number that is give in determinant
            {
                return mainDeterminant[0, 0];
            }
        }

        static float[,] smallerDeterminant(float[,] currentDeterminant, int i, int j)
        {
            int stageOfDeterminant = (int)Math.Round((double)Math.Sqrt(currentDeterminant.Length)); //transform the lenght of currentDeterminant to int
            float[,] minimizedDeterminant = new float[stageOfDeterminant - 1, stageOfDeterminant - 1]; //create determinant with smaller margins

            //calculate numbers for smaller determinant
            int x = 0, y = 0;
            for (int col = 0; col < stageOfDeterminant; col++, x++)
            {
                if (col != i) //check if the current calculation is in the same column
                {
                    y = 0;
                    for (int row = 0; row < stageOfDeterminant; row++)
                    {
                        if (row != j) //check if the current calculation is in the same row
                        {
                            minimizedDeterminant[x, y] = currentDeterminant[col, row]; //if they aren't add current column and row to the minimized determninant
                            y++; //go to the next row
                        }
                    }
                }
                else
                    x--; //go to the next column
            }
            return minimizedDeterminant; //returns smaller determinant after calculations
        }

        //calculation formula for 2nd stage
        static float secondStageDeterminant(float[,] currentDeterminant)
        {
            return (currentDeterminant[0, 0] * currentDeterminant[1, 1]) - (currentDeterminant[1, 0] * currentDeterminant[0, 1]); //calculate a * c, b * d
        }
    }
}
