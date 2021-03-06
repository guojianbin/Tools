Imports System.Drawing
Imports System.Windows.Forms
Imports Tools.ComponentModelT
Imports Tools.ResourcesT
Imports Tools.WindowsT.FormsT.UtilitiesT
'Extracted

Namespace WindowsT.FormsT
    ''' <summary>Marks state of data item</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">�onny</author>
    <Version(1, 0, GetType(StatusMarker), LastChange:="07/22/2007")> _
    <FirstVersion("06/22/2007")> _
    <ToolboxBitmap(GetType(StatusMarker), "StatusMarker.bmp")> _
    <Prefix("stm")> _
    <DefaultProperty("Status")> _
    <DefaultBindingProperty("Status")> _
    Public Class StatusMarker : Implements IControlWithStatus
        'TODO: Event attributes
        ''' <summary>Supported states of <see cref="StatusMarker"/></summary>
        Public Enum Statuses
            ''' <summary>Normal state (data are in sync)</summary>
            Normal
            ''' <summary>Null (data are not available)</summary>
            Null
            ''' <summary>New (data was null and now it is not null)</summary>
            [New]
            ''' <summary>Changed (data was changed since last save)</summary>
            Changed
            ''' <summary>Deleted (data was not null and now it is null)</summary>
            Deleted
            ''' <summary>Data error</summary>
            [Error]
            ''' <summary>Data status unknown</summary>
            NA
        End Enum
        ''' <summary>Contains value of the <see cref="Status"/> property</summary>
        Private _Status As Statuses = Statuses.Normal
        ''' <summary>Gets or sets shown status of the control</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="Statuses"/></exception>
        <DefaultValue(GetType(Statuses), "Normal")> _
        <LDescription(GetType(CompositeControls), "StatusMarkerStatus_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <Bindable(True)> _
        Public Overridable Property Status() As Statuses Implements IControlWithStatus.Status
            Get
                Return _Status
            End Get
            Set(ByVal value As Statuses)
                Select Case value
                    Case Statuses.Deleted : cmdStatus.ImageKey = "Deleted" : totToolTip.SetToolTip(cmdStatus, CompositeControls.Deleted)
                    Case Statuses.Error : cmdStatus.ImageKey = "Error" : totToolTip.SetToolTip(cmdStatus, My.Resources.Error_)
                    Case Statuses.Changed : cmdStatus.ImageKey = "Changed" : totToolTip.SetToolTip(cmdStatus, CompositeControls.Changed)
                    Case Statuses.NA : cmdStatus.ImageKey = "N/A" : totToolTip.SetToolTip(cmdStatus, CompositeControls.NA)
                    Case Statuses.[New] : cmdStatus.ImageKey = "New" : totToolTip.SetToolTip(cmdStatus, CompositeControls.New_)
                    Case Statuses.Normal : cmdStatus.ImageKey = "Normal" : totToolTip.SetToolTip(cmdStatus, CompositeControls.Normal)
                    Case Statuses.Null : cmdStatus.ImageKey = "Null" : totToolTip.SetToolTip(cmdStatus, CompositeControls.Null)
                    Case Else : Throw New InvalidEnumArgumentException("value", value, GetType(Statuses))
                End Select
                _Status = value
                OnStatusChanged(EventArgs.Empty)
            End Set
        End Property

        ''' <summary>Raised after <see cref="Status"/> changes</summary>
        <KnownCategory(KnownCategoryAttribute.AnotherCategories.PropertyChanged)> _
        <LDescription(GetType(CompositeControls), "StatusChanged_d")> _
        Public Event StatusChanged As EventHandler Implements IControlWithStatus.StatusChanged
        ''' <summary>Raises the <see cref="StatusChanged"/> event</summary>
        ''' <param name="e">Event parameters (<see cref="EventArgs.Empty"/>)</param>
        Protected Overridable Sub OnStatusChanged(ByVal e As EventArgs)
            RaiseEvent StatusChanged(Me, e)
        End Sub
        ''' <summary>Gets or set state of <see cref="tmiDelete"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Enabled")> _
        <LDescription(GetType(CompositeControls), "DeleteMenuState_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property DeleteMenuState() As ControlState Implements IControlWithStatus.DeleteMenustate
            Get
                Return MenuState(tmiDelete)
            End Get
            Set(ByVal value As ControlState)
                MenuState(tmiDelete) = value
            End Set
        End Property
        ''' <summary>Gets or set state of <see cref="tmiMarkAsChanged"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Hidden")> _
        <LDescription(GetType(CompositeControls), "MarkAsChangedMenuState_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property MarkAsChangedMenuState() As ControlState Implements IControlWithStatus.MarkAsChangedMenuState
            Get
                Return MenuState(tmiMarkAsChanged)
            End Get
            Set(ByVal value As ControlState)
                MenuState(tmiMarkAsChanged) = value
            End Set
        End Property
        ''' <summary>Gets or set state of <see cref="tmiReset"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Enabled")> _
        <LDescription(GetType(CompositeControls), "ResetMenuState_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property ResetMenuState() As ControlState Implements IControlWithStatus.ResetMenuState
            Get
                Return MenuState(tmiReset)
            End Get
            Set(ByVal value As ControlState)
                MenuState(tmiReset) = value
            End Set
        End Property
        ''' <summary>Gets or sets state of menu item</summary>
        ''' <param name="Menu">Menu item to get or set state for</param>
        Protected Property MenuState(ByVal Menu As ToolStripMenuItem) As ControlState
            Get
                Return Menu.Tag
            End Get
            Set(ByVal value As ControlState)
                With Menu
                    Select Case value
                        Case ControlState.Disabled
                            .Visible = True
                            .Enabled = False
                        Case ControlState.Enabled
                            .Visible = True
                            .Enabled = True
                        Case ControlState.Hidden
                            .Enabled = False
                            .Visible = False
                        Case Else
                            Throw New InvalidEnumArgumentException("value", value, GetType(ControlState))
                    End Select
                    .Tag = value
                End With
            End Set
        End Property
        ''' <summary>Gets or set state of <see cref="tmiAdd"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Hidden")> _
        <LDescription(GetType(CompositeControls), "AddMenuState_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property AddMenuState() As ControlState Implements IControlWithStatus.AddMenuState
            Get
                Return MenuState(tmiAdd)
            End Get
            Set(ByVal value As ControlState)
                MenuState(tmiAdd) = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="AutoChanged"/> property</summary>
        <EditorBrowsableAttribute(EditorBrowsableState.Never)> _
        Private _autoChanged As Boolean

        ''' <summary>Gets or sets value indicating if <see cref="Status"/> automatically changes to <see cref="Statuses.Changed"/> when <see cref="tmiMarkAsChanged"/> is clicked</summary>
        <DefaultValue(True)> _
        <LDescription(GetType(CompositeControls), "AutoChanged_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property AutoChanged() As Boolean Implements IControlWithStatus.AutoChanged
            Get
                Return _AutoChanged
            End Get
            Set(ByVal value As Boolean)
                _AutoChanged = value
            End Set
        End Property
        Private Sub tmiAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tmiAdd.Click
            OnAdd(e)
        End Sub

        Private Sub tmiDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tmiDelete.Click
            OnDelete(e)
        End Sub

        Private Sub tmiReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tmiReset.Click
            OnReset(e)
        End Sub
        Private Sub tmiMarkAsChanged_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tmiMarkAsChanged.Click
            OnMarkAsChanged(e)
        End Sub
        ''' <summary>Raised after <see cref="tmiAdd"/> is clicked</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(CompositeControls), "Add_d")> _
        Public Event Add As EventHandler Implements IControlWithStatus.Add
        ''' <summary>Raises the <see cref="Add"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnAdd(ByVal e As EventArgs)
            RaiseEvent Add(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="tmiMarkAsChanged"/> is clicked</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(CompositeControls), "MarkAsChanged_d")> _
        Public Event MarkAsChanged As EventHandler Implements IControlWithStatus.MarkAsChanged
        ''' <summary>Raises the <see cref="MarkAsChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnMarkAsChanged(ByVal e As EventArgs)
            If AutoChanged Then Me.Status = Statuses.Changed
            RaiseEvent MarkAsChanged(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="tmiReset"/> is clicked</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(CompositeControls), "Reset_d")> _
        Public Event Reset As EventHandler Implements IControlWithStatus.Reset
        ''' <summary>Raises the <see cref="Reset"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnReset(ByVal e As EventArgs)
            RaiseEvent Reset(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="tmiDelete"/> is clicked</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(CompositeControls), "Delete_d")> _
        Public Event Delete As EventHandler Implements IControlWithStatus.Delete
        ''' <summary>Raises the <see cref="Delete"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnDelete(ByVal e As EventArgs)
            RaiseEvent Delete(Me, e)
        End Sub

        Private Sub cmdStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdStatus.Click
            OnButtonClick()
        End Sub
        ''' <summary>Shows <see cref="cmsStatus"/></summary>
        Protected Overridable Sub OnButtonClick()
            cmsStatus.Show(cmdStatus, 0, cmdStatus.Height)
        End Sub
        ''' <summary>Gets or sets if this control automatically sizes by its content</summary>
        <DefaultValue(True), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overrides Property AutoSize() As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property

#Region "Universal"
        Private _StatusedControl As Component
        ''' <summary>Control this instance reports status of</summary>
        ''' <value>Can be null if there is no auto-monitored control</value>
        ''' <returns>Auto-monitored control or null if there is no auto-monitored control</returns>
        ''' <remarks>
        ''' <para>Supported types of StatusedControls are (ordered by precedence - if component implements <see cref="IReportsChange"/> then <see cref="IReportsChange"/> is used, otherwise <see cref="TextBoxBase"/> is checked etc.):</para>
        ''' <list type="table">
        ''' <listheader><term>Type</term><description>Depends on event</description></listheader>
        ''' <item><term><see cref="IReportsChange"/></term><description><see cref="IReportsChange.Changed"/></description></item>
        ''' <item><term><see cref="TextBoxBase"/></term><description><see cref="TextBoxBase.TextChanged"/></description></item>
        ''' <item><term><see cref="NumericUpDown"/></term><description><see cref="NumericUpDown.ValueChanged"/></description></item>
        ''' <item><term><see cref="DomainUpDown"/></term><description><see cref="DomainUpDown.SelectedItemChanged"/></description></item>
        ''' <item><term><see cref="ComboBox"/></term><description>If <see cref="ComboBox.DropDownStyle"/> is <see cref="ComboBoxStyle.DropDownList"/> <see cref="ComboBox.SelectedIndexChanged"/>; <see cref="ComboBox.TextChanged"/> otherwise</description>. Note: Do not change <see cref="ComboBox.DropDownStyle"/> property after the <see cref="ComboBox"/> is assigned to <see cref="StatusedControl"/></item>
        ''' <item><term><see cref="CheckBox"/></term><description>If <see cref="CheckBox.ThreeState"/> is True <see cref="CheckBox.CheckStateChanged"/>; <see cref="CheckBox.CheckedChanged"/> otherwise. Note: Do not change value of the <see cref="CheckBox.ThreeState"/> property after the <see cref="CheckBox"/> is assigned to <see cref="StatusedControl"/></description></item>
        ''' <item><term><see cref="RadioButton"/></term><description><see cref="RadioButton.CheckedChanged"/></description></item>
        ''' <item><term><see cref="CheckedListBox"/></term><description><see cref="CheckedListBox.ItemCheck"/></description></item>
        ''' <item><term><see cref="MonthCalendar"/></term><description><see cref="MonthCalendar.DateChanged"/></description></item>
        ''' <item><term><see cref="DateTimePicker"/></term><description><see cref="DateTimePicker.ValueChanged"/></description></item>
        ''' <item><term><see cref="TrackBar"/></term><description><see cref="TrackBar.ValueChanged"/></description></item>
        ''' <item><term><see cref="ScrollBar"/></term><description><see cref="ScrollBar.ValueChanged"/></description></item>
        ''' <item><term><see cref="ToolStripTextBox"/></term><description><see cref="ToolStripTextBox.TextChanged"/></description></item>
        ''' <item><term><see cref="ToolStripComboBox"/></term><description>If <see cref="ToolStripComboBox.DropDownStyle"/> is <see cref="ComboBoxStyle.DropDownList"/> <see cref="ToolStripComboBox.SelectedIndexChanged"/>; <see cref="ToolStripComboBox.TextChanged"/> otherwise</description>. Note: Do not change <see cref="ToolStripComboBox.DropDownStyle"/> property after the <see cref="ToolStripComboBox"/> is assigned to <see cref="StatusedControl"/></item>
        ''' <item><term><see cref="ToolStripButton"/></term><description><see cref="ToolStripButton.CheckedChanged"/></description></item>
        ''' <item><term><see cref="ToolStripMenuItem"/></term><description><see cref="ToolStripMenuItem.CheckedChanged"/></description></item>
        ''' </list>
        ''' </remarks>
        ''' <exception cref="NotSupportedException">Type of passed component is not supported</exception>
        Public Overridable Property StatusedControl() As Component
            Get
                Return _StatusedControl
            End Get
            Set(ByVal value As Component)
                [RemoveHandler]()
                If value IsNot Nothing Then
                    Try
                        [AddHandler](value)
                    Catch
                        [AddHandler](StatusedControl)
                        Throw
                    End Try
                End If
                _StatusedControl = value
            End Set
        End Property

        ''' <summary>Removes method <see cref="Handler"/> as hendler of event of <see cref="StatusedControl"/> where it was added in <see cref="[AddHandler]"/> method</summary>
        ''' <exception cref="NotSupportedException">Type of <see cref="StatusedControl"/> is not supported. See <seealso cref="StatusedControl"/> for list of supported types.</exception>
        ''' <remarks>Note for inheritors: You can extedn <see cref="StatusMarker"/>'s support of statused controls either by overriding <see cref="[AddHandler]"/> and <see cref="[RemoveHandler]"/> methods or by implementing <see cref="IReportsChange"/> by the control that should be supported</remarks>
        Protected Overridable Sub [RemoveHandler]()
            If StatusedControl Is Nothing Then Return
            Dim Component As Component = Me.StatusedControl
            If TypeOf Component Is IReportsChange Then
                RemoveHandler DirectCast(Component, IReportsChange).Changed, AddressOf Handler
            ElseIf TypeOf Component Is TextBoxBase Then
                RemoveHandler DirectCast(Component, TextBoxBase).TextChanged, AddressOf Handler
            ElseIf TypeOf Component Is NumericUpDown Then
                RemoveHandler DirectCast(Component, NumericUpDown).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is DomainUpDown Then
                RemoveHandler DirectCast(Component, DomainUpDown).SelectedItemChanged, AddressOf Handler
            ElseIf TypeOf Component Is ComboBox Then
                With DirectCast(Component, ComboBox)
                    If .DropDownStyle = ComboBoxStyle.DropDownList Then
                        RemoveHandler .TextChanged, AddressOf Handler
                    Else
                        RemoveHandler .SelectedIndexChanged, AddressOf Handler
                    End If
                End With
            ElseIf TypeOf Component Is CheckBox Then
                With DirectCast(Component, CheckBox)
                    If .ThreeState Then
                        RemoveHandler .CheckStateChanged, AddressOf Handler
                    Else
                        RemoveHandler .CheckedChanged, AddressOf Handler
                    End If
                End With
            ElseIf TypeOf Component Is RadioButton Then
                RemoveHandler DirectCast(Component, RadioButton).CheckedChanged, AddressOf Handler
            ElseIf TypeOf Component Is CheckedListBox Then
                RemoveHandler DirectCast(Component, CheckedListBox).ItemCheck, AddressOf Handler
            ElseIf TypeOf Component Is MonthCalendar Then
                RemoveHandler DirectCast(Component, MonthCalendar).DateChanged, AddressOf Handler
            ElseIf TypeOf Component Is DateTimePicker Then
                RemoveHandler DirectCast(Component, DateTimePicker).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is TrackBar Then
                RemoveHandler DirectCast(Component, TrackBar).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is ScrollBar Then
                RemoveHandler DirectCast(Component, ScrollBar).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is ToolStripTextBox Then
                RemoveHandler DirectCast(Component, ToolStripTextBox).TextChanged, AddressOf Handler
            ElseIf TypeOf Component Is ToolStripComboBox Then
                With DirectCast(Component, ToolStripComboBox)
                    If .DropDownStyle = ComboBoxStyle.DropDownList Then
                        RemoveHandler .TextChanged, AddressOf Handler
                    Else
                        RemoveHandler .SelectedIndexChanged, AddressOf Handler
                    End If
                End With
            ElseIf TypeOf Component Is ToolStripButton Then
                RemoveHandler DirectCast(Component, ToolStripButton).CheckedChanged, AddressOf Handler
            ElseIf TypeOf Component Is ToolStripMenuItem Then
                RemoveHandler DirectCast(Component, ToolStripMenuItem).CheckedChanged, AddressOf Handler
            Else
                Throw New NotSupportedException(String.Format(Type0IsNotSupportedAsValueOfStatusedControlProperty, Component.GetType.FullName))
            End If
        End Sub
        ''' <summary>Adds method <see cref="Handler"/> as handler of appropriate event of given component</summary>
        ''' <param name="Component">Component to add handler to</param>
        ''' <remarks>Note for inheritors: You can extedn <see cref="StatusMarker"/>'s support of statused controls either by overriding <see cref="[AddHandler]"/> and <see cref="[RemoveHandler]"/> methods or by implementing <see cref="IReportsChange"/> by the control that should be supported</remarks>
        ''' <exception cref="NotSupportedException">Type of <paramref name="Component"/> is not supported. See <seealso cref="StatusedControl"/> for list of supported types.</exception>
        Protected Overridable Sub [AddHandler](ByVal Component As Component)
            If TypeOf Component Is IReportsChange Then
                'TODO: All custom controls in Tools that contains value should implement IReportsChange
                AddHandler DirectCast(Component, IReportsChange).Changed, AddressOf Handler
            ElseIf TypeOf Component Is TextBoxBase Then
                AddHandler DirectCast(Component, TextBoxBase).TextChanged, AddressOf Handler
            ElseIf TypeOf Component Is NumericUpDown Then
                AddHandler DirectCast(Component, NumericUpDown).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is DomainUpDown Then
                AddHandler DirectCast(Component, DomainUpDown).SelectedItemChanged, AddressOf Handler
            ElseIf TypeOf Component Is ComboBox Then
                With DirectCast(Component, ComboBox)
                    If .DropDownStyle = ComboBoxStyle.DropDownList Then
                        AddHandler .TextChanged, AddressOf Handler
                    Else
                        AddHandler .SelectedIndexChanged, AddressOf Handler
                    End If
                End With
            ElseIf TypeOf Component Is CheckBox Then
                With DirectCast(Component, CheckBox)
                    If .ThreeState Then
                        AddHandler .CheckStateChanged, AddressOf Handler
                    Else
                        AddHandler .CheckedChanged, AddressOf Handler
                    End If
                End With
            ElseIf TypeOf Component Is RadioButton Then
                AddHandler DirectCast(Component, RadioButton).CheckedChanged, AddressOf Handler
            ElseIf TypeOf Component Is CheckedListBox Then
                AddHandler DirectCast(Component, CheckedListBox).ItemCheck, AddressOf Handler
            ElseIf TypeOf Component Is MonthCalendar Then
                AddHandler DirectCast(Component, MonthCalendar).DateChanged, AddressOf Handler
            ElseIf TypeOf Component Is DateTimePicker Then
                AddHandler DirectCast(Component, DateTimePicker).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is TrackBar Then
                AddHandler DirectCast(Component, TrackBar).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is ScrollBar Then
                AddHandler DirectCast(Component, ScrollBar).ValueChanged, AddressOf Handler
            ElseIf TypeOf Component Is ToolStripTextBox Then
                AddHandler DirectCast(Component, ToolStripTextBox).TextChanged, AddressOf Handler
            ElseIf TypeOf Component Is ToolStripComboBox Then
                With DirectCast(Component, ToolStripComboBox)
                    If .DropDownStyle = ComboBoxStyle.DropDownList Then
                        AddHandler .TextChanged, AddressOf Handler
                    Else
                        AddHandler .SelectedIndexChanged, AddressOf Handler
                    End If
                End With
            ElseIf TypeOf Component Is ToolStripButton Then
                AddHandler DirectCast(Component, ToolStripButton).CheckedChanged, AddressOf Handler
            ElseIf TypeOf Component Is ToolStripMenuItem Then
                AddHandler DirectCast(Component, ToolStripMenuItem).CheckedChanged, AddressOf Handler
            Else
                Throw New NotSupportedException(String.Format(Type0IsNotSupportedAsValueOfStatusedControlProperty, Component.GetType.FullName))
            End If
        End Sub
        ''' <summary>Handles change of value of monitored control</summary>
        ''' <param name="sender">Source of event - should be same as <see cref="StatusedControl"/></param>
        ''' <param name="e">Event arguments repored by <paramref name="sender"/></param>
        ''' <typeparam name="TSender">Type of sender (typically <see cref="Object"/>)</typeparam>
        ''' <typeparam name="TEventargs">Type of event arguments (typically <see cref="EventArgs"/>)</typeparam>
        ''' <remarks>This method is generic because it is required for handlers to work. It does not rely on exact types of <paramref name="sender"/> amd <paramref name="e"/>.</remarks>
        Protected Overridable Sub Handler(Of TSender, TEventargs As EventArgs)(ByVal sender As TSender, ByVal e As TEventargs)
            'TODO: Implement
        End Sub
#End Region
    End Class
    ''' <summary>Provides common interface for controls that exposes its status</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">�onny</author>
   ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IControlWithStatus
        ''' <summary>Current status of control</summary>
        Property Status() As StatusMarker.Statuses
        ''' <summary>State of delete menu item</summary>
        Property DeleteMenustate() As ControlState
        ''' <summary>State of mar-as-changed menu item</summary>
        Property MarkAsChangedMenuState() As ControlState
        ''' <summary>state of reset menu item</summary>
        Property ResetMenuState() As ControlState
        ''' <summary>state of add menu item</summary>
        Property AddMenuState() As ControlState
        ''' <summary>Indicates if <see cref="Status"/> automatically chnages to <see cref="StatusMarker.Statuses.Changed"/> and <see cref="StatusMarker.Statuses.[New]"/> if user takes appropriate action</summary>
        Property AutoChanged() As Boolean
        ''' <summary>Raised after add menu item is clicked</summary>
        Event Add As EventHandler
        ''' <summary>Raised after mark-as-changed menu item is clicked</summary>
        Event MarkAsChanged As EventHandler
        ''' <summary>Raised after reset menu item is clicked</summary>
        Event Reset As EventHandler
        ''' <summary>Raised after delete menu item is clcked</summary>
        Event Delete As EventHandler
        ''' <summary>Raised after <see cref="Status"/> is changed</summary>
        Event StatusChanged As EventHandler
    End Interface
End Namespace
