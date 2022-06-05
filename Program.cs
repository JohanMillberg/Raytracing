using System;

namespace Raytracing
{
    using point3 = vec3;
    using color = vec3;
    class Program
    {

        public static double hit_sphere(point3 center, double radius, ray r)
        {
            vec3 oc = r.origin() - center;
            var a = r.direction().length_squared();
            var half_b = vec3.dot(oc, r.direction());
            var c = oc.length_squared() - radius*radius;
            var discriminant = half_b*half_b - a*c;
            if (discriminant < 0)
                return -1.0;
            else
                return (-half_b - Math.Sqrt(discriminant)) / a;
        }

        public static color ray_color(ray r, hittable world, int depth)
        {
            hit_record rec = new hit_record();
            if (depth <= 0)
            {
                return new color(0,0,0);
            }

            if (world.hit(r, 0.001, double.PositiveInfinity, ref rec))
            {
                point3 target = rec.p + vec3.random_in_hemisphere(rec.normal);
                return (ray_color(new ray(rec.p, target - rec.p), world, depth-1))*0.5;
            }

            vec3 unit_direction = vec3.unit_vector(r.direction());
            var t = 0.5*(unit_direction.y() + 1.0);
            return new color(1.0, 1.0, 1.0)*(1.0-t) + new color(0.5, 0.7, 1.0)*t;
        }
        static void Main(string[] args) {
            // Image
            colorUtils colorFuncs = new colorUtils();
            const double aspect_ratio = 16.0/9.0;
            const int image_width = 400;
            const int image_height = (int) (image_width / aspect_ratio);
            const int samples_per_pixel = 100;
            const int max_depth = 50;

            // World
            hittable_list world = new hittable_list();
            world.add(new sphere(new point3(0, 0, -1), 0.5));
            world.add(new sphere(new point3(0, -100.5, -1), 100));

            // Camera
            camera cam = new camera();

            // Render
            string fileName = "image.ppm";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"P3");
                sw.WriteLine($"{image_width} {image_height}");
                sw.WriteLine($"255");
            }

            for (int j = image_height-1; j >= 0; j--)
            {
                for (int i = 0; i < image_width; i++)
                {
                    color pixel_color = new color(0, 0, 0);
                    for (int s = 0; s < samples_per_pixel; s++)
                    {
                        var v = (j + helper_class.random_double()) / (image_height-1);
                        var u = (i + helper_class.random_double()) / (image_width-1);
                        ray r = cam.get_ray(u, v);
                        pixel_color += ray_color(r, world, max_depth);
                    }
                    colorFuncs.write_color(pixel_color, samples_per_pixel);
                }
            }

        }
    }
}