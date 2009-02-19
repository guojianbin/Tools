﻿''' <summary>Builds a plugin</summary>
Module Builder
    ''' <summary>Builds a plugin</summary>
    Sub Main()
        If My.Application.CommandLineArgs.Count < 0 Then
            Console.WriteLine(My.Resources.Usage, My.Application.Info.AssemblyName)
            Exit Sub
        End If
        'Parameters
        Dim Assembly = My.Application.CommandLineArgs(0)
        Dim OutDir$
        If My.Application.CommandLineArgs.Contains("/out") AndAlso My.Application.CommandLineArgs.IndexOf("/out") < My.Application.CommandLineArgs.Count - 1 Then
            OutDir = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/out") + 1)
        Else
            OutDir = IO.Path.GetDirectoryName(Assembly)
        End If
        Dim TypeNames As New List(Of String)
        For i As Integer = 0 To My.Application.CommandLineArgs.Count - 2
            If My.Application.CommandLineArgs(i) = "/t" Then
                TypeNames.Add(My.Application.CommandLineArgs(i + 1))
            End If
        Next
        Dim PluginTypes As PluginType = PluginType.Content Or PluginType.FileSystem Or PluginType.Lister Or PluginType.Packer
        If My.Application.CommandLineArgs.Contains("/-wcx") Then PluginTypes = PluginTypes And Not PluginType.Packer
        If My.Application.CommandLineArgs.Contains("/-wdx") Then PluginTypes = PluginTypes And Not PluginType.Content
        If My.Application.CommandLineArgs.Contains("/-wfx") Then PluginTypes = PluginTypes And Not PluginType.FileSystem
        If My.Application.CommandLineArgs.Contains("/-wlx") Then PluginTypes = PluginTypes And Not PluginType.Lister
        Dim NamingDictionary As New Dictionary(Of String, String)
        For i As Integer = 0 To My.Application.CommandLineArgs.Count - 3
            If My.Application.CommandLineArgs(i) = "/n" Then
                If NamingDictionary.ContainsKey(My.Application.CommandLineArgs(i + 1)) Then
                    Console.WriteLine(My.Resources.e_DuplicitRenameType, My.Application.CommandLineArgs(i + 1))
                    Environment.Exit(3)
                    End
                End If
                If NamingDictionary.ContainsValue(My.Application.CommandLineArgs(i + 2)) Then
                    Console.WriteLine(My.Resources.e_DuplicitRenameName, My.Application.CommandLineArgs(i + 2))
                    Environment.Exit(3)
                    End
                End If
                NamingDictionary.Add(My.Application.CommandLineArgs(i + 1), My.Application.CommandLineArgs(i + 2))
            End If
        Next
        Dim IntDir$ = Nothing
        If My.Application.CommandLineArgs.IndexOf("/int") < My.Application.CommandLineArgs.Count - 1 Then IntDir = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/int") + 1)
        Dim KeepInt As Boolean = IntDir IsNot Nothing AndAlso My.Application.CommandLineArgs.Contains("/keepint")
        Dim TemplateDir$ = Nothing
        If My.Application.CommandLineArgs.IndexOf("/templ") < My.Application.CommandLineArgs.Count - 1 Then TemplateDir = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/templ") + 1)
        'Load assembly
        Dim LoadedAssembly As System.Reflection.Assembly
        Try
            LoadedAssembly = Reflection.Assembly.LoadFile(Assembly)
        Catch ex As Exception
            Console.WriteLine(My.Resources.e_LoadAssembly, ex.Message)
            Environment.Exit(1)
            End
        End Try
        'Load types
        Dim Types As Type() = Nothing
        If TypeNames.Count > 0 Then
            ReDim Types(TypeNames.Count - 1)
            For i As Integer = 0 To TypeNames.Count - 1
                Try
                    Types(i) = LoadedAssembly.GetType(TypeNames(i), True)
                Catch ex As Exception
                    Console.WriteLine(My.Resources.e_LoadType, TypeNames(i), ex.Message)
                    Environment.Exit(2)
                    End
                End Try
            Next
        End If
        'Prepare generator
        Dim Generator As New Generator(LoadedAssembly, OutDir)
        Generator.Filer = PluginTypes
        For Each item In NamingDictionary
            Generator.RenamingDictionary.Add(item.Key, item.Value)
        Next
        Generator.IntermediateDirectory = IntDir
        Generator.CleanIntermediateDirectory = Not KeepInt
        Generator.ProjectTemplateDirectory = TemplateDir

        'Generate
        Try
            Generator.Generate()
        Catch ex As Exception
            Console.WriteLine(My.Resources.e_Generating, ex.Message)
            Environment.Exit(10)
            End
        End Try
    End Sub

End Module