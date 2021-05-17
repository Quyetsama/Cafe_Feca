using cafe_cafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_cafe
{
    public partial class Main : Form
    {

        public static int IDTable = 0;
        public static string IDFOOD = "";

        public Main()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
        }

        //void LoadAccount()
        //{
        //    string query = "exec dbo.USP_GetListAccount @userName";

        //    dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] {"admin"});
        //}


        void LoadCategory()
        {
            // Lay danh sach loai san pham
            List<Category> listCategory = new List<Category>();

            string queryCategory = "select * from FoodCategory";

            DataTable dataCategory = DataProvider.Instance.ExecuteQuery(queryCategory);

            foreach(DataRow row in dataCategory.Rows)
            {
                Category category = new Category(row);
                listCategory.Add(category);
            }

            cbbLoaiSanPham.DataSource = listCategory;
            cbbLoaiSanPham.DisplayMember = "name";

        }


        void LoadFoodListByCategoryID(int id)
        {
            // Lay danh sach san pham theo loia san pham
            List<Food> listFood = new List<Food>();

            string queryFood = "select * from Food where idCategory = " + id;

            DataTable dataFood = DataProvider.Instance.ExecuteQuery(queryFood);

            foreach (DataRow row in dataFood.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }


            datagridFood.DataSource = dataFood;
            datagridFood.Columns["idCategory"].Visible = false;

        }


        void LoadTable()
        {
            lvTable.Items.Clear();

            List<Table> tablelist = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("exec USP_GetTableList");


            this.lvTable.View = View.LargeIcon;
            this.imageList1.ImageSize = new Size(120, 120);
            this.lvTable.LargeImageList = this.imageList1;


            foreach (DataRow row in data.Rows)
            {
                Table table = new Table(row);
                tablelist.Add(table);

                ListViewItem item = new ListViewItem(table.Name.ToString());
                ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, table.ID.ToString());
                ListViewItem status = new ListViewItem(table.Status.ToString());

                if (status.Text.ToString() == "Trống")
                {
                    item.SubItems.Add(subitem);
                    lvTable.Items.Add(item);
                    item.ImageIndex = 1;
                }
                else
                {
                    item.SubItems.Add(subitem);
                    lvTable.Items.Add(item);
                    item.ImageIndex = 0;
                }


                //Button btn = new Button() { Width = 80, Height = 80 };
                //btn.Text = table.Name + Environment.NewLine + table.Status;

                //if (table.Status == "Trống")
                //{
                //    btn.BackColor = Color.Aqua;
                //}
                //else
                //{
                //    btn.BackColor = Color.LightPink;
                //}

                //flpTable.Controls.Add(btn);
            }
        }

        void showBill(int id)
        {
            lvBill.Items.Clear();

            //// get bill chưa thanh toan
            //DataTable dataBill = DataProvider.Instance.ExecuteQuery("select * from Bill where idTable = " + id + "and status = 0");

            //int billID = -1;

            //if(dataBill.Rows.Count > 0)
            //{
            //    Bill bill = new Bill(dataBill.Rows[0]);
            //    billID = bill.ID;
            //}

            List<BillInfo> listBillInfo = new List<BillInfo>();
            //label1.Text = billID.ToString();

            string query = "select f.name, bi.count, f.price, f.price*bi.count as totalPrice from Bill as b, BillInfo as bi, Food as f where bi.idBill = b.id and bi.idFood = f.id and b.status = 0 and b.idTable = " + id;
            DataTable dataBillInfo = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow row in dataBillInfo.Rows)
            {
                BillInfo info = new BillInfo(row);
                listBillInfo.Add(info);
            }


            float totalPrice = 0;

            foreach(BillInfo item in listBillInfo)
            {
                ListViewItem lvItem = new ListViewItem(item.NameFood.ToString());
                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice;

                lvBill.Items.Add(lvItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");
            txtTotalPrice.Text = totalPrice.ToString("c", culture);



            //DataTable dataBillInfo = DataProvider.Instance.ExecuteQuery("select * from BillInfo where idBill = " + billID);

            //foreach (DataRow row in dataBillInfo.Rows)
            //{
            //    BillInfo info = new BillInfo(row);
            //    listBillInfo.Add(info);
            //}


            //foreach (BillInfo item in listBillInfo)
            //{
            //    ListViewItem lvItem = new ListViewItem(item.IdFood.ToString());
            //    lvItem.SubItems.Add(item.Count.ToString());

            //    lvBill.Items.Add(lvItem);
            //}

        }

        private void lvTable_Click(object sender, EventArgs e)
        {
            string a = lvTable.SelectedItems[0].SubItems[1].Text;
            //label1.Text = a.ToString();
            showBill(Int32.Parse(a));

            IDTable = Int32.Parse(a);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void cbbLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if(cb.SelectedItem == null)
            {
                return;
            }


            Category selected = cb.SelectedItem as Category;

            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }

        private void datagridFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(datagridFood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //{
            //    datagridFood.CurrentRow.Selected = true;
            //    txtFood.Text = datagridFood.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
            //}

            try
            {
                datagridFood.CurrentRow.Selected = true;
                txtFood.Text = datagridFood.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();

                // luu id food
                IDFOOD = datagridFood.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            }
            catch(Exception ex)
            {
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            // get bill chưa thanh toan
            DataTable dataBill = DataProvider.Instance.ExecuteQuery("select * from Bill where idTable = " + IDTable + "and status = 0");

            int billID = -1;

            if (dataBill.Rows.Count > 0)
            {
                Bill bill = new Bill(dataBill.Rows[0]);
                billID = bill.ID;

                
            }

            string idFood = IDFOOD;
            int count = (int)numericUpDown1.Value;

            // neu billID = -1, nghia la ban do chua co bill va phai them bill moi
            if (billID == -1)
            {
                // Insert Bill
                DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idTable", new object[]{ IDTable });

                //lay id cuoi cung de insert billinfo
                int idBill = (int)DataProvider.Instance.ExecuteScalar("SELECT TOP 1 * FROM Bill ORDER BY id DESC");
                

                // Insert BillInfo
                DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBillInfo @idBill , @idFood , @count , @idTable", new object[]{idBill, idFood , count, IDTable });

                
            }
            else
            {
                DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBillInfo @idBill , @idFood , @count , @idTable", new object[]{billID, idFood, count, IDTable });
            }

            LoadTable();
            showBill(IDTable);

        }
    }
}
