Imports System.Drawing.Design, System.Windows.Forms, System.Drawing

Namespace DrawingT.DesignT
    ''' <summary><see cref="UITypeEditor"/> capable of creating new instance either from <see cref="DefaultValueAttribute"/> (preffered if available and <see cref="DefaultValueAttribute.Value"/> is not null) or by parameterless CTor</summary>
    ''' <remarks>
    ''' The <see cref="DefaultValueAttribute"/> used can be applyed either on property (preffered) or on type of the property.
    ''' See also <seealso cref="InstanceCreationEditor"/>.
    ''' </remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">�onny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class NewEditor
        Inherits UITypeEditor
        ''' <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.</summary>
        ''' <returns><see cref="UITypeEditorEditStyle.DropDown"/></returns>
        Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
        End Function

        ''' <summary>Edits the specified object's value using the editor style indicated by the <see cref="System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information.</param>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        ''' <returns>New value of type of property  obtained either via <see cref="DefaultValueAttribute"/> or via default CTor</returns>
        Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
            Using Lbl As New Label
                Lbl.BackColor = SystemColors.Window
                Lbl.ForeColor = SystemColors.WindowText
                Lbl.Text = ResourcesT.Components.[New___]
                Lbl.Tag = context
                AddHandler Lbl.Click, AddressOf Lbl_Click
                Me.context = context
                service = provider.GetService(GetType(System.Windows.Forms.Design.IWindowsFormsEditorService))
                service.DropDownControl(Lbl)
                If Lbl.Tag Is Nothing Then Return value Else Return Lbl.Tag
            End Using
        End Function

        ''' <summary>The context parameter of <see cref="EditValue"/> used by <see cref="Lbl_Click"/></summary>
        Private context As ITypeDescriptorContext
        ''' <summary>service obtained from provider parameter of <see cref="EditValue"/> used by <see cref="Lbl_Click"/></summary>
        Private service As System.Windows.Forms.Design.IWindowsFormsEditorService

        ''' <summary>Handles <see cref="Label.Click"/> event of label used to provide drop-down UI</summary>
        ''' <param name="sender">The <see cref="Label"/></param>
        ''' <param name="e">Event params</param>
        Private Sub Lbl_Click(ByVal sender As Object, ByVal e As [EventArgs])
            With DirectCast(sender, Label)
                Try
                    Dim dva As DefaultValueAttribute = context.PropertyDescriptor.Attributes(GetType(DefaultValueAttribute))
                    If dva Is Nothing Then Try : dva = context.PropertyDescriptor.PropertyType.GetCustomAttributes(GetType(DefaultValueAttribute), True)(0) : Catch : End Try
                    If dva IsNot Nothing AndAlso dva.Value IsNot Nothing Then
                        .Tag = dva.Value
                    Else
                        .Tag = Activator.CreateInstance(context.PropertyDescriptor.PropertyType)
                    End If
                Finally
                    service.CloseDropDown()
                End Try
            End With
        End Sub
    End Class
End Namespace