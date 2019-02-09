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
    public partial class Новый_покупатель : Form
    {
        public Новый_покупатель()
        {
            InitializeComponent();
        }
        private int parsedCustomerID;
        private int orderID;
        private bool IsCustomerNameValin()
        {
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please enter a name.");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool IsOrderDataValid()
        {
            if (txtCustomerID.Text == "")
            {
                MessageBox.Show("{pls cr cus ac be pl or");
                return false;
            }
            else if ((numOrderAmount.Value < 1))
            {
                MessageBox.Show(" pls sp an or amo.");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ClearForm()
            {
            txtCustomerName.Clear();
            txtCustomerID.Clear();
            dtpOrderDate.Value = DateTime.Now;
            numOrderAmount.Value = 0;
            this.parsedCustomerID = 0;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (IsCustomerNameValin())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlCommand sqlComand = new SqlCommand("Sales.uspNewCustomer", connection))
                    {
                        sqlComand.CommandType = CommandType.StoredProcedure;
                        sqlComand.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar, 40));
                        sqlComand.Parameters["@CustomerName"].Value = txtCustomerName.Text;
                        sqlComand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                        sqlComand.Parameters["CustomerID"].Direction = ParameterDirection.Output;

                        try
                        {
                            connection.Open();
                            sqlComand.ExecuteNonQuery();
                            this.parsedCustomerID = (int)sqlComand.Parameters["@CustomerID"].Value;
                            this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
                        }
                        catch
                        {
                            MessageBox.Show("Customer ID was not returned.Account could not be created");
                        }
                        finally
                        {
                            connection.Close();
                        }
                        
                    }
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (IsOrderDataValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand ("Sales.uspPlaceNewOrder", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int)); 
                        sqlCommand.Parameters["@CustomerID"].Value = this.parsedCustomerID;

                        sqlCommand.Parameters.Add(new SqlParameter("@orderDate", SqlDbType.DateTime, 8));
                        sqlCommand.Parameters["@OrderDate"].Value = dtpOrderDate.Value;

                        sqlCommand.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                        sqlCommand.Parameters["@Amount"].Value = numOrderAmount.Value;

                        sqlCommand.Parameters.Add(new SqlParameter("@status",SqlDbType.Char,1));
                        sqlCommand.Parameters["@status"].Value = "0";

                        sqlCommand.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int));
                        sqlCommand.Parameters["@RC"].Direction = ParameterDirection.ReturnValue;

                        try
                        {
                            connection.Open();

                            sqlCommand.ExecuteNonQuery();

                            this.orderID = (int)sqlCommand.Parameters["@RC"].Value;
                            MessageBox.Show("Order number" + this.orderID + "has been submetted.");
                        }
                        catch
                        {
                            MessageBox.Show("Order could not be placed.");
                           
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }

                }
            }
        }

        private void btnAddAnotherAccount_Click(object sender, EventArgs e)
        {
            this.ClearForm();
        }

        private void btnAddFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
