﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB150218
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Vjezba.Vjezba1 vj = new Vjezba.Vjezba1();

            //Application.Run(new Vjezba.Vjezba4());
            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {

                Application.Run(new Main());
            }
        }
    }
    
}