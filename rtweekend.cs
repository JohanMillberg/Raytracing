using System;

namespace Raytracing
{
    public static class helper_class
    {
        public static double degrees_to_radians(double degrees)
        {
            return degrees*Math.PI / 180.0;
        }

        public static double random_double()
        {
            Random random = new Random();
            double random_value = random.NextDouble();
            return random_value;
        }

        public static double random_double(double min, double max)
        {
            Random random = new Random();
            return (random.NextDouble() * (max-min) + min);
        }

        public static double clamp(double x, double min, double max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }
    }
}