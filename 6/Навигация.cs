using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace _6
{
    public partial class Навигация : Form
    {
        public Навигация()
        {
            InitializeComponent();
        }
       
        private void Навигация_Load(object sender, EventArgs e)
        {

        }

        private void btnGoToAdd_Click(object sender, EventArgs e)
        {
            Form frm = new Новый_покупатель();
            frm.Show();
        }

        private void btnGoToFillOrCancel_Click(object sender, EventArgs e)
        {
            Form frm = new Добавить_или_удалить();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    
    
}
