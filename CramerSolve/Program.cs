using System;
using CramerSolve;

namespace program
{
     public class Equation
     {
          public static (double x, double y) MethodCramera(
               double OneX, double OneY, double OneEqals,
               double TwoX, double TwoY, double TwoEqals)
          {

               double determinator = (OneX * TwoY) - (OneY * TwoX);
               if (Math.Abs(determinator) < double.Epsilon)
               {
                    throw new InvalidOperationException(
                         "The determinant is zero. The system has no solution according to Cramer's method.");
               }

               ProgressBar.Progressbar();

               double DeltaX = (OneEqals * TwoY) - (TwoEqals * OneY);
               double DeltaY = (OneX * TwoEqals) - (TwoX * OneEqals);

               double x = DeltaX / determinator;
               double y = DeltaY / determinator;

               return (x, y);
          }

          public static (double x, double y, double z) MethodCrameraZ(
               double OneX, double OneY, double OneZ, double OneEqals,
               double TwoX, double TwoY, double TwoZ, double TwoEqals,
               double ThreeX, double ThreeY, double ThreeZ, double ThreeEqals)
          {
               double determinator =
                    OneX * (TwoY * ThreeZ - TwoZ * ThreeY) -
                    OneY * (TwoX * ThreeZ - TwoZ * ThreeX) +
                    OneZ * (TwoX * ThreeY - TwoY * ThreeX);

               if (Math.Abs(determinator) < double.Epsilon)
               {
                    throw new InvalidOperationException(
                         "The determinant is zero. The system has no solution according to Cramer's method.");
               }

               ProgressBar.Progressbar();

               double deltaX =
                    OneEqals * (TwoY * ThreeZ - TwoZ * ThreeY) -
                    OneY * (TwoEqals * ThreeZ - TwoZ * ThreeEqals) +
                    OneZ * (TwoEqals * ThreeY - TwoY * ThreeEqals);

               double deltaY =
                    OneX * (TwoEqals * ThreeZ - TwoZ * ThreeEqals) -
                    OneEqals * (TwoX * ThreeZ - TwoZ * ThreeX) +
                    OneZ * (TwoX * ThreeEqals - TwoEqals * ThreeX);

               double deltaZ =
                    OneX * (TwoY * ThreeEqals - TwoEqals * ThreeY) -
                    OneY * (TwoX * ThreeEqals - TwoEqals * ThreeX) +
                    OneEqals * (TwoX * ThreeY - TwoY * ThreeX);

               double x = deltaX / determinator;
               double y = deltaY / determinator;
               double z = deltaZ / determinator;

               return (x, y, z);
          }

          public class UserInput
          {
               public static double ReadDouble(string prompt)
               {
                    Console.Write(prompt);
                    while (true)
                    {
                         if (double.TryParse(Console.ReadLine(), out double result))
                              return result;
                         Console.Write("Error! Enter a number: ");
                    }
               }

             static int ReadChoice()
               {
                    Console.WriteLine("\nSelect system type:");
                    Console.WriteLine("1 - System 2x2 (2 equations, 2 unknowns)");
                    Console.WriteLine("2 - System 3x3 (3 equations, 3 unknowns)");
                    Console.WriteLine("0 - Exit");

                    while (true)
                    {
                         Console.Write("Your choice: ");
                         if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= 2)
                              return choice;
                         Console.Write("Error! Enter 0, 1 or 2: ");
                    }
               }

               internal class Program
               {
                    static void Main(string[] args)
                    {
                         Console.Write("Solving equations using Cramer's method");
                         var choice = UserInput.ReadChoice();
                         while (choice != 0)
                         {
                              try
                              {
                                   switch (choice)
                                   {
                                        case 1: SolveSystem2x2(); break;
                                        case 2: SolveSystem3x3(); break;
                                   }
                              }
                              catch (Exception ex)
                              {
                                   Console.WriteLine(ex.Message);
                              }
                         } while (choice != 0);
                    }

                  public static void SolveSystem2x2()
                    {
                         Console.WriteLine("\n=== System 2x2 ===");
                         Console.WriteLine("Format: a1*x + b1*y = c1");
                         Console.WriteLine("         a2*x + b2*y = c2\n");

                         double[] coefficients = new double[6];
                         string[] prompts = { "a1", "b1", "c1", "a2", "b2", "c2" };

                         for (int i = 0; i < 6; i++)
                         {
                              coefficients[i] = UserInput.ReadDouble($"Enter {prompts[i]}: ");
                         }

                         Console.WriteLine($"\nSystem:");
                         Console.WriteLine($"{coefficients[0]}x + {coefficients[1]}y = {coefficients[2]}");
                         Console.WriteLine($"{coefficients[3]}x + {coefficients[4]}y = {coefficients[5]}");

                         var result = Equation.MethodCramera(
                              coefficients[0], coefficients[1], coefficients[2],
                              coefficients[3], coefficients[4], coefficients[5]);

                         Console.WriteLine("ANSWER");
                         Console.WriteLine("---------------------");
                         Console.WriteLine($"x = {result.x:F3},\ny = {result.y:F3}");
                         Console.WriteLine("---------------------");

                    }

                   public static void SolveSystem3x3()
                    {
                         Console.WriteLine("\n=== System 3x3 ===");
                         Console.WriteLine("Format: a1*x + b1*y + c1*z = d1");
                         Console.WriteLine("         a2*x + b2*y + c2*z = d2");
                         Console.WriteLine("         a3*x + b3*y + c3*z = d3\n");

                         double[] coefficients = new double[12];
                         string[] prompts = { "a1", "b1", "c1", "d1", "a2", "b2", "c2", "d2", "a3", "b3", "c3", "d3" };

                         for (int i = 0; i < 12; i++)
                         {
                              coefficients[i] = UserInput.ReadDouble($"Enter {prompts[i]}: ");
                         }

                         Console.WriteLine($"\nSystem:");
                         Console.WriteLine($"{coefficients[0]}x + {coefficients[1]}y + {coefficients[2]}z = {coefficients[3]}");
                         Console.WriteLine($"{coefficients[4]}x + {coefficients[5]}y + {coefficients[6]}z = {coefficients[7]}");
                         Console.WriteLine($"{coefficients[8]}x + {coefficients[9]}y + {coefficients[10]}z = {coefficients[11]}");

                         var result = Equation.MethodCrameraZ(
                              coefficients[0], coefficients[1], coefficients[2], coefficients[3],
                              coefficients[4], coefficients[5], coefficients[6], coefficients[7],
                              coefficients[8], coefficients[9], coefficients[10], coefficients[11]);

                         Console.WriteLine("ANSWER");
                         Console.WriteLine("---------------------");
                         Console.WriteLine($"x = {result.x:F3},\ny = {result.y:F3}, \nz = {result.z:F3}");
                         Console.WriteLine("---------------------");

                    }
               }
          }
     }
}