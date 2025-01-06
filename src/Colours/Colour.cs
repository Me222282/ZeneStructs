using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGBA colour value as bytes.
    /// </summary>
    public struct Colour
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 255.
        /// </remarks>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public Colour(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        /// <summary>
        /// Creates a colour from RGBA values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        /// <param name="a">The alpha component of the colour.</param>
        public Colour(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Create a colour from an already defined colour with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public Colour(Colour3 colour, byte alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour stored as floats with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as floats.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public Colour(ColourF3 colour, byte alpha)
        {
            R = (byte)(colour.R * 255);
            G = (byte)(colour.G * 255);
            B = (byte)(colour.B * 255);
            A = alpha;
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
        /// The alpha component of the colour.
        /// </summary>
        public byte A { get; set; }
        
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
        /// Inverts just the alpha component.
        /// </summary>
        public void InvertA() => A = (byte)(255 - A);
        /// <summary>
        /// Returns a <see cref="Colour"/> with the colour components inverted.
        /// </summary>
        public Colour Inverted()
        {
            return new Colour(
                (byte)(255 - R),
                (byte)(255 - G),
                (byte)(255 - B)
            );
        }
        /// <summary>
        /// Returns a <see cref="Colour"/> with the alpha component inverted.
        /// </summary>
        public Colour InvertedA()
        {
            return new Colour(
                R, G, B,
                (byte)(255 - A)
            );
        }
        
        /// <summary>
        /// Returns this colour stored as HSL values.
        /// </summary>
        public Vector3 ToHsl() => ColourF3.ToHsl(R / 255f, G / 255f, B / 255f);
        /// <summary>
        /// Creates a colour from HLS values and opacity.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        /// <param name="a">THe alpha component of the colour.</param>
        public static Colour FromHsl(floatv h, floatv s, floatv l, byte a = 255)
            => new Colour(ColourF3.FromHsl(h, s, l), a);
        /// <summary>
        /// Creates a colour from a wavelength of light.
        /// </summary>
        /// <remarks>
        /// Sourced from https://stackoverflow.com/questions/3407942/rgb-values-of-visible-spectrum/22681410#22681410.
        /// </remarks>
        /// <param name="l">The wavelength in nm. 400 - 700</param>
        public static Colour FromWavelength(float l, byte a = 255)
            => new Colour(ColourF3.FromWavelength(l), a);

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
            return (obj is Colour c &&
                R == c.R &&
                G == c.G &&
                B == c.B &&
                A == c.A) ||
                (obj is ColourF f &&
                (R * 255f) == f.R &&
                (G * 255f) == f.G &&
                (B * 255f) == f.B &&
                (A * 255f) == f.A);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public static bool operator ==(Colour l, Colour r) => l.Equals(r);
        public static bool operator !=(Colour l, Colour r) => !l.Equals(r);

        public static bool operator ==(Colour l, ColourF r) => l.Equals(r);
        public static bool operator !=(Colour l, ColourF r) => !l.Equals(r);

        public static implicit operator ColourF(Colour c)
        {
            return new ColourF(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat,
                c.A * ColourF.ByteToFloat);
        }

        public static explicit operator ColourF3(Colour c)
        {
            return new ColourF3(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }
        public static explicit operator Colour3(Colour c)
        {
            return new Colour3(c.R, c.G, c.B);
        }

        public static explicit operator Vector4<byte>(Colour c)
        {
            return new Vector4<byte>(c.R, c.G, c.B, c.A);
        }
        public static explicit operator Colour(Vector4<byte> v)
        {
            return new Colour(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4I(Colour c)
        {
            return new Vector4I(c.R, c.G, c.B, c.A);
        }
        public static explicit operator Colour(Vector4I v)
        {
            return new Colour((byte)v.X, (byte)v.Y, (byte)v.Z, (byte)v.W);
        }

        public static explicit operator Vector4(Colour c)
        {
            ColourF cf = c;

            return new Vector4(cf.R, cf.G, cf.B, cf.A);
        }
        public static explicit operator Colour(Vector4 v)
        {
            return new Colour((byte)(v.X * 255), (byte)(v.Y * 255), (byte)(v.Z * 255), (byte)(v.W * 255));
        }

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static Colour Zero { get; } = new Colour(0, 0, 0, 0);

        // Pink
        public static Colour MediumVioletRed { get; } = new Colour(199, 21, 133);
        public static Colour DeepPink { get; } = new Colour(255, 20, 147);
        public static Colour PaleVioletRed { get; } = new Colour(219, 112, 147);
        public static Colour HotPink { get; } = new Colour(255, 105, 180);
        public static Colour LightPink { get; } = new Colour(255, 182, 193);
        public static Colour Pink { get; } = new Colour(255, 192, 203);

        // Red
        public static Colour DarkRed { get; } = new Colour(139, 0, 0);
        public static Colour Red { get; } = new Colour(255, 0, 0);
        public static Colour Firebrick { get; } = new Colour(178, 34, 34);
        public static Colour Crimson { get; } = new Colour(220, 20, 60);
        public static Colour IndianRed { get; } = new Colour(205, 92, 92);
        public static Colour LightCoral { get; } = new Colour(240, 128, 128);
        public static Colour Salmon { get; } = new Colour(250, 128, 114);
        public static Colour DarkSalmon { get; } = new Colour(233, 150, 122);
        public static Colour LightSalmon { get; } = new Colour(255, 160, 122);

        // Orange
        public static Colour OrangeRed { get; } = new Colour(255, 69, 0);
        public static Colour Tomato { get; } = new Colour(255, 99, 71);
        public static Colour DarkOrange { get; } = new Colour(255, 140, 0);
        public static Colour Coral { get; } = new Colour(255, 127, 80);
        public static Colour Orange { get; } = new Colour(255, 165, 0);

        // Yellow
        public static Colour DarkKhaki { get; } = new Colour(189, 183, 107);
        public static Colour Gold { get; } = new Colour(255, 215, 0);
        public static Colour Khaki { get; } = new Colour(240, 230, 140);
        public static Colour PeachPuff { get; } = new Colour(255, 218, 185);
        public static Colour Yellow { get; } = new Colour(255, 255, 0);
        public static Colour PaleGoldenrod { get; } = new Colour(238, 232, 170);
        public static Colour Moccasin { get; } = new Colour(255, 228, 181);
        public static Colour PapayaWhip { get; } = new Colour(255, 239, 213);
        public static Colour LightGoldenrodYellow { get; } = new Colour(250, 250, 210);
        public static Colour LemonChiffon { get; } = new Colour(255, 250, 205);
        public static Colour LightYellow { get; } = new Colour(255, 255, 224);

        // Brown
        public static Colour Maroon { get; } = new Colour(128, 0, 0);
        public static Colour Brown { get; } = new Colour(165, 42, 42);
        public static Colour SaddleBrown { get; } = new Colour(139, 69, 19);
        public static Colour Sienna { get; } = new Colour(160, 82, 45);
        public static Colour Chocolate { get; } = new Colour(210, 105, 30);
        public static Colour DarkGoldenrod { get; } = new Colour(184, 134, 11);
        public static Colour Peru { get; } = new Colour(205, 133, 63);
        public static Colour RosyBrown { get; } = new Colour(188, 143, 143);
        public static Colour Goldenrod { get; } = new Colour(218, 165, 32);
        public static Colour SandyBrown { get; } = new Colour(244, 164, 96);
        public static Colour Tan { get; } = new Colour(210, 180, 140);
        public static Colour Burlywood { get; } = new Colour(222, 184, 135);
        public static Colour Wheat { get; } = new Colour(245, 222, 179);
        public static Colour NavajoWhite { get; } = new Colour(255, 222, 173);
        public static Colour Bisque { get; } = new Colour(255, 228, 196);
        public static Colour BlanchedAlmond { get; } = new Colour(255, 235, 205);
        public static Colour Cornsilk { get; } = new Colour(255, 248, 220);

        // Purple
        public static Colour Indigo { get; } = new Colour(75, 0, 130);
        public static Colour Purple { get; } = new Colour(128, 0, 128);
        public static Colour DarkMagenta { get; } = new Colour(139, 0, 139);
        public static Colour DarkViolet { get; } = new Colour(148, 0, 211);
        public static Colour DarkSlateBlue { get; } = new Colour(72, 61, 139);
        public static Colour BlueViolet { get; } = new Colour(138, 43, 226);
        public static Colour DarkOrchid { get; } = new Colour(153, 50, 204);
        public static Colour Fuchsia { get; } = new Colour(255, 0, 255);
        public static Colour Magenta { get; } = new Colour(255, 0, 255);
        public static Colour SlateBlue { get; } = new Colour(106, 90, 205);
        public static Colour MediumSlateBlue { get; } = new Colour(123, 104, 238);
        public static Colour MediumOrchid { get; } = new Colour(186, 85, 211);
        public static Colour MediumPurple { get; } = new Colour(147, 112, 219);
        public static Colour Orchid { get; } = new Colour(218, 112, 214);
        public static Colour Violet { get; } = new Colour(238, 130, 238);
        public static Colour Plum { get; } = new Colour(221, 160, 221);
        public static Colour Thistle { get; } = new Colour(216, 191, 216);
        public static Colour Lavender { get; } = new Colour(230, 230, 250);

        // Blue
        public static Colour MidnightBlue { get; } = new Colour(25, 25, 112);
        public static Colour Navy { get; } = new Colour(0, 0, 128);
        public static Colour DarkBlue { get; } = new Colour(0, 0, 139);
        public static Colour MediumBlue { get; } = new Colour(0, 0, 205);
        public static Colour Blue { get; } = new Colour(0, 0, 255);
        public static Colour RoyalBlue { get; } = new Colour(65, 105, 225);
        public static Colour SteelBlue { get; } = new Colour(70, 130, 180);
        public static Colour DodgerBlue { get; } = new Colour(30, 144, 255);
        public static Colour DeepSkyBlue { get; } = new Colour(0, 191, 255);
        public static Colour CornflowerBlue { get; } = new Colour(100, 149, 237);
        public static Colour SkyBlue { get; } = new Colour(135, 206, 235);
        public static Colour LightSkyBlue { get; } = new Colour(135, 206, 250);
        public static Colour LightSteelBlue { get; } = new Colour(176, 196, 222);
        public static Colour LightBlue { get; } = new Colour(173, 216, 230);
        public static Colour PowderBlue { get; } = new Colour(176, 224, 230);

        // Turquoise
        public static Colour Teal { get; } = new Colour(0, 128, 128);
        public static Colour DarkCyan { get; } = new Colour(0, 139, 139);
        public static Colour LightSeaGreen { get; } = new Colour(32, 178, 170);
        public static Colour CadetBlue { get; } = new Colour(95, 158, 160);
        public static Colour DarkTurquoise { get; } = new Colour(0, 206, 209);
        public static Colour MediumTurquoise { get; } = new Colour(72, 209, 204);
        public static Colour Turquoise { get; } = new Colour(64, 224, 208);
        public static Colour Aqua { get; } = new Colour(0, 255, 255);
        public static Colour Cyan { get; } = new Colour(0, 255, 255);
        public static Colour Aquamarine { get; } = new Colour(127, 255, 212);
        public static Colour PaleTurquoise { get; } = new Colour(175, 238, 238);
        public static Colour LightCyan { get; } = new Colour(224, 255, 255);

        // Green
        public static Colour DarkGreen { get; } = new Colour(0, 100, 0);
        public static Colour Green { get; } = new Colour(0, 128, 0);
        public static Colour DarkOliveGreen { get; } = new Colour(85, 107, 47);
        public static Colour ForestGreen { get; } = new Colour(34, 139, 34);
        public static Colour SeaGreen { get; } = new Colour(46, 139, 87);
        public static Colour Olive { get; } = new Colour(128, 128, 0);
        public static Colour OliveDrab { get; } = new Colour(107, 142, 35);
        public static Colour MediumSeaGreen { get; } = new Colour(60, 179, 113);
        public static Colour LimeGreen { get; } = new Colour(50, 205, 50);
        public static Colour Lime { get; } = new Colour(0, 255, 0);
        public static Colour SpringGreen { get; } = new Colour(0, 255, 127);
        public static Colour MediumSpringGreen { get; } = new Colour(0, 250, 154);
        public static Colour DarkSeaGreen { get; } = new Colour(143, 188, 143);
        public static Colour MediumAquamarine { get; } = new Colour(102, 205, 170);
        public static Colour YellowGreen { get; } = new Colour(154, 205, 50);
        public static Colour LawnGreen { get; } = new Colour(124, 252, 0);
        public static Colour Chartreuse { get; } = new Colour(127, 255, 0);
        public static Colour LightGreen { get; } = new Colour(144, 238, 144);
        public static Colour GreenYellow { get; } = new Colour(173, 255, 47);
        public static Colour PaleGreen { get; } = new Colour(152, 251, 152);

        // White
        public static Colour MistyRose { get; } = new Colour(255, 228, 225);
        public static Colour AntiqueWhite { get; } = new Colour(250, 235, 215);
        public static Colour Linen { get; } = new Colour(250, 240, 230);
        public static Colour Beige { get; } = new Colour(245, 245, 220);
        public static Colour WhiteSmoke { get; } = new Colour(245, 245, 245);
        public static Colour LavenderBlush { get; } = new Colour(255, 240, 245);
        public static Colour OldLace { get; } = new Colour(253, 245, 230);
        public static Colour AliceBlue { get; } = new Colour(240, 248, 255);
        public static Colour Seashell { get; } = new Colour(255, 245, 238);
        public static Colour GhostWhite { get; } = new Colour(248, 248, 255);
        public static Colour Honeydew { get; } = new Colour(240, 255, 240);
        public static Colour FloralWhite { get; } = new Colour(255, 250, 240);
        public static Colour Azure { get; } = new Colour(240, 255, 255);
        public static Colour MintCream { get; } = new Colour(245, 255, 250);
        public static Colour Snow { get; } = new Colour(255, 250, 250);
        public static Colour Ivory { get; } = new Colour(255, 255, 240);
        public static Colour White { get; } = new Colour(255, 255, 255);

        // Black
        public static Colour Black { get; } = new Colour(0, 0, 0);
        public static Colour DarkSlateGrey { get; } = new Colour(47, 79, 79);
        public static Colour DimGrey { get; } = new Colour(105, 105, 105);
        public static Colour SlateGrey { get; } = new Colour(112, 128, 144);
        public static Colour DarkGrey { get; } = new Colour(128, 128, 128);
        public static Colour LightSlateGrey { get; } = new Colour(119, 136, 153);
        public static Colour Grey { get; } = new Colour(169, 169, 169);
        public static Colour Silver { get; } = new Colour(192, 192, 192);
        public static Colour LightGrey { get; } = new Colour(211, 211, 211);
        public static Colour Gainsboro { get; } = new Colour(220, 220, 220);
    }
}
