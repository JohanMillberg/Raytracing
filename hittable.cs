using System;

namespace Raytracing
{
    using point3 = vec3;
    public struct hit_record
    {
        public hit_record(point3 p, vec3 normal, double t)
        {
            this.p = p;
            this.normal = normal;
            this.t = t;
            this.front_face = false;
        }
        public point3 p { get; set; }
        public vec3 normal { get; set; }

        public double t { get; set; }
        public bool front_face { get; set; }

        public void set_face_normal(ray r, vec3 outward_normal)
        {
            this.front_face = vec3.dot(r.direction(), outward_normal) < 0;
            // normal is outward normal if front_face is true, otherwise -outward normal
            this.normal = front_face ? outward_normal : -outward_normal;
        }
    }

    public abstract class hittable
    {
        public abstract bool hit(ray r, double t_min, double t_max, ref hit_record rec);
    }
}