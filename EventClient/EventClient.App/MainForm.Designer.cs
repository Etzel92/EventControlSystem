namespace EventClient.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOpenManual = new Button();
            SuspendLayout();
            // 
            // btnOpenManual
            // 
            btnOpenManual.Location = new Point(571, 286);
            btnOpenManual.Name = "btnOpenManual";
            btnOpenManual.Size = new Size(182, 23);
            btnOpenManual.TabIndex = 0;
            btnOpenManual.Text = "Abrir formulario secundario";
            btnOpenManual.UseVisualStyleBackColor = true;
            btnOpenManual.Click += btnOpenManual_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOpenManual);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenManual;
    }
}
