using System;

namespace Raytracing
{
    using color = vec3;
    class colorUtils
    {
        public void write_color(color pixel_color)
        {
            string fileName = "image.ppm";
            TextWriter sw = new StreamWriter((fileName), true);
            sw.WriteLine($"{(int)(255.99*pixel_color.x())} {(int)(255.99 * pixel_color.y())} {(int)(255.99 * pixel_color.z())}");
            sw.Close();
        }
    }
}