using System;

namespace Raytracing
{
    using point3 = vec3;
    public class sphere : hittable
    {
        public point3 center { get; private set; }
        public double radius { get; private set; }
        public sphere(point3 cen, double r)
        {
            this.center = cen;
            this.radius = r;
        }

        public bool hit(ray r, double t_min, double t_max, ref hit_record rec)
        {
            vec3 oc = r.origin() - this.center;
            var a = r.direction().length_squared();
            var half_b = vec3.dot(oc, r.direction());
            var c = oc.length_squared() - radius*radius;

            var discriminant = half_b*half_b - a*c;
            if (discriminant < 0) return false;
            var sqrtd = Math.Sqrt(discriminant);

            var root = (-half_b - sqrtd) / a;
            if (root < t_min || t_max < root) {
                root = (-half_b + sqrtd) / a;
                if (root < t_min || t_max < root) return false;
            }

            rec.t = root;
            rec.p = r.at(rec.t);
            vec3 outward_normal = (rec.p - center) / radius;
            rec.set_face_normal(r, outward_normal);
            return true;
        }
    }
}