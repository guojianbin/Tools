﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.0
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Tools.DataT.SchemaT.DeployT.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to {0} cannot be an empty string.
        '''</summary>
        Friend ReadOnly Property err_CannotBeEmptyString() As String
            Get
                Return ResourceManager.GetString("err_CannotBeEmptyString", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Value of the {0} property cannot be changed once deployment attempt has been done..
        '''</summary>
        Friend ReadOnly Property err_CannotChangeValueOnceDeployed() As String
            Get
                Return ResourceManager.GetString("err_CannotChangeValueOnceDeployed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to The database schema file {0} does not exist.
        '''</summary>
        Friend ReadOnly Property err_DatabaseSchemaFileDoesNotExist() As String
            Get
                Return ResourceManager.GetString("err_DatabaseSchemaFileDoesNotExist", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to An error occured while deploying the database..
        '''</summary>
        Friend ReadOnly Property err_Deploy() As String
            Get
                Return ResourceManager.GetString("err_Deploy", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Failed to set some properties - {0}.
        '''</summary>
        Friend ReadOnly Property err_FailedSetProperties() As String
            Get
                Return ResourceManager.GetString("err_FailedSetProperties", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Manifest file not found.
        '''</summary>
        Friend ReadOnly Property err_ManifestFileNotFound() As String
            Get
                Return ResourceManager.GetString("err_ManifestFileNotFound", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to This object has already been disposed.
        '''</summary>
        Friend ReadOnly Property err_ObjectDisposed() As String
            Get
                Return ResourceManager.GetString("err_ObjectDisposed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Database schema provider {0} does not support schema file version &apos;{1}&apos;..
        '''</summary>
        Friend ReadOnly Property err_ProviderVsSchemaVersionConflict() As String
            Get
                Return ResourceManager.GetString("err_ProviderVsSchemaVersionConflict", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to This instance has already been used to deploy a database..
        '''</summary>
        Friend ReadOnly Property err_UsedInstanceUsedAgain() As String
            Get
                Return ResourceManager.GetString("err_UsedInstanceUsedAgain", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
