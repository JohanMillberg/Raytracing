using System;
using System.Collections.Generic;

namespace Raytracing
{
    public class hittable_list : hittable
    {
        public List<hittable> objects = new List<hittable>();

        public void clear()
        {
            this.objects.Clear();
        }

        public void add(hittable item)
        {
            this.objects.Add(item);
        }

        public override bool hit(ray r, double t_min, double t_max, ref hit_record rec)
        {
            hit_record temp_rec = new hit_record();
            bool hit_anything = false;
            var closest_so_far = t_max;

            foreach (hittable item in this.objects)
            {
                if (item.hit(r, t_min, closest_so_far, ref temp_rec))
                {
                    hit_anything = true;
                    closest_so_far = temp_rec.t;
                    rec = temp_rec;
                }
            }

            return hit_anything;
        }
    }
}