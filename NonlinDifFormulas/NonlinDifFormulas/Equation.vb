Imports System.Globalization, System.Threading
Imports Tools.VisualBasic
Imports System.ComponentModel
''' <summary>Reprezentuje jedno zadání pro diferenciální rovnice včetně měřítka a počátečních podmínek</summary>
<DebuggerDisplay("{Name}: dx={dx}, dy={dy}")> _
Public Class Equation
    ''' <summary>Diferenční schémata</summary>
    Public Enum enmDifSch As Integer
        ''' <summary>Newtonovo diferenční schéma</summary>
        Newton = 0
        ''' <summary>Diferenční schéma RK</summary>
        RK = 1
    End Enum
    ''' <summary>Osy</summary>
    Public Enum enmAxes As Integer
        ''' <summary>x</summary>
        x
        ''' <summary>y</summary>
        y
        ''' <summary>z</summary>
        z
        ''' <summary>u</summary>
        u
        ''' <summary>t (čas)</summary>
        t
    End Enum
#Region "XML const"
    ''' <summary>Názevg XML parametrru x-axe</summary>
    Private Const XMLXAxe As String = "x-axe"
    ''' <summary>Názevg XML parametrru y-axe</summary>
    Private Const XMLYAxe As String = "y-axe"
    ''' <summary>Názevg XML parametrru t-min</summary>
    Private Const XMLTmin As String = "t-min"
    ''' <summary>Názevg XML parametrru t-max</summary>
    Private Const XMLTmax As String = "t-max"
    ''' <summary>Názevg XML parametrru Δt</summary>
    Private Const XMLΔt As String = "Δt"
    ''' <summary>Název XML parametru x-min</summary>
    Private Const XMLXmin As String = "x-min"
    ''' <summary>Název XML parametru x-max</summary>
    Private Const XMLYmin As String = "y-min"
    ''' <summary>Název XML parametru y-max</summary>
    Private Const XMLYmax As String = "y-max"
    ''' <summary>Název XML parametru x-max</summary>
    Private Const XMLXmax As String = "x-max"
    ''' <summary>Název XML parametru start-x</summary>
    Private Const XMLStartx As String = "start-x"
    ''' <summary>Název XML parametru start-y</summary>
    Private Const XMLStarty As String = "start-y"
    ''' <summary>Název XML parametru start-y</summary>
    Private Const XMLStartz As String = "start-z"
    ''' <summary>Název XML parametru start-y</summary>
    Private Const XMLStartu As String = "start-u"
    ''' <summary>Náízev XML tagu dx</summary>
    Private Const XMLdx As String = "dx"
    ''' <summary>Název XML tagu dy</summary>
    Private Const XMLdy As String = "dy"
    ''' <summary>Název XML tagu dy</summary>
    Private Const XMLdz As String = "dz"
    ''' <summary>Název XML tagu dy</summary>
    Private Const XMLdu As String = "du"
    ''' <summary>Název XML parametru name</summary>
    Private Const XMLName As String = "name"
    ''' <summary>Název XML tagu equa</summary>
    Friend Const XMLEqua As String = "equa"
    ''' <summary>Název XML parametru Schema</summary>
    Private Const XMLSchema As String = "schema"
#End Region
#Region "Fields"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="AxeX"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _AxeX As enmAxes = enmAxes.x
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="AxeY"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _AxeY As enmAxes = enmAxes.y
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="MinX"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _MinX As Single = -10.0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="MinY"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _MinY As Single = -10.0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="MaxY"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _MaxY As Single = 10.0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="MaxX"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _MaxX As Single = 10.0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="StartX"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _StartX As Single = 0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="StartY"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _StartY As Single = 0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="StartZ"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _StartZ As Single = 0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="StartU"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _StartU As Single = 0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="dx"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _dx As String = "0"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="dy"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _dy As String = "0"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="du"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _du As String = "0"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="dz"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _dz As String = "0"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="Name"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Name As String = "no-name"
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="Tmin"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Tmin As Single = 0.0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="Tmax"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Tmax As Single = 100.0
    ''' <summary>Udržuje hodnotu vlastnosti <see cref="Δt"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Δt As Single = 1.0
    ''' <summary>Udžuje hodnotu vlastnosti <see cref="DifSch"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _DifSch As enmDifSch = enmDifSch.RK
#End Region
#Region "Property"
    ''' <summary>Přiřazení osy y</summary>
    <DefaultValue(GetType(enmAxes), "y")> _
    Public Property AxeY() As enmAxes
        <DebuggerStepThrough()> Get
            Return _AxeY
        End Get
        <DebuggerStepThrough()> Set(ByVal value As enmAxes)
            _AxeY = value
        End Set
    End Property
    ''' <summary>Přiřazení osy x</summary>
    <DefaultValue(GetType(enmAxes), "x")> _
    Public Property AxeX() As enmAxes
        <DebuggerStepThrough()> Get
            Return _AxeX
        End Get
        <DebuggerStepThrough()> Set(ByVal value As enmAxes)
            _AxeX = value
        End Set
    End Property
    ''' <summary>Diferenční schéma</summary>
    <DefaultValue(GetType(enmDifSch), "Newton")> _
    Public Property DifSch() As enmDifSch
        <DebuggerStepThrough()> Get
            Return _DifSch
        End Get
        <DebuggerStepThrough()> Set(ByVal value As enmDifSch)
            _DifSch = value
        End Set
    End Property
    ''' <summary>Krok času</summary>
    <DefaultValue(1.0)> _
    Public Property Δt() As Single
        <DebuggerStepThrough()> Get
            Return _Δt
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _Δt = value
        End Set
    End Property
    ''' <summary>Konec času</summary>
    <DefaultValue(100.0)> _
    Public Property Tmax() As Single
        <DebuggerStepThrough()> Get
            Return _Tmax
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _Tmax = value
        End Set
    End Property
    ''' <summary>Počátek času</summary>
    <DefaultValue(0.0)> _
    Public Property Tmin() As Single
        <DebuggerStepThrough()> Get
            Return _Tmin
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _Tmin = value
        End Set
    End Property
    ''' <summary>Název rovnice</summary>
    <DefaultValue("no-name")> _
    Public Property Name() As String
        <DebuggerStepThrough()> Get
            Return _Name
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    ''' <summary>Pravá strana pro dy</summary>
    <DefaultValue("0")> _
    Public Property Dy() As String
        <DebuggerStepThrough()> Get
            Return _dy
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _dy = value
        End Set
    End Property
    ''' <summary>Pravá strana pro dx</summary>
    <DefaultValue("0")> _
    Public Property Dx() As String
        <DebuggerStepThrough()> Get
            Return _dx
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _dx = value
        End Set
    End Property
    ''' <summary>Pravá strana pro dz</summary>
    <DefaultValue("0")> _
    Public Property Dz() As String
        <DebuggerStepThrough()> Get
            Return _dz
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _dz = value
        End Set
    End Property
    ''' <summary>Pravá strana pro du</summary>
    <DefaultValue("0")> _
    Public Property Du() As String
        <DebuggerStepThrough()> Get
            Return _du
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _du = value
        End Set
    End Property
    ''' <summary>Počáteční podmínka pro Y</summary>
    <DefaultValue(0.0)> _
    Public Property StartY() As Single
        <DebuggerStepThrough()> Get
            Return _StartY
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _StartY = value
        End Set
    End Property
    ''' <summary>Počáteční podmínka pro X</summary>
    <DefaultValue(0.0)> _
    Public Property StartX() As Single
        <DebuggerStepThrough()> Get
            Return _StartX
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _StartX = value
        End Set
    End Property
    ''' <summary>Počáteční podmínka pro Z</summary>
    <DefaultValue(0.0)> _
    Public Property StartZ() As Single
        <DebuggerStepThrough()> Get
            Return _StartZ
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _StartZ = value
        End Set
    End Property
    ''' <summary>Počáteční podmínka pro U</summary>
    <DefaultValue(0.0)> _
    Public Property StartU() As Single
        <DebuggerStepThrough()> Get
            Return _StartU
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _StartU = value
        End Set
    End Property
    ''' <summary>Maximum X - vykreslovací okno</summary>
    <DefaultValue(10.0)> _
    Public Property MaxX() As Single
        <DebuggerStepThrough()> Get
            Return _MaxX
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _MaxX = value
        End Set
    End Property
    ''' <summary>Maximum Y - vykreslovací okno</summary>
    <DefaultValue(10.0)> _
    Public Property MaxY() As Single
        <DebuggerStepThrough()> Get
            Return _MaxY
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _MaxY = value
        End Set
    End Property
    ''' <summary>Minimum Y - vykreslovací okno</summary>
    <DefaultValue(-10.0)> _
    Public Property MinY() As Single
        <DebuggerStepThrough()> Get
            Return _MinY
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _MinY = value
        End Set
    End Property
    ''' <summary>Minimum X - vykreslovací okno</summary>
    <DefaultValue(-10.0)> _
    Public Property MinX() As Single
        <DebuggerStepThrough()> Get
            Return _MinX
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Single)
            _MinX = value
        End Set
    End Property
#End Region
    ''' <summary>Default CTor</summary>
    Public Sub New()
    End Sub
    ''' <summary>Stringová reprezentace</summary>
    ''' <returns><see cref="Name"/></returns>
    Public Overrides Function ToString() As String
        Return Name
    End Function

#Region "XML"
    ''' <summary>CTor z XML elementu</summary>
    ''' <param name="el">XML element &lt;equa> s definicí zadání</param>
    Public Sub New(ByVal el As Xml.XmlElement)
        Dim oldc As CultureInfo = Thread.CurrentThread.CurrentCulture
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture
        Try
            MinX = el.GetAttribute(XMLXmin)
            MinY = el.GetAttribute(XMLYmin)
            MaxY = el.GetAttribute(XMLYmax)
            MaxX = el.GetAttribute(XMLXmax)
            StartX = el.GetAttribute(XMLStartx)
            StartY = el.GetAttribute(XMLStarty)
            StartZ = el.GetAttribute(XMLStartz)
            StartU = el.GetAttribute(XMLStartu)
            Tmax = el.GetAttribute(XMLTmax)
            Tmin = el.GetAttribute(XMLTmin)
            Δt = el.GetAttribute(XMLΔt)
            Name = el.GetAttribute(XMLName)
            DifSch = [Enum].Parse(GetType(enmDifSch), el.GetAttribute(XMLSchema))
            AxeX = [Enum].Parse(GetType(enmAxes), el.GetAttribute(XMLXAxe))
            AxeY = [Enum].Parse(GetType(enmAxes), el.GetAttribute(XMLYAxe))
            Dx = el.GetElementsByTagName(XMLdx)(0).InnerText
            Dy = el.GetElementsByTagName(XMLdy)(0).InnerText
            Dz = el.GetElementsByTagName(XMLdz)(0).InnerText
            Du = el.GetElementsByTagName(XMLdu)(0).InnerText
        Finally
            Thread.CurrentThread.CurrentCulture = oldc
        End Try
    End Sub
    ''' <summary>Převod na XML</summary>
    ''' <param name="Doc"><see cref="xml.XmlDocument"/> v němž mají být elementy vytvářeny</param>
    ''' <returns>XML element &lt;equa> reprezentující aktuální instanci</returns>
    Public Function ToXml(ByVal Doc As Xml.XmlDocument) As Xml.XmlElement
        Dim oldc As CultureInfo = Thread.CurrentThread.CurrentCulture
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture
        Try
            Dim el As Xml.XmlElement = Doc.CreateElement(XMLEqua)
            el.Attributes.Append(Doc.CreateAttribute(XMLXmin)).Value = MinX
            el.Attributes.Append(Doc.CreateAttribute(XMLYmin)).Value = MinY
            el.Attributes.Append(Doc.CreateAttribute(XMLYmax)).Value = MaxY
            el.Attributes.Append(Doc.CreateAttribute(XMLXmax)).Value = MaxX
            el.Attributes.Append(Doc.CreateAttribute(XMLStartx)).Value = StartX
            el.Attributes.Append(Doc.CreateAttribute(XMLStarty)).Value = StartY
            el.Attributes.Append(Doc.CreateAttribute(XMLStartu)).Value = StartZ
            el.Attributes.Append(Doc.CreateAttribute(XMLStartz)).Value = StartU
            el.Attributes.Append(Doc.CreateAttribute(XMLName)).Value = Name
            el.Attributes.Append(Doc.CreateAttribute(XMLTmin)).Value = Tmin
            el.Attributes.Append(Doc.CreateAttribute(XMLTmax)).Value = Tmax
            el.Attributes.Append(Doc.CreateAttribute(XMLΔt)).Value = Δt
            el.Attributes.Append(Doc.CreateAttribute(XMLSchema)).Value = [Enum].GetName(GetType(enmDifSch), DifSch)
            el.Attributes.Append(Doc.CreateAttribute(XMLXAxe)).Value = [Enum].GetName(GetType(enmAxes), AxeX)
            el.Attributes.Append(Doc.CreateAttribute(XMLYAxe)).Value = [Enum].GetName(GetType(enmAxes), AxeY)
            el.AppendChild(Doc.CreateElement(XMLdx)).InnerText = Dx
            el.AppendChild(Doc.CreateElement(XMLdy)).InnerText = Dy
            el.AppendChild(Doc.CreateElement(XMLdz)).InnerText = Dz
            el.AppendChild(Doc.CreateElement(XMLdu)).InnerText = Du
            Return el
        Finally
            Thread.CurrentThread.CurrentCulture = oldc
        End Try
    End Function
#End Region
End Class