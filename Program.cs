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

        public static color ray_color(ray r, hittable world)
        {
            hit_record rec = new hit_record();
            if (world.hit(r, 0, double.PositiveInfinity, ref rec))
            {
                return (rec.normal + new color(1, 1, 1))*0.5;
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

            // World
            hittable_list world = new hittable_list();
            world.add(new sphere(new point3(0, 0, -1), 0.5));
            world.add(new sphere(new point3(0, -100.5, -1), 100));

            // Camera
            var viewport_height = 2.0;
            var viewport_width = aspect_ratio * viewport_height;
            var focal_length = 1.0;

            var origin = new point3(0,0,0);
            var horizontal = new vec3(viewport_width, 0, 0);
            var vertical = new vec3(0, viewport_height, 0);
            var lower_left_corner = origin - horizontal/2 - vertical/2 - new vec3(0, 0, focal_length);

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
                var v = ((double)j/(double)(image_height-1));
                for (int i = 0; i < image_width; i++)
                {
                    var u = (double)i/(double)(image_width-1);
                    ray r = new ray(origin, lower_left_corner + horizontal*u + vertical*v - origin);
                    color pixel_color = ray_color(r, world);
                    colorFuncs.write_color(pixel_color);
                }
            }

        }
    }
}