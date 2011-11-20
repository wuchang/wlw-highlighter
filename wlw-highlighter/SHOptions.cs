﻿using System;
﻿using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace me.lizj.wlw_highlighter
{
    public class SHOptions
    {
        public SHOptions()
        {
            this.AutoLinks = true;
            this.Gutter = true;
            this.SmartTabs = true;
            this.Toolbar = true;
            this.WrapLines = true;
        }
        [Description("Allows you to turn detection of links in the highlighted element on and off. If the option is turned off, URLs won't be clickable.")]
        [DisplayName("brush")]
        [Browsable(false)]
        public string Brush { get; set; }

        [Description("Allows you to turn detection of links in the highlighted element on and off. If the option is turned off, URLs won't be clickable.")]
        [DisplayName("auto-links")]
        public bool AutoLinks { get; set; }

        [Description("Allows you to add a custom class (or multiple classes) to every highlighter element that will be created on the page. ")]
        [DisplayName("class-name")]
        public string ClassName { get; set; }

        [Description("Allows you to force highlighted elements on the page to be collapsed by default.")]
        [DisplayName("collapse")]
        public bool Collapse { get; set; }

        [Description("Allows you to change the first (starting) line number. ")]
        [DisplayName("first-line")]
        public string FirstLine { get; set; }

        [Description("Allows you to turn gutter with line numbers on and off.")]
        [DisplayName("gutter")]
        public bool Gutter { get; set; }

        [Description("Allows you to highlight one or more lines to focus user's attention. When specifying as a parameter, you have to pass an array looking value, like [1, 2, 3] or just an number for a single line. If you are changing SyntaxHighlighter.defaults['highlight'], you can pass a number or an array of numbers. ")]
        [DisplayName("highlight")]
        public bool Highlight { get; set; }

        [Description("Allows you to highlight a mixture of HTML/XML code and a script which is very common in web development. Setting this value to true requires that you have shBrushXml.js loaded and that the brush you are using supports this feature. ")]
        [DisplayName("html-script")]
        public bool HtmlScript { get; set; }


        [Description("Allows you to disable toolbar and gutter with a single property.  ")]
        [DisplayName("light")]
        public bool? Light { get; set; }

        [Description("llows you to turn smart tabs feature on and off. ")]
        [DisplayName("smart-tabs")]
        public bool? SmartTabs { get; set; }

        [Description("Allows you to adjust tab size.")]
        [DisplayName("tab-size")]
        public int? TabSize { get; set; }

        [Description("Toggles toolbar on/off.")]
        [DisplayName("toolbar")]
        public bool? Toolbar { get; set; }

        [Description("Allows you to turn line wrapping feature on and off. ")]
        [DisplayName("wrap-lines")]
        public bool? WrapLines { get; set; }

        public override string ToString()
        {
            var q = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .Select(o => new
                 {
                     name = (o.GetCustomAttributes(typeof(DisplayNameAttribute), false).First() as DisplayNameAttribute).DisplayName,
                     value = o.GetValue(this, null)
                 })
                 .Where(o => o.value != null)
                 .Select(o => string.Format("{0}:{1}", o.name, o.value))
                  ;
            return string.Join(";", q.ToArray()).ToLower();
        }

        public static SHOptions Parse(string src)
        {
            SHOptions obj = new SHOptions();
            var type = typeof(SHOptions);
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Select(o => new
                {
                    Property = o,
                    DisplayNameAttr = o.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute
                })
                .Where(o => o.DisplayNameAttr != null)
                .Select(o => new { Name = o.DisplayNameAttr.DisplayName, o.Property })
                .ToList()
                 ;

            var q = src.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries))
                .Where(o => o.Length == 2)
                .Select(o => new { Key = o[0], Value = o[1] })
                .ToList()
                ;
            ;

            foreach (var item in q)
            {
                try
                {
                    var p = properties.FirstOrDefault(o => o.Name.Equals(item.Key.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (p != null)
                    {
                        var propertyType = p.Property.PropertyType;
                        var converter = TypeDescriptor.GetConverter(propertyType);
                        var value = converter.ConvertFromString(item.Value.Trim());

                        p.Property.SetValue(obj, value, null);
                    }
                }
                catch (Exception)
                {
                }
            }

            return obj;
        }

    }
}
