using System.Windows.Forms;

namespace EvidencijaIzostanaka
{
    partial class TeacherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plBottomPanel = new System.Windows.Forms.Panel();
            this.lbSchoolName = new System.Windows.Forms.Label();
            this.lbTeacherName = new System.Windows.Forms.Label();
            this.tabRaspored = new System.Windows.Forms.TabPage();
            this.cbWeek = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabelaRaspored = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabImenik = new System.Windows.Forms.TabPage();
            this.tabelaImenikUčenika = new System.Windows.Forms.TableLayoutPanel();
            this.lbNaziv = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plBottomPanel.SuspendLayout();
            this.tabRaspored.SuspendLayout();
            this.tabelaRaspored.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabImenik.SuspendLayout();
            this.tabelaImenikUčenika.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plBottomPanel
            // 
            this.plBottomPanel.BackColor = System.Drawing.Color.Green;
            this.plBottomPanel.Controls.Add(this.lbSchoolName);
            this.plBottomPanel.Controls.Add(this.lbTeacherName);
            this.plBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottomPanel.ForeColor = System.Drawing.Color.White;
            this.plBottomPanel.Location = new System.Drawing.Point(0, 613);
            this.plBottomPanel.Name = "plBottomPanel";
            this.plBottomPanel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.plBottomPanel.Size = new System.Drawing.Size(1182, 70);
            this.plBottomPanel.TabIndex = 1;
            // 
            // lbSchoolName
            // 
            this.lbSchoolName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbSchoolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSchoolName.Location = new System.Drawing.Point(972, 0);
            this.lbSchoolName.MaximumSize = new System.Drawing.Size(300, 0);
            this.lbSchoolName.Name = "lbSchoolName";
            this.lbSchoolName.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lbSchoolName.Size = new System.Drawing.Size(200, 70);
            this.lbSchoolName.TabIndex = 1;
            this.lbSchoolName.Text = "Škola: Sveti Sava";
            this.lbSchoolName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTeacherName
            // 
            this.lbTeacherName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTeacherName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTeacherName.Location = new System.Drawing.Point(10, 0);
            this.lbTeacherName.Name = "lbTeacherName";
            this.lbTeacherName.Size = new System.Drawing.Size(300, 70);
            this.lbTeacherName.TabIndex = 0;
            this.lbTeacherName.Text = "Nastavnik:";
            this.lbTeacherName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabRaspored
            // 
            this.tabRaspored.BackColor = System.Drawing.Color.Ivory;
            this.tabRaspored.Controls.Add(this.cbWeek);
            this.tabRaspored.Controls.Add(this.label1);
            this.tabRaspored.Controls.Add(this.tabelaRaspored);
            this.tabRaspored.Location = new System.Drawing.Point(4, 28);
            this.tabRaspored.Name = "tabRaspored";
            this.tabRaspored.Padding = new System.Windows.Forms.Padding(3);
            this.tabRaspored.Size = new System.Drawing.Size(1174, 581);
            this.tabRaspored.TabIndex = 2;
            this.tabRaspored.Text = "Raspored časova";
            // 
            // cbWeek
            // 
            this.cbWeek.FormattingEnabled = true;
            this.cbWeek.Location = new System.Drawing.Point(166, 35);
            this.cbWeek.Name = "cbWeek";
            this.cbWeek.Size = new System.Drawing.Size(205, 24);
            this.cbWeek.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Radna sedmica:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabelaRaspored
            // 
            this.tabelaRaspored.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabelaRaspored.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabelaRaspored.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tabelaRaspored.ColumnCount = 5;
            this.tabelaRaspored.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabelaRaspored.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabelaRaspored.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabelaRaspored.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabelaRaspored.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabelaRaspored.Controls.Add(this.label6, 4, 0);
            this.tabelaRaspored.Controls.Add(this.label5, 3, 0);
            this.tabelaRaspored.Controls.Add(this.label3, 2, 0);
            this.tabelaRaspored.Controls.Add(this.label2, 1, 0);
            this.tabelaRaspored.Controls.Add(this.label4, 0, 0);
            this.tabelaRaspored.Location = new System.Drawing.Point(35, 110);
            this.tabelaRaspored.Name = "tabelaRaspored";
            this.tabelaRaspored.RowCount = 7;
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.34224F));
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77602F));
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77602F));
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77602F));
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77602F));
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77602F));
            this.tabelaRaspored.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.77768F));
            this.tabelaRaspored.Size = new System.Drawing.Size(1100, 406);
            this.tabelaRaspored.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Green;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(877, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 116);
            this.label6.TabIndex = 6;
            this.label6.Text = "PETAK";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Green;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(658, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 116);
            this.label5.TabIndex = 5;
            this.label5.Text = "ČETVRTAK";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Green;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(439, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 116);
            this.label3.TabIndex = 4;
            this.label3.Text = "SRIJEDA";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Green;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(220, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 116);
            this.label2.TabIndex = 3;
            this.label2.Text = "UTORAK";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Green;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 116);
            this.label4.TabIndex = 2;
            this.label4.Text = "PONEDJELJAK";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabMain.Controls.Add(this.tabRaspored);
            this.tabMain.Controls.Add(this.tabImenik);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1182, 613);
            this.tabMain.TabIndex = 2;
            this.tabMain.Text = "Imenik učenika";
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabImenik
            // 
            this.tabImenik.BackColor = System.Drawing.Color.Ivory;
            this.tabImenik.Controls.Add(this.panel1);
            this.tabImenik.Location = new System.Drawing.Point(4, 28);
            this.tabImenik.Name = "tabImenik";
            this.tabImenik.Padding = new System.Windows.Forms.Padding(3);
            this.tabImenik.Size = new System.Drawing.Size(1174, 581);
            this.tabImenik.TabIndex = 3;
            this.tabImenik.Text = "Imenik učenika";
            // 
            // tabelaImenikUčenika
            // 
            this.tabelaImenikUčenika.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabelaImenikUčenika.AutoSize = true;
            this.tabelaImenikUčenika.BackColor = System.Drawing.SystemColors.Control;
            this.tabelaImenikUčenika.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tabelaImenikUčenika.ColumnCount = 5;
            this.tabelaImenikUčenika.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tabelaImenikUčenika.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.33788F));
            this.tabelaImenikUčenika.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.22071F));
            this.tabelaImenikUčenika.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.22071F));
            this.tabelaImenikUčenika.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.22071F));
            this.tabelaImenikUčenika.Controls.Add(this.lbNaziv, 0, 0);
            this.tabelaImenikUčenika.Controls.Add(this.label7, 2, 0);
            this.tabelaImenikUčenika.Location = new System.Drawing.Point(0, 3);
            this.tabelaImenikUčenika.Name = "tabelaImenikUčenika";
            this.tabelaImenikUčenika.RowCount = 1;
            this.tabelaImenikUčenika.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tabelaImenikUčenika.Size = new System.Drawing.Size(1100, 102);
            this.tabelaImenikUčenika.TabIndex = 0;
            // 
            // lbNaziv
            // 
            this.lbNaziv.AutoSize = true;
            this.lbNaziv.BackColor = System.Drawing.Color.Green;
            this.tabelaImenikUčenika.SetColumnSpan(this.lbNaziv, 2);
            this.lbNaziv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNaziv.ForeColor = System.Drawing.Color.White;
            this.lbNaziv.Location = new System.Drawing.Point(1, 1);
            this.lbNaziv.Margin = new System.Windows.Forms.Padding(0);
            this.lbNaziv.Name = "lbNaziv";
            this.lbNaziv.Size = new System.Drawing.Size(653, 100);
            this.lbNaziv.TabIndex = 1;
            this.lbNaziv.Text = "Učenici";
            this.lbNaziv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Green;
            this.tabelaImenikUčenika.SetColumnSpan(this.label7, 3);
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(655, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(444, 100);
            this.label7.TabIndex = 2;
            this.label7.Text = "Izostanci";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabelaImenikUčenika);
            this.panel1.Location = new System.Drawing.Point(32, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 503);
            this.panel1.TabIndex = 1;
            // 
            // TeacherForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 683);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.plBottomPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TeacherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeacherForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeacherForm2_FormClosing);
            this.plBottomPanel.ResumeLayout(false);
            this.tabRaspored.ResumeLayout(false);
            this.tabRaspored.PerformLayout();
            this.tabelaRaspored.ResumeLayout(false);
            this.tabelaRaspored.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabImenik.ResumeLayout(false);
            this.tabelaImenikUčenika.ResumeLayout(false);
            this.tabelaImenikUčenika.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel plBottomPanel;
        private System.Windows.Forms.Label lbTeacherName;
        private System.Windows.Forms.Label lbSchoolName;
        private TabPage tabRaspored;
        private ComboBox cbWeek;
        private Label label1;
        private TableLayoutPanel tabelaRaspored;
        private Label label6;
        private Label label5;
        private Label label3;
        private Label label2;
        private Label label4;
        private TabControl tabMain;
        private TabPage tabImenik;
        private TableLayoutPanel tabelaImenikUčenika;
        private Label lbNaziv;
        private Label label7;
        private Panel panel1;
    }
}