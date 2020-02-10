using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bsoft.Common
{
    public static class FormUtility
    {
        /// <summary>
        /// To adjust the height of form based on content
        /// </summary>
        /// <param name="CotainerflPanel">Must be flowlayout,with autosize , grow and shrink,nowrap,topbottomflow </param>
        /// <param name="OnoffControl">The control in container that is on/off</param>
        public static void adjustHt(Form ContainerForm, FlowLayoutPanel CotainerPanel, Control OnoffControl)
        {
            adjustHt(ContainerForm, CotainerPanel, OnoffControl, !OnoffControl.Visible);
        }

        public static void adjustHt(Form ContainerForm, FlowLayoutPanel CotainerPanel, Control OnoffControl, bool visible)
        {
            CotainerPanel.AutoSize = true;
            CotainerPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            int spaceBelowFl = ContainerForm.Height - CotainerPanel.Bottom;
            OnoffControl.Visible = visible;
            CotainerPanel.Refresh();
            ContainerForm.Height = CotainerPanel.Height + CotainerPanel.Top + spaceBelowFl;
            ContainerForm.Top = (Screen.PrimaryScreen.WorkingArea.Height - ContainerForm.Height) / 2;
        }

        public static void ShowForm<T>(this Form owner, ref T frm) where T : Form, new()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new T();
                frm.Owner = owner;
                //frm.MdiParent = this;//for mdi child way
            }
            frm.ShowInTaskbar = true;
            frm.Show();
            frm.Activate();
        }

        #region Generic funtions

        //    http://innovatetech.blogspot.com/2008/01/handling-net-toolstrip-merge-and.html
        /// <summary>
        /// way to get the form if not instantiated gets new
        /// If instattiaged returns old.
        //to merge toolstrip
        //protected override void OnMdiChildActivate(EventArgs e)
        //{
        //    base.OnMdiChildActivate(e); //REQUIRED
        //    HandleChildMerge(); //Handle merging
        //}
        //private void HandleChildMerge()
        //{
        //    ToolStripManager.RevertMerge(tsMain);
        //    IChildForm ChildForm = ActiveMdiChild as IChildForm;
        //    if (ChildForm != null && ChildForm.ChildToolStrip != null)
        //    {
        //        ToolStripManager.Merge(ChildForm.ChildToolStrip, tsMain);
        //    }
        //}

        #endregion Generic funtions
    }

    //just creata a class
    //help is set by setting tag with help:
    public class SetHelpLable
    {
        private List<Label> listLable = new List<Label>();

        public void SetHelp(Form f)
        {
            if (listLable.Count < 1)
            {
                CallAllControls(f);
            }
            else
            {
                foreach (var item in listLable)
                {
                    item.Visible = !item.Visible;
                }
            }
        }

        private void CallAllControls(Control parent)
        {
            foreach (Control C in parent.Controls)
            {
                if (C.Tag != null && C.Tag.ToString().ToLower().StartsWith("help:"))
                {
                    SetLable(C, C.Tag.ToString());
                }
                CallAllControls(C);
            }
        }

        /// <summary>
        /// Sets lables in controls that has tags started with help:
        /// </summary>
        /// <param name="control"></param>
        /// <param name="caption"></param>
        private void SetLable(Control control, string caption)
        {
           // caption = StringUtilities.(caption, "help:", "");//
            Label label = new Label();
            control.Parent.Controls.Add(label);
            label.Text = caption;
            label.Width = control.Width;
            label.BackColor = Color.White;
            label.Visible = true;
            label.AutoSize = true;
            label.MaximumSize = new Size(control.Width, 0);
            label.Top = control.Top - label.Height;
            label.Left = control.Left;
            listLable.Add(label);
        }
    }
}