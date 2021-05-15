using cafe_cafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_cafe
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            LoadTable();
        }

        //void LoadAccount()
        //{
        //    string query = "exec dbo.USP_GetListAccount @userName";

        //    dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] {"admin"});
        //}

        void LoadTable()
        {
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

        private void lvTable_Click(object sender, EventArgs e)
        {
            string a = lvTable.SelectedItems[0].SubItems[1].Text;
            label1.Text = a.ToString();
        }
    }
}
