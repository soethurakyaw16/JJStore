using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JJStore
{
    public partial class Purchase : System.Web.UI.Page
    {
        string voucherCode;
        int StockID;
        string StockName;
        int Quantity;
        int PurchasePrice;
        int TotalPrice;
        string SupplierName;
        string Remark;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS.SOETHURAKYAW\\Desktop\\Computer_Studies_Semester_6\\C#_ADO.Net_ASP.Net\\Practice\\JJStore\\JJStore\\App_Data\\JJStore.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Session["VoucherCode"] == null)
                {
                    voucherCode = GenerateVoucherCodeWithDate(7); // Generate 4 random letters followed by today's date
                    Console.WriteLine(voucherCode);
                    Session["VoucherCode"] = voucherCode;
                 
                }
                lblVoucherCode.Text = Session["VoucherCode"].ToString(); // To fix the scenario voucher code
                
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
           
            string remark = ddlRemark.SelectedItem.Text;
            
            StockName = Name.Text;
            PurchasePrice = int.Parse(Price.Text);
            TotalPrice = int.Parse(Total_Price.Text);
            Quantity = int.Parse(Quanty.Text);
            Remark = ddlRemark.SelectedItem.Text;

            if(remark == "new")
            {
                int StockID = InsertStockFirstandGetStockID();
              
                InsertPurchase(StockID);
                //insertInventory();
            }
            else
            {
                StockID = int.Parse(ID.Text);
                InsertPurchase(StockID);// Automatically update the inventory SQL Trigger
            }
            
        }
        protected int InsertStockFirstandGetStockID()
        {
            int StockID = 0;
            string query = "INSERT INTO Stock (Name, PurchasePrice) VALUES (@Name, @PurchasePrice)";
            String query2 = "SELECT Id as lastrow FROM Stock";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Name", StockName );
                    command.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                    connection.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        using (SqlCommand command2 = new SqlCommand(query2, connection))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command2);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            DataRow lastRow = dataTable.Rows[dataTable.Rows.Count - 1];
                            StockID = (int)lastRow["lastrow"];
                        }
                    }
                }
            }
            return StockID;
        }

        protected void InsertPurchase(int StockID)
        {
            string name = GetName(StockID);
            string query = "INSERT INTO Purchase (VoucherCode, StockID, Name, Quantity, Price, TotalPrice, Remark) VALUES (@VoucherCode, @StockID, @Name, @Quantity, @Price, @TotalPrice, @Remark)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@VoucherCode", lblVoucherCode.Text);
                    command.Parameters.AddWithValue("@StockID", StockID);
                    command.Parameters.AddWithValue("@Name", name );
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@Price", PurchasePrice);
                    command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    command.Parameters.AddWithValue("@Remark", Remark);
                    connection.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    if(rowAffected > 0)
                    {
                        ID.Text = "";
                        Name.Text = "";
                        Quanty.Text = "";
                        Price.Text = "";
                        Total_Price.Text = "";

                        DataTable data = LoadData();
                        GridView1.DataSource = data;
                        GridView1.DataBind();
                    }
                }
            }
        }
        private DataTable LoadData()
        {
            DataTable data = new DataTable();

            string query = "SELECT * FROM Purchase WHERE VoucherCode = @VoucherCode";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VoucherCode", lblVoucherCode.Text);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(data);
                    }
                }
            }
            return data;
        }

        protected string GetName(int StockID)
        {
            string name = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Stock WHERE Id=@Id";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", StockID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             name = reader["Name"].ToString();
                        }
                    }
                }
            }
            return name;
        }







        private string GenerateVoucherCodeWithDate(int lettersLength) // Generating random voucher code with totday date
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();

            string randomLetters = new string(Enumerable.Repeat(letters, lettersLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            string formattedDate = DateTime.Now.ToString("yyyyMMdd");

            string voucherCode = randomLetters + formattedDate;
            return voucherCode;
        }
        
        protected void ddlRemark_SelectedIndexChanged(object sender, EventArgs e) //Handling the event for changing remark
        {
            string selectedValue = ddlRemark.SelectedValue;
            string selectedText =  ddlRemark.SelectedItem.Text;

            if (selectedText == "new")
            {
                ID.Enabled = false;
                Quanty.Enabled = true;
                Total_Price.Enabled = true;
                Name.Enabled = true;
                Price.Enabled = true;
            }
            if (selectedText == "none")
            {
                Name.Enabled = false;
                ID.Enabled = true;
                Quanty.Enabled = true;
                Total_Price.Enabled = true;
                Price.Enabled = false;
            }
            if (selectedText == "change")
            {
                Name.Enabled = false;
                ID.Enabled = true;
                Quanty.Enabled = true;
                Total_Price.Enabled = true;
                Price.Enabled = true;
            }

        }

    }

}