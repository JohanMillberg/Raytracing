using System;


namespace Raytracing
{
    public class vec3
    {
        public double[] e;

        public vec3() {
            this.e = new double[3];
            this.e[0] = 0;
            this.e[1] = 0;
            this.e[2] = 0;
        }

        public vec3(double x, double y, double z) {
            this.e = new double[3];
            this.e[0] = x;
            this.e[1] = y;
            this.e[2] = z;
        }

        public double x() { return this.e[0]; }
        public double y() { return this.e[1]; }
        public double z() { return this.e[2]; }

        public static vec3 operator- (vec3 v) { return new vec3(-v.e[0], -v.e[1], -v.e[2]);}

        public double this[int index]
        {
            get => this.e[index];
            set => this.e[index] = value;
        }

        public static vec3 operator+ (vec3 v1, vec3 v2)
        {
            double e0 = v1.e[0] + v2.e[0];
            double e1 = v1.e[1] + v2.e[1];
            double e2 = v1.e[2] + v2.e[2];

            return new vec3(e0, e1, e2);
        }

        public static vec3 operator- (vec3 v1, vec3 v2)
        {
            double e0 = v1.e[0] - v2.e[0];
            double e1 = v1.e[1] - v2.e[1];
            double e2 = v1.e[2] - v2.e[2];

            return new vec3(e0, e1, e2);
        }
        public static vec3 operator* (vec3 v, double t)
        {
            double e0 = v.e[0] * t;
            double e1 = v.e[1] * t;
            double e2 = v.e[2] * t;

            return new vec3(e0, e1, e2);
        }

        public static vec3 operator/ (vec3 v, double t)
        {
            double e0 = v.e[0] / t;
            double e1 = v.e[1] / t;
            double e2 = v.e[2] / t;

            return new vec3(e0, e1, e2);
        }

        public double length()
        {
            return Math.Sqrt(length_squared());
        }

        public double length_squared()
        {
            return this.e[0]*this.e[0] + this.e[1]*this.e[1] + this.e[2]*this.e[2];
        }

        public static double dot(vec3 v1, vec3 v2)
        {
            return v1.e[0]*v2.e[0] + v1.e[1]*v2.e[1] + v1.e[2]*v2.e[2];
        }

        public static vec3 cross(vec3 v1, vec3 v2)
        {
            double e0 = v1.e[1]*v2.e[2] - v1.e[2]*v2.e[1];
            double e1 = v1.e[2]*v2.e[0] - v1.e[0]*v2.e[2];
            double e2 = v1.e[0]*v2.e[1] - v1.e[1]*v2.e[0];

            return new vec3(e0, e1, e2);
        }

        public static vec3 unit_vector(vec3 v)
        {
            return v / v.length();
        }

        public override string ToString()
        {
            return $"({this.e[0]}, {this.e[1]}, {this.e[2]})";
        }

        public static vec3 random()
        {
            return new vec3(helper_class.random_double(), helper_class.random_double(), helper_class.random_double());
        }

        public static vec3 random(double min, double max)
        {
            return new vec3(helper_class.random_double(min, max), helper_class.random_double(min, max), helper_class.random_double(min, max));
        }

        public static vec3 random_in_unit_sphere()
        {
            while (true)
            {
                var p = vec3.random(-1, 1);
                if (p.length_squared() >= 1) continue;
                return p;
            }
        }

        public static vec3 random_unit_vector()
        {
            return unit_vector(random_in_unit_sphere());
        }

        public static vec3 random_in_hemisphere(vec3 normal)
        {
            vec3 in_unit_sphere = random_in_unit_sphere();
            if (dot(in_unit_sphere, normal) > 0.0)
                return in_unit_sphere;
            else
                return -in_unit_sphere;
        }
    }
}