﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;

namespace AP_CINE_APPLI
{
    
    public partial class home : Form
    {
        Film film;
        Genre genre;
        Salle salle;
        Projection projection;
        PDF pdf;
        public string pwdDB = "root";

        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public home()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(149, 222);
            panelMenu.Controls.Add(leftBorderBtn);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(253, 195, 0);
                currentBtn.ForeColor = Color.FromArgb(32, 32, 32);
                currentBtn.IconColor = Color.FromArgb(32, 32, 32);
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(32, 32, 32);
                currentBtn.ForeColor = Color.FromArgb(253, 195, 0);
                currentBtn.IconColor = Color.FromArgb(253, 195, 0);
            }
        }

        private void home_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm == null || currentChildForm.GetType() != childForm.GetType())
            {
                currentChildForm?.Close();

                currentChildForm = (Form)Activator.CreateInstance(childForm.GetType());
                currentChildForm.TopLevel = false;
                currentChildForm.FormBorderStyle = FormBorderStyle.None;
                currentChildForm.Dock = DockStyle.Fill;
                panelDesktop.Controls.Clear();
                panelDesktop.Controls.Add(currentChildForm);
                currentChildForm.BringToFront();
                currentChildForm.Show();
            }
        }

        private void btnGenre_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Genre());
        }

        private void btnSalle_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Salle());
        }

        private void btnFilm_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Film());
        }

        private void btnProjection_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Projection());
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new PDF());
        }

        private void btnAccueil_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm = null;
            }
        }

        //Déplacement de la fenêtre
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
