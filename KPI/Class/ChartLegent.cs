using KPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static KPI.ProdForm.MachineDownTimeForm;

namespace KPI.Class
{
   public static class ChartLegent
    {

        public static void Legend_ListMultiColor1(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color[] _color, List<string> _dt)
        {
            float rowHeight = 20F;
            int numberRow = _dt.Count-2;
    
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panel.Name = panelName;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(1);
            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = (count + 1).ToString(); //.ItemArray[0].ToString();
                    string lossNumber = _dt[count]; // string.Empty; // _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }


        public static void Legend_NewListMultiColor1(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color[] _color, List<PartNumber> legend)
        {
            float rowHeight = 20F;
           int numberRow = legend.Count;

            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panel.Name = panelName;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(1);
            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                //for (int count = 0; count < numberRow; count++)
                //{
                int count = 0;
                foreach (PartNumber p in legend) 
                { 
                    string lossGroupID = (count + 1).ToString(); //.ItemArray[0].ToString();
                    string lossNumber = p.Partnumber; // string.Empty; // _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }



        public static void Legend_NewListMultiColor2(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color[] _color, List<EmpNameList> legend)
        {
            float rowHeight = 20F;
            int numberRow = legend.Count;

            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panel.Name = panelName;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(1);
            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                //for (int count = 0; count < numberRow; count++)
                //{
                int count = 0;
                foreach (EmpNameList p in legend)
                {
                    string lossGroupID = (p.UserId).ToString(); //.ItemArray[0].ToString();
                    string lossNumber = $": {p.Fullname}" ; // string.Empty; // _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }




        public static void Legend_ListMultiColor(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color[] _color, List<string> _dt)
        {
            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
           
            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = _dt[count]; //.ItemArray[0].ToString();
                    string lossNumber = string.Empty; // _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }

        public static void Legend_DTMultiColor(TableLayoutPanel momPanel,
            int startPanelRow, string panelName, string lbName, Color[] _color, DataTable _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Rows.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
          
            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = _dt.Rows[count].ItemArray[0].ToString();
                    string lossNumber = _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }


        public static void Legend_DTMultiColor1(TableLayoutPanel momPanel,
           int startPanelRow, string panelName, string lbName, Color[] _color, DataTable _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Rows.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = _dt.Rows[count].ItemArray[0].ToString();
                    string lossNumber = _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }





        public static void Legend_ListSingleColor(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color _color, List<string> _dt)
        {
            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
           
            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = _dt[count]; //.ItemArray[0].ToString();
                    string lossNumber = string.Empty; // _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color,
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }



        public static void Legend_ListSingleNOTColor(TableLayoutPanel momPanel,
           int startPanelRow, string panelName, string lbName, Color _color, List<MCIdname> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            momPanel.Controls.Add(panel, 0, startPanelRow);

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = _dt[count].MachineId;
                    string lossNumber = _dt[count].MachineName;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = Color.White,
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }

        public static void Legend_DTSingleColor(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color _color, DataTable _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Rows.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            

            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;

                for (int count = 0; count < numberRow; count++)
                {
                    string lossGroupID = _dt.Rows[count].ItemArray[0].ToString();
                    string lossNumber = _dt.Rows[count].ItemArray[1].ToString();


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color,
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                }
            }
        }



        public static void LegendOEEmenu(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName,  List<SevenLoss> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (SevenLoss d in _dt)
                {
                    string lossGroupID = d.lossName;
                    string lossNumber = "";


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = Color.FromArgb(d.r, d.g, d.b),
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }

        public static void LegendSevenLoss(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName,  List<SevenLoss> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (SevenLoss d in _dt)
                {
                    string lossGroupID = d.lossCode;
                    string lossNumber = d.lossName;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = Color.FromArgb(d.r,d.g,d.b),
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }


        public static void LegendAlarmCode(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color _color, List<AlarmCode> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (AlarmCode d in _dt)
                {
                    string lossGroupID = d.alarmecode;
                    string lossNumber = d.alarmename;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color,
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }


        public static void LegendLossCode(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color _color, List<SevenLoss> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (SevenLoss d in _dt)
                {
                    string lossGroupID = d.lossCode;
                    string lossNumber = d.lossName;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color,
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }


        public static void Clear(TableLayoutPanel momPanel, string panelName)
        {
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }
        }

        //private static int PieHitPointIndex(Chart pie, MouseEventArgs e)
        //{
        //    HitTestResult hitPiece = pie.HitTest(e.X, e.Y, ChartElementType.DataPoint);
        //    HitTestResult hitLegend = pie.HitTest(e.X, e.Y, ChartElementType.LegendItem);
        //    int pointIndex = -1;
        //    if (hitPiece.Series != null) pointIndex = hitPiece.PointIndex;
        //    if (hitLegend.Series != null) pointIndex = hitLegend.PointIndex;
        //    return pointIndex;
        //}

        public static void LegendTracGoodDefectProduction(TableLayoutPanel momPanel, int startPanelRow, 
            string panelName, string lbName,Color[] _color, List<string> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (string d in _dt)
                {
                    string lossGroupID = (count + 1).ToString();
                    string lossNumber = d;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = _color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }

        public static void LegendTracMachinedowntime(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, List<TracMachineLoss> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (TracMachineLoss d in _dt)
                {
                    string lossGroupID = (count+1).ToString();
                    string lossNumber = d.machineName;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = Color.Transparent,
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }


        public static void LegendTracPackingSlip(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName,Color[] color ,List<string> _dt)
        {

            float rowHeight = 20F;
            int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.RowStyles[0].SizeType = SizeType.Absolute;
                _tp.RowStyles[0].Height = rowHeight;


                int len = _dt.Count;
                len = len > 7 ? 7 : len;
                int count = 0;
                foreach (string d in _dt)
                {
                    string lossGroupID = (count + 1).ToString();
                    string lossNumber = d;


                    Label lb2 = new Label
                    {
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        Text = "",
                        BackColor = color[count],
                        Name = lbName + count // + ToString();
                    };
                    Label lb1 = new Label
                    {
                        Name = lbName + "_" + count,//  + ToString();
                        Margin = new Padding(1),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
                        Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
                    };


                    _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                    _tp.Controls.Add(lb1, 0, count);
                    _tp.Controls.Add(lb2, 0, count);
                    if (count == numberRow - 1)
                    {
                        _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        _tp.Controls.Add(new Panel(), 0, count + 1);
                    }
                    count++;
                }
            }
        }

        public static void LegendTracInital(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName)
        {

            //float rowHeight = 20F;
            //int numberRow = _dt.Count;
            TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            if (_tp != null)
            {
                _tp.Controls.Clear();
                _tp.Dispose();
                _tp = null;
            }

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 1,
                Name = panelName,
                Dock = DockStyle.Fill,
                Margin = new Padding(1)
            };
            momPanel.Controls.Add(panel, 0, startPanelRow);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));


            _tp = (TableLayoutPanel)momPanel.Controls[panelName];
            //if (_tp != null)
            //{
            //    _tp.RowStyles[0].SizeType = SizeType.Absolute;
            //    _tp.RowStyles[0].Height = rowHeight;


            //    int len = _dt.Count;
            //    len = len > 7 ? 7 : len;
            //    int count = 0;
            //    foreach (string d in _dt)
            //    {
            //        string lossGroupID = (count + 1).ToString();
            //        string lossNumber = d;


            //        Label lb2 = new Label
            //        {
            //            Margin = new Padding(1),
            //            Dock = DockStyle.Fill,
            //            BorderStyle = BorderStyle.FixedSingle,
            //            TextAlign = ContentAlignment.MiddleCenter,
            //            ForeColor = Color.White,
            //            Text = "",
            //            BackColor = color[count],
            //            Name = lbName + count // + ToString();
            //        };
            //        Label lb1 = new Label
            //        {
            //            Name = lbName + "_" + count,//  + ToString();
            //            Margin = new Padding(1),
            //            Dock = DockStyle.Fill,
            //            BorderStyle = BorderStyle.FixedSingle,
            //            TextAlign = ContentAlignment.MiddleLeft,
            //            ForeColor = Color.White,
            //            Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular),
            //            Text = string.Format("{0}: {1}", lossGroupID, lossNumber)
            //        };


            //        _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
            //        _tp.Controls.Add(lb1, 0, count);
            //        _tp.Controls.Add(lb2, 0, count);
            //        if (count == numberRow - 1)
            //        {
            //            _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            //            _tp.Controls.Add(new Panel(), 0, count + 1);
            //        }
            //        count++;
            //    }
            //}

        }

    }
}
