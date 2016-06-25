using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ActiveUp.MailSystem.DesktopClient
{

    /// <summary>
    /// This class represents a splash screen.
    /// </summary>
    public partial class SplashForm : Form
    {

        /// <summary>
        /// Current thread attribute.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Splash Form constructor.
        /// </summary>
        public SplashForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Method for show splash screen.
        /// Starts the thread.
        /// </summary>
        public void ShowSplashScreen()
        {
            this.thread = new Thread(new ThreadStart(SplashScreen));
            this.thread.Start();
        }

        /// <summary>
        /// Method for close splash screen.
        /// Close the dialog and abort the thread.
        /// </summary>
        public void CloseSplashScreen()
        {
            Thread.Sleep(1000);
            this.DialogResult = DialogResult.OK;
            this.thread.Abort();
        }

        /// <summary>
        /// Method for splash screen.
        /// </summary>
        private void SplashScreen()
        {
            this.ShowDialog();
        }

        /// <summary>
        /// Event handler for timer tick.
        /// Increment the Form opacity.
        /// </summary>
        /// <param name="sender">The timer sender object.</param>
        /// <param name="e">The event arguments for timer.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.1;
            }
        }

        /// <summary>
        /// Event handler for splash form load.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void SplashForm_Load(object sender, EventArgs e)
        {
            this.timer.Start();
        }
    }
}