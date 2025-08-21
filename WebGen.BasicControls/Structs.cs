    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;
    using System.Text;

namespace WebGen.Controls.Structs
{
    /// <summary>
     /// 厚度。
     /// </summary>
    public struct Thickness
    {
        /// <summary>
        /// 左侧厚度
        /// </summary>
        private readonly double _left;

        /// <summary>
        /// 上侧厚度
        /// </summary>
        private readonly double _top;

        /// <summary>
        /// 右侧厚度
        /// </summary>
        private readonly double _right;

        /// <summary>
        /// 下侧厚度
        /// </summary>
        private readonly double _bottom;

        /// <summary>
        /// 初始化 Thickness 结构的新实例。
        /// </summary>
        /// <param name="uniformLength">应用于所有边的统一厚度。</param>
        public Thickness(double uniformLength)
        {
            _left = _top = _right = _bottom = uniformLength;
        }

        /// <summary>
        /// 初始化 Thickness 结构的新实例。
        /// </summary>
        /// <param name="horizontal">左右两侧的厚度。</param>
        /// <param name="vertical">上下两侧的厚度。</param>
        public Thickness(double horizontal, double vertical)
        {
            _left = _right = horizontal;
            _top = _bottom = vertical;
        }

        /// <summary>
        /// 初始化 Thickness 结构的新实例。
        /// </summary>
        /// <param name="left">左侧厚度。</param>
        /// <param name="top">上侧厚度。</param>
        /// <param name="right">右侧厚度。</param>
        /// <param name="bottom">下侧厚度。</param>
        public Thickness(double left, double top, double right, double bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        /// <summary>
        /// 获取左侧厚度。
        /// </summary>
        public double Left => _left;

        /// <summary>
        /// 获取上侧厚度。
        /// </summary>
        public double Top => _top;

        /// <summary>
        /// 获取右侧厚度。
        /// </summary>
        public double Right => _right;

        /// <summary>
        /// 获取下侧厚度。
        /// </summary>
        public double Bottom => _bottom;

        /// <summary>
        /// 获取一个值，指示四个边是否都是 0。
        /// </summary>
        public bool IsEmpty => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

        /// <summary>
        /// 比较两个 Thickness 是否相等。
        /// </summary>
        /// <param name="a">第一个 Thickness。</param>
        /// <param name="b">第二个 Thickness。</param>
        /// <returns>是否相等。</returns>
        public static bool operator ==(Thickness a, Thickness b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// 比较两个 Thickness 是否不相等。
        /// </summary>
        /// <param name="a">第一个 Thickness。</param>
        /// <param name="b">第二个 Thickness。</param>
        /// <returns>是否不相等。</returns>
        public static bool operator !=(Thickness a, Thickness b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// 将两个 Thickness 相加。
        /// </summary>
        /// <param name="a">第一个 Thickness。</param>
        /// <param name="b">第二个 Thickness。</param>
        /// <returns>相加后的 Thickness。</returns>
        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(
                a.Left + b.Left,
                a.Top + b.Top,
                a.Right + b.Right,
                a.Bottom + b.Bottom);
        }
    }

#if false //如果你要用这个的话，还是去试试 WebGL 弄出来一个 Avalonia 吧

    /// <summary>
    /// A 2x3 matrix.
    /// </summary>
    public struct Matrix
    {
        private readonly double _m11;
        private readonly double _m12;
        private readonly double _m21;
        private readonly double _m22;
        private readonly double _m31;
        private readonly double _m32;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// </summary>
        /// <param name="m11">The first element of the first row.</param>
        /// <param name="m12">The second element of the first row.</param>
        /// <param name="m21">The first element of the second row.</param>
        /// <param name="m22">The second element of the second row.</param>
        /// <param name="offsetX">The first element of the third row.</param>
        /// <param name="offsetY">The second element of the third row.</param>
        public Matrix(
            double m11,
            double m12,
            double m21,
            double m22,
            double offsetX,
            double offsetY)
        {
            _m11 = m11;
            _m12 = m12;
            _m21 = m21;
            _m22 = m22;
            _m31 = offsetX;
            _m32 = offsetY;
        }

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix Identity => new Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity => Equals(Identity);

        /// <summary>
        /// HasInverse Property - returns true if this matrix is invertable, false otherwise.
        /// </summary>
        public bool HasInverse => GetDeterminant() != 0;

        /// <summary>
        /// The first element of the first row
        /// </summary>
        public double M11 => _m11;

        /// <summary>
        /// The second element of the first row
        /// </summary>
        public double M12 => _m12;

        /// <summary>
        /// The first element of the second row
        /// </summary>
        public double M21 => _m21;

        /// <summary>
        /// The second element of the second row
        /// </summary>
        public double M22 => _m22;

        /// <summary>
        /// The first element of the third row
        /// </summary>
        public double M31 => _m31;

        /// <summary>
        /// The second element of the third row
        /// </summary>
        public double M32 => _m32;

        /// <summary>
        /// Multiplies two matrices together and returns the resulting matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The product matrix.</returns>
        public static Matrix operator *(Matrix value1, Matrix value2)
        {
            return new Matrix(
                (value1.M11 * value2.M11) + (value1.M12 * value2.M21),
                (value1.M11 * value2.M12) + (value1.M12 * value2.M22),
                (value1.M21 * value2.M11) + (value1.M22 * value2.M21),
                (value1.M21 * value2.M12) + (value1.M22 * value2.M22),
                (value1._m31 * value2.M11) + (value1._m32 * value2.M21) + value2._m31,
                (value1._m31 * value2.M12) + (value1._m32 * value2.M22) + value2._m32);
        }

        /// <summary>
        /// Negates the given matrix by multiplying all values by -1.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Matrix operator -(Matrix value)
        {
            return value.Invert();
        }

        /// <summary>
        /// Returns a boolean indicating whether the given matrices are equal.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>True if the matrices are equal; False otherwise.</returns>
        public static bool operator ==(Matrix value1, Matrix value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given matrices are not equal.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>True if the matrices are not equal; False if they are equal.</returns>
        public static bool operator !=(Matrix value1, Matrix value2)
        {
            return !value1.Equals(value2);
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix CreateRotation(double radians)
        {
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            return new Matrix(cos, sin, -sin, cos, 0, 0);
        }

        /// <summary>
        /// Creates a scale matrix from the given X and Y components.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix CreateScale(double xScale, double yScale)
        {
            return CreateScale(new Vector(xScale, yScale));
        }

        /// <summary>
        /// Creates a scale matrix from the given vector scale.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix CreateScale(Vector scales)
        {
            return new Matrix(scales.X, 0, 0, scales.Y, 0, 0);
        }

        /// <summary>
        /// Creates a translation matrix from the given vector.
        /// </summary>
        /// <param name="position">The translation position.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix CreateTranslation(Vector position)
        {
            return CreateTranslation(position.X, position.Y);
        }

        /// <summary>
        /// Creates a translation matrix from the given X and Y components.
        /// </summary>
        /// <param name="xPosition">The X position.</param>
        /// <param name="yPosition">The Y position.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix CreateTranslation(double xPosition, double yPosition)
        {
            return new Matrix(1.0, 0.0, 0.0, 1.0, xPosition, yPosition);
        }

        /// <summary>
        /// Converts an ange in degrees to radians.
        /// </summary>
        /// <param name="angle">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        public static double ToRadians(double angle)
        {
            return angle * 0.0174532925;
        }

        /// <summary>
        /// Calculates the determinant for this matrix.
        /// </summary>
        /// <returns>The determinant.</returns>
        /// <remarks>
        /// The determinant is calculated by expanding the matrix with a third column whose
        /// values are (0,0,1).
        /// </remarks>
        public double GetDeterminant()
        {
            return (_m11 * _m22) - (_m12 * _m21);
        }


        /// <summary>
        /// Returns a boolean indicating whether the matrix is equal to the other given matrix.
        /// </summary>
        /// <param name="other">The other matrix to test equality against.</param>
        /// <returns>True if this matrix is equal to other; False otherwise.</returns>
        public bool Equals(Matrix other)
        {
            return _m11 == other.M11 &&
                   _m12 == other.M12 &&
                   _m21 == other.M21 &&
                   _m22 == other.M22 &&
                   _m31 == other.M31 &&
                   _m32 == other.M32;
        }

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix))
            {
                return false;
            }

            return Equals((Matrix)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return M11.GetHashCode() + M12.GetHashCode() +
                   M21.GetHashCode() + M22.GetHashCode() +
                   M31.GetHashCode() + M32.GetHashCode();
        }

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            return string.Format(
                ci,
                "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}",
                M11.ToString(ci),
                M12.ToString(ci),
                M21.ToString(ci),
                M22.ToString(ci),
                M31.ToString(ci),
                M32.ToString(ci));
        }

        /// <summary>
        /// Inverts the Matrix.
        /// </summary>
        /// <returns>The inverted matrix.</returns>
        public Matrix Invert()
        {
            if (GetDeterminant() == 0)
            {
                throw new InvalidOperationException("Transform is not invertible.");
            }

            double d = GetDeterminant();

            return new Matrix(
                _m22 / d,
                -_m12 / d,
                -_m21 / d,
                _m11 / d,
                ((_m21 * _m32) - (_m22 * _m31)) / d,
                ((_m12 * _m31) - (_m11 * _m32)) / d);
        }
    }
    /// <summary>
    /// Defines a vector.
    /// </summary>
    public struct Vector
    {
        /// <summary>
        /// The X vector.
        /// </summary>
        private readonly double _x;

        /// <summary>
        /// The Y vector.
        /// </summary>
        private readonly double _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> structure.
        /// </summary>
        /// <param name="x">The X vector.</param>
        /// <param name="y">The Y vector.</param>
        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Gets the X vector.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// Gets the Y vector.
        /// </summary>
        public double Y => _y;

        /// <summary>
        /// Converts the <see cref="Vector"/> to a <see cref="Point"/>.
        /// </summary>
        /// <param name="a">The vector.</param>
        public static explicit operator Point(Vector a)
        {
            return new Point(a._x, a._y);
        }

        public struct Point
        {
            /// <summary>
            /// The X position.
            /// </summary>
            private readonly double _x;

            /// <summary>
            /// The Y position.
            /// </summary>
            private readonly double _y;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point"/> structure.
            /// </summary>
            /// <param name="x">The X position.</param>
            /// <param name="y">The Y position.</param>
            public Point(double x, double y)
            {
                _x = x;
                _y = y;
            }

            /// <summary>
            /// Gets the X position.
            /// </summary>
            public double X => _x;

            /// <summary>
            /// Gets the Y position.
            /// </summary>
            public double Y => _y;

            /// <summary>
            /// Converts the <see cref="Point"/> to a <see cref="Vector"/>.
            /// </summary>
            /// <param name="p">The point.</param>
            public static implicit operator Vector(Point p)
            {
                return new Vector(p._x, p._y);
            }

            /// <summary>
            /// Negates a point.
            /// </summary>
            /// <param name="a">The point.</param>
            /// <returns>The negated point.</returns>
            public static Point operator -(Point a)
            {
                return new Point(-a._x, -a._y);
            }

            /// <summary>
            /// Checks for equality between two <see cref="Point"/>s.
            /// </summary>
            /// <param name="left">The first point.</param>
            /// <param name="right">The second point.</param>
            /// <returns>True if the points are equal; otherwise false.</returns>
            public static bool operator ==(Point left, Point right)
            {
                return left.X == right.X && left.Y == right.Y;
            }

            /// <summary>
            /// Checks for unequality between two <see cref="Point"/>s.
            /// </summary>
            /// <param name="left">The first point.</param>
            /// <param name="right">The second point.</param>
            /// <returns>True if the points are unequal; otherwise false.</returns>
            public static bool operator !=(Point left, Point right)
            {
                return !(left == right);
            }

            /// <summary>
            /// Adds two points.
            /// </summary>
            /// <param name="a">The first point.</param>
            /// <param name="b">The second point.</param>
            /// <returns>A point that is the result of the addition.</returns>
            public static Point operator +(Point a, Point b)
            {
                return new Point(a._x + b._x, a._y + b._y);
            }

            /// <summary>
            /// Adds a vector to a point.
            /// </summary>
            /// <param name="a">The point.</param>
            /// <param name="b">The vector.</param>
            /// <returns>A point that is the result of the addition.</returns>
            public static Point operator +(Point a, Vector b)
            {
                return new Point(a._x + b.X, a._y + b.Y);
            }

            /// <summary>
            /// Subtracts two points.
            /// </summary>
            /// <param name="a">The first point.</param>
            /// <param name="b">The second point.</param>
            /// <returns>A point that is the result of the subtraction.</returns>
            public static Point operator -(Point a, Point b)
            {
                return new Point(a._x - b._x, a._y - b._y);
            }

            /// <summary>
            /// Subtracts a vector from a point.
            /// </summary>
            /// <param name="a">The point.</param>
            /// <param name="b">The vector.</param>
            /// <returns>A point that is the result of the subtraction.</returns>
            public static Point operator -(Point a, Vector b)
            {
                return new Point(a._x - b.X, a._y - b.Y);
            }

            /// <summary>
            /// Multiplies a point by a factor coordinate-wise
            /// </summary>
            /// <param name="p">Point to multiply</param>
            /// <param name="k">Factor</param>
            /// <returns>Points having its coordinates multiplied</returns>
            public static Point operator *(Point p, double k) => new Point(p.X * k, p.Y * k);

            /// <summary>
            /// Multiplies a point by a factor coordinate-wise
            /// </summary>
            /// <param name="p">Point to multiply</param>
            /// <param name="k">Factor</param>
            /// <returns>Points having its coordinates multiplied</returns>
            public static Point operator *(double k, Point p) => new Point(p.X * k, p.Y * k);

            /// <summary>
            /// Divides a point by a factor coordinate-wise
            /// </summary>
            /// <param name="p">Point to divide by</param>
            /// <param name="k">Factor</param>
            /// <returns>Points having its coordinates divided</returns>
            public static Point operator /(Point p, double k) => new Point(p.X / k, p.Y / k);

            /// <summary>
            /// Applies a matrix to a point.
            /// </summary>
            /// <param name="point">The point.</param>
            /// <param name="matrix">The matrix.</param>
            /// <returns>The resulting point.</returns>
            public static Point operator *(Point point, Matrix matrix)
            {
                return new Point(
                    (point.X * matrix.M11) + (point.Y * matrix.M21) + matrix.M31,
                    (point.X * matrix.M12) + (point.Y * matrix.M22) + matrix.M32);
            }

            /// <summary>
            /// Parses a <see cref="Point"/> string.
            /// </summary>
            /// <param name="s">The string.</param>
            /// <param name="culture">The current culture.</param>
            /// <returns>The <see cref="Thickness"/>.</returns>
            public static Point Parse(string s, CultureInfo culture)
            {
                var parts = s.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

                if (parts.Count == 2)
                {
                    return new Point(double.Parse(parts[0], culture), double.Parse(parts[1], culture));
                }
                else
                {
                    throw new FormatException("Invalid Thickness.");
                }
            }

            /// <summary>
            /// Checks for equality between a point and an object.
            /// </summary>
            /// <param name="obj">The object.</param>
            /// <returns>
            /// True if <paramref name="obj"/> is a point that equals the current point.
            /// </returns>
            public override bool Equals(object obj)
            {
                if (obj is Point)
                {
                    var other = (Point)obj;
                    return X == other.X && Y == other.Y;
                }

                return false;
            }

            /// <summary>
            /// Returns a hash code for a <see cref="Point"/>.
            /// </summary>
            /// <returns>The hash code.</returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = (hash * 23) + _x.GetHashCode();
                    hash = (hash * 23) + _y.GetHashCode();
                    return hash;
                }
            }

            /// <summary>
            /// Returns the string representation of the point.
            /// </summary>
            /// <returns>The string representation of the point.</returns>
            public override string ToString()
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _x, _y);
            }

            /// <summary>
            /// Transforms the point by a matrix.
            /// </summary>
            /// <param name="transform">The transform.</param>
            /// <returns>The transformed point.</returns>
            public Point Transform(Matrix transform)
            {
                var x = X;
                var y = Y;
                var xadd = y * transform.M21 + transform.M31;
                var yadd = x * transform.M12 + transform.M32;
                x *= transform.M11;
                x += xadd;
                y *= transform.M22;
                y += yadd;
                return new Point(x, y);
            }

            /// <summary>
            /// Returns a new point with the specified X coordinate.
            /// </summary>
            /// <param name="x">The X coordinate.</param>
            /// <returns>The new point.</returns>
            public Point WithX(double x)
            {
                return new Point(x, _y);
            }

            /// <summary>
            /// Returns a new point with the specified Y coordinate.
            /// </summary>
            /// <param name="y">The Y coordinate.</param>
            /// <returns>The new point.</returns>
            public Point WithY(double y)
            {
                return new Point(_x, y);
            }
        }


        /// <summary>
        /// Calculates the dot product of two vectors
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>The dot product</returns>
        public static double operator *(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Length of the vector
        /// </summary>
        public double Length => Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector operator -(Vector a)
        {
            return new Vector(-a._x, -a._y);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>A vector that is the result of the addition.</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a._x + b._x, a._y + b._y);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>A vector that is the result of the subtraction.</returns>
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a._x - b._x, a._y - b._y);
        }

        /// <summary>
        /// Returns the string representation of the point.
        /// </summary>
        /// <returns>The string representation of the point.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _x, _y);
        }

        /// <summary>
        /// Returns a new vector with the specified X coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <returns>The new vector.</returns>
        public Vector WithX(double x)
        {
            return new Vector(x, _y);
        }

        /// <summary>
        /// Returns a new vector with the specified Y coordinate.
        /// </summary>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The new vector.</returns>
        public Vector WithY(double y)
        {
            return new Vector(_x, y);
        }
    }

    /// <summary>
    /// Describes the thickness of a frame around a rectangle.
    /// </summary>
    public struct Thickness
    {
        /// <summary>
        /// The thickness on the left.
        /// </summary>
        private readonly double _left;

        /// <summary>
        /// The thickness on the top.
        /// </summary>
        private readonly double _top;

        /// <summary>
        /// The thickness on the right.
        /// </summary>
        private readonly double _right;

        /// <summary>
        /// The thickness on the bottom.
        /// </summary>
        private readonly double _bottom;

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure.
        /// </summary>
        /// <param name="uniformLength">The length that should be applied to all sides.</param>
        public Thickness(double uniformLength)
        {
            _left = _top = _right = _bottom = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure.
        /// </summary>
        /// <param name="horizontal">The thickness on the left and right.</param>
        /// <param name="vertical">The thickness on the top and bottom.</param>
        public Thickness(double horizontal, double vertical)
        {
            _left = _right = horizontal;
            _top = _bottom = vertical;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure.
        /// </summary>
        /// <param name="left">The thickness on the left.</param>
        /// <param name="top">The thickness on the top.</param>
        /// <param name="right">The thickness on the right.</param>
        /// <param name="bottom">The thickness on the bottom.</param>
        public Thickness(double left, double top, double right, double bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        /// <summary>
        /// Gets the thickness on the left.
        /// </summary>
        public double Left => _left;

        /// <summary>
        /// Gets the thickness on the top.
        /// </summary>
        public double Top => _top;

        /// <summary>
        /// Gets the thickness on the right.
        /// </summary>
        public double Right => _right;

        /// <summary>
        /// Gets the thickness on the bottom.
        /// </summary>
        public double Bottom => _bottom;

        /// <summary>
        /// Gets a value indicating whether all sides are set to 0.
        /// </summary>
        public bool IsEmpty => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

        /// <summary>
        /// Compares two Thicknesses.
        /// </summary>
        /// <param name="a">The first thickness.</param>
        /// <param name="b">The second thickness.</param>
        /// <returns>The equality.</returns>
        public static bool operator ==(Thickness a, Thickness b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two Thicknesses.
        /// </summary>
        /// <param name="a">The first thickness.</param>
        /// <param name="b">The second thickness.</param>
        /// <returns>The unequality.</returns>
        public static bool operator !=(Thickness a, Thickness b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Adds two Thicknesses.
        /// </summary>
        /// <param name="a">The first thickness.</param>
        /// <param name="b">The second thickness.</param>
        /// <returns>The equality.</returns>
        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(
                a.Left + b.Left,
                a.Top + b.Top,
                a.Right + b.Right,
                a.Bottom + b.Bottom);
        }

        /// <summary>
        /// Adds a Thickness to a Size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="thickness">The thickness.</param>
        /// <returns>The equality.</returns>
        public static Size operator +(Size size, Thickness thickness)
        {
            return new Size(
                size.Width + thickness.Left + thickness.Right,
                size.Height + thickness.Top + thickness.Bottom);
        }

        /// <summary>
        /// Subtracts a Thickness from a Size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="thickness">The thickness.</param>
        /// <returns>The equality.</returns>
        public static Size operator -(Size size, Thickness thickness)
        {
            return new Size(
                size.Width - (thickness.Left + thickness.Right),
                size.Height - (thickness.Top + thickness.Bottom));
        }

        /// <summary>
        /// Parses a <see cref="Thickness"/> string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="culture">The current culture.</param>
        /// <returns>The <see cref="Thickness"/>.</returns>
        public static Thickness Parse(string s, CultureInfo culture)
        {
            var parts = s.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            switch (parts.Count)
            {
                case 1:
                    var uniform = double.Parse(parts[0], culture);
                    return new Thickness(uniform);
                case 2:
                    var horizontal = double.Parse(parts[0], culture);
                    var vertical = double.Parse(parts[1], culture);
                    return new Thickness(horizontal, vertical);
                case 4:
                    var left = double.Parse(parts[0], culture);
                    var top = double.Parse(parts[1], culture);
                    var right = double.Parse(parts[2], culture);
                    var bottom = double.Parse(parts[3], culture);
                    return new Thickness(left, top, right, bottom);
            }

            throw new FormatException("Invalid Thickness.");
        }

        /// <summary>
        /// Checks for equality between a thickness and an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// True if <paramref name="obj"/> is a size that equals the current size.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Thickness)
            {
                Thickness other = (Thickness)obj;
                return Left == other.Left &&
                       Top == other.Top &&
                       Right == other.Right &&
                       Bottom == other.Bottom;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for a <see cref="Thickness"/>.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + Left.GetHashCode();
                hash = (hash * 23) + Top.GetHashCode();
                hash = (hash * 23) + Right.GetHashCode();
                hash = (hash * 23) + Bottom.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns the string representation of the thickness.
        /// </summary>
        /// <returns>The string representation of the thickness.</returns>
        public override string ToString()
        {
            return $"{_left},{_top},{_right},{_bottom}";
        }
    }
    /// <summary>
    /// Defines a size.
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// A size representing infinity.
        /// </summary>
        public static readonly Size Infinity = new Size(double.PositiveInfinity, double.PositiveInfinity);

        /// <summary>
        /// A size representing zero
        /// </summary>
        public static readonly Size Empty = new Size(0, 0);

        /// <summary>
        /// The width.
        /// </summary>
        private readonly double _width;

        /// <summary>
        /// The height.
        /// </summary>
        private readonly double _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> structure.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(double width, double height)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Gets the aspect ratio of the size.
        /// </summary>
        public double AspectRatio => _width / _height;

        /// <summary>
        /// Gets the width.
        /// </summary>
        public double Width => _width;

        /// <summary>
        /// Gets the height.
        /// </summary>
        public double Height => _height;

        /// <summary>
        /// Checks for equality between two <see cref="Size"/>s.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>True if the sizes are equal; otherwise false.</returns>
        public static bool operator ==(Size left, Size right)
        {
            return left._width == right._width && left._height == right._height;
        }

        /// <summary>
        /// Checks for unequality between two <see cref="Size"/>s.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>True if the sizes are unequal; otherwise false.</returns>
        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator *(Size size, Vector scale)
        {
            return new Size(size._width * scale.X, size._height * scale.Y);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator /(Size size, Vector scale)
        {
            return new Size(size._width / scale.X, size._height / scale.Y);
        }

        /// <summary>
        /// Divides a size by another size to produce a scaling factor.
        /// </summary>
        /// <param name="left">The first size</param>
        /// <param name="right">The second size.</param>
        /// <returns>The scaled size.</returns>
        public static Vector operator /(Size left, Size right)
        {
            return new Vector(left._width / right._width, left._height / right._height);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator *(Size size, double scale)
        {
            return new Size(size._width * scale, size._height * scale);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator /(Size size, double scale)
        {
            return new Size(size._width / scale, size._height / scale);
        }

        public static Size operator +(Size size, Size toAdd)
        {
            return new Size(size._width + toAdd._width, size._height + toAdd._height);
        }

        public static Size operator -(Size size, Size toSubstract)
        {
            return new Size(size._width - toSubstract._width, size._height - toSubstract._height);
        }

        /// <summary>
        /// Parses a <see cref="Size"/> string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="culture">The current culture.</param>
        /// <returns>The <see cref="Size"/>.</returns>
        public static Size Parse(string s, CultureInfo culture)
        {
            var parts = s.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            if (parts.Count == 2)
            {
                return new Size(double.Parse(parts[0], culture), double.Parse(parts[1], culture));
            }
            else
            {
                throw new FormatException("Invalid Size.");
            }
        }

        /// <summary>
        /// Constrains the size.
        /// </summary>
        /// <param name="constraint">The size to constrain to.</param>
        /// <returns>The constrained size.</returns>
        public Size Constrain(Size constraint)
        {
            return new Size(
                Math.Min(_width, constraint._width),
                Math.Min(_height, constraint._height));
        }

        /// <summary>
        /// Deflates the size by a <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness">The thickness.</param>
        /// <returns>The deflated size.</returns>
        /// <remarks>The deflated size cannot be less than 0.</remarks>
        public Size Deflate(Thickness thickness)
        {
            return new Size(
                Math.Max(0, _width - thickness.Left - thickness.Right),
                Math.Max(0, _height - thickness.Top - thickness.Bottom));
        }

        /// <summary>
        /// Checks for equality between a size and an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// True if <paramref name="obj"/> is a size that equals the current size.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Size)
            {
                var other = (Size)obj;
                return Width == other.Width && Height == other.Height;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for a <see cref="Size"/>.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + Width.GetHashCode();
                hash = (hash * 23) + Height.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Inflates the size by a <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness">The thickness.</param>
        /// <returns>The inflated size.</returns>
        public Size Inflate(Thickness thickness)
        {
            return new Size(
                _width + thickness.Left + thickness.Right,
                _height + thickness.Top + thickness.Bottom);
        }

        /// <summary>
        /// Returns a new <see cref="Size"/> with the same height and the specified width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns>The new <see cref="Size"/>.</returns>
        public Size WithWidth(double width)
        {
            return new Size(width, _height);
        }

        /// <summary>
        /// Returns a new <see cref="Size"/> with the same width and the specified height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns>The new <see cref="Size"/>.</returns>
        public Size WithHeight(double height)
        {
            return new Size(_width, height);
        }

        /// <summary>
        /// Returns the string representation of the size.
        /// </summary>
        /// <returns>The string representation of the size.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _width, _height);
        }
    }
#endif
}