using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class LoadingDialog : Form
    {
        ////Delegate for cross thread call to close
        //private delegate void CloseDelegate();

        ////The type of form to be displayed as the splash screen.
        //private static LoadingDialog loading;

        //static public void ShowLoadingScreen()
        //{
        //    // Make sure it is only launched once.

        //    if (loading != null)
        //        return;
        //    Thread thread = new Thread(new ThreadStart(LoadingDialog.ShowForm));
        //    thread.IsBackground = true;
        //    thread.SetApartmentState(ApartmentState.STA);
        //    thread.Start();
        //}

        //static private void ShowForm()
        //{
        //    loading = new LoadingDialog();
        //    Application.Run(loading);
        //}

        //static public void CloseLoadingScreen()
        //{
        //    loading.Invoke(new CloseDelegate(LoadingDialog.CloseFormInternal));
        //}

        //static private void CloseFormInternal()
        //{
        //    loading.Close();
        //}
    }
}
