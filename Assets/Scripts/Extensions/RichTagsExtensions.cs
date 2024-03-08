// Summaries were copied from https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichTextSupportedTags.html

using UnityEngine;
using TMPro;
using Unity.VisualScripting;

namespace Extensions
{
    /// <summary>
    /// Extensions that allows to faster insert rich tags
    /// </summary>
    public static class RichTagsExtensions
    {
        /// <summary>
        /// Changes the text's horizontal alignment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static string Align<T>(this T textValue, TextAlignmentOptions alignment) => $"<align=\"{alignment.ToString().ToLower()}\">{textValue}</align>";

        /// <summary>
        /// Converts text to uppercase before rendering.	Functionally identical to Uppercase.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string AllCaps<T>(this T textValue) => $"<allcaps>{textValue}</allcaps>";

        /// <summary>
        /// Changes text opacity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static string Alpha<T>(this T textValue, int alpha) => $"<alpha={alpha}>{textValue}</alpha>";

        /// <summary>
        /// Renders text in boldface.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Bold<T>(this T textValue) => $"<b>{textValue}</b>";

        /// <summary>
        /// Forces a line break in text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string LineBreak<T>(this T textValue) => $"<br>{textValue}</br>";

        /// <summary>
        /// Changes text color or color and opacity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string Color<T>(this T textValue, Color color) => $"<color=#{color.ToHexString()}>{textValue}</color>";

        /// <summary>
        /// Changes text color or color and opacity. The following color names are supported: black, blue, green, orange, purple, red, white, and yellow.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string Color<T>(this T textValue, string color) => $"<color={color}>{textValue}</color>";

        /// <summary>
        /// Changes spacing between characters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Spacing<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<cspace={value}{GetUnit(unitType)}>{textValue}</cspace>";

        /// <summary>
        /// Changes text font.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static string Font<T>(this T textValue, string fontName) => $"<font=\"{fontName}>\"{textValue}</font>";

        /// <summary>
        /// Changes text font and material.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="fontName"></param>
        /// <param name="materialName"></param>
        /// <returns></returns>
        public static string Font<T>(this T textValue, string fontName, string materialName) => $"<font=\"{fontName}\" material=\"{materialName}\">{textValue}</font>";

        /// <summary>
        /// Changes the text's font weight to any of the weights defined in the font Asset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static string FontWeight<T>(this T textValue, int weight) => $"<font-weight=\"{weight}\">{textValue}</font-weight>";

        /// <summary>
        /// Applies a gradient preset to text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="gradientName"></param>
        /// <returns></returns>
        public static string Gradient<T>(this T textValue, string gradientName) => $"<gradient=\"{gradientName}\">{textValue}</gradient>";

        /// <summary>
        /// Renders text in italics.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Italic<T>(this T textValue) => $"<i>{textValue}</i>";

        /// <summary>
        /// Indents all text between the tag and the next hard line break.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Indent<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<indent={value}{GetUnit(unitType)}>{textValue}</indent>";

        /// <summary>
        /// Modifies the line height relative to the default line height specified in the font Asset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string LineHeight<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<line-height={value}{GetUnit(unitType)}>{textValue}</line-height>";

        /// <summary>
        /// Indents the first line after every hard line break.	New lines created by word-wrapping are not indented.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string LineIndent<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<line-indent={value}{GetUnit(unitType)}>{textValue}</line-indent>";

        /// <summary>
        /// Specifies a link ID for a text segment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Link<T>(this T textValue, string id) => $"<link=\"{id}\">{textValue}</link>";

        /// <summary>
        /// Converts text to lowercase before rendering.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string LowerCase<T>(this T textValue) => $"<lowercase>{textValue}</lowercase>";

        /// <summary>
        /// Gives the text horizontal margins.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Margin<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<margin={value}{GetUnit(unitType)}>{textValue}</margin>";

        /// <summary>
        /// Gives the text horizontal left margins.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string MarginLeft<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<margin-left={value}{GetUnit(unitType)}>{textValue}</margin-left>";

        /// <summary>
        /// Gives the text horizontal right margins.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string MarginRight<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<margin-right={value}{GetUnit(unitType)}>{textValue}</margin-right>";

        /// <summary>
        /// Highlights the text with a colored overlay.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string Mark<T>(this T textValue, Color color) => $"<mark={color.ToHexString()}>{textValue}</mark>";

        /// <summary>
        /// Renders the text as monospace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Monospace<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<mspace={value}{GetUnit(unitType)}>{textValue}</mspace>";

        /// <summary>
        /// Keeps a segment of text together.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string NoBreak<T>(this T textValue) => $"<nobr>{textValue}</nobr>";

        /// <summary>
        /// Prevents parsing of text that TextMesh Pro would normally interpret as rich text tags.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string NoParse<T>(this T textValue) => $"<noparse>{textValue}</noparse>";

        /// <summary>
        /// Sets the horizontal caret position on the current line.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Position<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<pos={value}{GetUnit(unitType)}>{textValue}</pos>";

        /// <summary>
        /// Rotates each character about its center.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Rotate<T>(this T textValue, int value) => $"<rotate=\"{value}\">{textValue}</rotate>";

        /// <summary>
        /// Renders a line across the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Strikethrough<T>(this T textValue) => $"<s>{textValue}</s>";

        /// <summary>
        /// Adjusts the font size for a specified portion of the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Size<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<s={value}{GetUnit(unitType)}>{textValue}</s>";

        /// <summary>
        /// Converts text to uppercase before rendering.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Smallcaps<T>(this T textValue) => $"<smallcaps>{textValue}</smallcaps>";

        /// <summary>
        /// Adds a horizontal offset between itself and the rest of the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="value"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Space<T>(this T textValue, float value, TextUnit unitType = TextUnit.Pixels) => $"<space={value}{GetUnit(unitType)}>{textValue}</space>";

        /// <summary>
        /// Applies a custom style to the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public static string Style<T>(this T textValue, string styleName) => $"<style=\"{styleName}\">{textValue}</style>";

        /// <summary>
        /// Converts the text to subscript.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Subscript<T>(this T textValue) => $"<sub>{textValue}</sub>";

        /// <summary>
        /// Converts the test to superscript.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Superscript<T>(this T textValue) => $"<sup>{textValue}</sup>";

        /// <summary>
        /// Draws a line slightly below the baseline to underline the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string Underline<T>(this T textValue) => $"<u>{textValue}</u>";

        /// <summary>
        /// Converts <paramref name="textValue"/> to uppercase before rendering. Functionally identical to AllCaps.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <returns></returns>
        public static string UpperCase<T>(this T textValue) => $"<uppercase>{textValue}</uppercase>";

        /// <summary>
        /// Gives the baseline a vertical offset
        /// Specify the <paramref name="offset"/> in pixels or font units. The offset is always relative to the original baseline.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="offset"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Offset<T>(this T textValue, float offset, TextUnit unitType = TextUnit.Pixels) => $"<voffset={offset}{GetUnit(unitType)}>{textValue}</voffset>";

        /// <summary>
        /// Changes the horizontal size of text area.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textValue">Can be anything tht can be converted to string</param>
        /// <param name="width"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static string Width<T>(this T textValue, float width, TextUnit unitType = TextUnit.Pixels) => $"<width={width}{GetUnit(unitType)}>{textValue}</width>";

        private static string GetUnit(TextUnit unitType) => unitType switch
        {
            TextUnit.FontUnits => "em",
            TextUnit.Pixels => string.Empty,
            TextUnit.Percent => "%",
            _ => string.Empty,
        };
    }

    public enum TextUnit
    {
        FontUnits,
        Pixels,
        Percent
    }
}