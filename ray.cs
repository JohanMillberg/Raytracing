using System;

namespace Raytracing
{
    using point3 = vec3;
    public class ray
    {
        public point3 orig { get; private set;}
        public vec3 dir {get; private set;}

        public ray(point3 origin, vec3 direction)
        {
            this.orig = origin;
            this.dir = direction;
        }

        public point3 origin() {return this.orig;}
        public vec3 direction() {return this.dir;}

        public point3 at(double t)
        {
            return this.orig + this.dir*t;
        }
    }
}