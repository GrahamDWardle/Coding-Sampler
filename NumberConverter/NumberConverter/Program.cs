//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Graham D Wardle">
//     The Number words to number converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace NumberConverter
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The application entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormTextToNumber());
        }
    }
}
