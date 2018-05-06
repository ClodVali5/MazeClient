namespace Maze_Client
{
    partial class MazeClient
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
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.cmdSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.cmdStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPostData = new System.Windows.Forms.TextBox();
            this.chkPOST = new System.Windows.Forms.CheckBox();
            this.txtMovement = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdReset = new System.Windows.Forms.Button();
            this.txtStateRun = new System.Windows.Forms.TextBox();
            this.txtMaxDirections = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.cmdNorth = new System.Windows.Forms.Button();
            this.cmdWest = new System.Windows.Forms.Button();
            this.cmdEast = new System.Windows.Forms.Button();
            this.cmdSouth = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStatus.Location = new System.Drawing.Point(39, 208);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(657, 121);
            this.txtStatus.TabIndex = 22;
            this.txtStatus.TextChanged += new System.EventHandler(this.txtStatus_TextChanged);
            // 
            // cmdSend
            // 
            this.cmdSend.Location = new System.Drawing.Point(597, 172);
            this.cmdSend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(100, 28);
            this.cmdSend.TabIndex = 21;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 154);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Port:";
            this.label2.Visible = false;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(39, 95);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(333, 47);
            this.txtMessage.TabIndex = 19;
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(417, 28);
            this.cmdStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(64, 28);
            this.cmdStart.TabIndex = 16;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 154);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Host:";
            this.label1.Visible = false;
            // 
            // txtPostData
            // 
            this.txtPostData.Location = new System.Drawing.Point(417, 95);
            this.txtPostData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPostData.Multiline = true;
            this.txtPostData.Name = "txtPostData";
            this.txtPostData.Size = new System.Drawing.Size(279, 47);
            this.txtPostData.TabIndex = 23;
            // 
            // chkPOST
            // 
            this.chkPOST.AutoSize = true;
            this.chkPOST.Location = new System.Drawing.Point(624, 74);
            this.chkPOST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPOST.Name = "chkPOST";
            this.chkPOST.Size = new System.Drawing.Size(68, 21);
            this.chkPOST.TabIndex = 24;
            this.chkPOST.Text = "POST";
            this.chkPOST.UseVisualStyleBackColor = true;
            // 
            // txtMovement
            // 
            this.txtMovement.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMovement.Location = new System.Drawing.Point(39, 375);
            this.txtMovement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMovement.Multiline = true;
            this.txtMovement.Name = "txtMovement";
            this.txtMovement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMovement.Size = new System.Drawing.Size(657, 121);
            this.txtMovement.TabIndex = 25;
            this.txtMovement.TextChanged += new System.EventHandler(this.txtMovement_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Status / ResponseData";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 343);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Movement";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "URI  / Message";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(413, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "PostData";
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(597, 28);
            this.cmdReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(100, 28);
            this.cmdReset.TabIndex = 30;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // txtStateRun
            // 
            this.txtStateRun.Location = new System.Drawing.Point(389, 340);
            this.txtStateRun.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStateRun.Name = "txtStateRun";
            this.txtStateRun.Size = new System.Drawing.Size(147, 22);
            this.txtStateRun.TabIndex = 31;
            this.txtStateRun.Text = "State";
            // 
            // txtMaxDirections
            // 
            this.txtMaxDirections.Location = new System.Drawing.Point(652, 340);
            this.txtMaxDirections.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMaxDirections.Name = "txtMaxDirections";
            this.txtMaxDirections.Size = new System.Drawing.Size(39, 22);
            this.txtMaxDirections.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(545, 343);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 17);
            this.label7.TabIndex = 33;
            this.label7.Text = "MaxDirections";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(327, 343);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 34;
            this.label8.Text = "State ";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(325, 150);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(47, 22);
            this.txtPort.TabIndex = 36;
            this.txtPort.Text = "3000";
            this.txtPort.Visible = false;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(85, 150);
            this.txtHost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(153, 22);
            this.txtHost.TabIndex = 35;
            this.txtHost.Text = "127.0.0.1";
            this.txtHost.Visible = false;
            // 
            // cmdNorth
            // 
            this.cmdNorth.Location = new System.Drawing.Point(39, 28);
            this.cmdNorth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdNorth.Name = "cmdNorth";
            this.cmdNorth.Size = new System.Drawing.Size(61, 28);
            this.cmdNorth.TabIndex = 37;
            this.cmdNorth.Text = "North";
            this.cmdNorth.UseVisualStyleBackColor = true;
            this.cmdNorth.Click += new System.EventHandler(this.cmdNorth_Click);
            // 
            // cmdWest
            // 
            this.cmdWest.Location = new System.Drawing.Point(108, 28);
            this.cmdWest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdWest.Name = "cmdWest";
            this.cmdWest.Size = new System.Drawing.Size(61, 28);
            this.cmdWest.TabIndex = 38;
            this.cmdWest.Text = "West";
            this.cmdWest.UseVisualStyleBackColor = true;
            this.cmdWest.Click += new System.EventHandler(this.cmdWest_Click);
            // 
            // cmdEast
            // 
            this.cmdEast.Location = new System.Drawing.Point(177, 28);
            this.cmdEast.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdEast.Name = "cmdEast";
            this.cmdEast.Size = new System.Drawing.Size(61, 28);
            this.cmdEast.TabIndex = 39;
            this.cmdEast.Text = "East";
            this.cmdEast.UseVisualStyleBackColor = true;
            this.cmdEast.Click += new System.EventHandler(this.cmdEast_Click);
            // 
            // cmdSouth
            // 
            this.cmdSouth.Location = new System.Drawing.Point(247, 28);
            this.cmdSouth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdSouth.Name = "cmdSouth";
            this.cmdSouth.Size = new System.Drawing.Size(61, 28);
            this.cmdSouth.TabIndex = 40;
            this.cmdSouth.Text = "South";
            this.cmdSouth.UseVisualStyleBackColor = true;
            this.cmdSouth.Click += new System.EventHandler(this.cmdSouth_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(489, 28);
            this.cmdStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(64, 28);
            this.cmdStop.TabIndex = 41;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 512);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdSouth);
            this.Controls.Add(this.cmdEast);
            this.Controls.Add(this.cmdWest);
            this.Controls.Add(this.cmdNorth);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMaxDirections);
            this.Controls.Add(this.txtStateRun);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMovement);
            this.Controls.Add(this.chkPOST);
            this.Controls.Add(this.txtPostData);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.cmdSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Maze-Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPostData;
        private System.Windows.Forms.CheckBox chkPOST;
        private System.Windows.Forms.TextBox txtMovement;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.TextBox txtStateRun;
        private System.Windows.Forms.TextBox txtMaxDirections;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button cmdNorth;
        private System.Windows.Forms.Button cmdWest;
        private System.Windows.Forms.Button cmdEast;
        private System.Windows.Forms.Button cmdSouth;
        private System.Windows.Forms.Button cmdStop;
    }
}

