using System;

namespace Assignment03OOP
{
    class Program
    {
        public class Point3D : ICloneable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public Point3D()
            {
                X = 0;
                Y = 0;
                Z = 0;
            }

            public Point3D(int x, int y, int z) : this()
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override string ToString()
            {
                return $"Point Coordinates: ({X}, {Y}, {Z})";
            }

            public object Clone()
            {
                return new Point3D(X, Y, Z);
            }
        }

        public class Maths
        {
            public static int Add(int a, int b)
            {
                return a + b;
            }

            public static int Subtract(int a, int b)
            {
                return a - b;
            }

            public static int Multiply(int a, int b)
            {
                return a * b;
            }

            public static double Divide(int a, int b)
            {
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                return (double)a / b;
            }
        }

        public abstract class Discount
        {
            public abstract string Name { get; }
            public abstract decimal CalculateDiscount(decimal price, int quantity);
        }

        public class PercentageDiscount : Discount
        {
            public decimal Percentage { get; set; }

            public PercentageDiscount(decimal percentage)
            {
                Percentage = percentage;
            }

            public override string Name
            {
                get
                {
                    return "Percentage Discount";
                }
            }


            public override decimal CalculateDiscount(decimal price, int quantity)
            {
                return price * quantity * (Percentage / 100);
            }
        }

        public class FlatDiscount : Discount
        {
            public decimal FlatAmount { get; set; }

            public FlatDiscount(decimal flatAmount)
            {
                FlatAmount = flatAmount;
            }

            public override string Name => "Flat Discount";

            public override decimal CalculateDiscount(decimal price, int quantity)
            {
                return FlatAmount * Math.Min(quantity, 1);
            }
        }

        public class BuyOneGetOneDiscount : Discount
        {
            public override string Name => "Buy One Get One Discount";

            public override decimal CalculateDiscount(decimal price, int quantity)
            {
                return (price / 2) * (quantity / 2);
            }
        }

        public abstract class User
        {
            public abstract string Name { get; }
            public abstract Discount GetDiscount();
        }

        public class RegularUser : User
        {
            public override string Name => "Regular User";

            public override Discount GetDiscount()
            {
                return new PercentageDiscount(5);
            }
        }

        public class PremiumUser : User
        {
            public override string Name => "Premium User";

            public override Discount GetDiscount()
            {
                return new FlatDiscount(100);
            }
        }

        public class GuestUser : User
        {
            public override string Name => "Guest User";

            public override Discount GetDiscount()
            {
                return null;
            }
        }



        static void Main(string[] args)
        {
            #region First Project
            // Define 3D Point Class and the basic Constructors (use chaining in constructors).
            // Override the ToString Function to produce this output:
            Point3D P = new Point3D(10, 10, 10);
            Console.WriteLine(P.ToString());
            // Output: "Point Coordinates: (10, 10, 10)".

            // Read from the User the Coordinates for 2 points P1, P2 (Check the input using try
            // Parse, Parse, Convert).
            int x = 0, y = 0, z = 0;

            Console.WriteLine("Enter the coordinates for the first point:");
            x = int.Parse(Console.ReadLine());
            if (!int.TryParse(Console.ReadLine(), out y)) y = 0;
            z = Convert.ToInt32(Console.ReadLine());
            Point3D P1 = new Point3D(x, y, z);

            Console.WriteLine("Enter the coordinates for the second point:");
            x = int.Parse(Console.ReadLine());
            if (!int.TryParse(Console.ReadLine(), out y)) y = 0;
            z = Convert.ToInt32(Console.ReadLine());
            Point3D P2 = new Point3D(x, y, z);

            // Try to use ==
            // If (P1 == P2) Does it work properly?

            bool areEqual = P1 == P2;
            //The comparison P1 == P2 doesn't work because the == operator is not defined for the Point3D class. By default, it compares references instead of values


            // Define an array of points and sort this array based on X & Y coordinates.
            Point3D[] points = new Point3D[]
            {
                new Point3D(5, 5, 5),
                new Point3D(4, 3, 3),
                new Point3D(1, 1, 1),
                new Point3D(4, 4, 4),
                new Point3D(2, 2, 2)
            };

            Array.Sort(points, (p1, p2) =>
            {
                if (p1.X == p2.X)
                {
                    return p1.Y.CompareTo(p2.Y);
                }
                return p1.X.CompareTo(p2.X);
            });

            foreach (Point3D point in points)
            {
                Console.WriteLine(point);
            }
            // Implement ICloneable interface to be able to clone the object.
            Point3D P3 = (Point3D)P1.Clone();
            #endregion

            #region Second Project
            // Define Class Maths that has four methods:
            // ● Add()
            // ● Subtract()
            // ● Multiply()
            // ● Divide()
            // Each of them takes two parameters. Call each method in Main().
            // NOTE: Modify the program so that you do not have to create an instance of class to call the four methods.
            Console.WriteLine(Maths.Add(5, 10));
            Console.WriteLine(Maths.Subtract(5, 10));
            Console.WriteLine(Maths.Multiply(5, 10));
            Console.WriteLine(Maths.Divide(5, 10));
            #endregion

            #region Third Project
            // You are tasked with designing a system for an e-commerce platform that calculates
            // discounts for different types of users and products.
            // This system should utilize abstraction and include the following parts:

            // Part 1: Abstract Discount Class
            // Create an abstract class Discount with:
            // An abstract method CalculateDiscount(decimal price, int quantity) that returns the
            // discount amount based on the original price and quantity.
            // A Name property to store the type of discount.

            // Part 2: Specific Discounts
            // Implement the following concrete discount classes:
            // o PercentageDiscount:
            //   ▪ Accepts a percentage (e.g., 10%).
            //   ▪ Formula: Discount Amount = Price × Quantity × (Percentage / 100)
            // o FlatDiscount:
            //   ▪ Accepts a fixed amount to be deducted (e.g., $50).
            //   ▪ Formula: Discount Amount = Flat Amount × min(Quantity, 1)
            // o BuyOneGetOneDiscount:
            //   ▪ Applies a 50% discount if the quantity is greater than 1.
            //   ▪ Formula: Discount Amount = (Price / 2) × (Quantity ÷ 2)

            // Part 3: Discount Applicability
            // Create an abstract class User with:
            // o A Name property to store the user name.
            // o An abstract method GetDiscount() that returns a Discount object.
            // Implement the following specific user types:
            // o RegularUser: Applies a PercentageDiscount of 5%.
            // o PremiumUser: Applies a FlatDiscount of $100.
            // o GuestUser: No discount is applied.

            // Part 4: Integration
            // Write a program that:
            // o Asks the user to input their type (Regular, Premium, or Guest).
            // o Allows the user to input product details (price and quantity).
            // o Calculates and displays the total discount and final price after applying the
            //   appropriate discount.

            User user = null;
            Discount discount = null;

            Console.WriteLine("Enter the user type (Regular, Premium, or Guest):");
            string userType = Console.ReadLine();

            if (userType == "Regular")
            {
                user = new RegularUser();
            }
            else if (userType == "Premium")
            {
                user = new PremiumUser();
            }
            else if (userType == "Guest")
            {
                user = new GuestUser();
            }

            if (user != null)
            {
                discount = user.GetDiscount();

                Console.WriteLine("Enter the product price:");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter the product quantity:");
                int quantity = int.Parse(Console.ReadLine());

                decimal totalDiscount = discount.CalculateDiscount(price, quantity);
                decimal finalPrice = price * quantity - totalDiscount;

                Console.WriteLine($"Total Discount: {totalDiscount:C}");
                Console.WriteLine($"Final Price: {finalPrice:C}");
            }

            else
            {
                Console.WriteLine("Invalid user type.");
            }
            #endregion

        }
    }
}
