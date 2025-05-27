using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGB colour value as floats.
    /// </summary>
    public struct ColourF3
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public ColourF3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public ColourF3(double r, double g, double b)
        {
            R = (float)r;
            G = (float)g;
            B = (float)b;
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
        /// Inverts just the colour components.
        /// </summary>
        public void Invert()
        {
            R = 1f - R;
            G = 1f - G;
            B = 1f - B;
        }
        /// <summary>
        /// Returns a <see cref="ColourF3"/> with the colour components inverted.
        /// </summary>
        public ColourF3 Inverted()
        {
            return new ColourF3(
                1f - R,
                1f - G,
                1f - B
            );
        }
        
        /// <summary>
        /// Returns this colour stored as HSL values.
        /// </summary>
        public Vector3 ToHsl() => ToHsl(R, G, B);
        /// <summary>
        /// Creates a colour from HLS values.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        public static ColourF3 FromHsl(floatv h, floatv s, floatv l)
        {
            floatv p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            floatv p1 = 2 * l - p2;
            floatv r, g, b;
            if (s == 0)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                r = QqhToRgb(p1, p2, h + 120);
                g = QqhToRgb(p1, p2, h);
                b = QqhToRgb(p1, p2, h - 120);
            }

            return new ColourF3(r, g, b);
        }
        private static floatv QqhToRgb(floatv q1, floatv q2, floatv hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }
        internal static Vector3 ToHsl(floatv r, floatv g, floatv b)
        {
            floatv h;
            floatv s;
            floatv l;

            // Get the maximum and minimum RGB components.
            floatv max = r;
            if (max < g) max = g;
            if (max < b) max = b;

            floatv min = r;
            if (min > g) min = g;
            if (min > b) min = b;

            floatv diff = max - min;
            l = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                s = 0;
                h = 0;  // H is really undefined.
            }
            else
            {
                if (l <= 0.5) s = diff / (max + min);
                else s = diff / (2 - max - min);

                floatv r_dist = (max - r) / diff;
                floatv g_dist = (max - g) / diff;
                floatv b_dist = (max - b) / diff;

                if (r == max) h = b_dist - g_dist;
                else if (g == max) h = 2 + r_dist - b_dist;
                else h = 4 + g_dist - r_dist;

                h *= 60;
                if (h < 0) h += 360;
            }

            return new Vector3(h, s, l);
        }
        /// <summary>
        /// Creates a colour from a wavelength of light.
        /// </summary>
        /// <remarks>
        /// Sourced from https://stackoverflow.com/questions/3407942/rgb-values-of-visible-spectrum/22681410#22681410.
        /// </remarks>
        /// <param name="l">The wavelength in nm. 400 - 700</param>
        public static ColourF3 FromWavelength(float l)
        {
            float t, r=0f, g=0f, b=0f;
                if ((l>=400f)&&(l<410f)) { t=(l-400f)/(410f-400f); r=    +(0.33f*t)-(0.20f*t*t); }
            else if ((l>=410f)&&(l<475f)) { t=(l-410f)/(475f-410f); r=0.14f         -(0.13f*t*t); }
            else if ((l>=545f)&&(l<595f)) { t=(l-545f)/(595f-545f); r=    +(1.98f*t)-(     t*t); }
            else if ((l>=595f)&&(l<650f)) { t=(l-595f)/(650f-595f); r=0.98f+(0.06f*t)-(0.40f*t*t); }
            else if ((l>=650f)&&(l<700f)) { t=(l-650f)/(700f-650f); r=0.65f-(0.84f*t)+(0.20f*t*t); }
                if ((l>=415f)&&(l<475f)) { t=(l-415f)/(475f-415f); g=             +(0.80f*t*t); }
            else if ((l>=475f)&&(l<590f)) { t=(l-475f)/(590f-475f); g=0.8f +(0.76f*t)-(0.80f*t*t); }
            else if ((l>=585f)&&(l<639f)) { t=(l-585f)/(639f-585f); g=0.84f-(0.84f*t)           ; }
                if ((l>=400f)&&(l<475f)) { t=(l-400f)/(475f-400f); b=    +(2.20f*t)-(1.50f*t*t); }
            else if ((l>=475f)&&(l<560f)) { t=(l-475f)/(560f-475f); b=0.7f -(     t)+(0.30f*t*t); }
            
            return new ColourF3(r, g, b);
        }

#nullable enable
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }
        public string ToString(string? format)
        {
            return $"R:{R.ToString(format)}, G:{G.ToString(format)}, B:{B.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return (obj is ColourF3 f && this == f) ||
                (obj is Colour3 c && this == c);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public static bool operator ==(ColourF3 l, ColourF3 r)
        {
            return l.R == r.R &&
                l.G == r.G &&
                l.B == r.B;
        }
        public static bool operator !=(ColourF3 l, ColourF3 r) => !(l == r);

        public static bool operator ==(ColourF3 l, Colour3 r)
        {
            return l.R == (r.R * 255f) &&
                l.G == (r.G * 255f) &&
                l.B == (r.B * 255f);
        }
        public static bool operator !=(ColourF3 l, Colour3 r) => !(l == r);

        public static explicit operator Colour3(ColourF3 c)
        {
            return new Colour3(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }

        public static explicit operator ColourF(ColourF3 c)
        {
            return new ColourF(c.R, c.G, c.B);
        }
        public static explicit operator Colour(ColourF3 c)
        {
            return new Colour(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }

        public static explicit operator Vector3<byte>(ColourF3 c)
        {
            return new Vector3<byte>((byte)(c.R * 255), (byte)(c.G * 255), (byte)(c.B * 255));
        }
        public static explicit operator ColourF3(Vector3<byte> v)
        {
            return new ColourF3(v.X * ColourF.ByteToFloat, v.Y * ColourF.ByteToFloat, v.Z * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3I(ColourF3 c)
        {
            return new Vector3I((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255));
        }
        public static explicit operator ColourF3(Vector3I v)
        {
            return new ColourF3(v.X * ColourF.ByteToFloat, v.Y * ColourF.ByteToFloat, v.Z * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3(ColourF3 c)
        {
            return new Vector3(c.R, c.G, c.B);
        }
        public static explicit operator ColourF3(Vector3 v)
        {
            return new ColourF3(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static ColourF3 Zero { get; } = new ColourF3(0, 0, 0);

        // Pink
        public static ColourF3 MediumVioletRed { get; } = new ColourF3(0.780392f, 0.082353f, 0.521569f);
        public static ColourF3 DeepPink { get; } = new ColourF3(1f, 0.078431f, 0.576471f);
        public static ColourF3 PaleVioletRed { get; } = new ColourF3(0.858824f, 0.439216f, 0.576471f);
        public static ColourF3 HotPink { get; } = new ColourF3(1f, 0.411765f, 0.705882f);
        public static ColourF3 LightPink { get; } = new ColourF3(1f, 0.713726f, 0.756863f);
        public static ColourF3 Pink { get; } = new ColourF3(1f, 0.752941f, 0.796078f);

        // Red
        public static ColourF3 DarkRed { get; } = new ColourF3(0.545098f, 0f, 0f);
        public static ColourF3 Red { get; } = new ColourF3(1f, 0f, 0f);
        public static ColourF3 Firebrick { get; } = new ColourF3(0.698039f, 0.133333f, 0.133333f);
        public static ColourF3 Crimson { get; } = new ColourF3(0.862745f, 0.078431f, 0.235294f);
        public static ColourF3 IndianRed { get; } = new ColourF3(0.803922f, 0.360784f, 0.360784f);
        public static ColourF3 LightCoral { get; } = new ColourF3(0.941177f, 0.501961f, 0.501961f);
        public static ColourF3 Salmon { get; } = new ColourF3(0.980392f, 0.501961f, 0.447059f);
        public static ColourF3 DarkSalmon { get; } = new ColourF3(0.913726f, 0.588235f, 0.478431f);
        public static ColourF3 LightSalmon { get; } = new ColourF3(1f, 0.627451f, 0.478431f);

        // Orange
        public static ColourF3 OrangeRed { get; } = new ColourF3(1f, 0.270588f, 0f);
        public static ColourF3 Tomato { get; } = new ColourF3(1f, 0.388235f, 0.278431f);
        public static ColourF3 DarkOrange { get; } = new ColourF3(1f, 0.54902f, 0f);
        public static ColourF3 Coral { get; } = new ColourF3(1f, 0.498039f, 0.313726f);
        public static ColourF3 Orange { get; } = new ColourF3(1f, 0.647059f, 0f);

        // Yellow
        public static ColourF3 DarkKhaki { get; } = new ColourF3(0.741177f, 0.717647f, 0.419608f);
        public static ColourF3 Gold { get; } = new ColourF3(1f, 0.843137f, 0f);
        public static ColourF3 Khaki { get; } = new ColourF3(0.941177f, 0.901961f, 0.54902f);
        public static ColourF3 PeachPuff { get; } = new ColourF3(1f, 0.854902f, 0.72549f);
        public static ColourF3 Yellow { get; } = new ColourF3(1f, 1f, 0f);
        public static ColourF3 PaleGoldenrod { get; } = new ColourF3(0.933333f, 0.909804f, 0.666667f);
        public static ColourF3 Moccasin { get; } = new ColourF3(1f, 0.894118f, 0.709804f);
        public static ColourF3 PapayaWhip { get; } = new ColourF3(1f, 0.937255f, 0.835294f);
        public static ColourF3 LightGoldenrodYellow { get; } = new ColourF3(0.980392f, 0.980392f, 0.823529f);
        public static ColourF3 LemonChiffon { get; } = new ColourF3(1f, 0.980392f, 0.803922f);
        public static ColourF3 LightYellow { get; } = new ColourF3(1f, 1f, 0.878431f);

        // Brown
        public static ColourF3 Maroon { get; } = new ColourF3(0.501961f, 0f, 0f);
        public static ColourF3 Brown { get; } = new ColourF3(0.647059f, 0.164706f, 0.164706f);
        public static ColourF3 SaddleBrown { get; } = new ColourF3(0.545098f, 0.270588f, 0.07451f);
        public static ColourF3 Sienna { get; } = new ColourF3(0.627451f, 0.321569f, 0.176471f);
        public static ColourF3 Chocolate { get; } = new ColourF3(0.823529f, 0.411765f, 0.117647f);
        public static ColourF3 DarkGoldenrod { get; } = new ColourF3(0.721569f, 0.52549f, 0.043137f);
        public static ColourF3 Peru { get; } = new ColourF3(0.803922f, 0.521569f, 0.247059f);
        public static ColourF3 RosyBrown { get; } = new ColourF3(0.737255f, 0.560784f, 0.560784f);
        public static ColourF3 Goldenrod { get; } = new ColourF3(0.854902f, 0.647059f, 0.12549f);
        public static ColourF3 SandyBrown { get; } = new ColourF3(0.956863f, 0.643137f, 0.376471f);
        public static ColourF3 Tan { get; } = new ColourF3(0.823529f, 0.705882f, 0.54902f);
        public static ColourF3 Burlywood { get; } = new ColourF3(0.870588f, 0.721569f, 0.529412f);
        public static ColourF3 Wheat { get; } = new ColourF3(0.960784f, 0.870588f, 0.701961f);
        public static ColourF3 NavajoWhite { get; } = new ColourF3(1f, 0.870588f, 0.678431f);
        public static ColourF3 Bisque { get; } = new ColourF3(1f, 0.894118f, 0.768628f);
        public static ColourF3 BlanchedAlmond { get; } = new ColourF3(1f, 0.921569f, 0.803922f);
        public static ColourF3 Cornsilk { get; } = new ColourF3(1f, 0.972549f, 0.862745f);

        // Purple
        public static ColourF3 Indigo { get; } = new ColourF3(0.294118f, 0f, 0.509804f);
        public static ColourF3 Purple { get; } = new ColourF3(0.501961f, 0f, 0.501961f);
        public static ColourF3 DarkMagenta { get; } = new ColourF3(0.545098f, 0f, 0.545098f);
        public static ColourF3 DarkViolet { get; } = new ColourF3(0.580392f, 0f, 0.827451f);
        public static ColourF3 DarkSlateBlue { get; } = new ColourF3(0.282353f, 0.239216f, 0.545098f);
        public static ColourF3 BlueViolet { get; } = new ColourF3(0.541177f, 0.168628f, 0.886275f);
        public static ColourF3 DarkOrchid { get; } = new ColourF3(0.6f, 0.196078f, 0.8f);
        public static ColourF3 Fuchsia { get; } = new ColourF3(1f, 0f, 1f);
        public static ColourF3 Magenta { get; } = new ColourF3(1f, 0f, 1f);
        public static ColourF3 SlateBlue { get; } = new ColourF3(0.415686f, 0.352941f, 0.803922f);
        public static ColourF3 MediumSlateBlue { get; } = new ColourF3(0.482353f, 0.407843f, 0.933333f);
        public static ColourF3 MediumOrchid { get; } = new ColourF3(0.729412f, 0.333333f, 0.827451f);
        public static ColourF3 MediumPurple { get; } = new ColourF3(0.576471f, 0.439216f, 0.858824f);
        public static ColourF3 Orchid { get; } = new ColourF3(0.854902f, 0.439216f, 0.839216f);
        public static ColourF3 Violet { get; } = new ColourF3(0.933333f, 0.509804f, 0.933333f);
        public static ColourF3 Plum { get; } = new ColourF3(0.866667f, 0.627451f, 0.866667f);
        public static ColourF3 Thistle { get; } = new ColourF3(0.847059f, 0.74902f, 0.847059f);
        public static ColourF3 Lavender { get; } = new ColourF3(0.901961f, 0.901961f, 0.980392f);

        // Blue
        public static ColourF3 MidnightBlue { get; } = new ColourF3(0.098039f, 0.098039f, 0.439216f);
        public static ColourF3 Navy { get; } = new ColourF3(0f, 0f, 0.501961f);
        public static ColourF3 DarkBlue { get; } = new ColourF3(0f, 0f, 0.545098f);
        public static ColourF3 MediumBlue { get; } = new ColourF3(0f, 0f, 0.803922f);
        public static ColourF3 Blue { get; } = new ColourF3(0f, 0f, 1f);
        public static ColourF3 RoyalBlue { get; } = new ColourF3(0.254902f, 0.411765f, 0.882353f);
        public static ColourF3 SteelBlue { get; } = new ColourF3(0.27451f, 0.509804f, 0.705882f);
        public static ColourF3 DodgerBlue { get; } = new ColourF3(0.117647f, 0.564706f, 1f);
        public static ColourF3 DeepSkyBlue { get; } = new ColourF3(0f, 0.74902f, 1f);
        public static ColourF3 CornflowerBlue { get; } = new ColourF3(0.392157f, 0.584314f, 0.929412f);
        public static ColourF3 SkyBlue { get; } = new ColourF3(0.529412f, 0.807843f, 0.921569f);
        public static ColourF3 LightSkyBlue { get; } = new ColourF3(0.529412f, 0.807843f, 0.980392f);
        public static ColourF3 LightSteelBlue { get; } = new ColourF3(0.690196f, 0.768628f, 0.870588f);
        public static ColourF3 LightBlue { get; } = new ColourF3(0.678431f, 0.847059f, 0.901961f);
        public static ColourF3 PowderBlue { get; } = new ColourF3(0.690196f, 0.878431f, 0.901961f);

        // Turquoise
        public static ColourF3 Teal { get; } = new ColourF3(0f, 0.501961f, 0.501961f);
        public static ColourF3 DarkCyan { get; } = new ColourF3(0f, 0.545098f, 0.545098f);
        public static ColourF3 LightSeaGreen { get; } = new ColourF3(0.12549f, 0.698039f, 0.666667f);
        public static ColourF3 CadetBlue { get; } = new ColourF3(0.372549f, 0.619608f, 0.627451f);
        public static ColourF3 DarkTurquoise { get; } = new ColourF3(0f, 0.807843f, 0.819608f);
        public static ColourF3 MediumTurquoise { get; } = new ColourF3(0.282353f, 0.819608f, 0.8f);
        public static ColourF3 Turquoise { get; } = new ColourF3(0.25098f, 0.878431f, 0.815686f);
        public static ColourF3 Aqua { get; } = new ColourF3(0f, 1f, 1f);
        public static ColourF3 Cyan { get; } = new ColourF3(0f, 1f, 1f);
        public static ColourF3 Aquamarine { get; } = new ColourF3(0.498039f, 1f, 0.831373f);
        public static ColourF3 PaleTurquoise { get; } = new ColourF3(0.686275f, 0.933333f, 0.933333f);
        public static ColourF3 LightCyan { get; } = new ColourF3(0.878431f, 1f, 1f);

        // Green
        public static ColourF3 DarkGreen { get; } = new ColourF3(0f, 0.392157f, 0f);
        public static ColourF3 Green { get; } = new ColourF3(0f, 0.501961f, 0f);
        public static ColourF3 DarkOliveGreen { get; } = new ColourF3(0.333333f, 0.419608f, 0.184314f);
        public static ColourF3 ForestGreen { get; } = new ColourF3(0.133333f, 0.545098f, 0.133333f);
        public static ColourF3 SeaGreen { get; } = new ColourF3(0.180392f, 0.545098f, 0.341177f);
        public static ColourF3 Olive { get; } = new ColourF3(0.501961f, 0.501961f, 0f);
        public static ColourF3 OliveDrab { get; } = new ColourF3(0.419608f, 0.556863f, 0.137255f);
        public static ColourF3 MediumSeaGreen { get; } = new ColourF3(0.235294f, 0.701961f, 0.443137f);
        public static ColourF3 LimeGreen { get; } = new ColourF3(0.196078f, 0.803922f, 0.196078f);
        public static ColourF3 Lime { get; } = new ColourF3(0f, 1f, 0f);
        public static ColourF3 SpringGreen { get; } = new ColourF3(0f, 1f, 0.498039f);
        public static ColourF3 MediumSpringGreen { get; } = new ColourF3(0f, 0.980392f, 0.603922f);
        public static ColourF3 DarkSeaGreen { get; } = new ColourF3(0.560784f, 0.737255f, 0.560784f);
        public static ColourF3 MediumAquamarine { get; } = new ColourF3(0.4f, 0.803922f, 0.666667f);
        public static ColourF3 YellowGreen { get; } = new ColourF3(0.603922f, 0.803922f, 0.196078f);
        public static ColourF3 LawnGreen { get; } = new ColourF3(0.486275f, 0.988235f, 0f);
        public static ColourF3 Chartreuse { get; } = new ColourF3(0.498039f, 1f, 0f);
        public static ColourF3 LightGreen { get; } = new ColourF3(0.564706f, 0.933333f, 0.564706f);
        public static ColourF3 GreenYellow { get; } = new ColourF3(0.678431f, 1f, 0.184314f);
        public static ColourF3 PaleGreen { get; } = new ColourF3(0.596079f, 0.984314f, 0.596079f);

        // White
        public static ColourF3 MistyRose { get; } = new ColourF3(1f, 0.894118f, 0.882353f);
        public static ColourF3 AntiqueWhite { get; } = new ColourF3(0.980392f, 0.921569f, 0.843137f);
        public static ColourF3 Linen { get; } = new ColourF3(0.980392f, 0.941177f, 0.901961f);
        public static ColourF3 Beige { get; } = new ColourF3(0.960784f, 0.960784f, 0.862745f);
        public static ColourF3 WhiteSmoke { get; } = new ColourF3(0.960784f, 0.960784f, 0.960784f);
        public static ColourF3 LavenderBlush { get; } = new ColourF3(1f, 0.941177f, 0.960784f);
        public static ColourF3 OldLace { get; } = new ColourF3(0.992157f, 0.960784f, 0.901961f);
        public static ColourF3 AliceBlue { get; } = new ColourF3(0.941177f, 0.972549f, 1f);
        public static ColourF3 Seashell { get; } = new ColourF3(1f, 0.960784f, 0.933333f);
        public static ColourF3 GhostWhite { get; } = new ColourF3(0.972549f, 0.972549f, 1f);
        public static ColourF3 Honeydew { get; } = new ColourF3(0.941177f, 1f, 0.941177f);
        public static ColourF3 FloralWhite { get; } = new ColourF3(1f, 0.980392f, 0.941177f);
        public static ColourF3 Azure { get; } = new ColourF3(0.941177f, 1f, 1f);
        public static ColourF3 MintCream { get; } = new ColourF3(0.960784f, 1f, 0.980392f);
        public static ColourF3 Snow { get; } = new ColourF3(1f, 0.980392f, 0.980392f);
        public static ColourF3 Ivory { get; } = new ColourF3(1f, 1f, 0.941177f);
        public static ColourF3 White { get; } = new ColourF3(1f, 1f, 1f);

        // Black
        public static ColourF3 Black { get; } = new ColourF3(0f, 0f, 0f);
        public static ColourF3 DarkSlateGrey { get; } = new ColourF3(0.184314f, 0.309804f, 0.309804f);
        public static ColourF3 DimGrey { get; } = new ColourF3(0.411765f, 0.411765f, 0.411765f);
        public static ColourF3 SlateGrey { get; } = new ColourF3(0.439216f, 0.501961f, 0.564706f);
        public static ColourF3 DarkGrey { get; } = new ColourF3(0.501961f, 0.501961f, 0.501961f);
        public static ColourF3 LightSlateGrey { get; } = new ColourF3(0.466667f, 0.533333f, 0.6f);
        public static ColourF3 Grey { get; } = new ColourF3(0.662745f, 0.662745f, 0.662745f);
        public static ColourF3 Silver { get; } = new ColourF3(0.752941f, 0.752941f, 0.752941f);
        public static ColourF3 LightGrey { get; } = new ColourF3(0.827451f, 0.827451f, 0.827451f);
        public static ColourF3 Gainsboro { get; } = new ColourF3(0.862745f, 0.862745f, 0.862745f);
    }
}
