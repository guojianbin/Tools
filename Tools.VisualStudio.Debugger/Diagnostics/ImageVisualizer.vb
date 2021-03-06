Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualStudio.DebuggerVisualizers

'#If True
'<Assembly: DebuggerVisualizer(GetType(DiagnosticsT.ImageVisualizer), GetType(DiagnosticsT.ImageVisualizer.VisualizerImageSource), Description:="Image visualizer", Target:=GetType(Image))> 
<Assembly: DebuggerVisualizer(GetType(DiagnosticsT.ImageVisualizer), Target:=GetType(Image), Description:="Image visualizer")> 
Namespace DiagnosticsT
    'TODO: ImageVisualizer Testing needed
    ''' <summary>Implements <see cref="DialogDebuggerVisualizer"/> for datatype <see cref="Image"/></summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">�onny</author>
    ''' <version version="1.5.2" stage="Nightly"><c>VersionAttribute</c> and <c>AuthorAttribute</c> removed</version>
    ''' <version version="1.5.4">Moved from assembly Tools.VisualStuido to new Tools.VisualStudio.Debugger. The new assembly is AnyCPU.</version>
    Public Class ImageVisualizer
        Inherits DialogDebuggerVisualizer
        ''' <summary>Shows visualizer dialog</summary>
        ''' <param name="objectProvider">An object of type <see cref="Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider"/>. This object provides communication from the debugger side of the visualizer to the object source (<see cref="Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource"/>) on the debuggee side.</param>
        ''' <param name="windowService">An object of type <see cref="Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService"/>, which provides methods your visualizer can use to display Windows forms, controls, and dialogs.</param>
        Protected Overrides Sub Show(ByVal windowService As Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService, ByVal objectProvider As Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider)
            Dim frm As VisualizerForm
            Dim bmp As Image
            Try
                bmp = TryCast(objectProvider.GetObject, Image) ' New Bitmap(objectProvider.GetData)
            Catch ex As Exception
                'Try
                '    bmp = objectProvider.GetObject
                'Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
                'End Try
            End Try
            If bmp Is Nothing Then Exit Sub
            frm = New VisualizerForm(bmp)
            windowService.ShowDialog(frm)
        End Sub
        ''' <summary>Realizes <see cref="Form"/> that shows <see cref="Image"/> being visualized</summary>
        Protected Class VisualizerForm : Inherits Form
            ''' <summary><see cref="PictureBox"/> to show <see cref="Drawing.Image"/> in</summary>
            Private WithEvents Image As New PictureBox
            ''' <summary><see cref="Label"/> that displays additional information about <see cref="Drawing.Image"/></summary>
            Private WithEvents Label As New Label
            ''' <summary>CTor</summary>
            ''' <param name="Image"><see cref="Drawing.Image"/> to be shown</param>
            Public Sub New(ByVal Image As Image)
                Me.InitializeComponent()
                Me.Image.Image = Image
                'Me.Icon = Tools.ResourcesT.Resources.ToolsIcon
                Me.Label.Text = Image.Size.ToString
            End Sub
            ''' <summary>Initializes form</summary>
            Private Sub InitializeComponent()
                Me.Image.Dock = DockStyle.Fill
                Me.Image.SizeMode = PictureBoxSizeMode.Zoom
                Me.Controls.Add(Me.Image)
                Me.Text = My.Resources.Resources.ImageVisualizer
                Me.Label.Dock = DockStyle.Bottom
                Me.Controls.Add(Label)
                Me.ShowInTaskbar = False
                Me.MinimizeBox = False
                Me.MaximizeBox = False
            End Sub
            ''' <summary>If tru image is shown in real size, if false it is shown zoomed to form</summary>
            Private Real As Boolean = False
            Private Sub Label_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label.Click
                If Real Then
                    Me.AutoScroll = False
                    Image.SizeMode = PictureBoxSizeMode.Zoom
                    Image.Dock = DockStyle.Fill
                Else
                    Me.AutoScroll = True
                    Image.Dock = DockStyle.None
                    Image.Anchor = AnchorStyles.Top Or AnchorStyles.Left
                    Image.SizeMode = PictureBoxSizeMode.AutoSize
                    Image.Left = 0
                    Image.Top = 0
                End If
                Real = Not Real
            End Sub
        End Class
        '''' <summary>Implements <see cref="VisualizerObjectSource"/> for <see cref="Image"/></summary>
        'Public Class VisualizerImageSource
        '    Inherits VisualizerObjectSource
        '    ''' <summary>Writes data that represents <paramref name="target"/> into <paramref name="outgoingData"/></summary>
        '    ''' <param name="outgoingData">Outgoing data stream.</param>
        '    ''' <param name="target">Object being visualized</param>
        '    Public Overrides Sub GetData(ByVal target As Object, ByVal outgoingData As System.IO.Stream)
        '        DirectCast(target, Image).Save(outgoingData, Drawing.Imaging.ImageFormat.Png)
        '    End Sub
        'End Class
    End Class
End Namespace
'#End If