using ImageGetter.Handlers;
using ImageGetter.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGetter
{
    public partial class Form1 : Form
    {
        private Ui _ui;

        public Ui Ui { get { return _ui; } set { _ui = value; } }
        public Form1()
        {
            InitializeComponent();

            //Init the empty/inital datatable
            this.Setup();
            Ui.SetSysMessage(msgBox, "Program készen áll a használatra!");            
        }


        
        private void Setup()
        {
            /*
             *UI: 
             */
            Ui = new Ui();
            Ui.MsgBox = msgBox;            

            /*
             * Grid:
             */
            tableUI.Columns.Add("URL", "URL");
            tableUI.Columns.Add("Név", "Név");
            tableUI.Columns.Add("Kiterjesztés", "Kiterjesztés");
            tableUI.Columns.Add("Status", "Status");
            tableUI.Columns.Add("Leírás", "Leírás");
            tableUI.KeyDown += Paste;

            tableUI.Columns["Status"].ReadOnly = true;
            tableUI.Columns["Status"].DefaultCellStyle.BackColor = Color.LightGray;

            tableUI.Columns["Leírás"].DefaultCellStyle.BackColor = Color.LightGray;
            tableUI.Columns["Leírás"].ReadOnly = true;
            /*
             * Features:
             */

        }

        private void Paste(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string clipBoardText = Clipboard.GetText();
                    string[] rows = clipBoardText.Split('\n');

                    int currentRow = tableUI.CurrentCell.RowIndex;
                    int currentCol = tableUI.CurrentCell.ColumnIndex;

                    foreach (string row in rows)
                    {
                        if (string.IsNullOrWhiteSpace(row))
                        {
                            continue;
                        }
                        string[] cells = row.Trim().Split('\t');
                        for(int i = 0; i < cells.Length; i++)
                        {
                            if(currentRow+1 == tableUI.RowCount && tableUI.RowCount < rows.Length) tableUI.Rows.Add();
                            if (cells.Length == 1)
                            {
                                tableUI[currentCol, currentRow++].Value = cells[i].Trim();
                            }
                            else
                            {
                                tableUI[currentCol+i, currentRow].Value = cells[i].Trim();
                                //Utolsó eleménél hozzáadok egysor
                                if (i == cells.Length - 1) currentRow++;
                            }
                        }
                    }

                } catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a bemásoláskor \n" + ex.Message);
                }
            }

        }

        private void clearTable_Click(object sender, EventArgs e)
        {
            try
            {
                tableUI.Rows.Clear();

            } catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a tábla ürítésekor \n" + ex.Message);
            }
        }

        private async void downloadImages_Click(object sender, EventArgs e)
        {
            Ui.SetSysMessage(msgBox, "Letöltés megkezdődött");

            FileController fc = new FileController(this);
            await fc.DownloadFiles(tableUI);

            Ui.SetSysMessage(msgBox, "Letöltés befejeződött");
        }

        private async void inLoad_Click(object sender, EventArgs e)
        {   
            FileController fc = new FileController(this);
            await fc.ImportFromExcel(tableUI);

            Ui.SetSysMessage(msgBox, "Fájl betöltése sikeres");
        }

        private void OpenFileContainerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FileController fc = new FileController(this);
                if (Directory.Exists(fc.OutputDirectory))
                {                    
                    Process.Start(fc.OutputDirectory);
                } else
                {
                    Ui.SetSysMessage(msgBox, "Semmi baj! A mappa még nem létezik vagy törlődött, próbáld meg előbb futattni a letöltést és majd létrejön.");
                }

            } catch(Exception ex)
            {
                MessageBox.Show("Oops, valami hiba történt. A mappa megnyitás sikertelen" + ex.Message);
            }            
        }
    }
}