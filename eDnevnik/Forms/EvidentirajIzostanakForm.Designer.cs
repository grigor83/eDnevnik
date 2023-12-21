namespace EvidencijaIzostanaka
{
    partial class EvidentirajIzostanakForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbTop = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbLesson = new System.Windows.Forms.Label();
            this.lbSubjectName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(50, 57);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(500, 250);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // lbTop
            // 
            this.lbTop.BackColor = System.Drawing.Color.Green;
            this.lbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTop.ForeColor = System.Drawing.Color.White;
            this.lbTop.Location = new System.Drawing.Point(0, 0);
            this.lbTop.Name = "lbTop";
            this.lbTop.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbTop.Size = new System.Drawing.Size(600, 40);
            this.lbTop.TabIndex = 1;
            this.lbTop.Text = "Evidencija prisustva";
            this.lbTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Green;
            this.panel1.Controls.Add(this.lbLesson);
            this.panel1.Controls.Add(this.lbSubjectName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 326);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 40);
            this.panel1.TabIndex = 2;
            // 
            // lbLesson
            // 
            this.lbLesson.BackColor = System.Drawing.Color.Green;
            this.lbLesson.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbLesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLesson.ForeColor = System.Drawing.Color.White;
            this.lbLesson.Location = new System.Drawing.Point(335, 0);
            this.lbLesson.Name = "lbLesson";
            this.lbLesson.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lbLesson.Size = new System.Drawing.Size(265, 40);
            this.lbLesson.TabIndex = 1;
            this.lbLesson.Text = "Datum i čas: ";
            this.lbLesson.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbSubjectName
            // 
            this.lbSubjectName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSubjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubjectName.ForeColor = System.Drawing.Color.White;
            this.lbSubjectName.Location = new System.Drawing.Point(0, 0);
            this.lbSubjectName.Name = "lbSubjectName";
            this.lbSubjectName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbSubjectName.Size = new System.Drawing.Size(290, 40);
            this.lbSubjectName.TabIndex = 0;
            this.lbSubjectName.Text = "Predmet: ";
            this.lbSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EvidentirajIzostanakForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbTop);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "EvidentirajIzostanakForm";
            this.Text = "EvidentirajIzostanakForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbSubjectName;
        private System.Windows.Forms.Label lbLesson;
    }
}