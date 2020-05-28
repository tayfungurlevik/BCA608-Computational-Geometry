using UnityEngine;

public enum IntersectionCases
{
    IsInside,
    IsOnEdge,
    NoIntersection
}
internal class Intersections
{
    //
    // Are two lines intersecting?
    //
    //http://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
    //Notice that there are more than one way to test if two line segments are intersecting
    //but this is the fastest according to https://www.habrador.com/tutorials/math/5-line-line-intersection/
    public static bool LineLine(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
    {
        //To avoid floating point precision issues we can use a small value
        float epsilon = MathUtility.EPSILON;

        bool isIntersecting = false;

        float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

        //Make sure the denominator is > 0, if so the lines are parallel
        if (denominator != 0f)
        {
            float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;
            float u_b = ((l1_p2.x - l1_p1.x) * (l1_p1.y - l2_p1.y) - (l1_p2.y - l1_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

            //Are the line segments intersecting if the end points are the same
            if (shouldIncludeEndPoints)
            {
                //Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
                if (u_a >= 0f + epsilon && u_a <= 1f - epsilon && u_b >= 0f + epsilon && u_b <= 1f - epsilon)
                {
                    isIntersecting = true;
                }
            }
            else
            {
                //Is intersecting if u_a and u_b are between 0 and 1
                if (u_a > 0f + epsilon && u_a < 1f - epsilon && u_b > 0f + epsilon && u_b < 1f - epsilon)
                {
                    isIntersecting = true;
                }
            }

        }

        return isIntersecting;
    }



    //Whats the coordinate of an intersection point between two lines in 2d space if we know they are intersecting
    //http://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
    public static Vector2 GetLineLineIntersectionPoint(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2)
    {
        float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

        float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

        Vector2 intersectionPoint = l1_p1 + u_a * (l1_p2 - l1_p1);

        return intersectionPoint;
    }



    //
    // Line, plane, ray intersection with plane
    //

    //Ray-plane intersection
    //http://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
    public static bool RayPlane(Vector2 planePos, Vector2 planeNormal, Vector2 rayStart, Vector2 rayDir)
    {
        //To avoid floating point precision issues we can add a small value
        float epsilon = MathUtility.EPSILON;

        bool areIntersecting = false;

        float denominator = Vector2.Dot(planeNormal * -1f, rayDir);

        //Debug.Log(denominator);

        //The ray has to point at the surface of the plane
        //The surface of the plane is determined by the normal
        if (denominator > epsilon)
        {
            //Now we have to figur out of the ray starts "inside" of the plane
            //meaning on the other side of the normal
            //If so it can't hit the plane
            Vector2 vecBetween = planePos - rayStart;

            float t = Vector2.Dot(vecBetween, planeNormal * -1f) / denominator;

            //Debug.Log(t);

            if (t >= 0f)
            {
                areIntersecting = true;
            }
        }

        return areIntersecting;
    }

    //Get the coordinate if we know a ray-plane is intersecting
    public static Vector2 GetRayPlaneIntersectionPoint(Vector2 planePos, Vector2 planeNormal, Vector2 rayStart, Vector2 rayDir)
    {
        Vector2 intersectionPoint = GetIntersectionCoordinate(planePos, planeNormal, rayStart, rayDir);

        return intersectionPoint;
    }


    //This is a useful method to find the intersection coordinate if we know we are intersecting
    //Is used for ray-plane, line-plane, plane-plane
    private static Vector2 GetIntersectionCoordinate(Vector2 planePos, Vector2 planeNormal, Vector2 rayStart, Vector2 rayDir)
    {
        float denominator = Vector2.Dot(-planeNormal, rayDir);

        Vector2 vecBetween = planePos - rayStart;

        float t = Vector2.Dot(vecBetween, -planeNormal) / denominator;

        Vector2 intersectionPoint = rayStart + rayDir * t;

        return intersectionPoint;
    }



    //Line-plane intersection
    public static bool LinePlane(Vector2 planePos, Vector2 planeNormal, Vector2 line_p1, Vector2 line_p2)
    {
        //To avoid floating point precision issues we can add a small value
        float epsilon = MathUtility.EPSILON;

        bool areIntersecting = false;

        Vector2 lineDir =( line_p1 - line_p2).normalized;

        float denominator = Vector2.Dot(-planeNormal, lineDir);

        //Debug.Log(denominator);

        //No intersection if the line and plane are perpendicular
        if (denominator > epsilon || denominator < -epsilon)
        {
            Vector2 vecBetween = planePos - line_p1;

            float t = Vector2.Dot(vecBetween, -planeNormal) / denominator;

            Vector2 intersectionPoint = line_p1 + lineDir * t;

            //Gizmos.DrawWireSphere(intersectionPoint, 0.5f);

            if (Geometry.IsPointBetweenPoints(line_p1, line_p2, intersectionPoint))
            {
                areIntersecting = true;
            }
        }

        return areIntersecting;
    }

    //We know a line plane is intersecting and now we want the coordinate of intersection
    public static Vector2 GetLinePlaneIntersectionPoint(Vector2 planePos, Vector2 planeNormal, Vector2 line_p1, Vector2 line_p2)
    {
        Vector2 lineDir = (line_p1 - line_p2).normalized;

        Vector2 intersectionPoint = GetIntersectionCoordinate(planePos, planeNormal, line_p1, lineDir);

        return intersectionPoint;
    }



    //Plane-plane intersection
    public static bool PlanePlane(Vector2 planePos_1, Vector2 planeNormal_1, Vector2 planePos_2, Vector2 planeNormal_2)
    {
        bool areIntersecting = false;

        float dot = Vector2.Dot(planeNormal_1, planeNormal_2);

        //Debug.Log(dot);

        //No intersection if the planes are parallell
        //The are parallell if the dot product is 1 or -1

        //To avoid floating point precision issues we can add a small value
        float one = 1f - MathUtility.EPSILON;

        if (dot < one && dot > -one)
        {
            areIntersecting = true;
        }

        return areIntersecting;
    }

    //If we know two planes are intersecting, what's the point of intersection?
    public static Vector2 GetPlanePlaneIntersectionPoint(Vector2 planePos_1, Vector2 planeNormal_1, Vector2 planePos_2, Vector2 planeNormal_2)
    {
        Vector2 lineDir = new Vector2(planeNormal_2.y, -planeNormal_2.x).normalized;

        Vector2 intersectionPoint = GetIntersectionCoordinate(planePos_1, planeNormal_1, planePos_2, lineDir);

        return intersectionPoint;
    }




  


    //Is a point inside, outside, or on the border of a triangle
    //-1 if outside, 0 if on the border, 1 if inside the triangle
    //BROKEN use if the point is to the left or right of all edges in the triangle
    public static int IsPointInOutsideOnTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p)
    {
        //To avoid floating point precision issues we can add a small value
        float epsilon = MathUtility.EPSILON;

        float zero = 0f + epsilon;
        float one = 1f - epsilon;

        //Based on Barycentric coordinates
        float denominator = ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));

        float a = ((p2.y - p3.y) * (p.x - p3.x) + (p3.x - p2.x) * (p.y - p3.y)) / denominator;
        float b = ((p3.y - p1.y) * (p.x - p3.x) + (p1.x - p3.x) * (p.y - p3.y)) / denominator;
        float c = 1 - a - b;

        int returnValue = -1;

        //The point is on the border, meaning exactly 0 or 1 in a world with no floating precision issues

        //The point is on or within the triangle
        if (a >= zero && a <= one && b >= zero && b <= one && c >= zero && c <= one)
        {
            returnValue = 1;
        }

        return returnValue;
    }












}
