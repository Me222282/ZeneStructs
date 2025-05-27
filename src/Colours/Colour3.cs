using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGB colour value as bytes.
    /// </summary>
    public struct Colour3
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public Colour3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// The red component of the colour.
        /// </summary>
        public byte R { get; set; }
        /// <summary>
        /// The green component of the colour.
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// The blue component of the colour.
        /// </summary>
        public byte B { get; set; }
        
        /// <summary>
        /// Inverts just the colour components.
        /// </summary>
        public void Invert()
        {
            R = (byte)(255 - R);
            G = (byte)(255 - G);
            B = (byte)(255 - B);
        }
        /// <summary>
        /// Returns a <see cref="Colour"/> with the colour components inverted.
        /// </summary>
        public Colour3 Inverted()
        {
            return new Colour3(
                (byte)(255 - R),
                (byte)(255 - G),
                (byte)(255 - B)
            );
        }
        
        /// <summary>
        /// Returns this colour stored as HSL values.
        /// </summary>
        public Vector3 ToHsl() => ColourF3.ToHsl(R / 255f, G / 255f, B / 255f);
        /// <summary>
        /// Creates a colour from HLS values.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        public static Colour3 FromHsl(floatv h, floatv s, floatv l)
            => (Colour3)ColourF3.FromHsl(h, s, l);
        /// <summary>
        /// Creates a colour from a wavelength of light.
        /// </summary>
        /// <remarks>
        /// Sourced from https://stackoverflow.com/questions/3407942/rgb-values-of-visible-spectrum/22681410#22681410.
        /// </remarks>
        /// <param name="l">The wavelength in nm. 400 - 700</param>
        public static Colour3 FromWavelength(float l)
            => (Colour3)ColourF3.FromWavelength(l);

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
            return (obj is Colour3 c && this == c) ||
                (obj is ColourF3 f && this == f);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public static bool operator ==(Colour3 l, Colour3 r)
        {
            return l.R == r.R &&
                l.G == r.G &&
                l.B == r.B;
        }
        public static bool operator !=(Colour3 l, Colour3 r) => !(l == r);

        public static bool operator ==(Colour3 l, ColourF3 r)
        {
            return (l.R * 255f) == r.R &&
                (l.G * 255f) == r.G &&
                (l.B * 255f) == r.B;
        }
        public static bool operator !=(Colour3 l, ColourF3 r) => !(l == r);

        public static implicit operator ColourF3(Colour3 c)
        {
            return new ColourF3(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }

        public static explicit operator Colour(Colour3 c)
        {
            return new Colour(c.R, c.G, c.B);
        }
        public static explicit operator ColourF(Colour3 c)
        {
            return new ColourF(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3<byte>(Colour3 c)
        {
            return new Vector3<byte>(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(Vector3<byte> v)
        {
            return new Colour3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3I(Colour3 c)
        {
            return new Vector3I(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(Vector3I v)
        {
            return new Colour3((byte)v.X, (byte)v.Y, (byte)v.Z);
        }

        public static explicit operator Vector3(Colour3 c)
        {
            ColourF3 cf = c;

            return new Vector3(cf.R, cf.G, cf.B);
        }
        public static explicit operator Colour3(Vector3 v)
        {
            return new Colour3((byte)(v.X * 255), (byte)(v.Y * 255), (byte)(v.Z * 255));
        }

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static Colour3 Zero { get; } = new Colour3(0, 0, 0);

        // Pink
        public static Colour3 MediumVioletRed { get; } = new Colour3(199, 21, 133);
        public static Colour3 DeepPink { get; } = new Colour3(255, 20, 147);
        public static Colour3 PaleVioletRed { get; } = new Colour3(219, 112, 147);
        public static Colour3 HotPink { get; } = new Colour3(255, 105, 180);
        public static Colour3 LightPink { get; } = new Colour3(255, 182, 193);
        public static Colour3 Pink { get; } = new Colour3(255, 192, 203);

        // Red
        public static Colour3 DarkRed { get; } = new Colour3(139, 0, 0);
        public static Colour3 Red { get; } = new Colour3(255, 0, 0);
        public static Colour3 Firebrick { get; } = new Colour3(178, 34, 34);
        public static Colour3 Crimson { get; } = new Colour3(220, 20, 60);
        public static Colour3 IndianRed { get; } = new Colour3(205, 92, 92);
        public static Colour3 LightCoral { get; } = new Colour3(240, 128, 128);
        public static Colour3 Salmon { get; } = new Colour3(250, 128, 114);
        public static Colour3 DarkSalmon { get; } = new Colour3(233, 150, 122);
        public static Colour3 LightSalmon { get; } = new Colour3(255, 160, 122);

        // Orange
        public static Colour3 OrangeRed { get; } = new Colour3(255, 69, 0);
        public static Colour3 Tomato { get; } = new Colour3(255, 99, 71);
        public static Colour3 DarkOrange { get; } = new Colour3(255, 140, 0);
        public static Colour3 Coral { get; } = new Colour3(255, 127, 80);
        public static Colour3 Orange { get; } = new Colour3(255, 165, 0);

        // Yellow
        public static Colour3 DarkKhaki { get; } = new Colour3(189, 183, 107);
        public static Colour3 Gold { get; } = new Colour3(255, 215, 0);
        public static Colour3 Khaki { get; } = new Colour3(240, 230, 140);
        public static Colour3 PeachPuff { get; } = new Colour3(255, 218, 185);
        public static Colour3 Yellow { get; } = new Colour3(255, 255, 0);
        public static Colour3 PaleGoldenrod { get; } = new Colour3(238, 232, 170);
        public static Colour3 Moccasin { get; } = new Colour3(255, 228, 181);
        public static Colour3 PapayaWhip { get; } = new Colour3(255, 239, 213);
        public static Colour3 LightGoldenrodYellow { get; } = new Colour3(250, 250, 210);
        public static Colour3 LemonChiffon { get; } = new Colour3(255, 250, 205);
        public static Colour3 LightYellow { get; } = new Colour3(255, 255, 224);

        // Brown
        public static Colour3 Maroon { get; } = new Colour3(128, 0, 0);
        public static Colour3 Brown { get; } = new Colour3(165, 42, 42);
        public static Colour3 SaddleBrown { get; } = new Colour3(139, 69, 19);
        public static Colour3 Sienna { get; } = new Colour3(160, 82, 45);
        public static Colour3 Chocolate { get; } = new Colour3(210, 105, 30);
        public static Colour3 DarkGoldenrod { get; } = new Colour3(184, 134, 11);
        public static Colour3 Peru { get; } = new Colour3(205, 133, 63);
        public static Colour3 RosyBrown { get; } = new Colour3(188, 143, 143);
        public static Colour3 Goldenrod { get; } = new Colour3(218, 165, 32);
        public static Colour3 SandyBrown { get; } = new Colour3(244, 164, 96);
        public static Colour3 Tan { get; } = new Colour3(210, 180, 140);
        public static Colour3 Burlywood { get; } = new Colour3(222, 184, 135);
        public static Colour3 Wheat { get; } = new Colour3(245, 222, 179);
        public static Colour3 NavajoWhite { get; } = new Colour3(255, 222, 173);
        public static Colour3 Bisque { get; } = new Colour3(255, 228, 196);
        public static Colour3 BlanchedAlmond { get; } = new Colour3(255, 235, 205);
        public static Colour3 Cornsilk { get; } = new Colour3(255, 248, 220);

        // Purple
        public static Colour3 Indigo { get; } = new Colour3(75, 0, 130);
        public static Colour3 Purple { get; } = new Colour3(128, 0, 128);
        public static Colour3 DarkMagenta { get; } = new Colour3(139, 0, 139);
        public static Colour3 DarkViolet { get; } = new Colour3(148, 0, 211);
        public static Colour3 DarkSlateBlue { get; } = new Colour3(72, 61, 139);
        public static Colour3 BlueViolet { get; } = new Colour3(138, 43, 226);
        public static Colour3 DarkOrchid { get; } = new Colour3(153, 50, 204);
        public static Colour3 Fuchsia { get; } = new Colour3(255, 0, 255);
        public static Colour3 Magenta { get; } = new Colour3(255, 0, 255);
        public static Colour3 SlateBlue { get; } = new Colour3(106, 90, 205);
        public static Colour3 MediumSlateBlue { get; } = new Colour3(123, 104, 238);
        public static Colour3 MediumOrchid { get; } = new Colour3(186, 85, 211);
        public static Colour3 MediumPurple { get; } = new Colour3(147, 112, 219);
        public static Colour3 Orchid { get; } = new Colour3(218, 112, 214);
        public static Colour3 Violet { get; } = new Colour3(238, 130, 238);
        public static Colour3 Plum { get; } = new Colour3(221, 160, 221);
        public static Colour3 Thistle { get; } = new Colour3(216, 191, 216);
        public static Colour3 Lavender { get; } = new Colour3(230, 230, 250);

        // Blue
        public static Colour3 MidnightBlue { get; } = new Colour3(25, 25, 112);
        public static Colour3 Navy { get; } = new Colour3(0, 0, 128);
        public static Colour3 DarkBlue { get; } = new Colour3(0, 0, 139);
        public static Colour3 MediumBlue { get; } = new Colour3(0, 0, 205);
        public static Colour3 Blue { get; } = new Colour3(0, 0, 255);
        public static Colour3 RoyalBlue { get; } = new Colour3(65, 105, 225);
        public static Colour3 SteelBlue { get; } = new Colour3(70, 130, 180);
        public static Colour3 DodgerBlue { get; } = new Colour3(30, 144, 255);
        public static Colour3 DeepSkyBlue { get; } = new Colour3(0, 191, 255);
        public static Colour3 CornflowerBlue { get; } = new Colour3(100, 149, 237);
        public static Colour3 SkyBlue { get; } = new Colour3(135, 206, 235);
        public static Colour3 LightSkyBlue { get; } = new Colour3(135, 206, 250);
        public static Colour3 LightSteelBlue { get; } = new Colour3(176, 196, 222);
        public static Colour3 LightBlue { get; } = new Colour3(173, 216, 230);
        public static Colour3 PowderBlue { get; } = new Colour3(176, 224, 230);

        // Turquoise
        public static Colour3 Teal { get; } = new Colour3(0, 128, 128);
        public static Colour3 DarkCyan { get; } = new Colour3(0, 139, 139);
        public static Colour3 LightSeaGreen { get; } = new Colour3(32, 178, 170);
        public static Colour3 CadetBlue { get; } = new Colour3(95, 158, 160);
        public static Colour3 DarkTurquoise { get; } = new Colour3(0, 206, 209);
        public static Colour3 MediumTurquoise { get; } = new Colour3(72, 209, 204);
        public static Colour3 Turquoise { get; } = new Colour3(64, 224, 208);
        public static Colour3 Aqua { get; } = new Colour3(0, 255, 255);
        public static Colour3 Cyan { get; } = new Colour3(0, 255, 255);
        public static Colour3 Aquamarine { get; } = new Colour3(127, 255, 212);
        public static Colour3 PaleTurquoise { get; } = new Colour3(175, 238, 238);
        public static Colour3 LightCyan { get; } = new Colour3(224, 255, 255);

        // Green
        public static Colour3 DarkGreen { get; } = new Colour3(0, 100, 0);
        public static Colour3 Green { get; } = new Colour3(0, 128, 0);
        public static Colour3 DarkOliveGreen { get; } = new Colour3(85, 107, 47);
        public static Colour3 ForestGreen { get; } = new Colour3(34, 139, 34);
        public static Colour3 SeaGreen { get; } = new Colour3(46, 139, 87);
        public static Colour3 Olive { get; } = new Colour3(128, 128, 0);
        public static Colour3 OliveDrab { get; } = new Colour3(107, 142, 35);
        public static Colour3 MediumSeaGreen { get; } = new Colour3(60, 179, 113);
        public static Colour3 LimeGreen { get; } = new Colour3(50, 205, 50);
        public static Colour3 Lime { get; } = new Colour3(0, 255, 0);
        public static Colour3 SpringGreen { get; } = new Colour3(0, 255, 127);
        public static Colour3 MediumSpringGreen { get; } = new Colour3(0, 250, 154);
        public static Colour3 DarkSeaGreen { get; } = new Colour3(143, 188, 143);
        public static Colour3 MediumAquamarine { get; } = new Colour3(102, 205, 170);
        public static Colour3 YellowGreen { get; } = new Colour3(154, 205, 50);
        public static Colour3 LawnGreen { get; } = new Colour3(124, 252, 0);
        public static Colour3 Chartreuse { get; } = new Colour3(127, 255, 0);
        public static Colour3 LightGreen { get; } = new Colour3(144, 238, 144);
        public static Colour3 GreenYellow { get; } = new Colour3(173, 255, 47);
        public static Colour3 PaleGreen { get; } = new Colour3(152, 251, 152);

        // White
        public static Colour3 MistyRose { get; } = new Colour3(255, 228, 225);
        public static Colour3 AntiqueWhite { get; } = new Colour3(250, 235, 215);
        public static Colour3 Linen { get; } = new Colour3(250, 240, 230);
        public static Colour3 Beige { get; } = new Colour3(245, 245, 220);
        public static Colour3 WhiteSmoke { get; } = new Colour3(245, 245, 245);
        public static Colour3 LavenderBlush { get; } = new Colour3(255, 240, 245);
        public static Colour3 OldLace { get; } = new Colour3(253, 245, 230);
        public static Colour3 AliceBlue { get; } = new Colour3(240, 248, 255);
        public static Colour3 Seashell { get; } = new Colour3(255, 245, 238);
        public static Colour3 GhostWhite { get; } = new Colour3(248, 248, 255);
        public static Colour3 Honeydew { get; } = new Colour3(240, 255, 240);
        public static Colour3 FloralWhite { get; } = new Colour3(255, 250, 240);
        public static Colour3 Azure { get; } = new Colour3(240, 255, 255);
        public static Colour3 MintCream { get; } = new Colour3(245, 255, 250);
        public static Colour3 Snow { get; } = new Colour3(255, 250, 250);
        public static Colour3 Ivory { get; } = new Colour3(255, 255, 240);
        public static Colour3 White { get; } = new Colour3(255, 255, 255);

        // Black
        public static Colour3 Black { get; } = new Colour3(0, 0, 0);
        public static Colour3 DarkSlateGrey { get; } = new Colour3(47, 79, 79);
        public static Colour3 DimGrey { get; } = new Colour3(105, 105, 105);
        public static Colour3 SlateGrey { get; } = new Colour3(112, 128, 144);
        public static Colour3 DarkGrey { get; } = new Colour3(128, 128, 128);
        public static Colour3 LightSlateGrey { get; } = new Colour3(119, 136, 153);
        public static Colour3 Grey { get; } = new Colour3(169, 169, 169);
        public static Colour3 Silver { get; } = new Colour3(192, 192, 192);
        public static Colour3 LightGrey { get; } = new Colour3(211, 211, 211);
        public static Colour3 Gainsboro { get; } = new Colour3(220, 220, 220);
    }
}
