﻿Imports System.Linq
Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MessageBoxForm
        Inherits System.Windows.Forms.Form



        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.lblPlhTop = New System.Windows.Forms.Label
            Me.picPicture = New System.Windows.Forms.PictureBox
            Me.lblPrompt = New System.Windows.Forms.Label
            Me.cmbCombo = New System.Windows.Forms.ComboBox
            Me.flpRadio = New System.Windows.Forms.FlowLayoutPanel
            Me.RadioPlh1 = New System.Windows.Forms.RadioButton
            Me.RadioPlh2 = New System.Windows.Forms.RadioButton
            Me.lblPlhMid = New System.Windows.Forms.Label
            Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel
            Me.ButtonPlh1 = New System.Windows.Forms.Button
            Me.buttonPlh2 = New System.Windows.Forms.Button
            Me.lblPlhBottom = New System.Windows.Forms.Label
            Me.flpChecks = New System.Windows.Forms.FlowLayoutPanel
            Me.CheckPlh1 = New System.Windows.Forms.CheckBox
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.tmrCountDown = New System.Windows.Forms.Timer(Me.components)
            Me.tlpMain.SuspendLayout()
            CType(Me.picPicture, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.flpRadio.SuspendLayout()
            Me.flpButtons.SuspendLayout()
            Me.flpChecks.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.AutoSize = True
            Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.ColumnCount = 2
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Controls.Add(Me.lblPlhTop, 0, 0)
            Me.tlpMain.Controls.Add(Me.picPicture, 0, 1)
            Me.tlpMain.Controls.Add(Me.lblPrompt, 1, 1)
            Me.tlpMain.Controls.Add(Me.cmbCombo, 0, 3)
            Me.tlpMain.Controls.Add(Me.flpRadio, 0, 4)
            Me.tlpMain.Controls.Add(Me.lblPlhMid, 0, 5)
            Me.tlpMain.Controls.Add(Me.flpButtons, 0, 6)
            Me.tlpMain.Controls.Add(Me.lblPlhBottom, 0, 7)
            Me.tlpMain.Controls.Add(Me.flpChecks, 0, 2)
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 8
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(346, 211)
            Me.tlpMain.TabIndex = 0
            '
            'lblPlhTop
            '
            Me.lblPlhTop.AutoSize = True
            Me.tlpMain.SetColumnSpan(Me.lblPlhTop, 2)
            Me.lblPlhTop.Location = New System.Drawing.Point(3, 0)
            Me.lblPlhTop.Name = "lblPlhTop"
            Me.lblPlhTop.Size = New System.Drawing.Size(88, 13)
            Me.lblPlhTop.TabIndex = 0
            Me.lblPlhTop.Text = "TOP Placeholder"
            Me.lblPlhTop.Visible = False
            '
            'picPicture
            '
            Me.picPicture.Dock = System.Windows.Forms.DockStyle.Fill
            Me.picPicture.Location = New System.Drawing.Point(3, 16)
            Me.picPicture.Name = "picPicture"
            Me.picPicture.Size = New System.Drawing.Size(64, 64)
            Me.picPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picPicture.TabIndex = 0
            Me.picPicture.TabStop = False
            '
            'lblPrompt
            '
            Me.lblPrompt.AutoSize = True
            Me.lblPrompt.Location = New System.Drawing.Point(73, 13)
            Me.lblPrompt.Name = "lblPrompt"
            Me.lblPrompt.Size = New System.Drawing.Size(46, 13)
            Me.lblPrompt.TabIndex = 1
            Me.lblPrompt.Text = "Prom&pt"
            Me.lblPrompt.UseMnemonic = False
            '
            'cmbCombo
            '
            Me.cmbCombo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlpMain.SetColumnSpan(Me.cmbCombo, 2)
            Me.cmbCombo.FormattingEnabled = True
            Me.cmbCombo.Location = New System.Drawing.Point(3, 109)
            Me.cmbCombo.Name = "cmbCombo"
            Me.cmbCombo.Size = New System.Drawing.Size(340, 21)
            Me.cmbCombo.TabIndex = 3
            '
            'flpRadio
            '
            Me.flpRadio.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpRadio.AutoSize = True
            Me.flpRadio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.SetColumnSpan(Me.flpRadio, 2)
            Me.flpRadio.Controls.Add(Me.RadioPlh1)
            Me.flpRadio.Controls.Add(Me.RadioPlh2)
            Me.flpRadio.Location = New System.Drawing.Point(77, 133)
            Me.flpRadio.Margin = New System.Windows.Forms.Padding(0)
            Me.flpRadio.Name = "flpRadio"
            Me.flpRadio.Size = New System.Drawing.Size(192, 23)
            Me.flpRadio.TabIndex = 4
            '
            'RadioPlh1
            '
            Me.RadioPlh1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RadioPlh1.AutoSize = True
            Me.RadioPlh1.Location = New System.Drawing.Point(3, 3)
            Me.RadioPlh1.Name = "RadioPlh1"
            Me.RadioPlh1.Size = New System.Drawing.Size(90, 17)
            Me.RadioPlh1.TabIndex = 0
            Me.RadioPlh1.TabStop = True
            Me.RadioPlh1.Text = "RadioButton1"
            Me.RadioPlh1.UseVisualStyleBackColor = True
            '
            'RadioPlh2
            '
            Me.RadioPlh2.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RadioPlh2.AutoSize = True
            Me.RadioPlh2.Location = New System.Drawing.Point(99, 3)
            Me.RadioPlh2.Name = "RadioPlh2"
            Me.RadioPlh2.Size = New System.Drawing.Size(90, 17)
            Me.RadioPlh2.TabIndex = 1
            Me.RadioPlh2.TabStop = True
            Me.RadioPlh2.Text = "RadioButton2"
            Me.RadioPlh2.UseVisualStyleBackColor = True
            '
            'lblPlhMid
            '
            Me.lblPlhMid.AutoSize = True
            Me.tlpMain.SetColumnSpan(Me.lblPlhMid, 2)
            Me.lblPlhMid.Location = New System.Drawing.Point(3, 156)
            Me.lblPlhMid.Name = "lblPlhMid"
            Me.lblPlhMid.Size = New System.Drawing.Size(86, 13)
            Me.lblPlhMid.TabIndex = 5
            Me.lblPlhMid.Text = "MID Placeholder"
            Me.lblPlhMid.Visible = False
            '
            'flpButtons
            '
            Me.flpButtons.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpButtons.AutoSize = True
            Me.flpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.SetColumnSpan(Me.flpButtons, 2)
            Me.flpButtons.Controls.Add(Me.ButtonPlh1)
            Me.flpButtons.Controls.Add(Me.buttonPlh2)
            Me.flpButtons.Location = New System.Drawing.Point(113, 169)
            Me.flpButtons.Margin = New System.Windows.Forms.Padding(0)
            Me.flpButtons.Name = "flpButtons"
            Me.flpButtons.Size = New System.Drawing.Size(120, 29)
            Me.flpButtons.TabIndex = 6
            '
            'ButtonPlh1
            '
            Me.ButtonPlh1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonPlh1.AutoSize = True
            Me.ButtonPlh1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ButtonPlh1.Location = New System.Drawing.Point(3, 3)
            Me.ButtonPlh1.Name = "ButtonPlh1"
            Me.ButtonPlh1.Size = New System.Drawing.Size(54, 23)
            Me.ButtonPlh1.TabIndex = 0
            Me.ButtonPlh1.Text = "Button1"
            Me.ButtonPlh1.UseVisualStyleBackColor = True
            '
            'buttonPlh2
            '
            Me.buttonPlh2.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.buttonPlh2.AutoSize = True
            Me.buttonPlh2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.buttonPlh2.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.buttonPlh2.Location = New System.Drawing.Point(63, 3)
            Me.buttonPlh2.Name = "buttonPlh2"
            Me.buttonPlh2.Size = New System.Drawing.Size(54, 23)
            Me.buttonPlh2.TabIndex = 1
            Me.buttonPlh2.Text = "Button2"
            Me.buttonPlh2.UseVisualStyleBackColor = True
            '
            'lblPlhBottom
            '
            Me.lblPlhBottom.AutoSize = True
            Me.tlpMain.SetColumnSpan(Me.lblPlhBottom, 2)
            Me.lblPlhBottom.Location = New System.Drawing.Point(3, 198)
            Me.lblPlhBottom.Name = "lblPlhBottom"
            Me.lblPlhBottom.Size = New System.Drawing.Size(99, 13)
            Me.lblPlhBottom.TabIndex = 7
            Me.lblPlhBottom.Text = "Bottom Placeholder"
            Me.lblPlhBottom.Visible = False
            '
            'flpChecks
            '
            Me.flpChecks.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpChecks.AutoSize = True
            Me.flpChecks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.SetColumnSpan(Me.flpChecks, 2)
            Me.flpChecks.Controls.Add(Me.CheckPlh1)
            Me.flpChecks.Location = New System.Drawing.Point(129, 83)
            Me.flpChecks.Margin = New System.Windows.Forms.Padding(0)
            Me.flpChecks.Name = "flpChecks"
            Me.flpChecks.Size = New System.Drawing.Size(87, 23)
            Me.flpChecks.TabIndex = 8
            '
            'CheckPlh1
            '
            Me.CheckPlh1.AutoSize = True
            Me.CheckPlh1.Location = New System.Drawing.Point(3, 3)
            Me.CheckPlh1.Name = "CheckPlh1"
            Me.CheckPlh1.Size = New System.Drawing.Size(81, 17)
            Me.CheckPlh1.TabIndex = 2
            Me.CheckPlh1.Text = "CheckBox1"
            Me.CheckPlh1.UseMnemonic = False
            Me.CheckPlh1.UseVisualStyleBackColor = True
            '
            'tmrCountDown
            '
            Me.tmrCountDown.Interval = 1000
            '
            'MessageBoxForm
            '
            Me.AcceptButton = Me.ButtonPlh1
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.CancelButton = Me.buttonPlh2
            Me.ClientSize = New System.Drawing.Size(346, 211)
            Me.Controls.Add(Me.tlpMain)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MessageBoxForm"
            Me.RightToLeftLayout = True
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "MesssageBox"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            CType(Me.picPicture, System.ComponentModel.ISupportInitialize).EndInit()
            Me.flpRadio.ResumeLayout(False)
            Me.flpRadio.PerformLayout()
            Me.flpButtons.ResumeLayout(False)
            Me.flpButtons.PerformLayout()
            Me.flpChecks.ResumeLayout(False)
            Me.flpChecks.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Protected Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Protected Friend WithEvents picPicture As System.Windows.Forms.PictureBox
        Protected Friend WithEvents lblPrompt As System.Windows.Forms.Label
        Protected Friend WithEvents lblPlhTop As System.Windows.Forms.Label
        Protected Friend WithEvents CheckPlh1 As System.Windows.Forms.CheckBox
        Protected Friend WithEvents cmbCombo As System.Windows.Forms.ComboBox
        Protected Friend WithEvents flpRadio As System.Windows.Forms.FlowLayoutPanel
        Protected Friend WithEvents RadioPlh1 As System.Windows.Forms.RadioButton
        Protected Friend WithEvents RadioPlh2 As System.Windows.Forms.RadioButton
        Protected Friend WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
        Protected Friend WithEvents ButtonPlh1 As System.Windows.Forms.Button
        Protected Friend WithEvents buttonPlh2 As System.Windows.Forms.Button
        Protected Friend WithEvents lblPlhBottom As System.Windows.Forms.Label
        Protected Friend WithEvents lblPlhMid As System.Windows.Forms.Label
        Protected Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Protected Friend WithEvents tmrCountDown As System.Windows.Forms.Timer
        Friend WithEvents flpChecks As System.Windows.Forms.FlowLayoutPanel
    End Class
End Namespace
