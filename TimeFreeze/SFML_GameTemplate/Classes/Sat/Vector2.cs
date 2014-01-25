using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    struct Vector2
    {
        float x;
        float y;


        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public static Vector2 UnitX
        {
            get { return new Vector2(1f, 0f); }
        }
        public static Vector2 UnitY
        {
            get { return new Vector2(0f, 1f); }
        }
        public static Vector2 Zero
        {
            get { return new Vector2(0f, 0f); }
        }

        public Vector2(float n)
            : this(n,n)
        {
        }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(Vector2f v)
            : this(v.X, v.Y)
        {
        }
        public Vector2(Vector2i v)
            : this(v.X, v.Y)
        {
        }
        public Vector2(Vector2u v)
            : this(v.X, v.Y)
        {
        }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.x*v2.x + v1.y * v2.y;
        }

        public static Vector2 Normalize(Vector2 v1)
        {
            float l = v1.Length();
            return new Vector2(v1.x/l, v1.y/l);
        }

        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public float LengthSquared()
        {
            return x * x + y * y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.x,-v.y);
        }
        public static Vector2 operator *(float f, Vector2 v)
        {
            return new Vector2(v.x * f, v.y * f);
        }
        public static Vector2 operator *(Vector2 v, float f)
        {
            return new Vector2(v.x * f, v.y * f);
        }
        public static Vector2 operator /(float f, Vector2 v)
        {
            return new Vector2(v.x / f, v.y / f);
        }
        public static Vector2 operator /(Vector2 v, float f)
        {
            return new Vector2(v.x / f, v.y / f);
        }
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return v1.x == v2.x && v1.y == v2.y;
        }
        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1==v2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
 	         return base.GetHashCode();
        }
        public override string ToString()
        {
            return "(" + x + "/" + y + ")";
        }
    }