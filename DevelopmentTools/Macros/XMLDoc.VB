Imports EnvDTE
Imports EnvDTE80
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Linq
Imports System.Xml.Linq

''' <summary>XML comments tools</summary>
Public Module XMLDoc
    Private Const VB$ = "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}"
    Private Const CSharp = "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}"

#Region "Macro procedures"
    ''' <summary>Paste XML comment. In clipboard must be string formated as object browser does</summary>
    ''' <version version="1.5.4">Clipboard string now expects "Return Values" or "Returns" as header of returns part (previously only "Returns")</version> 
    Public Sub PasteXMLDoc()
        Dim xml = XMLCommentFromObjectBrowser(GetClipboardText)
        If XML IsNot Nothing AndAlso XML.Trim <> "" Then
            XML = XML.Trim
            DTE.UndoContext.Open("Paste XML doc")
            Try
                Dim selection As EnvDTE.TextSelection = CType(DTE.ActiveWindow.Selection, TextSelection)
                selection.Text = ""
                Dim editPoint As EnvDTE.EditPoint = selection.ActivePoint.CreateEditPoint
                EditPoint.Insert(XML)
            Finally
                DTE.UndoContext.Close()
            End Try
        End If
    End Sub
    ''' <summary>Inserts short XML comment - &lt;summary>only</summary>
    Public Sub XMLSummary()
        Dim selection As EnvDTE.TextSelection = CType(DTE.ActiveWindow.Selection, TextSelection)
        Dim st = selection.Text
        DTE.UndoContext.Open("XML <summary>")
        Try
            selection.Text = ""
            selection.LineDown()
            selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
            Dim col = selection.TopPoint.LineCharOffset
            selection.LineUp()
            selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn)
            selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, True)
            selection.Text = ""
            Dim editPoint As EnvDTE.EditPoint = selection.ActivePoint.CreateEditPoint
            EditPoint.Insert(New String(" "c, col - 1) & XMLCommentStart() & "<summary>" & st & "</summary>")
            'selection = DTE.ActiveWindow.Selection
            selection.CharLeft(False, "</summary>".Length + st.Length)
            selection.CharRight(True, st.Length)
        Finally
            DTE.UndoContext.Close()
        End Try
    End Sub
    ''' <summary>Creates XML comment for item under text cursor</summary>
    Public Sub XMLDoc_Long()
        Const XMLSummary$ = "<summary></summary>"
        Const XMLParam$ = "<param name=""{0}""></param>"
        Const XMLValue$ = "<value></value>"
        Const XMLReturns$ = "<returns></returns>"
        Const XMLTypeParam$ = "<typeparam name=""{0}""></typeparam>"
        Dim selection As EnvDTE.TextSelection
        Dim editPoint As EnvDTE.EditPoint
        selection = CType(DTE.ActiveWindow.Selection, TextSelection)
        editPoint = selection.ActivePoint.CreateEditPoint
        Dim ce As CodeElement = editPoint.GetCodeElement()
        If ce IsNot Nothing Then
            DTE.UndoContext.Open("XML comment")
            Try
                Dim sb As New System.Text.StringBuilder(XMLSummary & vbCrLf)
                Select Case ce.Kind
                    Case vsCMElement.vsCMElementClass 'Class
                        With CType(ce, CodeClass2)
                            If (.IsGeneric) Then
                                'Dim ex = .Extender("VBGenericExtender") does not work
                                For Each GP In vbParseGeneric(.FullName)
                                    sb.AppendLine(String.Format(XMLTypeParam, GP))
                                Next GP
                            End If
                            .DocComment = sb.ToString.Trim
                        End With
                    Case vsCMElement.vsCMElementInterface 'Interface
                        With CType(ce, CodeInterface2)
                            If (.IsGeneric) Then
                                If .Language = VB Then
                                    'Dim ex = .Extender("VBGenericExtender") does not work
                                    For Each GP In vbParseGeneric(.FullName)
                                        sb.AppendLine(String.Format(XMLTypeParam, GP))
                                    Next GP
                                End If
                            End If
                            .DocComment = sb.ToString.Trim
                        End With
                    Case vsCMElement.vsCMElementStruct 'Structure
                        With CType(ce, CodeStruct2)
                            If (.IsGeneric) Then
                                If .Language = VB Then
                                    'Dim ex = .Extender("VBGenericExtender") does not work
                                    For Each GP In vbParseGeneric(.FullName)
                                        sb.AppendLine(String.Format(XMLTypeParam, GP))
                                    Next GP
                                End If
                            End If
                            .DocComment = sb.ToString.Trim
                        End With
                    Case vsCMElement.vsCMElementDelegate 'Delegate
                        With CType(ce, CodeDelegate2)
                            For i = 1 To .Parameters.Count
                                sb.AppendLine(String.Format(XMLParam, .Parameters.Item(i).Name))
                            Next i
                            If (.IsGeneric) Then
                                If .Language = VB Then
                                    'Dim ex = .Extender("VBGenericExtender") does not work
                                    For Each GP In vbParseGeneric(.FullName)
                                        sb.AppendLine(String.Format(XMLTypeParam, GP))
                                    Next GP
                                End If
                            End If
                            If .Type.TypeKind <> vsCMTypeRef.vsCMTypeRefVoid Then _
                                sb.AppendLine(XMLReturns)
                            .DocComment = sb.ToString.Trim
                        End With
                    Case vsCMElement.vsCMElementEnum 'Enum
                        CType(ce, CodeEnum).DocComment = sb.ToString.Trim
                        'Case vsCMElement.vsCMElementModule
                        'With CType(ce,codemo
                    Case vsCMElement.vsCMElementVariable 'Dim
                        CType(ce, CodeVariable2).DocComment = sb.ToString.Trim
                    Case vsCMElement.vsCMElementEvent 'Event
                        With CType(ce, CodeEvent)
                            If .Language = VB Then
                                For Each Param In vbParseEventHeader(.StartPoint.CreateEditPoint.GetText(.EndPoint))
                                    sb.AppendLine(String.Format(XMLParam, Param))
                                Next Param
                            End If
                            .DocComment = sb.ToString.Trim
                        End With
                    Case vsCMElement.vsCMElementFunction 'Sub, Function
                        With CType(ce, CodeFunction2)
                            For i = 1 To .Parameters.Count
                                sb.AppendLine(String.Format(XMLParam, .Parameters.Item(i).Name))
                            Next i
                            If .Language = VB Then 'VB
                                If (.IsGeneric) Then
                                    'Dim ex = .Extender("VBGenericExtender") does not work
                                    For Each GP In vbParseGeneric(.FullName)
                                        sb.AppendLine(String.Format(XMLTypeParam, GP))
                                    Next GP
                                End If
                            End If
                            If .Type.TypeKind <> vsCMTypeRef.vsCMTypeRefVoid Then _
                                sb.AppendLine(XMLReturns)
                            Dim value = sb.ToString.Trim
                            If .Language = CSharp Then value = String.Format("<doc>{0}</doc>", value)
                            .DocComment = value
                        End With
                    Case vsCMElement.vsCMElementProperty 'Property
                        With CType(ce, CodeProperty2)
                            For i = 1 To .Children.Count
                                If .Children.Item(i).Kind = vsCMElement.vsCMElementParameter Then _
                                  sb.AppendLine(String.Format(XMLParam, CType(.Children.Item(i), CodeParameter).Name))
                            Next i
                            If .Setter IsNot Nothing Then
                                sb.AppendLine(XMLValue)
                            End If
                            If .Getter IsNot Nothing Then
                                sb.AppendLine(XMLReturns)
                            End If
                            .DocComment = sb.ToString.Trim
                        End With
                End Select
                selection.MoveToPoint(ce.GetStartPoint(vsCMPart.vsCMPartWholeWithAttributes))
                selection.LineUp(False, sb.ToString.Trim.Split(New String() {vbCrLf}, StringSplitOptions.None).Length)
                selection.CharRight(False, Utilities.XMLCommentStart.Length + "<summary>".Length)
            Finally
                DTE.UndoContext.Close()
            End Try
        End If
    End Sub
    ''' <summary>Copies XML doc from member that is implemented or overridden by given meber</summary>
    ''' <version version="1.5.3">Adddes support for members of structures</version>
    Public Sub InheritXMLDoc()
        Dim selection As EnvDTE.TextSelection = TryCast(DTE.ActiveWindow.Selection, TextSelection)
        If selection Is Nothing Then Return
        Dim editPoint = selection.ActivePoint.CreateEditPoint
        Dim ce1 = EditPoint.GetCodeElement()
        If ce1 Is Nothing Then Exit Sub
        Dim ce As CodeElement2 = CType(ce1, CodeElement2)
        If ce IsNot Nothing Then
            Select Case ce.Kind
                Case vsCMElement.vsCMElementEvent, vsCMElement.vsCMElementProperty, vsCMElement.vsCMElementFunction
                    Dim hintText$ = Nothing
                    Try
                        hintText = selection.Text
                    Catch : End Try
                    InheritXMLDoc(ce, hintText)
            End Select
        End If
    End Sub
#End Region
    ''' <summary>Helper enum for automaton in <see cref="vbParseGeneric"/></summary>
    Private Enum GState
        Start
        Open
        O
        [Of]
        Of_
        Of_T
        Of_T_
        Of_T_C
        Of_T_A
        Of_T_As
        Of_T_As_
        Of_T_As_O
        Of_T_As_T
        Of_T_C_O
        Of_T_C_O_O
        Of_T_C_O_Of
    End Enum
    ''' <summary>Parses VB-style header of generic something</summary>
    ''' <param name="name">Header text in form: whatever you want(Of Param As SimpleConstraint, Param2 As {Multiple,Constraints})</param>
    ''' <returns>Names of parameters</returns>
    Private Function vbParseGeneric(ByVal name As String) As List(Of String)
        Dim state = GState.Start
        Dim pName = ""
        Dim ret As New List(Of String)
        For Each ch As Char In Name
            Select Case state
                Case GState.Start
                    Select Case ch
                        Case "("c : state = GState.Open
                    End Select
                Case GState.Open
                    Select Case ch
                        Case "O"c : state = GState.O
                        Case Else : state = GState.Start
                    End Select
                Case GState.O
                    Select Case ch
                        Case "f"c : state = GState.Of
                        Case Else : state = GState.Start
                    End Select
                Case GState.Of
                    Select Case ch
                        Case " "c : state = GState.Of_ : ret.Clear()
                        Case Else : state = GState.Start
                    End Select
                Case GState.Of_
                    Select Case ch
                        Case ")"c : state = GState.Start
                        Case Else : PName = ch : state = GState.Of_T
                    End Select
                Case GState.Of_T
                    Select Case ch
                        Case " "c : ret.Add(PName) : state = GState.Of_T_
                        Case ","c : ret.Add(PName) : state = GState.Of_
                        Case ")"c : ret.Add(PName) : state = GState.Of_T_C
                        Case Else : PName &= ch
                    End Select
                Case GState.Of_T_
                    Select Case ch
                        Case "A"c, "a"c : state = GState.Of_T_A
                        Case " "c 'Do nothing
                        Case ","c : state = GState.Of_
                        Case ")"c : state = GState.Of_T_C
                    End Select
                Case GState.Of_T_A
                    Select Case ch
                        Case "s"c, "S"c : state = GState.Of_T_As
                        Case Else : state = GState.Of_T_As_T
                    End Select
                Case GState.Of_T_As
                    Select Case ch
                        Case " "c : state = GState.Of_T_As_
                        Case "{"c : state = GState.Of_T_As_O
                        Case Else : state = GState.Of_T_As_T
                    End Select
                Case GState.Of_T_As_
                    Select Case ch
                        Case " "c 'Do nothing
                        Case "{"c : state = GState.Of_T_As_O
                        Case Else : state = GState.Of_T_As_T
                    End Select
                Case GState.Of_T_As_O
                    Select Case ch
                        Case "}"c : state = GState.Of_T_As_T
                    End Select
                Case GState.Of_T_As_T
                    Select Case ch
                        Case ","c : state = GState.Of_T_As
                        Case ")"c : state = GState.Of_T_C
                    End Select
                Case GState.Of_T_C
                    Select Case ch
                        Case "("c : state = GState.Of_T_C_O
                    End Select
                Case GState.Of_T_C_O
                    Select Case ch
                        Case "O"c, "o"c : state = GState.Of_T_C_O_O
                        Case Else : state = GState.Of_T_C
                    End Select
                Case GState.Of_T_C_O_O
                    Select Case ch
                        Case "f"c, "F"c : state = GState.Of_T_C_O_Of
                        Case Else : state = GState.Of_T_C
                    End Select
                Case GState.Of_T_C_O_Of
                    Select Case ch
                        Case " "c : state = GState.Of_ : ret.Clear()
                        Case Else : state = GState.Of_T_C
                    End Select
            End Select
        Next ch
        Return ret
    End Function
    ''' <summary>Regular expression that parses VB event heder for <see cref="vbParseEventHeader"/></summary>
    ''' <remarks>Not universal - generic types can be nested 2 levels</remarks>
    Private VBEventHeaderRegEx As New Text.RegularExpressions.Regex("^\s*Event\s+(_|\p{L})(_|\p{L}|\p{N})*\s*\(\s*((ByVal|ByRef)\s+((?<Name>(_|\p{L})(_|\p{L}|\p{N})*)(\s+As\s+(_|\p{L})(_|\p{L}|\p{N})*(\s*\(Of\s+((_|\p{L})(_|\p{L}|\p{N})*(\s*\(Of\s+((_|\p{L})(_|\p{L}|\p{N})*\s*,?)+\s*\))?\s*,?)+\s*\))?)\s*,?\s*))*\s*\)\s*$", Text.RegularExpressions.RegexOptions.Compiled Or Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace Or Text.RegularExpressions.RegexOptions.CultureInvariant Or Text.RegularExpressions.RegexOptions.ExplicitCapture)
    ''' <summary>Parses string used as header of event in visual basic - long form - without delegate</summary>
    ''' <param name="h">Header string. sth. like Event e(sender As Object)</param>
    ''' <returns>Names of parameters</returns>
    Private Function vbParseEventHeader(ByVal h As String) As List(Of String)
        Dim ret As New List(Of String)
        Dim match = VBEventHeaderRegEx.Match(h)
        If match.Success Then
            For Each capt As Text.RegularExpressions.Capture In match.Groups("Name").Captures
                ret.Add(capt.Value)
            Next capt
        End If
        Return ret
    End Function
    ''' <summary>Gets element that starts at given line of code.</summary>
    ''' <param name="p">Determines current line</param>
    ''' <returns><see cref="CodeElement"/> that starts on current line or null</returns>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    <Extension(), CLSCompliant(False)>
    Public Function GetCodeElement(ByVal p As EnvDTE.EditPoint) As EnvDTE.CodeElement
        For Each t As vsCMElement In New vsCMElement() {vsCMElement.vsCMElementFunction, vsCMElement.vsCMElementEvent, vsCMElement.vsCMElementProperty}
            Try
                Dim ce As CodeElement = p.CodeElement(t)
                If CE.GetStartPoint(vsCMPart.vsCMPartWholeWithAttributes).Line = p.Line OrElse CE.GetStartPoint(vsCMPart.vsCMPartWhole).Line = p.Line Then _
                    Return CE
            Catch : End Try
        Next t
        Return Nothing
    End Function
    ''' <summary>Helper function: Works as val++ operator</summary>
    ''' <param name="val">Value to increment</param>
    ''' <returns><paramref name="vale"/> before incrementation</returns>
    Private Function PostNot(ByRef val As Boolean) As Boolean
        Try : Return val
        Finally : val = Not val
        End Try
    End Function
    ''' <summary>Replaces references in text with &lt;see cref=""/> XML comment tag</summary>
    ''' <param name="txt">String to replace in</param>
    ''' <returns>XML taht represens replaced text</returns>
    Private Function SeeReplace(ByVal txt$) As IEnumerable(Of Object)
        Dim ismatch As Boolean = False
        Return From el _
            In ( _
                From part _
                In SeeMatch.Split(txt) _
                Select If(PostNot(ismatch), <see cref=<%= Part %>/>, CObj(Part))) _
            Select el _
            Where Not TypeOf el Is String OrElse CStr(el) <> ""
    End Function
    ''' <summary>Translates string from ObjectBrowser to XML comment</summary>
    ''' <param name="obString">String from object browser</param>
    ''' <version version="2.5.3">Fixed. The &lt;see> is generated with cref containing ending dot</version>
    ''' <version version="1.5.4">Clipboard string now expects "Return Values" or "Returns" as header of returns part (previously only "Returns")</version>
    Public Function XMLCommentFromObjectBrowser(ByVal obString As String) As String
        '        Debug.Print("^(\r\n)*                                                                 " & vbCrLf & _
        '"(Summary:\r\n(?<Summary>[^\r\n]*))?                                      " & vbCrLf & _
        '"((\r\n){2}                                                               " & vbCrLf & _
        '"Type\sparameters:                                                        " & vbCrLf & _
        '"(\r\n(?<TypeParam>                                                       " & vbCrLf & _
        '"(?<TP_Name>[^\r\n:]+):\s*(?<TP_Desc>[^\r\n]*)))*                         " & vbCrLf & _
        '")?                                                                       " & vbCrLf & _
        '"((\r\n){2}                                                               " & vbCrLf & _
        '"Parameters:                                                              " & vbCrLf & _
        '"(\r\n(?<Param>(?<P_Name>[^\r\n:]+):\s*(?<P_Desc>[^\r\n]*)))*             " & vbCrLf & _
        '")?                                                                       " & vbCrLf & _
        '"((\r\n){2}                                                               " & vbCrLf & _
        '"Returns:\r\n(?<Returns>[^\r\n]*))?                                       " & vbCrLf & _
        '"((\r\n){2}                                                               " & vbCrLf & _
        '"Exceptions:                                                              " & vbCrLf & _
        '"(\r\n(?<Exception>(?<E_Name>[^\r\n:]+):\s*(?<E_Desc>[^\r\n]*)))*         " & vbCrLf & _
        '")?                                                                       " & vbCrLf & _
        '"((\r\n){2}                                                               " & vbCrLf & _
        '"Remarks:\r\n(?<Remarks>[^\r\n]*))?                                       " & vbCrLf & _
        '"(\r\n)*$                                                                 ")
        Dim xml = XMLCommentStart()
        Dim m = regexOB.Match(OBString)
        If M.Success Then
            Dim sb As New StringBuilder
            If M.Groups("Summary").Captures.Count = 1 AndAlso M.Groups("Summary").Captures(0).Value.Trim <> "" Then
                sb.AppendLine(String.Format(XML & <summary><%= SeeReplace(M.Groups("Summary").Captures(0).Value.Trim) %></summary>.ToString))
            End If
            For Each c As Capture In M.Groups("TypeParam").Captures
                Dim parts = c.Value.Split(New Char() {":"c}, 2)
                sb.AppendLine(XML & <typeparam name=<%= parts(0).Trim %>><%= SeeReplace(parts(1).Trim) %></typeparam>.ToString)
            Next c
            For Each c As Capture In M.Groups("Param").Captures
                Dim parts = c.Value.Split(New Char() {":"c}, 2)
                sb.AppendLine(XML & <param name=<%= parts(0).Trim %>><%= SeeReplace(parts(1).Trim) %></param>.ToString)
            Next c
            If M.Groups("Returns").Captures.Count = 1 AndAlso M.Groups("Returns").Captures(0).Value.Trim <> "" Then
                sb.AppendLine(String.Format(XML & <returns><%= SeeReplace(M.Groups("Returns").Captures(0).Value.Trim) %></returns>.ToString))
            End If
            For Each c As Capture In M.Groups("Exception").Captures
                Dim parts = c.Value.Split(New Char() {":"c}, 2)
                sb.AppendLine(XML & <exception cref=<%= parts(0).Trim %>><%= SeeReplace(parts(1).Trim) %></exception>.ToString)
            Next c
            If M.Groups("Remarks").Captures.Count = 1 AndAlso M.Groups("Remarks").Captures(0).Value.Trim <> "" Then
                sb.AppendLine(String.Format(XML & <remarks><%= SeeReplace(M.Groups("Remarks").Captures(0).Value.Trim) %></remarks>.ToString))
            End If
            Return sb.ToString.Trim
        End If
        Return Nothing
    End Function
    ''' <summary>Regular expression that looks for references in text transfered from ObjectBrowser as XML comment</summary>
    Private seeMatch As New Regex("(?<Name>(Microsoft|System)(\.\p{L}(\p{L}|p{N}|_)*(\(Of\s+[^)]+\))?)+)", RegexOptions.Compiled Or RegexOptions.ExplicitCapture Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.CultureInvariant Or RegexOptions.IgnoreCase)
    ''' <summary>Copies XML doc from member that is implemented or overridden by given member</summary>
    ''' <param name="ce">Target of comment</param>
    ''' <param name="Hint">Name of method and type split by dots (as usual). Method with best correspondence with this hint will be returned. Only for interfaces.</param>
    ''' <version version="1.5.3"><paramref name="ce"/> now can be defined in structure as well</version>
    ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False)</version>
    ''' <version version="1.5.4">Fix: Infinite loop may happen in VS2013</version>
    <CLSCompliant(False)>
    Public Sub InheritXmlDoc(ByVal ce As CodeElement2, Optional ByVal Hint$ = "")
        Dim this = New ClassMemberProvider(ce)
        Dim myName = This.Name
        Dim myPrototype = This.Prototype(2)
        If (This.OverrideKind And vsCMOverrideKind.vsCMOverrideKindOverride) = vsCMOverrideKind.vsCMOverrideKindOverride Then
            'overridden method in derived class
            'In base class looks for function with same name and same number of arguments
            Dim [myClass] As ClassProvider
            Try
                [MyClass] = New ClassProvider(CType(This.Parent, CodeClass2))
            Catch ex As InvalidCastException
                [MyClass] = New ClassProvider(CType(This.Parent, CodeStruct2))
            End Try
            Dim possibles As New List(Of KeyValuePair(Of Integer, ClassMemberProvider))
            For iBase = 1 To [MyClass].Bases.Count
                If [MyClass].Bases.Item(iBase).Kind = vsCMElement.vsCMElementClass Then
                    Dim baseClass = CType([MyClass].Bases(0), CodeClass2)
                    Dim level As Integer = 0
                    While BaseClass IsNot Nothing
                        Try
                            For i = 1 To BaseClass.Members.Count
                                If BaseClass.Members.Item(i).Kind = This.Kind Then
                                    Dim baseFunction = New ClassMemberProvider(BaseClass.Members.Item(i)) 'Base class's functions
                                    Dim otherName$ = Nothing : Dim otherPrototype$ = Nothing
                                    Try
                                        OtherName = BaseFunction.Name : OtherPrototype = BaseFunction.Prototype(2)
                                    Catch : OtherName = "" : End Try
                                    If OtherName <> "" AndAlso OtherName = MyName AndAlso OtherPrototype = MyPrototype Then
                                        Possibles.Add(New KeyValuePair(Of Integer, ClassMemberProvider)(level, BaseFunction))
                                    End If
                                End If
                            Next i
                            If BaseClass.Equals(BaseClass.Bases(0)) Then Exit While
                            BaseClass = BaseClass.Bases(0)
                        Finally
                            level += 1
                        End Try
                    End While
                    If Possibles.Count <> 0 Then Exit For
                End If
            Next iBase
            Dim onlyPossible As ClassMemberProvider = Nothing
            If Possibles.Count = 1 Then
                OnlyPossible = Possibles(0).Value
            ElseIf Possibles.Count > 0 Then
                Dim possibles2 = From pos In Possibles Select member = pos.Value, level = pos.Key, SignatureDismatch = SignatureDismatch(pos.Value, This)
                Dim possibles3 = _
                    From pos In Possibles2 _
                    Where pos.SignatureDismatch = (From pos2 In Possibles2 Select pos2.SignatureDismatch).Min _
                    Select pos.Level, pos.Member
                OnlyPossible = ( _
                    From pos In Possibles3 _
                    Where pos.Level = (From pos2 In Possibles3 Select pos2.Level).Min _
                    Select pos.Member).First
            End If
            If OnlyPossible IsNot Nothing Then
                SetXMLCommentFromOther(This, OnlyPossible)
                Exit Sub
            End If
        Else
            'Implemented member of interface
            'Look for member of interface with same name and number fo params
            Dim parent As CodeClass2 = This.Parent
            For i = 1 To Parent.ImplementedInterfaces.Count
                Dim base = Parent.ImplementedInterfaces.Item(i)
                If Base.Kind = vsCMElement.vsCMElementInterface Then
                    Dim bInt = TryCast(Base, CodeInterface2)
                    If Base Is Nothing Then Continue For
                    Dim method = SearchIMethod(This, Base, Hint)
                    If Method IsNot Nothing Then
                        SetXMLCommentFromOther(This, Method)
                        Exit Sub
                    End If
                End If
            Next i
        End If
    End Sub
    ''' <summary>Searches interface and its base interfaces for given method</summary>
    ''' <param name="Method">Method to search for</param>
    ''' <param name="face">Interface to search within</param>
    ''' <param name="hint">Name of method and type splitted by dots (as usual). Method with best correspondence with this hint will be returned.</param>
    ''' <param name="list">Nothing if not called by itself recursivelly</param>
    ''' <returns>Method found or null if not found. If <paramref name="list"/> is not null returns always null and adds all possible methods into <paramref name="list"/>.</returns>
    Private Function SearchIMethod(ByVal Method As ClassMemberProvider, ByVal face As CodeInterface2, Optional ByVal hint$ = "", Optional ByVal list As List(Of ClassMemberProvider) = Nothing) As ClassMemberProvider
        Dim iList = If(List, New List(Of ClassMemberProvider))
        For i = 1 To face.Members.Count
            Dim bItem As New ClassMemberProvider(face.Members.Item(i))
            If BItem.Kind = Method.Kind AndAlso (BItem.Name = Method.Name OrElse BItem.FullName.Contains(Hint)) AndAlso BItem.Prototype(2) = Method.Prototype(2) Then
                iList.Add(BItem)
            End If
        Next i
        For i = 1 To face.Bases.Count
            SearchIMethod(Method, face.Bases.Item(i), Hint, iList)
            'If FMet IsNot Nothing Then iList.Add(FMet)
        Next i
        If List Is Nothing AndAlso iList.Count > 1 Then
            If Hint = "" OrElse Hint Is Nothing Then Return iList(0)
            Dim matches As New List(Of KeyValuePair(Of Integer, ClassMemberProvider))
            For Each m In iList
                If m.FullName = Hint Then
                    Matches.Add(New KeyValuePair(Of Integer, ClassMemberProvider)(m.FullName.Length + 1, m))
                ElseIf m.FullName.EndsWith(Hint) Then
                    Matches.Add(New KeyValuePair(Of Integer, ClassMemberProvider)(m.FullName.Length - m.FullName.IndexOf(Hint), m))
                Else
                    Matches.Add(New KeyValuePair(Of Integer, ClassMemberProvider)(0, m))
                End If
            Next m
            Dim mPossible = From match In Matches Where Match.Key = (From min In Matches Select min.Key).Max Select Match.Value
            If mPossible.Count = 1 Then Return mPossible.First
            Dim mParS = From mp In mPossible Select New KeyValuePair(Of Integer, ClassMemberProvider)(Math.Abs(mp.Parameters.Count - Method.Parameters.Count), mp)
            Dim mPossible2 = From mp2 In mParS Where mp2.Key = (From mp2I In mParS Select mp2i.Key).Min Select mp2
            If mPossible2.Count = 1 Then Return mPossible2.First.Value
            Return (From mp3 In mPossible2 Order By SignatureDismatch(mp3.Value, Method) Select mp3.Value).First
        ElseIf List Is Nothing AndAlso iList.Count = 1 Then
            Return iList(0)
        End If
        Return Nothing
    End Function

    ''' <summary>Numericaly evaluates how much two signatures differs</summary>
    ''' <param name="cm1">A member to compare</param>
    ''' <param name="cm2">A member to compare</param>
    ''' <returns>Numerical evaluation of how much signatures differs. Higher number means bigger difference.</returns>
    ''' <remarks>Return types are not tested. Events alawys returns 0.</remarks>
    Private Function SignatureDismatch(ByVal cm1 As ClassMemberProvider, ByVal cm2 As ClassMemberProvider) As Integer
        If cm1.MyKind = ClassMemberProvider.MyKinds.Event OrElse cm2.MyKind = ClassMemberProvider.MyKinds.Event Then Return 0
        Dim dismatchs As Integer
        For i = 1I To cm1.Parameters.Count
            If i > cm2.Parameters.Count Then Exit For
            If Not CompareParams(cm1.Parameters(i), cm2.Parameters(i)) Then dismatchs += 1
        Next i
        Return dismatchs + Math.Abs(cm1.Parameters.Count - cm2.Parameters.Count)
    End Function
    ''' <summary>Compares 2 method parameters</summary>
    ''' <param name="p1">Parameter to compare</param>
    ''' <param name="p2">Parameter to compare</param>
    ''' <returns>False when either of params is not <see cref="CodeParameter2"/>. Otherwise returns true when parameter types are same, false if they differs</returns>
    Private Function CompareParams(ByVal p1 As EnvDTE.CodeElement, ByVal p2 As EnvDTE.CodeElement) As Boolean
        Dim par1 = TryCast(p1, CodeParameter2)
        Dim par2 = TryCast(p2, CodeParameter2)
        If par1 Is Nothing OrElse par2 Is Nothing Then Return False
        Return par1.Type.AsFullName = par2.Type.AsFullName
    End Function

    ''' <summary>Mover XML comment from one property/event/function to another. Can deal with custom as well as external source.</summary>
    ''' <param name="this">Target of comment</param>
    ''' <param name="other">Source of comment</param>
    Public Sub SetXMLCommentFromOther(ByVal this As ClassMemberProvider, ByVal other As ClassMemberProvider)
        If Other.DocComment <> "" Then
            If Other.InfoLocation = vsCMInfoLocation.vsCMInfoLocationProject Then
                This.DocComment = Other.DocComment.Trim
            Else
                DTE.UndoContext.Open("Ihnerit XML comment")
                Try
                    Dim xml = XElement.Load(New IO.StringReader(Other.DocComment))
                    If xml.Name = "doc" Then
                        Dim xml2 = New Xml.XmlDocument
                        xml2.LoadXml(Other.DocComment)
                        This.DocComment = xml2.FirstChild.InnerXml.Trim.Replace(vbCr, "").Replace(vbLf, "").Replace("<remarks>", vbCrLf & "<remarks>").Replace("<returns>", vbCrLf & "<returns>").Replace("<exception", vbCrLf & "<exception").Replace("<param", vbCrLf & "<param").Replace("<typeparam", vbCrLf & "<typepara,").Replace("<permission", vbCrLf & "<permission").Replace("<filterpriority>", vbCrLf & "<filterpriority>")
                    Else : This.DocComment = xml.ToString
                    End If
                Catch
                    This.DocComment = Other.DocComment.Trim
                Finally
                    DTE.UndoContext.Close()
                End Try
            End If
        End If
    End Sub


    ''' <summary>In current selections wraps each new line with the &lt;para> tag preceded by ''' (''' is not set for 1st line)</summary>
    Public Sub Nl2Para()
        DTE.UndoContext.Open("Nl2Para")
        Try
            DirectCast(DTE.ActiveWindow.Selection, TextSelection).Insert(Nl2Para(DirectCast(DTE.ActiveWindow.Selection, TextSelection).Text))
        Finally
            DTE.UndoContext.Close()
        End Try
    End Sub
    ''' <summary>Wraps each new line in given string with &lt;para> tag</summary>
    ''' <param name="Selection">A multiline string</param>
    ''' <returns><paramref name="Selection"/> with all linew wrapped in &lt;para> tags preceded by ''' (''' is not set for first line)</returns>
    Private Function Nl2Para$(ByVal Selection$)
        Dim ret As New System.Text.StringBuilder
        Dim r As New IO.StringReader(Selection)
        Dim line$ = r.ReadLine
        Dim apos3$ = ""
        While Line IsNot Nothing
            If Line <> "" Then
                ret.AppendLine(String.Format("{1}<para>{0}</para>", Line.Trim.Replace("&", "&amp;").Replace("<", "&lt;"), Apos3))
                Apos3 = XMLCommentStart()
            End If
            Line = r.ReadLine
        End While
        Return ret.ToString.Trim
    End Function
    ''' <summary>Surronds selection with &lt;see cref="{0}"/></summary> 
    Public Sub CreateSee()
        DirectCast(DTE.ActiveWindow.Selection, TextSelection).Insert(String.Format("<see cref=""{0}""/>", DirectCast(DTE.ActiveWindow.Selection, TextSelection).Text))
    End Sub
    ''' <summary>Surronds selection with &lt;paramref name="{0}"/></summary> 
    Public Sub CreateParamref()
        DirectCast(DTE.ActiveWindow.Selection, TextSelection).Insert(String.Format("<paramref name=""{0}""/>", DirectCast(DTE.ActiveWindow.Selection, TextSelection).Text))
    End Sub
    ''' <summary>Removes from selection lines starting with '''</summary>
    Public Sub NoComments()
        DTE.UndoContext.Open("NoComments")
        Try
            DirectCast(DTE.ActiveWindow.Selection, TextSelection).Insert(NoComments(DirectCast(DTE.ActiveWindow.Selection, TextSelection).Text))
        Finally
            DTE.UndoContext.Close()
        End Try
    End Sub
    ''' <sumary>Removes all doc comments from given VB code</sumary>
    ''' <param name="Code">Code to remove comments from</param>
    ''' <returns><paramref name="Code"/> without lines starting with '''</returns>
    Private Function NoComments$(ByVal Code$)
        Dim r As New IO.StringReader(Code)
        Dim ret As New Text.StringBuilder
        Dim line As String = r.ReadLine
        While Line IsNot Nothing
            If Not Line.TrimStart.StartsWith(XMLCommentStart) Then ret.AppendLine(Line)
            Line = r.ReadLine
        End While
        Return ret.ToString
    End Function
End Module
