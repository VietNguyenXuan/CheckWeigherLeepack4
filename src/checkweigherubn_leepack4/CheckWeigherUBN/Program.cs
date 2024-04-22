using CheckWeigherUBN.ASingleInstanceApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Viet dev add
            if (!SingleInstance.Start())
            {
              SingleInstance.ShowFirstInstance();
              return;
            }
            // Viet dev add

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

			      //Dev add
			      SingleInstance.Stop();
		    }
    }
}
