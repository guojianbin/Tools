﻿Imports System.Runtime.CompilerServices
Imports CultureInfo = System.Globalization.CultureInfo
Imports System.Text
Imports System.Web
Imports Tools.TextT

Namespace ExtensionsT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Provides string formatting</summary>
    ''' <remarks>Formatting rules:
    ''' <para>If C#-like backslash (\) escape sequences are allowed formating combines C#-like string escaping and formatting used by <see cref="[String].Format"/>.</para>
    ''' <para>Escaping rules:</para>
    ''' <list type="table"><listheader><term>Escape sequence</term><description>Meaning</description></listheader>
    ''' <item><term>\a</term><description>Allert 0x7 (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\b</term><description>Backspace 0x8 (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\f</term><description>Form feed 0xC (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\n</term><description>New line 0xA (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\r</term><description>Carriage return 0xD (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\t</term><description>Horizontal tab 0x9 (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\v</term><description>Vertical tab 0xB (only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\.</term><description>Empty string (ignored; only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\U[0-9A-Za-z]+, \u[0-9A-Za-z]+, \X[0-9A-Za-z]+, \x[0-9A-Za-z]+</term><description>Hexadecimal Unicode escape sequence. Given hexadecimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.  (Only when C#-style escape sequences are allowed)</description></item>
    ''' <item><term>\[0-9]+</term><description>Decimal Unicode escape sequence. Given decimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>  (only when C#-style escape sequences are allowed).</description></item>
    ''' <item><term>\&lt;any other character></term><description>The character, not the backslash.</description></item>
    ''' <item><term>{{</term><description>{ (only when formatting is allowed; {{ otherwise)</description></item>
    ''' <item><term>}}</term><description>} (only when formatting is allowed; }} otherwise)<note>Unlike when using <see cref="System.String.Format"/> it is not error to have } alone in string.</note></description></item>
    ''' </list>
    ''' <para>
    ''' For various <see cref="StringFormatting.Replace"/> and <see cref="StringFormatting.CReplace"/> overloads escapes also work in name and format part.
    ''' They does not work in align part. ({name,align:format|transform})
    ''' </para>
    ''' <para>For various <see cref="StringFormatting.Replace"/> and <see cref="StringFormatting.CReplace"/> overloads additional rules apply:</para>
    ''' <list type="table">
    ''' <listheader><term>Escape sequence</term><description>Description</description></listheader>
    ''' <item><term>::</term><description>Only in placeholder name part: :</description></item>
    ''' <item><term>,,</term><description>Only in placeholder name part: ,</description></item>
    ''' <item><term>||</term><description>Only in placeholder name, format or transformation part: |</description></item>
    ''' <item><term>{}</term><description>An empty string (only immediatelly before or after placeholder). This rule was added for confilct resolution, see below.</description></item>
    ''' </list>
    ''' <note>You can also use \: and \, to escape : and , (only when C#-style escaping is allowed).</note>
    ''' <para>Conflict resolution (only in <see cref="StringFormatting.Replace"/> and <see cref="StringFormatting.CReplace"/>):</para>
    ''' <para>
    ''' {{{Name}}} is translated as "{" + value of "Name}". It would be impossible to specify value of "{Name" or value of something + "}". Thus additional rules was apply:
    ''' {} is treated as an empty string only when:
    ''' </para>
    ''' <list type="bullet">
    ''' <item>It immediatelly follows closing } of placeholder</item>
    ''' <item>It immediatelly precedes openning { of placeholder. Exception: {{} is not treated this way. Special behavior: {}{{ - {{ does not mean escape of {, it's treated as first chanacter of placeholder name</item>
    ''' </list>
    ''' <para>In particular following escape sequences are available:</para>
    ''' <list type="table"><listheader><term>Escape sequance</term><description>Meaning</description></listheader>
    ''' <item><term>\0</term><description>Nullchar</description></item>
    ''' <item><term>\\</term><description>\</description></item>
    ''' <item><term>\"</term><description>"</description></item>
    ''' <item><term>\'</term><description>'</description></item>
    ''' </list>
    ''' <para>Be carefull when typign such string in languages that processes it like C#, C++ or PHP. Get output \ from escaping \\\\ must be typed.</para>
    ''' <para>Following rules apply to fromatting (only when formating is being done):</para>
    ''' <list type="bullet">
    ''' <item>String may contain placeholers of arguments being formatted (passed in array). Each placeholder reffers to index within the array.</item>
    ''' <item>Placeholder is in format  {index[,alignment][:formatString][|transfomation]*}</item>
    ''' <item>Index is any non-negative decimal integral number [0-9]+</item>
    ''' <item>
    ''' Alignment is optional, preceded with comma and it is -?[0-9]+ decimal integral number delaring minimal width of string replacing the placeholder.
    ''' Negative for left-align, positive for right aling. If padding is necessary space is used. Trimming never occurs.
    ''' </item>
    ''' <item>
    ''' FormatString is optional formatting string passed to formating method of argument.
    ''' Ignored when argument does implement neither <see cref="ICustomFormatter"/> nor <see cref="IFormattable"/>.
    ''' </item>
    ''' <item>
    ''' Transformation is optional transformation identifier (case-insensitive), multiple identifiers can specifies be separated by |.
    ''' Transformation identifiers must be registered in <see cref="StringFormatting.Transformations"/>. There are several pre-defined transformations.
    ''' A transformation taks a string comming from formatting result and transform it. It can be used e.g. for HTML encoding.
    ''' </item>
    ''' </list>
    ''' <para>In format string escaping is done in same way as described above.</para>
    ''' </remarks>
    ''' <version version="1.5.2">Module introduced</version>
    ''' <version version="1.5.3">In 1.5.2 the module was not made public by mistake - so, accessibility changed from Friend (internal) to Public.</version>
    ''' <version version="1.5.4">Fixes in documentation (wrong formatting and clarification)</version>
    ''' <version version="1.5.4">Added various <see cref="StringFormatting.Replace"/> and <see cref="StringFormatting.CReplace"/> overloads</version>
    ''' <version version="1.5.4">Added documentation for transformations</version>
    Public Module StringFormatting
        ''' <summary>Formats and escape string according to rules described for <see cref="StringFormatting"/></summary>
        ''' <param name="str">Formatting string</param>
        ''' <param name="Args">Objects to format</param>
        ''' <returns>String formatted</returns>
        ''' <param name="provider">Formatting provider</param>
        ''' <exception cref="FormatException"><paramref name="str"/> is invalid format string</exception>
        ''' <version version="1.5.4">Transformations (|) are now supported. This can cause breaking changes.</version>
        ''' <version version="1.5.4">Parameter <c>Args</c> renamed to <c>args</c>.</version>
        <Extension()>
        Public Function CFormat(ByVal str As String, ByVal provider As IFormatProvider, ByVal ParamArray args As Object()) As String
            Return CFormat(provider, str, args, True)
        End Function
        ''' <summary>Escapes string according to rules described for <see cref="StringFormatting"/> without formatting it</summary>
        ''' <param name="str">String to un-escape</param>
        ''' <returns>String unescaped</returns>
        ''' <exception cref="FormatException"><paramref name="str"/> contains invalid escape sequence</exception>
        ''' <remarks>This method does not allow formatting, so "{{" gets to output as "{{" and "}}" as "}}".</remarks>
        <Extension()>
        Public Function CEscape(ByVal str As String) As String
            Return CFormat(CultureInfo.CurrentCulture, str, New Object() {}, False)
        End Function
        ''' <summary>Formats and escape string according to rules described for <see cref="StringFormatting"/></summary>
        ''' <param name="str">Formatting string</param>
        ''' <param name="Args">Objects to format</param>
        ''' <returns>String formatted</returns>
        ''' <exception cref="FormatException"><paramref name="str"/> is invalid format string</exception>
        ''' <remarks>This method uses <see cref="CultureInfo.CurrentCulture"/></remarks>
        ''' <version version="1.5.4">Transformations (|) are now supported. This can cause breaking changes.</version>
        ''' <version version="1.5.4">Parameter <c>Args</c> renamed to <c>args</c>.</version>
        <Extension()>
        Public Function CFormat(ByVal str As String, ByVal ParamArray args As Object()) As String
            Return CFormat(str, CultureInfo.CurrentCulture, args)
        End Function
        ''' <summary>Fine State Automaton states for string formatting and escaping</summary>
        Private Enum CFormatFSA
            ''' <summary>Normal state</summary>
            [String]
            ''' <summary>{ in normal</summary>
            Open1
            ''' <summary>} in normal</summary>
            Close1
            ''' <summary>\ in normal</summary>
            Back
            ''' <summary>\x, \X, \u, \U in normal</summary>
            X
            ''' <summary>\x, \X, \u, \U and hexanumber in normal</summary>
            Xnext
            ''' <summary>\ and number in normal</summary>
            NumEscape
            ''' <summary>{0</summary>
            ArgNum
            ''' <summary>{0,</summary>
            Comma
            ''' <summary>{0,0, {0,-0</summary>
            Width
            ''' <summary>Custom format</summary>
            CustomFormat
            ''' <summary>{ in format</summary>
            cOpen1
            ''' <summary>} in format</summary>
            cClose1
            ''' <summary>{0,-</summary>
            MinusWidth
            ''' <summary>{0:aa|</summary>
            CustomFormatPipe
            ''' <summary>{0| or {0:x|a etc.</summary>
            TransformName
            ''' <summary>{0|a| or {0:x|a| etc.</summary>
            TransformNamePipe
            ''' <summary>{0|a\ or {0:x|a\ etc.</summary>
            TransformNameBack
        End Enum
        ''' <summary>Internaly pefrorms the formatting process</summary>
        ''' <param name="provider">Provider providing formatting</param>
        ''' <param name="str">Formatting string</param>
        ''' <param name="args">Arguments to format</param>
        ''' <param name="format">True to do formatting and escaping, false to do escaping only</param>
        ''' <returns>String formatted</returns>
        ''' <exception cref="FormatException"><paramref name="str"/> contains invalid format string</exception>
        Private Function CFormat(ByVal provider As IFormatProvider, ByVal str As String, ByVal args As Object(), ByVal format As Boolean) As String
            If provider Is Nothing Then provider = CultureInfo.CurrentCulture
            If str = "" Then Return ""
            If args Is Nothing Then args = New Object() {}
            Dim ret As New System.Text.StringBuilder
            Dim state As CFormatFSA = CFormatFSA.String
            Dim numEscapeValue% = 0
            Dim argNum% = 0
            Dim width% = 0
            Dim widthSign% = 1
            Dim customFormat As System.Text.StringBuilder = Nothing
            Dim retState As CFormatFSA = CFormatFSA.String
            Dim trans As New List(Of StringBuilder)
            For Each ch As Char In str
SelectCase:     Select Case state
                    Case CFormatFSA.String 'Normal situation
                        Select Case ch
                            Case "{"c : If format Then state = CFormatFSA.Open1 Else ret.Append(ch)
                            Case "}"c : If format Then state = CFormatFSA.Close1 Else ret.Append(ch)
                            Case "\"c : state = CFormatFSA.Back : retState = CFormatFSA.String
                            Case Else : ret.Append(ch)
                        End Select
                    Case CFormatFSA.Back '\
                        Dim AppendTo As System.Text.StringBuilder = IIf(retState = CFormatFSA.String, ret, customFormat)
                        Select Case ch
                            Case "a"c : AppendTo.Append(ChrW(7)) : state = retState  'Aletr
                            Case "b"c : AppendTo.Append(ChrW(8)) : state = retState 'Backspace
                            Case "f"c : AppendTo.Append(ChrW(&HC)) : state = retState 'Form feed
                            Case "n"c : AppendTo.Append(ChrW(&HA)) : state = retState 'New line
                            Case "r"c : AppendTo.Append(ChrW(&HD)) : state = retState 'Carriage return
                            Case "t"c : AppendTo.Append(ChrW(9)) : state = retState 'Horizontal tab
                            Case "v"c : AppendTo.Append(ChrW(&HB)) : state = retState 'Vertical tab
                            Case "."c : state = retState
                            Case "x"c, "X"c, "u"c, "U"c : state = CFormatFSA.X
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                state = CFormatFSA.NumEscape : numEscapeValue = AscW(ch) - AscW("0"c)
                            Case Else : AppendTo.Append(ch) : state = retState
                        End Select
                    Case CFormatFSA.NumEscape '\0 \1 ...
                        Dim AppendTo As System.Text.StringBuilder = If(retState = CFormatFSA.String, ret, customFormat)
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                numEscapeValue = numEscapeValue * 10 + AscW(ch) - AscW("0"c)
                            Case Else
                                state = retState
                                Try
                                    AppendTo.Append(Char.ConvertFromUtf32(numEscapeValue))
                                Catch ex As ArgumentOutOfRangeException
                                    Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, numEscapeValue), ex)
                                End Try
                                GoTo SelectCase
                        End Select
                    Case CFormatFSA.X '\x \X \U \u
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                numEscapeValue = AscW(ch) - AscW("0"c) : state = CFormatFSA.Xnext
                            Case "a"c, "b"c, "c"c, "d"c, "e"c, "f"c
                                numEscapeValue = AscW(ch) - AscW("a"c) + 10 : state = CFormatFSA.Xnext
                            Case "A"c, "B"c, "C"c, "D"c, "E"c, "F"c
                                numEscapeValue = AscW(ch) - AscW("A"c) + 10 : state = CFormatFSA.Xnext
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringInvalidHexadecimalEscapeSequence)
                        End Select
                    Case CFormatFSA.Xnext '\x1 \X1 \u1 \U1 ...
                        Dim AppendTo As System.Text.StringBuilder = IIf(retState = CFormatFSA.String, ret, customFormat)
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                numEscapeValue = numEscapeValue * 16 + AscW(ch) - AscW("0"c)
                            Case "a"c, "b"c, "c"c, "d"c, "e"c, "f"c
                                numEscapeValue = numEscapeValue * 16 + AscW(ch) - AscW("a"c) + 10
                            Case "A"c, "B"c, "C"c, "D"c, "E"c, "F"c
                                numEscapeValue = numEscapeValue * 16 + AscW(ch) - AscW("A"c) + 10
                            Case Else
                                state = retState
                                Try
                                    AppendTo.Append(Char.ConvertFromUtf32(numEscapeValue))
                                Catch ex As ArgumentOutOfRangeException
                                    Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0x0X, numEscapeValue), ex)
                                End Try
                                GoTo SelectCase
                        End Select
                    Case CFormatFSA.Close1 '}
                        Select Case ch
                            Case "}"c : ret.Append("}"c) : state = CFormatFSA.String
                            Case Else : ret.Append("}"c) : state = CFormatFSA.String : GoTo SelectCase
                        End Select
                    Case CFormatFSA.Open1 '{
                        Select Case ch
                            Case "{"c : ret.Append("{"c) : state = CFormatFSA.String
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                argNum = AscW(ch) - AscW("0"c) : state = CFormatFSA.ArgNum
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringArgumentNumberExpected)
                        End Select
                    Case CFormatFSA.ArgNum '{0, {1
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                argNum = argNum * 10 + AscW(ch) - AscW("0"c)
                            Case ","c : state = CFormatFSA.Comma
                            Case ":"c : state = CFormatFSA.CustomFormat : width = 0 : customFormat = New System.Text.StringBuilder
                            Case "}"c : ret.Append(FormatInternal(argNum, 0, Nothing, args, provider, trans)) : state = CFormatFSA.String
                            Case "|"c : state = CFormatFSA.TransformName : trans.Clear() : trans.Add(New StringBuilder) : customFormat = New StringBuilder()
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringNumeralOrExpected2)
                        End Select
                    Case CFormatFSA.Comma '{0,
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                width = AscW(ch) - AscW("0"c) : state = CFormatFSA.Width
                                widthSign = +1
                            Case "-"c : widthSign = -1 : state = CFormatFSA.MinusWidth
                            Case Else : Throw New Exception(ResourcesT.Exceptions.InvalidFormatStringExpectedWidthNumberOr)
                        End Select
                    Case CFormatFSA.MinusWidth '{0,-
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                width = AscW(ch) - AscW("0"c) : state = CFormatFSA.Width
                            Case Else : Throw New Exception(ResourcesT.Exceptions.InvalidFormatStringExpectedWidthNumber)
                        End Select
                    Case CFormatFSA.Width '{0,0
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                width = width * 10 + AscW(ch) - AscW("0"c)
                            Case ":"c : state = CFormatFSA.CustomFormat : customFormat = New System.Text.StringBuilder
                            Case "}"c : ret.Append(FormatInternal(argNum, width * widthSign, Nothing, args, provider, trans)) : state = CFormatFSA.String
                            Case "|"c : state = CFormatFSA.TransformName : trans.Clear() : trans.Add(New StringBuilder) : customFormat = New StringBuilder
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringNumeralOrExpected)
                        End Select
                    Case CFormatFSA.CustomFormat  '{0:, {0,0:
                        Select Case ch
                            Case "{"c : state = CFormatFSA.cOpen1
                            Case "}"c : state = CFormatFSA.cClose1
                            Case "\"c : state = CFormatFSA.Back : retState = CFormatFSA.CustomFormat
                            Case "|"c : state = CFormatFSA.CustomFormatPipe
                            Case Else : customFormat.Append(ch)
                        End Select
                    Case CFormatFSA.CustomFormatPipe '{0:a|
                        Select Case ch
                            Case "|"c : customFormat.Append("|"c) : state = CFormatFSA.CustomFormat
                            Case "}"c : ret.Append(FormatInternal(argNum, width * widthSign, customFormat.ToString, args, provider, trans))
                            Case Else : trans.Clear() : trans.Add(New StringBuilder(CStr(ch))) : state = CFormatFSA.TransformName
                        End Select
                    Case CFormatFSA.cOpen1
                        Select Case ch
                            Case "{"c : customFormat.Append("{"c) : state = CFormatFSA.CustomFormat
                            Case Else : state = CFormatFSA.CustomFormat : customFormat.Append("{"c) : GoTo SelectCase
                        End Select
                    Case CFormatFSA.cClose1
                        Select Case ch
                            Case "}"c : customFormat.Append("}"c) : state = CFormatFSA.CustomFormat
                            Case Else : ret.Append(FormatInternal(argNum, width * widthSign, customFormat.ToString, args, provider, trans)) : state = CFormatFSA.String : GoTo SelectCase
                        End Select
                    Case CFormatFSA.TransformName
                        Select Case ch
                            Case "}"c : ret.Append(FormatInternal(argNum, width * widthSign, customFormat.ToString, args, provider, trans)) : state = CFormatFSA.String
                            Case "|"c : state = CFormatFSA.TransformNamePipe
                            Case "\"c : state = CFormatFSA.TransformNameBack
                            Case Else : trans(trans.Count - 1).Append(ch)
                        End Select
                    Case CFormatFSA.TransformNamePipe
                        Select Case ch
                            Case "}"c : ret.Append(FormatInternal(argNum, width * widthSign, customFormat.ToString, args, provider, trans)) : state = CFormatFSA.String
                            Case "|"c : trans(trans.Count - 1).Append("|"c) : state = CFormatFSA.TransformName
                            Case Else : trans.Add(New StringBuilder(CStr(ch))) : state = CFormatFSA.TransformName
                        End Select
                    Case CFormatFSA.TransformNameBack
                        trans(trans.Count - 1).Append(ch) : state = CFormatFSA.TransformName
                End Select
            Next
            Select Case state
                Case CFormatFSA.String 'Do nothing
                Case CFormatFSA.Back
                    If retState = CFormatFSA.String Then ret.Append("\") Else Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                Case CFormatFSA.NumEscape
                    If retState = CFormatFSA.String Then
                        Try
                            ret.Append(Char.ConvertFromUtf32(numEscapeValue))
                        Catch ex As ArgumentOutOfRangeException
                            Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, numEscapeValue), ex)
                        End Try
                    Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End If
                Case CFormatFSA.Xnext
                    If retState = CFormatFSA.String Then
                        Try
                            ret.Append(Char.ConvertFromUtf32(numEscapeValue))
                        Catch ex As ArgumentOutOfRangeException
                            Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0x0X, numEscapeValue), ex)
                        End Try
                    Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End If
                Case CFormatFSA.Close1 : ret.Append("}"c)
                Case CFormatFSA.Open1 : ret.Append("{"c)
                Case CFormatFSA.X, CFormatFSA.ArgNum, CFormatFSA.Comma, CFormatFSA.MinusWidth, CFormatFSA.Width, CFormatFSA.CustomFormat, CFormatFSA.cOpen1, CFormatFSA.CustomFormatPipe, CFormatFSA.TransformName, CFormatFSA.TransformNamePipe, CFormatFSA.TransformNameBack
                    Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                Case CFormatFSA.cClose1
                    ret.Append(FormatInternal(argNum, width * widthSign, customFormat.ToString, args, provider, trans))
            End Select
            Return ret.ToString
        End Function
        ''' <summary>Formats object using format string, width and format provider</summary>
        ''' <param name="argNum">Number of argument - index to <paramref name="Args"/></param>
        ''' <param name="width">Specifies minimal width of returned string. Negative for left align, positive for right align.</param>
        ''' <param name="format">Format string fo value</param>
        ''' <param name="args">Arguments. Item with index <paramref name="ArgNum"/> from this array will be formatted</param>
        ''' <param name="provider">Formatting provider</param>
        ''' <param name="trans">Identifiers of transformations to apply in given order</param>
        ''' <returns>Formatted <paramref name="args"/>[<paramref name="argNum"/>]. If argument is null an empty string is used; if it is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used; if it is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used; otherwise <see cref="System.[Object].ToString"/>. After formatting, value if widhtened to <paramref name="Width"/>.</returns>
        ''' <exception cref="FormatException"><paramref name="ArgNum"/> is greater than or equal to <paramref name="args"/>.<see cref="Array.Length">Length</see> -or- <paramref name="format"/> is invalid according to object being formatted.</exception>
        Private Function FormatInternal(ByVal argNum%, ByVal width%, ByVal format As String, ByVal args As Object(), ByVal provider As IFormatProvider, trans As IEnumerable(Of StringBuilder)) As String
            If argNum >= args.Length Then Throw New FormatException(String.Format("Invalid format string. Required argument number {0} missing.", argNum))
            Dim ret As String
            If args(argNum) Is Nothing Then
                ret = ""
            ElseIf TypeOf args(argNum) Is ICustomFormatter Then
                ret = DirectCast(args(argNum), ICustomFormatter).Format(format, args(argNum), provider)
            ElseIf TypeOf args(argNum) Is IFormattable Then
                ret = DirectCast(args(argNum), IFormattable).ToString(format, provider)
            Else
                ret = args(argNum).ToString
            End If
            If ret.Length >= Math.Abs(width) Then
                'do nothing
            ElseIf width > 0 Then 'Right
                ret = New String(" "c, width - ret.Length) & ret
            Else 'Left
                ret = ret & New String(" "c, -width - ret.Length)
            End If
            'Apply transformations
            If trans IsNot Nothing Then
                For Each tr In trans
                    Dim trs = tr.ToString
                    If trs <> "" Then
                        Dim trf As Func(Of String, String)
                        If Not Transformations.TryGetValue(trs, trf) Then Throw New FormatException(String.Format("Transformation {0} is not defined", trs))
                        If trf IsNot Nothing Then
                            ret = trf(ret)
                        End If
                    End If
                Next
            End If
            Return ret
        End Function

#Region "Replace"
        ''' <summary>States of FSA used by <see cref="Replace"/> methods</summary>
        Private Enum ReplaceFSA
            ''' <summary>Normal state</summary>
            [String]
            ''' <summary>{ in string</summary>
            Open
            ''' <summary>{ after closing } of placeholder</summary>
            CloseOpen
            ''' <summary>{} in string</summary>
            OpenClose
            ''' <summary>\ in string</summary>
            StringBackSlash
            ''' <summary>Name of placeholder</summary>
            Name
            ''' <summary>\ in name of placeholder</summary>
            NameBackSlash
            ''' <summary>: in name of placeholder</summary>
            NameColon
            ''' <summary>, in name of placceholder</summary>
            NameComma
            ''' <summary>} in name of placeholder</summary>
            NameClose
            ''' <summary>{ in name</summary>
            NameOpen
            ''' <summary>{ in format</summary>
            FormatOpen
            ''' <summary>+ at the beginnign of align</summary>
            AlignPlus
            ''' <summary>- at the beginning of align</summary>
            AlignMinus
            ''' <summary>Alignment number</summary>
            Align
            ''' <summary>-0 at the beginnging of align</summary>
            AlignMinusZero
            ''' <summary>Formatting sring (after :)</summary>
            Format
            ''' <summary>} in formatting string</summary>
            FormatClose
            ''' <summary>\ in formatting string</summary>
            FormatBackSlash
            ''' <summary>General processing of char after \ anywhere</summary>
            BackSlash
            ''' <summary>\x, \X, \U, \u</summary>
            BackSlashHex1
            ''' <summary>\[0-9]</summary>
            BackSlashDec
            ''' <summary>\[xXuU][0-9a-fA-F]</summary>
            BackSlashHex
            ''' <summary>} in string</summary>
            Close
            ''' <summary>{name|</summary>
            NamePipe
            ''' <summary>{name|a</summary>
            Transformation
            ''' <summary>{name:0|</summary>
            FormatPipe
            ''' <summary>{name|a}</summary>
            TransformationClose
            ''' <summary>{name|a\</summary>
            TransformationBackSlash
            ''' <summary>{name|a{</summary>
            TransformationOpen
            ''' <summary>{name|a|</summary>
            TransformationPipe
        End Enum

        ''' <summary>Replaces placeholders with formatting in given string</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="getValue">A function that provides values of placeholders. Names of placeholders are passed here and values are expected to be returned.</param>
        ''' <param name="cEscapes">True to allow C#-style backslash (\) escaping. False not to allow it.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="getValue"/> is null</exception>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.</remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Friend Function ReplaceInternal(pattern As String, getValue As Func(Of String, Object), cEscapes As Boolean, provider As IFormatProvider) As String
            If getValue Is Nothing Then Throw New ArgumentNullException("getValue")
            If pattern Is Nothing Then Return Nothing

            Dim ret As New StringBuilder
            Dim state As ReplaceFSA = ReplaceFSA.String
            Dim name As StringBuilder = Nothing
            Dim format As StringBuilder = Nothing
            Dim align = 0
            Dim code = 0
            Dim oldChar As Char = Chars.NullChar
            Dim trans As New List(Of StringBuilder)

            Dim backslashAppendTo As StringBuilder = Nothing
            Dim backslashReturnTo As ReplaceFSA = ReplaceFSA.String

            For Each ch In pattern
DoItAgain:
                Select Case state
                    Case ReplaceFSA.String 'Normal string
                        Select Case ch
                            Case "{"c : state = ReplaceFSA.Open
                            Case "\"c : If cEscapes Then state = ReplaceFSA.StringBackSlash Else ret.Append("\"c)
                            Case "}"c : state = ReplaceFSA.Close
                            Case Else : ret.Append(ch)
                        End Select
                    Case ReplaceFSA.Close '} in string
                        Select Case ch
                            Case "{"c : ret.Append("}"c) : state = ReplaceFSA.Open
                            Case "\"c : ret.Append("}"c) : If cEscapes Then state = ReplaceFSA.StringBackSlash Else ret.Append("\"c)
                            Case "}"c : ret.Append("}"c) : state = ReplaceFSA.String
                            Case Else : ret.Append("}"c) : ret.Append(ch) : state = ReplaceFSA.String
                        End Select
                    Case ReplaceFSA.Open '{ in string
                        Select Case ch
                            Case "{"c : ret.Append("{") : state = ReplaceFSA.String
                            Case "\"c
                                name = New StringBuilder
                                If cEscapes Then
                                    state = ReplaceFSA.NameBackSlash
                                Else
                                    state = ReplaceFSA.Name
                                    name.Append("\"c)
                                End If
                            Case ":"c : name = New StringBuilder : state = ReplaceFSA.NameColon
                            Case ","c : name = New StringBuilder : state = ReplaceFSA.NameComma
                            Case "}"c : state = ReplaceFSA.OpenClose
                            Case Else : name = New StringBuilder : name.Append(ch) : state = ReplaceFSA.Name
                        End Select
                    Case ReplaceFSA.CloseOpen '}{ (end placeholder followed by {
                        Select Case ch
                            Case "}"c : state = ReplaceFSA.String 'Empty string
                            Case "{"c : ret.Append("{"c) : state = ReplaceFSA.String
                            Case "\"c
                                ret.Append("{"c)
                                If cEscapes Then
                                    state = ReplaceFSA.StringBackSlash
                                Else
                                    state = ReplaceFSA.String
                                    ret.Append("\"c)
                                End If
                            Case Else : ret.Append("{"c) : ret.Append(ch) : state = ReplaceFSA.String
                        End Select
                    Case ReplaceFSA.OpenClose '{}
                        Select Case ch
                            Case "{"c : name = New StringBuilder : state = ReplaceFSA.NameOpen
                            Case "}"c : name = New StringBuilder : name.Append("}"c) : state = ReplaceFSA.Name
                            Case "\"c
                                name = New StringBuilder
                                name.Append("}"c)
                                If cEscapes Then
                                    state = ReplaceFSA.NameBackSlash
                                Else
                                    state = ReplaceFSA.Name
                                    ret.Append("\"c)
                                End If
                            Case Else : name = New StringBuilder : name.Append("}"c) : state = ReplaceFSA.Name
                        End Select
                    Case ReplaceFSA.Name 'Name of placeholder after {
                        Select Case ch
                            Case "\"c : If cEscapes Then state = ReplaceFSA.NameBackSlash Else ret.Append("\c")
                            Case ":"c : state = ReplaceFSA.NameColon
                            Case ","c : state = ReplaceFSA.NameComma
                            Case "}"c : state = ReplaceFSA.NameClose
                            Case "{"c : state = ReplaceFSA.NameOpen
                            Case "|"c : state = ReplaceFSA.NamePipe
                            Case Else : name.Append(ch)
                        End Select
                    Case ReplaceFSA.NamePipe
                        Select Case ch
                            Case "|"c : name.Append("|"c) : state = ReplaceFSA.Name
                            Case Else : state = ReplaceFSA.Transformation : trans.Clear() : trans.Add(New StringBuilder(CStr(ch))) : format = New StringBuilder
                        End Select
                    Case ReplaceFSA.NameClose '} in name of placeholder
                        Select Case ch
                            Case "}"c : name.Append("}"c) : state = ReplaceFSA.Name
                            Case "{"c
                                ret.Append(FormatInternal$(name.ToString, Nothing, 0, getValue, provider))
                                state = ReplaceFSA.CloseOpen
                            Case "\"c
                                ret.Append(FormatInternal$(name.ToString, Nothing, 0, getValue, provider))
                                If cEscapes Then state = ReplaceFSA.StringBackSlash Else ret.Append("\"c) : state = ReplaceFSA.String
                            Case Else
                                ret.Append(FormatInternal$(name.ToString, Nothing, 0, getValue, provider))
                                ret.Append(ch)
                                state = ReplaceFSA.String
                        End Select
                    Case ReplaceFSA.NameComma ', in name of placeholder
                        Select Case ch
                            Case ","c : name.Append(","c) : state = ReplaceFSA.Name
                            Case "+"c : state = ReplaceFSA.AlignPlus
                            Case "-"c : state = ReplaceFSA.AlignMinus
                            Case "0"c To "9"c : align = ch.NumericValue : state = ReplaceFSA.Align
                            Case Else : Throw New FormatException("Invalid formatting string - invalid align: Expected +, - or digit 0-9")
                        End Select
                    Case ReplaceFSA.AlignPlus ',+ (align)
                        Select Case ch
                            Case "0"c To "9"c : align = ch.NumericValue : state = ReplaceFSA.Align
                            Case Else : Throw New FormatException("Invalid formatting string - invalid align: Expected digit 0-9")
                        End Select
                    Case ReplaceFSA.AlignMinus ',- (align)
                        Select Case ch
                            Case "0"c : state = ReplaceFSA.AlignMinusZero
                            Case "1"c To "9"c : align = -ch.NumericValue : state = ReplaceFSA.Align
                            Case Else : Throw New FormatException("Invalid formatting string - invalid align: Expected digit 0-9")
                        End Select
                    Case ReplaceFSA.AlignMinusZero ',-0, ,-00, -000 etc.
                        Select Case ch
                            Case "0"c 'Do nothing
                            Case "1"c To "9"c : align = -ch.NumericValue : state = ReplaceFSA.Align
                            Case ":"c : align = 0 : state = ReplaceFSA.Format : format = New StringBuilder
                            Case "|"c : align = 0 : state = ReplaceFSA.Transformation : format = New StringBuilder : trans.Clear() : trans.Add(New StringBuilder)
                            Case "}"c : ret.Append(FormatInternal$(name.ToString, Nothing, 0, getValue, provider)) : state = ReplaceFSA.CloseOpen
                            Case Else : Throw New FormatException("Invalid formatting string - invalid align: Expected :, } or digit 0-9")
                        End Select
                    Case ReplaceFSA.Align 'alignemnt - number after ,
                        Select Case ch
                            Case "0"c To "9"c : align = align * 10 + ch.NumericValue
                            Case ":"c : state = ReplaceFSA.Format : format = New StringBuilder
                            Case "|"c : state = ReplaceFSA.Name : format = New StringBuilder : trans.Clear() : trans.Add(New StringBuilder)
                            Case "}"c : ret.Append(FormatInternal$(name.ToString, Nothing, align, getValue, provider)) : state = ReplaceFSA.CloseOpen
                            Case Else : Throw New FormatException("Invalid formatting string - invalid align: Expected :, } or digit 0-9")
                        End Select
                    Case ReplaceFSA.NameColon ': in name
                        Select Case ch
                            Case ":"c : name.Append(":"c) : state = ReplaceFSA.Name
                            Case "}"c : format = New StringBuilder : state = ReplaceFSA.FormatClose : align = 0
                            Case "{"c : format = New StringBuilder : state = ReplaceFSA.FormatOpen : align = 0
                            Case "\"c
                                format = New StringBuilder
                                align = 0
                                If cEscapes Then
                                    state = ReplaceFSA.FormatBackSlash
                                Else
                                    format.Append("\"c)
                                    state = ReplaceFSA.Format
                                End If
                            Case "|"c : format = New StringBuilder : state = ReplaceFSA.FormatPipe : align = 0
                            Case Else : format = New StringBuilder() : format.Append(ch) : state = ReplaceFSA.Format : align = 0
                        End Select
                    Case ReplaceFSA.Format ':
                        Select Case ch
                            Case "}"c : state = ReplaceFSA.FormatClose
                            Case "{"c : state = ReplaceFSA.FormatOpen
                            Case "\"c : If cEscapes Then state = ReplaceFSA.FormatBackSlash Else format.Append("\"c)
                            Case "|"c : state = ReplaceFSA.FormatPipe
                            Case Else : format.Append(ch)
                        End Select
                    Case ReplaceFSA.FormatPipe '|
                        Select Case ch
                            Case "|"c : format.Append("|"c) : state = ReplaceFSA.Format
                            Case "}"c : state = ReplaceFSA.TransformationClose : trans.Clear() : trans.Add(New StringBuilder)
                            Case Else : state = ReplaceFSA.Transformation : trans.Clear() : trans.Add(New StringBuilder(CStr(ch)))
                        End Select
                    Case ReplaceFSA.FormatClose ':}
                        Select Case ch
                            Case "}"c : format.Append("}"c) : state = ReplaceFSA.Format
                            Case "{"c
                                ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider))
                                state = ReplaceFSA.CloseOpen
                            Case "\"c
                                ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider))
                                If cEscapes Then state = ReplaceFSA.StringBackSlash Else ret.Append("\"c) : state = ReplaceFSA.String
                            Case Else
                                ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider))
                                ret.Append(ch)
                                state = ReplaceFSA.String
                        End Select
                    Case ReplaceFSA.StringBackSlash '\ in string
                        backslashAppendTo = ret
                        backslashReturnTo = ReplaceFSA.String
                        state = ReplaceFSA.BackSlash
                        GoTo DoItAgain
                    Case ReplaceFSA.NameBackSlash '\ in name
                        backslashAppendTo = name
                        backslashReturnTo = ReplaceFSA.Name
                        state = ReplaceFSA.BackSlash
                        GoTo DoItAgain
                    Case ReplaceFSA.FormatBackSlash '\ in format
                        backslashAppendTo = format
                        backslashReturnTo = ReplaceFSA.Name
                        state = ReplaceFSA.BackSlash
                        GoTo DoItAgain
                    Case ReplaceFSA.TransformationBackSlash '\ in transformation
                        backslashAppendTo = format
                        backslashReturnTo = ReplaceFSA.Transformation
                        state = ReplaceFSA.BackSlash
                        GoTo DoItAgain
                    Case ReplaceFSA.BackSlash 'General \ processing
                        Select Case ch
                            Case "a"c : backslashAppendTo.Append(Chars.Alert) : state = backslashReturnTo
                            Case "b"c : backslashAppendTo.Append(Chars.Back) : state = backslashReturnTo
                            Case "f"c : backslashAppendTo.Append(Chars.FormFeed) : state = backslashReturnTo
                            Case "n"c : backslashAppendTo.Append(Chars.Lf) : state = backslashReturnTo
                            Case "r"c : backslashAppendTo.Append(Chars.Cr) : state = backslashReturnTo
                            Case "t"c : backslashAppendTo.Append(Chars.Tab) : state = backslashReturnTo
                            Case "v"c : backslashAppendTo.Append(Chars.VerticalTab) : state = backslashReturnTo
                            Case "."c : state = backslashReturnTo
                            Case "U"c, "u"c, "x"c, "X"c : state = ReplaceFSA.BackSlashHex1 : oldChar = ch
                            Case "0"c To "9"c : code = ch.NumericValue : state = ReplaceFSA.BackSlashDec
                            Case Else : backslashAppendTo.Append(ch) : state = backslashReturnTo
                        End Select
                    Case ReplaceFSA.BackSlashHex1 '\u, \U, \x, \X
                        Select Case ch
                            Case "0"c To "9"c : code = ch.NumericValue : state = ReplaceFSA.BackSlashHex
                            Case "a"c, "b"c, "c"c, "d"c, "e"c, "f"c : code = AscW(ch) - AscW("a"c) + 10 : state = ReplaceFSA.BackSlashHex
                            Case "A"c, "B"c, "C"c, "D"c, "E"c, "F"c : code = AscW(ch) - AscW("A"c) + 10 : state = ReplaceFSA.BackSlashHex
                            Case Else : backslashAppendTo.Append(oldChar) : state = backslashReturnTo : GoTo DoItAgain
                        End Select
                    Case ReplaceFSA.BackSlashDec '\[0-9]
                        Select Case ch
                            Case "0"c To "9"c : code = code * 10 + ch.NumericValue
                            Case Else
                                Try
                                    backslashAppendTo.Append(Char.ConvertFromUtf32(code))
                                Catch ex As ArgumentOutOfRangeException
                                    Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, code), ex)
                                End Try
                                state = backslashReturnTo
                                GoTo DoItAgain
                        End Select
                    Case ReplaceFSA.BackSlashHex '\[uUxX][0-9a-fA-F]
                        Select Case ch
                            Case "0"c To "9"c : code = code * 16 + ch.NumericValue
                            Case "a"c, "b"c, "c"c, "d"c, "e"c, "f"c : code = code * 16 + (AscW(ch) - AscW("a"c) + 10)
                            Case "A"c, "B"c, "C"c, "D"c, "E"c, "F"c : code = code * 16 + (AscW(ch) - AscW("A"c) + 10)
                            Case Else
                                Try
                                    backslashAppendTo.Append(Char.ConvertFromUtf32(code))
                                Catch ex As ArgumentOutOfRangeException
                                    Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, code), ex)
                                End Try
                                state = backslashReturnTo
                                GoTo DoItAgain
                        End Select
                    Case ReplaceFSA.NameOpen '{ in name
                        name.Append("{"c)
                        Select Case ch
                            Case "\"c : If cEscapes Then state = ReplaceFSA.NameBackSlash Else ret.Append("\c")
                            Case ":"c : state = ReplaceFSA.NameColon
                            Case ","c : state = ReplaceFSA.NameComma
                            Case "}"c : state = ReplaceFSA.NameClose
                            Case "{"c : state = ReplaceFSA.Name
                            Case "|"c : state = ReplaceFSA.NamePipe
                            Case Else : name.Append(ch) : state = ReplaceFSA.Name
                        End Select
                    Case ReplaceFSA.FormatOpen '{ in format
                        format.Append("{"c)
                        Select Case ch
                            Case "}"c : state = ReplaceFSA.FormatClose
                            Case "{"c : state = ReplaceFSA.Format
                            Case "\"c : If cEscapes Then state = ReplaceFSA.FormatBackSlash Else format.Append("\"c)
                            Case "|"c : state = ReplaceFSA.FormatPipe
                            Case Else : format.Append(ch) : state = ReplaceFSA.Format
                        End Select
                    Case ReplaceFSA.TransformationOpen '{ in transformation
                        trans(trans.Count - 1).Append("{")
                        Select Case ch
                            Case "}"c : state = ReplaceFSA.TransformationClose
                            Case "{"c : state = ReplaceFSA.Transformation
                            Case "\"c : If cEscapes Then state = ReplaceFSA.TransformationBackSlash Else format.Append("\"c)
                            Case "|"c : state = ReplaceFSA.TransformationPipe
                            Case Else : trans(trans.Count - 1).Append(ch) : state = ReplaceFSA.Transformation
                        End Select
                    Case ReplaceFSA.Transformation
                        Select Case ch
                            Case "|"c : state = ReplaceFSA.TransformationPipe
                            Case "\"c : If cEscapes Then state = ReplaceFSA.TransformationBackSlash Else trans(trans.Count - 1).Append("\"c)
                            Case "}"c : state = ReplaceFSA.TransformationClose
                            Case "{"c : state = ReplaceFSA.TransformationOpen
                            Case Else : trans(trans.Count - 1).Append(ch)
                        End Select
                    Case ReplaceFSA.TransformationPipe
                        Select Case ch
                            Case "|"c : trans(trans.Count - 1).Append(ch) : state = ReplaceFSA.Transformation
                            Case Else : trans.Add(New StringBuilder(CStr(ch))) : state = ReplaceFSA.Transformation
                        End Select
                    Case ReplaceFSA.TransformationClose '|}
                        Select Case ch
                            Case "}"c : trans(trans.Count - 1).Append("}"c) : state = ReplaceFSA.Transformation
                            Case "{"c
                                ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider, trans))
                                state = ReplaceFSA.CloseOpen
                            Case "\"c
                                ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider, trans))
                                If cEscapes Then state = ReplaceFSA.StringBackSlash Else ret.Append("\"c) : state = ReplaceFSA.String
                            Case Else
                                ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider, trans))
                                ret.Append(ch)
                                state = ReplaceFSA.String
                        End Select
                End Select
            Next

            Select Case state
                Case ReplaceFSA.String 'OK
                Case ReplaceFSA.Open : ret.Append("}c")
                Case ReplaceFSA.NameClose : ret.Append(FormatInternal$(name.ToString, Nothing, 0, getValue, provider))
                Case ReplaceFSA.FormatClose : ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider))
                Case ReplaceFSA.Name, ReplaceFSA.NameComma, ReplaceFSA.AlignPlus, ReplaceFSA.AlignMinus, ReplaceFSA.AlignMinusZero, ReplaceFSA.Align,
                     ReplaceFSA.NameColon, ReplaceFSA.Format, ReplaceFSA.NamePipe, ReplaceFSA.Transformation, ReplaceFSA.FormatPipe, ReplaceFSA.TransformationBackSlash,
                     ReplaceFSA.TransformationOpen, ReplaceFSA.TransformationPipe
                    Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                Case ReplaceFSA.StringBackSlash : ret.Append("\"c) 'Should not happen
                Case ReplaceFSA.NameBackSlash, ReplaceFSA.FormatBackSlash : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString) 'Should not happen
                Case ReplaceFSA.BackSlash
                    Select Case backslashReturnTo
                        Case ReplaceFSA.String : ret.Append("\"c)
                        Case Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End Select
                Case ReplaceFSA.BackSlashHex1
                    Select Case backslashReturnTo
                        Case ReplaceFSA.String : ret.Append(oldChar)
                        Case Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End Select
                Case ReplaceFSA.BackSlashDec
                    Select Case backslashReturnTo
                        Case ReplaceFSA.String
                            Try
                                ret.Append(Char.ConvertFromUtf32(code))
                            Catch ex As ArgumentOutOfRangeException
                                Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, code), ex)
                            End Try
                        Case Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End Select
                Case ReplaceFSA.Close : ret.Append("}"c)
                Case ReplaceFSA.BackSlashHex
                    Select Case backslashReturnTo
                        Case ReplaceFSA.String
                            Try
                                ret.Append(Char.ConvertFromUtf32(code))
                            Catch ex As ArgumentOutOfRangeException
                                Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, code), ex)
                            End Try
                        Case Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End Select
                Case ReplaceFSA.TransformationClose
                    ret.Append(FormatInternal$(name.ToString, format.ToString, align, getValue, provider, trans))
            End Select
            Return ret.ToString
        End Function

        ''' <summary>Formats object using format string, width and format provider</summary>
        ''' <param name="name">name of object to be format. Parameter for <paramref name="getValue"/>.</param>
        ''' <param name="align">Specifies minimal width of returned string. Negative for left align, positive for right align.</param>
        ''' <param name="format">Format string for value</param>
        ''' <param name="getValue">Function that can get object to be formatted by name. <paramref name="name"/> is passed here.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <param name="trans">Identifiers of transformations to apply in given order</param>
        ''' <returns>
        ''' Formatted <paramref name="getValue"/>(<paramref name="name"/>).
        ''' If argument is null or <paramref name="getValue"/> throws an exception an empty string is used; if it is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used; if it is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used; otherwise <see cref="System.[Object].ToString"/>.
        ''' After formatting, value if widhtened to <paramref name="align"/>.
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="getValue"/> is null.</exception>
        ''' <exception cref="FormatException"><paramref name="format"/> is invalid according to object being formatted.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format][|transformation]}. See <see cref="StringFormatting"/> for details.
        ''' If <paramref name="getValue"/> throws an exception for particular name null is used as value for that placeholder.
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Private Function FormatInternal$(name$, format$, align%, getValue As Func(Of String, Object), provider As IFormatProvider, Optional trans As IEnumerable(Of StringBuilder) = Nothing)
            If getValue Is Nothing Then Throw New ArgumentNullException("getValue")
            Dim value As Object
            Try
                value = getValue(name)
            Catch
                value = Nothing
            End Try

            Dim ret As String
            If value Is Nothing Then
                ret = ""
            ElseIf TypeOf value Is ICustomFormatter Then
                ret = DirectCast(value, ICustomFormatter).Format(format, value, provider)
            ElseIf TypeOf value Is IFormattable Then
                ret = DirectCast(value, IFormattable).ToString(format, provider)
            Else
                ret = value.ToString
            End If
            If ret.Length >= Math.Abs(align) Then
                'Do nothing
            ElseIf align > 0 Then 'Right
                ret = New String(" "c, align - ret.Length) & ret
            Else 'Left
                ret &= New String(" "c, -align - ret.Length)
            End If
            'Apply transformations
            If trans IsNot Nothing Then
                For Each tr In trans
                    Dim trs = tr.ToString
                    If trs <> "" Then
                        Dim trf As Func(Of String, String) = Nothing
                        If Not Transformations.TryGetValue(trs, trf) Then Throw New FormatException(String.Format("Transformation {0} is not defined", trs))
                        If trf IsNot Nothing Then
                            ret = trf(ret)
                        End If
                    End If
                Next
            End If
            Return ret
        End Function

        ''' <summary>Replaces placeholders with formatting in given string</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="getValue">A function that provides values of placeholders. Names of placeholders are passed here and values are expected to be returned.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="getValue"/> is null</exception>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' If <paramref name="getValue"/> throws an exception for particular name null is used as value for that placeholder.
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, getValue As Func(Of String, Object), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, getValue, False, provider)
        End Function
        ''' <summary>Replaces placeholders with formatting in given string and also replaces C#-style backslash (\) espace sequences</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="getValue">A function that provides values of placeholders. Names of placeholders are passed here and values are expected to be returned.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="getValue"/> is null</exception>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' If <paramref name="getValue"/> throws an exception for particular name null is used as value for that placeholder.
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, getValue As Func(Of String, Object), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, getValue, True, provider)
        End Function

#Region "IDictionary"
        ''' <summary>Replaces placeholders with formatting in given string using values dictionary</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">Dictionary of values to be replaced. Key are names of placeholders specified in <paramref name="pattern"/>. If null all values are treated as null.</param>
        ''' <param name="cEscapes">True to allow C#-style backslash (\) escaping. False not to allow it.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Null is supplied as value for nonexistent keys.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Friend Function ReplaceInternal(pattern As String, values As IDictionary(Of String, Object), cEscapes As Boolean, Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern,
                                   Function(name)
                                       If values Is Nothing Then Return Nothing
                                       Dim ret As Object = Nothing
                                       If values.TryGetValue(name, ret) Then Return ret
                                       Return Nothing
                                   End Function,
                                   cEscapes, provider
                                  )
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using values dictionary</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">Dictionary of values to be replaced. Key are names of placeholders specified in <paramref name="pattern"/>. If null all values are treated as null.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Null is supplied as value for nonexistent keys.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, values As IDictionary(Of String, Object), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using values dictionary and also replaces C#-style backslash (\) character espace sequences</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">Dictionary of values to be replaced. Key are names of placeholders specified in <paramref name="pattern"/>. If null all values are treated as null.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Null is supplied as value for nonexistent keys.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, values As IDictionary(Of String, Object), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, values, True, provider)
        End Function
#End Region

#Region "IIndexable"
        ''' <summary>Replaces placeholders with formatting in given string using values collection</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">Collection of values to be replaced. Indices are names of placeholders specified in <paramref name="pattern"/>. If null all values are treated as null.</param>
        ''' <param name="cEscapes">True to allow C#-style backslash (\) escaping. False not to allow it.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>If indexer of <paramref name="values"/> throws an exception null value is supplied for that placeholder.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Friend Function ReplaceInternal(pattern As String, values As IIndexable(Of String, Object), cEscapes As Boolean, provider As IFormatProvider) As String
            Return ReplaceInternal(pattern, Function(name) If(values Is Nothing, Nothing, values(name)), cEscapes, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using values collection</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">Collection of values to be replaced. Indices are names of placeholders specified in <paramref name="pattern"/>. If null all values are treated as null.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>If indexer of <paramref name="values"/> throws an exception null value is supplied for that placeholder.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, values As IIndexable(Of String, Object), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using values collection and also replaces C#-style backslash (\) character escape sequences</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">Collection of values to be replaced. Indices are names of placeholders specified in <paramref name="pattern"/>. If null all values are treated as null.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>If indexer of <paramref name="values"/> throws an exception null value is supplied for that placeholder.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, values As IIndexable(Of String, Object), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, values, True, provider)
        End Function
#End Region

#Region "ParamArray Object()"
        ''' <summary>Replaces placeholders with formatting in given string using combined keys-values array.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at even indices (0, 2, 4, etc.). Values are at odd indices (1, 3, 5, etc.). If length of array is odd last value for last item is assumed to be null.
        ''' <para>Items at odd indices (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="cEscapes">True to allow C#-style backslash (\) escaping. False not to allow it.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Friend Function ReplaceInternal(pattern As String, values As Object(), cEscapes As Boolean, provider As IFormatProvider) As String
            If values Is Nothing Then Return ReplaceInternal(pattern, Function(a) Nothing, cEscapes, provider)
            Dim dic As New Dictionary(Of String, Object)
            For i = 0 To values.Length - 1 Step 2
                If values(i) Is Nothing Then Continue For
                Dim key$
                If TypeOf values(i) Is ICustomFormatter Then
                    key = DirectCast(values(i), ICustomFormatter).Format(Nothing, values, provider)
                ElseIf TypeOf values(i) Is IFormattable Then
                    key = DirectCast(values(i), IFormattable).ToString(provider)
                Else
                    key = values(i).ToString
                End If
                If dic.ContainsKey(key) Then Continue For
                If i + 1 >= values.Length Then
                    dic.Add(key, Nothing)
                Else
                    dic.Add(key, values(i + 1))
                End If
            Next
            Return ReplaceInternal(pattern, dic, cEscapes, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using vcombined keys-values array.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at even indices (0, 2, 4, etc.). Values are at odd indices (1, 3, 5, etc.). If length of array is odd last value for last item is assumed to be null.
        ''' <para>Items at odd indices (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, provider As IFormatProvider, ParamArray values As Object()) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using combined keys-values array and also replaces C#-style backslash (\) character escape sequences.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at even indices (0, 2, 4, etc.). Values are at odd indices (1, 3, 5, etc.). If length of array is odd last value for last item is assumed to be null.
        ''' <para>Items at odd indices (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, provider As IFormatProvider, ParamArray values As Object()) As String
            Return ReplaceInternal(pattern, values, True, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using combined keys-values array. Uses current culture.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at even indices (0, 2, 4, etc.). Values are at odd indices (1, 3, 5, etc.). If length of array is odd last value for last item is assumed to be null.
        ''' <para>Items at odd indices (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and current culture as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and current culture as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, ParamArray values As Object()) As String
            Return ReplaceInternal(pattern, values, False, Nothing)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using combined keys-values array and also replaces C#-style backslash (\) character escape sequences. Uses current culture.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at even indices (0, 2, 4, etc.). Values are at odd indices (1, 3, 5, etc.). If length of array is odd last value for last item is assumed to be null.
        ''' <para>Items at odd indices (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and current culture as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and current culture as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, ParamArray values As Object()) As String
            Return ReplaceInternal(pattern, values, True, Nothing)
        End Function
#End Region

#Region "Object(,)"
        ''' <summary>Replaces placeholders with formatting in given string using two dimensional array.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i, 0). Values are are at index (i, 1). (Other indices are ignored. So, the array normally has rank 2 in 2nd dimension. If it does not have rank at least 2 in 2nd dimmension all values are treated as null.)
        ''' <para>Items in 1st dimension (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="cEscapes">True to allow C#-style backslash (\) escaping. False not to allow it.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Friend Function ReplaceInternal(pattern As String, values As Object(,), cEscapes As Boolean, provider As IFormatProvider) As String
            If values Is Nothing Then Return ReplaceInternal(pattern, Function(a) Nothing, cEscapes, provider)
            Dim dic As New Dictionary(Of String, Object)
            If values.GetLength(1) >= 2 Then
                For i = 0 To values.GetLength(0) - 1
                    If values(i, 0) Is Nothing Then Continue For
                    Dim key$
                    If TypeOf values(i, 0) Is ICustomFormatter Then
                        key = DirectCast(values(i, 0), ICustomFormatter).Format(Nothing, values, provider)
                    ElseIf TypeOf values(i, 0) Is IFormattable Then
                        key = DirectCast(values(i, 0), IFormattable).ToString(provider)
                    Else
                        key = values(i, 0).ToString
                    End If
                    If dic.ContainsKey(key) Then Continue For
                    dic.Add(key, values(i, 1))
                Next
            End If
            Return ReplaceInternal(pattern, dic, cEscapes, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using two dimensional array.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i, 0). Values are are at index (i, 1). (Other indices are ignored. So, the array normally has rank 2 in 2nd dimension. If it does not have rank at least 2 in 2nd dimmension all values are treated as null.)
        ''' <para>Items in 1st dimension (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, values As Object(,), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using two dimensional array. Also replaces C#-style backslash (\) character escape sequences.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i, 0). Values are are at index (i, 1). (Other indices are ignored. So, the array normally has rank 2 in 2nd dimension. If it does not have rank at least 2 in 2nd dimmension all values are treated as null.)
        ''' <para>Items in 1st dimension (names) are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, values As Object(,), Optional provider As IFormatProvider = Nothing) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function
#End Region

#Region "Object()()"
        ''' <summary>Replaces placeholders with formatting in given string using array or arrays.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i)(0). Values are are at index (i)(1). (Other indices at 2nd level are ignored. So, the nested array normaly has length 2. If it does not have lenght at least 2 all values are treated as null.)
        ''' <para>Items at index 0 of nested array are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="cEscapes">True to allow C#-style backslash (\) escaping. False not to allow it.</param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Friend Function ReplaceInternal(pattern As String, values As Object()(), cEscapes As Boolean, provider As IFormatProvider) As String
            If values Is Nothing Then Return ReplaceInternal(pattern, Function(a) Nothing, cEscapes, provider)
            Dim dic As New Dictionary(Of String, Object)
            If values.GetLength(1) >= 2 Then
                For Each array In values
                    If array Is Nothing OrElse array.Length = 0 Then Continue For
                    Dim key$
                    If TypeOf array(0) Is ICustomFormatter Then
                        key = DirectCast(array(0), ICustomFormatter).Format(Nothing, values, provider)
                    ElseIf TypeOf array(0) Is IFormattable Then
                        key = DirectCast(array(0), IFormattable).ToString(provider)
                    Else
                        key = array(0).ToString
                    End If
                    If dic.ContainsKey(key) Then Continue For
                    If array.Length <= 1 Then
                        dic.Add(key, Nothing)
                    Else
                        dic.Add(key, array(1))
                    End If
                Next
            End If
            Return ReplaceInternal(pattern, dic, cEscapes, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using array or arrays.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i)(0). Values are are at index (i)(1). (Other indices at 2nd level are ignored. So, the nested array normaly has length 2. If it does not have lenght at least 2 all values are treated as null.)
        ''' <para>Items at index 0 of nested array are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, provider As IFormatProvider, ParamArray values As Object()()) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using array or arrays. Also replaces C#-style backslash (\) character escape sequences.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i)(0). Values are are at index (i)(1). (Other indices at 2nd level are ignored. So, the nested array normaly has length 2. If it does not have lenght at least 2 all values are treated as null.)
        ''' <para>Items at index 0 of nested array are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <param name="provider">Formatting provider. When null current culture is used.</param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, provider As IFormatProvider, ParamArray values As Object()()) As String
            Return ReplaceInternal(pattern, values, False, provider)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using array or arrays. Uses current culture.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i)(0). Values are are at index (i)(1). (Other indices at 2nd level are ignored. So, the nested array normaly has length 2. If it does not have lenght at least 2 all values are treated as null.)
        ''' <para>Items at index 0 of nested array are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function Replace(pattern As String, ParamArray values As Object()()) As String
            Return ReplaceInternal(pattern, values, False, Nothing)
        End Function

        ''' <summary>Replaces placeholders with formatting in given string using array or arrays. Also replaces C#-style backslash (\) character escape sequences. Uses current culture.</summary>
        ''' <param name="pattern">A string that contains placeholders to be replaced</param>
        ''' <param name="values">
        ''' Array that contains names and values of items for replacement.
        ''' Names are at index (i)(0). Values are are at index (i)(1). (Other indices at 2nd level are ignored. So, the nested array normaly has length 2. If it does not have lenght at least 2 all values are treated as null.)
        ''' <para>Items at index 0 of nested array are expected to be strings. If they are not they are converted to string:</para>
        ''' <list type="number">
        ''' <item>Null names are skipped.</item>
        ''' <item>If name is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used (null as format string, actual name as value and <paramref name="provider"/> as format provider)</item>
        ''' <item>If name is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used (passing null as format and <paramref name="provider"/> as format provider)</item>
        ''' <item>Otherwise <see cref="System.Object.ToString"/> is used.</item>
        ''' </list>
        ''' <para>If <paramref name="values"/> is null all values are treated as null.</para>
        ''' </param>
        ''' <returns><paramref name="pattern"/> with placeholders replaced with their values.</returns>
        ''' <exception cref="FormatException">Composite format string is invalid -or- Individual format specified for placeholder is invalid.</exception>
        ''' <remarks>
        ''' Specify placeholders in format {name[,align][:format]}. See <see cref="StringFormatting"/> for details.
        ''' <para>Values of nonexistend names are treated as null. In case of duplicate names in <paramref name="values"/> 1st wins.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension>
        Public Function CReplace(pattern As String, ParamArray values As Object()()) As String
            Return ReplaceInternal(pattern, values, False, Nothing)
        End Function
#End Region
#End Region

        Private ReadOnly _transformations As New Dictionary(Of String, Func(Of String, String))(StringComparer.InvariantCultureIgnoreCase) From {
            {"Html", AddressOf HttpUtility.HtmlEncode},
            {"HtmlAttribute", AddressOf HttpUtility.HtmlAttributeEncode},
            {"Url", AddressOf HttpUtility.UrlEncode},
            {"UrlPath", AddressOf HttpUtility.UrlPathEncode},
            {"SQL", AddressOf Escaping.EscapeSql},
            {"Xml", AddressOf Escaping.EscapeXml},
            {"XmlAttribute", AddressOf Escaping.EscapeXmlAttribute},
            {"JS", AddressOf Escaping.EscapeJavaScript},
            {"String.Format", AddressOf Escaping.EscapeStringFormat},
            {"MySql", AddressOf Escaping.EscapeMySql},
            {"PostrgreSql", Function(str) Escaping.EscapePostgreSql(Str, Mode.Native)},
            {"SqlLike", AddressOf Escaping.EscapeSqlLike},
            {"C#", AddressOf Escaping.EscapeCSharp},
            {"C", AddressOf Escaping.EscapeC},
            {"PHPSingle", AddressOf Escaping.EscapePhpSingle},
            {"PHPDouble", AddressOf Escaping.EscapePhpDouble},
            {"VBLike", AddressOf Escaping.EscapeVBLike},
            {"CSS", AddressOf Escaping.EscapeCss},
            {"RegEx", AddressOf Escaping.EscapeRegEx}
        }

        ''' <summary>Gets transformations registered for string formatting</summary>
        ''' <remarks>
        ''' You can register your own transformations here. You can also unregister or replace existing transformations. Beware that transformations are registered globaly.
        ''' A transformations is a function that accepts a string and returns a string. It should never fail. It should accept nulls and empty strings as well.
        ''' <para>Key of the dictionary are case-insensitive</para>
        ''' <para>Predefined transformations are</para>
        ''' <list type="table">
        ''' <listheader><term>key</term><description>Implementation</description></listheader>
        ''' <term><item>Html</item><description><see cref="HttpUtility.HtmlEncode"/></description></term>
        ''' <term><item>HtmlAttribute</item><description><see cref="HttpUtility.HtmlAttributeEncode"/></description></term>
        ''' <term><item>Url</item><description><see cref="HttpUtility.UrlEncode"/></description></term>
        ''' <term><item>UrlPath</item><description><see cref="HttpUtility.UrlPathEncode"/></description></term>
        ''' <term><item>SQL</item><description><see cref="Escaping.EscapeSql"/></description></item>
        ''' <term><item>Xml</item><description><see cref="Escaping.EscapeXml"/></description></item>
        ''' <term><item>XmlAttribute</item><description><see cref="Escaping.EscapeXmlAttribute"/></description></item>
        ''' <term><item>JS</item><description><see cref="Escaping.EscapeJavaScript"/></description></item>
        ''' <term><item>String.Format</item><description><see cref="Escaping.EscapeStringFormat"/></description></item>
        ''' <term><item>MySql</item><description><see cref="Escaping.EscapeMySql"/></description></item>
        ''' <term><item>PostrgreSql</item><description><see cref="Escaping.EscapePostgreSql"/> (uses <see cref="Escaping.Mode.Native"/> mode)</description></item>
        ''' <term><item>SqlLike</item><description><see cref="Escaping.EscapeSqlLike"/></description></item>
        ''' <term><item>C#</item><description><see cref="Escaping.EscapeCSharp"/></description></item>
        ''' <term><item>C</item><description><see cref="Escaping.EscapeC"/></description></item>
        ''' <term><item>PHPSingle</item><description><see cref="Escaping.EscapePhpSingle"/></description></item>
        ''' <term><item>PHPDouble</item><description><see cref="Escaping.EscapePhpDouble"/></description></item>
        ''' <term><item>VBLike</item><description><see cref="Escaping.EscapeVBLike"/></description></item>
        ''' <term><item>CSS</item><description><see cref="Escaping.EscapeCss"/></description></item>
        ''' <term><item>RegEx</item><description><see cref="Escaping.EscapeRegEx"/></description></item>
        ''' </list>
        ''' All functions use default parameters unless specified otherwise.
        ''' </remarks>
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        Public ReadOnly Property Transformations As IDictionary(Of String, Func(Of String, String))
            Get
                Return _transformations
            End Get
        End Property
    End Module
#End If
End Namespace
