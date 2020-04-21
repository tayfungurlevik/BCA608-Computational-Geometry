using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplePolygonGenerator.Extensions
{
    public static class PointExtension
    {
        public static double NoktaAcisi(this Point point )
        {
            return Math.Atan(point.Y / point.X);
        }
        public static double NoktaAcisi(this Point point,Point merkez)
        {
            var temp = new Point();
            temp.X = point.X - merkez.X;
            temp.Y = point.Y - merkez.Y;
            double aci= Math.Atan2(temp.Y,   temp.X)*180/Math.PI;
            if (aci < 0)
            {
                return 2 * 180 - Math.Abs(aci);
            }
            else
                return aci;
        }
    }
}
