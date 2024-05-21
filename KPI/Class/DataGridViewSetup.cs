using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KPI.Class
{
    public static class DataGridViewSetup
    {
        public static void Norm1(DataGridView Dgv, string[] header, int[] width)
        {
            int len = header.Length;
            Dgv.ColumnCount = len;
            for (int i = 0; i < len; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", 9);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            Dgv.RowTemplate.Height = 30;
            // Dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //  Dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 197, 197);
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;

        }

        public static void Norm2(DataGridView Dgv, string[] header, int[] width)
        {
            int len = header.Length;
            Dgv.ColumnCount = len;
            for (int i = 0; i < len; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                Dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", 8);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8);
            Dgv.RowTemplate.Height = 25;
            // Dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //  Dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 197, 197);
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;

        }


        public static void Norm3(DataGridView Dgv, string[] header, int[] width, DataGridViewContentAlignment[] alignment)
        {
            int len = header.Length;
            Dgv.ColumnCount = len;
            for (int i = 0; i < len; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                Dgv.Columns[i].DefaultCellStyle.Alignment = alignment[i];
            }
            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", 8);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8);
            Dgv.RowTemplate.Height = 25;
            // Dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //  Dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 197, 197);
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;

        }

        public static void Norm4(DataGridView Dgv, string[] header, int[] width,int font)
        {
            int len = header.Length;
            Dgv.ColumnCount = len;
            for (int i = 0; i < len; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                Dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", font);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", font);
            Dgv.RowTemplate.Height = 25;
            // Dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //  Dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 197, 197);
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;

        }


        public static void Norm5(DataGridView Dgv, string[] header, int[] width, int font)
        {
            int len = header.Length;
            Dgv.ColumnCount = len;
            for (int i = 0; i < len; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                Dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", font);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", font);
            Dgv.RowTemplate.Height = 25;
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;

        }


        public static void MarkRowColor(DataGridView Dgv)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 197, 197),
                ForeColor = Color.Black
            };
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 255, 255),
                ForeColor = Color.Black
            };
            int row = Dgv.Rows.Count;
            int col = Dgv.Columns.Count;
            if (row > 0)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        Dgv.Rows[i].Cells[j].Style = styleNormal;
                    }
                }
                int rowc = Dgv.CurrentRow.Index;
                for (int j = 0; j < col; j++)
                {
                    Dgv.Rows[rowc].Cells[j].Style = style;
                }
            }



            //MClossDetailDic.Clear();
            //cmbLossDetail.Items.Clear();
            //string mcID = DataGridViewHistory.Rows[rowc].Cells[12].Value.ToString();
            //if (mcID != null && mcID != "")
            //{
            //    string lossId = DataGridViewHistory.Rows[rowc].Cells[2].Value.ToString();
            //    SqlClass sql = new SqlClass();
            //    bool sqlstatus = sql.Loss_MachineLossDetailSQL(mcID, lossId);
            //    if (sqlstatus)
            //    {
            //        DataTable dt = sql.Datatable;
            //        if (dt.Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                MClossDetailDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
            //                cmbLossDetail.Items.Add(dr.ItemArray[1].ToString());
            //            }
            //            cmbLossDetail.SelectedIndex = 0;

            //        }
            //    }
            //    else
            //    {
            //        cmbLossDetail.SelectedIndex = -1;
            //    }
            //}


        }

    }
}
