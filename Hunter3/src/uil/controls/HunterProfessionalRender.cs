using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hunter3
{
    public class HunterProfessionalColors : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin
        { 
            get 
            { 
                return Color.FromArgb(39, 40, 34); 
            } 
        }

        public override Color ToolStripGradientMiddle
        {
            get
            {
                return Color.FromArgb(39, 40, 34);
            }
        }

        public override Color ToolStripGradientEnd
        {
            get
            {
                return Color.FromArgb(39, 40, 34);
            }
        }

        public override Color ToolStripBorder
        {
            get
            {
                return Color.FromArgb(39, 40, 34);
            }
        }

        public override Color MenuStripGradientBegin
        { 
            get 
            { 
                return Color.FromArgb(39, 40, 34); 
            } 
        }

        public override Color MenuStripGradientEnd
        { 
            get 
            { 
                return Color.FromArgb(39,40,34); 
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color SeparatorDark

        {
            get
            {
                return Color.FromArgb(51, 51, 55);
            }
        }

        public override Color SeparatorLight
        {
            get
            {
                return Color.FromArgb(51, 51, 55);
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return Color.FromArgb(62, 62, 64);
            }
        }

    }

    public class HunterToolStripRender : ToolStripProfessionalRenderer
    {
        ToolStrip StripOwner;
        public HunterToolStripRender(ProfessionalColorTable table, ToolStrip owner) : base(table) { StripOwner = owner; }
        private string CheckIcon = @"AAABAAEAEBAAAAEAIABoBAAAFgAAACgAAAAQAAAAIAAAAAEAIAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAxejYKLXUyBgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA5hUAKN4M9/zF7N/sudjMGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABCkkkKQI5H/1SjXP9Pn1f/Mnw4/y53NAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABLnlMKSZpR/1usZP93yoL/dMh+/1GgWf8zfTn/L3g1CAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABTqVwKUaZa/2O1bf9+zon/e8yH/3bKgf92yYH/UqJa/zR+Ov8weTUIAAAAAAAAAAAAAAAAAAAAAAAAAABatGUIWbBj/2u9dv+E0pD/esmF/2Cyav9jtG3/eMmD/3jLgv9To1z/NH86/TF6NggAAAAAAAAAAAAAAAAAAAAAXrlpRlu1ZuV5yYb/gM6N/1GmWv1NoVZuSZxRi1ytZ/98zIb/ecuF/1SkXf81gDv9MXs3CAAAAAAAAAAAAAAAAAAAAABfumo8XLZm523Aef9VrF9uAAAAAAAAAABKnVKRXq5o/33Nif98zYf/VqVf/zaBPP0yfDgIAAAAAAAAAAAAAAAAAAAAAF+7akJct2dkAAAAAAAAAAAAAAAAAAAAAEueU5Ffr2n/f86K/37Oif9XpmD/N4I9/TN9OQgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAS59UkWCwav+Bz43/f8+L/1inYf85hUD/NH46CAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABMoFWRYrJs/4LRj/96yIX/V6Zg/ziEP3oAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE2hVpFjs23/X69p/0GRSXgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAATqJXkUqdUn4AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA//8AAP//AAD5/wAA8P8AAOB/AADAPwAAgB8AAIQPAADOBwAA/wMAAP+BAAD/wQAA/+MAAP/3AAD//wAA//8AAA==";

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            for (int i = 0; i < e.ToolStrip.Items.Count; i++)
            {
                ToolStripMenuItem _t = e.ToolStrip.Items[i] as ToolStripMenuItem;
                if (_t != null)
                    _t.ForeColor = Color.White;
            }
            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            g.DrawImage(DeserializeFromBase64(CheckIcon), bounds);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Item.Selected && !e.Item.Pressed)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
                using (Brush b = new SolidBrush(Color.FromArgb(62, 62, 64)))
                {
                    g.FillRectangle(b, bounds);
                }
            }
            else if (e.Item.Pressed)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
                using (Brush b = new SolidBrush(ColorTable.ToolStripDropDownBackground))
                {
                    g.FillRectangle(b, bounds);
                    if (StripOwner.Items.Contains(e.Item))
                    {
                        using (Pen p = new Pen(ColorTable.MenuBorder, 2))
                        {
                            g.DrawLine(p, Point.Empty, new Point(0, bounds.Height));
                            g.DrawLine(p, Point.Empty, new Point(bounds.Width, 0));
                            g.DrawLine(p, new Point(bounds.Width, 0), new Point(bounds.Width, bounds.Height));
                        }
                    }
                }

            }
        }

        private static Bitmap DeserializeFromBase64(string data)
        {
            MemoryStream stream =
                new MemoryStream(Convert.FromBase64String(data));
            Bitmap b = new Bitmap(stream);
            return b;
        }

    }
}
