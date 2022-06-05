using System;

namespace Raytracing
{
    using point3 = vec3;
    public class camera
    {
        public point3 origin { get; private set; }
        public point3 lower_left_corner { get; private set; }
        public vec3 horizontal { get; private set; }
        public vec3 vertical { get; private set; }
        public double aspect_ratio { get; set; }
        public double viewport_height { get; set; }
        public double viewport_width { get; set; }
        public double focal_length { get; set; }

        public camera()
        {
            this.aspect_ratio = 16.0 / 9.0;
            this.viewport_height = 2.0;
            this.viewport_width = aspect_ratio * viewport_height;
            this.focal_length = 1.0;

            this.origin = new point3(0.0, 0.0, 0.0);
            this.horizontal = new point3(viewport_width, 0.0, 0.0);
            this.vertical = new vec3(0.0, viewport_height, 0.0);
            this.lower_left_corner = origin - horizontal/2 - vertical/2 - new vec3(0, 0, focal_length);
        }

        public ray get_ray(double u, double v)
        {
            return new ray(origin, lower_left_corner + horizontal*u + vertical*v - origin);
        }
    };
}