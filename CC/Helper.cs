using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public static class Helper
    {
        public static void ShowLoadingBox(this Form form)
        {
            XLoadingIcon loading = new XLoadingIcon();
            loading.Anchor = AnchorStyles.Top;
            int x = Convert.ToInt32((form.Width / 2) - (loading.Width / 2));
            int y = Convert.ToInt32((form.Height / 2) - (loading.Height / 2));
            loading.Location = new System.Drawing.Point(x, y);
            form.ResizeEnd += delegate (object sender, EventArgs e)
            {
                int mod_x = Convert.ToInt32((form.Width / 2) - (loading.Width / 2));
                int mod_y = Convert.ToInt32((form.Height / 2) - (loading.Height / 2));
                loading.Location = new System.Drawing.Point(mod_x, mod_y);
            };
            form.Controls.Add(loading);
            loading.BringToFront();
        }

        public static void HideLoadingBox(this Form form)
        {
            var loading = form.Controls.Cast<Control>().Where(c => c.GetType() == typeof(XLoadingIcon)).FirstOrDefault();

            if(loading != null)
                form.Controls.Remove(loading);
        }
    }
}
