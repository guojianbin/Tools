﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.42
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace ComponentModel
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")>  _
    Partial Friend NotInheritable Class SettingsAttributesTestSettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As SettingsAttributesTestSettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New SettingsAttributesTestSettings),SettingsAttributesTestSettings)
        
        Public Shared ReadOnly Property [Default]() As SettingsAttributesTestSettings
            Get
                Return defaultInstance
            End Get
        End Property
        
        '''<summary>
        '''This is setting wizh default value &quot;Test&quot;
        '''</summary>
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsDescriptionAttribute("This is setting wizh default value ""Test"""),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Test")>  _
        Public Property TestSetting1() As String
            Get
                Return CType(Me("TestSetting1"),String)
            End Get
            Set
                Me("TestSetting1") = value
            End Set
        End Property
        
        '''<summary>
        '''This is setting with default value 14;23
        '''</summary>
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsDescriptionAttribute("This is setting with default value 14;23"),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("14, 23")>  _
        Public Property TestSetting2() As Global.System.Drawing.Point
            Get
                Return CType(Me("TestSetting2"),Global.System.Drawing.Point)
            End Get
            Set
                Me("TestSetting2") = value
            End Set
        End Property
        
        '''<summary>
        '''This is setting with default value {First value; Second Value; Third value}
        '''</summary>
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsDescriptionAttribute("This is setting with default value {First value; Second Value; Third value}"),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<ArrayOfString xmlns:xsi=""http://www.w3."& _ 
            "org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <s"& _ 
            "tring>First value</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <string>Second value</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <string>Third val"& _ 
            "ue</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"</ArrayOfString>")>  _
        Public Property TestSetting3() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("TestSetting3"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("TestSetting3") = value
            End Set
        End Property
    End Class
End Namespace
