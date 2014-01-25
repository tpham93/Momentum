using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Window;
using SFML.Graphics;

using Sat.Collision;

namespace Sat.Object.Shape
{
    abstract class Shape2DSAT
    {

        /*
         * Xna-specific attributes for drawing and handling calculations
         */
        private Vector2 middlePoint;

        public Vector2 MiddlePoint
        {
            get { return middlePoint; }
            set { middlePoint = value; }
        }
        private bool moveable;

        public abstract EShapeType ShapeType
        {
            get;
        }


        /*
         * Physic-spcefic attributes for Calculations
         */
        protected float radius;

        protected Vector2 position;
        public Vector2f Position
        {
            get
            {
                Vector2 p = Position_;
                return new Vector2f(p.X, p.Y);
            }
            set
            {
                Position_ = new Vector2(value);
            }
        }
        public abstract Vector2 Position_
        {
            get;
            set;
        }

        protected float rotation;
        public float Rotation
        {
            get { return Rotation_ * (float)Math.PI / 180f; }
            set { Rotation_ = value * 180f / (float)Math.PI; }
        }
        public abstract float Rotation_
        {
            get;
            set;
        }
        public bool Moveable
        {
            get { return moveable; }
            set { moveable = value; }
        }

        protected float area;
        public float Area
        {
            get { return area; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected Shape2DSAT()
        {
            this.radius = 0f;
            this.position = Vector2.Zero;
            this.middlePoint = Vector2.Zero;
            this.moveable = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected Shape2DSAT(float radius, Vector2 position, Vector2 middlePoint, bool moveable)
            : this()
        {
            this.radius = radius;
            this.position = position;
            this.middlePoint = middlePoint;
            this.moveable = moveable;
        }

        /// <summary>
        /// checks if the object is intersecting with another object
        /// </summary>
        /// <param name="o">Object which is to be checked for an intersection</param>
        /// <returns>true if it intersects</returns>
        public IntersectData intersects(Shape2DSAT o)
        {
            if (moveable)
            {
                float minDistance = this.radius + o.radius;
                if (minDistance * minDistance >= (o.Position_ - Position_).LengthSquared())
                {
                    IntersectData intersectData = null;
                    switch (o.ShapeType)
                    {
                        case EShapeType.PolygonShape:
                            intersectData = intersects((PolygonShapeSAT)o);
                            break;
                        case EShapeType.CircleShape:
                            intersectData = intersects((CircleShapeSAT)o);
                            break;
                        default:
                            return null;
                    }

                    if (Vector2.Dot(intersectData.Mtv, -o.Position_ + Position_) > 0)
                    {
                        intersectData.Mtv *= -1;
                    }
                    return intersectData;
                }
            }
            return new IntersectData();
        }

        public abstract Range getProjectionRange(Vector2 v);


        public abstract IntersectData intersects(PolygonShapeSAT o);
        public abstract IntersectData intersects(CircleShapeSAT o);

        /// <summary>
        /// checks if a point is inside of the object
        /// </summary>
        /// <param name="point">Point which needs to be checked</param>
        /// <returns>true if point is inside of the object</returns>
        public abstract bool contains(Vector2 point);

        //public static void handleCollision(IntersectData data, Shape2D o1, Shape2D o2)
        //{
        //    if (o1.moveable)
        //    {
        //        if (o2.moveable)
        //        {
        //            o1.Position += (data.Mtv * data.PenetrationDepth / 2);
        //            o2.Position -= (data.Mtv * data.PenetrationDepth / 2);
        //        }
        //        else
        //            o1.Position += (data.Mtv * data.PenetrationDepth);
        //    }
        //}
    }
}
