using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGBA colour value as floats.
    /// </summary>
    public struct ColourF
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 1.0f.
        /// </remarks>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public ColourF(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
            A = 1;
        }
        /// <summary>
        /// Creates a colour from RGBA values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        /// <param name="a">The alpha component of the colour.</param>
        public ColourF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Create a colour from an already defined colour stored as bytes with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as bytes.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public ColourF(Colour3 colour, float alpha)
        {
            R = colour.R * ByteToFloat;
            G = colour.G * ByteToFloat;
            B = colour.B * ByteToFloat;
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public ColourF(ColourF3 colour, float alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }

        /// <summary>
        /// The red component of the colour.
        /// </summary>
        public float R { get; set; }
        /// <summary>
        /// The green component of the colour.
        /// </summary>
        public float G { get; set; }
        /// <summary>
        /// The blue component of the colour.
        /// </summary>
        public float B { get; set; }
        /// <summary>
        /// The alpha component of the colour.
        /// </summary>
        public float A { get; set; }

        /// <summary>
        /// Returns this colour stored as HSL values.
        /// </summary>
        public Vector3 ToHsl() => ColourF3.ToHsl(R, G, B);
        /// <summary>
        /// Creates a colour from HLS values.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        /// <param name="a">THe alpha component of the colour.</param>
        public static ColourF FromHsl(double h, double s, double l, float a = 1f)
            => new ColourF(ColourF3.FromHsl(h, s, l), a);
        /// <summary>
        /// Creates a colour from a wavelength of light.
        /// </summary>
        /// <remarks>
        /// Sourced from https://stackoverflow.com/questions/3407942/rgb-values-of-visible-spectrum/22681410#22681410.
        /// </remarks>
        /// <param name="l">The wavelength in nm. 400 - 700</param>
        public static ColourF FromWavelength(float l, float a = 1f)
            => new ColourF(ColourF3.FromWavelength(l), a);

#nullable enable
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }
        public string ToString(string? format)
        {
            return $"R:{R.ToString(format)}, G:{G.ToString(format)}, B:{B.ToString(format)}, A:{A.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return (obj is ColourF f &&
                R == f.R &&
                G == f.G &&
                B == f.B &&
                A == f.A) ||
                (obj is Colour c &&
                R == (c.R * 255f) &&
                G == (c.G * 255f) &&
                B == (c.B * 255f) &&
                A == (c.A * 255f));
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public static bool operator ==(ColourF l, ColourF r) => l.Equals(r);
        public static bool operator !=(ColourF l, ColourF r) => !l.Equals(r);

        public static bool operator ==(ColourF l, Colour r) => l.Equals(r);
        public static bool operator !=(ColourF l, Colour r) => !l.Equals(r);

        public static explicit operator Colour(ColourF c)
        {
            return new Colour(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255),
                (byte)(c.A * 255));
        }

        public static explicit operator ColourF3(ColourF c)
        {
            return new ColourF3(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(ColourF c)
        {
            return new Colour3(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }

        public static explicit operator Vector4<byte>(ColourF c)
        {
            return new Vector4<byte>((byte)(c.R * 255), (byte)(c.G * 255), (byte)(c.B * 255), (byte)(c.A * 255));
        }
        public static explicit operator ColourF(Vector4<byte> v)
        {
            return new ColourF(v.X * ByteToFloat, v.Y * ByteToFloat, v.Z * ByteToFloat, v.W * ByteToFloat);
        }

        public static explicit operator Vector4<int>(ColourF c)
        {
            return new Vector4<int>((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255), (int)(c.A * 255));
        }
        public static explicit operator ColourF(Vector4<int> v)
        {
            return new ColourF(v.X * ByteToFloat, v.Y * ByteToFloat, v.Z * ByteToFloat, v.W * ByteToFloat);
        }

        public static explicit operator Vector4<float>(ColourF c)
        {
            return new Vector4<float>(c.R, c.G, c.B, c.A);
        }
        public static explicit operator ColourF(Vector4<float> v)
        {
            return new ColourF(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4I(ColourF c)
        {
            return new Vector4I((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255), (int)(c.A * 255));
        }
        public static explicit operator ColourF(Vector4I v)
        {
            return new ColourF(v.X * ByteToFloat, v.Y * ByteToFloat, v.Z * ByteToFloat, v.W * ByteToFloat);
        }

        public static explicit operator Vector4(ColourF c)
        {
            return new Vector4(c.R, c.G, c.B, c.A);
        }
        public static explicit operator ColourF(Vector4 v)
        {
            return new ColourF((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
        }

        public const float ByteToFloat = /*0.00392156862745098f*/ (float)1 / 255;

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static ColourF Zero { get; } = new ColourF(0, 0, 0, 0);

        // Pink
        public static ColourF MediumVioletRed { get; } = new ColourF(0.780392f, 0.082353f, 0.521569f);
        public static ColourF DeepPink { get; } = new ColourF(1f, 0.078431f, 0.576471f);
        public static ColourF PaleVioletRed { get; } = new ColourF(0.858824f, 0.439216f, 0.576471f);
        public static ColourF HotPink { get; } = new ColourF(1f, 0.411765f, 0.705882f);
        public static ColourF LightPink { get; } = new ColourF(1f, 0.713726f, 0.756863f);
        public static ColourF Pink { get; } = new ColourF(1f, 0.752941f, 0.796078f);

        // Red
        public static ColourF DarkRed { get; } = new ColourF(0.545098f, 0f, 0f);
        public static ColourF Red { get; } = new ColourF(1f, 0f, 0f);
        public static ColourF Firebrick { get; } = new ColourF(0.698039f, 0.133333f, 0.133333f);
        public static ColourF Crimson { get; } = new ColourF(0.862745f, 0.078431f, 0.235294f);
        public static ColourF IndianRed { get; } = new ColourF(0.803922f, 0.360784f, 0.360784f);
        public static ColourF LightCoral { get; } = new ColourF(0.941177f, 0.501961f, 0.501961f);
        public static ColourF Salmon { get; } = new ColourF(0.980392f, 0.501961f, 0.447059f);
        public static ColourF DarkSalmon { get; } = new ColourF(0.913726f, 0.588235f, 0.478431f);
        public static ColourF LightSalmon { get; } = new ColourF(1f, 0.627451f, 0.478431f);

        // Orange
        public static ColourF OrangeRed { get; } = new ColourF(1f, 0.270588f, 0f);
        public static ColourF Tomato { get; } = new ColourF(1f, 0.388235f, 0.278431f);
        public static ColourF DarkOrange { get; } = new ColourF(1f, 0.54902f, 0f);
        public static ColourF Coral { get; } = new ColourF(1f, 0.498039f, 0.313726f);
        public static ColourF Orange { get; } = new ColourF(1f, 0.647059f, 0f);

        // Yellow
        public static ColourF DarkKhaki { get; } = new ColourF(0.741177f, 0.717647f, 0.419608f);
        public static ColourF Gold { get; } = new ColourF(1f, 0.843137f, 0f);
        public static ColourF Khaki { get; } = new ColourF(0.941177f, 0.901961f, 0.54902f);
        public static ColourF PeachPuff { get; } = new ColourF(1f, 0.854902f, 0.72549f);
        public static ColourF Yellow { get; } = new ColourF(1f, 1f, 0f);
        public static ColourF PaleGoldenrod { get; } = new ColourF(0.933333f, 0.909804f, 0.666667f);
        public static ColourF Moccasin { get; } = new ColourF(1f, 0.894118f, 0.709804f);
        public static ColourF PapayaWhip { get; } = new ColourF(1f, 0.937255f, 0.835294f);
        public static ColourF LightGoldenrodYellow { get; } = new ColourF(0.980392f, 0.980392f, 0.823529f);
        public static ColourF LemonChiffon { get; } = new ColourF(1f, 0.980392f, 0.803922f);
        public static ColourF LightYellow { get; } = new ColourF(1f, 1f, 0.878431f);

        // Brown
        public static ColourF Maroon { get; } = new ColourF(0.501961f, 0f, 0f);
        public static ColourF Brown { get; } = new ColourF(0.647059f, 0.164706f, 0.164706f);
        public static ColourF SaddleBrown { get; } = new ColourF(0.545098f, 0.270588f, 0.07451f);
        public static ColourF Sienna { get; } = new ColourF(0.627451f, 0.321569f, 0.176471f);
        public static ColourF Chocolate { get; } = new ColourF(0.823529f, 0.411765f, 0.117647f);
        public static ColourF DarkGoldenrod { get; } = new ColourF(0.721569f, 0.52549f, 0.043137f);
        public static ColourF Peru { get; } = new ColourF(0.803922f, 0.521569f, 0.247059f);
        public static ColourF RosyBrown { get; } = new ColourF(0.737255f, 0.560784f, 0.560784f);
        public static ColourF Goldenrod { get; } = new ColourF(0.854902f, 0.647059f, 0.12549f);
        public static ColourF SandyBrown { get; } = new ColourF(0.956863f, 0.643137f, 0.376471f);
        public static ColourF Tan { get; } = new ColourF(0.823529f, 0.705882f, 0.54902f);
        public static ColourF Burlywood { get; } = new ColourF(0.870588f, 0.721569f, 0.529412f);
        public static ColourF Wheat { get; } = new ColourF(0.960784f, 0.870588f, 0.701961f);
        public static ColourF NavajoWhite { get; } = new ColourF(1f, 0.870588f, 0.678431f);
        public static ColourF Bisque { get; } = new ColourF(1f, 0.894118f, 0.768628f);
        public static ColourF BlanchedAlmond { get; } = new ColourF(1f, 0.921569f, 0.803922f);
        public static ColourF Cornsilk { get; } = new ColourF(1f, 0.972549f, 0.862745f);

        // Purple
        public static ColourF Indigo { get; } = new ColourF(0.294118f, 0f, 0.509804f);
        public static ColourF Purple { get; } = new ColourF(0.501961f, 0f, 0.501961f);
        public static ColourF DarkMagenta { get; } = new ColourF(0.545098f, 0f, 0.545098f);
        public static ColourF DarkViolet { get; } = new ColourF(0.580392f, 0f, 0.827451f);
        public static ColourF DarkSlateBlue { get; } = new ColourF(0.282353f, 0.239216f, 0.545098f);
        public static ColourF BlueViolet { get; } = new ColourF(0.541177f, 0.168628f, 0.886275f);
        public static ColourF DarkOrchid { get; } = new ColourF(0.6f, 0.196078f, 0.8f);
        public static ColourF Fuchsia { get; } = new ColourF(1f, 0f, 1f);
        public static ColourF Magenta { get; } = new ColourF(1f, 0f, 1f);
        public static ColourF SlateBlue { get; } = new ColourF(0.415686f, 0.352941f, 0.803922f);
        public static ColourF MediumSlateBlue { get; } = new ColourF(0.482353f, 0.407843f, 0.933333f);
        public static ColourF MediumOrchid { get; } = new ColourF(0.729412f, 0.333333f, 0.827451f);
        public static ColourF MediumPurple { get; } = new ColourF(0.576471f, 0.439216f, 0.858824f);
        public static ColourF Orchid { get; } = new ColourF(0.854902f, 0.439216f, 0.839216f);
        public static ColourF Violet { get; } = new ColourF(0.933333f, 0.509804f, 0.933333f);
        public static ColourF Plum { get; } = new ColourF(0.866667f, 0.627451f, 0.866667f);
        public static ColourF Thistle { get; } = new ColourF(0.847059f, 0.74902f, 0.847059f);
        public static ColourF Lavender { get; } = new ColourF(0.901961f, 0.901961f, 0.980392f);

        // Blue
        public static ColourF MidnightBlue { get; } = new ColourF(0.098039f, 0.098039f, 0.439216f);
        public static ColourF Navy { get; } = new ColourF(0f, 0f, 0.501961f);
        public static ColourF DarkBlue { get; } = new ColourF(0f, 0f, 0.545098f);
        public static ColourF MediumBlue { get; } = new ColourF(0f, 0f, 0.803922f);
        public static ColourF Blue { get; } = new ColourF(0f, 0f, 1f);
        public static ColourF RoyalBlue { get; } = new ColourF(0.254902f, 0.411765f, 0.882353f);
        public static ColourF SteelBlue { get; } = new ColourF(0.27451f, 0.509804f, 0.705882f);
        public static ColourF DodgerBlue { get; } = new ColourF(0.117647f, 0.564706f, 1f);
        public static ColourF DeepSkyBlue { get; } = new ColourF(0f, 0.74902f, 1f);
        public static ColourF CornflowerBlue { get; } = new ColourF(0.392157f, 0.584314f, 0.929412f);
        public static ColourF SkyBlue { get; } = new ColourF(0.529412f, 0.807843f, 0.921569f);
        public static ColourF LightSkyBlue { get; } = new ColourF(0.529412f, 0.807843f, 0.980392f);
        public static ColourF LightSteelBlue { get; } = new ColourF(0.690196f, 0.768628f, 0.870588f);
        public static ColourF LightBlue { get; } = new ColourF(0.678431f, 0.847059f, 0.901961f);
        public static ColourF PowderBlue { get; } = new ColourF(0.690196f, 0.878431f, 0.901961f);

        // Turquoise
        public static ColourF Teal { get; } = new ColourF(0f, 0.501961f, 0.501961f);
        public static ColourF DarkCyan { get; } = new ColourF(0f, 0.545098f, 0.545098f);
        public static ColourF LightSeaGreen { get; } = new ColourF(0.12549f, 0.698039f, 0.666667f);
        public static ColourF CadetBlue { get; } = new ColourF(0.372549f, 0.619608f, 0.627451f);
        public static ColourF DarkTurquoise { get; } = new ColourF(0f, 0.807843f, 0.819608f);
        public static ColourF MediumTurquoise { get; } = new ColourF(0.282353f, 0.819608f, 0.8f);
        public static ColourF Turquoise { get; } = new ColourF(0.25098f, 0.878431f, 0.815686f);
        public static ColourF Aqua { get; } = new ColourF(0f, 1f, 1f);
        public static ColourF Cyan { get; } = new ColourF(0f, 1f, 1f);
        public static ColourF Aquamarine { get; } = new ColourF(0.498039f, 1f, 0.831373f);
        public static ColourF PaleTurquoise { get; } = new ColourF(0.686275f, 0.933333f, 0.933333f);
        public static ColourF LightCyan { get; } = new ColourF(0.878431f, 1f, 1f);

        // Green
        public static ColourF DarkGreen { get; } = new ColourF(0f, 0.392157f, 0f);
        public static ColourF Green { get; } = new ColourF(0f, 0.501961f, 0f);
        public static ColourF DarkOliveGreen { get; } = new ColourF(0.333333f, 0.419608f, 0.184314f);
        public static ColourF ForestGreen { get; } = new ColourF(0.133333f, 0.545098f, 0.133333f);
        public static ColourF SeaGreen { get; } = new ColourF(0.180392f, 0.545098f, 0.341177f);
        public static ColourF Olive { get; } = new ColourF(0.501961f, 0.501961f, 0f);
        public static ColourF OliveDrab { get; } = new ColourF(0.419608f, 0.556863f, 0.137255f);
        public static ColourF MediumSeaGreen { get; } = new ColourF(0.235294f, 0.701961f, 0.443137f);
        public static ColourF LimeGreen { get; } = new ColourF(0.196078f, 0.803922f, 0.196078f);
        public static ColourF Lime { get; } = new ColourF(0f, 1f, 0f);
        public static ColourF SpringGreen { get; } = new ColourF(0f, 1f, 0.498039f);
        public static ColourF MediumSpringGreen { get; } = new ColourF(0f, 0.980392f, 0.603922f);
        public static ColourF DarkSeaGreen { get; } = new ColourF(0.560784f, 0.737255f, 0.560784f);
        public static ColourF MediumAquamarine { get; } = new ColourF(0.4f, 0.803922f, 0.666667f);
        public static ColourF YellowGreen { get; } = new ColourF(0.603922f, 0.803922f, 0.196078f);
        public static ColourF LawnGreen { get; } = new ColourF(0.486275f, 0.988235f, 0f);
        public static ColourF Chartreuse { get; } = new ColourF(0.498039f, 1f, 0f);
        public static ColourF LightGreen { get; } = new ColourF(0.564706f, 0.933333f, 0.564706f);
        public static ColourF GreenYellow { get; } = new ColourF(0.678431f, 1f, 0.184314f);
        public static ColourF PaleGreen { get; } = new ColourF(0.596079f, 0.984314f, 0.596079f);

        // White
        public static ColourF MistyRose { get; } = new ColourF(1f, 0.894118f, 0.882353f);
        public static ColourF AntiqueWhite { get; } = new ColourF(0.980392f, 0.921569f, 0.843137f);
        public static ColourF Linen { get; } = new ColourF(0.980392f, 0.941177f, 0.901961f);
        public static ColourF Beige { get; } = new ColourF(0.960784f, 0.960784f, 0.862745f);
        public static ColourF WhiteSmoke { get; } = new ColourF(0.960784f, 0.960784f, 0.960784f);
        public static ColourF LavenderBlush { get; } = new ColourF(1f, 0.941177f, 0.960784f);
        public static ColourF OldLace { get; } = new ColourF(0.992157f, 0.960784f, 0.901961f);
        public static ColourF AliceBlue { get; } = new ColourF(0.941177f, 0.972549f, 1f);
        public static ColourF Seashell { get; } = new ColourF(1f, 0.960784f, 0.933333f);
        public static ColourF GhostWhite { get; } = new ColourF(0.972549f, 0.972549f, 1f);
        public static ColourF Honeydew { get; } = new ColourF(0.941177f, 1f, 0.941177f);
        public static ColourF FloralWhite { get; } = new ColourF(1f, 0.980392f, 0.941177f);
        public static ColourF Azure { get; } = new ColourF(0.941177f, 1f, 1f);
        public static ColourF MintCream { get; } = new ColourF(0.960784f, 1f, 0.980392f);
        public static ColourF Snow { get; } = new ColourF(1f, 0.980392f, 0.980392f);
        public static ColourF Ivory { get; } = new ColourF(1f, 1f, 0.941177f);
        public static ColourF White { get; } = new ColourF(1f, 1f, 1f);

        // Black
        public static ColourF Black { get; } = new ColourF(0f, 0f, 0f);
        public static ColourF DarkSlateGrey { get; } = new ColourF(0.184314f, 0.309804f, 0.309804f);
        public static ColourF DimGrey { get; } = new ColourF(0.411765f, 0.411765f, 0.411765f);
        public static ColourF SlateGrey { get; } = new ColourF(0.439216f, 0.501961f, 0.564706f);
        public static ColourF DarkGrey { get; } = new ColourF(0.501961f, 0.501961f, 0.501961f);
        public static ColourF LightSlateGrey { get; } = new ColourF(0.466667f, 0.533333f, 0.6f);
        public static ColourF Grey { get; } = new ColourF(0.662745f, 0.662745f, 0.662745f);
        public static ColourF Silver { get; } = new ColourF(0.752941f, 0.752941f, 0.752941f);
        public static ColourF LightGrey { get; } = new ColourF(0.827451f, 0.827451f, 0.827451f);
        public static ColourF Gainsboro { get; } = new ColourF(0.862745f, 0.862745f, 0.862745f);
    }
}
