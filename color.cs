using System;

namespace Raytracing
{
    using color = vec3;
    class colorUtils
    {
        public void write_color(color pixel_color, int samples_per_pixel)
        {

            var r = pixel_color.x();
            var g = pixel_color.y();
            var b = pixel_color.z();

            var scale = 1.0 / samples_per_pixel;
            r = Math.Sqrt(scale*r);
            g = Math.Sqrt(scale*g);
            b = Math.Sqrt(scale*b);

            string fileName = "image.ppm";
            TextWriter sw = new StreamWriter((fileName), true);
            sw.WriteLine($"{(int)(256 * helper_class.clamp(r, 0.0, 0.999))} {(int)(256 * helper_class.clamp(g, 0.0, 0.999))} {(int)(256 * helper_class.clamp(b, 0.0, 0.999))}");
            sw.Close();
        }
    }
}